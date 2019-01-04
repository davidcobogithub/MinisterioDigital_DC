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

Imports System.Collections
Imports System.Configuration

Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq

Imports System.Security.Cryptography
Imports System.Net

Imports Newtonsoft.Json


Partial Class Pruebas
    Inherits System.Web.UI.Page

    Dim cencr As New ClaseEncripta("1234")
    Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Public horaActual As String

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Session("LoginEmpresa") = "SI"
        'Session("LoginMinisterio") = "SI"
        Session("cuit") = "30703392241"
        Session("empresa") = "asegada"
        'Session("cuit") = "30708323353"
        Session("UsuarioMin") = "asegada"
        Session("nrocuenta") = 64285
        Session("Acuerdo") = "SI"
        Session("SucursalLI") = "00"
        'Session("lugarident") = "0472"
        'Session("rectificativa") = "S"
        'Session("tipopersoneria") = "FISICA"
        'Session("cantemp") = 30
        Response.Redirect("frmLIapertura.aspx")
        'Response.Redirect("frmPersonasFisicasCotejarRcivil.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Session("LoginEmpresa") = "SI"
        'Session("LoginMinisterio") = "SI"
        Session("cuit") = "23134473274"
        Session("empresa") = "asegada"
        'Session("cuit") = "30708323353"
        Session("UsuarioMin") = "asegada"
        Session("UsuarioMinId") = 360
        Session("nrocuenta") = 13433
        'Session("lugarident") = "0472"
        'Session("rectificativa") = "S"
        'Session("tipopersoneria") = "FISICA"
        'Session("cantemp") = 30

        'Response.Redirect("frmLIMenuInspeccion.aspx?nc=13433")
        'Response.Redirect("frmLIactainfraccion.aspx?p=ddiJIiPxL2ih2WjHTI6DhQ/HDjvclCmTI9S3nYYREOVasj3A97FYrw==")
        ' Response.Redirect("frmLIReporteRPvacio.aspx")

        Response.Redirect("frmPlanillaHorarioQR.aspx?psec=98152546636&pcuit=10306545898&pxn=99868&pd=0")
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
  
        Dim s As String = ""
        With Request.Browser
            s &= "Browser Capabilities" & vbCrLf
            s &= "Type = " & .Type & vbCrLf
            s &= "Name = " & .Browser & vbCrLf
            s &= "Version = " & .Version & vbCrLf
            s &= "Major Version = " & .MajorVersion & vbCrLf
            s &= "Minor Version = " & .MinorVersion & vbCrLf
            s &= "Platform = " & .Platform & vbCrLf
            s &= "Is Beta = " & .Beta & vbCrLf
            s &= "Is Crawler = " & .Crawler & vbCrLf
            s &= "Is AOL = " & .AOL & vbCrLf
            s &= "Is Win16 = " & .Win16 & vbCrLf
            s &= "Is Win32 = " & .Win32 & vbCrLf
            s &= "Supports Frames = " & .Frames & vbCrLf
            s &= "Supports Tables = " & .Tables & vbCrLf
            s &= "Supports Cookies = " & .Cookies & vbCrLf
            s &= "Supports VBScript = " & .VBScript & vbCrLf
            s &= "Supports JavaScript = " & _
                .EcmaScriptVersion.ToString() & vbCrLf
            s &= "Supports Java Applets = " & .JavaApplets & vbCrLf
            s &= "Supports ActiveX Controls = " & .ActiveXControls & _
                vbCrLf
            s &= "Supports JavaScript Version = " & _
                Request.Browser("JavaScriptVersion") & vbCrLf
        End With
        MsgBox(s)


    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
      


    End Sub

    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
        'For i As Integer = 93360 To 93360
        '    InsertarTurnos(i)
        'Next

        
      
    End Sub

    Private Function InsertarTurnos(ByVal idturnomatriz As Integer)

        'Dim ssql As String = String.Empty
        'ssql = "insert into turnos (nrocuenta,idturnomatriz,idturno,coddelegacion) " _
        '      & "SELECT E.Nrocuenta, " & idturnomatriz & ",sec_idturno.nextval, 26 FROM DOC_LABORAL.EMPRESAS E " _
        '      & "LEFT JOIN DOC_LABORAL.TURNOS T " _
        '      & "ON E.NROCUENTA = T.NROCUENTA WHERE FECALTA>'01/04/2016' AND T.IDTURNO IS NULL"


        'Dim obj(1) As Object
        'obj(0) = idturnomatriz

        'OracleHelper.ExecuteNonQuery(cad1, CommandType.Text, ssql)

    End Function

    
    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        'Session("LoginEmpresa") = "SI"
        Session("LoginMinisterio") = "SI"
        'Session("cuit") = "23134473274"
        Session("empresa") = "asegada"
        'Session("cuit") = "30708323353"
        Session("UsuarioMin") = "27203523101"
        Session("UsuarioMinId") = 2
        'Session("nrocuenta") = 13433

        'Session("lugarident") = "0472"
        'Session("rectificativa") = "S"
        'Session("tipopersoneria") = "FISICA"
        'Session("cantemp") = 30

        'Response.Redirect("frmLIMenuInspeccion.aspx?nc=13433")

        'Response.Redirect("frmLIactainfraccion.aspx?p=ddiJIiPxL2ih2WjHTI6DhQ/HDjvclCmTI9S3nYYREOVasj3A97FYrw==")
        Response.Redirect("frmLiBarandillaLi.aspx")
        'Response.Redirect("frmLIactaConstatacion.aspx?p=ZGRpSklpUHhMMmloMldqSFRJNkRoUS9IRGp2Y2xDbVRJOVMzbllZUkVPWDJsWHRobkF1eFF3PT01")


    End Sub

    Protected Sub Button7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button7.Click
        'Session("LoginEmpresa") = "SI"
        Session("LoginMinisterio") = "SI"
        Session("Logueada") = "SI"
        'Session("cuit") = "30700254425"
        Session("empresa") = "asegada"
        Session("cuit") = "30700254425"
        Session("UsuarioMin") = "33707713289"
        Session("UsuarioMinId") = 2
        Session("nrocuenta") = 36294
        'Session("nrocuenta") = 66743
        'Response.Redirect("frmAutorizados.aspx")
        'Response.Redirect("frmPantallaBarandillaDocumentosPdf.aspx")
        'Response.Redirect("EmpleadosABM.aspx")
        'Response.Redirect("frmSIN-Contactos.aspx")
        'Response.Redirect("frmSIN-Apoderados.aspx")
        'Response.Redirect("frmSIN-Actividades.aspx")
        'Response.Redirect("frmSIN-Generales.aspx")
        'Response.Redirect("frmSIN-Domicilios.aspx")
        Response.Redirect("frmAURegistrar.aspx")

    End Sub

    Protected Sub Button8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button8.Click

        Session("LoginMinisterio") = "SI"
        Session("cuit") = "20298794056"
        Session("empresa") = "asegada"

        Session("UsuarioMin") = "27203523101"
        Session("UsuarioMinId") = "2"

        Session("nrocuenta") = 13433

        Dim psec As String = ClaseGeneraClaveMin.EncriptarCadena("123123231231")
        Dim pcuit As String = ClaseGeneraClaveMin.EncriptarCadena("10210270892")
        Dim pxn As String = ClaseGeneraClaveMin.EncriptarCadena("79892")
        Dim pd As String = ClaseGeneraClaveMin.EncriptarCadena("9")

        Response.Redirect("frmLiBarandillaLi.aspx")


    End Sub

    'Protected Sub btnValidaHora_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidaHora.Click
    '    ValidarTextosFecha()
    'End Sub

    'Public Function ValidarTextosFecha() As Boolean
    '    Dim blnValido As Boolean = True
    '    Dim Expresion As System.Text.RegularExpressions.Regex
    '    Expresion = New System.Text.RegularExpressions.Regex("^((([0]?[1-9]|1[0-2])(:)[0-5][0-9]( )?" & _
    '                "(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:)[0-5][0-9]))$", _
    '                System.Text.RegularExpressions.RegexOptions.IgnoreCase)

    '    If Not Expresion.IsMatch(txtHoraFin.Text) Then
    '        Windows.Forms.MessageBox.Show("Formato inválido fecha inicio")
    '        blnValido = False
    '    End If
    '    If Not Expresion.IsMatch(txtHoraInicio.Text) Then
    '        Windows.Forms.MessageBox.Show("Formato inválido fecha final")
    '        blnValido = False
    '    End If
    '    If blnValido Then

    '        Dim inthorainicio As Integer = 0
    '        Dim inthorafin As Integer = 0
    '        Dim strTemporal As String = ""

    '        If txtHoraInicio.Text.ToLower.IndexOf("pm") > 0 Then
    '            inthorainicio = 1200
    '        End If
    '        strTemporal = txtHoraInicio.Text.Trim.Replace("am", "").Replace("pm", "").Replace(":", "")
    '        If txtHoraInicio.Text.ToLower.IndexOf("pm") > 0 Or _
    '          txtHoraInicio.Text.ToLower.IndexOf("am") > 0 Then
    '            strTemporal = strTemporal.Substring(0, 2).Replace("12", "00") & _
    '                          strTemporal.Substring(2, strTemporal.Length - 2)
    '        End If
    '        inthorainicio += CInt(strTemporal)

    '        If txtHoraFin.Text.ToLower.IndexOf("pm") > 0 Then
    '            inthorafin = 1200
    '        End If
    '        strTemporal = txtHoraFin.Text.Trim.Replace("am", "").Replace("pm", "").Replace(":", "")
    '        If txtHoraFin.Text.ToLower.IndexOf("pm") > 0 Or _
    '          txtHoraFin.Text.ToLower.IndexOf("am") > 0 Then
    '            strTemporal = strTemporal.Substring(0, 2).Replace("12", "00") & _
    '                          strTemporal.Substring(2, strTemporal.Length - 2)
    '        End If
    '        inthorafin += CInt(strTemporal)
    '        If inthorafin <= inthorainicio Then
    '            Windows.Forms.MessageBox.Show("La hora final debe ser mayor que la hora inicial")
    '            blnValido = False
    '        End If
    '    End If
    '    Return blnValido
    'End Function

    Protected Sub BtnEmpleados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEmpleados.Click
        Session("LoginEmpresa") = "SI"
        'Session("LoginMinisterio") = "SI"
        Session("cuit") = "23134473274"
        Session("empresa") = "asegada"
        'Session("cuit") = "30708323353"
        Session("UsuarioMin") = "asegada"
        Session("UsuarioMinId") = 2
        Session("nrocuenta") = 36294
        'Session("lugarident") = "0472"
        'Session("rectificativa") = "S"
        'Session("tipopersoneria") = "FISICA"
        'Session("cantemp") = 30

        'Response.Redirect("frmLIMenuInspeccion.aspx?nc=13433")

        'Response.Redirect("frmLIactainfraccion.aspx?p=ddiJIiPxL2ih2WjHTI6DhQ/HDjvclCmTI9S3nYYREOVasj3A97FYrw==")
        'Response.Redirect("frmLIactaConstatacion.aspx")
        'Response.Redirect("frmLIactaConstatacion.aspx?p=ZGRpSklpUHhMMmloMldqSFRJNkRoUS9IRGp2Y2xDbVRJOVMzbllZUkVPWDJsWHRobkF1eFF3PT01")
        Response.Redirect("frmCOM-BandejaComunicaciones.aspx")

    End Sub


