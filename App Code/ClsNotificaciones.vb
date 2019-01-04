Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OracleClient
Imports System.Windows.Forms


Public Class ClsNotificaciones

    'Dim cencr As New ClaseEncripta("1234")
    'Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Public Shared Sub TNotificaciones_insert(ByVal id_not As Integer, ByVal id_ord As Integer, ByVal nrocuen As Integer,
                                             ByVal envia As Char, ByVal fech As DateTime, ByVal fecha_lect As DateTime,
                                             ByVal id_barandilla As Integer, ByVal id_area As Integer, ByVal id_model As Integer,
                                             ByVal id_usu As Integer)

        Try
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            Dim fecha As String = "TO_DATE('" & fecha_lect.ToString("d") & " 01:01:01 AM','dd/mm/yyyy HH:MI:SS AM')"

            conexion.Open()

            Dim query As String = "insert into DOC_LABORAL.T_NOTIFICACIONES (ID_NOTIFICACIONES,ID_ORDEN, NROCUENTA, ENVIADO, FECHA, " _
                                   & "FECHA_LECTURA, ID_BARANDILLADOCUMENTOPDF, ID_AREAS, ID_MODELO, ID_USUARIO)  values" _
                                   & " (" & id_not.ToString() & ", " & id_ord.ToString() & " , " & nrocuen.ToString() & " , '" _
                                   & envia.ToString() & "', " & fecha.ToString() & ", " & fecha.ToString() & ", " & id_barandilla.ToString() & ", " _
                                   & id_area.ToString() & " , " & id_model.ToString() & " , " & id_usu.ToString() & " )"

            Dim comm As New OracleCommand(query, conexion)

            comm.ExecuteNonQuery()
            comm.Dispose()

        Catch ex As Exception
            Throw

        End Try
    End Sub


    Public Shared Sub TNotificaciones_update(ByVal id_not As Integer, ByVal id_ord As Integer, ByVal nrocuen As Integer,
                                             ByVal envia As Char, ByVal fech As DateTime, ByVal fecha_lect As DateTime,
                                             ByVal id_barandilla As Integer, ByVal id_area As Integer, ByVal id_model As Integer,
                                             ByVal id_usu As Integer)

        Try
            Dim fecha As String = "TO_DATE('" & fecha_lect.ToString("d") & " 01:01:01 AM','dd/mm/yyyy HH:MI:SS AM')"
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim query As String = "update DOC_LABORAL.T_NOTIFICACIONES set ID_ORDEN = " & id_ord.ToString() & ", NROCUENTA = " & nrocuen.ToString() _
                                   & ",  ENVIADO = '" & envia.ToString() & "', " & "FECHA = " & fecha.ToString() & ", FECHA_LECTURA= " _
                                   & fecha.ToString() & "," & "ID_BARANDILLADOCUMENTOPDF = " & id_barandilla.ToString() & ", ID_AREAS = " _
                                   & id_area.ToString() & ", " & "ID_MODELO = " & id_model.ToString() & ", ID_USUARIO = " & id_usu.ToString() _
                                   & " where ID_NOTIFICACIONES = " & id_not.ToString()

            Dim comm As New OracleCommand(query, conexion)
            comm.ExecuteNonQuery()
            comm.Dispose()

        Catch ex As Exception
            Throw

        End Try
    End Sub


    Public Shared Function TNotificaciones_findByID(ByVal id_not As Integer) As ArrayList

        Dim listaResultado As New ArrayList

        Try
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim query As String = "select * from DOC_LABORAL.T_NOTIFICACIONES  where TipoDocumento = '" + id_not + "'"

            Dim command As New OracleCommand(query, conexion)
            Dim reader As OracleDataReader = command.ExecuteReader()

            While reader.Read()

                For i As Integer = 0 To reader.FieldCount

                    listaResultado.Add(reader.GetValue(i).ToString().Trim())
                Next

            End While

            reader.Close()
            command.Dispose()

        Catch ex As Exception
            Throw

        End Try
        Return listaResultado
    End Function

    Public Shared Function mostrarEmpresasPorNombre() As ArrayList

        Dim listaResultado As New ArrayList

        Try
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim command As New OracleCommand("select RAZON_SOCIAL from T_COMUNES.T_PERS_JURIDICA order by RAZON_SOCIAL ASC", conexion)
            Dim reader As OracleDataReader = command.ExecuteReader()

            While reader.Read()

                listaResultado.Add(reader.GetValue(0).ToString().Trim())

            End While

            reader.Close()
            command.Dispose()

        Catch ex As Exception
            Throw

        End Try
        Return listaResultado
    End Function



    Public Shared Function mostrarNombreActividadesEmpresas() As ArrayList

        Dim listaResultado As New ArrayList

        Try
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim command As New OracleCommand("select NOMBRE from DOC_LABORAL.EMPRESASACTIVIDADES order by NOMBRE ASC", conexion)
            Dim reader As OracleDataReader = command.ExecuteReader()

            While reader.Read()

                listaResultado.Add(reader.GetValue(0).ToString().Trim())

            End While

            reader.Close()
            command.Dispose()

        Catch ex As Exception
            Throw

        End Try
        Return listaResultado
    End Function

    Public Shared Function mostrarNombreEstadosEmpresas() As ArrayList

        Dim listaResultado As New ArrayList

        Try
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim command As New OracleCommand("select estado from DOC_LABORAL.estado order by estado ASC", conexion)
            Dim reader As OracleDataReader = command.ExecuteReader()

            While reader.Read()

                listaResultado.Add(reader.GetValue(0).ToString().Trim())

            End While

            reader.Close()
            command.Dispose()

        Catch ex As Exception
            Throw

        End Try
        Return listaResultado
    End Function

    Public Shared Function mostrarAreasMinisterio() As ArrayList

        Dim listaResultado As New ArrayList

        Try
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim command As New OracleCommand("select n_area from DOC_LABORAL.T_AREAS order by n_area ASC", conexion)
            Dim reader As OracleDataReader = command.ExecuteReader()

            While reader.Read()

                listaResultado.Add(reader.GetValue(0).ToString().Trim())

            End While

            reader.Close()
            command.Dispose()

        Catch ex As Exception
            Throw

        End Try
        Return listaResultado
    End Function

    Public Shared Function mostrarDelegaciones() As ArrayList

        Dim listaResultado As New ArrayList

        Try
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim command As New OracleCommand("SELECT distinct delegacion FROM doc_laboral.delegacion" _
                                             & " UNION SELECT lugaridentificacion FROM doc_laboral.lugaridentificacion", conexion)
            Dim reader As OracleDataReader = command.ExecuteReader()

            While reader.Read()

                listaResultado.Add(reader.GetValue(0).ToString().Trim())

            End While

            reader.Close()
            command.Dispose()

        Catch ex As Exception
            Throw

        End Try
        Return listaResultado
    End Function


    Public Shared Function mostrarRubros() As ArrayList

        Dim listaResultado As New ArrayList

        Try
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim command As New OracleCommand("SELECT n_rubro FROM t_comunes.t_rubros order by n_rubro ASC", conexion)
            Dim reader As OracleDataReader = command.ExecuteReader()

            While reader.Read()

                listaResultado.Add(reader.GetValue(0).ToString().Trim())

            End While

            reader.Close()
            command.Dispose()

        Catch ex As Exception
            Throw

        End Try
        Return listaResultado
    End Function

    Public Shared Function cargarTablaNotificaciones(ByVal cuit As String, ByVal razon As String, ByVal cantUno As String, ByVal cantDos As String, ByVal actividad As String, ByVal delegacion As String, ByVal estado As String, ByVal acuerdo As String, ByVal tipo_personeria As String, ByVal rubro As String, ByVal id_actividad1 As String, ByVal id_actividad2 As String) As DataTable

        Dim dt As New DataTable()
        Try
            Dim cad1 As String = "data source=encode_min_dc;user id=system;password = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            If cuit.Equals("") And razon.Equals("") Then

                If acuerdo.Equals("SELECCIONE TIPO PRESENTACIÓN") And delegacion.Equals("SELECCIONE DELEGACIÓN") And
                    cantUno.Equals("") And cantDos.Equals("") And tipo_personeria.Equals("SELECCIONE TIPO PERSONERÍA") And
                    estado.Equals("SELECCIONE ESTADO") And id_actividad1.Equals("") And id_actividad1.Equals("") And
                    id_actividad2.Equals("") And rubro.Equals("SELECCIONE RUBRO") And actividad.Equals("SELECCIONE ACTIVIDAD") Then

                    dt = Nothing
                Else

                    Dim sql As String = "select cuit, razon_social, tipopresentacion, lugaridentificacion, cantempleado, estadoempadronamiento, n_rubro, nombre, nrocuenta from doc_laboral.vt_constancia_empleador where 0=0 "

                    If Not acuerdo.Equals("SELECCIONE TIPO PRESENTACIÓN") Then
                        sql += "And tipopresentacion Like upper('%" & acuerdo.Trim() & "%') "
                    End If
                    If Not delegacion.Equals("SELECCIONE DELEGACIÓN") Then
                        sql += "And lugaridentificacion Like upper('%" & delegacion.Trim() & "%') "
                    End If
                    If Not cantUno.Equals("") And Not cantDos.Equals("") And cantUno.Equals(cantDos) Then
                        sql += "And cantempleado='" & cantUno.Trim() & "' "
                    End If
                    If Not cantUno.Equals("") And Not cantDos.Equals("") And Not cantUno.Equals(cantDos) Then
                        sql += "And cantempleado Between '" & cantUno.Trim() & "' and '" & cantDos.Trim() & "' "
                    End If
                    If cantUno.Equals("") And Not cantDos.Equals("") Then
                        sql += "And cantempleado <='" & cantDos.Trim() & "' "
                    End If
                    If Not cantUno.Equals("") And cantDos.Equals("") Then
                        Exit Function
                    End If
                    If Not tipo_personeria.Equals("SELECCIONE TIPO PERSONERÍA") Then

                        If tipo_personeria.Equals("FÍSICA") Then

                            sql += "And cuit Like '2%' "
                        End If

                        If tipo_personeria.Equals("JURÍDICA") Then

                            sql += "And cuit Like '3%' "
                        End If

                    End If
                    If Not estado.Equals("SELECCIONE ESTADO") Then
                        sql += "And estadoempadronamiento Like '%" & estado.Trim() & "%' "
                    End If
                    If Not id_actividad1.Equals("") And Not id_actividad2.Equals("") And id_actividad1.Equals(id_actividad2) Then
                        sql += "And id_actividad = '" & id_actividad1.Trim() & "' "
                    End If
                    If Not id_actividad1.Equals("") And Not id_actividad2.Equals("") And Not id_actividad1.Equals(id_actividad2) Then
                        sql += "And id_actividad  Between '" & id_actividad1.Trim() & "' and '" & id_actividad2.Trim() & "' "
                    End If
                    If id_actividad1.Equals("") And Not id_actividad2.Equals("") Then
                        sql += "And id_actividad <= '" & id_actividad2.Trim() & "' "
                    End If
                    If Not id_actividad1.Equals("") And id_actividad2.Equals("") Then
                        Exit Function
                    End If
                    If Not rubro.Equals("SELECCIONE RUBRO") Then
                        sql += "And n_rubro Like '%" & rubro.Trim() & "%' "
                    End If
                    If Not actividad.Equals("SELECCIONE ACTIVIDAD") Then
                        sql += "And nombre Like upper('%" & actividad.Trim() & "%') "
                    End If

                    sql += "order by razon_social asc"

                    Dim command As New OracleCommand(sql, conexion)
                    Dim da As New OracleDataAdapter(command)
                    da.Fill(dt)
                    dt.Columns(0).ColumnName = "CUIT"
                    dt.Columns(1).ColumnName = "RAZÓN SOCIAL"
                    dt.Columns(2).ColumnName = "TIPO DE PRESENTACIÓN"
                    dt.Columns(3).ColumnName = "DELEGACIÓN"
                    dt.Columns(4).ColumnName = "CANTIDAD EMPLEADOS"
                    dt.Columns(5).ColumnName = "ESTADO EMPADRONAMIENTO"
                    dt.Columns(6).ColumnName = "RUBRO"
                    dt.Columns(7).ColumnName = "ACTIVIDAD EMPRESA"
                    dt.Columns(8).ColumnName = "NÚMERO DE CUENTA"

                    command.Dispose()

                End If
            Else

                If Not cuit.Equals("") Then

                    Dim sql As String = "select cuit, razon_social, tipopresentacion, lugaridentificacion, cantempleado, estadoempadronamiento, n_rubro, nombre, nrocuenta from doc_laboral.vt_constancia_empleador" _
                                    & " where cuit = '" & cuit & "'"
                    Dim command As New OracleCommand(sql, conexion)
                    Dim da As New OracleDataAdapter(command)
                    da.Fill(dt)
                    dt.Columns(0).ColumnName = "CUIT"
                    dt.Columns(1).ColumnName = "RAZÓN SOCIAL"
                    dt.Columns(2).ColumnName = "TIPO DE PRESENTACIÓN"
                    dt.Columns(3).ColumnName = "DELEGACIÓN"
                    dt.Columns(4).ColumnName = "CANTIDAD EMPLEADOS"
                    dt.Columns(5).ColumnName = "ESTADO EMPADRONAMIENTO"
                    dt.Columns(6).ColumnName = "RUBRO"
                    dt.Columns(7).ColumnName = "ACTIVIDAD EMPRESA"
                    dt.Columns(8).ColumnName = "NÚMERO DE CUENTA"

                    command.Dispose()

                ElseIf Not razon.Equals("") Then

                    Dim sql As String = "select cuit, razon_social, tipopresentacion, lugaridentificacion, cantempleado, estadoempadronamiento, n_rubro, nombre, nrocuenta from doc_laboral.vt_constancia_empleador" _
                                   & " where razon_social Like upper('%" & razon & "%') order by razon_social asc"
                    Dim command As New OracleCommand(sql, conexion)
                    Dim da As New OracleDataAdapter(command)
                    da.Fill(dt)
                    dt.Columns(0).ColumnName = "CUIT"
                    dt.Columns(1).ColumnName = "RAZÓN SOCIAL"
                    dt.Columns(2).ColumnName = "TIPO DE PRESENTACIÓN"
                    dt.Columns(3).ColumnName = "DELEGACIÓN"
                    dt.Columns(4).ColumnName = "CANTIDAD EMPLEADOS"
                    dt.Columns(5).ColumnName = "ESTADO EMPADRONAMIENTO"
                    dt.Columns(6).ColumnName = "RUBRO"
                    dt.Columns(7).ColumnName = "ACTIVIDAD EMPRESA"
                    dt.Columns(8).ColumnName = "NÚMERO DE CUENTA"
                    command.Dispose()

                End If

            End If

            conexion.Close()
        Catch ex As Exception
            Throw
        End Try

        Return dt
    End Function

End Class
