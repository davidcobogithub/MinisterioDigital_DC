Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OracleClient
Imports CapaDatos


Public Class clsmenu

    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Private ssql As String = String.Empty
    Private dt As New DataTable
    Private pidpadre As Integer

    Public Function ObtenerMenuRaizABM(ByVal pidusuario As Integer) As DataTable
        ssql = "SELECT idmenu, Descripcion, Posicion, Idpadre, Icono, Habilitado, Url" _
             & " FROM MenuXusuarios WHERE idusuario = " & pidusuario & " and idpadre = 0"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function ObtenerMenuRaiz(ByVal pidusuario As Integer) As DataTable
        ssql = "SELECT idmenu, case idusuario when 2 then  descripcion when 4 then descripcion" _
             & " else case habilitado when 1 then Descripcion else '-----' " _
             & " end end Descripcion, Posicion, Idpadre, Icono, Habilitado, Url" _
             & " FROM MenuXusuarios WHERE idusuario = " & pidusuario & " and idpadre = 0"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function ObtenerMenuRaiz() As DataTable
        ssql = "SELECT idmenu, Descripcion, Posicion, Idpadre, Icono, Habilitado, Url" _
             & " FROM Menu WHERE idpadre = 0 and idtipomenu = 1 order by posicion asc"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function ObtenerMenuHijos(ByVal pidmenu As Integer, ByVal pidusuario As Integer) As DataTable
        ssql = "SELECT idmenu, case idusuario when 2 then  descripcion when 4 then descripcion" _
             & " else case habilitado when 1 then Descripcion else '-----' " _
             & " end end Descripcion, Posicion, to_number(Idpadre) Idpadre, Icono, " _
              & "  to_char(Habilitado) Habilitado, Url" _
              & " FROM MenuXusuarios WHERE idusuario = " & pidusuario & " and idpadre = " & pidmenu & " order by posicion asc"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function ObtenerMenuHijosABM(ByVal pidmenu As Integer, ByVal pidusuario As Integer) As DataTable
        ssql = "SELECT idmenu, Descripcion, Posicion, to_number(Idpadre) Idpadre, Icono, " _
              & "  to_char(Habilitado) Habilitado, Url" _
              & " FROM MenuXusuarios WHERE idusuario = " & pidusuario & " and idpadre = " & pidmenu & " order by posicion asc"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function ObtenerMenuHijos(ByVal pidmenu As Integer) As DataTable
        ssql = "SELECT idmenu, Descripcion, Posicion, Idpadre, Icono, Habilitado, Url" _
              & " FROM Menu WHERE idpadre = " & pidmenu & " and idtipomenu = 1 order by posicion asc"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function BuscarUsuarios() As DataTable
        ssql = "select  idusuario, usuario  from usuarios U " _
              & "inner join usuariostipospermisos UP on u.codigopermiso = UP.codigopermiso " _
              & "where u.codigopermiso <> 2"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Sub BorrarMenuXusuario(ByVal puser As Integer)
        ssql = "delete from menuxusuarios where idusuario = " & puser & " "
        OracleHelper.ExecuteNonQuery(cad1, CommandType.Text, ssql)
    End Sub

    Public Sub CrearMenuXusuario(ByVal pidusuario As Integer, ByVal pidmenu As Integer, ByVal pdesc As String, ByVal pidpadre As Integer, ByVal pposicion As Integer, ByVal picono As String, ByVal phabilitado As Byte, ByVal purl As String)
        ssql = "Insert into MENUXUSUARIOS (idmenu, idusuario, descripcion, idpadre,posicion, icono, habilitado, url) values " _
             & " (" & pidmenu & "," & pidusuario & ", '" & pdesc & "', " & pidpadre & ", " & pposicion & ", '" & picono & "', '" & phabilitado & "', '" & purl & "')"
        OracleHelper.ExecuteNonQuery(cad1, CommandType.Text, ssql)
    End Sub

    Public Function UsuarioTieneMenu(ByVal pidusuario As Integer) As Boolean
        Dim tiene As Boolean
        ssql = "select * from menuxusuarios where idusuario = " & pidusuario & ""
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        If dt.Rows.Count > 0 Then
            tiene = True
        Else
            tiene = False
        End If
        Return tiene
    End Function

End Class


