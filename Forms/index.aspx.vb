Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Text
Imports System.Security.Cryptography
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports CapaDatos


Partial Class index
    Inherits System.Web.UI.Page
    Dim cencr As New ClaseEncripta("1234")
    Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())


#Region "Variables"
    ''' <summary>
    ''' Variable para guardar el valor de la cookie
    ''' </summary>
    Public _Hash As String
#End Region


#Region "Load"

    

#End Region



#Region "Obtener Usuario"

    ''' <summary>
    ''' Se invoca al metodo Obtener_Usuario_Aplicacion de la web api para obtener los datos 
    ''' del usuario logueado
    ''' </summary>
    ''' <param name="_Hash"></param>
    Protected Sub ObtenerUsuario(ByVal _Hash As String)
        Dim _TimeStamp As String = DateTime.Now.ToString("yyyyMMddHHmmssfff")
        Dim _Token As String = Config.ObtenerToken(_TimeStamp)

        Dim _URI As String = Config._Url_ApiCuentaCiDi + "api/Usuario/Obtener_Usuario_Aplicacion"
        'Se crea un string con la petición a la web api concatenando los paerametros necesarios
        ' Dim _UrlRequest As String = Convert.ToString((Convert.ToString((Convert.ToString((_URI & Convert.ToString("?IdAplicacion=")) + Config._Cidi_Id_Aplicacion + "&Contrasenia=" + Config._Cidi_Pass_Aplicacion + "&HashCookie=") & _Hash) + "&TokenValue=") & _Token) + "&TimeStamp=") & _TimeStamp
        Dim _UrlRequest As String = _URI & "?IdAplicacion=" & Config._Cidi_Id_Aplicacion.ToString().Trim() & "&Contrasenia=" & Config._Cidi_Pass_Aplicacion.ToString().Trim() & "&HashCookie=" & _Hash & "&TokenValue=" & _Token & "&TimeStamp=" & _TimeStamp

        Dim _Request As HttpWebRequest = DirectCast(WebRequest.Create(_UrlRequest), HttpWebRequest)
        _Request.Method = "GET"

        Dim _Response As WebResponse = _Request.GetResponse()
        Dim _Reader As New StreamReader(_Response.GetResponseStream(), Encoding.UTF8)
        Dim _Result As [String] = _Reader.ReadToEnd()

        'Usamos la librería Newtonsoft.Json para deserializar el objeto, la librería esta inculida en el 
        'proyecto en la carpeta dlls
        Dim _Usuario As Usuario = JsonConvert.DeserializeObject(Of Usuario)(_Result)


        '---------------------------------------------------------------------------------------
        'Variables Integradas DOC_LABORAL
        Dim banderaEncontrado As Boolean = False
        Dim usuario As String = String.Empty
        Dim mensaje As String
        Dim clsUsario As New clsUsuarios
        '---------------------------------------------------------------------------------------

        If _Usuario IsNot Nothing Then
            If _Usuario.Respuesta.Resultado = "OK" Then
                'lblApellido.Text = _Usuario.Apellido.ToUpper()
                'lblNombre.Text = _Usuario.Nombre.ToUpper()
                'lblCuil.Text = _Usuario.CUIL


                'cargar session
                Dim dt As New DataTable
                dt = clsUsario.usuariosGetByUsuario(_Usuario.CUIL)

                If (dt.Rows.Count > 0) Then
                    usuario = dt.Rows(0)("usuario1").ToString().ToUpper().Trim()
                    Session("UsuarioMin") = _Usuario.CUIL
                    Session("UsuarioMinId") = dt.Rows(0)("idusuario").ToString().ToUpper().Trim()
                    banderaEncontrado = True
                    Session("TipoUsuario") = dt.Rows(0)("permiso").ToString().ToUpper().Trim()

                End If


                If (banderaEncontrado = True) Then

                    Session("nrocuenta") = -1
                    Session("Empresa") = String.Empty
                    Session("Empresa") = (usuario & " - MINISTERIO").Substring(0, Math.Min((usuario & " - MINISTERIO").ToString.Length, 25))

                    Session("Logueada") = "SI"
                    Session("LoginEmpresa") = "NO"
                    Session("LoginMinisterio") = "SI"
                    ' Loguear Ingreso Exitoso
                    ClaseOperLog.loguear(Session("UsuarioMin"), Date.Now.ToShortDateString, "Logueo Exitoso", cad1, Request.UserHostAddress.ToString())

                    If Me.validoFechaCaduca(_Usuario.CUIL) = False Then
                        Session("caduca") = "N"
                        Response.Redirect("frmPantallaInicio1.aspx")
                    Else
                        Session("caduca") = "S"
                        Response.Redirect("frmcambiopass.aspx")
                    End If
                Else
                    '  Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('Usuario No Encontrado En DOCLABORAL');", True)
                    Response.Redirect("https://cidi.cba.gov.ar/restringido/14")

                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType, "script1", "alert('" & _Usuario.Respuesta.Resultado & "');", True)
            End If
        End If
    End Sub

    Public Function validoFechaCaduca(ByVal pcuit As String) As Boolean
        Dim fecha As Date
        Dim ssql As String = "Select to_char(fechacaduc,'dd/mm/yyyy') fechacaduc from usuarios  Where usuario = '" & pcuit.Trim() & "' "
        fecha = OracleHelper.ExecuteScalar(cad1, CommandType.Text, ssql)

        'If Date.Today.Date.ToShortDateString >= fecha Then
        '    Return True
        'Else
        Return False
        'End If

    End Function

