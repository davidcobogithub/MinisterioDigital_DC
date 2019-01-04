'Imports Microsoft.ApplicationBlocks.Data
Imports System.Data
Imports System.Data.SqlClient
Imports CapaDatos
Imports System.Data.OracleClient
Imports System.IO
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf.qrcode
Imports iTextSharp.text.pdf.codec
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Partial Class frmPantallaBarandillaDocumentosPdf
    Inherits System.Web.UI.Page

    Dim cencr As New ClaseEncripta("1234")
    Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("Acuerdo") = "NO" Then
                Dim popupScript As String = String.Empty
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "script1", "<script type='text/javascript'>alert('Ud. No puede ingresar al aplicativo digital, en virtud de haber optado por: \n No a la Presentación Digital y/o Presentación Libro Manual ');</script>")
                modalPanelOpcionDigital.Show()
            Else
                Me.PanelContenedorPrincipal.Visible = True
            End If

            GrdPdf.DataKeyNames = New String() {"idbarandilladocumento"}
            'Button1.Visible = False
            ibPresentar.Visible = False
            hfCuit.Value = Session("cuit")
            hfNroCuenta.Value = Session("nrocuenta")
            'CargarPeriodosConDoc()
        Else
            Me.PanelContenedorPrincipal.Visible = True
        End If

    End Sub

    Protected Sub GrdPdf_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdPdf.PageIndexChanging

        GrdPdf.PageIndex = e.NewPageIndex
        buscarPorPeriodos(Session("nrocuenta"), hfAnoSel.Value, hfMesSel.Value, hfDocSel.Value)

    End Sub

    Protected Sub btnPdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim idDoc As Integer
        idDoc = Convert.ToInt32(row.Cells(0).Text)

        Dim popupScript As String

        popupScript = String.Empty
        popupScript = "<script language='JavaScript'>"
        popupScript += "window.open('frmPdfView.aspx?op=2&origen=&idDoc=" & idDoc.ToString() & "', 'CustomPopUp', "
        popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
        popupScript += "</script>"
        AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "popPdf", popupScript, False)

    End Sub

    Protected Sub ibPresentar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibPresentar.Click
        Dim i, b As Integer
        b = 0
        Dim Id As Integer
        Dim FechaPresentacion As DateTime

        GrdPdf.Columns(GrdPdf.Columns.Count - 1).Visible = True

        For i = 0 To GrdPdf.Rows.Count - 1

            Dim chk As CheckBox = GrdPdf.Rows(i).FindControl("chkpresentar")
            Dim lbl As Label = GrdPdf.Rows(i).FindControl("lblfecha")
            Dim btnFecha As ImageButton = GrdPdf.Rows(i).FindControl("btnFecha")
            Dim btnFirmado As WebControls.Image = GrdPdf.Rows(i).FindControl("ImgFirmado")

            If chk.Checked = True And chk.Enabled = True Then

                chk.Checked = False
                Id = Convert.ToInt32(GrdPdf.Rows(i).Cells(0).Text)
                FechaPresentacion = DateTime.Now()
                Dim pAno As String
                Dim pMes As String
                pAno = GrdPdf.Rows(i).Cells(3).Text.Trim()
                pMes = GrdPdf.Rows(i).Cells(4).Text.Trim()

                If GrdPdf.Rows(i).Cells(GrdPdf.Columns.Count - 1).Text = "2" And btnFirmado.Visible = True Then
                    ActualizarFecha(Id, FechaPresentacion, pAno, pMes)
                    lbl.Visible = False
                    lbl.Text = Left(Date.Now(), 10).ToString()
                    btnFecha.Visible = False
                    b = 1
                    chk.Checked = True
                    'aca actualizo la fecha de entrega
                Else
                    'no se puede presentar porque requiere la firma de la empresa
                    If GrdPdf.Rows(i).Cells(GrdPdf.Columns.Count - 1).Text = "1" Then
                        lbl.Visible = False
                        lbl.Text = Left(Date.Now(), 10).ToString()
                        btnFecha.Visible = False
                        b = 1
                        ActualizarFecha(Id, FechaPresentacion, pAno, pMes)
                        chk.Checked = True
                    End If
                End If

            Else
                lbl.Visible = False
                btnFecha.Visible = False
                lbl.Text = ""
            End If

        Next

        GrdPdf.Columns(GrdPdf.Columns.Count - 1).Visible = False

        If b = 1 Then
            lblMensaje.Visible = True
            buscarPorPeriodos(Session("nrocuenta"), hfAnoSel.Value, hfMesSel.Value, hfDocSel.Value)
        Else
            lblMensaje.Visible = False
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mensaje", "alert('Debe seleccionar un documento para su presentación.');", True)
        End If
    End Sub

    Private Sub ActualizarControles()

        Dim i, b As Integer
        b = 0
        Dim Id As Integer
        Dim FechaPresentacion As DateTime

        For i = 0 To GrdPdf.Rows.Count - 1

            Dim chk As CheckBox = GrdPdf.Rows(i).FindControl("chkpresentar")
            Dim lbl As Label = GrdPdf.Rows(i).FindControl("lblfecha")
            Dim btnFecha As ImageButton = GrdPdf.Rows(i).FindControl("btnFecha")

            If chk.Checked = True Then
                Dim pAno As String
                Dim pMes As String
                pAno = GrdPdf.Rows(i).Cells(3).Text
                pMes = ""
                lbl.Visible = False
                lbl.Text = Left(Date.Now(), 10).ToString()
                btnFecha.Visible = False
                b = 1
                Id = Convert.ToInt32(GrdPdf.Rows(i).Cells(0).Text)
                FechaPresentacion = DateTime.Now()
                ActualizarFecha(Id, FechaPresentacion, pAno, pMes)
                'aca actualizo la fecha de entrega
            Else
                lbl.Visible = False
                btnFecha.Visible = False
                lbl.Text = ""
            End If
        Next

        If b = 1 Then
            lblMensaje.Visible = True
        Else
            lblMensaje.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' Funcion que se ejecuta cada vez que se bindea una fila de la grilla.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub GrdPdf_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdPdf.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dato As DataRowView
            dato = e.Row.DataItem

            Dim btnFirmar As ImageButton = e.Row.FindControl("btnFirmar")
            Dim ImgFirmado As WebControls.Image = e.Row.FindControl("ImgFirmado")
            Dim ImgFirmadoMin As WebControls.Image = e.Row.FindControl("imgFirmadoMin")
            Dim chk As CheckBox = e.Row.FindControl("chkPresentar")
            Dim ImgDiag As WebControls.Image = e.Row.FindControl("ibDiagTrab")
            Dim btnfecha As ImageButton = e.Row.FindControl("btnfecha")
            Dim ibPeso As ImageButton = e.Row.FindControl("ibPagado")
            Dim chkFirmar As CheckBox = e.Row.FindControl("chkFirmar")
            Dim btnEmitirNota As ImageButton = e.Row.FindControl("btnEmitirNota")
            Dim ibEliminar As ImageButton = e.Row.FindControl("ibEliminar")
            Dim ibFT As ImageButton = e.Row.FindControl("ibFT")
            Dim btnPdf As ImageButton = e.Row.FindControl("btnPdf")
            Dim btnQR As ImageButton = e.Row.FindControl("btnQR")

            If dato("firmar") = "NO" Then
                'SIGNIFICA QUE EL FORMULARIO NO SE PUEDE FIRMAR 
                'ADEMAS SE DEBERA OCULTAR LA IMAGEN QUE FUE FIRMADO
                btnFirmar.Visible = False
                ImgFirmado.Visible = False

                chkFirmar.Visible = False

            End If

            If dato("FEmpresa") = 0 Then
                ImgFirmado.Visible = False
                btnFirmar.Visible = True
            If dato("iddefiniciondocumentos") = 1 Then
                    btnPdf.Visible = False
            End If
			
            Else
                'aca va lo de master
                btnFirmar.Visible = False
                ImgFirmado.Visible = True
            End If

            'aqui mostramos el icono de firma del lado del ministerio si este lo firmo
            If dato("FMinisterio") = 0 Then
                ImgFirmadoMin.Visible = False
            Else
                ImgFirmadoMin.Visible = True
            End If

            If Not dato("FechaPresentacion") Is DBNull.Value Then
                chk.Checked = True
                chk.Enabled = False
                btnfecha.Visible = True
                ibEliminar.Visible = False
            Else
                ibEliminar.Visible = True
            End If

            If dato("condicion") IsNot DBNull.Value Then
                If dato("condicion") = "1" Then
                    ibFT.Visible = True
                End If
            End If

            If dato("cantObs") <> 0 Then
                Dim ImgObs As WebControls.Image = e.Row.FindControl("ImgObs")
                ImgObs.Visible = True
            End If

            If dato("diagrama") <> 0 Then
                ImgDiag.Visible = True
            End If

            If dato("cantidadfirmas") = 1 Then
                btnFirmar.Visible = False

                chkFirmar.Visible = False
            End If
            If dato("cantidadfirmas") = "2" And dato("FEmpresa") = "0" Then
                chk.Enabled = False
                chkFirmar.Visible = True
                btnFirmar.Visible = True

            End If

            If dato("estadoPago") = "1" Then
                ibPeso.Visible = True
                btnEmitirNota.Visible = False
            Else
                ibPeso.Visible = False
                If Not dato("FechaPresentacion") Is DBNull.Value Then
                    btnEmitirNota.Visible = True
                Else
                    btnEmitirNota.Visible = False
                End If
            End If
            'la tasa se puede generar siempre
            'If dato("condicion") = "1" Then
            '    btnEmitirNota.Visible = False
            'End If

            If dato("Id_Estado") IsNot DBNull.Value Then
                If dato("Id_Estado") = "8" Or dato("Id_Estado") = "9" Or dato("Id_Estado") = "10" Or dato("Id_Estado") = "2" Then
                    Dim btn As Button = e.Row.FindControl("btnAccion")
                    btn.Text = "Corregir"
                    btn.Visible = True

                    chk.Enabled = False
                    chkFirmar.Visible = False
                    ibEliminar.Visible = False



                End If

                If dato("Id_Estado") = "11" Or dato("Id_Estado") = "12" Then
                    chk.Enabled = False
                    btnFirmar.Visible = False
                    chkFirmar.Visible = False


                    chk.Enabled = False
                    btnEmitirNota.Visible = False

                    ibEliminar.Visible = False

                End If

            End If

            If IsDBNull(dato("idplanilla")) = False Then
                btnQR.Visible = True
            Else
                btnQR.Visible = False
            End If

        End If

    End Sub

    ''' <summary>
    ''' Funcion obsoleta (29/12/2012)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFirmar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim IDDOCUMENTO As Integer
        Dim contenido() As Byte

        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

        IDDOCUMENTO = Convert.ToInt32(row.Cells(0).Text)

        Dim idTipoDoc As Integer
        idTipoDoc = ObtenerTipoDocumento(IDDOCUMENTO)
        CargarPosicion(idTipoDoc)

        contenido = OracleHelper.ExecuteScalar(cad1, CommandType.Text, "select Foriginal from BarandillaDocumentoPDF where idbarandilladocumento = " & IDDOCUMENTO & "")

        Dim clave As New StringBuilder()

        clave.Append("BarandillaDocumentoPDF")
        clave.Append(",UPDATE,")
        clave.Append(cad1)
        clave.Append(",idbarandilladocumento=" & IDDOCUMENTO & ",")
        clave.Append("FEmpresa=:p_contenido")

        Dim plainText As String = clave.ToString().Trim()
        Dim password As String = "ENCODE123456"

        Dim wrapper As New ClaseEncripta(password)
        Dim cipherText As String = wrapper.EncryptData(plainText)
        wrapper = Nothing

        Dim c As String = String.Empty

        '*** modificado por pabloa

        Session("OP") = cipherText
        Session("B64") = Me.convertToBase64(contenido)
        Dim popupScript As New StringBuilder()

        popupScript.Append("<script language='JavaScript'>")
        popupScript.Append("window.open('FirmaOnlineJava.aspx', 'CustomPopUp', ")
        popupScript.Append("'left=1,top=1 ,width=1, height=1, menubar=no, scrollbars=no ,resizable=yes')")
        popupScript.Append("</script>")

        ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popupfirma", popupScript.ToString().Trim(), False)

    End Sub

    '*** modificado por pabloa

    Public Function convertToBase64(ByVal binaryData As Byte()) As String

        Dim base64String As String = String.Empty

        Try
            base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length)
        Catch generatedExceptionName As System.ArgumentNullException
            System.Console.WriteLine("Error en lectura de datos.")
        End Try

        Return base64String

    End Function


    Protected Sub btnEmitirNota_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

        timerGrilla.Enabled = False

        hfDocSel.Value = Convert.ToInt32(row.Cells(0).Text)

        Dim obj(1) As Object
        obj(0) = Convert.ToInt32(row.Cells(0).Text)
        obj(1) = ""

        Dim dt As New DataTable
        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.pagoDocumentoGetByIdDoc", obj).Tables(0)

        Dim contenido As Byte() = Nothing

        Dim tbl As DataTable
        tbl = OracleHelper.ExecuteDataset(cad1, "doc_laboral.BarandillaDocumentoPdfGetById", obj).Tables(0)

        lblCantHojas.Text = OracleHelper.ExecuteScalar(cad1, CommandType.Text, "select canthojas from BarandillaDocumentoPDF where idbarandilladocumento = " & hfDocSel.Value & "")
        lblEstado.Text = "Pendiente"
        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("Estado").ToString() = "1" Then
                btnGenerar.Visible = False
                btnGenerarPagoLink.Visible = False
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

    Protected Sub GrdPdf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdPdf.SelectedIndexChanged

    End Sub

    Protected Sub btnFecha_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

        Dim popupScript As String

        popupScript = String.Empty
        popupScript = "<script language='JavaScript'>"
        popupScript += "window.open('Acuse.aspx?IdDoc=" + row.Cells(0).Text + "', 'CustomPopUp', "
        popupScript += "'top=80,left=300 ,width=700, height=500, menubar=no, scrollbars=NO ,resizable=NO')"
        popupScript += "</script>"

        ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popCompro", popupScript.ToString(), False)

    End Sub

    ''' <summary>
    ''' Funcion que actualiza la fecha de presentacion de un documento por su id.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="FechaPresentacion"></param>
    ''' <remarks></remarks>
    Private Sub ActualizarFecha(ByVal Id As Integer, ByVal FechaPresentacion As DateTime, ByVal pAno As String, ByVal pMes As String)

        Dim obj(1) As Object
        obj(0) = Id
        obj(1) = FechaPresentacion

        OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.BarandDocPDFUpdateFPres", obj)

        If validarPeriodoPresentacion(hfDocSel.Value, pAno, pMes) Then
            Dim consulta As String = "UPDATE barandilladocumentopdf set condicion=1 where idbarandilladocumento=" & Id
            Dim conexion As New OracleConnection(cad1)
            Dim comando As New OracleCommand(consulta, conexion)
            comando.CommandType = CommandType.Text

            conexion.Open()
            comando.ExecuteNonQuery()
            conexion.Close()
            conexion.Dispose()
        End If

    End Sub

    ''' <summary>
    ''' Funcion que devuelve la definicion del documento por su id de documento.
    ''' </summary>
    ''' <param name="iddocumento"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ObtenerTipoDocumento(ByVal iddocumento As Int32) As Integer

        Dim obj(1) As Object
        obj(0) = iddocumento
        obj(1) = ""

        Dim t As DataTable
        t = OracleHelper.ExecuteDataset(cad1, "doc_laboral.BarandDocPDFGetIdDefDoc", obj).Tables(0)

        If t.Rows.Count > 0 Then
            Return Convert.ToInt32(t.Rows(0)("IdDefinicionDocumentos"))
        Else
            Return 0
        End If

    End Function

    ''' <summary>
    ''' Funcion que obtiene las coordenas del posicionamiento de la firma en el documento.
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Funcion obsoleta (29/11/2012)
    ''' </summary>
    ''' <param name="IDDOCUMENTO"></param>
    ''' <remarks></remarks>
    Private Sub firmarDigital2(ByVal IDDOCUMENTO As Int32)
        'ACA VA EL CODIGO DE BILL
        'Dim IDDOCUMENTO As Integer
        ''''''
        'Dim btn As ImageButton = CType(sender, ImageButton)
        'Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        'IDDOCUMENTO = Convert.ToInt32(row.Cells(0).Text)
        ''''''

        Dim i As Integer = -1

        Dim procID As Integer
        procID = 0
        Dim pathFirmar = "C:\Archivos de programa\Encode S.A\Firm.ar\Firm.ar.exe"
        Session("OP") = ""
        Session("B64") = ""

        Dim objpdf As New ClaseObjDocu1("C:\planilla.pdf", cad1, "BarandillaDocumentoPDF", "idbarandilladocumento", "'planilla.pdf'", "FOriginal", Session("nrocuenta"))
        Dim contenido() As Byte = objpdf.extraerContenidoDeBD(IDDOCUMENTO)

        Dim clave As New StringBuilder()

        clave.Append("BarandillaDocumentoPDF")
        clave.Append(",UPDATE,")
        clave.Append(cad1)
        clave.Append(",Id=" & IDDOCUMENTO & ",")
        clave.Append("FEmpresa=@Contenido")

        'Aca tiene que firmarse el PDF por el ActiveX.
        'Si se firmo sigue.

        Dim plainText As String = clave.ToString().Trim()
        Dim password As String = "ENCODE123456"

        Dim wrapper As New ClaseEncripta(password)
        Dim cipherText As String = wrapper.EncryptData(plainText)
        wrapper = Nothing

        Dim c As String = String.Empty

        '*** modificado por pabloa
        If Session("OP") = "" Then
            Session("OP") = cipherText
            Session("B64") = Me.convertToBase64(contenido)
        Else
            Session("OP") = Session("OP") & ";" & cipherText
            Session("B64") = Session("B64") & ";" & Me.convertToBase64(contenido)
        End If

        Dim popupScript As New StringBuilder()
        popupScript.Append("<script language='JavaScript'>")
        popupScript.Append("window.open('FirmaOnlineJava.aspx', 'CustomPopUp', ")
        popupScript.Append("'left=1,top=1 ,width=390px, height=240px, menubar=no, scrollbars=no ,resizable=no')")
        popupScript.Append("</script>")

        'Page.RegisterStartupScript("popup", popupScript.ToString().Trim())
        ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popFirma", popupScript.ToString(), False)

    End Sub

    Private Sub CargarPeriodosConDoc()
        Dim dt As New DataTable
        Dim obj(1) As Object
        obj(0) = Session("nrocuenta")
        obj(1) = ""

        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.barandillaDocGetPerConDoc", obj).Tables(0)
        gvPeriodos.Columns(5).Visible = True
        gvPeriodos.DataSource = dt
        gvPeriodos.DataBind()
        gvPeriodos.Columns(5).Visible = False
    End Sub

    Protected Sub imgVer_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        gvPeriodos.Columns(5).Visible = True

        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

        buscarPorPeriodos(Session("nrocuenta"), row.Cells(1).Text, row.Cells(2).Text, row.Cells(5).Text)
        gvPeriodos.Columns(5).Visible = False
    End Sub

    Protected Sub ImgObs_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

        Dim popupScript As New StringBuilder()
        popupScript.Append("<script language='JavaScript'>")
        popupScript.Append("window.open('frmObservaciones.aspx?idDoc=" & row.Cells(0).Text & "', 'CustomPopUp', ")
        popupScript.Append("'left=300,top=150 ,width=600px, height=500px, menubar=no, scrollbars=yes ,resizable=yes')")
        popupScript.Append("</script>")

        'Page.RegisterStartupScript("popup", popupScript.ToString().Trim())
        ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popObserva", popupScript.ToString(), False)

    End Sub

    Private Sub buscarPorPeriodos(ByVal pNroCuenta As String, ByVal pAno As String, ByVal pMes As String, ByVal pIdTipoDoc As String)

        'btnFirmarVarios.Visible = True
        'cbFirma.Visible = True

        If Not String.IsNullOrEmpty(pAno) Then

            hfAnoSel.Value = pAno
            hfMesSel.Value = pMes
            hfDocSel.Value = pIdTipoDoc

            Dim obj(4) As Object
            obj(0) = pAno
            obj(1) = pMes
            obj(2) = pNroCuenta
            obj(3) = pIdTipoDoc
            obj(4) = ""

            Dim dt As New DataTable
            dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.BARDOCPDFGETBYANOMESNROCTA", obj).Tables(0)

            If dt.Rows.Count > 0 Then
                'Button1.Visible = True
                ibPresentar.Visible = True
                ibFAct.Visible = True
                ibFJava.Visible = True
                ibFWeb.Visible = True
            Else
                'Button1.Visible = False
                ibPresentar.Visible = False
                ibFAct.Visible = False
                ibFJava.Visible = False
                ibFWeb.Visible = False
            End If
            GrdPdf.Columns(GrdPdf.Columns.Count - 1).Visible = True
            GrdPdf.DataSource = dt
            GrdPdf.DataBind()
            GrdPdf.Columns(GrdPdf.Columns.Count - 1).Visible = False
        End If
    End Sub

    Protected Sub timerGrilla_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        buscarPorPeriodos(Session("nrocuenta"), hfAnoSel.Value, hfMesSel.Value, hfDocSel.Value)
    End Sub

    Protected Sub gvPeriodos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvPeriodos.PageIndexChanging
        gvPeriodos.PageIndex = e.NewPageIndex
        CargarPeriodosConDoc()
    End Sub

    Protected Sub btnAccion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

        Dim clsEncr As New ClaseEncripta("1234")

        Dim obj(1) As Object
        obj(0) = row.Cells(0).Text
        obj(1) = ""

        Dim dt1 As New DataTable
        dt1 = OracleHelper.ExecuteDataset(cad1, "doc_laboral.BarandillaDocPdfGetByIdFull", obj).Tables(0)

        If dt1.Rows(0)("controlar") = 1 Then
            Select Case dt1.Rows(0)("IdDefinicionDocumentos")
                Case "1"
                    Dim dt As New DataTable
                    dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.barDocPdfGetIdPlanillaById", obj).Tables(0)

                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0)(0).ToString() Then
                            Response.Redirect("~/frmPlanillaHorarios.aspx?IdPlanilla=" & Server.UrlEncode(clsEncr.EncryptData(dt.Rows(0)(0).ToString().Trim())) & "&IdSuc=" & Server.UrlEncode(clsEncr.EncryptData(dt.Rows(0)(1).ToString().Trim())) & "&idDoc=" & Server.UrlEncode(clsEncr.EncryptData(row.Cells(0).Text)))
                        End If
                    End If
                Case Else
                    'Response.Redirect("~/frmPantallaSubirPDFExternos.aspx?Id=" & Server.UrlEncode(clsEncr.EncryptData(row.Cells(0).Text & "|1")))
                    Response.Redirect("~/LSAsistenteSubidaPDFEdit.aspx?Id=" & Server.UrlEncode(clsEncr.EncryptData(row.Cells(0).Text & "|1")))
            End Select
        End If

        If dt1.Rows(0)("controlar") = 2 Then
            Response.Redirect("~/frmMultinotasEdit.aspx?Id=" & Server.UrlEncode(clsEncr.EncryptData(row.Cells(0).Text)))

        End If
        
    End Sub

    Protected Sub ibDiagTrab_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim idDoc As Integer
        idDoc = Convert.ToInt32(row.Cells(0).Text)

        Dim popupScript As String

        popupScript = String.Empty
        popupScript = "<script language='JavaScript'>"
        popupScript += "window.open('frmDiagramasTrabajo.aspx?id=" & idDoc.ToString() & "', 'CustomPopUp', "
        popupScript += "'top=80,left=100 ,width=800, height=600, menubar=no, scrollbars=NO ,resizable=NO')"
        popupScript += "</script>"
        AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "popPdf", popupScript, False)
       
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim idDoc As Int64 = hfDocSel.Value
        Dim nroTran As String = verificarTransaccion(hfDocSel.Value)
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
                    GuardarPagoDoc(dt.Rows(0)("nroliquidacionoriginal").ToString(), "0", dt.Rows(0)("fecha_vencimiento").ToString(), dt.Rows(0)("obligacion").ToString(), dt.Rows(0)("importe_total").ToString(), hfDocSel.Value)
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

    Protected Sub btnGenerarPagoLink_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim idDoc As Int64 = hfDocSel.Value
        Dim nroTran As String = verificarTransaccion(hfDocSel.Value)
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
                    GuardarPagoDoc(dt.Rows(0)("nroliquidacionoriginal").ToString(), "0", dt.Rows(0)("fecha_vencimiento").ToString(), dt.Rows(0)("obligacion").ToString(), dt.Rows(0)("importe_total").ToString(), hfDocSel.Value)
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

    Protected Sub ibEliminar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim idDoc As Integer
        idDoc = Convert.ToInt32(row.Cells(0).Text)

        EliminarDocumento(idDoc)

        buscarPorPeriodos(Session("nrocuenta"), hfAnoSel.Value, hfMesSel.Value, hfDocSel.Value)

    End Sub

    Private Function ObtenerDetalleTransaccion(ByVal nroTran As String) As DataTable
        Dim dt As New DataTable
        Dim obj(1) As Object
        obj(0) = nroTran
        obj(1) = ""
        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.transaccionesGetByObligacion", obj).Tables(0)
        Return dt
    End Function

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

    Private Function validarVencimiento(ByVal idDoc As String) As Boolean
        Dim dt As New DataTable
        Dim b As Boolean = False
        Dim obj(1) As Object
        obj(0) = idDoc
        obj(1) = ""
        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.pagoDocumentoGetByIdDoc", obj).Tables(0)

        If dt.Rows.Count > 0 Then
            Dim fecVenc As DateTime
            fecVenc = dt.Rows(0)("fechavencimiento").ToString()
            If DateTime.Now.Date > fecVenc.Date Then
                b = True
            End If
        End If
        Return b
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

            str = o_resultado.Value + "|" + o_nro_transaccion.Value
            'str = "OK|00000000000000262013"

        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            con.Dispose()
            com.Dispose()
        End Try

        Return str
    End Function

    Private Function obtenerDNI(ByVal pcuit As String) As String
        Dim dni As String
        dni = pcuit.Substring(2, 8)
        Return dni
    End Function


    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        timerGrilla.Enabled = True
        mpeVolante.Dispose()
    End Sub

    ''' <summary>
    '''  Funcion que obtiene los ids seleccionados de la grilla y los guarda en un arraylist.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFirmarVarios_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim idsarray As New ArrayList()

        For i As Integer = 0 To GrdPdf.Rows.Count - 1

            Dim checkboxFir As New CheckBox()
            checkboxFir = GrdPdf.Rows(i).FindControl("chkFirmar")

            If (checkboxFir.Visible) Then

                If (checkboxFir.Checked) Then
                    Dim idDoc As Integer = -1
                    idDoc = CInt(GrdPdf.Rows(i).Cells(0).Text)

                    idsarray.Add(idDoc)

                End If

            End If

        Next

        If (idsarray.Count > 0) Then

            Dim idTipoDoc As Integer
            idTipoDoc = ObtenerTipoDocumento(idsarray.Item(0))

            CargarPosicion(idTipoDoc)
            FirmarDigitalVarios(idsarray, 1)

        End If

    End Sub

    ''' <summary>
    ''' Funcion que 
    ''' </summary>
    ''' <param name="IDDOCUMENTOS"></param>
    ''' <remarks></remarks>
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
            If opt = 2 Then
                popscript.Append("<script language='Javascript'>")
                popscript.Append("ref = window.open('FirmaOnline.aspx', 'Custompopup', ")
                popscript.Append("'left = 5, top = 30, width = 450, height = 250, menubar = no, scrollbars = no, resizable = no');")
                popscript.Append("</script>")
            Else
                popscript.Append("<script language='Javascript'>")
                popscript.Append("ref = window.open('FirmaOnlineWebStart.aspx', 'Custompopup', ")
                popscript.Append("'left = 5, top = 30, width = 450, height = 250, menubar = no, scrollbars = no, resizable = no');")
                popscript.Append("</script>")
            End If
        End If

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "script", popscript.ToString().Trim(), False)

    End Sub

    Private Function obtenerCantidadHojasPdj(ByVal pContenido As Byte()) As Int64
        Dim reader = New PdfReader(pContenido)

        Dim memoryStream As New MemoryStream()
        Dim pdfStamper As New PdfStamper(reader, memoryStream)

        Return reader.NumberOfPages()
    End Function

    Private Function obtenerConceptosPadres(ByVal pIdConceptoPadre As Int64) As ArrayList

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

    Private Sub FrimarDocs(ByVal opt As Integer)
        Dim idsarray As New ArrayList()

        For i As Integer = 0 To GrdPdf.Rows.Count - 1

            Dim checkboxFir As New CheckBox()
            checkboxFir = GrdPdf.Rows(i).FindControl("chkFirmar")

            If (checkboxFir.Visible) Then

                If (checkboxFir.Checked) Then
                    Dim idDoc As Integer = -1
                    idDoc = CInt(GrdPdf.Rows(i).Cells(0).Text)

                    idsarray.Add(idDoc)

                End If

            End If

        Next

        If (idsarray.Count > 0) Then

            Dim idTipoDoc As Integer
            idTipoDoc = ObtenerTipoDocumento(idsarray.Item(0))

            CargarPosicion(idTipoDoc)
            FirmarDigitalVarios(idsarray, opt)

        End If
    End Sub

    Protected Sub ibFJava_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibFJava.Click
        FrimarDocs(1)
    End Sub

    Protected Sub ibFAct_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibFAct.Click
        FrimarDocs(2)
    End Sub


    Protected Sub ibPagado_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)

        timerGrilla.Enabled = False

        hfDocSel.Value = Convert.ToInt32(row.Cells(0).Text)

        Dim obj(1) As Object
        obj(0) = Convert.ToInt32(row.Cells(0).Text)
        obj(1) = ""

        Dim dt As New DataTable
        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.pagoDocumentoGetByIdDoc", obj).Tables(0)

        Dim contenido As Byte() = Nothing

        Dim tbl As DataTable
        tbl = OracleHelper.ExecuteDataset(cad1, "doc_laboral.BarandillaDocumentoPdfGetById", obj).Tables(0)

        lblCantHojas.Text = OracleHelper.ExecuteScalar(cad1, CommandType.Text, "select canthojas from BarandillaDocumentoPDF where idbarandilladocumento = " & hfDocSel.Value & "")
        lblEstado.Text = "Pendiente"
        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("Estado").ToString() = "1" Then
                btnGenerar.Visible = False
                btnGenerarPagoLink.Visible = False
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

    Private Function retornarValor(ByVal pkey As String) As String
        Dim valor As String = ""
        Dim dt As New DataTable
        Dim xmlToDt As New ClaseXmlManager

        dt = xmlToDt.XmlToDataSet("ConfGral.xml").Tables(0)

        For index = 0 To dt.Rows.Count - 1
            If dt.Rows(index)(0).ToString() = pkey Then
                valor = dt.Rows(index)(1).ToString()
                Exit For
            End If
        Next
        Return valor
    End Function

    Private Function validarPeriodoPresentacion(ByVal pTipoLiq As Int32, ByVal pAno As String, ByVal pMes As Int16) As Boolean

        Dim fechaActual As DateTime
        fechaActual = DateTime.Now.Day.ToString() & "/" & DateTime.Now.Month.ToString() & "/" & DateTime.Now.Year.ToString()

        If pTipoLiq = TipoLiquidacion.mensual Or pTipoLiq = TipoLiquidacion.vacacion Or _
           pTipoLiq = TipoLiquidacion.LiquidacionFinal Or pTipoLiq = TipoLiquidacion.primeraQuincena Or _
           pTipoLiq = TipoLiquidacion.segundaQuincena Or pTipoLiq = TipoLiquidacion.primerSAC Or _
           pTipoLiq = TipoLiquidacion.segundoSAC Then

            Dim anoTope As String = retornarValor("anoTope")
            Dim vencTope As DateTime = retornarValor("fechaVencTope")

            If anoTope >= pAno Then
                If vencTope < fechaActual Then
                    Return True
                Else
                    Return False
                End If
            End If

            Dim messel As Int16
            messel = pMes

            Dim anoActual As Integer = DateTime.Now.AddMonths(-1).Year

            Dim mesaprensentar As Int16
            mesaprensentar = DateTime.Now.AddMonths(-1).Month

            Dim terminacion As String
            terminacion = hfCuit.Value.Substring(hfCuit.Value.Length - 1, 1)

            Dim obj(4) As Object
            obj(0) = pAno
            obj(1) = pMes
            obj(2) = "1" 'id Grupo de hojas moviles
            obj(3) = terminacion
            obj(4) = ""

            Dim dt As New DataTable
            dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.PresVencSearchTerminacion", obj).Tables(0)

            Dim fechaVenc As DateTime = DateTime.MinValue

            If dt.Rows.Count > 0 Then
                'si hay vencimientos cargados para el periodo y grupo de doc
                'buscamos el correspondiente a la terminacion de CUIT
                fechaVenc = dt.Rows(0)("fechavenc").ToString()
            Else
                'si no encontramos periodo para la terminacion y es el periodo que corresponde presentar
                'buscamos el mas reciente cargado para esa terminacion
                If messel = mesaprensentar And anoActual = pAno Then
                    Dim obj1(2) As Object
                    obj1(0) = terminacion
                    obj1(1) = "1"
                    obj1(2) = ""
                    Dim dt1 As New DataTable
                    dt1 = OracleHelper.ExecuteDataset(cad1, "Doc_Laboral.presvencGetByTerminacion", obj1).Tables(0)

                    For i = 0 To dt1.Rows.Count - 1
                        fechaVenc = dt1.Rows(i)("fechavenc").ToString()

                        Dim fec As String
                        fec = fechaVenc.Day.ToString() & "/" & DateTime.Now.Month.ToString() & "/" & DateTime.Now.Year.ToString()
                        Dim cont As Integer = -1

                        While Not IsDate(fec)
                            fec = fechaVenc.AddDays(cont).Day.ToString() & "/" & DateTime.Now.Month.ToString() & "/" & DateTime.Now.Year.ToString()
                            cont = cont - 1
                        End While

                        fechaVenc = fec

                        'fechaVenc = fechaVenc.Day.ToString() & "/" & DateTime.Now.Month.ToString() & "/" & DateTime.Now.Year.ToString()
                        Exit For
                    Next
                Else
                    'si no es el mes que corresponde presentar, se marca como fuera de termino
                    Return True
                End If
            End If

            If fechaVenc < fechaActual Then
                Return True
            End If
        End If

        Return False
    End Function

    Private Sub EliminarDocumento(ByVal pIdDoc As Int64)
        Dim obj(0) As Object
        obj(0) = pIdDoc

        OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.barandocpdfDeleteById", obj(0))
        OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.docxusuarioDeleteByIdDoc", obj(0))
        OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.docxMultinotaDeleteByIdDoc", obj(0))

    End Sub


    
    Protected Sub btnQR_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim popupScript As String

        popupScript = String.Empty
        popupScript = "<script language='JavaScript'>"
        popupScript += "window.open('frmPdfPruebaQR.aspx?pnrocuenta=" & Session("nrocuenta") & "', 'CustomPopUp', "
        popupScript += "'top=80,left=300 ,width=700, height=500, menubar=no, scrollbars=NO ,resizable=NO')"
        popupScript += "</script>"

        ToolkitScriptManager1.RegisterStartupScript(Me.Page, Me.GetType, "popCompro", popupScript.ToString(), False)

    End Sub

    Protected Sub btnCancelarPopup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarPopup.Click
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "script2", "<script type='text/javascript'>window.location='frmPantallaInicio1.aspx';</script>")
    End Sub

    Protected Sub btnAceptarDigital_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarDigital.Click
        cambiaOpcion()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "script3", "<script type='text/javascript'>alert('USTED YA SE ENCUENTRA HABILITADO PARA EL USO DE LA BANDEJA DIGITAL');</script>")
        Me.PanelContenedorPrincipal.Visible = True
    End Sub

    Private Sub cambiaOpcion()

        Dim ssql As String = "update doc_laboral.empresas set acuerdo = 1, idmedpres = 2 where nrocuenta = " & Session("nrocuenta")
        OracleHelper.ExecuteNonQuery(cad1, CommandType.Text, ssql.ToString)
        Session("Acuerdo") = "SI"
    End Sub

    Protected Sub ibFWeb_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibFWeb.Click
        FrimarDocs(3)
    End Sub
End Class
