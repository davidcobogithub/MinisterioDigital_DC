Imports CapaDatos
Imports System.Data
Imports System.Data.OracleClient
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports iTextSharp.text.Image


Partial Class frmLIapertura
    Inherits System.Web.UI.Page


    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hfnrocuenta.Value = Session("nrocuenta")
            hfacuerdo.Value = Session("Acuerdo")
            hfidsucursal.Value = Session("SucursalLI")

            If hfacuerdo.Value = "NO" Then
                lblpaso1.Text = "APERTURA LIBRO INSPECCION MANUAL"
                PanelActuaciones.Visible = False
                PanelApertura.Visible = False
                PanelSurcursales.Visible = True
                CargarSucursales()
            Else

                Dim ClibroInsp As New ClsLibroInspeccion
                If ClibroInsp.TieneActas(CInt(hfnrocuenta.Value)) = 0 Then
                    lblpaso1.Text = "APERTURA LIBRO INSPECCION DIGITAL - PASO 1 DATOS GENERALES"
                    PanelApertura.Visible = True
                    PanelActuaciones.Visible = False
                    PanelSurcursales.Visible = False
                    cargarcaratula()
                Else
                    lblpaso1.Text = "LIBRO INSPECCION DIGITAL"
                    ListadoActuaciones()
                    PanelApertura.Visible = False
                    PanelActuaciones.Visible = True
                    PanelSurcursales.Visible = False
                End If

            End If

        End If
    End Sub

    Public Sub cargarcaratula()
        Try
            Dim ClibroInsp As New ClsLibroInspeccion
            Dim dt As DataTable
            dt = ClibroInsp.TraerDatosCaratulaApertura(CInt(hfnrocuenta.Value), hfidsucursal.Value)

            If dt.Rows.Count > 0 Then
                txtrazon.Text = dt.Rows(0)("razon_social").ToString()
                txtfantasia.Text = dt.Rows(0)("nombre_fantasia").ToString()
                txtcuit.Text = dt.Rows(0)("cuit").ToString()
                txtfecins.Text = dt.Rows(0)("fechainscripcion").ToString()
                txtnromat.Text = dt.Rows(0)("nromatricula").ToString()

                If hfacuerdo.Value = "NO" Then
                    lbldom.Text = "Domicilio Sucursal"
                    txtdom.Text = dt.Rows(0)("DomicilioSuc").ToString()
                Else
                    lbldom.Text = "Domicilio Legal"
                    txtdom.Text = dt.Rows(0)("domicilioLegal").ToString()
                End If

                txtdomLS.Text = dt.Rows(0)("DomicilioLS").ToString()
                txtMedPres.Text = dt.Rows(0)("Medio").ToString()
                txtNroLibro.Text = hfnrocuenta.Value & hfidsucursal.Value & "AL" & hfNroLibro.Value
                hfforjuridica.Value = dt.Rows(0)("formajuridica").ToString()
                txtActPrincipal.Text = dt.Rows(0)("ActividadPrincipal").ToString()

                If dt.Rows(0)("numcentral").ToString.Trim = String.Empty Then
                    If dt.Rows(0)("RegDocUnificada").ToString.Trim = String.Empty Then
                        If dt.Rows(0)("domiciliounico").ToString.Trim = String.Empty Then

                        Else
                            Me.lbltipocentra.Text = "DOMICILIO.UNICO"
                            Me.lblnrocentral.Text = dt.Rows(0)("DomicilioUnico").ToString
                        End If

                    Else
                        Me.lbltipocentra.Text = "REG.DOC.UNIFICADA"
                        Me.lblnrocentral.Text = dt.Rows(0)("RegDocUnificada").ToString
                    End If
                Else
                    Me.lbltipocentra.Text = "NUM. CENTRALIZACIÓN"
                    Me.lblnrocentral.Text = dt.Rows(0)("NumCentral").ToString
                End If

            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub cargarcaratulaNuevaAperturaDigital()
        Try
            Dim ClibroInsp As New ClsLibroInspeccion
            Dim dt As DataTable
            dt = ClibroInsp.TraerDatosCaratulaNuevaAperturaDigital(CInt(hfnrocuenta.Value), hfidsucursal.Value)

            If dt.Rows.Count > 0 Then
                txtrazon.Text = dt.Rows(0)("razon_social").ToString()
                txtfantasia.Text = dt.Rows(0)("nombre_fantasia").ToString()
                txtcuit.Text = dt.Rows(0)("cuit").ToString()
                txtfecins.Text = dt.Rows(0)("fechainscripcion").ToString()
                txtnromat.Text = dt.Rows(0)("nromatricula").ToString()

                If hfacuerdo.Value = "NO" Then
                    lbldom.Text = "Domicilio Sucursal"
                    txtdom.Text = dt.Rows(0)("DomicilioSuc").ToString()
                Else
                    lbldom.Text = "Domicilio Legal"
                    txtdom.Text = dt.Rows(0)("domicilioLegal").ToString()
                End If

                txtdomLS.Text = dt.Rows(0)("DomicilioLS").ToString()
                txtMedPres.Text = dt.Rows(0)("Medio").ToString()
                txtNroLibro.Text = hfnrocuenta.Value & hfidsucursal.Value & "AL" & hfNroLibro.Value
                hfforjuridica.Value = dt.Rows(0)("formajuridica").ToString()
                txtActPrincipal.Text = dt.Rows(0)("ActividadPrincipal").ToString()

                Dim secuencia As Integer
                secuencia = SecuenciaDigital(dt.Rows(0)("NroLibro").ToString().Split("L")(1))
                txtNroLibro.Text = hfnrocuenta.Value & hfidsucursal.Value & "AL" & secuencia

                If dt.Rows(0)("numcentral").ToString.Trim = String.Empty Then
                    If dt.Rows(0)("RegDocUnificada").ToString.Trim = String.Empty Then
                        If dt.Rows(0)("domiciliounico").ToString.Trim = String.Empty Then

                        Else
                            Me.lbltipocentra.Text = "DOMICILIO.UNICO"
                            Me.lblnrocentral.Text = dt.Rows(0)("DomicilioUnico").ToString
                        End If

                    Else
                        Me.lbltipocentra.Text = "REG.DOC.UNIFICADA"
                        Me.lblnrocentral.Text = dt.Rows(0)("RegDocUnificada").ToString
                    End If
                Else
                    Me.lbltipocentra.Text = "NUM. CENTRALIZACIÓN"
                    Me.lblnrocentral.Text = dt.Rows(0)("NumCentral").ToString
                End If

            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function SecuenciaDigital(ByVal pnrolibro As String) As Integer
        Dim sec As Integer
        sec = CInt(IIf(pnrolibro = String.Empty, 0, pnrolibro)) + 1
        Return sec
    End Function

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Dim idacta As Integer = 0
        'Id Barandilla Manual
        Dim iddoc As Integer = 0

        Dim ClibroInsp As New ClsLibroInspeccion
        Dim trx As OracleTransaction
        Dim conn As New OracleConnection(cad1)

        If hfacuerdo.Value = "NO" Then

            ' Hago Visible Parrafo Si Solicita Audiencia
            If hfforjuridica.Value = "3" Then
                hfVisibleforjuridica.Value = "font-family: Arial, Helvetica, sans-serif; text-align:justify;font-size:12px;"
            Else
                hfVisibleforjuridica.Value = "display:none;"
            End If


            If ClibroInsp.ExisteLibroAbiertoManual(hfnrocuenta.Value, hfidsucursal.Value, 2000) = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('No Puede dar de alta mas de 2 Libros, para hacerlo debera cerrar uno');", True)
                Return
            End If

            'Imprimir Acuse Manual
            Dim lista As New ArrayList
            lista.Add(txtrazon.Text)
            lista.Add(txtfantasia.Text)
            lista.Add(txtcuit.Text)
            lista.Add(txtfecins.Text)
            lista.Add(txtnromat.Text)
            lista.Add(txtMedPres.Text)
            lista.Add(txtdomLS.Text)
            Dim param As String
            param = clsUtiles.CodificaParametros(lista, "-")

            conn.Open()
            trx = conn.BeginTransaction()

            iddoc = ClibroInsp.GuardarDocEnBDManual(ClibroInsp.htmlToPDFMemoryQR(ArmarActaHTMLManual() _
            , Server.MapPath(".") + "\Images\logo.png"), hfnrocuenta.Value, hfidsucursal.Value, Date.Now.Year, Date.Now.Month, 1, 2000, trx, hfNroLibro.Value)
            hfiddoc.Value = iddoc

            CargarSucursales()
            PanelSurcursales.Visible = True
            PanelApertura.Visible = False
            PanelActuaciones.Visible = False

            Dim popupScript As String

            popupScript = String.Empty
            popupScript = "<script language='JavaScript'>"
            popupScript += "window.open('frmPdfViewManual.aspx?op=2&origen=&idDoc=" & hfiddoc.Value.ToString() & "', 'CustomPopUp', "
            popupScript += "'top=80,left=300 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
            popupScript += "</script>"
            AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "popPdf", popupScript, False)

        Else

            Try
                If ClibroInsp.TieneActas(CInt(hfnrocuenta.Value)) = 0 Then

                    'conn.Open()
                    'trx = conn.BeginTransaction()

                    'idacta = ClibroInsp.InsertCaratula(1, ArmarActaHTML, hfnrocuenta.Value, trx)
                    'ClibroInsp.guardarDocEnBD(ClibroInsp.htmlToPDFMemory(ArmarActaHTML, Server.MapPath(".") + "\Images\logo.png"), cad1, "BarandilladocumentoPDF", hfnrocuenta.Value, Date.Now.Year, Date.Now.Month, 2000, "00", "", idacta, trx)

                    'trx.Commit()

                    PanelApertura.Visible = False
                    'PanelActuaciones.Visible = True
                    lblnrolibroDigital.Text = txtNroLibro.Text
                    PADigitalPaso2.Visible = True
                    PADigitalPaso3.Visible = False
                    PADigitalPaso4.Visible = False
                    PADigitalPaso5.Visible = False


                    'If ClibroInsp.TieneActas(CInt(hfnrocuenta.Value)) = 0 Then
                    '    PanelApertura.Visible = True
                    '    PanelActuaciones.Visible = False
                    '    cargarcaratula()
                    'Else
                    '    ListadoActuaciones()
                    '    PanelApertura.Visible = False
                    '    PanelActuaciones.Visible = True
                    'End If
                Else
                    PanelApertura.Visible = False
                    'PanelActuaciones.Visible = True
                    lblnrolibroDigital.Text = txtNroLibro.Text
                    PADigitalPaso2.Visible = True
                    PADigitalPaso3.Visible = False
                    PADigitalPaso4.Visible = False
                    PADigitalPaso5.Visible = False
                    ' Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Ud. Ya tiene generada la Apertura del Libro');", True)
                End If
            Catch ex As Exception
                Throw

                trx.Rollback()

            Finally

                'trx.Dispose()
                conn.Close()
            End Try

        End If


    End Sub

    Public Function ArmarActaHTML() As String
        Dim actahtml As String
        actahtml = "<html><head></head>" _
                   & "<body>" _
                   & "<h1 style='color: #5e9ca0; text-align: center;'>LIBRO INSPECCION ON LINE</h1>" _
                   & "<h3 style='color: #2e6c80;'>LIBRO NRO: " & txtNroLibro.Text & "</h3>" _
                   & "<h3 style='color: #2e6c80;'>RAZON SOCIAL: " & txtrazon.Text.Trim & "</h3> " _
                   & "<h3 style='color: #2e6c80;'>NOMBRE DE FANTASIA: " & txtfantasia.Text.Trim & " </h3> " _
                   & "<h3 style='color: #2e6c80;'>CUIT: " & txtcuit.Text.Trim & "</h3> " _
                   & "<h3 style='color: #2e6c80;'>FECHA DE INSCRIPCION: " & txtfecins.Text.Trim & "</h3> " _
                   & "<h3 style='color: #2e6c80;'>NRO DE MATRICULA: " & txtnromat.Text.Trim & "</h3> " _
                   & "<h3 style='color: #2e6c80;'>MED. PRESENTACION: " & txtMedPres.Text.Trim & "</h3> " _
                   & "<h3 style='color: #2e6c80;'>DOMICILIO LEGAL: " & txtdom.Text.Trim & "</h3> " _
                   & "<h3 style='color: #2e6c80;'>DOMICILIO SUELDO: " & txtdomLS.Text.Trim & "</h3> " _
                   & "<h3 style='color: #2e6c80;'>ACTIVIDAD PRINCIPAL: " & txtActPrincipal.Text.Trim & "</h3> " _
                   & "<h3 style='color: #2e6c80;'>" & lbltipocentra.Text.Trim & ": " & lblnrocentral.Text.Trim & "</h3> " _
                   & "<p>Por la presente se hace el libro de Inspecci&oacute;n</p> " _
                   & "</body> " _
                   & "</html>"
        Return actahtml
    End Function

    

    Public Function ArmarActaHTMLManual() As String
        Dim actahtml As String

        actahtml = "<html><head></head><body style='font-family: Arial, Helvetica, sans-serif'>" _
                   & "<div><h2 style='text-align: center; color: #808080;'>SOLICITUD DE APERTURA DE LIBRO DE INSPECCION</h2>" _
                   & "<table style='border: thin solid #CCCCCC; padding: 10px; width: 100%;'>" _
                   & "<tr>" _
                   & "<td style='padding: 5px; width: 150px;'>RAZON SOCIAL: </td>" _
                   & "<td style='padding: 5px'><b>" & txtrazon.Text.Trim & "</b></td> " _
                   & "</tr>" _
                   & "<tr>" _
                   & "<td style='padding: 5px; width: 150px;'>NOMBRE DE FANTASIA: </td> " _
                   & "<td style='padding: 5px'><b>" & txtfantasia.Text.Trim & "</b></td> " _
                   & "</tr>" _
                   & "<tr>" _
                   & "<td style='padding: 5px; width: 150px;'>CUIT: </td>" _
                   & "<td style='padding: 5px'><b>" & txtcuit.Text.Trim & "</b></td> " _
                   & "</tr>" _
                   & "<tr>" _
                   & "<td style='padding: 5px;width: 150px;' >FECHA DE INSCRIPCCION: </td> " _
                   & "<td style='padding: 5px'><b>" & txtfecins.Text.Trim & "</b></td> " _
                   & "</tr>" _
                   & "<tr>" _
                   & "<td style='padding: 5px; width: 150px;'>NRO. DE MATRICULA: </td>" _
                   & "<td style='padding: 5px'><b>" & txtnromat.Text.Trim & "</b></td> " _
                   & "</tr>" _
                   & "<tr> " _
                   & "<td style='padding: 5px; width: 150px;'>MED. DE PRESENTACION: </td> " _
                   & "<td style='padding: 5px'><b>MANUAL</b></td> " _
                   & "</tr> " _
                   & "<tr> " _
                   & "<td style='padding: 5px 5px 20px 5px; width: 150px;'>DOMICILIO SUCURSAL: </td> " _
                   & "<td style='padding: 5px 5px 20px 5px'><b>" & txtdom.Text.Trim & "</b></td> " _
                   & "</tr>" _
                   & "<tr> " _
                   & "<td style='padding: 5px 5px 20px 5px; width: 150px;'>DOMICILIO SUELDO: </td> " _
                   & "<td style='padding: 5px 5px 20px 5px'><b>" & txtdomLS.Text.Trim & "</b></td> " _
                   & "</tr>" _
                    & "<tr> " _
                   & "<td style='padding: 5px 5px 20px 5px; width: 150px;'>ACTIVIDAD PRINCIPAL: </td> " _
                   & "<td style='padding: 5px 5px 20px 5px'><b>" & txtActPrincipal.Text.Trim & "</b></td> " _
                   & "</tr>" _
                    & "<tr>" _
                   & "<td style='padding: 5px; width: 150px;'>" & lbltipocentra.Text.Trim & ": </td>" _
                   & "<td style='padding: 5px'><b>" & lblnrocentral.Text.Trim & "</b></td> " _
                   & "</tr>" _
                     & "<tr> " _
                   & "<td style='padding: 5px 5px 20px 5px; width: 150px;'>NRO LIBRO: </td> " _
                   & "<td style='padding: 5px 5px 20px 5px'><b>" & txtNroLibro.Text.Trim & "</b></td> " _
                   & "</tr>" _
                   & "</table>" _
                   & "<br/>" _
                   & "<div style='text-align:right;'>Firma Empleador</div>" _
                   & "</div></body></html>"

        Return actahtml
    End Function

    Public Sub ListadoActuaciones()
        Dim ClibroInsp As New ClsLibroInspeccion
        gvActuaciones.Columns(0).Visible = True
        gvActuaciones.DataSource = ClibroInsp.traerListaActuaciones(hfnrocuenta.Value)
        gvActuaciones.DataBind()
        gvActuaciones.Columns(0).Visible = False
    End Sub

    Protected Sub imgbtn_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim idDoc As Integer
        gvActuaciones.Columns(0).Visible = True
        idDoc = Convert.ToInt32(row.Cells(0).Text)
        gvActuaciones.Columns(0).Visible = False
        Dim popupScript As String

        popupScript = String.Empty
        popupScript = "<script language='JavaScript'>"
        popupScript += "window.open('frmPdfView.aspx?op=2&origen=&idDoc=" & idDoc.ToString() & "', 'CustomPopUp', "
        popupScript += "'top=80,left=300 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
        popupScript += "</script>"
        AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "popPdf", popupScript, False)
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        If hfacuerdo.Value = "NO" Then
            PanelApertura.Visible = False
            PanelActuaciones.Visible = False
            PanelSurcursales.Visible = True
            CargarSucursales()

        Else

            Dim ClibroInsp As New ClsLibroInspeccion
            If ClibroInsp.TieneActas(CInt(hfnrocuenta.Value)) = 0 Then
                PanelApertura.Visible = True
                PanelActuaciones.Visible = False
                cargarcaratula()
            Else
                ListadoActuaciones()
                PanelApertura.Visible = False
                PanelActuaciones.Visible = True
            End If

        End If

    End Sub

    Protected Sub gvActuaciones_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvActuaciones.PageIndexChanging
        gvActuaciones.PageIndex = e.NewPageIndex
        ListadoActuaciones()

    End Sub

    Protected Sub btnBandeja_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBandeja.Click
        Response.Redirect("frmLiBarandillaEmpresaLi.aspx")
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click

        hfOP.Value = "S"

        Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Ud. Esta a Punto de Realizar una nueva Apertura del Libro, Para efectuar la misma debe tener en cuenta, haber modificado algun dato de la Apertura Anterior');", True)

        If hfacuerdo.Value = "NO" Then
            'Apertura Manual
            PanelApertura.Visible = True
            PanelActuaciones.Visible = False
            cargarcaratula()
        Else
            'Apertura Digital
            Dim ClibroInsp As New ClsLibroInspeccion
            If ClibroInsp.TieneActas(CInt(hfnrocuenta.Value)) = 0 Then
                PanelApertura.Visible = True
                PanelActuaciones.Visible = False
                cargarcaratula()
            Else
                'ListadoActuaciones()
                'PanelApertura.Visible = False
                'PanelActuaciones.Visible = True
                'Cambiar el estado de la Apertura Anterior y crear una nueva
                cargarcaratulaNuevaAperturaDigital()
                PanelApertura.Visible = True
                PanelActuaciones.Visible = False
            End If
        End If


    End Sub

    Protected Sub btnAceptarCierre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarCierre.Click
        If txtmotivosCierre.Text = String.Empty Then
            Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Ingrese un Motivo del Cierre del Libro');", True)
        Else
            Dim motivo As String
            motivo = txtmotivosCierre.Text.Trim
            If motivo.Length > 470 Then
                motivo = motivo.Substring(1, 470)
            End If
            Dim ClibroInsp As New ClsLibroInspeccion
            ClibroInsp.CerrarLibroInsManual(hfnrocuenta.Value, hfidsucursal.Value, 2000, hfNroLibro.Value, motivo)
            Response.Redirect("frmLiapertura.aspx")
        End If

    End Sub

    Protected Sub lnkApertura_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim NroLibro As Integer
        Dim idsuc As String
        idsuc = row.Cells(0).Text.ToString()
        hfidsucursal.Value = idsuc

        If hfacuerdo.Value = "NO" Then
            'Apertura Manual
            Dim ClibroInsp As New ClsLibroInspeccion
            If ClibroInsp.ExisteLibroAbiertoManual(hfnrocuenta.Value, hfidsucursal.Value, 2000) = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('No Puede dar de alta mas de 2 Libros, para hacerlo debera cerrar uno');", True)
                Return
            Else

                If row.Cells(13).Text = "&nbsp;" Then
                    NroLibro = 1
                    hfNroLibro.Value = NroLibro

                    PanelSurcursales.Visible = False
                    PanelApertura.Visible = True
                    PanelActuaciones.Visible = False
                    cargarcaratula()
                Else
                    NroLibro = Int32.Parse(row.Cells(13).Text.Split("L")(1).ToString()) + 1
                    hfNroLibro.Value = NroLibro

                    PanelSurcursales.Visible = False
                    PanelApertura.Visible = True
                    PanelActuaciones.Visible = False
                    cargarcaratula()

                End If

            End If
        Else
            'Apertura Digital
            Dim ClibroInsp As New ClsLibroInspeccion
            If ClibroInsp.TieneActas(CInt(hfnrocuenta.Value)) = 0 Then
                PanelSurcursales.Visible = False
                PanelApertura.Visible = True
                PanelActuaciones.Visible = False
                cargarcaratula()
            Else
                ListadoActuaciones()
                PanelSurcursales.Visible = False
                PanelApertura.Visible = False
                PanelActuaciones.Visible = True
            End If
        End If

    End Sub

    Protected Sub lnkCierre_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim NroLibro As Integer
        If row.Cells(14).Text = "&nbsp;" Then
            If row.Cells(13).Text = "&nbsp;" Then
                ' si no posee nrolibro no se puede cerrar 
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Ud. No Puede cerrar el libro ya que no esta  generada la Apertura');", True)
            Else
                Dim idsuc As String
                idsuc = row.Cells(0).Text.ToString()
                hfidsucursal.Value = idsuc

                NroLibro = Int32.Parse(row.Cells(13).Text.Split("L")(1).ToString())
                hfNroLibro.Value = NroLibro
                titcierre.Visible = True
                titcierremotivos.Visible = False
                tithistorial.Visible = False
                txtmotivosCierre.Text = String.Empty
                btnAceptarCierre.Visible = True
                modalgvHistorial.Visible = False
                modalpanelCierre.Visible = True
                txtmotivosCierre.Enabled = True
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "openModal();", True)
            End If
        Else


        End If
    End Sub

    Public Sub CargarSucursales()
        Dim t As DataTable
        Dim param(3) As Object

        param(0) = hfnrocuenta.Value
        param(1) = 11
        param(2) = 1
        param(3) = ""

        t = OracleHelper.ExecuteDataset(cad1, "doc_laboral.pr_li_sucursales_sel", param).Tables(0)

        Session("tSucursales") = t
        GrillaDomicilios.DataSource = t
        GrillaDomicilios.DataBind()
    End Sub

    Protected Sub lnkHistorial_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim idsuc As String
        idsuc = row.Cells(0).Text.ToString()
        hfidsucursal.Value = idsuc
        Dim ClibroInsp As New ClsLibroInspeccion
        gvHistorial.DataSource = ClibroInsp.HistorialLibroInsManual(hfnrocuenta.Value, hfidsucursal.Value)
        gvHistorial.DataBind()
        titcierre.Visible = False
        tithistorial.Visible = True
        modalgvHistorial.Visible = True
        modalpanelCierre.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "openModal();", True)
    End Sub

    Protected Sub GrillaDomicilios_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrillaDomicilios.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then

            Dim rowView As DataRowView

            rowView = e.Row.DataItem


            Dim lnkHistorial As LinkButton = e.Row.FindControl("lnkHistorial")
            Dim lnkImpPdf As LinkButton = e.Row.FindControl("lnkImpPdf")
            Dim LnkMotivo As LinkButton = e.Row.FindControl("LnkMotivo")


            Dim NroLibro As Integer
            If e.Row.Cells(14).Text = "&nbsp;" Or e.Row.Cells(14).Text = "" Then
                If e.Row.Cells(13).Text = "&nbsp;" Or e.Row.Cells(13).Text = "" Then
                    ' si no posee nrolibro no se puede cerrar 
                    lnkHistorial.Visible = False
                    lnkImpPdf.Visible = False
                Else
                    NroLibro = Int32.Parse(e.Row.Cells(13).Text.Split("L")(1).ToString())
                    If NroLibro > 1 Then
                        lnkHistorial.Visible = True
                        lnkImpPdf.Visible = True
                    Else
                        lnkHistorial.Visible = False
                        lnkImpPdf.Visible = True
                    End If
                End If
                LnkMotivo.Visible = False

            Else

                LnkMotivo.Visible = True
                'e.Row.Cells(14).ToolTip = e.Row.Cells(14).Text.Trim
                e.Row.Cells(14).Text = e.Row.Cells(14).Text.Trim().Substring(0, 10)

                NroLibro = Int32.Parse(e.Row.Cells(13).Text.Split("L")(1).ToString())
                If NroLibro > 1 Then
                    lnkHistorial.Visible = True
                Else
                    lnkHistorial.Visible = False
                End If
                lnkImpPdf.Visible = False
            End If



        End If



    End Sub

    Protected Sub lnkVerPdf_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(lb.NamingContainer, GridViewRow)
        Dim popupScript As String

        Dim iddoc As Integer
        iddoc = gvHistorial.DataKeys(row.RowIndex).Values(0).ToString

        If Not row.Cells(2).Text = "&nbsp;" Then
            Dim contenido As Byte() = Nothing
            contenido = cargarpdf(0, 0, iddoc)

            Dim reader As New PdfReader(contenido)
            Dim fs As FileStream = Nothing
            Dim stamp As PdfStamper = Nothing
            Dim document As Document = Nothing

            Try
                document = New Document()
                Dim directorio As String
                directorio = Server.MapPath(".") + "\PDFTemporal\"

                Dim outputPdf As String = directorio & "LI" & hfnrocuenta.Value & ".pdf"
                fs = New FileStream(outputPdf, FileMode.Create, FileAccess.Write)
                stamp = New PdfStamper(reader, fs)

                Dim bf As BaseFont = BaseFont.CreateFont(Server.MapPath(".") + "\Fonts\ARIAL.TTF", BaseFont.CP1252, True)

                Dim gs As New PdfGState()

                gs.FillOpacity = 0.349999994F

                gs.StrokeOpacity = 0.349999994F

                For nPag As Integer = 1 To reader.NumberOfPages
                    Dim tamPagina As Rectangle = reader.GetPageSizeWithRotation(nPag)
                    Dim over As PdfContentByte = stamp.GetOverContent(nPag)
                    over.BeginText()
                    WriteTextToDocument(bf, tamPagina, over, gs, "Documento Dado de Baja")
                    over.EndText()
                Next
            Finally

                reader.Close()

                If stamp IsNot Nothing Then
                    stamp.Close()
                End If

                If fs IsNot Nothing Then
                    fs.Close()
                End If

                If document IsNot Nothing Then
                    document.Close()

                End If
            End Try


            Dim ruta As String = RutaHttp() & "PDFTemporal/" & "LI" & hfnrocuenta.Value & ".pdf"


            popupScript = String.Empty
            popupScript = "<script language='JavaScript'>"
            popupScript += "window.open('" + ruta + "', 'CustomPopUp', titlebar = 'no', "
            popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
            popupScript += "</script>"
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "popPdf", popupScript, False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "openModal();", True)

        Else

            popupScript = String.Empty
            popupScript = "<script language='JavaScript'>"
            popupScript += "window.open('frmPdfViewManual.aspx?op=2&origen=&idDoc=" & gvHistorial.DataKeys(row.RowIndex).Values(0).ToString & "', 'CustomPopUp', "
            popupScript += "'top=80,left=300 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
            popupScript += "</script>"
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "popPdf", popupScript, False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "openModal();", True)

        End If

    End Sub