#End Region



    
    'Protected Sub A1_Click(ByVal sender As Object, ByVal e As System.Web.UI) Handles A1.Click


    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

      
        If Not sitioOnLine() Then
            Response.Redirect("frmSitioOffLine.aspx")
        End If
        Dim pos As Integer = Request.QueryString("pos")
        If pos = 1 Then
            HLK1.DataBind()
        End If

    End Sub

    Private Function sitioOnLine() As Boolean

        Dim xmlToDt As New ClaseXmlManager
        Dim t As DataTable = xmlToDt.XmlToDataSet("estadositio.xml").Tables(0)

        If CInt(t.Rows(0)(0).ToString().Trim()) = 2 Then
            Return False
        End If

        Return True

    End Function


    Protected Sub AbrirNoticias(ByVal idDoc As Integer)
        Dim camponombrearchivo As String
        Dim ruta As String

        Select Case idDoc
            Case 2
                ruta = "http://comercioyjusticia.info/blog/profesionales/cordoba-despapeliza-libros-sueldos-desde-mayo/#content"
            Case 3
                ruta = "https://trabajo.cba.gov.ar/ayudas/IMAGES/Invitacionmtd.jpg"
            Case Else
                camponombrearchivo = idDoc & ".pdf"
                ruta = RutaHttp() & "ayudas/docs/" & camponombrearchivo
        End Select


        Dim popupScript As String
        popupScript = String.Empty
        popupScript = "<script language='JavaScript'>"
        popupScript += "window.open('" + ruta + "', 'CustomPopUp', "
        popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=YES ,resizable=NO')"
        popupScript += "</script>"
        Page.RegisterStartupScript("popup", popupScript)

        Return
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

    'Protected Sub not1_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles not1.ServerClick
    '    AbrirNoticias(2)
    'End Sub
    'Protected Sub not2_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles not2.ServerClick
    '    AbrirNoticias(1002)
    'End Sub
    'Protected Sub not3_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles not3.ServerClick
    '    AbrirNoticias(1001)
    'End Sub
    'Protected Sub not4_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles not4.ServerClick
    '    AbrirNoticias(1003)
    'End Sub

    Public Sub HLK1_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        '''//////   
        'Verificar si la cookie existe, para saber si el usuario ya esta logueado
        '''//////      
        ''' 

        If Request.Cookies("CiDi") IsNot Nothing Then
            '''//////
            'Se guarda el valor de la cookie en una variable
            '''//////
            _Hash = Request.Cookies("CiDi").Value.ToString()
            ObtenerUsuario(_Hash)
        Else
            '''//////
            'Como el usuario no se logueo se redirecciona al login de CiDi

            ' Response.Redirect(Config._Url_CiDi + "?url=" + Config._Url_App_Test + "&app=" + Config._Cidi_Id_Aplicacion.ToString().Trim())
            Response.Redirect(Config._Url_CiDi + "?url=" + "https://trabajo.cba.gov.ar/Autenticacion.aspx" + "&app=" + Config._Cidi_Id_Aplicacion.ToString().Trim())

        End If


    End Sub



End Class
