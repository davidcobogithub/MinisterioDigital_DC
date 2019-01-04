Imports System.Data.OracleClient
Imports System.Data
Imports CapaDatos

Partial Class frmLiBarandillaEmpresaLi
    Inherits System.Web.UI.Page

    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Dim primeravez As Boolean = True
    Dim acuerdoDigitalSINO As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'acuerdoDigitalSINO = Session("Acuerdo").ToString()

        'MsgBox(acuerdoDigitalSINO)

        'MsgBox("FILTRO POR EMPRESA Y POR SUCURSAL: " + Session("nrocuenta").ToString)

        If Not IsPostBack Then
            cargarBarandillaLIEmpresa(Me.txtBuscarProcedimiento.Text.Trim)
            Dim sqlStringSuc As String = "" & _
              "select to_number(suc.idsucursal) IDSUCURSAL, suc.ubicacion || ' - (' || dv.n_calle || ' ' || CONCAT(' NRO: ', dv.ALTURA) || ' ' || case when dv.piso is null Then '' else CONCAT(' PISO: ', dv.PISO) end  || ' ' || case when dv.depto is null then '' else CONCAT(' DPTO: ', dv.DEPTO) end || ' - ' || trim(dv.n_localidad) || ' - ' || trim(dv.n_provincia) || ')' AS DOMICILIO " & _
              "from doc_laboral.sucursales suc, dom_manager.vt_domicilios_cond dv " & _
              "where(suc.id_vin = dv.id_vin) And dv.id_app = 197 And dv.fecha_baja Is null And suc.nrocuenta = " & Session("nrocuenta").ToString & " And dv.id_tipodom = 11 And suc.fechabaja Is null "
            '
            clsUtiles.CargaComboGen(ddlSucursales, sqlStringSuc, "DOMICILIO", "IDSUCURSAL", "(Todas las sucursales ... )")
            Me.ddlSucursales.SelectedIndex = 0
        Else
            primeravez = False
        End If
    End Sub

    Private Sub cargarBarandillaLIEmpresa(ByVal pFiltro As String)
        'Dim sqlString As String = "select A.ID_ACTA, A.NRO_ACTA, A.FECHA_HORA, E.N_ESTADO_ACTA, DD.DOCUMENTO, A.IDSUCURSAL, SUBSTR(A.NRO_ACTA,9,2) ABREVIATURA " & _
        '                                        "From DOC_LABORAL.T_LI_ACTAS A " & _
        '                                        "LEFT JOIN DOC_LABORAL.T_LI_ACTAS_ESTADOS E " & _
        '                                        "ON A.ID_ESTADO_ACTA = E.ID_ESTADO_ACTA " & _
        '                                        "LEFT JOIN DOC_LABORAL.T_LI_MODELOSACTAS M " & _
        '                                        "ON A.ID_ACTA_MODELO=M.ID_ACTA_MODELO " & _
        '                                        "LEFT JOIN DOC_LABORAL.DEFINICIONDOCUMENTOS DD " & _
        '                                        "ON M.ID_DEFINICIONDOCUMENTO = DD.IDDEFINICIONDOCUMENTO " & _
        '                                        "WHERE (UPPER(E.N_ESTADO_ACTA) LIKE '%GENERADA%') OR DD.IDDEFINICIONDOCUMENTO=2000"

        Dim sqlString As String = "select A.ID_ACTA," & _
                                        "       A.NRO_ACTA," & _
                                        "       A.FECHA_HORA," & _
                                        "       E.N_ESTADO_ACTA," & _
                                        "       DD.DOCUMENTO," & _
                                        "       A.IDSUCURSAL," & _
                                        "       SUBSTR(A.NRO_ACTA, 9, 2) ABREVIATURA" & _
                                        "  From DOC_LABORAL.T_LI_ACTAS A" & _
                                        "  LEFT JOIN DOC_LABORAL.T_LI_ACTAS_ESTADOS E" & _
                                        "    ON A.ID_ESTADO_ACTA = E.ID_ESTADO_ACTA" & _
                                        "  LEFT JOIN DOC_LABORAL.T_LI_MODELOSACTAS M" & _
                                        "    ON A.ID_ACTA_MODELO = M.ID_ACTA_MODELO" & _
                                        "  LEFT JOIN DOC_LABORAL.DEFINICIONDOCUMENTOS DD" & _
                                        "    ON M.ID_DEFINICIONDOCUMENTO = DD.IDDEFINICIONDOCUMENTO" & _
                                        " WHERE A.NROCUENTA = " & Session("nrocuenta").ToString & _
                                        "   AND (A.ID_ESTADO_ACTA IN (4)) "
        'OR DD.IDDEFINICIONDOCUMENTO = 2000)"


        If pFiltro.Trim <> "" Then
            If IsDate(pFiltro.ToString.Trim) Then
                sqlString += " AND TO_CHAR(FECHA_HORA,'DD/MM/RRRR')='" & pFiltro.ToUpper & "'"
            Else
                sqlString += " AND A.NRO_ACTA LIKE '%" & pFiltro.ToUpper & "%'"
            End If
        End If

        'MsgBox(Me.ddlSucursales.SelectedValue)
        'MsgBox(Me.ddlSucursales.SelectedIndex)
        If primeravez = True Then
            primeravez = False
        Else
            Dim miSuc As String = ""
            If Me.ddlSucursales.SelectedIndex <> 0 Then
                If ddlSucursales.SelectedValue.ToString.Trim.Length = 1 Then
                    miSuc = "0" + ddlSucursales.SelectedValue.ToString.Trim()
                Else
                    miSuc = ddlSucursales.SelectedValue.ToString.Trim()
                End If

                sqlString += " AND A.IDSUCURSAL= '" & miSuc & "'"
            End If
        End If

        sqlString += " ORDER BY A.FECHA_HORA DESC "

        Dim t As DataTable
        t = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlString).Tables(0)

        If t.Rows.Count > 0 Then
            Me.grillaBandejaLibroInspeccionEmpresa.DataSource = t
            Me.grillaBandejaLibroInspeccionEmpresa.DataBind()
        Else
            'Response.Write("<script>alert('No existen actuaciones para esos parametros de busqueda');</script>")
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showalert", "alert('No existen actuaciones para esos parametros de busqueda');", True)
        End If



    End Sub

    Protected Sub grillaBandejaLibroInspeccionEmpresa_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grillaBandejaLibroInspeccionEmpresa.PageIndexChanging
        grillaBandejaLibroInspeccionEmpresa.PageIndex = e.NewPageIndex
        cargarBarandillaLIEmpresa(Me.txtBuscarProcedimiento.Text.Trim)
    End Sub

    Protected Sub LinkButtonPdfActa_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim linkB As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(linkB.NamingContainer, GridViewRow)
        '
        'Dim sqlString As String = "select bd.idbarandilladocumento from doc_laboral.barandilladocumentopdf bd where id_acta=" & row.Cells(0).Text.Trim
        Dim sqlString As String = "select bd.idbarandilladocumento from doc_laboral.barandilladocumentopdf bd where id_acta=" & Me.grillaBandejaLibroInspeccionEmpresa.DataKeys(row.RowIndex).Values(0).ToString
        '
        Dim pIdBarandilla As Int32 = OracleHelper.ExecuteScalar(cad1, CommandType.Text, sqlString)
        '
        If pIdBarandilla > 0 Then
            Dim popupScript As String

            'popupScript = String.Empty
            'popupScript = "<script language='JavaScript'>"
            'popupScript += "window.open('frmPdfViewActas.aspx?op=2&origen=&idDoc=" & pIdBarandilla.ToString & "', 'CustomPopUp', "
            'popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
            'popupScript += "</script>"
            'AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "popPdf", popupScript, False)


            popupScript = String.Empty
            popupScript = "<script language='JavaScript'>"
            popupScript += "window.open('frmPdfView.aspx?op=2&origen=&idDoc=" & pIdBarandilla.ToString() & "', 'CustomPopUp', "
            popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
            popupScript += "</script>"
            AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "popPdf", popupScript, False)


        Else
            Response.Write("<script>alert('No se ha generado el archivo PDF de esta actuación');</script>")
        End If
        '
    End Sub


    Protected Sub btnBuscarProcedimiento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarProcedimiento.Click
        Me.hfSuc.Value = ""
        Me.ddlSucursales.SelectedIndex = 0
        cargarBarandillaLIEmpresa(Me.txtBuscarProcedimiento.Text.Trim)
    End Sub

    Protected Sub btnTodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTodos.Click
        Me.txtBuscarProcedimiento.Text = ""
        cargarBarandillaLIEmpresa(Me.txtBuscarProcedimiento.Text.Trim)
    End Sub

    Protected Sub ddlSucursales_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSucursales.SelectedIndexChanged
        If primeravez = False Then
            Me.txtBuscarProcedimiento.Text = ""
            Dim strSuc As String = ""
            If Me.ddlSucursales.SelectedIndex = 0 Then
                strSuc = ""
                hfSuc.Value = strSuc
                cargarBarandillaLIEmpresa("")
            Else
                If Me.ddlSucursales.SelectedValue < 100 Then
                    strSuc = Left("000" + Str(Me.ddlSucursales.SelectedIndex).Trim(), 2)
                    'MsgBox(strSuc)
                    hfSuc.Value = strSuc
                Else
                    strSuc = Me.ddlSucursales.SelectedValue.ToString.Trim
                    hfSuc.Value = Me.ddlSucursales.SelectedValue.ToString.Trim
                End If
                cargarBarandillaLIEmpresa("")
            End If

            'Else
            '    Me.ddlSucursales.SelectedIndex = 0
        End If
    End Sub

End Class