#Region "MARCA DE AGUA"

    Public Function cargarpdf(ByVal op As String, ByVal origen As String, ByVal id As String) As Byte()
        Dim contenido As Byte() = Nothing

        Try
            Dim param(1) As Object
            param(0) = id
            param(1) = ""

            Dim dt As DataTable
            dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.BarandillaDocumentoPdfMGetById", param).Tables(0)

            Dim param1(1) As Object
            param1(0) = id
            param1(1) = ""

            Dim dt1 As DataTable
            dt1 = OracleHelper.ExecuteDataset(cad1, "doc_laboral.BarandillaDocPdfMGetByIdFull", param1).Tables(0)

            If dt.Rows.Count > 0 Then
                'If op = "1" Then
                contenido = dt1.Rows(0)("FOriginal")
                'Else
                '    If dt.Rows(0)("FMinisterio") = 0 Then
                '        If dt.Rows(0)("FEmpresa") = 0 Then
                '            contenido = dt1.Rows(0)("FOriginal")
                '        Else
                '            contenido = dt1.Rows(0)("FEmpresa")
                '        End If
                '    Else
                '        contenido = dt1.Rows(0)("FMinisterio")
                '    End If
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

        'Response.ContentType = "application/pdf"
        'Response.BinaryWrite(contenido)
        'Response.End()
        '*****************************************
        'Response.Buffer = False 'transmitfile self buffers
        'Response.Clear()
        'Response.ClearContent()
        'Response.ClearHeaders()
        'Response.ContentType = "application/pdf"
        ''Response.AddHeader("Content-Disposition", "attachment; filename=myfile.pdf")
        ''Response.TransmitFile(contenido) '//transmitfile keeps entire file from loading into memory
        ''Response.OutputStream.Write(contenido, 0, contenido.Length)
        'Response.Charset = "UTF-8"
        'Response.ContentEncoding = System.Text.Encoding.UTF8
        'Response.BinaryWrite(contenido)
        'Response.End()
        '*****************************************

        'Response.Clear()
        'Response.ClearHeaders()
        'Response.ClearContent()
        'Response.ContentType = "application/pdf"
        'Response.AddHeader("content-disposition", "inline;filename=report.pdf")
        'Response.BinaryWrite(contenido)
        'Response.End()
        Return contenido

    End Function

    Private Shared Sub WriteTextToDocument(ByVal bf As BaseFont, ByVal tamPagina As Rectangle, ByVal over As PdfContentByte, ByVal gs As PdfGState, ByVal texto As String)

        over.SetGState(gs)

        over.SetRGBColorFill(220, 220, 220)

        over.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_STROKE)

        over.SetFontAndSize(bf, 46)

        over.SetColorStroke(BaseColor.RED)


        Dim anchoDiag As Single = Math.Sqrt(Math.Pow((tamPagina.Height - 120), 2) + Math.Pow((tamPagina.Width - 60), 2))


        Dim porc As Single = 100 * (anchoDiag / bf.GetWidthPoint(texto, 46))


        over.SetHorizontalScaling(porc)

        Dim angPage As Double = (-1) * Math.Atan((tamPagina.Height - 60) / (tamPagina.Width - 60))

        over.SetTextMatrix(CSng(Math.Cos(angPage)), CSng(Math.Sin(angPage)), CSng((-1.0F) * Math.Sin(angPage)), CSng(Math.Cos(angPage)), 30.0F, CSng(tamPagina.Height) - 60)
        over.ShowText(texto)

    End Sub

    Public Function RutaHttp() As String
        Dim sruta As String = String.Empty
        Dim devruta As String = String.Empty
        Dim mruta() As String
        sruta = Request.Url.AbsoluteUri
        mruta = sruta.Split("/")
        For i As Integer = 0 To mruta.Length - 2
            devruta = devruta & mruta(i) & "/"
        Next
        Return devruta
    End Function

