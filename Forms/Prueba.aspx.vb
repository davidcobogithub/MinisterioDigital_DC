Imports CapaDatos
Imports System.Data

Partial Class Prueba
    Inherits System.Web.UI.Page
    Dim cencr As New ClaseEncripta("1234")
    Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If IsPostBack Then

            ''            ''''ANA
            ''
            '' Session("LoginEmpresa") = "SI"
            'Session("LoginMinisterio") = "SI"
            'Session("cuit") = "20298794056"
            'Session("empresa") = "asegada"
            ''Session("cuit") = "30708323353"
            'Session("UsuarioMin") = "27203523101"
            'Session("UsuarioMinId") = "2"
            ' ''''''Session("UsuarioMinId") = "360"


            ''''' WALTER----------------------------
            '


            'MsgBox("WALTER CBA1")
            ''
            Session("LoginMinisterio") = "SI"
            Session("cuit") = "20165017170"
            Session("empresa") = "asegada"
            Session("UsuarioMin") = "20165017170"

            'desarrollo
            'Session("UsuarioMinId") = "360"

            'produccion
            Session("UsuarioMinId") = "621"


            '
            ''-------------------------------------





            Session("nrocuenta") = 55674
            '13433
            'Session("lugarident") = "0472"
            'Session("rectificativa") = "S"
            'Session("tipopersoneria") = "FISICA"
            'Session("cantemp") = 30
            'Response.Redirect("FrmEmpresasDomicilios.aspx")
            'Response.Redirect("Empresas.aspx")

            Dim psec As String = ClaseGeneraClaveMin.EncriptarCadena("123123231231")
            Dim pcuit As String = ClaseGeneraClaveMin.EncriptarCadena("10210270892")
            Dim pxn As String = ClaseGeneraClaveMin.EncriptarCadena("79892")
            Dim pd As String = ClaseGeneraClaveMin.EncriptarCadena("9")



            '---------PCP
            'Response.Redirect("frmPCP-EmpleadorAdmin.aspx")
            'Response.Redirect("frmPCP-Inicio.aspx")

            'Response.Redirect("frmInspectorDigital.aspx")

            ' Response.Redirect("http://localhost:57850/MinTrabajoDigital_DESA/frmEvValidacion.aspx?p=Z3dRaGwreS81dUZSdUZEcUdaakRRODZUVW4za3FXM1c1V0dUdE9GOEEycz01")

            'Response.Redirect("frmEVEventos.aspx")
            'Response.Redirect("frmEVEventosAdministracion.aspx")
            Response.Redirect("frmsin01.aspx")

            'Response.Redirect("index.aspx")
            'Response.Redirect("frmPlanillaHorarioQR.aspx?psec=" & "111142123165421354645643213121231321564456545632131321" & "&pcuit=" & "10210270892" & "&pxn=" & "79892" & "&pd=" & "9" & " ")
            'Response.Redirect("frmConsultaInspeccion.aspx")
            'Response.Redirect("frmUsuariosLibrosPasaje.aspx")
            'Response.Redirect("frmEmpleadoCambiaFechaIngreso.aspx")
            'Response.Redirect("frmDesbloqueCidi.aspx")
            'Response.Redirect("frmVencimientos.aspx")
            'Response.Redirect("frmsucursales.aspx")
            'Response.Redirect("frmFaq.aspx")
            'Response.Redirect("frmCymat.aspx")
            'Response.Redirect("frmEstadosDocumentos.aspx")
            'Response.Redirect("frmUsuariosSistema.aspx")
            'Response.Redirect("frmCambiarEstadoBD.aspx")
            'Response.Redirect("frmLogs.aspx")
            'Response.Redirect("frmLIMenuInspeccion.aspx")
            'Response.Redirect("frmEstadisticas.aspx")
            'Response.Redirect("frmConsultaEmpleadoLM.aspx")
            'Response.Redirect("frmLiBarandillaLi.aspx")
            'Response.Redirect("frmLiBarandillaEmpresaLi.aspx")
            'Response.Redirect("frmDashboard.aspx")
            'Response.Redirect("frmDashboardEmpresas.aspx")
            'Response.Redirect("frmLiEmpresasAltaOficio.aspx")
            'Response.Redirect("frmDashboardPD.aspx")
            'Response.Redirect("frminsertacuilmasivo.aspx")
            'Response.Redirect("frmInsertaPersonaRCMasivo.aspx")
            'Response.Redirect("frmeetsd.aspx")
            'Response.Redirect("frmPersonasFisicasCotejarRcivil.aspx")
            'Response.Redirect("frmAprobadoProvisorioPasar.aspx")
            'Response.Redirect("frmCambiarEstadoBDM.aspx")
            'Response.Redirect("frmLiBarandillaEmpresaLi.aspx")
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim contenido As Byte() = Nothing
        Dim dt As New DataTable
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, "select adjunto  from multinotasadjuntos where id_muladj = 21").Tables(0)
        contenido = dt.Rows(0)("adjunto")
        Response.ContentType = "application/pdf"
        Response.BinaryWrite(contenido)
        Response.End()
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click

    End Sub
End Class
