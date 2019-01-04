Imports System.Data
Imports System.Data.OracleClient
Imports CapaDatos
Imports Microsoft.VisualBasic

Public Class clsUsuarios
    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())


    Public Function BuscarUsuariosSecretaria() As DataTable
        Dim dt As DataTable
        Dim ssql As String
        ssql = "select usuario Id, usuario Estado from usuarios where usuario = 'asegada' Order By Usuario ASC"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function FechaCaduca() As Date
        Dim fecdev As Date
        fecdev = Date.Today.AddMonths(6)
        Return fecdev
    End Function

    Public Function BuscarSiglaAreaUsuarios(ByVal pidusuario As Integer) As String
        Dim sigla As String
        Dim ssql As String
        ssql = "select nvl(a.n_area_abreviatura,'SinAsignar') sigla from doc_laboral.usuarios u " _
             & "inner join doc_laboral.t_areas a on u.idarea = a.id_area " _
             & "where idusuario = " & pidusuario & ""
        sigla = OracleHelper.ExecuteScalar(cad1, CommandType.Text, ssql)
        Return sigla
    End Function

    Public Function BuscarCodigoSUACUsuarios(ByVal pidusuario As Integer) As String
        Dim codigo As String
        Dim ssql As String
        ssql = "select nvl(l.codsuac, 'SinAsignar') CodSuac  from usuarios U " _
              & "inner join lugaridentificacion L on u.codigodelegacion =  l.idlugaridenti " _
              & "where idusuario = " & pidusuario & ""
        codigo = OracleHelper.ExecuteScalar(cad1, CommandType.Text, ssql)
        Return codigo
    End Function

    Public Function BuscarIDAreaUsuarios(ByVal pidusuario As Integer) As Integer
        Dim idarea As Integer
        Dim ssql As String
        ssql = "select nvl(u.idarea, 0) Idarea   from usuarios U " _
             & "where idusuario = " & pidusuario & ""
        idarea = OracleHelper.ExecuteScalar(cad1, CommandType.Text, ssql)
        Return idarea
    End Function