#End Region





    Protected Sub lnkImpPdf_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(lb.NamingContainer, GridViewRow)

        Dim popupScript As String

        popupScript = String.Empty
        popupScript = "<script language='JavaScript'>"
        popupScript += "window.open('frmPdfViewManual.aspx?op=2&origen=&idDoc=" & GrillaDomicilios.DataKeys(row.RowIndex).Values(0).ToString & "', 'CustomPopUp', "
        popupScript += "'top=80,left=300 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
        popupScript += "</script>"
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "popPdf", popupScript, False)


    End Sub


    Protected Sub gvHistorial_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvHistorial.PageIndexChanging

        ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "openModal();", True)
        gvHistorial.PageIndex = e.NewPageIndex
        Dim ClibroInsp As New ClsLibroInspeccion
        gvHistorial.DataSource = ClibroInsp.HistorialLibroInsManual(hfnrocuenta.Value, hfidsucursal.Value)
        gvHistorial.DataBind()
        'titcierre.Visible = False
        'tithistorial.Visible = True
        'modalgvHistorial.Visible = True
        'modalpanelCierre.Visible = False

    End Sub

    Protected Sub btnDigitalSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDigitalSI.Click
        PanelApertura.Visible = False
        PanelActuaciones.Visible = False
        PADigitalPaso2.Visible = False
        PADigitalPaso3.Visible = True
        PADigitalPaso4.Visible = False
        PADigitalPaso5.Visible = False

        Dim idacta As Integer = 0
        'Id Barandilla Manual
        Dim iddoc As Integer = 0

        Dim ClibroInsp As New ClsLibroInspeccion
        Dim trx As OracleTransaction
        Dim conn As New OracleConnection(cad1)
        Dim cConfBaneja As New ClaseConfBandeja

        If ClibroInsp.TieneActas(CInt(hfnrocuenta.Value)) = 0 Then

            conn.Open()
            trx = conn.BeginTransaction()

            idacta = ClibroInsp.InsertCaratula(1, ArmarActaHTML, hfnrocuenta.Value, txtNroLibro.Text.Trim, trx)
            iddoc = ClibroInsp.guardarDocEnBD(ClibroInsp.htmlToPDFMemory(ArmarActaHTML, Server.MapPath(".") + "\Images\logo.png"), cad1, "BarandilladocumentoPDF", hfnrocuenta.Value, Date.Now.Year, Date.Now.Month, 2000, "00", "", idacta, trx)

            ' Se asigna a Cordoba Luego se distribuye
            cConfBaneja.asignarDocumentoAUsuario(2000, iddoc, 26, hfnrocuenta.Value, trx)


            trx.Commit()
            hfiddoc.Value = iddoc

            'If ClibroInsp.TieneActas(CInt(hfnrocuenta.Value)) = 0 Then
            '    PanelApertura.Visible = True
            '    PanelActuaciones.Visible = False
            '    cargarcaratula()
            'Else
            '    ListadoActuaciones()
            '    PanelApertura.Visible = False
            '    PanelActuaciones.Visible = True
            'End If
        Else
            If hfOP.Value = "S" Then
                conn.Open()
                trx = conn.BeginTransaction()

                idacta = ClibroInsp.InsertCaratula(1, ArmarActaHTML, hfnrocuenta.Value, txtNroLibro.Text.Trim, trx)
                iddoc = ClibroInsp.guardarDocEnBD(ClibroInsp.htmlToPDFMemory(ArmarActaHTML, Server.MapPath(".") + "\Images\logo.png"), cad1, "BarandilladocumentoPDF", hfnrocuenta.Value, Date.Now.Year, Date.Now.Month, 2000, "00", "", idacta, trx)

                cConfBaneja.asignarDocumentoAUsuario(2000, iddoc, 26, hfnrocuenta.Value, trx)

                trx.Commit()
                hfiddoc.Value = iddoc
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Ud. Ya tiene generada la Apertura del Libro');", True)
            End If

        End If

    End Sub

    Protected Sub btnPaso4Sig_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPaso4Sig.Click
        If validarFirmaEmpresa() = True Then
            PanelApertura.Visible = False
            PanelActuaciones.Visible = False
            PADigitalPaso2.Visible = False
            PADigitalPaso3.Visible = False
            PADigitalPaso4.Visible = True
            PADigitalPaso5.Visible = False
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('El Libro No Esta Firmado');", True)
            Return
        End If

    End Sub

    Protected Sub btnPaso5Sig_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPaso5Sig.Click
        PanelApertura.Visible = False
        PanelActuaciones.Visible = False

        PADigitalPaso2.Visible = False
        PADigitalPaso3.Visible = False
        PADigitalPaso4.Visible = False
        PADigitalPaso5.Visible = True
    End Sub

