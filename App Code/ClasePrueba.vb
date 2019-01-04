Imports Microsoft.VisualBasic
Imports System.Data
Imports Oracle.DataAccess.Client

Public Class ClasePrueba


    Public Sub conexionBD()

        Try
            Dim conexion As OracleConnection = New OracleConnection("Data Source = localhost; User ID = system; Password = admin; Integrated Security=True")
            conexion.Open()

            Console.WriteLine("Bien Bien")

        Catch ex As Exception

            Console.WriteLine("Error " + ex.Message)
        End Try

    End Sub
End Class
