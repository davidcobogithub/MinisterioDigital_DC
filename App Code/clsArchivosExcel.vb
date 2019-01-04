Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OracleClient
Imports System.IO


Public Class clsArchivosExcel

    'Dim cencr As New ClaseEncripta("1234")
    'Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    'Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
    '    For Each key As String In context.Request.Files
    '        Dim postedFile As HttpPostedFile = context.Request.Files(key)
    '        Dim folderPath As String = context.Server.MapPath("~/Uploads/")
    '        If Not Directory.Exists(folderPath) Then
    '            Directory.CreateDirectory(folderPath)
    '        End If
    '        postedFile.SaveAs(folderPath + postedFile.FileName)
    '    Next

    '    context.Response.StatusCode = 200
    '    context.Response.ContentType = "text/plain"
    '    context.Response.Write("Success")
    'End Sub

    'Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
    '    Get
    '        Return False
    '    End Get
    'End Property



End Class
