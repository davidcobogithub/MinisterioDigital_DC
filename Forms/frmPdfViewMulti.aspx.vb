Imports System.Data.OracleClient
Imports System.Data
Imports CapaDatos

Partial Class frmPdfViewMulti
    Inherits System.Web.UI.Page
    Dim cencr As New ClaseEncripta("1234")
    Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cargarpdf(Request.QueryString("adj").ToString())
        End If
    End Sub

    Private Sub cargarpdf(ByVal p_idadj As String)
        Dim contenido As Byte() = Nothing

        Try

            Dim dt As New DataTable
            Dim param(1) As Object
            param(0) = p_idadj
            param(1) = ""
            dt = OracleHelper.ExecuteDataset(cad1, "multinotasadjLmin_selectID", param).Tables(0)
            contenido = dt.Rows(0)("adjunto")

        Catch ex As Exception
            Throw ex
        End Try

        'Response.ContentType = "application/pdf"
        'Response.BinaryWrite(contenido)
        'Response.End()
        '*****************************************
        'Response.Buffer = False 'transmitfile self buffers
        'Response.Clear()
        'Response.ClearContent()
        'Response.ClearHeaders()
        'Response.ContentType = "application/pdf"
        ''Response.AddHeader("Content-Disposition", "attachment; filename=myfile.pdf")
        ''Response.TransmitFile(contenido) '//transmitfile keeps entire file from loading into memory
        ''Response.OutputStream.Write(contenido, 0, contenido.Length)
        'Response.Charset = "UTF-8"
        'Response.ContentEncoding = System.Text.Encoding.UTF8
        'Response.BinaryWrite(contenido)
        'Response.End()
        '*****************************************

        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "inline;filename=report.pdf")
        Response.BinaryWrite(contenido)
        Response.End()

    End Sub
End Class

