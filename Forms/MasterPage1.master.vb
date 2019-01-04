
Partial Class MasterPage1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("LoginEmpresa") = "SI" Then
            lblEmpresa.Text = " " & Session("empresa")
        End If

    End Sub
End Class

