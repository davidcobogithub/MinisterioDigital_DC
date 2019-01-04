Imports CapaDatos
Imports System.Data
Imports System.Data.OracleClient
Imports Microsoft.VisualBasic
Imports System.Reflection


Public Class clsEmpleados
    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Private conexion As New OracleConnection(cad1)
    Private cmd As New OracleCommand

    Public id_aplicacion As Integer
    Public id_sexo As String
    Public nro_documento As String
    Public pai_cod_pais_origen As String
    Public pai_cod_pais_nacionalidad As String
    Public id_tipo_documento As String
    Public organismo_emisor_doc As String
    Public pais_tipo_doc As String
    Public localidad As Integer
    Public apellido As String
    Public nombre As String
    Public clase As Integer
    Public observaciones As String
    Public fec_nacimiento As DateTime
    Public id_grupo_familiar As String
    Public id_estado_civil As String
    Public vinculo As String
    Public fecha_defuncion As DateTime
    Public fechaingreso As DateTime
    Public nrocuenta As Integer
    Public idempleado As Integer
    Public legajo As String
    Public idsindicato As Integer
    Public idconvenio As Integer
    Public afiliado As Integer
    Public fecingreso As DateTime
    Public fecegreso As DateTime
    Public idsocial As Integer
    Public idcategoria As Integer
    Public idcontrato As Integer
    Public idsucursal As String
    Public sueldo As Decimal
    Public reorgsocietaria As String
    Public nrolibretachofer As String
    Public idmotivo As Integer
    Public id_numero As Integer
    Public cuil As String
    Public id_sindicato As Integer
    Public id_rama As Integer
    Public id_convenio As Integer
    Public id_orden As Integer
    Public categoria As String
    Public cuit As String
    Public fechaegreso As Date
    Public id_empleado As Int32
    Public personascargo As Integer
    Public convenioafip As String
    Public idnivel As String
    Public idaseguradora As Integer
    Public fechaingresoant As String
    Public o_retorno As String

    Public Function insertacttualizaEmpleado()
        Dim mensaje As String
        Dim cmd As New OracleCommand("DOC_LABORAL.P_EMPLEADOSXSUCURSAL_IUD", conexion)

        Try
            conexion.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("P_ID_APLICACION", OracleType.Number).Value = seteaValoresNulos(id_aplicacion)
            cmd.Parameters.Add("P_ID_SEXO", OracleType.VarChar).Value = seteaValoresNulos(id_sexo)
            cmd.Parameters.Add("P_NRO_DOCUMENTO", OracleType.VarChar).Value = seteaValoresNulos(nro_documento)
            cmd.Parameters.Add("P_PAI_COD_PAIS_ORIGEN", OracleType.VarChar).Value = seteaValoresNulos(pai_cod_pais_origen)
            cmd.Parameters.Add("P_PAI_COD_PAIS_NACIONALIDAD", OracleType.VarChar).Value = seteaValoresNulos(pai_cod_pais_nacionalidad)
            cmd.Parameters.Add("P_ID_TIPO_DOCUMENTO", OracleType.VarChar).Value = seteaValoresNulos(id_tipo_documento)
            cmd.Parameters.Add("P_ORGANISMO_EMISOR_DOC", OracleType.VarChar).Value = seteaValoresNulos(organismo_emisor_doc)
            cmd.Parameters.Add("P_PAIS_TIPO_DOC", OracleType.VarChar).Value = seteaValoresNulos(pais_tipo_doc)
            cmd.Parameters.Add("P_LOCALIDAD", OracleType.Number).Value = seteaValoresNulos(localidad)
            cmd.Parameters.Add("P_APELLIDO", OracleType.VarChar).Value = seteaValoresNulos(apellido)
            cmd.Parameters.Add("P_NOMBRE", OracleType.VarChar).Value = seteaValoresNulos(nombre)
            cmd.Parameters.Add("P_CLASE", OracleType.Number).Value = seteaValoresNulos(clase)
            cmd.Parameters.Add("P_OBSERVACIONES", OracleType.VarChar).Value = seteaValoresNulos(observaciones)
            cmd.Parameters.Add("P_FEC_NACIMIENTO", OracleType.DateTime).Value = seteaValoresNulos(fec_nacimiento)
            cmd.Parameters.Add("P_ID_GRUPO_FAMILIAR", OracleType.VarChar).Value = seteaValoresNulos(id_grupo_familiar)
            cmd.Parameters.Add("P_ID_ESTADO_CIVIL", OracleType.VarChar).Value = seteaValoresNulos(id_estado_civil)
            cmd.Parameters.Add("P_VINCULO", OracleType.VarChar).Value = seteaValoresNulos(vinculo)
            cmd.Parameters.Add("P_FECHA_DEFUNCION", OracleType.DateTime).Value = seteaValoresNulos(fecha_defuncion)
            cmd.Parameters.Add("PNROCUENTA", OracleType.Number).Value = seteaValoresNulos(nrocuenta)
            cmd.Parameters.Add("PIDEMPLEADO", OracleType.Number).Value = seteaValoresNulos(idempleado)
            cmd.Parameters.Add("PLEGAJO", OracleType.VarChar).Value = seteaValoresNulos(legajo)
            cmd.Parameters.Add("PIDSINDICATO", OracleType.Number).Value = seteaValoresNulos(idsindicato)
            cmd.Parameters.Add("PIDCONVENIO", OracleType.Number).Value = seteaValoresNulos(idconvenio)
            cmd.Parameters.Add("PAFILIADO", OracleType.Number).Value = seteaValoresNulos(afiliado)
            cmd.Parameters.Add("PFECINGRESO", OracleType.DateTime).Value = seteaValoresNulos(fecingreso)
            cmd.Parameters.Add("PFECEGRESO", OracleType.DateTime).Value = seteaValoresNulos(fecegreso)
            cmd.Parameters.Add("PIDSOCIAL", OracleType.Number).Value = seteaValoresNulos(idsocial)
            cmd.Parameters.Add("PIDCATEGORIA", OracleType.Number).Value = seteaValoresNulos(idcategoria)
            cmd.Parameters.Add("PIDCONTRATO", OracleType.Number).Value = seteaValoresNulos(idcontrato)
            cmd.Parameters.Add("PIDSUCURSAL", OracleType.VarChar).Value = seteaValoresNulos(idsucursal)
            cmd.Parameters.Add("PSUELDO", OracleType.Number).Value = seteaValoresNulos(sueldo)
            cmd.Parameters.Add("PREORGSOCIETARIA", OracleType.VarChar).Value = seteaValoresNulos(reorgsocietaria)
            cmd.Parameters.Add("PNROLIBRETACHOFER", OracleType.VarChar).Value = seteaValoresNulos(nrolibretachofer)
            cmd.Parameters.Add("PIDMOTIVO", OracleType.Number).Value = seteaValoresNulos(idmotivo)
            cmd.Parameters.Add("PID_NUMERO", OracleType.Number).Value = seteaValoresNulos(id_numero)
            cmd.Parameters.Add("PCUIL", OracleType.VarChar).Value = seteaValoresNulos(cuil)
            cmd.Parameters.Add("PID_SINDICATO", OracleType.Number).Value = id_sindicato
            cmd.Parameters.Add("PID_RAMA", OracleType.Number).Value = id_rama
            cmd.Parameters.Add("PID_CONVENIO", OracleType.Number).Value = id_convenio
            cmd.Parameters.Add("PID_ORDEN", OracleType.Number).Value = id_orden
            cmd.Parameters.Add("PCATEGORIA", OracleType.NVarChar).Value = seteaValoresNulos(categoria)
            cmd.Parameters.Add("PO_RETORNO", OracleType.VarChar, 300).Direction = ParameterDirection.Output
            cmd.ExecuteNonQuery()
            conexion.Close()
        Catch ex As Exception
            Throw ex
        Finally
            cmd.Dispose()
            conexion.Close() 
        End Try


    End Function


    Public Function mostrarEmpleados() As DataTable
        Dim param(2) As Object
        Try
            param(0) = nrocuenta
            param(1) = idsucursal
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.p_empleadosxsucursal_sel", param).Tables(0)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function buscarEmpleadoPersona()
        Dim param(5) As Object

        param(0) = nro_documento
        param(1) = id_sexo
        param(2) = idempleado
        param(3) = nrocuenta
        param(4) = idsucursal
        param(5) = ""

        Try
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.p_empleados_sel", param).Tables(0)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function seteaValoresNulos(ByVal ppropiedad As Object) As Object
        Dim propiedades() As PropertyInfo
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

    Public Function empleadoscuil_sel(ByVal pcuil As String, ByVal pnrocuenta As Integer) As DataTable
        Dim param(2) As Object
        Try
            param(0) = pcuil
            param(1) = pnrocuenta
            param(2) = ""
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.empleados_sel", param).Tables(0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function empleados_ins()
        Dim param(18) As Object
        Dim sqlIns As New StringBuilder
        Dim vfechaingreso As String
        Dim vfechaegreso As Object
        Dim vconvenioafip As Object
        Dim vidmovitoegreso As Object

        vfechaingreso = fechaingreso.ToString.Substring(0, 10)
        If fechaegreso = Date.MinValue Then
            vfechaegreso = DBNull.Value
        Else
            vfechaegreso = fechaegreso.ToString.Substring(0, 10)
        End If

        If idmotivo = 0 Then
            vidmovitoegreso = DBNull.Value
        Else
            vidmovitoegreso = idmotivo
        End If

        If String.IsNullOrEmpty(convenioafip.ToString) Then
            vconvenioafip = DBNull.Value
        End If



        Try
            sqlIns.Append("  insert into doc_laboral.empleadosempresa")
            sqlIns.Append(" (id_empleado,nro_documento,id_sexo,pai_cod_pais,")
            sqlIns.Append("  id_numero,fec_ingreso,fec_egreso,idmotivoegreso,idsindicato,")
            sqlIns.Append("  idconvenio,idobrasocial,idcontrato,fec_alta,usr_alta,")
            sqlIns.Append("  nrocuenta,cant_personascargo,idniveleducativo,convenioafip,")
            sqlIns.Append("  idaseguradora,cargo,origen_dato)")
            sqlIns.Append(" values")
            sqlIns.Append(" (doc_laboral.sec_empleados.nextval,'" + nro_documento.ToString + "','" + id_sexo.ToString + "','" + pai_cod_pais_origen.ToString + "',")
            sqlIns.Append(" " + id_numero.ToString + ", to_date('" + vfechaingreso.ToString + "','dd/mm/yyyy'), ")
            sqlIns.Append(" to_date('" + vfechaegreso.ToString + "','dd/mm/yyyy'), '" + vidmovitoegreso.ToString + "'," + idsindicato.ToString + ",")
            sqlIns.Append(" " + idconvenio.ToString + "," + idsocial.ToString + "," + idcontrato.ToString + ", sysdate, user,")
            sqlIns.Append(" " + nrocuenta.ToString + "," + personascargo.ToString + ",'" + idnivel.ToString + "','" + vconvenioafip.ToString + "',")
            sqlIns.Append(" " + idaseguradora.ToString + ", '" + categoria.ToString + "','CM')")


            OracleHelper.ExecuteNonQuery(cad1, CommandType.Text, sqlIns.ToString)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function empleados_retornaid(ByVal pobjempleados As clsEmpleados) As String
        Try
            Dim param(3) As Object
            param(0) = pobjempleados.nrocuenta
            param(1) = pobjempleados.nro_documento
            param(2) = pobjempleados.id_sexo
            param(3) = ""
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.empleadosid_sel", param).Tables(0).Rows(0)("id_empleado").ToString

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function empleados_ins(ByVal EmpleadoObj As Object)
        Dim param(18) As Object
        Dim sqlIns As New StringBuilder
        Dim vfechaingreso As String
        Dim vfechaegreso As Object
        Dim vconvenioafip As Object
        Dim vidmovitoegreso As Object

        Try
            vfechaingreso = EmpleadoObj.fechaingreso.ToString.Substring(0, 10)
            If EmpleadoObj.fechaegreso = Date.MinValue Then
                vfechaegreso = DBNull.Value
            Else
                vfechaegreso = EmpleadoObj.fechaegreso.ToString.Substring(0, 10)
            End If

            If EmpleadoObj.idmotivo = 0 Then
                vidmovitoegreso = DBNull.Value
            Else
                vidmovitoegreso = idmotivo
            End If

            If String.IsNullOrEmpty(EmpleadoObj.convenioafip.ToString) Then
                vconvenioafip = DBNull.Value
            End If


            sqlIns.Append("  insert into doc_laboral.empleadosempresa")
            sqlIns.Append(" (id_empleado,nro_documento,id_sexo,pai_cod_pais,")
            sqlIns.Append("  id_numero,fec_ingreso,fec_egreso,idmotivoegreso,idsindicato,")
            sqlIns.Append("  idconvenio,idobrasocial,idcontrato,fec_alta,usr_alta,")
            sqlIns.Append("  nrocuenta,cant_personascargo,idniveleducativo,convenioafip,")
            sqlIns.Append("  idaseguradora,cargo,origen_dato)")
            sqlIns.Append(" values")
            sqlIns.Append(" (" + EmpleadoObj.id_empleado.ToString + ",'" + EmpleadoObj.nro_documento.ToString + "','" + EmpleadoObj.id_sexo.ToString + "','" + EmpleadoObj.pai_cod_pais_origen.ToString + "',")
            sqlIns.Append(" " + EmpleadoObj.id_numero.ToString + ", to_date('" + vfechaingreso.ToString + "','dd/mm/yyyy'), ")
            sqlIns.Append(" to_date('" + vfechaegreso.ToString + "','dd/mm/yyyy'), '" + vidmovitoegreso.ToString + "'," + EmpleadoObj.idsindicato.ToString + ",")
            sqlIns.Append(" " + EmpleadoObj.idconvenio.ToString + "," + EmpleadoObj.idsocial.ToString + "," + EmpleadoObj.idcontrato.ToString + ", sysdate, user,")
            sqlIns.Append(" " + EmpleadoObj.nrocuenta.ToString + "," + EmpleadoObj.personascargo.ToString + ",'" + EmpleadoObj.idnivel.ToString + "','" + vconvenioafip.ToString + "',")
            sqlIns.Append(" " + EmpleadoObj.idaseguradora.ToString + ", '" + EmpleadoObj.categoria.ToString + "', 'CM')")


            OracleHelper.ExecuteNonQuery(cad1, CommandType.Text, sqlIns.ToString)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function empleados_upd()

        Dim param(19) As Object
        Dim sqlUpd As New StringBuilder
        Dim vfechaegreso As Object
        Try

            If fechaegreso = Date.MinValue Then
                vfechaegreso = DBNull.Value
            Else
                vfechaegreso = fechaegreso.ToString.Substring(0, 10)
            End If

            sqlUpd.Append(" update doc_laboral.empleadosempresa set ")
            sqlUpd.Append(" fec_egreso=  to_date('" + vfechaegreso.ToString + "', 'dd/mm/yyyy'),")
            sqlUpd.Append(" idmotivoegreso   = decode(" + idmotivo.ToString + ",0,null," + idmotivo.ToString + "),")
            sqlUpd.Append(" idsindicato=" + idsindicato.ToString + ",")
            sqlUpd.Append(" idconvenio=" + idconvenio.ToString + ",")
            sqlUpd.Append(" idobrasocial=" + idsocial.ToString + ",")
            sqlUpd.Append(" idaseguradora=" + idaseguradora.ToString + ",")
            sqlUpd.Append(" cant_personascargo=" + personascargo.ToString + ",")
            sqlUpd.Append(" idniveleducativo='" + idnivel.ToString + "',")
            sqlUpd.Append(" cargo='" + categoria.ToString + "',")
            sqlUpd.Append(" idcontrato=" + idcontrato.ToString)
            sqlUpd.Append(" where nrocuenta=" + nrocuenta.ToString + "")
            sqlUpd.Append(" and id_empleado=" + id_empleado.ToString)
            sqlUpd.Append(" and fec_ingreso= to_date('" + fechaingresoant.ToString.Substring(0, 10) + "','dd/mm/yyyy')")


            OracleHelper.ExecuteNonQuery(cad1, CommandType.Text, sqlUpd.ToString)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Function

    Public Function empleados_del()
        Try
            Dim param(2) As Object
            param(0) = nrocuenta
            param(1) = idempleado
            param(2) = fecingreso
            OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.empleados_del", param)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function empleados_actbaj(ByVal pnrocuenta As Integer, ByVal pactbaj As Int16) As DataTable
        Try
            Dim param(2) As Object
            param(0) = pnrocuenta
            param(1) = pactbaj
            param(2) = ""

            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.empleados_abt", param).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function contratos_sel() As DataTable

        Try
            Dim param(1) As Object
            param(0) = 0
            param(1) = ""
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.contratos_sel", param).Tables(0)
        Catch ex As Exception

        End Try

    End Function

    Public Function empleadosCargaCabecera(ByVal pnArchivo As String, ByVal pCantidadRegistros As Int16)
        Try
            Dim param(2) As Object
            param(0) = pnArchivo.Trim
            param(1) = Int32.Parse(HttpContext.Current.Session("nrocuenta"))
            param(2) = pCantidadRegistros
            OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.empleadoscargacabecera", param)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function empleadosCargaDetalle(ByVal pIdarchivo As Int32, ByVal pLista As ArrayList)
        Try
            Dim param(15) As Object
            Dim vIdDetalle As Integer = 1

            For Each l In pLista
                param(0) = l(0).ToString()
                param(1) = l(1).ToString()
                param(2) = l(2).ToString()
                param(3) = l(3).ToString()
                param(4) = l(4).ToString()
                param(5) = l(5).ToString()
                param(6) = l(6).ToString().ToUpper.Replace("Í", "I")
                param(7) = l(7).ToString().PadLeft(8, "0")
                param(8) = l(8).ToString()
                param(9) = l(9).ToString()
                param(10) = l(10).ToString()
                param(11) = l(11).ToString()
                param(12) = pIdarchivo
                param(13) = vIdDetalle
                param(14) = HttpContext.Current.Session("nrocuenta")
                param(15) = HttpContext.Current.Session("cuit")

                OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.empleadoscargadetalle", param)
                vIdDetalle = vIdDetalle + 1

            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function empleadosCargaDetalleBaja(ByVal pIdarchivo As Int32, ByVal pLista As ArrayList)
        Try
            Dim param(3) As Object
            Dim vIdDetalle As Integer = 1

            For Each l In pLista
                param(0) = l(0).ToString
                param(1) = l(1).ToString
                param(2) = l(2).ToString
                param(3) = vIdDetalle

                OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.empleadoscargadetallebaja", param)
                vIdDetalle = vIdDetalle + 1
            Next
        Catch ex As Exception

        End Try

    End Function

    Public Function startProcesoCargaMasiva(ByVal pcuit As String, ByVal pnrocuenta As Integer) As Integer
        Dim param(1) As Object
        Try
            param(0) = pcuit
            param(1) = pnrocuenta

            Return OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.empleadoscargamasivaproc", param)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function startProcesoBajaMasiva(ByVal pnrocuenta As Int32)
        Try
            Dim param(0) As Object
            param(0) = pnrocuenta

            Return OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.empleadosbajamasivaproc", param)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function retornaResultadoCM() As DataTable
        Try
            Dim param(0)
            param(0) = ""
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.empleadosresultcm", param).Tables(0)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function aseguradorasrt_sel() As DataTable
        Dim param(1) As Object

        Try
            param(0) = 0
            param(1) = ""
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.aseguradorasrt_sel", param).Tables(0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function niveleseducativos_sel() As DataTable
        Dim param(1) As Object
        Dim sqlStr As New StringBuilder
        Try
            sqlStr.Append(" select id_caracteristica idnivel")
            sqlStr.Append(" ,decode(n_caracteristica,'NO APLICABLE','UNIVERSITARIO COMPLETO','SIN DESCRIPCION','SIN DATOS', n_caracteristica) nivel")
            sqlStr.Append(" from rcivil.vt_caracteristicas")
            sqlStr.Append(" where id_tipo_caracteristica = '2004'")
            sqlStr.Append(" and id_caracteristica in")
            sqlStr.Append(" ('00', '01', '02', '03', '04', '05', '06', '07', '08', '09', '10','38')")

            Return OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlStr.ToString).Tables(0)
            'param(0) = 0
            'param(1) = ""
            'Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.niveleseducativos_sel", param).Tables(0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Function nacionalidades_sel() As DataTable
        Try
            Dim strSql As New StringBuilder

            strSql.Append("select * from dom_manager.dom_paises")
            Return OracleHelper.ExecuteDataset(cad1, CommandType.Text, strSql.ToString).Tables(0)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function tiposdocumento_sel() As DataTable
        Try
            Dim strSql As New StringBuilder
            strSql.Append(" select id_tipo_documento")
            strSql.Append("       ,n_tipo_documento")
            strSql.Append(" from rcivil.vt_tipos_documento ")
            strSql.Append(" where id_organismo_emisor='RNP'")

            Return OracleHelper.ExecuteDataset(cad1, CommandType.Text, strSql.ToString).Tables(0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function motivosEgreso_sel() As DataTable
        Try
            Dim strSql As New StringBuilder
            strSql.Append(" select idmotivoegreso")
            strSql.Append("       ,moe.nmotivoegreso")
            strSql.Append(" from doc_laboral.motivos_egreso moe")

            Return OracleHelper.ExecuteDataset(cad1, CommandType.Text, strSql.ToString).Tables(0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Function
    Public Function consultaGeneral(ByVal pnrocuenta As Integer, _
                                    ByVal pcuil As String, _
                                    ByVal papellido As String, _
                                    ByVal pactivos As Integer, _
                                    ByVal pinactivos As Integer, _
                                    ByVal pperActivos As Integer, _
                                    ByVal pperInactivos As Integer, _
                                    ByVal pfechadesdeact As DateTime, _
                                    ByVal pfechahastaact As DateTime, _
                                    ByVal pfechadesdeinact As DateTime, _
                                    ByVal pfechahastainact As DateTime, _
                                    ByVal pidordenamiento As Integer) As DataTable
        Try
            Dim param(13) As Object

            param(0) = pnrocuenta
            param(1) = pcuil
            param(2) = papellido
            param(3) = pactivos
            param(4) = pinactivos
            param(5) = pperActivos
            param(6) = pperInactivos
            param(7) = IIf(pfechadesdeact = DateTime.MinValue, DBNull.Value, pfechadesdeact)
            param(8) = IIf(pfechahastaact = DateTime.MinValue, DBNull.Value, pfechahastaact)
            param(9) = IIf(pfechadesdeinact = DateTime.MinValue, DBNull.Value, pfechadesdeinact)
            param(10) = IIf(pfechahastainact = DateTime.MinValue, DBNull.Value, pfechahastainact)
            param(11) = "00"
            param(12) = pidordenamiento
            param(13) = ""

            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.empleadosconsulta", param).Tables(0)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function reporteErroresCM() As DataTable
        Try
            Dim param(0) As Object
            param(0) = ""

            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.empleadoscmerrores_sel", param).Tables(0)
        Catch ex As Exception

        End Try

    End Function

    Public Function ordenamientoSel() As DataTable
        Try
            Dim param(0) As Object
            param(0) = ""
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.empleadosordenameinto_sel", param).Tables(0)
        Catch ex As Exception

        End Try

    End Function

    Public Function EmpleadosSujetosVerSel(ByVal pnrocuenta As Integer) As DataTable
        Try
            Dim param(1) As Object
            param(0) = pnrocuenta
            param(1) = ""
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.p_empleadossujetover_sel", param).Tables(0)
        Catch ex As Exception

        End Try

    End Function

    Public Function EstaEnEmpleadosTEMP(ByVal pcuil As String) As Integer

        Dim rta As Integer
        Dim conexion As New OracleConnection(cad1)
        Dim comando As New OracleCommand()


        Try
            comando.CommandType = CommandType.StoredProcedure
            comando.Connection = conexion
            comando.CommandText = "doc_laboral.f_esempleadotem"
            conexion.Open()


            Dim v_cuil As OracleParameter = New OracleParameter("pcuil", System.Data.OracleClient.OracleType.VarChar)
            v_cuil.Value = pcuil
            comando.Parameters.Add(v_cuil)

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

    Public Function InsertarPersonasTEMP(ByVal pidsexo As String, _
                                         ByVal pnrodocumento As String, _
                                         ByVal ppaicodpais As String, _
                                         ByVal pidnumero As Integer, _
                                         ByVal papellido As String, _
                                         ByVal pnombres As String, _
                                         ByVal pfechanac As DateTime, _
                                         ByVal pcuil As String, _
                                         ByVal pidtipodocumento As String, _
                                         ByVal pobservacion As String _
                                         ) As String
        Dim rta As String = String.Empty

        Dim param(9) As Object
        Try
            param(0) = pidsexo
            param(1) = pnrodocumento
            param(2) = ppaicodpais
            param(3) = pidnumero
            param(4) = papellido
            param(5) = pnombres
            param(6) = pfechanac
            param(7) = pcuil
            param(8) = pidtipodocumento
            param(9) = pobservacion

            OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.p_personastemp_insert", param)
            rta = "OK"
        Catch ex As Exception
            rta = ex.Message
            Throw New Exception(ex.Message)
        End Try
        Return rta
    End Function

    Public Function Insertarempleadostemp(ByVal pidsexo As String, _
                                     ByVal pnrodocumento As String, _
                                     ByVal ppaicodpais As String, _
                                     ByVal pidnumero As Integer, _
                                     ByVal papellido As String, _
                                     ByVal pnombres As String, _
                                     ByVal pfechanac As DateTime, _
                                     ByVal pcuil As String, _
                                     ByVal pidtipodocumento As String, _
                                     ByVal pobservacion As String, _
                                     ByVal pnrocuenta As Int32, _
                                     ByVal porigendato As String _
                                     ) As String
        Dim rta As String = String.Empty

        Dim param(12) As Object
        Try
            param(0) = pidsexo
            param(1) = pnrodocumento
            param(2) = ppaicodpais
            param(3) = pidnumero
            param(4) = papellido
            param(5) = pnombres
            param(6) = pfechanac
            param(7) = pcuil
            param(8) = pidtipodocumento
            param(9) = DBNull.Value  'pobservacion observacion insert pack rcivil
            param(10) = pnrocuenta
            param(11) = porigendato
            param(12) = DBNull.Value
            OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.empleadostemp_ins", param)
            rta = "OK"
        Catch ex As Exception
            rta = ex.Message
            Throw New Exception(ex.Message)
        End Try
        Return rta
    End Function


    Public Function BuscarCuilPersona(ByVal pnrodocumento As String, ByVal pidsexo As String, ByVal pidnumero As Integer) As DataTable
        Dim param(3) As Object
        Try
            param(0) = pnrodocumento
            param(1) = pidsexo
            param(2) = pidnumero
            param(3) = ""
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.P_BuscarCuilPersona", param).Tables(0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function


    Public Function validaHistorial(ByVal pEmpleados As clsEmpleados) As DataTable
        Dim param(3) As Object
        Try
            param(0) = pEmpleados.nrocuenta
            param(1) = pEmpleados.id_empleado
            param(2) = pEmpleados.fechaingreso.ToString().Substring(0, 10)
            param(3) = ""
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.empleados_validahist", param).Tables(0)
        Catch ex As Exception

        End Try
    End Function

    Public Function validaHistorialcargamasiva(ByVal pEmpleados As clsEmpleados) As DataTable
        Dim param(3) As Object
        Try
            param(0) = pEmpleados.nrocuenta
            param(1) = pEmpleados.cuil
            param(2) = pEmpleados.fechaingreso.ToString().Substring(0, 10)
            param(3) = ""
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.empleados_validahist_cm", param).Tables(0)
        Catch ex As Exception

        End Try
    End Function
    Public Sub empleadoUpdateTarea(ByVal pIdEmpleado As Integer, ByVal pTarea As String)
        Dim param(1) As Object

        param(0) = pIdEmpleado
        param(1) = pTarea

        OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.empleadosUpdateTarea", param)

    End Sub

    Public Function empleadoVerificarSinCargo(ByVal pNrocuenta As Integer, ByVal pIdSuc As String) As Boolean
        Dim param(2) As Object
        param(0) = pNrocuenta
        param(1) = pIdSuc
        param(2) = ""

        Dim dt As New DataTable

        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.empleadosVerificarSinCargo", param).Tables(0)

        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function motrarsucursalesactivas() As DataTable
        Dim param(1) As Object
        Dim tSucursales As DataTable

        Try
            param(0) = nrocuenta
            param(1) = ""
            tSucursales = OracleHelper.ExecuteDataset(cad1, "doc_laboral.sucursales_sel", param).Tables(0)
            Return tSucursales
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'param(0) = nro_documento
    'param(1) = id_sexo
    'param(2) = pai_cod_pais_origen
    'param(3) = id_numero
    'param(4) = fechaingreso
    'param(5) = IIf(fechaegreso = DateTime.MinValue, DBNull.Value, fechaegreso)
    'param(6) = idmotivo
    'param(7) = idsindicato
    'param(8) = idconvenio
    'param(9) = idsocial
    'param(10) = idcontrato
    'param(11) = cuit
    'param(12) = idsucursal
    'param(13) = nrocuenta
    'param(14) = id_empleado
    'param(15) = personascargo
    'param(16) = idnivel
    'param(17) = idaseguradora
    'param(18) = convenioafip
    'param(19) = fechaingresoant
End Class


