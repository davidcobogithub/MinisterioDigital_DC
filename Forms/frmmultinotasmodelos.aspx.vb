Imports System.Data
Imports System.Data.OracleClient
Imports CapaDatos

Partial Class frmmultinotasmodelos
    Inherits System.Web.UI.Page
    Dim cencr As New ClaseEncripta("1234")
    Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Enum acciones
        Insertar
        Modificar
        Eliminar
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            obtenerConceptosPadres()
            obtenermodelos_all()
        End If
    End Sub

    Private Sub obtenerConceptosPadres()
        Dim dt As New DataTable
        Dim obj(1) As Object
        obj(0) = 1
        obj(1) = ""
        'dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.p_conceptos_tasas_sel", obj).Tables(0)
        'If dt.Rows.Count > 0 Then
        '    Me.cboconceptos.DataSource = dt
        '    Me.cboconceptos.DataTextField = "N_CONCEPTO"
        '    Me.cboconceptos.DataValueField = "ID_CONCEPTO"
        '    cboconceptos.DataBind()
        'End If
    End Sub

    Private Sub obtenermodelos_all()
        Dim dt As New DataTable
        Dim obj(0) As Object
        obj(0) = ""
        'dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.multinotasmodelos_selectall", obj).Tables(0)
        'If dt.Rows.Count > 0 Then
        '    GrillaModelos.DataSource = dt
        '    GrillaModelos.DataBind()
        'Else
        '    GrillaModelos.DataSource = Nothing
        '    GrillaModelos.DataBind()
        'End If

    End Sub

    Private Sub obtenermodelo(ByVal p_mulmod As Integer)
        Dim dt As New DataTable
        Dim obj(1) As Object
        obj(0) = p_mulmod
        obj(1) = ""
        'dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.multinotasmodelos_select", obj).Tables(0)
        'If dt.Rows.Count > 0 Then
        '    txtidmul.Text = dt.Rows(0)("id_mulmod")
        '    txtnombre.Text = dt.Rows(0)("nombre")
        '    txtBox1.Text = IIf(IsDBNull(dt.Rows(0)("modelo")), "", dt.Rows(0)("modelo"))
        '    cboconceptos.SelectedValue = dt.Rows(0)("concepto")
        'End If
    End Sub

    Protected Sub btnmodeloeditar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Boxaccion.Value = acciones.Modificar
        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim idMul As Integer
        idMul = Convert.ToInt32(row.Cells(0).Text)
        obtenermodelo(idMul)
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub btnmodeloeliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
       
    End Sub

    Protected Sub btnnuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnuevo.Click
        Boxaccion.Value = acciones.Insertar
        Dim nromul As Integer
        'nromul = OracleHelper.ExecuteScalar(cad1, CommandType.Text, "select doc_laboral.seq_multinotasmodelos.nextval  from dual")
        'Boxidmul.Value = nromul
        'txtidmul.Text = nromul
        txtBox1.Text = ""
        txtnombre.Text = ""
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub btnguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguardar.Click
        rqv1.Enabled = True
        If Boxaccion.Value = acciones.Modificar Then
            Modificar(CInt(txtidmul.Text), txtnombre.Text, txtBox1.Text, cboconceptos.SelectedValue)
        End If
        If Boxaccion.Value = acciones.Insertar Then
            Insertar(Boxidmul.Value, txtnombre.Text, txtBox1.Text, cboconceptos.SelectedValue)
        End If
        obtenermodelos_all()
    End Sub

    Protected Sub Modificar(ByVal p_mulmod As Integer, ByVal p_nombre As String, ByVal p_modelo As String, ByVal p_concepto As Integer)
        ' OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.multinotasmodelos_update", p_mulmod, p_nombre, p_modelo, p_concepto, Boxiddef.Value)

        Dim conexion As New OracleConnection
        Try
            conexion.ConnectionString = cad1
            conexion.Open()
            Dim comando As New OracleCommand()
            comando.Connection = conexion
            comando.CommandType = CommandType.StoredProcedure
            comando.CommandText = "doc_laboral.multinotasmodelos_update"

            Dim parIdMul As OracleParameter = New OracleParameter("p_mulmod", System.Data.OracleClient.OracleType.Int32)
            parIdMul.Direction = ParameterDirection.InputOutput
            parIdMul.Value = p_mulmod

            Dim parNombre As OracleParameter = New OracleParameter("p_nombre", System.Data.OracleClient.OracleType.NVarChar, 200)
            parNombre.Direction = ParameterDirection.Input
            parNombre.Value = p_nombre

            Dim parModelo As System.Data.OracleClient.OracleParameter
            parModelo = New System.Data.OracleClient.OracleParameter()
            parModelo.ParameterName = "p_modelo"
            parModelo.OracleType = OracleType.Clob
            parModelo.Size = p_modelo.Length
            parModelo.Value = p_modelo
            parModelo.Direction = ParameterDirection.Input

            Dim parConcepto As OracleParameter = New OracleParameter("p_concepto", System.Data.OracleClient.OracleType.Int32)
            parConcepto.Direction = ParameterDirection.Input
            parConcepto.Value = p_concepto

            Dim parIdDef As OracleParameter = New OracleParameter("p_iddef", System.Data.OracleClient.OracleType.Int32)
            parIdDef.Direction = ParameterDirection.Input
            parIdDef.Value = CInt(Boxiddef.Value)

            comando.Parameters.Add(parIdMul)
            comando.Parameters.Add(parNombre)
            comando.Parameters.Add(parModelo)
            comando.Parameters.Add(parConcepto)
            comando.Parameters.Add(parIdDef)

            comando.ExecuteNonQuery()

        Catch ex As Exception

        Finally
            conexion.Close()
        End Try

    End Sub

    Protected Sub Insertar(ByVal p_mulmod As Integer, ByVal p_nombre As String, ByVal p_modelo As String, ByVal p_concepto As Integer)


        Dim conexion As New OracleConnection
        Try
            conexion.ConnectionString = cad1
            conexion.Open()
            Dim comando As New OracleCommand()
            comando.Connection = conexion
            comando.CommandType = CommandType.StoredProcedure
            comando.CommandText = "doc_laboral.multinotasmodelos_INSERTS"

            Dim parIdMul As OracleParameter = New OracleParameter("p_idmul", System.Data.OracleClient.OracleType.Int32)
            parIdMul.Direction = ParameterDirection.InputOutput
            parIdMul.Value = p_mulmod

            Dim parNombre As OracleParameter = New OracleParameter("p_nombre", System.Data.OracleClient.OracleType.NVarChar, 200)
            parNombre.Direction = ParameterDirection.Input
            parNombre.Value = p_nombre

            Dim parModelo As System.Data.OracleClient.OracleParameter
            parModelo = New System.Data.OracleClient.OracleParameter()
            parModelo.ParameterName = "p_modelo"
            parModelo.OracleType = OracleType.Clob
            parModelo.Size = p_modelo.Length
            parModelo.Value = p_modelo
            parModelo.Direction = ParameterDirection.Input

            Dim parConcepto As OracleParameter = New OracleParameter("p_concepto", System.Data.OracleClient.OracleType.Int32)
            parConcepto.Direction = ParameterDirection.Input
            parConcepto.Value = p_concepto

            Dim parIdDef As OracleParameter = New OracleParameter("p_iddef", System.Data.OracleClient.OracleType.Int32)
            parIdDef.Direction = ParameterDirection.Input
            parIdDef.Value = CInt(Boxiddef.Value)

            comando.Parameters.Add(parIdMul)
            comando.Parameters.Add(parNombre)
            comando.Parameters.Add(parModelo)
            comando.Parameters.Add(parConcepto)
            comando.Parameters.Add(parIdDef)

            comando.ExecuteNonQuery()

        Catch ex As Exception

        Finally
            conexion.Close()
        End Try


    End Sub
    Protected Sub Eliminar(ByVal p_mulmod As Integer)
        OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.multinotasmodelos_delete", p_mulmod)
    End Sub

    Protected Sub OnConfirm(ByVal sender As Object, ByVal e As EventArgs)
        Boxaccion.Value = acciones.Eliminar
        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim idMul As Integer
        idMul = Convert.ToInt32(row.Cells(0).Text)
        Boxidmul.Value = idMul

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Eliminar(Boxidmul.Value)
            obtenermodelos_all()
        Else

        End If
    End Sub

    Protected Sub cboconceptos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboconceptos.Load
        If Not IsPostBack Then
            Dim dt As New DataTable
            Dim obj(1) As Object
            obj(0) = cboconceptos.SelectedValue
            obj(1) = ""
            'dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.p_conceptos_tasas_sel", obj).Tables(0)
            'If dt.Rows.Count > 0 Then
            '    Boxiddef.Value = dt.Rows(0)("iddef")
            'End If
        End If
    End Sub

    Protected Sub cboconceptos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboconceptos.SelectedIndexChanged
        Dim dt As New DataTable
        Dim obj(1) As Object
        obj(0) = cboconceptos.SelectedValue
        obj(1) = ""
        'dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.p_conceptos_tasas_sel", obj).Tables(0)
        'If dt.Rows.Count > 0 Then
        '    Boxiddef.Value = dt.Rows(0)("iddef")
        'End If
    End Sub

    Protected Sub GrillaModelos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrillaModelos.PageIndexChanging
        GrillaModelos.PageIndex = e.NewPageIndex
        obtenermodelos_all()
    End Sub
End Class
