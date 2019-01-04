Imports System.Data
Imports System.Data.OracleClient
Imports CapaDatos

Partial Class Prueba_DC_DB
    Inherits System.Web.UI.Page

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim conexion As New OracleConnection("DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin")

        Try
            conexion.Open()

            Dim comm As New OracleCommand("insert into T_COMUNES.T_VINCULOS(ID_VINCULO, TIPO_VINCULO) values ('" + txtNom.Text + "', '" + txtApe.Text + "')", conexion)
            comm.ExecuteNonQuery()

            'Dim nombre As String = "P"
            'Dim sqlQue As String = "select * from T_COMUNES.T_VINCULOS where ID_VINCULO = '" + nombre + "' "

            'Dim command As New OracleCommand(sqlQue, conexion)
            'Dim reader As OracleDataReader = command.ExecuteReader()


            'While reader.Read()

            '    txtNom.Text = reader.GetValue(0)
            '    txtApe.Text = reader.GetValue(1)
            'End While

            'reader.Close()

            comm.Dispose()
            conexion.Close()

            MessageBoxShow("EXITO")

        Catch ex As Exception

            MessageBoxShow(ex.Message)
            conexion.Close()


        End Try

    End Sub

    Public Sub MessageBoxShow(message As String)

        Response.Write("<script type='text/javascript'> alert('" + message + "') </script>")

    End Sub
End Class
