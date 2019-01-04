Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OracleClient

Public Class ClsNotificacionTipos


    'Dim cencr As New ClaseEncripta("1234")
    'Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Public Shared Sub TNotificacionesTipos_insert(ByVal id_not_tipo As Integer, ByVal nom As String)

        Try
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim query As String = "insert into DOC_LABORAL.T_NOTIFICACION_TIPOS (ID_TIPO, NOMBRE) values (" & id_not_tipo.ToString() & ", '" & nom & "')"

            Dim comm As New OracleCommand(query, conexion)

            comm.ExecuteNonQuery()
            comm.Dispose()
            conexion.Close()

        Catch ex As Exception
            Throw

        End Try
    End Sub


    Public Shared Sub TNotificacionesTipos_update(ByVal id_not_tipo As Integer, ByVal nom As String)

        Try
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim query As String = "update DOC_LABORAL.T_NOTIFICACION_TIPOS set NOMBRE = '" & nom & "' where ID_TIPO = " & id_not_tipo.ToString()

            Dim comm As New OracleCommand(query, conexion)
            comm.ExecuteNonQuery()
            comm.Dispose()
            conexion.Close()
        Catch ex As Exception
            Throw

        End Try
    End Sub


    Public Shared Function TNotificacionesTipos_findByID(ByVal id_not_tipo As Integer) As ArrayList

        Dim listaResultado As New ArrayList

        Try
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim query As String = "Select * from DOC_LABORAL.T_NOTIFICACION_TIPOS where ID_TIPO = '" + id_not_tipo.ToString() + "'"

            Dim command As New OracleCommand(query, conexion)
            Dim reader As OracleDataReader = command.ExecuteReader()

            While reader.Read()

                For i As Integer = 0 To reader.FieldCount

                    listaResultado.Add(reader.GetValue(i).ToString().Trim())
                Next

            End While

            reader.Close()
            command.Dispose()
            conexion.Close()
        Catch ex As Exception
            Throw
        End Try
        Return listaResultado
    End Function

End Class