#Region "MARCA AGUA"

    'Protected Sub btnMarcaAgua_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMarcaAgua.Click

    '    Dim contenido As Byte() = Nothing
    '    contenido = cargarpdf(0, 0, 3080)

    '    Dim reader As New PdfReader(contenido)
    '    Dim fs As MemoryStream = Nothing
    '    Dim stamp As PdfStamper = Nothing
    '    Dim document As Document = Nothing


    '    Try


    '        document = New Document()


    '        '  Dim outputPdf As String = [String].Format("C:\temp\{0}.pdf", Guid.NewGuid().ToString())

    '        fs = New MemoryStream(contenido)
    '        stamp = New PdfStamper(reader, fs)

    '        Dim bf As BaseFont = BaseFont.CreateFont("c:\windows\fonts\arial.ttf", BaseFont.CP1252, True)

    '        Dim gs As New PdfGState()

    '        gs.FillOpacity = 0.349999994F

    '        gs.StrokeOpacity = 0.349999994F

    '        For nPag As Integer = 1 To reader.NumberOfPages



    '            Dim tamPagina As Rectangle = reader.GetPageSizeWithRotation(nPag)

    '            Dim over As PdfContentByte = stamp.GetOverContent(nPag)

    '            over.BeginText()


    '            WriteTextToDocument(bf, tamPagina, over, gs, "www.devjoker.com")


    '            over.EndText()


    '        Next
    '    Finally




    '        ' Garantizamos que aunque falle se cierran los objetos

    '        ' alternativa:usar using

    '        reader.Close()

    '        'If stamp IsNot Nothing Then
    '        '    stamp.Close()
    '        'End If

    '        If fs IsNot Nothing Then
    '            fs.Close()
    '        End If

    '        If document IsNot Nothing Then
    '            document.Close()

    '        End If

    '        Response.Clear()
    '        Response.ClearHeaders()
    '        Response.ClearContent()
    '        Response.ContentType = "application/pdf"
    '        Response.AddHeader("content-disposition", "inline;filename=report.pdf")
    '        Response.BinaryWrite(fs.ToArray)
    '        Response.End()







    '    End Try



    'End Sub

    Private Shared Sub WriteTextToDocument(ByVal bf As BaseFont, ByVal tamPagina As Rectangle, ByVal over As PdfContentByte, ByVal gs As PdfGState, ByVal texto As String)

        over.SetGState(gs)

        over.SetRGBColorFill(220, 220, 220)

        over.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_STROKE)

        over.SetFontAndSize(bf, 46)

        Dim anchoDiag As Single = Math.Sqrt(Math.Pow((tamPagina.Height - 120), 2) + Math.Pow((tamPagina.Width - 60), 2))


        Dim porc As Single = 100 * (anchoDiag / bf.GetWidthPoint(texto, 46))


        over.SetHorizontalScaling(porc)

        Dim angPage As Double = (-1) * Math.Atan((tamPagina.Height - 60) / (tamPagina.Width - 60))

        over.SetTextMatrix(CSng(Math.Cos(angPage)), CSng(Math.Sin(angPage)), CSng((-1.0F) * Math.Sin(angPage)), CSng(Math.Cos(angPage)), 30.0F, CSng(tamPagina.Height) - 60)
        over.ShowText(texto)

    End Sub

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

    'Protected Sub btnMarcaAgua0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMarcaAgua0.Click

    '    Dim contenido As Byte() = Nothing
    '    contenido = cargarpdf(0, 0, 3080)

    '    Dim reader As New PdfReader(contenido)

    '            Dim fs As FileStream = Nothing

    '            Dim stamp As PdfStamper = Nothing

    '            Dim document As Document = Nothing

    '            Try


    '                document = New Document()
    '        Dim directorio As String
    '        directorio = Server.MapPath(".") + "\PDFTemporal\"

    '        Dim outputPdf As String = directorio & "NUevoarchivos" & ".pdf"



    '        fs = New FileStream(outputPdf, FileMode.Create, FileAccess.Write)

    '                stamp = New PdfStamper(reader, fs)



    '                Dim bf As BaseFont = BaseFont.CreateFont("c:\windows\fonts\arial.ttf", BaseFont.CP1252, True)

    '                Dim gs As New PdfGState()

    '                gs.FillOpacity = 0.349999994F

    '                gs.StrokeOpacity = 0.349999994F

    '                For nPag As Integer = 1 To reader.NumberOfPages



    '                    Dim tamPagina As Rectangle = reader.GetPageSizeWithRotation(nPag)

    '                    Dim over As PdfContentByte = stamp.GetOverContent(nPag)

    '                    over.BeginText()





    '            WriteTextToDocument(bf, tamPagina, over, gs, "Documento Dado de Baja")


    '                    over.EndText()


    '                Next
    '            Finally



    '                ' Garantizamos que aunque falle se cierran los objetos

    '                ' alternativa:usar using

    '                reader.Close()

    '                If stamp IsNot Nothing Then
    '                    stamp.Close()
    '                End If

    '                If fs IsNot Nothing Then
    '                    fs.Close()
    '                End If

    '                If document IsNot Nothing Then
    '                    document.Close()

    '                End If
    '            End Try


    '    Dim ruta As String = RutaHttp() & "PDFTemporal/" & "NUevoarchivos" & ".pdf"

    '    Dim popupScript As String
    '    popupScript = String.Empty
    '    popupScript = "<script language='JavaScript'>"
    '    popupScript += "window.open('" + ruta + "', 'CustomPopUp', titlebar = 'no', "
    '    popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
    '    popupScript += "</script>"
    '    Page.RegisterStartupScript("popup", popupScript)



    '        End Sub

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

    Protected Sub btnNotificaciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNotificaciones.Click
        Session("LoginEmpresa") = "SI"
        'Session("LoginMinisterio") = "SI"
        Session("cuit") = "30703392241"
        Session("empresa") = "asegada"
        'Session("cuit") = "30708323353"
        Session("UsuarioMin") = "asegada"
        Session("nrocuenta") = 64285
        Session("Acuerdo") = "SI"
        Session("SucursalLI") = "00"
        'Session("lugarident") = "0472"
        'Session("rectificativa") = "S"
        'Session("tipopersoneria") = "FISICA"
        'Session("cantemp") = 30
        Response.Redirect("frmLIresultadosNotificaciones.aspx")
        'Response.Redirect("frmPersonasFisicasCotejarRcivil.aspx")
    End Sub

    Protected Sub Button9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button9.Click
        Session("LoginEmpresa") = "SI"
        'Session("LoginMinisterio") = "SI"
        Session("cuit") = "30714187895"
        Session("empresa") = "asegada"
        'Session("cuit") = "30708323353"
        Session("UsuarioMin") = "asegada"
        Session("UsuarioMinId") = 2
        Session("nrocuenta") = 64285
        'Session("lugarident") = "0472"
        'Session("rectificativa") = "S"
        'Session("tipopersoneria") = "FISICA"
        'Session("cantemp") = 30

        'Response.Redirect("frmLIMenuInspeccion.aspx?nc=13433")

        'Response.Redirect("frmLIactainfraccion.aspx?p=ddiJIiPxL2ih2WjHTI6DhQ/HDjvclCmTI9S3nYYREOVasj3A97FYrw==")
        'Response.Redirect("frmLIactaConstatacion.aspx")
        'Response.Redirect("frmLIactaConstatacion.aspx?p=ZGRpSklpUHhMMmloMldqSFRJNkRoUS9IRGp2Y2xDbVRJOVMzbllZUkVPWDJsWHRobkF1eFF3PT01")
        Dim parsincod As New ArrayList
        parsincod.Add("30714187895")
        Response.Redirect("frmCambioPassEmpresas.aspx?p=" & clsUtiles.CodificaParametros(parsincod, "-") & "")

    End Sub

    Protected Sub BtnServerMap_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnServerMap.Click
        Dim pdfFilePath As String = Server.MapPath(".") + "\PDFTemporal\" & 123 & 32 & ".pdf"

    End Sub

    Protected Sub btnPSP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPSP.Click
        Session("LoginPCP") = "SI"
        Dim par As New ArrayList
        par.Add("0")
        par.Add("20165017170")
        Response.Redirect("frmPCP-EmpleadorAdmin.aspx?p=" & clsUtiles.CodificaParametros(par, "-"))
    End Sub

    Protected Sub btnpcpreporte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpcpreporte.Click
        generaConstancia("20165017170")
    End Sub

    Private Sub generaConstancia(ByVal pCuil As String)


        Dim usuarioCidi As Usuario = ObtieneUsuarioCidi(pCuil.Trim)

        Using oConexion As New OracleConnection(cad1)
            Dim oCmd As New OracleCommand
            With oCmd
                .Connection = oConexion
                .CommandType = CommandType.Text
                ' .CommandText = "select 1 id_empleador, '01/01/2016' fecha_alta from doc_laboral.t_pcp_empleadores where cuil='" + pCuil.Trim + "'"
                .CommandText = "select 1 id_empleador, '01/01/2016' fecha_alta from dual"
            End With
            Dim da As New OracleDataAdapter(oCmd)
            Dim t As New DataTable
            da.Fill(t)



            If t.Rows.Count > 0 Then
                Dim contenidoPDF() As Byte
                Dim b As New ClaseExportadoraPlus("Reportes\RptConstanciaEmpleadorPCP.rdlc")
                b.cadenaConexion = cad1
                b.agregarParametroLocalReport("rpCuit", usuarioCidi.CUIL)
                b.agregarParametroLocalReport("rpRazonSocial", usuarioCidi.Apellido & " " & usuarioCidi.Nombre)
                b.agregarParametroLocalReport("rpAltaEmpleador", t.Rows(0)("Fecha_alta").ToString())
                b.agregarParametroLocalReport("rpCantidadEmpleadosActivos", clsPCP.CantidadTrabajadoresPCP(pCuil))
                b.agregarParametroLocalReport("rpCantidadSucursales", clsPCP.CantidadDomiciliosPCP(pCuil))
                b.agregarParametroLocalReport("rpDomicilioReal", "Calle " & usuarioCidi.Domicilio.Calle & " Altura " _
                                              & usuarioCidi.Domicilio.Altura & " Piso " & IIf(usuarioCidi.Domicilio.Piso = "", "S/D", usuarioCidi.Domicilio.Piso) & " Dpto " _
                                              & IIf(usuarioCidi.Domicilio.Depto = "", "S/D", usuarioCidi.Domicilio.Depto) & " Localidad " & IIf(usuarioCidi.Domicilio.Localidad = "", "S/D", usuarioCidi.Domicilio.Localidad))

                'Dim oCmd2 As New OracleCommand
                'With oCmd2
                '    .Connection = oConexion
                '    .CommandType = CommandType.Text
                '    .CommandText = "select NOMBRE, FECHAINICIO from doc_laboral.empresasactividades where FECHAFIN IS NULL AND nrocuenta=" + mNroCuenta.ToString.Trim
                'End With
                'Dim da2 As New OracleDataAdapter(oCmd2)
                'Dim t2 As New DataTable
                'da2.Fill(t2)

                'b.agregarDataSourceDesconectado("DataSet1", t2)

                contenidoPDF = b.generarInforme(8.25, 11.699999999999999)

                Response.Clear()
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-disposition", "attachment; filename=ConstanciaInscripcionLaboralPCP.pdf")
                Response.BinaryWrite(contenidoPDF)
                Response.End()

            Else
                Response.Write("<script>alert('Ingrese un CUIT válido');</script>")
                'Me.lblMessage.Text = "<div class='alert alert-danger' role='alert'>INGRESE UN CUIT VALIDO</div>"

            End If
        End Using
        '------------------------------------
    End Sub

    Private Function ObtieneUsuarioCidi(ByVal pCuit As String) As Usuario
        Dim _TimeStamp As [String] = DateTime.Now.ToString("yyyyMMddHHmmssfff")
        Dim _Token As [String] = Config.ObtenerToken(_TimeStamp)

        Dim httpWebRequest = DirectCast(WebRequest.Create(Config._Url_ApiCuentaCiDi + "api/Usuario/Obtener_Usuario"), HttpWebRequest)
        httpWebRequest.ContentType = "application/json; charset=utf-8"

        Dim Entrada As New Entrada()

        Entrada.IdAplicacion = Config._Cidi_Id_Aplicacion
        Entrada.Contrasenia = Config._Cidi_Pass_Aplicacion
        Entrada.Cuil = pCuit
        Entrada.TokenValue = _Token
        Entrada.TimeStamp = _TimeStamp

        Dim rawjson As [String] = JsonConvert.SerializeObject(Entrada)
        httpWebRequest.Method = "POST"

        Using streamWriter = New StreamWriter(httpWebRequest.GetRequestStream())
            streamWriter.Write(rawjson)
            streamWriter.Flush()
            streamWriter.Close()
            Dim httpResponse = DirectCast(httpWebRequest.GetResponse(), HttpWebResponse)
            Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                Dim result = streamReader.ReadToEnd()
                Dim _Usuario As Usuario = JsonConvert.DeserializeObject(Of Usuario)(result)

                If _Usuario IsNot Nothing Then
                    If _Usuario.Respuesta.Resultado = "OK" Then
                        Return _Usuario
                    Else
                        Return _Usuario
                    End If
                Else
                    Return Nothing
                End If
            End Using
        End Using
    End Function

    Protected Sub btnpcpreporteTrabajadores_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpcpreporteTrabajadores.Click
        generaConstanciaTrabajadores("27259183176")
    End Sub

    Private Sub generaConstanciaTrabajadores(ByVal pcuil As String)

        Dim usuarioCidi As Usuario = ObtieneUsuarioCidi(pcuil.Trim)

        Using oConexion As New OracleConnection(cad1)
            Dim oCmd As New OracleCommand
            With oCmd
                .Connection = oConexion
                .CommandType = CommandType.Text
                ' .CommandText = "select 1 id_empleador, '01/01/2016' fecha_alta from doc_laboral.t_pcp_empleadores where cuil='" + pCuil.Trim + "'"

                .CommandText = "select rl.id_pcp_relacion_laboral, em.id_empleador, " _
                              & " rl.id_pcp_relacion_laboral, rc.cuil, rc.NRO_DOCUMENTO,  (rc.APELLIDO || ' ' || rc.NOMBRE) ApellidoyNombre, " _
                              & " to_char(rl.fec_alta,'dd-mm-rrrr') fec_alta, art.naseguradora, cat.n_pcp_categoria, hs.n_descripcion,  ml.n_pcp_modalidad_liquidacion, " _
                              & " rl.remuneracion_pactada, mp.n_pcp_modalidad_prestacion, rl.fec_baja,  Me.nmotivoegreso  from doc_laboral.t_pcp_relaciones_laboral rl " _
                              & " left join doc_laboral.t_pcp_trabajadores tr    on rl.id_trabajador = tr.id_trabajador " _
                              & " left join rcivil.vt_pk_persona rc  on tr.id_sexo = rc.ID_SEXO  and tr.nro_documento = rc.NRO_DOCUMENTO   and tr.pai_cod_pais = rc.PAI_COD_PAIS    and tr.id_numero = rc.ID_NUMERO " _
                              & " left join doc_laboral.t_pcp_empleadores em  on tr.id_empleador = em.id_empleador " _
                              & " left join doc_laboral.aseguradorasrt art   on em.idaseguradora = art.idaseguradora " _
                              & " left join doc_laboral.t_pcp_horas_semanal hs  on hs.id_pcp_horas_semanales = rl.id_pcp_horas_semanales " _
                              & " left join doc_laboral.t_pcp_modalidades_liquidacion ml  on ml.id_pcp_modalidad_liquidacion = rl.id_pcp_modalidad_liquidacion " _
                              & " left join doc_laboral.t_pcp_modalidades_prestacion mp  on mp.id_pcp_modalidad_prestacion = rl.id_pcp_modalidad_prestacion " _
                              & " left join doc_laboral.t_pcp_categorias cat  on cat.id_pcp_categoria = rl.id_pcp_categorias " _
                              & " left join doc_laboral.motivos_egreso me on rl.id_motivo_egreso = me.idmotivoegreso " _
                              & " where(em.id_empleador = 262 and rl.id_pcp_relacion_laboral = 141  and rownum = 1)"

                               
            End With
            Dim da As New OracleDataAdapter(oCmd)
            Dim t As New DataTable
            da.Fill(t)



            If t.Rows.Count > 0 Then
                Dim contenidoPDF() As Byte
                Dim b As New ClaseExportadoraPlus("Reportes\rptMiRegistroPCP.rdlc")
                b.cadenaConexion = cad1
                b.agregarParametroLocalReport("pCuilApeynom", t.Rows(0)("ApellidoyNombre").ToString())
                b.agregarParametroLocalReport("pEmpleador", usuarioCidi.Apellido & " " & usuarioCidi.Nombre)
                b.agregarParametroLocalReport("pCuitEmpleador", usuarioCidi.CUIL)
                b.agregarParametroLocalReport("pFecIngreso", t.Rows(0)("fec_alta").ToString())
                b.agregarParametroLocalReport("pCategoria", t.Rows(0)("n_pcp_categoria").ToString())
                b.agregarParametroLocalReport("pAseguradora", t.Rows(0)("naseguradora").ToString())
                b.agregarParametroLocalReport("pHorasSemanales", t.Rows(0)("n_descripcion").ToString())
                b.agregarParametroLocalReport("pModLiquidacion", t.Rows(0)("n_pcp_modalidad_liquidacion").ToString())
                b.agregarParametroLocalReport("pModPrestacion", t.Rows(0)("n_pcp_modalidad_prestacion").ToString())
                b.agregarParametroLocalReport("pRetribPactada", t.Rows(0)("remuneracion_pactada").ToString())
                b.agregarParametroLocalReport("pFecEgreso", t.Rows(0)("fec_baja").ToString())
                b.agregarParametroLocalReport("pMotivo", t.Rows(0)("nmotivoegreso").ToString())

                'Dim oCmd2 As New OracleCommand
                'With oCmd2
                '    .Connection = oConexion
                '    .CommandType = CommandType.Text
                '    .CommandText = "select NOMBRE, FECHAINICIO from dual"
                'End With
                'Dim da2 As New OracleDataAdapter(oCmd2)
                'Dim t2 As New DataTable
                'da2.Fill(t2)
                'b.agregarDataSourceDesconectado("DataSet1", t2)

                contenidoPDF = b.generarInforme(8.25, 11.699999999999999)
                

                Response.Clear()
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-disposition", "attachment; filename=ConstanciaTrabajadoresPCP.pdf")
                Response.BinaryWrite(contenidoPDF)
                Response.End()

            Else
                Response.Write("<script>alert('Ingrese un CUIT válido');</script>")
                'Me.lblMessage.Text = "<div class='alert alert-danger' role='alert'>INGRESE UN CUIT VALIDO</div>"

            End If
        End Using
        '------------------------------------
    End Sub

    Protected Sub btnInsertaDom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsertaDom.Click
        Session("LoginEmpresa") = "SI"
        Session("cuit") = "20165017170"
        Session("empresa") = "asegada"
        Session("UsuarioMin") = "asegada"
        Session("nrocuenta") = 64285
        Session("Acuerdo") = "SI"


        Response.Redirect("PruebaDomicilios.aspx")
    End Sub


    

    Protected Sub Button10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button10.Click


      
            '**************OBTENER ID y USUARIO CIDI trabajador
            Dim mIdEmpleador = CInt(Me.hfIdEmpleador.Value.ToString.Trim)
            Dim usuarioCidiTrabajador As Usuario = ObtieneUsuarioCidi(Me.hfCuilTrabajador.Value.ToString.Trim)
            '***********************************************************************
            'VALORES FIJOS - SINDICATO Y CONVENIO SON UNICOS PARA ESTA ACTIVIDAD   !
            'SINDICATO = 134      -     CONVENIO = 5295                            !
            '***********************************************************************
            Dim midSindicato As Int32 = 134
            Dim midConvenio As Int32 = 5295
            '-----------------------------------------------------------------------
            Dim mFechaRegistro As String = Now.ToShortDateString

            Dim conn As New OracleConnection(cad1)
            conn.Open()

            Dim tx As OracleTransaction
            tx = conn.BeginTransaction()

            Try

                'ALTA TRABAJADOR--------------
                Dim cmd1 As New OracleCommand()
                cmd1 = conn.CreateCommand()
                cmd1.Transaction = tx

                Dim sqlString1 As String = "insert into DOC_LABORAL.t_Pcp_Trabajadores" & _
                                                "  (" & _
                                                "   id_trabajador," & _
                                                "   id_sexo," & _
                                                "   nro_documento," & _
                                                "   pai_cod_pais," & _
                                                "   id_numero," & _
                                                "   id_empleador," & _
                                                "   id_sindicato," & _
                                                "   idconvenio," & _
                                                "   fec_registro" & _
                                                "   )" & _
                                                "values" & _
                                                "  (" & _
                                                "   DOC_LABORAL.sec_t_pcp_trabajadores.NEXTVAL," & _
                                                "     '" + usuarioCidiTrabajador.Id_Sexo.ToString.Trim + "'," & _
                                                "     '" + usuarioCidiTrabajador.NroDocumento.ToString.Trim + "'," & _
                                                "     '" + usuarioCidiTrabajador.PaiCodPais.ToString.Trim + "'," & _
                                                "     " + usuarioCidiTrabajador.Id_Numero.ToString.Trim + "," & _
                                                "     " + mIdEmpleador.ToString.Trim + "," & _
                                                "     " + midSindicato.ToString.Trim + "," & _
                                                "     " + midConvenio.ToString.Trim + "," & _
                                                "     '" + mFechaRegistro.ToString.Trim + "'" & _
                                                "   )"

                cmd1.CommandType = CommandType.Text
                cmd1.CommandText = sqlString1

                cmd1.ExecuteNonQuery()

                Dim mIdTrabajador As Int32 = ClsPSP.IdEmpleadoPCP(usuarioCidiTrabajador.Id_Sexo.ToString.Trim, usuarioCidiTrabajador.NroDocumento.ToString.Trim, usuarioCidiTrabajador.PaiCodPais.ToString.Trim, usuarioCidiTrabajador.Id_Numero.ToString.Trim, mIdEmpleador.ToString.Trim)

                'ALTA RELACION LABORAL--------------
                Dim cmd2 As New OracleCommand()
                cmd2 = conn.CreateCommand()
                cmd2.Transaction = tx


                Dim sqlString2 As String = "insert into DOC_LABORAL.t_pcp_relaciones_laboral" & _
                                                "  (" & _
                                                "   id_pcp_relacion_laboral," & _
                                                "   id_trabajador," & _
                                                "   id_pcp_categorias," & _
                                                "   id_pcp_horas_semanales," & _
                                                "   remuneracion_pactada," & _
                                                "   id_pcp_modalidad_liquidacion," & _
                                                "   id_pcp_modalidad_prestacion," & _
                                                "   fec_registro," & _
                                                "   fec_alta" & _
                                                "   )" & _
                                                " values " & _
                                                "  (" & _
                                                "   DOC_LABORAL.SEC_T_PCP_RELACIONES_LABORAL.NEXTVAL," & _
                                                "   " & mIdTrabajador.ToString.Trim() & "," & _
                                                "   " & 1 & "," & _
                                                "   " & 1 & "," & _
                                                "   " & 100 & "," & _
                                                "   " & 1 & "," & _
                                                "   " & 1 & "," & _
                                                "   '" & mFechaRegistro.ToString.Trim & "'," & _
                                                "   '" & "12/02/2015" & "'" & _
                                                "   )"

                cmd2.CommandType = CommandType.Text
                cmd2.CommandText = sqlString2

                cmd2.ExecuteNonQuery()

                'ALTA LUGARES DE TRABAJO - HORAS TRABAJADAS ----------------


                Dim mIdPcpRelacionLaboral As Int32 = ClsPSP.ultimaRelacionLaboralIngresada(mIdTrabajador.ToString.Trim())
                Dim DT As DataTable = Session("miTablaPCP")

                For Each row As DataRow In DT.Rows
                    Dim cmd3 As New OracleCommand()
                    cmd3 = conn.CreateCommand()
                    cmd3.Transaction = tx

                    Dim mIdLugarTrabajo As String = row("ID_PCP_LUGAR_TRABAJO").ToString.Trim
                    Dim mHorasTrabajo As String = row("HORAS_TRABAJO").ToString.Trim

                    Dim sqlString3 As String = "insert into DOC_LABORAL.t_pcp_relaciones_lugar" & _
                                "  (" & _
                                "   id_pcp_relacion_lugar," & _
                                "   id_pcp_relacion_laboral," & _
                                "   id_pcp_lugar_trabajo," & _
                                "   horas_trabajo" & _
                                "   )" & _
                                "values" & _
                                "  (" & _
                                "   DOC_LABORAL.SEC_T_PCP_RELACIONES_LUGAR.NEXTVAL," & _
                                "   " & mIdPcpRelacionLaboral.ToString.Trim & "," & _
                                "   " & mIdLugarTrabajo & "," & _
                                "   " & mHorasTrabajo & "" & _
                                "   )"

                    cmd3.CommandType = CommandType.Text
                    cmd3.CommandText = sqlString3

                    cmd3.ExecuteNonQuery()
                Next

                tx.Commit()
            Catch ex As Exception
                tx.Rollback()
                Throw New Exception(ex.Message)

        Finally
            tx.Dispose()
            conn.Close()
        End Try



    End Sub

    Protected Sub Button11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button11.Click

        'Session("LoginEmpresa") = "SI"
        'Session("LoginMinisterio") = "SI"
        'Session("Logueada") = "SI"
        'Session("cuit") = "30700254425"
        'Session("empresa") = "asegada"
        'Session("cuit") = "30700254425"
        'Session("UsuarioMin") = "33707713289"
        'Session("UsuarioMinId") = 2
        'Session("nrocuenta") = 66743
        'Response.Redirect("frmAURegistrar.aspx")
        If txtcuit.Text.Substring(0, 1) = 2 Then
            Session("idformaJuridica") = "02"
        Else
            Session("idformaJuridica") = "04"
        End If

        Dim dt As DataTable
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, "select nrocuenta from empresas where cuit = " & txtcuit.Text.Trim & "").Tables(0)
        If dt.Rows.Count > 0 Then

            'Session("LoginEmpresa") = "SI"
            Session("LoginMinisterio") = "SI"
            Session("Logueada") = "SI"
            'Session("cuit") = "30700254425"
            Session("empresa") = "asegada"
            Session("cuit") = txtcuit.Text.Trim
            Session("UsuarioMin") = "33707713289"
            Session("UsuarioMinId") = 2
            Session("nrocuenta") = dt.Rows(0)("nrocuenta").ToString().Trim

            'Session("nrocuenta") = 66743
            'Response.Redirect("frmAutorizados.aspx")
            'Response.Redirect("frmPantallaBarandillaDocumentosPdf.aspx")
            'Response.Redirect("EmpleadosABM.aspx")
            'Response.Redirect("frmSIN-Contactos.aspx")
            'Response.Redirect("frmSIN-Apoderados.aspx")
            'Response.Redirect("frmSIN-Actividades.aspx")
            'Response.Redirect("frmSIN-Generales.aspx")
            Response.Redirect("frmSIN-Domicilios.aspx")

        End If
    End Sub

    Protected Sub Button12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button12.Click

        'Dim par As New ArrayList
        'par.Add("1")
        'Response.Redirect("frmEVEdicion.aspx?p=" & clsUtiles.CodificaParametros(par, "-"))
        'Response.Redirect("frmEVEdicion.aspx")
        'Response.Redirect("frmEVInscripciones.aspx")

        Dim par As New ArrayList

        par.Add("27257545046")
        par.Add("1,3,4,12")


        ' Response.Redirect("frmEvbajas.aspx?p=" & clsUtiles.CodificaParametros(par, "|"))
        Response.Redirect("frmEvValidacion.aspx?p=" & clsUtiles.CodificaParametros(par, "|"))

        'Response.Redirect("frmEVEventosAdministracion.aspx")

    End Sub

    Protected Sub btnEnviomails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviomails.Click
        EnvioMailEventos("Jornadas de Salud y Seguridad Ocupacional Compromiso y Participacion 2018", txtemail.Text.Trim, "1,2,3", "20207850331")
    End Sub

    Public Sub EnvioMailEventos(ByVal Evento As String, ByVal email As String, ByVal idevento As String, ByVal cuil As String)



        Dim MailFrom As String = String.Empty
        Dim MailTo As String = String.Empty
        Dim Asunto As String = String.Empty
        Dim CuerpoMail As String = String.Empty
        Dim FinPie As String = String.Empty
        Dim ImageFondo As String = String.Empty
        Dim Server As String = String.Empty
        Dim Usuario As String = String.Empty
        Dim Clave As String = String.Empty

        Dim UsuarioSistema As String = String.Empty
        MailTo = email
        UsuarioSistema = cuil


        Dim parsincod As New ArrayList
        parsincod.Add(cuil)
        parsincod.Add(idevento)
        Dim url As String = RutaHttp() & "frmEvValidacion.aspx?p=" & clsUtiles.CodificaParametros(parsincod, "-") & ""

        MailFrom = ClaseManejadoraMail.MailFrom

        Dim cempadro As New clsEmpadro
      


        Asunto = "Ministerio de Trabajo de Cordoba - Inscripción Evento"
        CuerpoMail = "Ud. Se ha Inscripto al Evento: " & Evento & " . <br/>" _
                  & "Al realizar un solo CLICK, confirma su inscripción y permite generar la cuponera de asistencia al Evento en forma automática. <br/>" _
                  & "Si no ha podido descargar la cuponera desde el enlace para su impresión puede recuperarla ingresando a https://trabajo.cba.gov.ar/frmEveventos.aspx <br/>" _
                  & "Para confirmar su inscripción ingresar al siguiente link: " _
                  & "<a href=" & url & "><input type='button' value='Click Aqui'></a><br/>" _
                  & "SU DEMORA EN CONFIRMAR LA INSCRIPCION PUEDE DEJARLO SIN CUPO Y NO PODRA ASISTIR A LAS ACTIVIDADES"


        FinPie = "Ministerio de Trabajo de Córdoba"
        ImageFondo = RutaHttp() & "images/taco_ministerio_evento.jpg"

        Server = ClaseManejadoraMail.ServerGMail
        Usuario = ""
        Clave = ""

        ClaseManejadoraMail.EnviarMailEvento("mintrabajoeventos@cba.gov.ar", MailTo, Asunto, CuerpoMail, FinPie, ImageFondo, Server, Usuario, Clave, False, "#4B6E50", "#4B6E50", "C7D7A8", "#083105", "#4B6E50")


    End Sub

    Protected Sub Button13_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button13.Click
        Session("LoginMinisterio") = "SI"
        Session("Logueada") = "SI"
        'Session("cuit") = "30700254425"
        Session("empresa") = "asegada"
        Session("cuit") = txtcuit.Text.Trim
        Session("UsuarioMin") = "33707713289"
        Session("UsuarioMinId") = 2

        Response.Redirect("frmAcreditaPagos.aspx")
    End Sub
End Class
