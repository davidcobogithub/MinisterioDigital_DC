Imports System.Data.OracleClient
Imports System.Data
Imports CapaDatos
Imports iTextSharp.text.pdf
Imports System.IO
Imports iTextSharp.text
Imports System.Data.Linq
Imports System.Collections.Generic

Partial Class frmPdfViewActas
    Inherits System.Web.UI.Page


    Dim cencr As New ClaseEncripta("1234")
    Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cargarpdf(Request.QueryString("op").ToString(), Request.QueryString("origen").ToString(), Request.QueryString("idDoc").ToString())
        End If
    End Sub

    Private Sub cargarpdf(ByVal op As String, ByVal origen As String, ByVal id As String)
        Dim contenido As Byte() = Nothing

        Try
            Dim param(1) As Object
            param(0) = id
            param(1) = ""

            Dim dt As DataTable
            dt = OracleHelper.ExecuteDataset(cad1, "BarandillaDocumentoPdfGetById", param).Tables(0)

            Dim param1(1) As Object
            param1(0) = id
            param1(1) = ""

            Dim dt1 As DataTable
            dt1 = OracleHelper.ExecuteDataset(cad1, "BarandillaDocPdfGetByIdFull", param1).Tables(0)

            contenido = dt1.Rows(0)("FOriginal")

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


