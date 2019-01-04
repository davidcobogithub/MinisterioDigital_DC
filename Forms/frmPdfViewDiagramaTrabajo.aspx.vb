Imports System.Data

Partial Class frmPdfViewDiagramaTrabajo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cargarDiagramaTrabajo(Request.QueryString("idEsq").ToString())
        End If

    End Sub

    Private Sub cargarDiagramaTrabajo(ByVal pIdEsq As Integer)

        Dim clsesquemashorariodetOracleProvider As New clsEsquemasHorarioDetalleOracleProvider
        Dim dt As DataTable
        dt = clsesquemashorariodetOracleProvider.EsquemaHorarioDetalleGetByIdEsquemaDataTable(pIdEsq)

        If dt.Rows.Count > 0 Then
            Dim contenido As Byte() = dt.Rows(0)("diagramatrabajo")

            Response.Clear()
            Response.ClearHeaders()
            Response.ClearContent()
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "inline;filename=report.pdf")
            Response.BinaryWrite(contenido)
            Response.End()
        End If
    End Sub

End Class
