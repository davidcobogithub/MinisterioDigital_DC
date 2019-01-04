Imports System.Data
Imports System.Data.OracleClient
Imports CapaDatos


Partial Class MasterPage3
    Inherits System.Web.UI.MasterPage
    Private cmenu As New clsmenu

    Dim cencr As New ClaseEncripta("1234")
    Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Not IsPostBack = True Then

        '    Dim b As Boolean
        '    b = False
        '    If Session("LoginEmpresa") = "SI" Then               
        '        lblEmpresa.Text = Session("empresa").ToString.Trim

        '        Menu1.Visible = True
        '        MenuMinisterio.Visible = False
        '        b = True
        '        Me.divCidi.Visible = False
        '    End If

        '    If Session("LoginMinisterio") = "SI" Then

        '        lblNombreDeUsuarioCidi.Text = Session("empresa").ToString.Trim

        '        'If lblNombreDeUsuarioCidi.Text.Length > 19 Then
        '        '    lblNombreDeUsuarioCidi.Text.Substring(0, 19)
        '        'End If

        '        Label1.Text = ""
        '        lblEmpresa.Text = " "
        '        Menu1.Visible = False
        '        MenuMinisterio.Visible = True

        '        ArmarMenu()
        '        b = True
        '    End If

        '    If Session("LoginPCP") = "SI" Then
        '        b = True
        '    End If

        '    If Not b Then
        '        Response.Redirect("index.aspx")
        '    End If

        '    'Session("Logueada") Sirve para saber si se muestra o no el menu de la empresa o del ministerio respectivamente. Dependiendo por que Login ingresaron
        '    If Session("Logueada") = "NO" Or IsNothing(Session("Logueada")) Then

        '        Menu1.Visible = False
        '        MenuMinisterio.Visible = False
        '        Label1.Visible = False
        '        lblEmpresa.Visible = False

        '    End If
        'End If



    End Sub

    Private Sub ArmarMenu()

        Dim dtmenu As New DataTable
        Dim dtmenuc As New DataTable

        Dim drmenu As DataRow
        dtmenu = cmenu.ObtenerMenuRaiz(BuscarIdusuario(Session("UsuarioMin")))

        For Each drmenu In dtmenu.Rows

            If drmenu("idpadre") = 0 Then
                Dim mnuitems As New MenuItem
                mnuitems.Value = drmenu("idmenu")
                mnuitems.Text = drmenu("descripcion").ToString.TrimStart.TrimEnd
                'mnuitems.ImageUrl = drmenu("icono").ToString()
                mnuitems.NavigateUrl = drmenu("url").ToString.TrimStart.TrimEnd
                MenuMinisterio.Items.Add(mnuitems)

                dtmenuc = cmenu.ObtenerMenuHijos(drmenu("idmenu"), BuscarIdusuario(Session("UsuarioMin")))
                AgregarNuevoItem(mnuitems, dtmenuc)
            End If
        Next

    End Sub

    Private Sub AgregarNuevoItem(ByRef mnuitems As MenuItem, ByVal dtmenu As DataTable)
        Dim drmenu As DataRow
        Dim dtmenucn As DataTable

        Dim vIdMenu As Integer



        For Each drmenu In dtmenu.Rows
            vIdMenu = drmenu("idpadre").ToString()

            If vIdMenu.Equals(18) Then
                Dim x As Integer
                x = 25
            End If

            If vIdMenu.Equals(CInt(mnuitems.Value)) Then
                Dim Nuevomnuitems As New MenuItem
                Nuevomnuitems.Value = drmenu("idmenu")
                Nuevomnuitems.Text = drmenu("descripcion").ToString.TrimStart.TrimEnd
                'Nuevomnuitems.ImageUrl = drmenu("icono").ToString()
                Nuevomnuitems.NavigateUrl = drmenu("url").ToString.TrimStart.TrimEnd

                mnuitems.ChildItems.Add(Nuevomnuitems)

                If drmenu.Item("habilitado") = 1 Then
                    Nuevomnuitems.Enabled = True
                Else
                    Nuevomnuitems.Enabled = False
                End If

                dtmenucn = cmenu.ObtenerMenuHijos(drmenu("idmenu"), BuscarIdusuario(Session("UsuarioMin")))
                AgregarNuevoItem(Nuevomnuitems, dtmenucn)

            End If
        Next
    End Sub

    Private Function BuscarIdusuario(ByVal pusuario As String) As Integer
        Dim iduser As Integer
        Dim ssql As String = "select idusuario from usuarios where usuario = '" & pusuario.TrimStart.TrimEnd & "' "
        iduser = OracleHelper.ExecuteScalar(cad1, CommandType.Text, ssql)
        Return iduser
    End Function

    Protected Sub BtnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCerrar.Click
        Response.Redirect("Autenticacion.aspx?CS=1")
    End Sub

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVolver.Click
        Response.Redirect("index.aspx")
    End Sub
End Class