#Region "firma"

    Protected Sub ibFJava_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibFJava.Click
        Dim idsarray As New ArrayList()
        idsarray.Add(CInt(hfiddoc.Value))
        'planilla id =1
        CargarPosicion(1)
        FirmarDigitalVarios(idsarray, 1)
    End Sub

    Protected Sub ibFAct_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibFAct.Click
        Dim idsarray As New ArrayList()
        idsarray.Add(CInt(hfiddoc.Value))
        'planilla id =1
        CargarPosicion(1)
        FirmarDigitalVarios(idsarray, 2)
    End Sub

    Private Sub CargarPosicion(ByVal id As Integer)

        Dim s1, s2 As String
        s1 = ""
        s2 = ""

        Dim obj(1) As Object
        obj(0) = id
        obj(1) = ""

        Dim t As DataTable
        t = OracleHelper.ExecuteDataset(cad1, "doc_laboral.DefinicionDocGetPosicion", obj).Tables(0)

        If t.Rows.Count > 0 Then
            Session("X") = t.Rows(0)("X").ToString()
            Session("Y") = t.Rows(0)("Y").ToString()
            Session("Ubicacion") = t.Rows(0)("hojafirma").ToString()
        Else
            Session("X") = 300
            Session("Y") = 100
            Session("Ubicacion") = "Ultima"
        End If

    End Sub

    Private Sub FirmarDigitalVarios(ByVal IDDOCUMENTOS As ArrayList, ByVal opt As Integer)
        'cargamos los motivos
        Session("RazonFirma") = "Soy Autor del Documento"
        Dim ids As String = String.Empty

        Session("OP") = String.Empty

        Dim clave As New StringBuilder()

        clave.Append("BarandillaDocumentoPdf,")
        clave.Append("UPDATE,")
        clave.Append(cad1.ToString() & ",")
        clave.Append("IdBarandillaDocumento=:pIdDoc,")
        clave.Append("FEmpresa=:p_contenido,")
        clave.Append("FOriginal")

        Dim encripta As New ClaseEncripta("ENCODE123456")
        Dim cipherText As String = encripta.EncryptData(clave.ToString())

        Session("OP") = cipherText

        For i As Integer = 0 To IDDOCUMENTOS.Count - 1

            If (ids = "") Then
                ids = IDDOCUMENTOS(i).ToString()
            Else
                ids = ids & ";" & IDDOCUMENTOS(i).ToString()
            End If

        Next

        Session("OP") = Session("OP") & ";" & ids.ToString().Trim()

        Dim popscript As New StringBuilder()

        If opt = 1 Then
            popscript.Append("<script language='Javascript'>")
            popscript.Append("ref = window.open('FirmaOnlineJava.aspx', 'Custompopup', ")
            popscript.Append("'left = 5, top = 30, width = 450, height = 250, menubar = no, scrollbars = no, resizable = no');")
            popscript.Append("</script>")
        Else
            popscript.Append("<script language='Javascript'>")
            popscript.Append("ref = window.open('FirmaOnline.aspx', 'Custompopup', ")
            popscript.Append("'left = 5, top = 30, width = 450, height = 250, menubar = no, scrollbars = no, resizable = no');")
            popscript.Append("</script>")
        End If

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "script", popscript.ToString().Trim(), False)

    End Sub

    Private Function validarFirmaEmpresa() As Boolean
        Dim dt As New DataTable
        Dim obj(1) As Object
        obj(0) = Integer.Parse(hfiddoc.Value) 'hfId.Value
        obj(1) = ""

        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.BarandillaDocPdfGetByIdFull", obj).Tables(0)

        If dt.Rows(0)("FEmpresa") Is DBNull.Value Then
            Return False 'no esta firmado
        Else
            Return True 'esta firmado
        End If
    End Function

