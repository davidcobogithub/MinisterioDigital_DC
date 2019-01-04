Imports System.Data.OracleClient
Imports System.Data
Imports CapaDatos

Partial Class frmLiBarandillaLi
    Inherits System.Web.UI.Page

    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
    'Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            clsUtiles.CargaComboGen(ddlAgentes, "select u.idusuario, UPPER(trim(u.apellido) || ' ' || trim(u.nombre)) APEYNOM from doc_laboral.t_usuariosxfuncion uf left join doc_laboral.usuarios u on uf.id_usuario = u.idusuario WHERE uf.id_funcion=2 order by UPPER(trim(u.apellido) || ' ' || trim(u.nombre))", "APEYNOM", "IDUSUARIO", "(TODOS LOS AGENTES)")

            'clsUtiles.CargaComboGen(ddlEstados, "select * from doc_laboral.t_li_actas_estados order by n_estado_acta", "N_ESTADO_ACTA", "ID_ESTADO_ACTA", "(TODOS LOS ESTADOS)")
            'clsUtiles.CargaComboGen(ddlareas, "SELECT ID_AREA, UPPER(TRIM(N_AREA)) || '  -  (' || N_AREA_ABREVIATURA || ')' AREA FROM DOC_LABORAL.T_AREAS", "AREA", "ID_AREA", "(TODAS LAS AREAS)")
            '
            cargarBarandillaLI(Me.txtBuscarProcedimiento.Text.Trim)
            CargarComboOpciones(cboOpcion.SelectedValue)
        End If
    End Sub


    'FILTRA GRILLA POR NUMERO DE PROCEDIMIENTO/CARPETA
    Protected Sub btnBuscarProcedimiento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarProcedimiento.Click
        cargarBarandillaLI(Me.txtBuscarProcedimiento.Text.Trim)

        'LIMPIA GRILLA DETALLE DE ACTAS
        grillaCarpetaDetalle.DataSource = Nothing
        grillaCarpetaDetalle.DataBind()
        '
        Me.lblTituloGrillaActasDetalle.Text = ""

    End Sub

    'CARGA GRILLA DE PROCEDIMIENTOS/CARPETAS
    Private Sub cargarBarandillaLI(ByVal filtro As String)

        'MsgBox(Me.ddlAgentes.SelectedIndex)
        'MsgBox(Me.ddlAgentes.SelectedValue)

        'Dim sqlString As String = "select distinct c.id_carpeta ID_CARPETA, c.Nro_Carpeta NRO_CARPETA, a.nrocuenta NROCUENTA, e.cuit, upper(pj.razon_social) RAZON_SOCIAL, (select count(*) from (doc_laboral.t_li_actas) where doc_laboral.t_li_actas.id_carpeta = c.id_carpeta) MOVIMIENTOS, (select min(fecha_hora)from (doc_laboral.t_li_actas) where doc_laboral.t_li_actas.id_carpeta = c.id_carpeta) PM, (select max(fecha_hora) from (doc_laboral.t_li_actas) where doc_laboral.t_li_actas.id_carpeta = c.id_carpeta) UM, c.idusuario, TRIM(UPPER(u.apellido)) || ' ' || TRIM(UPPER(u.nombre)) AGENTE from doc_laboral.t_li_carpetas c left join doc_laboral.t_li_actas a on c.id_carpeta = a.id_carpeta left join doc_laboral.empresas e on a.nrocuenta = e.nrocuenta left join T_COMUNES.VT_PERS_JURIDICAS_COMPLETA pj on e.cuit = pj.cuit left join doc_laboral.usuarios u on c.idusuario = u.idusuario"

        Dim sqlString As String = "select * from (select distinct c.id_carpeta ID_CARPETA, " & _
                                    "                c.Nro_Carpeta NRO_CARPETA, " & _
                                    "                a.nrocuenta NROCUENTA, " & _
                                    "                e.cuit, " & _
                                    "                pj.razon_social RAZON_SOCIAL, " & _
                                    "                (select count(*) " & _
                                    "                   from (doc_laboral.t_li_actas) " & _
                                    "                  where doc_laboral.t_li_actas.id_carpeta = c.id_carpeta) MOVIMIENTOS, " & _
                                    "                (select min(fecha_hora) " & _
                                    "                   from (doc_laboral.t_li_actas) " & _
                                    "                  where doc_laboral.t_li_actas.id_carpeta = c.id_carpeta) PM, " & _
                                    "                (select max(fecha_hora)" & _
                                    "                   from (doc_laboral.t_li_actas) " & _
                                    "                  where doc_laboral.t_li_actas.id_carpeta = c.id_carpeta) UM, " & _
                                    "                c.idusuario, " & _
                                    "                TRIM(UPPER(u.apellido)) || ' ' || TRIM(UPPER(u.nombre)) AGENTE " & _
                                    "  from doc_laboral.t_li_carpetas c " & _
                                    "  left join doc_laboral.t_li_actas a " & _
                                    "    on c.id_carpeta = a.id_carpeta " & _
                                    "  left join doc_laboral.empresas e " & _
                                    "    on a.nrocuenta = e.nrocuenta " & _
                                    "  left join T_COMUNES.VT_PERS_JURIDICAS_COMPLETA pj " & _
                                    "    on e.cuit = pj.cuit " & _
                                    "  left join doc_laboral.usuarios u " & _
                                    "    on c.idusuario = u.idusuario) " & _
                                    " where 1 = 1 "

        If filtro = "USU" Then
            filtro = " and movimientos=0 and idusuario = " & Session("UsuarioMinId").ToString & " order by NRO_CARPETA ASC "
            sqlString = sqlString + filtro
            sqlString = "SELECT * FROM ( " + sqlString + ") WHERE ROWNUM = 1"
        Else
            sqlString += " AND movimientos > 0 "
            If Me.txtBuscarProcedimiento.Text.Trim <> "" Then

                If IsNumeric(Me.txtBuscarProcedimiento.Text) And Me.txtBuscarProcedimiento.Text.Trim.Length = 11 Then
                    filtro = " and cuit = '" + Me.txtBuscarProcedimiento.Text.Trim & "'"
                Else
                    If IsNumeric(Me.txtBuscarProcedimiento.Text) Then
                        filtro = " and nro_carpeta = " + Me.txtBuscarProcedimiento.Text.Trim
                    Else
                        filtro = " and razon_social LIKE '" & Me.txtBuscarProcedimiento.Text.Trim.ToUpper & "%'"
                    End If
                End If
                sqlString += filtro
            Else
                sqlString = sqlString
            End If

            If Me.ddlAgentes.SelectedIndex <> 0 Then
                sqlString += " and idusuario = " + Me.ddlAgentes.SelectedValue.ToString
            End If

            sqlString += " order by NRO_CARPETA desc"

        End If

        Dim t As DataTable
        t = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlString).Tables(0)

        Me.grillaBandejaLibroInspeccion.DataSource = t
        Me.grillaBandejaLibroInspeccion.DataBind()

    End Sub

    'PAGINACION GRILLA PROCEDIMIENTOS/CARPETAS
    Protected Sub grillaBandejaLibroInspeccion_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grillaBandejaLibroInspeccion.PageIndexChanging
        grillaBandejaLibroInspeccion.PageIndex = e.NewPageIndex
        cargarBarandillaLI(Me.txtBuscarProcedimiento.Text.Trim)
    End Sub


    '
    ' ABRE POPUP PARA DAR DE ALTA UNA NUEVA ACTUACION
    '
    Protected Sub LinkButtonActuaciones_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'LIMPIA GRILLA DETALLE DE ACTAS
        grillaCarpetaDetalle.DataSource = Nothing
        grillaCarpetaDetalle.DataBind()
        '
        Me.lblTituloGrillaActasDetalle.Text = ""
        '-----------

        'Limpia mensajes de validacion
        Me.lblErrorComboDelegacion.Text = ""
        Me.lblErrorComboSucursal.Text = ""
        Me.lblErrorComboTipoDocumento.Text = ""


        Me.lblError.Text = ""
        Me.txtCuitModal.Text = ""
        'Me.ddlAgentes.Items.Clear()
        'Me.ddlareas.Items.Clear()
        'Me.ddlEstados.Items.Clear()
        Me.lblDatosEmpresa.Text = ""
        Me.NoEncuentraCuit.Visible = False
        Me.CombosArea.Visible = False
        Me.btnGuardar.Enabled = False
        Me.buscarCuitArea.Visible = True
        Me.btnGuardar.Text = "Nueva Actuación"
        '
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(lb.NamingContainer, GridViewRow)
        '
        Dim miClsUsuarios As New clsUsuarios
        Dim dtu As New DataTable
        'dtu = miClsUsuarios.usuariosGetByUsuario(Session("UsuarioMinId").ToString)
        dtu = miClsUsuarios.usuariosGetByUsuario(Session("UsuarioMin").ToString)

        Me.lblModalNombreAgente.Text = dtu.Rows(0)("usuario1").ToString().ToUpper().Trim()
        dtu.Dispose()

        Me.lblModalNroCarpeta.Text = row.Cells(0).Text

        'hfIdCarpeta.Value = Trim(row.Cells(0).Text.ToString + " ")
        hfIdCarpeta.Value = grillaBandejaLibroInspeccion.DataKeys(row.RowIndex).Values(0).ToString

        'hfNroCuenta.Value = Trim(row.Cells(1).Text.ToString + " ")
        hfNroCuenta.Value = grillaBandejaLibroInspeccion.DataKeys(row.RowIndex).Values(1).ToString

        hfNroCarpeta.Value = Trim(row.Cells(0).Text.ToString + " ")
        hfNroCuit.Value = Trim(row.Cells(1).Text.ToString + " ")

        ''''''''''Me.lblModalNroCarpeta.Text += "<br> id_carpeta " + hfIdCarpeta.Value.ToString + " numero de cuenta " + hfNroCuenta.Value.ToString
        '
        If Me.hfNroCuit.Value.ToString.Length <> 11 Then
            Me.hfNroCuit.Value = ""
        End If
        If hfNroCuit.Value.Trim <> "" Then
            Me.buscarEmpresaPorCuit(hfNroCuit.Value.Trim)
        End If

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        upModal.Update()
        '
    End Sub

    Protected Sub grillaBandejaLibroInspeccion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grillaBandejaLibroInspeccion.RowDataBound
        ''pinta fila si se esta mostrando detalle
        'If hfNroCarpeta.Value = e.Row.Cells(0).Text.ToString Then
        '    e.Row.BackColor = Drawing.Color.FromName("#F9DD9D")
        'End If
    End Sub

    '
    '  INICIA PASO A PASO SEGUN TIPO DE DOCUMENTO
    '
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        'Dim mivector As New ArrayList
        'mivector.Add(13433)
        'mivector.Add("01")
        'mivector.Add("2002")
        'mivector.Add("26")
        'mivector.Add("22")

        If Me.btnGuardar.Text.Trim = "Registro Provisorio" Then
            Response.Redirect("frmLiEmpresasAltaOficio.aspx?param=" & Me.txtCuitModal.Text.Trim)
        Else

            'Es valido el formulario?
            If validaModal() = True Then
                'acta de constatacion (definicion documeto=2001)

                Dim miVector As New ArrayList
                'Número de cuenta
                miVector.Add(hfNroCuenta.Value.ToString)
                'Sucursal
                If DropDownListSuc.SelectedIndex = 0 Then
                    miVector.Add("00")
                Else
                    If DropDownListSuc.SelectedValue.ToString.Trim.Length = 1 Then
                        miVector.Add("0" + DropDownListSuc.SelectedValue.ToString.Trim)
                    Else
                        miVector.Add(DropDownListSuc.SelectedValue.ToString.Trim())
                    End If
                End If
                'tipo de documento
                miVector.Add(Me.DropDownListTD.SelectedValue.ToString.Trim)
                'Delegacion
                miVector.Add(Me.DropDownListDel.SelectedValue.ToString.Trim)
                'idcarpeta
                miVector.Add(hfIdCarpeta.Value.ToString.Trim())
                'Nro carpeta
                miVector.Add(hfNroCarpeta.Value.ToString.Trim())
                '
                '-------------------------------------2001 acta de constatacion
                If Me.DropDownListTD.SelectedValue = "2001" Then
                    Dim proximoNumero As Int32 = clsUtiles.ObtenerProximoNroDocumentoPorTipo(CInt(hfNroCuenta.Value.ToString), CInt(hfIdCarpeta.Value.ToString.Trim()), "A")
                    'TD (EJEMPLO: A0, C0, ...)
                    miVector.Add("A" + Right("000" + proximoNumero.ToString.Trim, 3))
                    miVector.Add("0")

                    Response.Redirect("frmLIactaConstatacion.aspx?p=" + clsUtiles.CodificaParametros(miVector, "-"))
                End If
                '-------------------------------------2002 acta de infracción
                If Me.DropDownListTD.SelectedValue = "2002" Then

                    Dim proximoNumero As Int32 = clsUtiles.ObtenerProximoNroDocumentoPorTipo(CInt(hfNroCuenta.Value.ToString), CInt(hfIdCarpeta.Value.ToString.Trim()), "C")
                    'TD (EJEMPLO: A0, C0, ...)
                    miVector.Add("C" + Right("000" + proximoNumero.ToString.Trim, 3))
                    miVector.Add("0")

                    Response.Redirect("frmLIactaInfraccion.aspx?p=" + clsUtiles.CodificaParametros(miVector, "-"))
                End If
                '-------------------------------------2003 relevamiento de personal
                If Me.DropDownListTD.SelectedValue = "2003" Then

                    Dim proximoNumero As Int32 = clsUtiles.ObtenerProximoNroDocumentoPorTipo(CInt(hfNroCuenta.Value.ToString), CInt(hfIdCarpeta.Value.ToString.Trim()), "B")
                    'TD (EJEMPLO: A0, C0, ...)
                    miVector.Add("B" + Right("000" + proximoNumero.ToString.Trim, 3))
                    miVector.Add("0")

                    Response.Redirect("frmLiRelevamientoPersonal01.aspx?p=" + clsUtiles.CodificaParametros(miVector, "-"))
                End If
            End If
        End If

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        upModal.Update()

    End Sub

    '
    ' VALIDA MODAL NUEVA ACTUACION
    '
    Private Function validaModal() As Boolean
        Dim resultado As Boolean = True
        '
        Me.lblErrorComboDelegacion.Text = ""
        Me.lblErrorComboSucursal.Text = ""
        Me.lblErrorComboTipoDocumento.Text = ""
        '
        If Me.DropDownListDel.SelectedIndex = 0 Then
            resultado = False
            Me.lblErrorComboDelegacion.Text = "Ingrese la delegacion"
        End If

        If Me.DropDownListTD.SelectedIndex = 0 Then
            resultado = False
            Me.lblErrorComboTipoDocumento.Text = "Ingrese el tipo de documento"
        End If
        Return resultado
    End Function

    '
    ' BUSCA EMPRESA POR CUIL DENTRO DEL MODAL
    '
    Protected Sub btnBuscaCuilModal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscaCuilModal.Click
        buscarEmpresaPorCuit(Me.txtCuitModal.Text.Trim)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
        upModal.Update()
    End Sub

    '
    ' BUSCA EMPRESA POR CUIL DENTRO DEL MODAL (PROCEDIMIENTO)
    '
    Private Sub buscarEmpresaPorCuit(ByVal pCuit As String)

        Me.lblError.Text = ""

        If pCuit.Trim.Length = 11 And IsNumeric(pCuit) Then

            Using oConexion As New OracleConnection(cad1)
                Dim oCmd As New OracleCommand
                With oCmd
                    .Connection = oConexion
                    .CommandType = CommandType.Text
                    .CommandText = "select * from doc_laboral.vt_constancia_empleador where cuit='" + pCuit.Trim + "'"
                End With
                Dim da As New OracleDataAdapter(oCmd)
                Dim t As New DataTable
                da.Fill(t)

                '
                If t.Rows.Count > 0 Then
                    'ENABLE DISABLE
                    Me.CombosArea.Visible = True
                    Me.btnGuardar.Enabled = True
                    Me.buscarCuitArea.Visible = False
                    '

                    ' Verifica estado de las empresas y muestra color Azul para aprobado 
                    ' y aprobado provisorio y rojo para los otros estados
                    '
                    Dim htmlColor As String = ""
                    If t.Rows(0)("ESTADOEMPADRONAMIENTO").ToString.ToUpper.Contains("APROB") Then
                        htmlColor = " style=""color: #0000FF"""
                    Else
                        htmlColor = " style=""color: #FF0000"""
                    End If
                    '

                    Me.lblDatosEmpresa.Text = "Empleador: <strong>(" & t.Rows(0)("CUIT").ToString.Substring(0, 2) & "-" & _
                                            t.Rows(0)("CUIT").ToString.Substring(2, 8) & "-" & _
                                            t.Rows(0)("CUIT").ToString.Substring(10, 1) & ") " + t.Rows(0)("RAZON_SOCIAL") + "</strong> <br> Estado: <strong><span " & htmlColor & ">" + t.Rows(0)("ESTADOEMPADRONAMIENTO").ToString.ToUpper + "</span></strong>"

                    'Encuentra empresa y carga numero de cuenta
                    hfNroCuenta.Value = t.Rows(0)("NROCUENTA").ToString

                    Dim sqlStringSuc As String = "" & _
                     "select to_number(suc.idsucursal) IDSUCURSAL, suc.ubicacion || ' - (' || dv.n_calle || ' ' || CONCAT(' NRO: ', dv.ALTURA) || ' ' || case when dv.piso is null Then '' else CONCAT(' PISO: ', dv.PISO) end  || ' ' || case when dv.depto is null then '' else CONCAT(' DPTO: ', dv.DEPTO) end || ' - ' || trim(dv.n_localidad) || ' - ' || trim(dv.n_provincia) || ')' AS DOMICILIO " & _
                     "from doc_laboral.sucursales suc, dom_manager.vt_domicilios_cond dv " & _
                     "where(suc.id_vin = dv.id_vin) And dv.id_app = 197 And dv.fecha_baja Is null And suc.nrocuenta = " & t.Rows(0)("NROCUENTA").ToString & " And dv.id_tipodom = 11 And suc.fechabaja Is null "
                    '
                    If Me.txtCuitModal.Text = "" Then
                        ' provisoriamente por ahora solo acta circunstanciada - clsUtiles.CargaComboGen(DropDownListTD, "SELECT DD.IDDEFINICIONDOCUMENTO, DD.DOCUMENTO FROM DOC_LABORAL.DEFINICIONDOCUMENTOS DD WHERE DD.IDDEFINICIONDOCUMENTO IN (2001,2002,2003) ORDER BY DOCUMENTO", "DOCUMENTO", "IDDEFINICIONDOCUMENTO", "(Seleccione tipo de documento ...)")
                        clsUtiles.CargaComboGen(DropDownListTD, "SELECT DD.IDDEFINICIONDOCUMENTO, DD.DOCUMENTO FROM DOC_LABORAL.DEFINICIONDOCUMENTOS DD WHERE DD.IDDEFINICIONDOCUMENTO IN (2001) ORDER BY DOCUMENTO", "DOCUMENTO", "IDDEFINICIONDOCUMENTO", "(Seleccione tipo de documento ...)")
                    Else
                        'provisoriamente ... - clsUtiles.CargaComboGen(DropDownListTD, "SELECT DD.IDDEFINICIONDOCUMENTO, DD.DOCUMENTO FROM DOC_LABORAL.DEFINICIONDOCUMENTOS DD WHERE DD.IDDEFINICIONDOCUMENTO IN (2001,2003) ORDER BY DOCUMENTO", "DOCUMENTO", "IDDEFINICIONDOCUMENTO", "(Seleccione tipo de documento ...)")
                        clsUtiles.CargaComboGen(DropDownListTD, "SELECT DD.IDDEFINICIONDOCUMENTO, DD.DOCUMENTO FROM DOC_LABORAL.DEFINICIONDOCUMENTOS DD WHERE DD.IDDEFINICIONDOCUMENTO IN (2001) ORDER BY DOCUMENTO", "DOCUMENTO", "IDDEFINICIONDOCUMENTO", "(Seleccione tipo de documento ...)")
                    End If

                    clsUtiles.CargaComboGen(DropDownListSuc, sqlStringSuc, "DOMICILIO", "IDSUCURSAL", "(Empresa en general ... o seleccione sucursal)")
                    clsUtiles.CargaComboGen(DropDownListDel, "select TO_NUMBER(d.idlugaridenti) ID_DELEGACION, TRIM(D.LUGARIDENTIFICACION) N_DELEGACION from doc_laboral.lugaridentificacion D WHERE TO_NUMBER(d.idlugaridenti) <> 0 ORDER BY N_DELEGACION", "N_DELEGACION", "ID_DELEGACION", "(Seleccion Delegación)")

                Else
                    Me.buscarCuitArea.Visible = False
                    Me.NoEncuentraCuit.Visible = True
                    Me.btnGuardar.Text = "Registro Provisorio"
                    Me.btnGuardar.Enabled = True
                    Me.lblCuitNoEncontrado.Text = "No existe empresa Empadronada con esa identificación. De ser correcto el CUIT: <b>" & Me.txtCuitModal.Text.Trim & "</b> proceda a registrar provisoriamente la actuación."
                End If
            End Using
        Else
            Me.lblError.Text = "El Numero de CUIT no tiene el formato esperado"
            Me.txtCuitModal.Focus()
        End If
    End Sub


    '
    ' CARGA DETALLE DE ACTUACIONES PARA UN PROCEDIMIENTO/CARPETA
    '
    Protected Sub LinkButtonDetalleActuaciones_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lb As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(lb.NamingContainer, GridViewRow)
        '
        If CInt(row.Cells(3).Text) > 0 Then
            'Me.lblTituloGrillaActasDetalle.Text = "<strong>Procedimiento: " & Right(("00000000" + row.Cells(0).Text.Trim).ToString, 8) & " - Empleador: " & row.Cells(2).Text.Trim & "</ strong>"
            'Me.lblTituloGrillaActasDetalle.Text = "<div style=""color: #FFFFFF; background-color: #0099FF; padding-top: 1px; padding-bottom: 5px""> PROCEDIMIENTO : <strong>" & row.Cells(0).Text.Trim & " </strong> - EMPLEADOR : <strong>" & row.Cells(2).Text.Trim & "</strong></div>"
            Me.lblTituloGrillaActasDetalle.Text = "PROCEDIMIENTO : <strong>" & row.Cells(0).Text.Trim & " </strong> - EMPLEADOR : <strong>" & row.Cells(2).Text.Trim & "</strong>"
            'cargaDetalleActuacionesCarpeta(CInt(row.Cells(0).Text))
            cargaDetalleActuacionesCarpeta(CInt(grillaBandejaLibroInspeccion.DataKeys(row.RowIndex).Values(0).ToString()))

            Dim A As Integer = 1

            ''''''''''''''''''''''''''''''''  row.BackColor = Drawing.Color.FromName("#F9DD9D")

        Else
            Me.lblTituloGrillaActasDetalle.Text = ""
            Me.grillaCarpetaDetalle.DataSource = ""
            Me.grillaCarpetaDetalle.DataBind()
        End If


        'If CType(e.Row.FindControl("idcontrol"), Label).Text = ID Then
        '    e.Row.BackColor = Drawing.Color.FromName("#F9DD9D")
        'End If

    End Sub


    '
    ' CARGA DETALLE DE ACTUACIONES PARA UN PROCEDIMIENTO/CARPETA (PROCEDIMIENTO)
    '
    Private Sub cargaDetalleActuacionesCarpeta(ByVal pIdCarpeta As Integer)
        'Dim sqlString As String = "select * from doc_laboral.t_li_actas where id_carpeta=" + pIdCarpeta.ToString.Trim
        Dim sqlString As String = "select A.ID_ACTA, A.NRO_ACTA, A.FECHA_HORA, E.N_ESTADO_ACTA, DD.DOCUMENTO, SUBSTR(A.NRO_ACTA,9,2) ABREVIATURA,  a.id_resultado_notifica, rn.n_resultado_notifica, a.n_obs_notifica " & _
                                    "From DOC_LABORAL.T_LI_ACTAS A " & _
                                    "LEFT JOIN DOC_LABORAL.T_LI_ACTAS_ESTADOS E " & _
                                    "ON A.ID_ESTADO_ACTA = E.ID_ESTADO_ACTA " & _
                                    "LEFT JOIN DOC_LABORAL.T_LI_MODELOSACTAS M " & _
                                    "ON A.ID_ACTA_MODELO=M.ID_ACTA_MODELO " & _
                                    "LEFT JOIN DOC_LABORAL.DEFINICIONDOCUMENTOS DD " & _
                                    "ON M.ID_DEFINICIONDOCUMENTO = DD.IDDEFINICIONDOCUMENTO " & _
                                    "LEFT JOIN DOC_LABORAL.t_Li_Resultados_Notificaciones rn " & _
                                    "ON a.id_resultado_notifica = rn.id_resultado_notifica " & _
                                    "WHERE ID_CARPETA=" + pIdCarpeta.ToString.Trim & _
                                    " ORDER BY A.FECHA_HORA "
        Dim t As DataTable
        t = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlString).Tables(0)
        Me.grillaCarpetaDetalle.DataSource = t
        Me.grillaCarpetaDetalle.DataBind()

    End Sub

    '
    ' RESTAURA GRILLA PROCEDIMIENTOS / CARPETAS Y MUESTRA TODOS
    '
    Protected Sub btnTodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTodos.Click
        Me.txtBuscarProcedimiento.Text = ""
        cargarBarandillaLI(Me.txtBuscarProcedimiento.Text.Trim)

        'LIMPIA GRILLA DETALLE DE ACTAS
        grillaCarpetaDetalle.DataSource = Nothing
        grillaCarpetaDetalle.DataBind()
        '
        Me.lblTituloGrillaActasDetalle.Text = ""

    End Sub

    '
    '   GENERA CARPETAS VACIAS PARA AGENTE EN CASO DE SER NECESARIO 
    '
    Protected Sub btnLibretasSinUsar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLibretasSinUsar.Click
        'LIMPIA GRILLA DETALLE DE ACTAS
        grillaCarpetaDetalle.DataSource = Nothing
        grillaCarpetaDetalle.DataBind()
        '
        Me.lblTituloGrillaActasDetalle.Text = ""
        '-------------------------

        'Limpia mensajes de validacion
        Me.lblErrorComboDelegacion.Text = ""
        Me.lblErrorComboSucursal.Text = ""
        Me.lblErrorComboTipoDocumento.Text = ""


        Dim sqlString1 As String = "SELECT COUNT(*) FROM (select (select count(*) from (doc_laboral.t_li_actas) where doc_laboral.t_li_actas.id_carpeta = c.id_carpeta) MOVIMIENTOS from doc_laboral.t_li_carpetas c where c.idusuario=" & Session("UsuarioMinId").ToString & " ) WHERE MOVIMIENTOS=0"
        Dim CarpetasVacias As Int32 = OracleHelper.ExecuteScalar(cad1, CommandType.Text, sqlString1)

        If CarpetasVacias < 5 Then
            Dim res As Int32 = 0
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()
            Dim cmd As New OracleCommand("Doc_laboral.PR_LI_CARPETAS_NRO", conexion)
            cmd.CommandType = CommandType.StoredProcedure
            '
            cmd.Parameters.Add("pCANTIDAD", OracleType.Number).Value = 10 - CarpetasVacias
            cmd.Parameters.Add("pIDUSUARIO", OracleType.Number).Value = CInt(Session("UsuarioMinId").ToString)
            cmd.Parameters.Add("O_RESULTADO", OracleType.Number).Direction = ParameterDirection.Output
            cmd.ExecuteNonQuery()
            res = cmd.Parameters("O_RESULTADO").Value
            '
            conexion.Close()
        End If

        cargarBarandillaLI("USU")

    End Sub


    '
    ' CARGA DETALLE DE ACTUACIONES PARA UN PROCEDIMIENTO/CARPETA
    '
    Protected Sub ddlAgentes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAgentes.SelectedIndexChanged
        cargarBarandillaLI("Me.txtBuscarProcedimiento.Text.Trim")

        'LIMPIA GRILLA DETALLE DE ACTAS
        grillaCarpetaDetalle.DataSource = Nothing
        grillaCarpetaDetalle.DataBind()
        '
        Me.lblTituloGrillaActasDetalle.Text = ""
    End Sub

    Protected Sub grillaCarpetaDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grillaCarpetaDetalle.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            If e.Row.Cells(3).Text.Trim <> "EN PROCESO" Then
                Dim lnkBtn As LinkButton = e.Row.FindControl("LinkButtonEditarActa")
                lnkBtn.Visible = False
            End If
            Dim lblTipoDoc As Label = e.Row.FindControl("lblTipoDocu")
            If e.Row.Cells(1).Text.Trim.Substring(8, 1) = "A" Then
                lblTipoDoc.Text = "<span class=""label label-primary""><strong>" & e.Row.Cells(1).Text.Trim.Substring(8, 4) & "<strong></span>"
            End If
            If e.Row.Cells(1).Text.Trim.Substring(8, 1) = "B" Then
                lblTipoDoc.Text = "<span class=""label label-success""><strong>" & e.Row.Cells(1).Text.Trim.Substring(8, 4) & "<strong></span>"
            End If
            If e.Row.Cells(1).Text.Trim.Substring(8, 1) = "C" Then
                lblTipoDoc.Text = "<span class=""label label-danger""><strong>" & e.Row.Cells(1).Text.Trim.Substring(8, 4) & "<strong></span>"
            End If

            Dim lnkBtnNot As LinkButton = e.Row.FindControl("LinkButtonNotifica")
            Dim lbl As Label = e.Row.FindControl("lblresNotifica")
            If grillaCarpetaDetalle.DataKeys(e.Row.RowIndex).Values(0).ToString = "" Then
                lnkBtnNot.Visible = True
                lbl.Visible = False
            Else
                lnkBtnNot.Visible = False
                lbl.Text = grillaCarpetaDetalle.DataKeys(e.Row.RowIndex).Values(0).ToString
                lbl.ToolTip = grillaCarpetaDetalle.DataKeys(e.Row.RowIndex).Values(1).ToString
            End If

        End If
    End Sub

    Protected Sub LinkButtonPdfActa_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnkbtn As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(lnkbtn.NamingContainer, GridViewRow)

        Dim sqlString As String = "select bd.idbarandilladocumento from doc_laboral.barandilladocumentopdf bd where id_acta=" & row.Cells(0).Text.Trim
        Dim pIdBarandilla As Int32 = OracleHelper.ExecuteScalar(cad1, CommandType.Text, sqlString)
        '
        If pIdBarandilla > 0 Then
            Dim popupScript As String
            popupScript = String.Empty
            popupScript = "<script language='JavaScript'>"
            popupScript += "window.open('frmPdfViewActas.aspx?op=2&origen=&idDoc=" & pIdBarandilla.ToString & "', 'CustomPopUp', "
            popupScript += "'top=80,left=100 ,width=800, height=800, menubar=no, scrollbars=NO ,resizable=NO')"
            popupScript += "</script>"
            AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "popPdf", popupScript, False)
        Else
            Response.Write("<script>alert('No se ha generado el archivo PDF de esta actuación');</script>")
        End If
        '
    End Sub

    Protected Sub LinkButtonEditarActa_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnkbtn As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(lnkbtn.NamingContainer, GridViewRow)

        Dim sqlString As String = "SELECT NVL(A.NROCUENTA,0) NROCUENTA, " & _
                                    "NVL(A.IDSUCURSAL,'00') IDSUCURSAL, " & _
                                    "NVL(M.ID_DEFINICIONDOCUMENTO,0) ID_DEFINICIONDOCUMENTO, " & _
                                    "NVL(A.ID_LUGARIDENTI,0) ID_LUGARIDENTI, " & _
                                    "NVL(A.ID_CARPETA,0) ID_CARPETA, " & _
                                    "NVL(C.NRO_CARPETA,0) NRO_CARPETA, " & _
                                    "NVL(SUBSTR(A.NRO_ACTA, 9, 4),'  ') TIPO_DOCUMENTO, " & _
                                    "NVL(A.ID_ACTA ,0) ID_ACTA " & _
                                    "FROM DOC_LABORAL.T_LI_ACTAS A " & _
                                    "LEFT JOIN DOC_LABORAL.T_LI_CARPETAS C " & _
                                    "ON A.ID_CARPETA = C.ID_CARPETA " & _
                                    "LEFT JOIN DOC_LABORAL.T_LI_MODELOSACTAS M " & _
                                    "ON A.ID_ACTA_MODELO = M.ID_ACTA_MODELO " & _
                                    "WHERE ID_ACTA = " + row.Cells(0).Text.ToString

        Dim t As DataTable
        t = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlString).Tables(0)

        Dim mivector As New ArrayList
        mivector.Add(t.Rows(0)(0).ToString) ' numero de cuenta
        mivector.Add(t.Rows(0)(1).ToString) ' id sucursal
        mivector.Add(t.Rows(0)(2).ToString) ' definicion documento
        mivector.Add(t.Rows(0)(3).ToString) ' delegacion
        mivector.Add(t.Rows(0)(4).ToString) ' id carpeta
        mivector.Add(t.Rows(0)(5).ToString) ' nro carpeta
        mivector.Add(t.Rows(0)(6).ToString) ' tipo documento Ej. A0
        mivector.Add(t.Rows(0)(7).ToString) ' ID_ACTA

        If t.Rows(0)(6).ToString.Substring(0, 1) = "A" Then   ' ACTA DE CONSTATACION
            Response.Redirect("frmLIactaConstatacion.aspx?p=" + clsUtiles.CodificaParametros(mivector, "-"))
        End If

        If t.Rows(0)(6).ToString.Substring(0, 1) = "C" Then   ' ACTA DE INFRACCION
            Response.Redirect("frmLIactaInfraccion.aspx?p=" + clsUtiles.CodificaParametros(mivector, "-"))
        End If
    End Sub


#Region "Notificadores"

    Protected Sub LinkButtonNotifica_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnkbtn As LinkButton = CType(sender, LinkButton)
        Dim row As GridViewRow = CType(lnkbtn.NamingContainer, GridViewRow)
        hfidacta.Value = row.Cells(0).Text.ToString
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalNotifica", "$('#myModalNotifica').modal();", True)
        UpdatePanelNotifica.Update()
    End Sub

    Protected Sub btnAceptarNoti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarNoti.Click
        If txtdescripción.Text.Trim = String.Empty Then
            ScriptManager.RegisterStartupScript(Page, Me.GetType, "script1", "alert('Ingrese la Descripción');", True)
            txtdescripción.Focus()
            Return
        Else

            'Actualizo Notificación

            Dim cnn As New OracleConnection(cad1)
            cnn.Open()
            Dim cmd As New OracleCommand("Doc_Laboral.PR_LI_Res_Notif_Update", cnn)
            cmd.CommandType = CommandType.StoredProcedure
            Try

                cmd.Parameters.Add("p_id_acta", OracleType.Number).Value = CInt(hfidacta.Value)
                cmd.Parameters.Add("p_id_resultado", OracleType.Number).Value = CInt(cbonombre.SelectedValue)
                cmd.Parameters.Add("p_descripcion", OracleType.VarChar).Value = txtdescripción.Text.Trim

                cmd.ExecuteNonQuery()

            Catch ex As Exception
                Throw
            Finally
                cnn.Close()
                cmd.Dispose()
                cnn.Dispose()
            End Try

            ScriptManager.RegisterStartupScript(Page, Me.GetType, "script1", "alert('Se Actualizó Correctamente');", True)
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "myModalNotifica", "$('#myModalNotifica').modal('hide');", True)

        End If
    End Sub

    Protected Sub btnCancelarNoti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarNoti.Click


    End Sub

    Public Sub CargarComboOpciones(ByVal popcion As Integer)

        Dim ssql As String = String.Empty
        ssql = "select id_resultado_notifica, n_resultado_notifica" & _
               "  from doc_laboral.t_li_resultados_notificaciones n" & _
               " where n.n_opcion = " & popcion & ""

        clsUtiles.CargaComboGen(cbonombre, ssql, "n_resultado_notifica", "id_resultado_notifica", "")
    End Sub


#End Region

    Protected Sub cboOpcion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboOpcion.SelectedIndexChanged
        UpdatePanelNotifica.Update()
        If IsPostBack Then
            CargarComboOpciones(cboOpcion.SelectedValue)
        End If


    End Sub
End Class


