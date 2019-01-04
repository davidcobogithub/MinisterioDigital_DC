Imports System.Data.OracleClient
Imports System.Data
Imports CapaDatos

Partial Class frmLiBarandillaAOf
    Inherits System.Web.UI.Page

    Dim cencr As New ClaseEncripta("1234")
    'Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
    Private cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cargarBarandillaAof("")
            panelDetalle.Visible = False
        End If
    End Sub

    Private Sub cargarBarandillaAof(ByVal filtro As String)
        Dim sqlString As String = "select a.id_actas_temp, a.cuit, a.razon_social, dd.documento, a.nrodocumentomanual, a.fecha, a.plazo, i.n_tipo_intimacion" & _
                                    "  from doc_laboral.t_li_actas_temp a" & _
                                    "  left join doc_laboral.definiciondocumentos dd" & _
                                    "    on a.iddefiniciondocumento = dd.iddefiniciondocumento" & _
                                    "    left join doc_laboral.t_li_tipos_intimaciones i" & _
                                    "    on a.Id_Tipo_Intimacion = i.id_tipo_intimacion "

        If filtro <> "" Then
            If IsNumeric(Me.txtBuscarProcedimiento.Text.Trim) Then
                If Me.txtBuscarProcedimiento.Text.Trim.Length = 11 Then                  
                    sqlString += " where a.cuit = '" & Me.txtBuscarProcedimiento.Text.Trim & "'"
                Else
                    sqlString += " where a.nrodocumentomanual = " & Me.txtBuscarProcedimiento.Text.Trim & ""
                End If
            Else
                sqlString += " where upper(a.razon_social) like '%" & Me.txtBuscarProcedimiento.Text.Trim.ToUpper & "%'"
            End If
        End If

        sqlString += "  order by a.fecha DESC "

        Dim t As DataTable
        t = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlString).Tables(0)
        Me.grillaBandejaAltasOficio.DataSource = t
        Me.grillaBandejaAltasOficio.DataBind()

        If t.Rows.Count = 0 Then
            Response.Write("<script>alert('No hay resultados con esos parametros de busqueda ... ');</script>")
        End If


    End Sub

    Protected Sub grillaBandejaAltasOficio_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grillaBandejaAltasOficio.PageIndexChanging
        grillaBandejaAltasOficio.PageIndex = e.NewPageIndex
        cargarBarandillaAof(Me.txtBuscarProcedimiento.Text.Trim)
        panelDetalle.Visible = False
    End Sub

    Protected Sub LinkButtonDetalleActaManual_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(lb.NamingContainer, GridViewRow)
        '
        Dim strSql As String = "select NVL(at.cuit,' ') CUIT," & _
                                "       NVL(at.razon_social,' ') RAZON_SOCIAL," & _
                                "       NVL(at.domicilio,' ') DOMICILIO," & _
                                "       NVL(ac.N_ACTIVIDAD,' ') N_ACTIVIDAD," & _
                                "       NVL(dd.documento,' ') DOCUMENTO," & _
                                "       NVL(to_char(at.nrodocumentomanual,'99999999'),' ') NRODOCUMENTOMANUAL," & _
                                "       NVL(trim(u.apellido) || ' ' || trim(u.nombre),' ') APEYNOM," & _
                                "       NVL(to_char(FECHA,'DD/MM/RRRR'),' ') FECHA," & _
                                "       NVL(to_char(PLAZO,'9999'),' ') PLAZO," & _
                                "       NVL(ti.n_tipo_intimacion,' ') N_TIPO_INTIMACION" & _
                                "  from doc_laboral.t_li_actas_temp at" & _
                                "  left join T_COMUNES.VT_ACTIVIDADES ac" & _
                                "    on at.id_actividad = ac.ID_ACTIVIDAD" & _
                                "  left join doc_laboral.definiciondocumentos dd" & _
                                "    on at.iddefiniciondocumento = dd.iddefiniciondocumento" & _
                                "  left join doc_laboral.usuarios u" & _
                                "    on at.idusuario = u.idusuario" & _
                                "  left join doc_laboral.t_li_tipos_intimaciones ti" & _
                                "    on at.Id_Tipo_Intimacion = ti.id_tipo_intimacion" & _
                                " where at.id_actas_temp = " + row.Cells(0).Text.Trim

        Dim conexion As New OracleConnection(cad1)
        Dim comando As New OracleCommand(strSql, conexion)
        Dim dt As New DataTable
        '
        comando.CommandType = CommandType.Text
        conexion.Open()
        dt.Load(comando.ExecuteReader)
        conexion.Close()
        '
        If dt.Rows.Count > 0 Then
            panelDetalle.Visible = True
            Me.txtCuit.Text = dt.Rows(0)("CUIT").ToString.Substring(0, 2) + "-" + dt.Rows(0)("CUIT").ToString.Substring(2, 8) + "-" + dt.Rows(0)("CUIT").ToString.Substring(10, 1)
            Me.txtRazon.Text = dt.Rows(0)("razon_social")
            Me.txtDomicilio.Text = dt.Rows(0)("domicilio")
            Me.txtActividad.Text = dt.Rows(0)("N_ACTIVIDAD")
            Me.txtTipoDocumento.Text = dt.Rows(0)("DOCUMENTO")
            Me.txtNroActaManual.Text = dt.Rows(0)("NRODOCUMENTOMANUAL")
            Me.txtAgente.Text = dt.Rows(0)("APEYNOM")
            Me.txtFechaActuacion.Text = dt.Rows(0)("FECHA")
            Me.txtPlazo.Text = dt.Rows(0)("PLAZO")
            Me.txtTipoIntimacion.Text = dt.Rows(0)("N_TIPO_INTIMACION")
        Else
            panelDetalle.Visible = False
        End If

        dt.Dispose()
        '
    End Sub

    Protected Sub btnBuscarProcedimiento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarProcedimiento.Click
        cargarBarandillaAof(Me.txtBuscarProcedimiento.Text.Trim)
        'LIMPIA GRILLA DETALLE DE ACTAS
        panelDetalle.Visible = False
    End Sub

    Protected Sub btnTodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTodos.Click
        Me.txtBuscarProcedimiento.Text = ""
        cargarBarandillaAof("")
        panelDetalle.Visible = False

    End Sub

End Class
