Imports CapaDatos
Imports System.Data
Imports System.Data.OracleClient
Imports Microsoft.VisualBasic

Public Class clsEmpresas
    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Private tipoempresa As Object

    Public Function BuscarCondIVA() As DataTable
        Dim dt As DataTable
        Dim sqlSelect As New StringBuilder()
        sqlSelect.Append("Select id_condicion_iva, n_condicion_iva From  t_comunes.t_condiciones_iva Order By n_condicion_iva ASC")
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlSelect.ToString()).Tables(0)
        Return dt
    End Function

    Public Function BuscarTipoIIBB() As DataTable
        Dim dt As DataTable
        Dim sqlSelect As New StringBuilder()
        sqlSelect.Append("select id_condicion_ingbruto, n_condicion_ingbruto from t_comunes.t_condiciones_ingbruto")
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlSelect.ToString()).Tables(0)
        Return dt
    End Function

    Public Function BuscarRubros() As DataTable
        Dim dt As DataTable
        Dim sqlSelect As New StringBuilder()
        sqlSelect.Append("select id_rubro, n_rubro from t_comunes.t_rubros Order By n_rubro ASC")
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlSelect.ToString()).Tables(0)
        Return dt
    End Function

    Public Function mostrarRubros()
        Dim param(1) As Object
        Try
            param(0) = ""
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.p_rubros_sel", param).Tables(0)
        Catch ex As Exception

        End Try
    End Function

    Public Function BuscarTipoEmpresas() As DataTable
        Dim dt As DataTable
        Dim ssql As String
        ssql = "Select id_forma_juridica,n_forma_juridica From t_comunes.t_formas_juridica Order By id_forma_juridica ASC"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function BuscarTipoEmpresas(ByVal pidemptipo As Integer) As DataTable
        Dim dt As DataTable
        Dim ssql As String
        ssql = "Select id_forma_juridica,n_forma_juridica From t_comunes.t_formas_juridica where id_forma_juridica = " & pidemptipo & " "
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function BuscarEmpresa(ByVal pcuit As String) As DataTable
        Dim dt As DataTable
        Dim ssql As String = "Select * From t_comunes.t_pers_juridica Where cuit = '" & pcuit.Trim & "'"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function
    Public Function BuscarEmpresaNroCta(ByVal pcuit As String) As DataTable
        Dim dt As DataTable
        Dim ssql As String = "Select * From Empresas Where cuit = '" & pcuit.Trim & "' and idestado <> 4"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function BuscarEmpresaPorRazon(ByVal prazon As String) As DataTable
        Dim dt As DataTable
        Dim ssql As String = "Select * From t_comunes.t_pers_juridica where razon_social like '%" & prazon.ToUpper & "%' "
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function BuscarEmpresaPorFantasia(ByVal pfantasia As String) As DataTable
        Dim dt As DataTable
        Dim ssql As String = "select cuit, nombre_fantasia as razon_social, razon_social as nombre_fantasia from t_comunes.t_pers_juridica " _
                    & "where nombre_fantasia like '%" & pfantasia.ToUpper.Trim & "%' order by nombre_fantasia"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function BuscarMailsEmpresa(ByVal pcuit As String) As DataTable
        Dim dt As DataTable
        Dim ssql As String = "select mail, mail1 from empresas where cuit = '" + pcuit.Trim + "'"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Sub InsertarEmpresa(ByVal nrocuenta As Integer, ByVal razon As String, ByVal fantasia As String, ByVal fechaAltaEmp As String, ByVal fechaInicioAct As String, _
    ByVal fechaInicioCom As String, ByVal cuit As String, ByVal ingbruto As String, ByVal nrocentralizacion As String, ByVal regdocunificada As String, ByVal domiciliounico As String, _
    ByVal tipopersoneria As String, ByVal nromatricula As String, ByVal fechaInscripcion As String, ByVal idRubro As String, ByVal empleadorSiNo As String, _
    ByVal correoEmpleador As String, ByVal correoEstudio As String, ByVal tipoempresa As String, ByVal idcondiva As String, ByVal clave As String, ByVal cantemp As String, ByVal acuerdo As String, ByVal idmedpres As String, _
    ByVal pobsfj As String, ByVal pobjci As String)

        Dim sqlInsert As New StringBuilder
        sqlInsert.Append("insert into empresas (nrocuenta, idemptipo, " _
        & "cuit, usuario_internet, clave_internet, fecalta, fecbaja, " _
        & "fecaltaemp, feciniciocom, " _
        & "claveenviada, numcentral, regdocunificada, dunico, fecha_revision, estado_revision, " _
        & "nromatricula, fechainscripcion, empleadorsino, " _
        & "cantempleado, idestado, id_catdocxempresa, idmedpres, acuerdo, secuencia, id_rubro, fecfa, obsfj, obsci ) ")
        sqlInsert.Append("values ")
        sqlInsert.Append("(" & nrocuenta & ", ")
        sqlInsert.Append("" & tipoempresa & ", ")
        sqlInsert.Append("'" & cuit.Trim & "', ")
        sqlInsert.Append("'" & cuit.Trim & "','" & clave.Trim & "',")
        sqlInsert.Append("to_date(sysdate,'dd/mm/rrrr'),NULL,")
        sqlInsert.Append("to_date('" & fechaAltaEmp.Trim & "','dd/mm/rrrr'), ")
        sqlInsert.Append("to_date('" & fechaInicioCom.Trim & "','dd/mm/rrrr'), ")
        sqlInsert.Append("NULL,")
        sqlInsert.Append("'" & nrocentralizacion.Trim & "', ")
        sqlInsert.Append("'" & regdocunificada.Trim & "', ")
        sqlInsert.Append("'" & domiciliounico.Trim & "', ")
        sqlInsert.Append("NULL,NULL,")
        sqlInsert.Append("'" & nromatricula.Trim & "', ")
        sqlInsert.Append("to_date('" & fechaInscripcion.Trim & "','dd/mm/rrrr'), ")
        sqlInsert.Append("'" & empleadorSiNo & "', ")
        sqlInsert.Append("" & cantemp & ", ")
        sqlInsert.Append("10,NULL,")
        sqlInsert.Append("" & idmedpres & ", ")
        sqlInsert.Append("" & acuerdo & ", ")
        sqlInsert.Append("NULL, ")
        sqlInsert.Append("'" & idRubro.Trim & "' ,")
        sqlInsert.Append("to_date('" & fechaInicioAct & "','dd/mm/rrrr'), ")
        sqlInsert.Append(" '" & pobsfj & "', '" & pobjci & " ' )")

        OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlInsert.ToString())
    End Sub

    Public Function ObtenerNroCuenta(ByVal pcuit As String) As Integer
        Dim sqlnrocuenta As String
        Dim nrocuenta As Integer
        sqlnrocuenta = "select nrocuenta from empresas where cuit=" & "'" & pcuit.Trim & "'"
        nrocuenta = CInt(OracleHelper.ExecuteScalar(cad1, CommandType.Text, sqlnrocuenta).ToString)
        Return nrocuenta
    End Function

    Public Function GenerarID() As Integer
        Dim nro As Integer
        nro = OracleHelper.ExecuteScalar(cad1, CommandType.Text, "select sec_nrocuenta.nextval  from dual")
        Return nro
    End Function

    Public Function BuscarEstado(ByVal pestado As String) As Integer
        Dim estado As Integer
        Dim ssql As String
        ssql = "select idestado from Estado where estado = '" & pestado.Trim & "'"
        estado = CInt(OracleHelper.ExecuteScalar(cad1, CommandType.Text, ssql))
        Return estado
    End Function

    Public Function BuscarEstado() As DataTable
        Dim dt As DataTable
        Dim ssql As String
        ssql = "select * from Estado order by idestado asc"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Sub ActualizarEstado(ByVal pestado As Integer, ByVal pcuit As String)
        Dim ssql As String
        ssql = "update empresas set idestado = " & pestado & " where cuit = '" & pcuit.Trim & "'"
        OracleHelper.ExecuteNonQuery(cad1, CommandType.Text, ssql)
    End Sub

    Public Sub ActualizarSecuencia(ByVal psecuencia As Integer, ByVal pcuit As String)
        Dim ssql As String
        ssql = "update empresas set secuencia = " & psecuencia & " where cuit = '" & pcuit.Trim & "'"
        OracleHelper.ExecuteNonQuery(cad1, CommandType.Text, ssql)
    End Sub

    Public Function BuscarEmpresasActividades(ByVal pcuit As String) As DataTable
        Dim ssql As String
        Dim dt As DataTable
        ssql = "SELECT EA.Id_actividad, tA.n_actividad, ea.fecha_inicio, ea.fecfa " _
             & "FROM EMPRESAS E " _
             & "inner join  t_comunes.t_actividades_perjur EA on ea.cuit =e.cuit " _
             & "inner join  t_comunes.t_actividades TA on ta.id_actividad = ea.id_actividad " _
             & "WHERE E.cuit ='" & pcuit.Trim & "'"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function InsertarT_Rubros_Perjuridica(ByVal p_cuit As String, ByVal pid_rubro As String, ByVal p_fec_ini As Date) As String
        Dim cnn As New OracleConnection(cad1)
        cnn.Open()
        Dim cmd As New OracleCommand("t_comunes.pack_persona_juridica.INSERTA_RUBROS_PERSJUR", cnn)
        cmd.CommandType = CommandType.StoredProcedure

        Dim res As String
        cmd.Parameters.Add("p_cuit", OracleType.NVarChar).Value = p_cuit
        cmd.Parameters.Add("p_id_rubro", OracleType.NVarChar).Value = pid_rubro
        cmd.Parameters.Add("p_fec_ini", OracleType.DateTime).Value = p_fec_ini
        cmd.Parameters.Add("o_resultado", OracleType.NVarChar, 900).Direction = ParameterDirection.Output
        cmd.ExecuteNonQuery()

        res = cmd.Parameters("o_resultado").Value
        cmd.Dispose()
        cnn.Dispose()
        Return res
    End Function

    Public Function InsertarPerjuridica(ByVal p_cuit As String, ByVal p_razon_social As String, ByVal p_nom_fan As String, _
    ByVal p_id_for_jur As String, ByVal p_id_cond_iva As String, ByVal p_id_aplicacion As Integer, ByVal p_fec_inicio_act As Date, _
    ByVal p_nro_ing_bruto As String, ByVal p_id_cond_ingbruto As String) As String

        Dim cnn As New OracleConnection(cad1)
        cnn.Open()
        Dim cmd As New OracleCommand("t_comunes.pack_persona_juridica.INSERTA_PERSJUR", cnn)
        cmd.CommandType = CommandType.StoredProcedure

        Dim res As String
        cmd.Parameters.Add("p_cuit", OracleType.NVarChar).Value = p_cuit
        cmd.Parameters.Add("p_razon_social", OracleType.NVarChar).Value = p_razon_social
        cmd.Parameters.Add("p_nom_fan", OracleType.VarChar).Value = p_nom_fan
        cmd.Parameters.Add("p_id_for_jur", OracleType.VarChar).Value = p_id_for_jur
        cmd.Parameters.Add("p_id_cond_iva", OracleType.NVarChar).Value = p_id_cond_iva
        cmd.Parameters.Add("p_id_aplicacion", OracleType.Number).Value = p_id_aplicacion
        cmd.Parameters.Add("p_fec_inicio_act", OracleType.DateTime).Value = p_fec_inicio_act
        cmd.Parameters.Add("p_nro_ing_bruto", OracleType.VarChar).Value = p_nro_ing_bruto
        cmd.Parameters.Add("p_id_cond_ingbruto", OracleType.VarChar).Value = p_id_cond_ingbruto

        cmd.Parameters.Add("o_id_sede", OracleType.NVarChar, 100).Direction = ParameterDirection.Output
        cmd.Parameters.Add("o_resultado", OracleType.NVarChar, 900).Direction = ParameterDirection.Output

        cmd.ExecuteNonQuery()

        res = cmd.Parameters("o_resultado").Value
        cmd.Dispose()
        cnn.Close()
        cnn.Dispose()
        Return res
    End Function

    Public Function BuscaDatosEmpresas(ByVal OP As Integer, ByVal pcuit As String) As DataTable
        Dim dt As DataTable
        Dim ssql As String = String.Empty
        ' si OP es igual a 1 entonces buscamos en Perjuridica
        If OP = 1 Then
            ssql = "select vj.cuit, vj.razon_social, vj.nombre_fantasia, nvl(vj.id_forma_juridica,'01') id_forma_juridica, " _
                 & "nvl(vj.id_condicion_iva,'01') id_condicion_iva, nvl(vj.id_condicion_ingbruto,'01') id_condicion_ingbruto, " _
                 & "vj.nro_ingbruto, vj.id_aplicacion, to_char(vj.fec_inicio_act,'dd/mm/yyyy') fec_inicio_act, rp.id_rubro " _
                 & "from t_comunes.vt_pers_juridicas_completa vj " _
                 & "inner join t_comunes.t_rubros_persjuridica rp on vj.cuit = rp.cuit " _
                 & "where vj.cuit = '" & pcuit.Trim() & "'"
        End If
        ' si OP es igual a 2 buscamos en Empresas
        If OP = 2 Then
            ssql = "select nrocuenta, cuit, to_char(fecaltaemp,'dd/mm/yyyy') fecaltaemp, to_char(feciniciocom,'dd/mm/yyyy')feciniciocom, numcentral, regdocunificada, nromatricula, to_char(fechainscripcion,'dd/mm/yyyy') fechainscripcion, " _
                 & "empleadorsino, cantempleado, idestado, id_catdocxempresa, idmedpres,acuerdo,nvl(secuencia,0) secuencia  from empresas " _
                 & "where cuit = '" & pcuit.Trim() & "'"
        End If
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function ModificarPerjuridica(ByVal p_cuit As String, ByVal p_razon_social As String, ByVal p_nom_fan As String, _
    ByVal p_id_for_jur As String, ByVal p_id_cond_iva As String, ByVal p_id_aplicacion As Integer, ByVal p_fec_inicio_act As Date, _
    ByVal p_nro_ing_bruto As String, ByVal p_id_cond_ingbruto As String) As String

        Dim cnn As New OracleConnection(cad1)
        cnn.Open()
        Dim cmd As New OracleCommand("t_comunes.pack_persona_juridica.MODIFICA_PERSJUR", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim res As String

        cmd.Parameters.Add("p_cuit", OracleType.NVarChar).Value = p_cuit
        cmd.Parameters.Add("p_razon_social", OracleType.NVarChar).Value = p_razon_social
        cmd.Parameters.Add("p_nom_fan", OracleType.VarChar).Value = p_nom_fan
        cmd.Parameters.Add("p_id_for_jur", OracleType.VarChar).Value = p_id_for_jur
        cmd.Parameters.Add("p_id_cond_iva", OracleType.NVarChar).Value = p_id_cond_iva
        cmd.Parameters.Add("p_id_aplicacion", OracleType.Number).Value = p_id_aplicacion
        cmd.Parameters.Add("p_nro_ing_bruto", OracleType.VarChar).Value = p_nro_ing_bruto
        cmd.Parameters.Add("p_id_cond_ingbruto", OracleType.VarChar).Value = IIf(String.IsNullOrEmpty(p_id_cond_ingbruto), DBNull.Value, p_id_cond_ingbruto)

        cmd.Parameters.Add("o_resultado", OracleType.NVarChar, 900).Direction = ParameterDirection.Output

        cmd.ExecuteNonQuery()

        res = cmd.Parameters("o_resultado").Value
        cmd.Dispose()
        cnn.Dispose()
        cnn.Close()


        Return res
        
    End Function

    Public Function ModificarT_Rubros_Perjuridica(ByVal p_cuit As String, ByVal pid_rubro_viejo As String, ByVal pid_rubro_nuevo As String) As String
        Dim cnn As New OracleConnection(cad1)
        cnn.Open()
        Dim cmd As New OracleCommand("t_comunes.pack_persona_juridica.MODIFICA_RUBRO_DOCLAB", cnn)
        cmd.CommandType = CommandType.StoredProcedure

        Dim res As String
        cmd.Parameters.Add("p_cuit", OracleType.NVarChar).Value = p_cuit
        cmd.Parameters.Add("p_id_rubro_viejo", OracleType.NVarChar).Value = pid_rubro_viejo
        cmd.Parameters.Add("p_id_rubro_nuevo", OracleType.NVarChar).Value = pid_rubro_nuevo
        cmd.Parameters.Add("p_fec_ini", OracleType.NVarChar).Value = ""
        cmd.Parameters.Add("p_fec_fin", OracleType.NVarChar).Value = ""
        cmd.Parameters.Add("o_resultado", OracleType.NVarChar, 900).Direction = ParameterDirection.Output
        cmd.ExecuteNonQuery()



        res = IIf(IsDBNull(cmd.Parameters("o_resultado").Value), "OK", cmd.Parameters("o_resultado").Value)
        cmd.Dispose()
        cnn.Dispose()
        cnn.Close()
        Return res
        
    End Function

    Public Sub ModificarEmpresa(ByVal nrocuenta As String, ByVal fechaAltaEmp As String, ByVal fechaInicioAct As String, _
    ByVal fechaInicioCom As String, ByVal cuit As String, ByVal nrocentralizacion As String, ByVal regdocunificada As String, ByVal domiciliounico As String, _
    ByVal nromatricula As String, ByVal fechaInscripcion As String, ByVal idRubro As String, ByVal empleadorSiNo As String, _
    ByVal tipoempresa As String, ByVal cantemp As String, ByVal acuerdo As String, ByVal idmedpres As String, ByVal pObsfj As String, ByVal pObjci As String, Optional ByVal pfiltrado As Integer = 0)

        Try

            Dim cnn As New OracleConnection(cad1)
            cnn.Open()
            Dim cmd As New OracleCommand("DOC_LABORAL.P_EMPRESAS_UPD", cnn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("PCUIT", OracleType.VarChar).Value = seteaValoresNulos(cuit)
            cmd.Parameters.Add("PNROCUENTA", OracleType.Number).Value = seteaValoresNulos(nrocuenta)
            cmd.Parameters.Add("PNUMCENTRAL", OracleType.VarChar).Value = seteaValoresNulos(nrocentralizacion)
            cmd.Parameters.Add("PREGDOCUNIFICADA", OracleType.VarChar).Value = seteaValoresNulos(regdocunificada)
            cmd.Parameters.Add("PDUNICO", OracleType.VarChar).Value = seteaValoresNulos(domiciliounico)
            cmd.Parameters.Add("PNROMATRICULA", OracleType.VarChar).Value = seteaValoresNulos(nromatricula)
            cmd.Parameters.Add("PFECHAINSCRIPCION", OracleType.VarChar).Value = seteaValoresNulos(fechaInscripcion)
            cmd.Parameters.Add("PFECHAALTAEMPLEADOR", OracleType.VarChar).Value = seteaValoresNulos(fechaAltaEmp)
            cmd.Parameters.Add("PFECHAINICIOCOM", OracleType.VarChar).Value = seteaValoresNulos(fechaInicioCom)
            cmd.Parameters.Add("pfechainicioact", OracleType.VarChar).Value = seteaValoresNulos(fechaInicioAct)
            cmd.Parameters.Add("PEMPLEADORSINO", OracleType.VarChar).Value = seteaValoresNulos(empleadorSiNo)
            cmd.Parameters.Add("PCANT_EMPLEADOS", OracleType.Number).Value = seteaValoresNulos(cantemp)
            cmd.Parameters.Add("PIDRUBRO", OracleType.VarChar).Value = seteaValoresNulos(idRubro)
            cmd.Parameters.Add("PACUERDO", OracleType.VarChar).Value = seteaValoresNulos(acuerdo)
            cmd.Parameters.Add("PIDMEDIOPRESENTACION", OracleType.Number).Value = seteaValoresNulos(idmedpres)
            cmd.Parameters.Add("PIDEMPTIPO", OracleType.Number).Value = seteaValoresNulos(tipoempresa)
            cmd.Parameters.Add("POBSFJ", OracleType.VarChar).Value = pObsfj
            cmd.Parameters.Add("POBSCI", OracleType.VarChar).Value = pObjci

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnn.Close()
            cnn.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Public Function seteaValoresNulos(ByVal ppropiedad As Object) As Object

        Dim obj As Object
        Dim vRetorno As Object
        vRetorno = ppropiedad

        Try
            obj = ppropiedad.GetType.Name

            Select Case obj
                Case "Int32"
                    If IsNothing(ppropiedad) Then
                        Return DBNull.Value
                    Else
                        Return vRetorno
                    End If

                Case "Decimal"
                    If IsNothing(ppropiedad) Then
                        Return DBNull.Value
                    Else
                        Return vRetorno
                    End If

                Case "DateTime"
                    If DateTime.MinValue = ppropiedad Then
                        Return DBNull.Value
                    Else
                        Return vRetorno
                    End If
                Case "String"
                    If IsNothing(ppropiedad) Then
                        Return DBNull.Value
                    Else
                        Return vRetorno
                    End If
                Case "Int16"
                    If IsNothing(ppropiedad) Then
                        Return DBNull.Value
                    Else
                        Return vRetorno
                    End If
            End Select



        Catch ex As Exception
            vRetorno = DBNull.Value
            Return vRetorno
        End Try

    End Function


    Public Sub InsertarMedioComunica(ByVal P_ID_TIPO_COMUNICACION As String, ByVal p_cuit As String, ByVal P_NRO_MAIL As String, _
                                     ByVal P_COD_AREA As String)

        Dim cnn As New OracleConnection(cad1)
        cnn.Open()
        Dim cmd As New OracleCommand("t_comunes.pack_comunicaciones.INSERTA_COMUNICACIONES", cnn)
        cmd.CommandType = CommandType.StoredProcedure

        Dim res As String
        cmd.Parameters.Add("P_ID_TIPO_COMUNICACION", OracleType.NVarChar).Value = P_ID_TIPO_COMUNICACION
        cmd.Parameters.Add("P_ENTIDAD", OracleType.NVarChar).Value = p_cuit & "00"
        cmd.Parameters.Add("P_NRO_MAIL", OracleType.NVarChar).Value = P_NRO_MAIL
        cmd.Parameters.Add("P_COD_AREA", OracleType.NVarChar).Value = P_COD_AREA
        cmd.Parameters.Add("P_ID_APLICACION", OracleType.NVarChar).Value = "197"
        cmd.Parameters.Add("P_ORIGEN_TABLA", OracleType.NVarChar).Value = "EMPRESAS"
        cmd.Parameters.Add("o_resultado", OracleType.NVarChar, 900).Direction = ParameterDirection.Output
        cmd.ExecuteNonQuery()

        res = cmd.Parameters("o_resultado").Value
        cmd.Dispose()
        cnn.Dispose()
        cnn.Close()
    End Sub

    Public Sub EliminarMedioComunica(ByVal P_ID_TIPO_COMUNICACION As String, ByVal p_cuit As String)

        Dim cnn As New OracleConnection(cad1)
        cnn.Open()
        Dim cmd As New OracleCommand("t_comunes.pack_comunicaciones.ELIMINA_COMUNICACION", cnn)
        cmd.CommandType = CommandType.StoredProcedure

        Dim res As String

        cmd.Parameters.Add("P_ID_TIPO_COMUNICACION", OracleType.NVarChar).Value = P_ID_TIPO_COMUNICACION
        cmd.Parameters.Add("P_ENTIDAD", OracleType.NVarChar).Value = p_cuit & "00"
        cmd.Parameters.Add("P_ID_APLICACION", OracleType.NVarChar).Value = "197"
        cmd.Parameters.Add("P_TABLA_ORIGEN", OracleType.NVarChar).Value = "EMPRESAS"
        cmd.Parameters.Add("o_resultado", OracleType.NVarChar, 900).Direction = ParameterDirection.Output
        cmd.ExecuteNonQuery()

        res = cmd.Parameters("o_resultado").Value
        cmd.Dispose()
        cnn.Dispose()
        cnn.Close()
    End Sub

    Public Function BuscarEmails(ByVal pidcomu As String, ByVal pentidad As String) As DataTable
        Dim dt As DataTable
        Dim ssql As String
        ssql = "select * from t_comunes.t_comunicaciones where id_tipo_comunicacion = '" & pidcomu & "' and " _
              & "id_entidad = '" & pentidad & "'"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function EmpresaGetbyCuit(ByVal pCuit As String) As DataTable

        Dim param(1) As Object
        param(0) = pCuit

        Return OracleHelper.ExecuteDataset(cad1, "EmpresaGetbyCuit", param).Tables(0)

    End Function

    Public Function actualizaEmpresa(ByVal pcuit As String _
                                    , ByVal pnrocuenta As Integer _
                                    , ByVal pnumcentral As String _
                                    , ByVal pregdocunificada As String _
                                    , ByVal pdunico As String _
                                    , ByVal pnromatricula As String _
                                    , ByVal pfechainscripcion As String _
                                    , ByVal pfechaAltaEmp As String _
                                    , ByVal pfechaInicioCom As String _
                                    , ByVal pfechaInicioAct As String _
                                    , ByVal pempleadorSiNo As String _
                                    , ByVal pcantemp As Integer _
                                    , ByVal pidrubro As String _
                                    , ByVal pacuerdo As String _
                                    , ByVal pidmedpres As Integer _
                                    , ByVal ptipoempresa As String _
                                    , ByRef pidcondiinva As String)

        Dim cnn As New OracleConnection(cad1)
        Dim obj As Object
        cnn.Open()
        Dim cmd As New OracleCommand("DOC_LABORAL.P_EMPRESAS_UPD", cnn)
        If String.IsNullOrEmpty(pfechainscripcion) Then
            obj = DBNull.Value
        Else
            obj = Date.Parse(pfechainscripcion)
        End If
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("PCUIT", OracleType.VarChar).Value = seteaValoresNulos(pcuit)
        cmd.Parameters.Add("PNROCUENTA", OracleType.Number).Value = seteaValoresNulos(pnrocuenta)
        cmd.Parameters.Add("PNUMCENTRAL", OracleType.VarChar).Value = seteaValoresNulos(pnumcentral)
        cmd.Parameters.Add("PREGDOCUNIFICADA", OracleType.VarChar).Value = seteaValoresNulos(pregdocunificada)
        cmd.Parameters.Add("PDUNICO", OracleType.VarChar).Value = seteaValoresNulos(pdunico)
        cmd.Parameters.Add("PNROMATRICULA", OracleType.VarChar).Value = seteaValoresNulos(pnromatricula)
        cmd.Parameters.Add("PFECHAINSCRIPCION", OracleType.VarChar).Value = seteaValoresNulos(pfechainscripcion)
        cmd.Parameters.Add("PFECHAALTAEMPLEADOR", OracleType.VarChar).Value = seteaValoresNulos(pfechaAltaEmp)
        cmd.Parameters.Add("PFECHAINICIOCOM", OracleType.VarChar).Value = seteaValoresNulos(pfechaInicioCom)
        cmd.Parameters.Add("pfechainicioact", OracleType.VarChar).Value = seteaValoresNulos(pfechaInicioAct)
        cmd.Parameters.Add("PEMPLEADORSINO", OracleType.VarChar).Value = seteaValoresNulos(pempleadorSiNo)
        cmd.Parameters.Add("PCANT_EMPLEADOS", OracleType.Number).Value = seteaValoresNulos(pcantemp)
        cmd.Parameters.Add("PIDRUBRO", OracleType.VarChar).Value = seteaValoresNulos(pidrubro)
        cmd.Parameters.Add("PACUERDO", OracleType.VarChar).Value = seteaValoresNulos(pacuerdo)
        cmd.Parameters.Add("PIDMEDIOPRESENTACION", OracleType.Number).Value = seteaValoresNulos(pidmedpres)
        cmd.Parameters.Add("PIDEMPTIPO", OracleType.Number).Value = seteaValoresNulos(tipoempresa)
        cmd.Parameters.Add("POBSFJ", OracleType.VarChar).Value = ptipoempresa
        cmd.Parameters.Add("POBSCI", OracleType.VarChar).Value = pidcondiinva
        cmd.ExecuteNonQuery()
        cmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    'Se Utiliza para el sinceramiento
    Public Function actualizaEmpresaSinceramiento(ByVal pcuit As String _
                                    , ByVal pnrocuenta As Integer _
                                    , ByVal pnumcentral As String _
                                    , ByVal pregdocunificada As String _
                                    , ByVal pdunico As String _
                                    , ByVal pnromatricula As String _
                                    , ByVal pfechainscripcion As String _
                                    , ByVal pfechaAltaEmp As String _
                                    , ByVal pfechaInicioCom As String _
                                    , ByVal pfechaInicioAct As String _
                                    , ByVal pempleadorSiNo As String _
                                    , ByVal pcantemp As Integer _
                                    , ByVal pidart As Integer _
                                    , ByVal pacuerdo As String _
                                    , ByVal pidmedpres As Integer _
                                    , ByVal ptipoempresa As String _
                                    , ByRef pidcondiinva As String)

        Dim cnn As New OracleConnection(cad1)
        Dim obj As Object
        cnn.Open()
        Dim cmd As New OracleCommand("DOC_LABORAL.P_EMPRESAS_UPD_SIN", cnn)
        If String.IsNullOrEmpty(pfechainscripcion) Then
            obj = DBNull.Value
        Else
            obj = Date.Parse(pfechainscripcion)
        End If
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("PCUIT", OracleType.VarChar).Value = seteaValoresNulos(pcuit)
        cmd.Parameters.Add("PNROCUENTA", OracleType.Number).Value = seteaValoresNulos(pnrocuenta)
        cmd.Parameters.Add("PNUMCENTRAL", OracleType.VarChar).Value = seteaValoresNulos(pnumcentral)
        cmd.Parameters.Add("PREGDOCUNIFICADA", OracleType.VarChar).Value = seteaValoresNulos(pregdocunificada)
        cmd.Parameters.Add("PDUNICO", OracleType.VarChar).Value = seteaValoresNulos(pdunico)
        cmd.Parameters.Add("PNROMATRICULA", OracleType.VarChar).Value = seteaValoresNulos(pnromatricula)
        cmd.Parameters.Add("PFECHAINSCRIPCION", OracleType.VarChar).Value = seteaValoresNulos(pfechainscripcion)
        cmd.Parameters.Add("PFECHAALTAEMPLEADOR", OracleType.VarChar).Value = seteaValoresNulos(pfechaAltaEmp)
        cmd.Parameters.Add("PFECHAINICIOCOM", OracleType.VarChar).Value = seteaValoresNulos(pfechaInicioCom)
        cmd.Parameters.Add("pfechainicioact", OracleType.VarChar).Value = seteaValoresNulos(pfechaInicioAct)
        cmd.Parameters.Add("PEMPLEADORSINO", OracleType.VarChar).Value = seteaValoresNulos(pempleadorSiNo)
        cmd.Parameters.Add("PCANT_EMPLEADOS", OracleType.Number).Value = seteaValoresNulos(pcantemp)
        cmd.Parameters.Add("PIDART", OracleType.VarChar).Value = seteaValoresNulos(pidart)
        cmd.Parameters.Add("PACUERDO", OracleType.VarChar).Value = seteaValoresNulos(pacuerdo)
        cmd.Parameters.Add("PIDMEDIOPRESENTACION", OracleType.Number).Value = seteaValoresNulos(pidmedpres)
        cmd.Parameters.Add("PIDEMPTIPO", OracleType.Number).Value = seteaValoresNulos(tipoempresa)
        cmd.Parameters.Add("POBSFJ", OracleType.VarChar).Value = ptipoempresa
        cmd.Parameters.Add("POBSCI", OracleType.VarChar).Value = pidcondiinva
        cmd.ExecuteNonQuery()
        cmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

	
	  Public Function BuscarEmpresasFiltradas(ByVal pcuit As String) As DataTable
        Dim ssql As String
        Dim dt As DataTable
        ssql = "SELECT CUIT from filtradas WHERE cuit ='" & pcuit.Trim & "'"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
     End Function

    Public Function BuscarEmpresasActividadesPrincipal(ByVal pcuit As String) As DataTable
        Dim ssql As String
        Dim dt As DataTable
        Dim vretorno As Integer
        Dim vselect As String
        vretorno = cad1.ToUpper.IndexOf("DESA")

        If vretorno > 1 Then
            vselect = "SELECT e.nrocuenta, EA.Id_actividad, tA.n_actividad, ea.fecha_inicio,  ea.id_rubro, ea.id_tipo_actividad "
        Else
            vselect = "SELECT e.nrocuenta, EA.Id_actividad, tA.n_actividad, ea.fecha_inicio,  ea.id_rubro, ea.id_tipo_actividad "
        End If

        ssql = vselect _
              & "FROM EMPRESAS E " _
              & "inner join  t_comunes.vt_perjur_actividades EA on ea.cuit =e.cuit " _
              & "inner join  t_comunes.t_actividades TA on ta.id_actividad = ea.id_actividad " _
              & "where e.cuit = '" & pcuit.Trim & "' and ea.id_actividad = " _
              & "(select empa.id_actividad from empresasactividades empa " _
              & "inner join empresas on empa.nrocuenta = empresas.nrocuenta " _
              & "where empresas.cuit = '" & pcuit.Trim & "' and empa.id_tipoa = 1) and ea.fecha_fin is null"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)


        Return dt
    End Function

    Public Function mostrarEmpresas(ByVal pCuit As String, ByVal pid_estado As Integer) As DataTable

        Dim param(2) As Object

        Try
            param(0) = pCuit
            param(1) = pid_estado
            param(2) = ""
            Return OracleHelper.ExecuteDataset(cad1, "DOC_LABORAL.p_empresas_sel", param).Tables(0)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
	
	 Public Function AcuerdoFD(ByVal pcuit As String) As Integer

        Dim rta As Integer
        Dim conexion As New OracleConnection(cad1)
        Dim comando As New OracleCommand()


        Try
            comando.CommandType = CommandType.StoredProcedure
            comando.Connection = conexion
            comando.CommandText = "AcuerdoFD"
            conexion.Open()


            Dim v_cuit As OracleParameter = New OracleParameter("pcuit", System.Data.OracleClient.OracleType.VarChar)
            v_cuit.Value = pcuit
            comando.Parameters.Add(v_cuit)

            Dim o_resultado As OracleParameter = New OracleParameter("o_resultado", System.Data.OracleClient.OracleType.Int32)
            o_resultado.Direction = ParameterDirection.ReturnValue
            comando.Parameters.Add(o_resultado)

            comando.ExecuteNonQuery()
            rta = o_resultado.Value.ToString().Trim()


        Catch ex As Exception

        Finally
            comando.Dispose()
            conexion.Close()
            conexion.Dispose()
        End Try

        Return rta
    End Function

    Public Function domicilioSueldoEmpresa(ByVal pNroCuenta As Int32) As String

        Dim rta As String = ""
        Dim conexion As New OracleConnection(cad1)
        Dim comando As New OracleCommand()

        Try
            comando.CommandType = CommandType.StoredProcedure
            comando.Connection = conexion
            comando.CommandText = "DOC_LABORAL.F_DOMICILIO_SUELDO"
            conexion.Open()


            Dim p_NroCuenta As OracleParameter = New OracleParameter("pnrocuenta", System.Data.OracleClient.OracleType.Number)
            p_NroCuenta.Value = pNroCuenta
            comando.Parameters.Add(p_NroCuenta)

            Dim o_resultado As OracleParameter = New OracleParameter("o_resultado", System.Data.OracleClient.OracleType.VarChar, 300)
            o_resultado.Direction = ParameterDirection.ReturnValue
            comando.Parameters.Add(o_resultado)

            comando.ExecuteNonQuery()
            rta = o_resultado.Value.ToString().Trim()


        Catch ex As Exception

        Finally
            comando.Dispose()
            conexion.Close()
            conexion.Dispose()
        End Try

        Return rta
    End Function

    Public Function empresasCantidadDeEmpleados(ByVal pNroCuenta As String) As Integer
        Dim sqlString As String
        sqlString = "SELECT doc_laboral.F_EMPLEADOS_POR_EMPRESA(" & pNroCuenta.ToString.Trim & ") FROM DUAL"
        Return CInt(OracleHelper.ExecuteScalar(cad1, CommandType.Text, sqlString).ToString)
    End Function


End Class
