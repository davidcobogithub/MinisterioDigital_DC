Imports CapaDatos
Imports System.Data

Imports System.Data.OracleClient
Imports Microsoft.VisualBasic

Public Class clsSindicato
    Public id_sindicato As Integer
    Public n_sindicato As String
    Public sigla As String

    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())


    Public Function mostrarSindicatos() As DataTable
        Dim param(2) As Object
        Try
            param(0) = IIf(id_sindicato = 0, DBNull.Value, id_sindicato)
            param(1) = IIf(IsNothing(n_sindicato), "", n_sindicato)
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.p_sindicatos_sel", param).Tables(0)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
