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

Partial Class frmLIaperturaInterviene
    Inherits System.Web.UI.Page

    Dim cencr As New ClaseEncripta("1234")
    Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hfnrocuenta.Value = Session("nrocuenta")
            hfAcuerdo.Value = Session("Acuerdo")
            CargaCboAgentes(Session("UsuarioMinId"))
            panelBarandilla.Visible = False
            panelHabilita.Visible = False
            hfmodal.Value = "0"

        End If
    End Sub

    Protected Sub btnBuscador_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscador.Click
        panelHabilita.Visible = False
        panelRA.Visible = False
        panelN.Visible = False
        cargarDatosEmpresa(Me.txtBuscarEmpresa.Text.Trim)
    End Sub

    Private Sub cargarDatosEmpresa(ByVal pCuit As String)

        Dim params(1) As Object
        params(0) = pCuit
        params(1) = ""

        Dim t As New DataTable()
        t = OracleHelper.ExecuteDataset(cad1, "Doc_laboral.sp_empresagetbycuitencabezado", params).Tables(0)

        If t.Rows.Count > 0 Then
            Dim miClsEmpresas As New clsEmpresas
            Dim cantidadEmpleados As Int32 = miClsEmpresas.empresasCantidadDeEmpleados(t.Rows(0)("nrocuenta"))
            Dim numeroCuentaEmpresa As Integer

            panelBarandilla.Visible = True
            numeroCuentaEmpresa = t.Rows(0)("nrocuenta")
            hfnrocuenta.Value = numeroCuentaEmpresa
            buscarPorCuenta(hfnrocuenta.Value)
        Else
            panelBarandilla.Visible = False
            Response.Write("<script>alert('El CUIT de la Empresa No existe o No se realizo la Apertura del Libro');</script>")
            Me.txtBuscarEmpresa.Text = ""
        End If

    End Sub

    Private Sub buscarPorCuenta(ByVal pNroCuenta As String)


        Dim obj(1) As Object
        obj(0) = pNroCuenta
        obj(1) = ""

        Dim dt As New DataTable
        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.PR_LI_IntervencionLibro", obj).Tables(0)

        'grdManual.Columns(grdManual.Columns.Count - 1).Visible = True

        If dt.Rows.Count > 0 Then
            Titulopanelbarandilla.Visible = True
        Else
            Titulopanelbarandilla.Visible = False
        End If

        grdManual.DataSource = dt
        grdManual.DataBind()

    End Sub

    Protected Sub btnPdf_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim idDoc As Integer
        idDoc = Convert.ToInt32(row.Cells(0).Text)
        Dim popupScript As String

        If Not row.Cells(8).Text = "&nbsp;" Then
            Dim contenido As Byte() = Nothing
            contenido = cargarpdf(0, 0, idDoc)

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
            popupScript += "window.open('frmPdfViewManual.aspx?op=2&origen=&idDoc=" & idDoc.ToString() & "', 'CustomPopUp', "
            popupScript += "'top=80,left=300 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
            popupScript += "</script>"
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "popPdf", popupScript, False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "openModal();", True)

        End If


        'popupScript = String.Empty
        'popupScript = "<script language='JavaScript'>"
        'popupScript += "window.open('frmPdfViewManual.aspx?op=1&origen=&idDoc=" & idDoc.ToString() & "', 'CustomPopUp', "
        'popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
        'popupScript += "</script>"
        'AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "popPdf", popupScript, False)

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

    Protected Sub btnControlar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim linkBtn As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(linkBtn.NamingContainer, GridViewRow)
        Dim RowIndex As Integer
        Dim iddoc As Integer
        RowIndex = CType(linkBtn.NamingContainer, GridViewRow).RowIndex
        iddoc = CInt(row.Cells(0).Text)
        hfidbarandilla.Value = iddoc

        hfidsucursal.Value = Me.grdManual.DataKeys(RowIndex).Value().ToString()

        panelBarandilla.Visible = False
        panelHabilita.Visible = True

       

        cargarcaratula(hfidbarandilla.Value)

        If EstaIntervenido() = True Then
            btnImprimir.Visible = True
            btnGuardar.Visible = False
        Else
            btnImprimir.Visible = False
            btnGuardar.Visible = True
        End If


    End Sub

    Protected Sub btnReimprime_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim linkBtn As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(linkBtn.NamingContainer, GridViewRow)
        Dim RowIndex As Integer
        Dim iddoc As Integer
        RowIndex = CType(linkBtn.NamingContainer, GridViewRow).RowIndex

        iddoc = CInt(row.Cells(0).Text)
        hfidbarandilla.Value = iddoc

        hfidsucursal.Value = Me.grdManual.DataKeys(RowIndex).Value().ToString()

        'panelBarandilla.Visible = False
        'panelHabilita.Visible = True

        'cargarcaratula(iddoc)
        'CargarAcuseInterviene()

        hfmodal.Value = "1"
        If hfmodal.Value = "1" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalRP", "$('#modalRP').modal();", True)
            Return
        End If


    End Sub

    Protected Sub btnBandeja_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBandeja.Click
        'Oculta los campos del relevamiento anterior
        panelRA.Visible = False
        panelN.Visible = False

        txtfecAlta.Text = String.Empty
        txtNroLibroManual.Text = String.Empty
        txtfechabi.Text = String.Empty
        'cboAgente.SelectedValue = 0
        txtUsuarioAnterior.Text = String.Empty
        txtDelegacionAnterior.Text = String.Empty
        txtsticker.Text = String.Empty

        panelBarandilla.Visible = True
        panelHabilita.Visible = False
        cargarDatosEmpresa(Me.txtBuscarEmpresa.Text.Trim)
    End Sub

    Public Sub cargarcaratula(ByVal pidbarandilla As Integer)

        Try
            Dim ClibroInsp As New ClsLibroInspeccion
            Dim dt As DataTable
            dt = ClibroInsp.TraerDatosCaratulaIdbarandilla(CInt(hfnrocuenta.Value), hfidsucursal.Value, pidbarandilla)

            If dt.Rows.Count > 0 Then

                txtrazon.Text = dt.Rows(0)("razon_social").ToString()
                txtfantasia.Text = dt.Rows(0)("nombre_fantasia").ToString()
                txtcuit.Text = dt.Rows(0)("cuit").ToString()
                txtfecins.Text = dt.Rows(0)("fechainscripcion").ToString()
                txtnromat.Text = dt.Rows(0)("nromatricula").ToString()

                If hfAcuerdo.Value = "NO" Then
                    lbldom.Text = "Domicilio Sucursal"
                    txtdom.Text = dt.Rows(0)("DomicilioSuc").ToString()
                Else
                    lbldom.Text = "Domicilio Legal"
                    txtdom.Text = dt.Rows(0)("domicilioLegal").ToString()
                End If

                txtdomLS.Text = dt.Rows(0)("DomicilioLS").ToString()
                txtMedPres.Text = dt.Rows(0)("Medio").ToString()
                txtNroLibro.Text = dt.Rows(0)("Nrolibro").ToString()
                hffecalta.Value = dt.Rows(0)("fechaalta").ToString()
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


                'txtrazon.Text = dt.Rows(0)("razon_social").ToString()
                'txtfantasia.Text = dt.Rows(0)("nombre_fantasia").ToString()
                'txtcuit.Text = dt.Rows(0)("cuit").ToString()
                'txtfecins.Text = dt.Rows(0)("fechainscripcion").ToString()
                'txtnromat.Text = dt.Rows(0)("nromatricula").ToString()
                'txtdomleg.Text = dt.Rows(0)("Domicilio").ToString()
                'txtMedPres.Text = dt.Rows(0)("Medio").ToString()
                'txtNroLibro.Text = dt.Rows(0)("Nrolibro").ToString()
                'txtActPrincipal.Text = dt.Rows(0)("ActividadPrincipal").ToString()
                '' hfforjuridica.Value = dt.Rows(0)("formajuridica").ToString()
                txtfechabi.Text = Today.ToShortDateString()
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub CargarAcuseInterviene()
        Try
            Dim ClibroInsp As New ClsLibroInspeccion
            Dim dt As DataTable
            dt = ClibroInsp.TraerDatosIntervencionLibroManual(CInt(hfidbarandilla.Value))

            txtfecAlta.Text = String.Empty
            txtNroLibroManual.Text = String.Empty

            'cboAgente.SelectedValue = 0
            txtUsuarioAnterior.Text = String.Empty
            txtDelegacionAnterior.Text = String.Empty
            txtsticker.Text = String.Empty

            If dt.Rows.Count > 0 Then
                txtfechabi.Text = String.Empty
                txtfecAlta.Text = String.Empty
                txtNroLibroManual.Text = dt.Rows(0)("nrolibroant").ToString()
                txtfechabi.Text = CDate(dt.Rows(0)("fechaintervencion").ToString()).ToShortDateString()
                cboAgente.SelectedValue = dt.Rows(0)("idusuario").ToString()
                txtDelegacion.Text = dt.Rows(0)("lugaridentificacion").ToString()
                txtsticker.Text = dt.Rows(0)("stickerseguridad").ToString()

                If Not IsDBNull(dt.Rows(0)("n_datosanteriores")) Then
                    hfRA.Value = "SI"
                    SepararDatosAnteriores(dt.Rows(0)("n_datosanteriores").ToString())
                Else
                    hfRA.Value = "NO"
                End If

            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub CargaCboAgentes(ByVal pidusuario As String)
        Using oConexion As New OracleConnection(cad1)
            oConexion.Open()
            Dim oCmd As New OracleClient.OracleCommand("SELECT IDUSUARIO, TRIM(APELLIDO) || ' ' || TRIM(NOMBRE) APEYNOM FROM DOC_LABORAL.USUARIOS WHERE idUsuario = " & pidusuario & "  and idestado = 1 ORDER BY APEYNOM ")
            oCmd.Connection = oConexion
            Dim oReader As OracleClient.OracleDataReader
            oReader = oCmd.ExecuteReader
            With cboAgente
                .DataSource = oReader
                .DataTextField = "APEYNOM"
                .DataValueField = "IDUSUARIO"
                .DataBind()
                oReader.Close()
            End With
        End Using
        CargoDelegacionXusuario(pidusuario)
    End Sub

    Private Sub CargoDelegacionXusuario(ByVal pidusuario As Integer)
        Try
            Dim dt As New DataTable
            Dim obj(1) As Object
            obj(0) = pidusuario
            obj(1) = ""
            dt = OracleHelper.ExecuteDataset(cad1, "Doc_Laboral.DelegacionXidusuario", obj).Tables(0)
            If dt.Rows.Count > 0 Then
                txtDelegacion.Text = dt.Rows(0)("delegacion").ToString().Trim()
                hfcoddelegacion.Value = dt.Rows(0)("coddelegacion").ToString().Trim()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboAgente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAgente.SelectedIndexChanged
        CargoDelegacionXusuario(cboAgente.SelectedValue)
    End Sub

    Public Function ValidaCampoObligatorios() As Boolean
        Dim rta As Boolean = True

        If hfRA.Value = "SI" Then
            If txtfecAlta.Text = String.Empty Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Ingrese la Fecha de Alta del Libro');", True)
                txtfecAlta.Focus()
                Return False
            End If
            If txtNroLibroManual.Text = String.Empty Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Ingrese el Nro del Libro Manual');", True)
                txtNroLibroManual.Focus()
                Return False
            End If

            If txtUsuarioAnterior.Text = String.Empty Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Ingrese el Agente Interviniente');", True)
                txtUsuarioAnterior.Focus()
                Return False
            End If

            If txtDelegacionAnterior.Text = String.Empty Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Ingrese La Delegación Interviniente');", True)
                txtDelegacionAnterior.Focus()
                Return False
            End If

            If txtfechabi.Text = String.Empty Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Ingrese La Fecha de Habilitación');", True)
                txtfechabi.Focus()
                Return False
            End If

            If txtDelegacion.Text = String.Empty Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Seleccione El Agente Interviniente');", True)
                cboAgente.Focus()
                Return False
            End If

            If DateTime.Compare(CDate(txtfecAlta.Text), CDate(txtfechabi.Text)) > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('La Fecha de Habilitación no puede ser menor a la fecha de Apertura');", True)
                txtfecAlta.Focus()
                Return False
            End If


        End If

        If hfRA.Value = "NO" Then

            If txtfechabi.Text = String.Empty Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Ingrese La Fecha de Habilitación');", True)
                txtfechabi.Focus()
                Return False
            End If

            If txtDelegacion.Text = String.Empty Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Seleccione El Agente Interviniente');", True)
                cboAgente.Focus()
                Return False
            End If


        End If

        If txtsticker.Text = String.Empty Then
            Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Ingrese el Sticker de Seguridad');", True)
            txtsticker.Focus()
            Return False
        End If



        Return rta
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampoObligatorios() = True Then
                GuardarAcuse()
                If EstaIntervenido() = True Then
                    btnImprimir.Visible = True
                    btnGuardar.Visible = False
                Else
                    btnImprimir.Visible = False
                    btnGuardar.Visible = True
                End If
            Else
                Return
            End If

            txtfecAlta.Text = String.Empty
            txtNroLibroManual.Text = String.Empty
            txtfechabi.Text = String.Empty
            'cboAgente.SelectedValue = 0
            txtUsuarioAnterior.Text = String.Empty
            txtDelegacionAnterior.Text = String.Empty
            txtsticker.Text = String.Empty

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Btnsup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnsup.Click

        cargarcaratula(hfidbarandilla.Value)
        CargarAcuseInterviene()

        hfpos.Value = 1
        Dim clsLibroManual As New claseBarandillaManual
        Dim lista As New ArrayList

        clsLibroManual.midbarandilladocmanual = hfidbarandilla.Value
        clsLibroManual.mfechapresentacion = txtfechabi.Text.Trim
        clsLibroManual.mcodigodelegacion = hfcoddelegacion.Value
        clsLibroManual.mresponsable = cboAgente.SelectedValue
        clsLibroManual.mstickerseguridad = txtsticker.Text.Trim
        clsLibroManual.mfechaintervencion = txtfechabi.Text.Trim

        If hfRA.Value = "NO" Then
            clsLibroManual.BarandilladocManual_Update_LI()

            lista.Add(CDate(hffecalta.Value.ToString()).ToShortDateString())
            lista.Add(txtNroLibro.Text.Trim)
            lista.Add(txtfechabi.Text.Trim)
            lista.Add(cboAgente.SelectedItem.Text)
            lista.Add(txtDelegacion.Text.Trim)
            lista.Add(hfpos.Value)
            lista.Add("")

            Dim param As String
            param = clsUtiles.CodificaParametros(lista, "-")
            Response.Redirect("frmLIRptInterviene.aspx?p=" + param)



        Else

            clsLibroManual.mnrolibroant = txtNroLibroManual.Text.Trim
            Dim datosanteriores As String
            datosanteriores = " *RELEVAMIENTO ANTERIOR FECHA ALTA RUBRICA LIBRO:* " & txtfecAlta.Text.Trim & " *NRO LIBRO ANTERIOR:* " & txtNroLibroManual.Text.Trim & " *AGENTE QUE INTERVINO:* " & txtUsuarioAnterior.Text.Trim & "  *DELEGACION QUE INTERVINO:* " & txtDelegacionAnterior.Text.Trim
            clsLibroManual.mdatosanteriores = datosanteriores
            clsLibroManual.BarandilladocManual_Update_LI_Relevamiento()
            datosanteriores = String.Empty
            datosanteriores = " RELEVAMIENTO ANTERIOR FECHA ALTA RUBRICA LIBRO: " & txtfecAlta.Text.Trim & " NRO LIBRO ANTERIOR: " & txtNroLibroManual.Text.Trim & " AGENTE QUE INTERVINO: " & txtUsuarioAnterior.Text.Trim & "  DELEGACION QUE INTERVINO: " & txtDelegacionAnterior.Text.Trim


            lista.Add(CDate(hffecalta.Value.ToString()).ToShortDateString())
            lista.Add(txtNroLibro.Text.Trim)
            lista.Add(txtfechabi.Text.Trim)
            lista.Add(cboAgente.SelectedItem.Text)
            lista.Add(txtDelegacion.Text.Trim)
            lista.Add(hfpos.Value)
            lista.Add(datosanteriores)

            Dim param As String
            param = clsUtiles.CodificaParametros(lista, "-")
            Response.Redirect("frmLIRptInterviene.aspx?p=" + param)

        End If


        UpdatePanelRP.Update()

    End Sub

    Protected Sub Btnmed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnmed.Click

        cargarcaratula(hfidbarandilla.Value)
        CargarAcuseInterviene()

        hfpos.Value = 2
        Dim clsLibroManual As New claseBarandillaManual
        Dim lista As New ArrayList

        clsLibroManual.midbarandilladocmanual = hfidbarandilla.Value
        clsLibroManual.mfechapresentacion = txtfechabi.Text.Trim
        clsLibroManual.mcodigodelegacion = hfcoddelegacion.Value
        clsLibroManual.mresponsable = cboAgente.SelectedValue
        clsLibroManual.mstickerseguridad = txtsticker.Text.Trim
        clsLibroManual.mfechaintervencion = txtfechabi.Text.Trim

        If hfRA.Value = "NO" Then
            clsLibroManual.BarandilladocManual_Update_LI()

            lista.Add(CDate(hffecalta.Value.ToString()).ToShortDateString())
            lista.Add(txtNroLibro.Text.Trim)
            lista.Add(txtfechabi.Text.Trim)
            lista.Add(cboAgente.SelectedItem.Text)
            lista.Add(txtDelegacion.Text.Trim)
            lista.Add(hfpos.Value)
            lista.Add("")

            Dim param As String
            param = clsUtiles.CodificaParametros(lista, "-")
            Response.Redirect("frmLIRptInterviene.aspx?p=" + param)



        Else

            clsLibroManual.mnrolibroant = txtNroLibroManual.Text.Trim
            Dim datosanteriores As String
            datosanteriores = " *RELEVAMIENTO ANTERIOR FECHA ALTA RUBRICA LIBRO:* " & txtfecAlta.Text.Trim & " *NRO LIBRO ANTERIOR:* " & txtNroLibroManual.Text.Trim & " *AGENTE QUE INTERVINO:* " & txtUsuarioAnterior.Text.Trim & "  *DELEGACION QUE INTERVINO:* " & txtDelegacionAnterior.Text.Trim
            clsLibroManual.mdatosanteriores = datosanteriores
            clsLibroManual.BarandilladocManual_Update_LI_Relevamiento()
            datosanteriores = String.Empty
            datosanteriores = " RELEVAMIENTO ANTERIOR FECHA ALTA RUBRICA LIBRO: " & txtfecAlta.Text.Trim & " NRO LIBRO ANTERIOR: " & txtNroLibroManual.Text.Trim & " AGENTE QUE INTERVINO: " & txtUsuarioAnterior.Text.Trim & "  DELEGACION QUE INTERVINO: " & txtDelegacionAnterior.Text.Trim


            lista.Add(CDate(hffecalta.Value.ToString()).ToShortDateString())
            lista.Add(txtNroLibro.Text.Trim)
            lista.Add(txtfechabi.Text.Trim)
            lista.Add(cboAgente.SelectedItem.Text)
            lista.Add(txtDelegacion.Text.Trim)
            lista.Add(hfpos.Value)
            lista.Add(datosanteriores)

            Dim param As String
            param = clsUtiles.CodificaParametros(lista, "-")
            Response.Redirect("frmLIRptInterviene.aspx?p=" + param)

        End If


        UpdatePanelRP.Update()


    End Sub

    Protected Sub GuardarAcuse()

        Dim clsLibroManual As New claseBarandillaManual
        Dim lista As New ArrayList

        clsLibroManual.midbarandilladocmanual = hfidbarandilla.Value
        clsLibroManual.mfechapresentacion = txtfechabi.Text.Trim
        clsLibroManual.mcodigodelegacion = hfcoddelegacion.Value
        clsLibroManual.mresponsable = cboAgente.SelectedValue
        clsLibroManual.mstickerseguridad = txtsticker.Text.Trim
        clsLibroManual.mfechaintervencion = txtfechabi.Text.Trim

        If hfRA.Value = "NO" Then
            clsLibroManual.BarandilladocManual_Update_LI()

            lista.Add(CDate(hffecalta.Value.ToString()).ToShortDateString())
            lista.Add(txtNroLibro.Text.Trim)
            lista.Add(txtfechabi.Text.Trim)
            lista.Add(cboAgente.SelectedItem.Text)
            lista.Add(txtDelegacion.Text.Trim)
            lista.Add(hfpos.Value)
            lista.Add("")


        Else

            clsLibroManual.mnrolibroant = txtNroLibroManual.Text.Trim
            Dim datosanteriores As String
            datosanteriores = " *RELEVAMIENTO ANTERIOR FECHA ALTA RUBRICA LIBRO* " & txtfecAlta.Text.Trim & " *NRO LIBRO ANTERIOR* " & txtNroLibroManual.Text.Trim & " *AGENTE QUE INTERVINO* " & txtUsuarioAnterior.Text.Trim & "  *DELEGACION QUE INTERVINO* " & txtDelegacionAnterior.Text.Trim
            clsLibroManual.mdatosanteriores = datosanteriores
            clsLibroManual.BarandilladocManual_Update_LI_Relevamiento()

            lista.Add(CDate(hffecalta.Value.ToString()).ToShortDateString())
            lista.Add(txtNroLibro.Text.Trim)
            lista.Add(txtfechabi.Text.Trim)
            lista.Add(cboAgente.SelectedItem.Text)
            lista.Add(txtDelegacion.Text.Trim)
            lista.Add(hfpos.Value)
            lista.Add(datosanteriores)

       
        End If

    End Sub

    Protected Sub grdManual_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdManual.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dato As DataRowView
            dato = e.Row.DataItem
            Dim mImgRechazada As WebControls.Image = e.Row.FindControl("imgRechazado")
            Dim mImgAprobada As WebControls.Image = e.Row.FindControl("imgAprobado")
            Dim mImgFueraTermino As WebControls.Image = e.Row.FindControl("imgFueraTermino")
            Dim mBtnControlar As LinkButton = e.Row.FindControl("btnControlar")
            Dim mBtnReimprime As LinkButton = e.Row.FindControl("btnReimprime")
            Dim mBtnEditar As LinkButton = e.Row.FindControl("btnEditar")

            If dato("IDESTADODOCMANUAL") = 4 Then
                'mImgAprobada.Visible = True
                ' mImgRechazada.Visible = False
                mBtnControlar.Visible = False
                mBtnReimprime.Visible = True
                mBtnEditar.Visible = True
            Else
                'mImgAprobada.Visible = False
                'mImgRechazada.Visible = False
                mBtnControlar.Visible = True
                mBtnReimprime.Visible = False
                mBtnEditar.Visible = False
            End If

            If Not IsDBNull(dato("FUERADETERMINO")) Then
                If dato("FUERADETERMINO") = 1 Then
                    mImgFueraTermino.Visible = True
                End If
            End If

        End If

    End Sub


    Protected Sub grdManual_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdManual.PageIndexChanging
        Me.grdManual.PageIndex = e.NewPageIndex
        buscarPorCuenta(hfnrocuenta.Value)
    End Sub

    Protected Sub btnSIRelAnt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSIRelAnt.Click
        hfRA.value = "SI"
        panelRA.Visible = True
        panelN.Visible = True

        txtNroLibroManual.Text = String.Empty
        txtfecAlta.Text = String.Empty
        txtUsuarioAnterior.Text = String.Empty
        txtDelegacionAnterior.Text = String.Empty

        Dim clsLibroManual As New claseBarandillaManual
        Dim dt As New DataTable

        dt = clsLibroManual.BarandilladocManual_SELECT_LI_Relevamiento(hfidbarandilla.Value)
        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt.Rows(0)("n_datosanteriores")) Then
                SepararDatosAnteriores(dt.Rows(0)("n_datosanteriores").ToString())
            End If
        End If


    End Sub

    Protected Sub btnNORelAnt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNORelAnt.Click
        hfRA.Value = "NO"
        panelRA.Visible = False
        panelN.Visible = True
    End Sub

    Private Sub SepararDatosAnteriores(ByVal pdatosant As String)
        Try
            Dim arr As String()
            arr = pdatosant.Split("*")
            txtfecAlta.Text = arr(2)
            txtNroLibroManual.Text = arr(4)
            txtUsuarioAnterior.Text = arr(6)
            txtDelegacionAnterior.Text = arr(8)
        Catch ex As Exception
            txtfecAlta.Text = ""
            txtNroLibroManual.Text = ""
            txtUsuarioAnterior.Text = ""
            txtDelegacionAnterior.Text = ""
        End Try
    End Sub


    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim linkBtn As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(linkBtn.NamingContainer, GridViewRow)
        Dim RowIndex As Integer
        Dim iddoc As Integer
        RowIndex = CType(linkBtn.NamingContainer, GridViewRow).RowIndex

        iddoc = CInt(row.Cells(0).Text)
        hfidbarandilla.Value = iddoc

        hfidsucursal.Value = Me.grdManual.DataKeys(RowIndex).Value().ToString()

        panelBarandilla.Visible = False
        panelHabilita.Visible = True

        cargarcaratula(iddoc)
        CargarAcuseInterviene()

        If EstaIntervenido() = True Then
            btnImprimir.Visible = False
            btnGuardar.Visible = True
        Else
            btnImprimir.Visible = False
            btnGuardar.Visible = True
        End If

    End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        hfmodal.Value = "1"
        If hfmodal.Value = "1" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalRP", "$('#modalRP').modal();", True)
            Return
        End If
    End Sub

    Public Function EstaIntervenido() As Boolean
        Dim ban As Boolean = False
        Dim ClibroInsp As New ClsLibroInspeccion
        Dim dt As DataTable
        dt = ClibroInsp.TraerDatosIntervencionLibroManual(CInt(hfidbarandilla.Value))

        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("fechaintervencion").ToString() <> String.Empty Then
                ban = True
            Else
                ban = False
            End If

        End If
        Return ban
    End Function

End Class
