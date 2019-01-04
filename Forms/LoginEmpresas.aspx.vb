Imports System.Data.OracleClient
Imports System.Data
Imports System.Web.UI.HtmlControls.HtmlGenericControl
Imports CapaDatos


Partial Class LoginEmpresas
    Inherits System.Web.UI.Page

    Dim cencr As New ClaseEncripta("1234")
    Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not IsPostBack) Then

            Session.Clear()

            Session("Logueada") = "NO"
            Session("Autorizado") = "NO"
            Session("Existente") = "NO"

            txtCuit.Text = ""
            txtClave.Text = ""

            lblMensajeUsuarioNoEncontrado.Visible = False
            LinkPrimerIngreso.PostBackUrl = String.Empty
            LinkFiscalizacionPCP.PostBackUrl = String.Empty

        End If

    End Sub


    Private Sub validarAccesoEmpresa(ByVal pUser As String, ByVal pPass As String)

        Dim b As Boolean = False
        Dim md5 As New MD5Encode(pPass)

        Dim passMD5 As String = md5.getMD5Hash()
        Dim obj(2) As Object
        obj(0) = pUser
        obj(1) = passMD5
        obj(2) = ""

        Dim dt As New DataTable
        dt = OracleHelper.ExecuteDataset(cad1, "empresasGetByCUITPass", obj).Tables(0)
        If dt.Rows.Count > 0 Then

            If dt.Rows(0)("idestado").ToString() = "3" Or dt.Rows(0)("idestado").ToString() = "12" Then
                Session("nrocuenta") = dt.Rows(0)("nrocuenta").ToString().Trim()
                Session("cuit") = dt.Rows(0)("cuit").ToString().Trim()
                Session("Empresa") = dt.Rows(0)("razon").ToString().ToUpper().Trim().Substring(0, Math.Min((dt.Rows(0)("razon").ToString().ToUpper().Trim()).ToString.Length, 25))
                Session("emptipo") = Trim(dt.Rows(0)("idemptipo").ToString())
                Session("Autorizado") = "NO"
                Session("Existente") = "SI"
                Session("Logueada") = "NO"
                Session("LoginEmpresa") = "SI"
                Session("LoginMinisterio") = "NO"
                Session("Autorizado") = "SI"
                Session("Logueada") = "SI"
                Session("Acuerdo") = IIf(CInt(dt.Rows(0)("acuerdo").ToString()) = 1, "SI", "NO")
                Response.Redirect("frmPantallaInicio1.aspx")
            Else
                Session("Autorizado") = "NO"
                Session("Existente") = "NO"
                Session("Logueada") = "NO"
                Session("LoginEmpresa") = "SI"

                txtCuit.Text = ""
                txtClave.Text = ""

                lblMensajeUsuarioNoEncontrado.Visible = True
            End If
        Else
            Session("Autorizado") = "NO"
            Session("Existente") = "NO"
            Session("Logueada") = "NO"
            Session("LoginEmpresa") = "SI"

            txtCuit.Text = ""
            txtClave.Text = ""

            lblMensajeUsuarioNoEncontrado.Visible = True
        End If


    End Sub


    Private Function ValidarEstado() As Boolean

        lblMensajeUsuarioNoEncontrado.Visible = False

        Dim cn As New OracleConnection(cad1)
        cn.Open()

        If (cn.State = ConnectionState.Open) Then

            Dim sqlSelect As New StringBuilder()

            sqlSelect.Append("Select idestado From Empresas Where ")
            sqlSelect.Append("usuario_internet = '" & txtCuit.Text.Trim() + "' And ")
            sqlSelect.Append("clave_internet = '" & txtClave.Text.Trim() + "'")

            Dim cmd As New OracleCommand(sqlSelect.ToString().Trim(), cn)
            Dim da As New OracleDataAdapter(cmd)
            Dim ds As New Data.DataSet()

            da.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then
                If ds.Tables(0).Rows(0)("idestado").ToString() = "3" Or ds.Tables(0).Rows(0)("idestado").ToString() = "12" Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

            cmd.Dispose()
            da.Dispose()
            ds.Dispose()

        End If

        cn.Close()
        cn.Dispose()

    End Function


    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        lblMensajeUsuarioNoEncontrado.Visible = False

        If (IsNumeric(txtCuit.Text.Trim()) = False) Then

            lblMensajeUsuarioNoEncontrado.Visible = True
            lblMensajeUsuarioNoEncontrado.Text = "Cuit debe ser un valor numérico"
            Return

        End If

        validarAccesoEmpresa(txtCuit.Text, txtClave.Text)
    End Sub

    Protected Sub linkPrimerIngreso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkPrimerIngreso.Click

        Session("Logueada") = "NO"
        Session("Existente") = "NO"
        Session("LoginEmpresa") = "SI"
        Session("LoginMinisterio") = "NO"
        Response.Redirect("frmAyudaEmpadronamiento.aspx")

    End Sub

    Protected Sub LinkFiscalizacionPCP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkFiscalizacionPCP.Click
        Response.Redirect("frmPCP-Inicio.aspx")
    End Sub
End Class