#Region "CIDI"

    Public Function InsetarUsuarioCIDI(ByVal _usuario As String, ByVal _password As String, ByVal _apellido As String, ByVal _nombre As String, ByVal _nro_documento As String, ByVal _email As String, ByVal _ip As String) As String

        Dim rta As String
        Dim con As New OracleConnection(cad1)
        con.Open()

        Try

            Dim com As New OracleCommand()
            com.CommandText = "pkg_interfaz_cidi.registrar_usuario"
            'com.CommandText = "registrar_usuario"
            com.CommandType = CommandType.StoredProcedure
            com.Connection = con

            Dim pusuario As New OracleParameter("i_n_usuario", OracleType.VarChar)
            pusuario.Value = _usuario
            com.Parameters.Add(pusuario)

            Dim ppass As New OracleParameter("i_password", OracleType.VarChar)
            ppass.Value = _password
            com.Parameters.Add(ppass)

            Dim papellido As New OracleParameter("i_apellido", OracleType.VarChar)
            papellido.Value = _apellido
            com.Parameters.Add(papellido)

            Dim pnombre As New OracleParameter("i_nombre", OracleType.VarChar)
            pnombre.Value = _nombre
            com.Parameters.Add(pnombre)

            Dim pnrodoc As New OracleParameter("i_nro_documento", OracleType.VarChar)
            pnrodoc.Value = _nro_documento
            com.Parameters.Add(pnrodoc)

            Dim pemail As New OracleParameter("i_email", OracleType.VarChar)
            pemail.Value = _email
            com.Parameters.Add(pemail)

            Dim pip As New OracleParameter("i_ip", OracleType.VarChar)
            pip.Value = _ip
            com.Parameters.Add(pip)

            Dim pidusuario As New OracleParameter("o_id_usuario", OracleType.Number, 30)
            pidusuario.Direction = ParameterDirection.Output
            com.Parameters.Add(pidusuario)

            Dim presultado As New OracleParameter("o_resultado", OracleType.VarChar, 300)
            presultado.Direction = ParameterDirection.Output
            com.Parameters.Add(presultado)

            com.ExecuteNonQuery()
            rta = presultado.Value
            Return rta

        Catch ex As Exception
            con.Close()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function

    Public Function TraerUsuario(ByVal _usuario As String, ByVal _ip As String) As DataTable

        Dim dt As DataTable
        Dim param(3) As Object

        Try
            param(0) = _usuario
            param(1) = _ip
            param(2) = ""
            param(3) = ""
            dt = OracleHelper.ExecuteDataset(cad1, "pkg_interfaz_cidi.traer_usuario", param).Tables(0)
            'dt = OracleHelper.ExecuteDataset(cad1, "traer_usuario", param).Tables(0)
        Catch ex As Exception
            Throw ex
        End Try
        Return dt

    End Function


    Public Function validarUsuario(ByVal _user As String, ByVal _pass As String, ByVal _ip As String, ByVal _token As String) As String
        Dim mensaje As String = ""
        Dim conexion As New OracleConnection(cad1)
        Dim comando As New OracleCommand()

        Try
            comando.CommandType = CommandType.StoredProcedure
            comando.Connection = conexion
            'comando.CommandText = "login"
            comando.CommandText = "pkg_interfaz_cidi.login"

            conexion.Open()

            
            Dim i_n_usuario As OracleParameter = New OracleParameter("i_n_usuario", System.Data.OracleClient.OracleType.VarChar)
            i_n_usuario.Value = _user
            comando.Parameters.Add(i_n_usuario)

            Dim i_password As OracleParameter = New OracleParameter("i_password", System.Data.OracleClient.OracleType.VarChar)
            i_password.Value = _pass
            comando.Parameters.Add(i_password)

            Dim i_ip As OracleParameter = New OracleParameter("i_ip", System.Data.OracleClient.OracleType.VarChar)
            'Funcion para obtener el ip de una oc
            i_ip.Value = _ip
            comando.Parameters.Add(i_ip)


            Dim o_cursor As OracleParameter = New OracleParameter("o_cursor", System.Data.OracleClient.OracleType.Cursor)
            o_cursor.Direction = ParameterDirection.Output
            comando.Parameters.Add(o_cursor)


            Dim o_resultado As OracleParameter = New OracleParameter("o_resultado", System.Data.OracleClient.OracleType.VarChar, 300)
            o_resultado.Direction = ParameterDirection.Output
            comando.Parameters.Add(o_resultado)

            comando.ExecuteNonQuery()
            mensaje = o_resultado.Value.ToString().Trim()

        Catch ex As Exception
            mensaje = "Error, comuniquese con el administrador de sistemas"
        Finally
            conexion.Close()
            conexion.Dispose()
        End Try

        Return mensaje

    End Function

    Public Function usuariosGetByUsuario(ByVal _user As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim obj(1) As Object
            obj(0) = _user
            obj(1) = ""

            dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.usuariosGetByUsuario", obj).Tables(0)
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    Public Function cambiar_passwordCidi(ByVal _usuario As String, ByVal _old_pass As String, ByVal _new_pass As String, ByVal _ip As String) As String

        Dim rta As String
        Dim con As New OracleConnection(cad1)
        con.Open()

        Try

            Dim com As New OracleCommand()
            'com.CommandText = "cambiar_password"
            com.CommandText = "pkg_interfaz_cidi.cambiar_password"
            com.CommandType = CommandType.StoredProcedure


            Dim pusuario As New OracleParameter("i_n_usuario", OracleType.VarChar)
            pusuario.Value = _usuario
            com.Parameters.Add(pusuario)

            Dim poldpass As New OracleParameter("i_old_pass", OracleType.VarChar)
            poldpass.Value = _old_pass
            com.Parameters.Add(poldpass)

            Dim pnewpass As New OracleParameter("i_new_pass", OracleType.VarChar)
            pnewpass.Value = _new_pass
            com.Parameters.Add(pnewpass)

            Dim pip As New OracleParameter("i_ip", OracleType.VarChar)
            pip.Value = _ip
            com.Parameters.Add(pip)

            Dim presultado As New OracleParameter("o_resultado", OracleType.VarChar, 300)
            presultado.Direction = ParameterDirection.Output
            com.Parameters.Add(presultado)

            com.Connection = con
            com.ExecuteNonQuery()
            rta = presultado.Value
            Return rta

        Catch ex As Exception
            con.Close()
            Throw ex
        Finally
            con.Close()
        End Try


    End Function




#End Region






End Class
