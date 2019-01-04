Imports System.Data.OracleClient
Imports System.Data
Imports CapaDatos
Imports iTextSharp.text.pdf
Imports System.IO
Imports iTextSharp.text
Imports System.Data.Linq
Imports System.Collections.Generic

Partial Class frmPdfViewDoc
    Inherits System.Web.UI.Page

    Dim cencr As New ClaseEncripta("1234")
    Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim parenc As String
            parenc = Request.QueryString("i")
            Dim vecpar As New ArrayList
            vecpar = clsUtiles.DecodificaParametros(parenc, "-")

            cargarpdf(Request.QueryString("t").ToString(), Request.QueryString("c").ToString(), vecpar(0).ToString())
            'cargarpdf(Request.QueryString("t").ToString(), Request.QueryString("c").ToString(), Request.QueryString("i").ToString())
        End If
    End Sub

    Private Sub cargarpdf(ByVal pTabla As String, ByVal pCampo As String, ByVal pId As String)
        Dim contenido As Byte() = Nothing

        Dim sqlString As String = "SELECT " & pCampo & " FROM DOC_LABORAL." & pTabla & " WHERE ROWID='" & pId & "'"

        Try

            Dim dt As New DataTable
            dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlString).Tables(0)

            contenido = dt.Rows(0)(pCampo)

        Catch ex As Exception
            Throw ex
        End Try

        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "inline;filename=report.pdf")
        Response.BinaryWrite(contenido)
        Response.End()
    End Sub

End Class