#End Region

    Protected Sub BtnDigitalNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDigitalNO.Click
        Response.Redirect("frmPantallaInicio1.aspx")
    End Sub

#Region "TASA RETRIBUTIVA"

    Protected Sub ButtonAcuseDeRecibo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAcuseDeRecibo.Click
        Dim lista As New ArrayList
        lista.Add(txtrazon.Text)
        lista.Add(txtfantasia.Text)
        lista.Add(txtcuit.Text)
        lista.Add(txtfecins.Text)
        lista.Add(txtnromat.Text)
        lista.Add(txtMedPres.Text)
        lista.Add(txtdomLS.Text)
        Dim param As String
        param = clsUtiles.CodificaParametros(lista, "-")


        Dim popupScript As String

        popupScript = String.Empty
        popupScript = "<script language='JavaScript'>"
        popupScript += "window.open('frmLIAcuseApertura.aspx?p=" & param & "', 'CustomPopUp', "
        popupScript += "'top=80,left=300 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
        popupScript += "</script>"
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "popPdf", popupScript, False)

    End Sub

    Protected Sub ButtonComprobantePago_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComprobantePago.Click

        Dim obj(1) As Object
        obj(0) = Convert.ToInt32(hfiddoc.Value)
        obj(1) = ""

        Dim dt As New DataTable
        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.pagoDocumentoGetByIdDoc", obj).Tables(0)

        Dim contenido As Byte() = Nothing

        Dim tbl As DataTable
        tbl = OracleHelper.ExecuteDataset(cad1, "doc_laboral.BarandillaDocumentoPdfGetById", obj).Tables(0)

        'lblCantHojas.Text = OracleHelper.ExecuteScalar(cad1, CommandType.Text, "select canthojas from BarandillaDocumentoPDF where idbarandilladocumento = " & hfiddoc.Value & "")

        lblCantHojas.Text = 1

        lblEstado.Text = "Pendiente"
        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("Estado").ToString() = "1" Then
                btnGenerar.Visible = False
                lblEstado.Text = "Pagado"
            End If
        End If

        'De acuerdo a la cantidad de hojas del documento se calcula el precio
        Dim dtCantHojas As New DataTable

        Dim objCant(1) As Object
        objCant(0) = tbl.Rows(0)("Iddefiniciondocumentos").ToString()
        objCant(1) = ""

        dtCantHojas = OracleHelper.ExecuteDataset(cad1, "doc_laboral.tasapordocGetByIdDefDoc", objCant).Tables(0)

        Dim cantDesde As Int64 = 0
        Dim cantHasta As Int64 = 0
        Dim cantHojas As Int64 = lblCantHojas.Text
        Dim costoTotal As Decimal = 0
        Dim costoUnitario As Decimal = 0
        Dim costoExtra As Decimal = 0
        Dim idConceptoPadre As Int64 = 0
        Dim reqCant As String = ""

        For index As Integer = 0 To dtCantHojas.Rows.Count - 1
            cantDesde = IIf(dtCantHojas.Rows(index)("cantidad_base").ToString() = "", 0, dtCantHojas.Rows(index)("cantidad_base"))
            cantHasta = IIf(dtCantHojas.Rows(index)("cantidad_hasta").ToString() = "", 0, dtCantHojas.Rows(index)("cantidad_hasta").ToString())

            'identificamos el rango
            If cantHojas >= cantDesde And cantHojas <= cantHasta Then
                lblConcepto.Text = dtCantHojas.Rows(index)("n_concepto")
                hfConSel.Value = dtCantHojas.Rows(index)("id_concepto")
                hfFVigencia.Value = dtCantHojas.Rows(index)("fecha_desde")
                idConceptoPadre = dtCantHojas.Rows(index)("id_concepto_padre")
                costoUnitario = dtCantHojas.Rows(index)("precio_base")
                costoExtra = dtCantHojas.Rows(index)("precio_extra")
                costoTotal = costoUnitario
                reqCant = dtCantHojas.Rows(index)("requiere_cantidad")
                'si la cantidad de hojas minimas del rango es mayor a uno el monto que se cobra es por hojo
                If reqCant = "S" Then
                    costoTotal = costoExtra * cantHojas
                End If
            End If

            cantDesde = 0
            cantHasta = 0
        Next

        Dim arrStr As New ArrayList
        arrStr = obtenerConceptosPadres(idConceptoPadre)
        Dim conc As String = ""
        For Each str As String In arrStr
            conc = conc + str + "<br/>"
        Next

        lblConPadre.InnerHtml = conc
        lblPrecio.Text = costoTotal.ToString()

        mpeVolante.Show()
    End Sub

    Private Function obtenerConceptosPadres(ByVal pIdConceptoPadre As Int64)
        Dim arrStr As New ArrayList

        Dim conceptoinicial As String = ""

        While pIdConceptoPadre <> 0

            Dim dt As New DataTable
            Dim obj(1) As Object
            obj(0) = pIdConceptoPadre

            dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.conceptosLPGetByIdConcepto", obj).Tables(0)

            If dt.Rows.Count > 0 Then

                pIdConceptoPadre = IIf(dt.Rows(0)("id_concepto_padre").ToString() = "", 0, dt.Rows(0)("id_concepto_padre").ToString())

                If (pIdConceptoPadre = 0) Then
                    conceptoinicial = dt.Rows(0)("n_concepto").ToString()
                    'arrStr.Add(dt.Rows(0)("n_concepto").ToString())
                    arrStr.Insert(0, conceptoinicial)
                Else
                    arrStr.Insert(0, dt.Rows(0)("n_concepto").ToString())
                End If
            Else
                pIdConceptoPadre = 0
            End If

        End While

        Return arrStr
    End Function

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click

        Dim idDoc As Int64 = hfiddoc.Value
        Dim nroTran As String = verificarTransaccion(hfiddoc.Value)
        Dim popupScript As String
        Dim host As String = ConfigurationManager.AppSettings.Item("host")
        Dim ruta As String = host & "MensajeTran.aspx"

        btnGenerarPagoLink.Visible = True

        'validamos si el documento seleccionado tiene una transaccion anterior asociada
        If nroTran = "0" Then
            'si no la tiene generamos una nueva transaccion
            Dim str As String
            str = generarTransaccion()
            Dim strarr(1) As String
            strarr = str.Split("|")

            If strarr(0) = "OK" Then
                nroTran = strarr(1)
                'recuperamos los datos de la transaccion
                Dim dt As New DataTable
                dt = ObtenerDetalleTransaccion(strarr(1))
                'guardamos en la tabla de pagobanco y barandilla(enviar parametros correctos)
                If dt.Rows.Count > 0 Then
                    GuardarPagoDoc(dt.Rows(0)("nroliquidacionoriginal").ToString(), "0", dt.Rows(0)("fecha_vencimiento").ToString(), dt.Rows(0)("obligacion").ToString(), dt.Rows(0)("importe_total").ToString(), hfiddoc.Value)
                    'redireccionar a pagina con el codigo de transaccion
                    popupScript = String.Empty
                    popupScript = "<script language='JavaScript'>"
                    popupScript += "window.open('http://tasas.cba.gov.ar/GenerarCedulon.aspx?id=" + nroTran + "', 'CustomPopUp', "
                    popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
                    popupScript += "</script>"

                    ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popup", popupScript, False)
                Else
                    'popupScript = String.Empty
                    'popupScript = "<script language='JavaScript'>"
                    'popupScript += "window.open('" + ruta + "', 'CustomPopUp', "
                    'popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
                    'popupScript += "</script>"
                    ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popup", "alert('No se encontro la tansacción.');", True)
                End If
            Else
                'popupScript = String.Empty
                'popupScript = "<script language='JavaScript'>"
                'popupScript += "window.open('" + ruta + "', 'CustomPopUp', "
                'popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
                'popupScript += "</script>"
                'ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popup", popupScript, False)
                ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popup", "alert('Error en la tansacción.');", True)
            End If
        Else
            popupScript = String.Empty
            popupScript = "<script language='JavaScript'>"
            popupScript += "window.open('http://tasas.cba.gov.ar/GenerarCedulon.aspx?id=" + nroTran + "', 'CustomPopUp', "
            popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
            popupScript += "</script>"

            ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popup", popupScript, False)
        End If
    End Sub

    Protected Sub btnGenerarPagoLink_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerarPagoLink.Click

        Dim idDoc As Int64 = hfiddoc.Value
        Dim nroTran As String = verificarTransaccion(hfiddoc.Value)
        Dim popupScript As String
        Dim host As String = ConfigurationManager.AppSettings.Item("host")
        Dim ruta As String = host & "MensajeTran.aspx"
        'validamos si el documento seleccionado tiene una transaccion anterior asociada
        If nroTran = "0" Then
            'si no la tiene generamos una nueva transaccion
            Dim str As String
            str = generarTransaccion()
            Dim strarr(1) As String
            strarr = str.Split("|")

            If strarr(0) = "OK" Then
                nroTran = strarr(1)
                'recuperamos los datos de la transaccion
                Dim dt As New DataTable
                dt = ObtenerDetalleTransaccion(strarr(1))
                'guardamos en la tabla de pagobanco y barandilla(enviar parametros correctos)
                If dt.Rows.Count > 0 Then
                    GuardarPagoDoc(dt.Rows(0)("nroliquidacionoriginal").ToString(), "0", dt.Rows(0)("fecha_vencimiento").ToString(), dt.Rows(0)("obligacion").ToString(), dt.Rows(0)("importe_total").ToString(), hfiddoc.Value)
                    'redireccionar a pagina con el codigo de transaccion
                    popupScript = String.Empty
                    popupScript = "<script language='JavaScript'>"
                    popupScript += "window.open('http://tasas.cba.gov.ar/PagosRedLink.aspx?id=" + nroTran + "', 'CustomPopUp', "
                    'popupScript += "window.open('http://d250nt32b:8123/PagosRedLink.aspx?id=" + nroTran + "', 'CustomPopUp', "
                    popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
                    popupScript += "</script>"

                    ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popup", popupScript, False)
                Else
                    'popupScript = String.Empty
                    'popupScript = "<script language='JavaScript'>"
                    'popupScript += "window.open('" + ruta + "', 'CustomPopUp', "
                    'popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
                    'popupScript += "</script>"
                    ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popup", "alert('No se encontro la tansacción.');", True)
                End If
            Else
                'popupScript = String.Empty
                'popupScript = "<script language='JavaScript'>"
                'popupScript += "window.open('" + ruta + "', 'CustomPopUp', "
                'popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
                'popupScript += "</script>"
                'ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popup", popupScript, False)
                ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popup", "alert('Error en la tansacción.');", True)
            End If
        Else
            popupScript = String.Empty
            popupScript = "<script language='JavaScript'>"
            popupScript += "window.open('http://tasas.cba.gov.ar/PagosRedLink.aspx?id=" + nroTran + "', 'CustomPopUp', "
            'popupScript += "window.open('http://d250nt32b:8123/PagosRedLink.aspx?id=" + nroTran + "', 'CustomPopUp', "
            popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
            popupScript += "</script>"

            ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popup", popupScript, False)
        End If
    End Sub

    Private Function verificarTransaccion(ByVal pIdDoc As String) As String
        Dim nroTran As String = "0"
        Dim dt As New DataTable
        Dim obj(1) As Object
        obj(0) = pIdDoc
        obj(1) = ""
        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.pagoDocumentoGetByIdDocNV", obj).Tables(0)

        If dt.Rows.Count > 0 Then
            Dim fecVenc As DateTime
            fecVenc = dt.Rows(0)("fechavencimiento").ToString()
            If DateTime.Now.Date > fecVenc.Date Then
                Dim obj1(1) As Object
                obj1(0) = dt.Rows(0)("id_pagobanco").ToString()
                obj1(1) = "3"
                OracleHelper.ExecuteDataset(cad1, "doc_laboral.pagodocumentoUpdateState", obj1)
            Else
                nroTran = dt.Rows(0)("nrotransaccion").ToString()
            End If
        End If

        Return nroTran
    End Function

    Private Function generarTransaccion() As String

        Dim str As String = ""

        Dim cuit As String = Session("cuit")
        Dim id_sexo As String = ""
        Dim nro_documento As String = ""
        Dim pai_cod_pais As String = ""
        Dim id_numero As Int64 = 0
        Dim id_concepto As Int64 = 0
        Dim fecha_desde As DateTime
        Dim cod_ente As String = ""
        Dim cantidad As Int64 = 0
        Dim importe As Int64 = 0
        Dim nro_expediente As String = ""
        Dim anio_expediente As String = ""

        cod_ente = "020"
        fecha_desde = hfFVigencia.Value
        id_concepto = hfConSel.Value
        cantidad = lblCantHojas.Text

        'validamos si es persona fisica
        'If dt.Rows(0)("tipopersoneria").ToString() = "FISICA" Then
        '    'obtener dni, convertir a entero para que no quede el cero adelante
        '    Dim objp(1) As Object
        '    Dim dni As Int64 = obtenerDNI(cuit)
        '    cuit = ""
        '    objp(0) = dni
        '    objp(1) = ""
        '    Dim dtp As New DataTable
        '    dtp = OracleHelper.ExecuteDataset(cad1, "rcivilgetbynrodocumento", objp).Tables(0)

        '    If (dtp.Rows.Count > 0) Then
        '        id_sexo = dtp.Rows(0)("id_sexo").ToString()
        '        nro_documento = dni
        '        pai_cod_pais = dtp.Rows(0)("pai_cod_pais").ToString()
        '        id_numero = dtp.Rows(0)("id_numero").ToString()
        '    End If
        'End If

        Dim con As New OracleConnection(cad1)
        Dim com As New OracleCommand

        Try

            com.Connection = con
            com.CommandType = CommandType.StoredProcedure
            com.CommandText = "tasas_servicio.sp_generar_transaccion"
            'com.CommandText = "tasas_servicio_test"
            con.Open()

            Dim p_cuit As New OracleParameter("p_cuit", OracleType.VarChar, 50)
            p_cuit.Value = cuit
            p_cuit.Direction = ParameterDirection.Input
            com.Parameters.Add(p_cuit)

            Dim p_id_sexo As New OracleParameter("p_id_sexo", OracleType.VarChar, 50)
            p_id_sexo.Value = id_sexo
            p_id_sexo.Direction = ParameterDirection.Input
            com.Parameters.Add(p_id_sexo)

            Dim p_nro_documento As New OracleParameter("p_nro_documento", OracleType.VarChar, 50)
            p_nro_documento.Value = nro_documento
            p_nro_documento.Direction = ParameterDirection.Input
            com.Parameters.Add(p_nro_documento)

            Dim p_pai_cod_pais As New OracleParameter("p_pai_cod_pais", OracleType.VarChar, 50)
            p_pai_cod_pais.Value = pai_cod_pais
            p_pai_cod_pais.Direction = ParameterDirection.Input
            com.Parameters.Add(p_pai_cod_pais)

            Dim p_id_numero As New OracleParameter("p_id_numero", OracleType.Number, 20)
            p_id_numero.Value = id_numero
            p_id_numero.Direction = ParameterDirection.Input
            com.Parameters.Add(p_id_numero)

            Dim p_id_concepto As New OracleParameter("p_id_concepto", OracleType.Number, 20)
            p_id_concepto.Value = id_concepto
            p_id_concepto.Direction = ParameterDirection.Input
            com.Parameters.Add(p_id_concepto)

            Dim p_fecha_desde As New OracleParameter("p_fecha_desde", OracleType.DateTime)
            p_fecha_desde.Value = fecha_desde
            p_fecha_desde.Direction = ParameterDirection.Input
            com.Parameters.Add(p_fecha_desde)

            Dim p_cod_ente As New OracleParameter("p_cod_ente", OracleType.VarChar, 50)
            p_cod_ente.Value = cod_ente
            p_cod_ente.Direction = ParameterDirection.Input
            com.Parameters.Add(p_cod_ente)

            Dim p_cantidad As New OracleParameter("p_cantidad", OracleType.Number, 20)
            p_cantidad.Value = cantidad
            p_cantidad.Direction = ParameterDirection.Input
            com.Parameters.Add(p_cantidad)

            Dim p_importe As New OracleParameter("p_importe", OracleType.Number, 20)
            p_importe.Value = importe
            p_importe.Direction = ParameterDirection.Input
            com.Parameters.Add(p_importe)

            Dim p_nro_expediente As New OracleParameter("p_nro_expediente", OracleType.VarChar, 50)
            p_nro_expediente.Value = nro_expediente
            p_nro_expediente.Direction = ParameterDirection.Input
            com.Parameters.Add(p_nro_expediente)

            Dim p_anio_expediente As New OracleParameter("p_anio_expediente", OracleType.VarChar, 50)
            p_anio_expediente.Value = anio_expediente
            p_anio_expediente.Direction = ParameterDirection.Input
            com.Parameters.Add(p_anio_expediente)

            Dim o_nro_transaccion As New OracleParameter("o_nro_transaccion", OracleType.VarChar, 300)
            o_nro_transaccion.Direction = ParameterDirection.Output
            com.Parameters.Add(o_nro_transaccion)

            Dim o_resultado As New OracleParameter("o_resultado", OracleType.VarChar, 300)
            o_resultado.Direction = ParameterDirection.Output
            com.Parameters.Add(o_resultado)

            com.ExecuteNonQuery()

            'str = o_resultado.Value + "|" + o_nro_transaccion.Value
            str = "OK|5010201300000053"

        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            con.Dispose()
            com.Dispose()
        End Try

        Return str
    End Function

    Private Function ObtenerDetalleTransaccion(ByVal nroTran As String) As DataTable
        Dim dt As New DataTable
        Dim obj(1) As Object
        obj(0) = nroTran
        obj(1) = ""
        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.transaccionesGetByObligacion", obj).Tables(0)
        Return dt
    End Function

    Private Sub GuardarPagoDoc(ByVal _barCode As String, ByVal _estado As String, ByVal _fecVenc As String, ByVal _nroTran As String, ByVal _importe As String, ByVal _idDoc As String)
        Dim trx As OracleTransaction = Nothing
        Dim con As New OracleConnection(cad1)
        con.Open()

        Try
            trx = con.BeginTransaction

            Dim com As New OracleCommand()
            com.CommandText = "doc_laboral.PagoDocumentoInsert"
            com.CommandType = CommandType.StoredProcedure
            com.Connection = trx.Connection
            com.Transaction = trx

            Dim pbarcode As New OracleParameter("pbarcode", OracleType.VarChar)
            pbarcode.Value = _barCode
            com.Parameters.Add(pbarcode)

            Dim pestado As New OracleParameter("pestado", OracleType.Number)
            pestado.Value = _estado
            com.Parameters.Add(pestado)

            Dim pfechavencimiento As New OracleParameter("pfechavencimiento", OracleType.DateTime)
            pfechavencimiento.Value = _fecVenc
            com.Parameters.Add(pfechavencimiento)

            Dim pnrotransaccion As New OracleParameter("pnrotransaccion", OracleType.VarChar)
            pnrotransaccion.Value = _nroTran
            com.Parameters.Add(pnrotransaccion)

            Dim pimporte As New OracleParameter("pimporte", OracleType.VarChar)
            pimporte.Value = _importe
            com.Parameters.Add(pimporte)

            Dim pidbarandilladoc As New OracleParameter("pidBarandillaDocumento", OracleType.Number)
            pidbarandilladoc.Value = _idDoc
            com.Parameters.Add(pidbarandilladoc)

            Dim p_cursor As New OracleParameter("p_cursor", OracleType.Cursor)
            p_cursor.Direction = ParameterDirection.Output
            com.Parameters.Add(p_cursor)

            Dim da As New OracleDataAdapter
            da.SelectCommand = com

            Dim dt As New DataTable
            da.Fill(dt)

            trx.Commit()
        Catch ex As Exception
            trx.Rollback()
            con.Close()
            Throw ex
        Finally
            trx.Dispose()
            con.Close()
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        mpeVolante.Dispose()
        pVolantePago.Visible = False
    End Sub

