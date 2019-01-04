Imports CapaDatos
Imports System.Data
Imports System.Data.OracleClient
Imports Microsoft.VisualBasic

Public Class ClsBarandillaManualDetalleOracleProvider

    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Public Sub BarandillaDocumentoManualDetalle_Insert(ByVal objBM As ClsBarandillaManualDetalle, ByVal cnn As OracleConnection, ByVal trx As OracleTransaction)
        Dim cmd As New OracleCommand("Doc_Laboral.pr_barandocmanualDet_insert", cnn, trx)
        cmd.CommandType = CommandType.StoredProcedure
        Try
            cmd.Parameters.Add("pidbarandilladocmanual", OracleType.Number).Value = objBM.idbarandilladocmanual
            cmd.Parameters.Add("pid_empleado", OracleType.Number).Value = objBM.id_empleado
            cmd.Parameters.Add("pnrolibro", OracleType.Number).Value = objBM.nrolibro
            cmd.Parameters.Add("pnrofolio", OracleType.Number).Value = objBM.nrofolio
            cmd.Parameters.Add("ptotalneto", OracleType.Number).Value = objBM.totalneto
            cmd.Parameters.Add("pobs", OracleType.VarChar).Value = objBM.obs
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw
        Finally
            cmd.Dispose()
        End Try
    End Sub
    Public Sub BarandillaDocumentoManualDetalle_Update(ByVal objBM As ClsBarandillaManualDetalle, ByVal cnn As OracleConnection, ByVal trx As OracleTransaction)

        Dim cmd As New OracleCommand("Doc_Laboral.pr_barandocmanualDet_update", cnn, trx)
        cmd.CommandType = CommandType.StoredProcedure

        Try
            cmd.Parameters.Add("pidbarandilladocmanual", OracleType.Number).Value = objBM.idbarandilladocmanual
            cmd.Parameters.Add("pid_empleado", OracleType.Number).Value = objBM.id_empleado
            cmd.Parameters.Add("pnrolibro", OracleType.Number).Value = objBM.nrolibro
            cmd.Parameters.Add("pnrofolio", OracleType.Number).Value = objBM.nrofolio
            cmd.Parameters.Add("ptotalneto", OracleType.Number).Value = objBM.totalneto
            cmd.Parameters.Add("pobs", OracleType.VarChar).Value = objBM.obs
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw
        Finally
            cmd.Dispose()
        End Try
    End Sub

End Class