#End Region


    Protected Sub ButtonPresentar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPresentar.Click
        Dim Id As Integer
        Dim FechaPresentacion As DateTime

        Id = Convert.ToInt32(hfiddoc.Value)
        FechaPresentacion = DateTime.Now()

        ActualizarFecha(Id, FechaPresentacion)

        lblMensajesS3.Text = "El documento fue presentado correctamente"
        lblMensajesS3.Visible = True
        ButtonPresentar.Enabled = False
        btnvolver.Visible = True
        'Page.ClientScript.RegisterStartupScript(Page.GetType(), "mensaje", "alert('El documento fue presentado correctamente');", True)
    End Sub

    Private Sub ActualizarFecha(ByVal Id As Integer, ByVal FechaPresentacion As DateTime)

        Dim obj(1) As Object
        obj(0) = Id
        obj(1) = FechaPresentacion

        OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.BarandDocPDFUpdateFPres", obj)

    End Sub

    Protected Sub btnvolver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnvolver.Click
        Response.Redirect("frmLIapertura.aspx")
    End Sub

    Protected Sub GrillaDomicilios_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrillaDomicilios.PageIndexChanging
        GrillaDomicilios.PageIndex = e.NewPageIndex

        Dim t As DataTable
        Dim param(3) As Object

        param(0) = hfnrocuenta.Value
        param(1) = 11
        param(2) = 1
        param(3) = ""

        t = OracleHelper.ExecuteDataset(cad1, "doc_laboral.pr_li_sucursales_sel", param).Tables(0)
        GrillaDomicilios.DataSource = t
        GrillaDomicilios.DataBind()

    End Sub

    Protected Sub btnIrBandejaActuaciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIrBandejaActuaciones.Click
        Response.Redirect("frmLiBarandillaEmpresaLi.aspx")
    End Sub

    Protected Sub LnkMotivo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim NroLibro As Integer
        If row.Cells(14).Text = "&nbsp;" Or row.Cells(14).Text = " " Then
           
            Else
                Dim idsuc As String
                idsuc = row.Cells(0).Text.ToString()
                hfidsucursal.Value = idsuc

                NroLibro = Int32.Parse(row.Cells(13).Text.Split("L")(1).ToString())
                hfNroLibro.Value = NroLibro
            titcierre.Visible = False
            tithistorial.Visible = False
            titcierremotivos.Visible = True

                modalgvHistorial.Visible = False
            modalpanelCierre.Visible = True
            btnAceptarCierre.Visible = False
            txtmotivosCierre.Text = GrillaDomicilios.DataKeys(row.RowIndex).Values(1)

            txtmotivosCierre.Enabled = False
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "openModal();", True)
        


        End If
    End Sub
End Class

