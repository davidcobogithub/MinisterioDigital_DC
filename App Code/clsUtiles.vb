Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports System.Data
Imports CapaDatos
Imports System.Security.Cryptography
Imports System.IO
'
Imports System.Net
Imports Newtonsoft.Json

Public Class clsUtiles

    'Retorna NRO_DOCUMENTO DEL REPRESENTANTE a partir de NROCUENTA EMPRESA
    Public Shared Function getNroDocumentoRepresentante(ByVal pNroCuenta As String) As String

        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())


        Dim sqlString As String = "SELECT EA.NRO_DOCUMENTO" & _
                                    "  FROM DOC_LABORAL.EMPRESASAPODERADOS EA" & _
                                    " WHERE NROCUENTA = '" & pNroCuenta.Trim & "'" & _
                                    "   AND ROWNUM = 1"
        Dim nrodocumento As String = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)

        If nrodocumento Is Nothing Then
            nrodocumento = " "
        End If

        Return nrodocumento

    End Function

    'Retorna CUIL a partir de NRO_DOCUMENTO
    Public Shared Function getCuitFromNroDocumento(ByVal pNroDocumento As String) As String
        Try

            Dim cencr As New ClaseEncripta("1234")
            Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

            'verifica que haya uno y solo un registro para el numero de documento indicado
            Dim sqlstring2 As String = "select count(p.NRO_DOCUMENTO) FROM RCIVIL.VT_PK_PERSONA P WHERE P.NRO_DOCUMENTO = '" & pNroDocumento.Trim & "'"
            Dim cantresult As Integer = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlstring2)
            If cantresult <> 1 Then
                Return " "
            End If
            '---

            Dim sqlString As String = "select CUIL FROM RCIVIL.VT_PK_PERSONA P WHERE P.NRO_DOCUMENTO = '" & pNroDocumento.Trim & "'"

            Dim CUIL As String = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)

            If CUIL Is Nothing Then
                CUIL = " "
            End If

            Return CUIL

        Catch ex As Exception
            Return " "
        End Try


    End Function

    'OBTIENE USUARIO CIDI
    Shared Function ObtieneUsuarioCidi(ByVal pCuit As String) As Usuario
        Dim _TimeStamp As [String] = DateTime.Now.ToString("yyyyMMddHHmmssfff")
        Dim _Token As [String] = Config.ObtenerToken(_TimeStamp)

        Dim httpWebRequest = DirectCast(WebRequest.Create(Config._Url_ApiCuentaCiDi + "api/Usuario/Obtener_Usuario"), HttpWebRequest)
        httpWebRequest.ContentType = "application/json; charset=utf-8"

        Dim Entrada As New Entrada()

        Entrada.IdAplicacion = Config._Cidi_Id_Aplicacion
        Entrada.Contrasenia = Config._Cidi_Pass_Aplicacion
        Entrada.Cuil = pCuit
        Entrada.TokenValue = _Token
        Entrada.TimeStamp = _TimeStamp

        Dim rawjson As [String] = JsonConvert.SerializeObject(Entrada)
        httpWebRequest.Method = "POST"

        Using streamWriter = New StreamWriter(httpWebRequest.GetRequestStream())
            streamWriter.Write(rawjson)
            streamWriter.Flush()
            streamWriter.Close()
            Dim httpResponse = DirectCast(httpWebRequest.GetResponse(), HttpWebResponse)
            Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                Dim result = streamReader.ReadToEnd()
                Dim _Usuario As Usuario = JsonConvert.DeserializeObject(Of Usuario)(result)

                If _Usuario IsNot Nothing Then
                    If _Usuario.Respuesta.Resultado = "OK" Then
                        Return _Usuario
                    Else
                        Return _Usuario
                    End If
                Else
                    Return Nothing
                End If
            End Using
        End Using
    End Function


    Public Shared Function DomicilioPorID_VIN(ByVal pID_VIN As String) As DataTable

        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

        Dim sqlString1 As String = "select * from dom_manager.vt_domicilios_cond where id_vin= " + pID_VIN.ToString.Trim
        Dim dt As DataTable = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlString1).Tables(0)

        Return dt

    End Function


    ''' <summary>
    ''' Verifica nivel 0
    ''' </summary>
    ''' <param name="pCuit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObtenerNivelCidi(ByVal pCuit As String) As Integer

        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

        Dim sqlString1 As String = "select count(*) from gestion_ciudadanos.vt_usuarios_cidi_cruces where cuil= '" + pCuit.ToString.Trim + "'"
        Dim cantidadRegistrosEnCidi As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString1)

        If cantidadRegistrosEnCidi = 0 Then
            Return 99
        End If

        Dim sqlString2 As String = "select nivel from gestion_ciudadanos.vt_usuarios_cidi_cruces where cuil='" + pCuit.ToString.Trim + "' and rownum=1"

        Dim nivelCidi As Integer = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString2)
        Return nivelCidi

    End Function


    ''' <summary>
    ''' Devuelve la cantidad de empleados activos en una sucursal
    ''' </summary>
    ''' <param name="pNroCuenta">Numero de cuenta de la empresa</param>
    ''' <param name="pIdSucursal">Id de sucursal</param>
    ''' <returns>Cantidad de empleados</returns>
    ''' <remarks></remarks>
    Public Shared Function empleadosPorSucursal(ByVal pNroCuenta As Int32, ByVal pIdSucursal As String) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        pIdSucursal = pIdSucursal.Trim
        '
        If pIdSucursal.Length = 1 Then
            pIdSucursal = "0" + pIdSucursal
        End If
        '
        Dim sqlString As String = "select doc_laboral.f_empleados_por_sucursal(" & pNroCuenta.ToString & ",'" & pIdSucursal & "') from dual"
        Dim cantidadEmpleados As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
        Return cantidadEmpleados
    End Function


    ''' <summary>
    ''' Devuelve el cuit con guiones
    ''' </summary>
    ''' <param name="pCuit">
    ''' Cuit sin guiones
    ''' </param>
    ''' <returns>
    ''' Cuit con guiones
    ''' </returns>
    ''' <remarks>
    ''' El cuit debe ser un string de longitud 11 sin guiones
    ''' </remarks>
    Public Shared Function CuitGuiones(ByVal pCuit As String) As String
        Return pCuit.Substring(0, 2) + "-" + pCuit.Substring(2, 8) + "-" + pCuit.Substring(10, 1)
    End Function

    ''' <summary>
    ''' Meses en letra
    ''' </summary>
    ''' <param name="pNumeroMes"> Numero de mes 1-12</param>
    ''' <returns>Nombre del mes</returns>
    ''' <remarks></remarks>
    Public Shared Function mesEnLetra(ByVal pNumeroMes As Integer) As String
        Dim meses(12) As String
        meses(0) = "Enero"
        meses(1) = "Febrero"
        meses(2) = "Marzo"
        meses(3) = "Abril"
        meses(4) = "Mayo"
        meses(5) = "Junio"
        meses(6) = "Julio"
        meses(7) = "Agosto"
        meses(8) = "Septiembre"
        meses(9) = "Octubre"
        meses(10) = "Noviembre"
        meses(11) = "Diciembre"
        If pNumeroMes < 1 Or pNumeroMes > 12 Then
            Return "ERROR!!!"
        Else
            Return meses(pNumeroMes - 1)
        End If
    End Function


    ''' <summary>
    ''' Carga Combo tablas auxiliares
    ''' </summary>
    ''' <param name="pCombo">Combo a poblar</param>
    ''' <param name="pSqlString">Select SQL</param>
    ''' <param name="pDataTextFiel">Campo que se muestra en el combo</param>
    ''' <param name="pDataValueField">Campo ID</param>
    ''' <param name="pMensaje">Mensaje a mostrar en primera fila del combo</param>
    ''' <remarks></remarks>
    Public Shared Sub CargaComboGen(ByVal pCombo As DropDownList, ByVal pSqlString As String, ByVal pDataTextFiel As String, ByVal pDataValueField As String, ByVal pMensaje As String)
        Dim cencr As New ClaseEncripta("1234")
        'Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
        pCombo.Items.Clear()
        Using oConexion As New OracleConnection(cad1)
            oConexion.Open()
            Dim oCmd As New OracleClient.OracleCommand(pSqlString)
            oCmd.Connection = oConexion
            Dim oReader As OracleClient.OracleDataReader
            oReader = oCmd.ExecuteReader
            With pCombo
                .DataSource = oReader
                .DataTextField = pDataTextFiel
                .DataValueField = pDataValueField
                .DataBind()
                oReader.Close()
            End With
            If pMensaje.Trim <> "" Then
                pCombo.Items.Insert(0, New ListItem(pMensaje.Trim))
            End If
        End Using

    End Sub

    ''' <summary>
    ''' Pasa numeros a letras
    ''' </summary>
    ''' <param name="pNumero">Numero a convertir</param>
    ''' <returns>Numero expresado en letras</returns>
    ''' <remarks>Si se pasa un numero decimal lo conviente a entero</remarks>
    Public Shared Function NumeroEnLetras(ByVal pNumero As String) As String
        Dim numero As Int32 = 0
        numero = CInt(pNumero)

        Dim numstr As String = Right("000000000000000000" + Trim(Str(numero)), 18) 'numero relleno con 0 - 18 pos
        Dim enletra As String = ""      'almacena el resultado - numero en letras
        Dim terna As String = ""        'terna que contiene Cen Dec Un
        Dim unilet As String = ""       'unidad en letra
        Dim decilet As String = ""      'decena en letra
        Dim centilet As String = ""     'centena en letra
        Dim unidad As String = ""       'unidad numero
        Dim decena As String = ""       'decena numero
        Dim centena As String = ""      'centena numero
        Dim conector As String = ""     'conector (billones, millones, miles)
        Dim pos As Integer = 1          'para apuntar a las diferentes ternas (avanza 3)

        For grupo As Integer = 6 To 1 Step -1   '6 grupos de 3 digitos c/u
            terna = Mid(numstr, pos, 3)         'terna a procesar Cen Dec Un
            unidad = Right(terna, 1)            'unidad a procesar
            decena = Mid(terna, 2, 1)           'decena a procesar
            centena = Left(terna, 1)            'centena a procesar
            conector = ""                       'conector
            'unidad
            If decena <> "1" Then
                Select Case unidad
                    Case "0"
                        If numero = 0 And grupo = 1 Then unilet = "CERO " Else unilet = ""
                    Case "1"
                        If grupo = 1 Then unilet = "UNO " Else unilet = "UN "
                    Case "2"
                        unilet = "DOS "
                    Case "3"
                        unilet = "TRES "
                    Case "4"
                        unilet = "CUATRO "
                    Case "5"
                        unilet = "CINCO "
                    Case "6"
                        unilet = "SEIS "
                    Case "7"
                        unilet = "SIETE "
                    Case "8"
                        unilet = "OCHO "
                    Case "9"
                        unilet = "NUEVE "
                End Select
            Else
                unilet = ""
            End If
            'decena
            Select Case decena
                Case "0"
                    decilet = ""
                Case "1"
                    Select Case unidad
                        Case "0"
                            decilet = "DIEZ "
                        Case "1"
                            decilet = "ONCE "
                        Case "2"
                            decilet = "DOCE "
                        Case "3"
                            decilet = "TRECE "
                        Case "4"
                            decilet = "CATORCE "
                        Case "5"
                            decilet = "QUINCE "
                        Case "6"
                            decilet = "DIECISEIS "
                        Case "7"
                            decilet = "DIECISIETE "
                        Case "8"
                            decilet = "DIECIOCHO "
                        Case "9"
                            decilet = "DIECINUEVE "
                    End Select
                Case "2"
                    If unidad > "0" Then decilet = "VEINTI" Else decilet = "VEINTE "
                Case "3"
                    decilet = "TREINTA "
                Case "4"
                    decilet = "CUARENTA "
                Case "5"
                    decilet = "CINCUENTA "
                Case "6"
                    decilet = "SESENTA "
                Case "7"
                    decilet = "SETENTA "
                Case "8"
                    decilet = "OCHENTA "
                Case "9"
                    decilet = "NOVENTA "
            End Select
            If unidad <> "0" And decena > "2" Then decilet += " Y "
            'centena
            Select Case centena
                Case "0"
                    centilet = ""
                Case "1"
                    If Mid(terna, 2, 2) > "00" Then centilet = "CIENTO " Else centilet = "CIEN "
                Case "2"
                    centilet = "DOSCIENTOS "
                Case "3"
                    centilet = "TRESCIENTOS "
                Case "4"
                    centilet = "CUATROCIENTOS "
                Case "5"
                    centilet = "QUINIENTOS "
                Case "6"
                    centilet = "SEISCIENTOS "
                Case "7"
                    centilet = "SETECIENTOS "
                Case "8"
                    centilet = "OCHOCIENTOS "
                Case "9"
                    centilet = "NOVECIENTOS "
            End Select
            'conector
            Select Case grupo
                Case 6
                    If terna > "000" Then conector = "mil "
                Case 5
                    If terna > "000" Or Mid(numstr, 1, 3) > "000" Then
                        If terna = "001" Then
                            conector = "billon "
                        Else
                            conector = "billones "
                        End If
                    End If
                Case 4
                    If terna > "000" Then conector = "mil "
                Case 3
                    If terna > "000" Or Mid(numstr, 7, 3) > "000" Then
                        If terna = "001" Then
                            conector = "millon "
                        Else
                            conector = "millones "
                        End If
                    End If
                Case 2
                    If terna > "000" Then conector = "mil "
                Case 1
                    conector = ""
            End Select
            enletra += centilet + decilet + unilet + conector
            pos += 3
        Next
        NumeroEnLetras = enletra
    End Function

    ''' <summary>
    ''' Pasa Parametros Encriptados por URL
    ''' </summary>
    ''' <param name="vector"></param>
    ''' lista de parametros 
    ''' <param name="separador"></param>
    ''' separador que se va utilizar por defecto utilizamos el -
    ''' <returns></returns>
    ''' Devuelve una Cadena Cifrada con la clase en Encripta
    ''' <remarks></remarks>
    Public Shared Function CodificaParametros(ByVal vector As ArrayList, ByVal separador As Char) As String
        Dim parametro As String = String.Empty
        Dim parametroEnc As String
        Dim cencr As New ClaseEncripta("1234")

        For Each param As String In vector
            parametro = parametro & param & separador
        Next
        'Eliminamos el ultimo separador
        parametroEnc = cencr.EncryptData(parametro.Remove(parametro.Length - 1, 1))

        'Dim b64 As String = System.Web.HttpUtility.UrlEncode(parametroEnc)
        Dim b64 As String = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(parametroEnc))
        Return b64
    End Function

    ''' <summary>
    ''' Desencripta parametros por URL
    ''' </summary>
    ''' <param name="cadena"></param>
    ''' <param name="separador"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DecodificaParametros(ByVal cadena As String, ByVal separador As Char) As ArrayList
        Dim cencr As New ClaseEncripta("1234")
        Dim cadencr As String
        Dim cadDecode As String
        Dim parmat() As String

        'cadencr = System.Web.HttpUtility.UrlDecode(cadena)
        cadencr = Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(cadena))
        cadDecode = cencr.DecryptData(cadencr)
        parmat = cadDecode.Split(separador)

        Dim mivector As New ArrayList
        For Each p As String In parmat
            mivector.Add(p)
        Next
        Return mivector
    End Function

    ''' <summary>
    ''' Obtiene proximo numero de documento por tipo
    ''' </summary>
    ''' <param name="pNroCuenta"></param>
    ''' <param name="pIdCarpeta"></param>
    ''' <param name="pTipoDocumento"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Shared Function ObtenerProximoNroDocumentoPorTipo(ByVal pNroCuenta As Int32, ByVal pIdCarpeta As Int32, ByVal pTipoDocumento As String) As Int32
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

        Dim sqlString0 As String = "select count(*) from DOC_LABORAL.T_LI_ACTAS A WHERE a.nrocuenta = " & pNroCuenta.ToString & _
                                    "   AND A.ID_CARPETA = " & pIdCarpeta.ToString & _
                                    "   AND SUBSTR(A.NRO_ACTA,9,1)=  '" & pTipoDocumento.Trim & "'"

        'Dim sqlString1 As String = "SELECT TO_NUMBER(NVL(MAX(SECACTA),0)) SECACTA FROM (" & _
        '                    " SELECT SUBSTR(A.NRO_ACTA,10,1) SECACTA FROM DOC_LABORAL.T_LI_ACTAS A" & _
        '                    "  WHERE a.nrocuenta = " & pNroCuenta & _
        '                    "   AND A.ID_CARPETA =  " & pIdCarpeta & _
        '                    "   AND SUBSTR(A.NRO_ACTA,9,1)='" & pTipoDocumento & "'" & _
        '                    "   ORDER BY SUBSTR(A.NRO_ACTA,10,1)" & _
        '                    ")"

        Dim sqlString1 As String = "SELECT TO_NUMBER(NVL(MAX(SECACTA),0)) SECACTA FROM (" & _
                            " SELECT SUBSTR(A.NRO_ACTA,10,3) SECACTA FROM DOC_LABORAL.T_LI_ACTAS A" & _
                            "  WHERE a.nrocuenta = " & pNroCuenta & _
                            "   AND A.ID_CARPETA =  " & pIdCarpeta & _
                            "   AND SUBSTR(A.NRO_ACTA,9,1)='" & pTipoDocumento & "'" & _
                            "   ORDER BY SUBSTR(A.NRO_ACTA,10,3)" & _
                            ")"



        Dim proximoNumero As Int32 = 0
        Dim cantRegistros As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString0)

        If cantRegistros = 0 Then
            proximoNumero = 0
        Else
            proximoNumero = CInt(OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString1)) + 1
        End If

        Return proximoNumero

    End Function

    ''' <summary>
    ''' Obtiene Actividad Principal. Nombre de actividad o Id y Nombre de Actividad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObtieneActividadPrincipal(ByVal pNroCuenta As Int32) As String
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

        Dim sqlString As String = "select doc_laboral.f_actividades_empresa(" + pNroCuenta.ToString + ",1,2) from dual"

        Dim actividad As String = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)

        Return actividad
    End Function


    ''' <summary>
    ''' Genera Numero de Cuil desde paquete Gobierno
    ''' </summary>
    ''' <param name="pSexo"></param>
    ''' <param name="pDocumento"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function retornaCuil(ByVal pSexo As String, ByVal pDocumento As String) As String
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        Dim retorno As String
        Dim oCuil As String = ""
        Dim oMensaje As String = ""
        '
        Dim conexion As New OracleConnection(cad1)
        Dim cmd As New OracleCommand
        Try
            conexion.Open()
            cmd.CommandText = "rcivil.pack_persona.pr_genera_cuil "
            cmd.Connection = conexion
            cmd.CommandType = CommandType.StoredProcedure
            'p_sexo	String		
            'p_nro_documento	String	
            'o_cuil	String		
            'o_mensaje	String	
            cmd.Parameters.Add("p_sexo", OracleType.VarChar).Value = pSexo
            cmd.Parameters.Add("p_nro_documento", OracleType.VarChar).Value = pDocumento
            cmd.Parameters.Add("o_cuil", OracleType.VarChar, 100).Direction = ParameterDirection.Output
            cmd.Parameters.Add("o_mensaje", OracleType.VarChar, 100).Direction = ParameterDirection.Output
            '
            retorno = cmd.ExecuteNonQuery
            oCuil = cmd.Parameters("o_cuil").Value
            oMensaje = cmd.Parameters("o_mensaje").Value
            '
        Catch ex As Exception
            Throw
        Finally
            cmd.Dispose()
            conexion.Close()
            conexion.Dispose()
        End Try
        Return oCuil
    End Function

    Public Shared Function insertarCUIL(ByVal pCuil As String, ByVal pIdAplicacion As Integer, ByVal pIdSexo As String, _
                         ByVal pNroDocumento As Int32, ByVal pPais As String, ByVal pIdNumero As Int32) As String
        Dim oRetorno As String

        Dim persona As New clsPersonaRCivil
        persona.id_aplicacion = 197
        persona.id_sexo = pIdSexo
        persona.nro_documento = pNroDocumento
        'persona.pai_cod_pais_nacionalidad = pPais
        persona.pai_cod_pais_origen = pPais
        persona.id_numero = pIdNumero
        '
        oRetorno = persona.insert_persona_cuil(pCuil)

        Return oRetorno

    End Function

    ''' <summary>
    ''' Obtiene cuil agente a partir de idusuario en tabla usuarios del sistema
    ''' </summary>
    ''' <param name="pIdUsuario">idusuario</param>
    ''' <returns>cuil o cadena en blanco si no encuentra el agente a partir del idusuario</returns>
    ''' <remarks></remarks>
    Public Shared Function getCuilFromIdUsuario(ByVal pIdUsuario As Integer) As String

        Try
            Dim cencr As New ClaseEncripta("1234")
            Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

            Dim sqlString As String = "select usuario from doc_laboral.usuarios u where u.idusuario = " & pIdUsuario

            Dim strCuil As String
            strCuil = OracleHelper.ExecuteScalar(cad1, CommandType.Text, sqlString)

            Return strCuil.Trim
        Catch ex As Exception
            Return " "
        End Try

    End Function


    ''' <summary>
    ''' Obtiene apellido y nombre del agente en base al CUIL
    ''' </summary>
    ''' <param name="pCuil">CUIL del agente</param>
    ''' <returns>Apellido y Nombre del Agente</returns>
    ''' <remarks></remarks>
    Public Shared Function ObtenerNombreUsuario(ByVal pCuil As String) As String

        If pCuil Is Nothing Or pCuil.Trim.Length = 0 Then
            Return " "
        End If

        If (Not IsNumeric(pCuil) Or pCuil.Length <> 11) Then
            Return pCuil
        End If

        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

        Dim sqlString As String = "Select UPPER((ltrim(rtrim(u.apellido)) || ' ' || ltrim(rtrim(u.nombre)))) As AGENTE" & _
                                    "  FROM DOC_LABORAL.Usuarios U" & _
                                    " WHERE USUARIO = '" & pCuil.Trim & "'" & _
                                    "   AND USUARIO IS NOT NULL"
        Dim strAgente As String
        strAgente = OracleHelper.ExecuteScalar(cad1, CommandType.Text, sqlString)

        Return strAgente

    End Function



    ''' <summary>
    ''' Encripta string 
    ''' </summary>
    ''' <param name="Cadena">Texto a encriptar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Encripta(ByVal Cadena As String) As String
        'clave y vector de inicalizacion metodos de cifrado
        Dim Clave As Byte() = Encoding.ASCII.GetBytes("mini2016")
        Dim IV As Byte() = Encoding.ASCII.GetBytes("elmaspul12345678")
        '
        Dim inputBytes As Byte() = Encoding.ASCII.GetBytes(Cadena)
        Dim encripted As Byte()
        Dim cripto As New RijndaelManaged()
        Using ms As New MemoryStream(inputBytes.Length)
            Using objCryptoStream As New CryptoStream(ms, cripto.CreateEncryptor(Clave, IV), CryptoStreamMode.Write)
                objCryptoStream.Write(inputBytes, 0, inputBytes.Length)
                objCryptoStream.FlushFinalBlock()
                objCryptoStream.Close()
            End Using
            encripted = ms.ToArray()
        End Using
        Return Convert.ToBase64String(encripted)
    End Function

    ''' <summary>
    ''' DesEncripta string
    ''' </summary>
    ''' <param name="Cadena">Texto a desencriptar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Desencripta(ByVal Cadena As String) As String
        Dim Clave As Byte() = Encoding.ASCII.GetBytes("mini2016")
        Dim IV As Byte() = Encoding.ASCII.GetBytes("elmaspul12345678")
        '
        Dim inputBytes As Byte() = Convert.FromBase64String(Cadena)
        Dim resultBytes As Byte() = New Byte(inputBytes.Length - 1) {}
        Dim textoLimpio As String = [String].Empty
        Dim cripto As New RijndaelManaged()
        Using ms As New MemoryStream(inputBytes)
            Using objCryptoStream As New CryptoStream(ms, cripto.CreateDecryptor(Clave, IV), CryptoStreamMode.Read)
                Using sr As New StreamReader(objCryptoStream, True)
                    textoLimpio = sr.ReadToEnd()
                End Using
            End Using
        End Using
        Return textoLimpio
    End Function

    ''' <summary>
    ''' Quita codigos HTMl de las cadenas de texto
    ''' </summary>
    ''' <param name="Cadena"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function reemplazarCodigosHTML(ByVal Cadena As String) As String

        'Espacio
        Cadena = Cadena.Replace("&nbsp;", " ")
        Cadena = Cadena.Replace("&#160;", " ")
        Cadena = Cadena.Replace("nbsp;", " ")
        'ñ
        Cadena = Cadena.Replace("&ntilde;", "ñ")
        Cadena = Cadena.Replace("&#241;", "ñ")
        'Ñ
        Cadena = Cadena.Replace("&Ntilde;", "Ñ")
        Cadena = Cadena.Replace("&#209;", "Ñ")
        'á
        Cadena = Cadena.Replace("&aacute;", "á")
        Cadena = Cadena.Replace("&#225;", "á")
        'é
        Cadena = Cadena.Replace("&eacute;", "é")
        Cadena = Cadena.Replace("&#233;", "é")
        'í
        Cadena = Cadena.Replace("&iacute;", "í")
        Cadena = Cadena.Replace("&#237;", "í")
        'ó
        Cadena = Cadena.Replace("&oacute;", "ó")
        Cadena = Cadena.Replace("&#243;", "ó")
        'ú
        Cadena = Cadena.Replace("&uacute;", "ú")
        Cadena = Cadena.Replace("&#250;", "ú")
        '&
        Cadena = Cadena.Replace("&amp;", " ")

        Return Cadena

    End Function

    Public Shared Function nroCuentaToCuit(ByVal pNrocuenta As String) As String

        If pNrocuenta Is Nothing Or pNrocuenta.Trim.Length = 0 Or Not IsNumeric(pNrocuenta) Then
            Return "0"
        End If

        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

        Dim sqlString As String = "select e.cuit from doc_laboral.empresas e where e.nrocuenta= " & pNrocuenta.ToString.Trim

        Dim resultado As String = OracleHelper.ExecuteScalar(cad1, CommandType.Text, sqlString)

        Return resultado

    End Function

    Public Shared Function CuitToNroCuenta(ByVal pCuit As String) As String

        If pCuit Is Nothing Or pCuit.Trim.Length = 0 Or Not IsNumeric(pCuit) Then
            Return ""
        End If

        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

        Dim sqlString As String = "select e.nrocuenta from doc_laboral.empresas e where e.cuit= " & pCuit.ToString.Trim

        Dim resultado As String = OracleHelper.ExecuteScalar(cad1, CommandType.Text, sqlString)

        Return resultado

    End Function


    Public Shared Function empleadosPorEmpresa(ByVal pNroCuenta As Int32) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

        Dim sqlString As String = "select doc_laboral.f_empleados_por_empresa(" & pNroCuenta.ToString & ") from dual"
        Dim cantidadEmpleados As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
        Return cantidadEmpleados
    End Function

    ''' <summary>
    ''' EMPLEADOS SIN ESQUEMA - ASUME QUE SE VERIFICO CARGA DE SUCURSALES Y TIENE EMPLEADOS ACTIVOS
    ''' </summary>
    ''' <param name="pCuit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function PHDEmpleadosSinEsquema(ByVal pCuit As String) As DataTable

        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '

        Dim sqlStringAux As String = "select NROCUENTA, IDSUCURSAL" & _
                                        "  from doc_laboral.sucursales" & _
                                        " where nrocuenta in (select nrocuenta" & _
                                        "                       from doc_laboral.empresas" & _
                                        "                      where cuit = '" & pCuit.Trim & "')" & _
                                        "   and fechabaja is null" & _
                                        " order by idsucursal"

        Dim dtAux As DataTable = OracleHelper.ExecuteDataset(cad1, Data.CommandType.Text, sqlStringAux).Tables(0)

        If dtAux.Rows.Count = 0 Then
            Return Nothing
        End If


        Dim mNroCuenta As String = ""
        Dim mSucursales As String = ""


        For Each row As DataRow In dtAux.Rows
            mNroCuenta = CStr(row("NROCUENTA")).Trim
            mSucursales += "'" & CStr(row("IDSUCURSAL")).Trim & "',"
        Next

        mSucursales = mSucursales.TrimEnd(",")

        Dim sqlString As String = "select e.id_empleado idempleado," & _
                                    "       e.nro_documento," & _
                                    "       vee.CUIL," & _
                                    "       vee.APEYNOM," & _
                                    "       ee.id_esquema," & _
                                    "       oe.observacion," & _
                                    "       exs.idsucursal," & _
                                    "       suc.ubicacion" & _
                                    "  from doc_laboral.empleadosempresa e" & _
                                    " inner join doc_laboral.emplemprxsucursal exs" & _
                                    "    on e.id_empleado = exs.id_empleado" & _
                                    "  left join doc_laboral.vt_empleados_empresa vee" & _
                                    "    on (vee.nro_documento = e.nro_documento and vee.nrocuenta = " & mNroCuenta & ")" & _
                                    "  left join doc_laboral.t_esquemaporempleado ee" & _
                                    "    on ee.id_empleado = e.id_empleado" & _
                                    "  left join doc_laboral.t_observacionporempleado oe" & _
                                    "    on oe.id_empleado = e.id_empleado" & _
                                    "  left join doc_laboral.sucursales suc" & _
                                    "    on (suc.nrocuenta = " & mNroCuenta & _
                                    "       and suc.idsucursal = exs.idsucursal)" & _
                                    " where e.nrocuenta = " & mNroCuenta & _
                                    "   and exs.idsucursal IN (" & mSucursales & ") " & _
                                    "   and exs.nrocuenta = " & mNroCuenta & _
                                    "   and e.fec_egreso is null" & _
                                    "   and exs.fec_baja is null" & _
                                    "   and (oe.observacion is null and nvl(ee.id_esquema, 0) = 0)" & _
                                    " order by IDSUCURSAL"

        Dim dt As DataTable = OracleHelper.ExecuteDataset(cad1, Data.CommandType.Text, sqlString).Tables(0)
        Return dt
    End Function


    '
    '----------------------------------------------------------------------------------------------------------
    '----------------------------------- I N S P E C T O R   D I G I T A L ------------------------------------
    '----------------------------------------------------------------------------------------------------------
    '
    'CANTIDAD DE DOMICILIOS cargados para un NROCUENTA dado
    Public Shared Function EmpresaCantidadDeDomicilios(ByVal pNroCuenta As Int32) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        Dim sqlString As String = "SELECT COUNT(*)" & _
                                    "  FROM DOC_LABORAL.EMPRESASDOMICILIOS ED" & _
                                    " WHERE ED.NROCUENTA = " + pNroCuenta.ToString()

        Dim cantidad = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
        Return cantidad
    End Function

    'TIPOS DE DOMICILIOS EMPRESAS
    Public Shared Function EmpresasTiposDomicilios(ByVal pNroCuenta As Int32) As DataTable
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        Dim sqlString As String = "SELECT ID_TIPODOM, N_TIPODOM" & _
                                    "  FROM DOC_LABORAL.Empresasdomicilios ED" & _
                                    "  LEFT JOIN DOM_MANAGER.VT_DOMICILIOS_COND D" & _
                                    "    ON ED.ID_VIN = D.id_vin" & _
                                    "  LEFT JOIN DOC_LABORAL.EMPRESAS EE" & _
                                    "    ON ED.NROCUENTA = EE.NROCUENTA" & _
                                    " WHERE ED.NROCUENTA = " + pNroCuenta.ToString()

        Dim dt As DataTable = OracleHelper.ExecuteDataset(cad1, Data.CommandType.Text, sqlString).Tables(0)
        Return dt
    End Function

    Public Shared Function EmpresasDomiciliosCorrectos(ByVal pNroCuenta As Int32, ByVal pTipoEmpresa As String) As Boolean
        'pTipoEmpresa - "2" persona fisica - "3" persona juridica

        Dim DT As DataTable = EmpresasTiposDomicilios(pNroCuenta)
        Dim contador As Integer = 0

        Dim boolLegal, boolFiscal, boolExplotacion, boolSueldos, boolReal, boolSocial As Boolean
        '
        boolLegal = True
        boolFiscal = True
        boolExplotacion = True
        boolSueldos = True
        boolReal = True
        boolSocial = True
        '
        If pTipoEmpresa = "2" Then
            For Each row As DataRow In DT.Rows
                If row("N_TIPODOM").ToString.Trim = "LEGAL" Then
                    If boolLegal = True Then
                        contador += 1
                        boolLegal = False
                    End If
                End If
                If row("N_TIPODOM").ToString.Trim = "FISCAL" Then
                    If boolFiscal = True Then
                        contador += 1
                        boolFiscal = False
                    End If
                End If
                If row("N_TIPODOM").ToString.Trim = "EXPLOTACION" Then
                    If boolExplotacion = True Then
                        contador += 1
                        boolExplotacion = False
                    End If
                End If
                If row("N_TIPODOM").ToString.Trim = "LIBRO SUELDOS" Then
                    If boolSueldos = True Then
                        contador += 1
                        boolSueldos = False
                    End If
                End If
                If row("N_TIPODOM").ToString.Trim = "REAL" Then
                    If boolReal = True Then
                        contador += 1
                        boolReal = False
                    End If
                End If
            Next
        Else
            For Each row As DataRow In DT.Rows
                If row("N_TIPODOM").ToString.Trim = "LEGAL" Then
                    If boolLegal = True Then
                        contador += 1
                        boolLegal = False
                    End If
                End If
                If row("N_TIPODOM").ToString.Trim = "FISCAL" Then
                    If boolFiscal = True Then
                        contador += 1
                        boolFiscal = False
                    End If
                End If
                If row("N_TIPODOM").ToString.Trim = "EXPLOTACION" Then
                    If boolExplotacion = True Then
                        contador += 1
                        boolExplotacion = False
                    End If
                End If
                If row("N_TIPODOM").ToString.Trim = "LIBRO SUELDOS" Then
                    If boolSueldos = True Then
                        contador += 1
                        boolSueldos = False
                    End If
                End If
                If row("N_TIPODOM").ToString.Trim = "SOCIAL" Then
                    If boolSocial = True Then
                        contador += 1
                        boolSocial = False
                    End If
                End If
            Next
        End If

        If contador = 5 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Shared Function EmpresasDomicilioRepetidos(ByVal Pnrocuenta As Integer, ByVal ptipoEmpresa As String) As String
        'pTipoEmpresa - "2" persona fisica - "3" persona juridica

        Dim DT As DataTable = EmpresasTiposDomicilios(Pnrocuenta)

        Dim contadorLegal As Integer = 0
        Dim contadorFiscal As Integer = 0
        Dim contadorExp As Integer = 0
        Dim contadorSueldo As Integer = 0
        Dim contadorReal As Integer = 0
        Dim contadorSocial As Integer = 0


        If ptipoEmpresa = "2" Then
            For Each row As DataRow In DT.Rows
                If row("N_TIPODOM").ToString.Trim = "LEGAL" Then
                    contadorLegal += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "FISCAL" Then
                    contadorFiscal += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "EXPLOTACION" Then
                    contadorExp += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "LIBRO SUELDOS" Then
                    contadorSueldo += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "REAL" Then
                    contadorReal += 1
                End If
            Next

        End If

        If ptipoEmpresa = "3" Then
            For Each row As DataRow In DT.Rows
                If row("N_TIPODOM").ToString.Trim = "LEGAL" Then
                    contadorLegal += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "FISCAL" Then
                    contadorFiscal += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "EXPLOTACION" Then
                    contadorExp += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "LIBRO SUELDOS" Then
                    contadorSueldo += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "SOCIAL" Then
                    contadorSocial += 1
                End If
            Next
        End If


        If contadorLegal > 1 Then
            Return "LEGAL"
        End If

        If contadorExp > 1 Then
            Return "EXPLOTACION"
        End If

        If contadorFiscal > 1 Then
            Return "FISCAL"
        End If

        If contadorSueldo > 1 Then
            Return "LIBRO SUELDOS"
        End If

        If ptipoEmpresa = "2" Then
            If contadorReal > 1 Then
                Return "REAL"
            End If
        End If

        If ptipoEmpresa = "3" Then
            If contadorSocial > 1 Then
                Return "SOCIAL"
            End If
        End If
        Return "NINGUNO"
    End Function

    Public Shared Function EmpresasDomicilioFaltantes(ByVal Pnrocuenta As Integer, ByVal ptipoEmpresa As String) As String

        'pTipoEmpresa - "2" persona fisica - "3" persona juridica

        Dim DT As DataTable = EmpresasTiposDomicilios(Pnrocuenta)

        Dim contadorLegal As Integer = 0
        Dim contadorFiscal As Integer = 0
        Dim contadorExp As Integer = 0
        Dim contadorSueldo As Integer = 0
        Dim contadorReal As Integer = 0
        Dim contadorSocial As Integer = 0

        '
        If ptipoEmpresa = "2" Then
            For Each row As DataRow In DT.Rows
                If row("N_TIPODOM").ToString.Trim = "LEGAL" Then
                    contadorLegal += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "FISCAL" Then
                    contadorFiscal += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "EXPLOTACION" Then
                    contadorExp += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "LIBRO SUELDOS" Then
                    contadorSueldo += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "REAL" Then
                    contadorReal += 1
                End If
            Next

        End If

        If ptipoEmpresa = "3" Then
            For Each row As DataRow In DT.Rows
                If row("N_TIPODOM").ToString.Trim = "LEGAL" Then
                    contadorLegal += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "FISCAL" Then
                    contadorFiscal += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "EXPLOTACION" Then
                    contadorExp += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "LIBRO SUELDOS" Then
                    contadorSueldo += 1
                End If
                If row("N_TIPODOM").ToString.Trim = "SOCIAL" Then
                    contadorSocial += 1
                End If
            Next
        End If


        If contadorLegal = 0 Then
            Return "LEGAL"
        End If

        If contadorExp = 0 Then
            Return "EXPLOTACION"
        End If

        If contadorFiscal = 0 Then
            Return "FISCAL"
        End If

        If contadorSueldo = 0 Then
            Return "LIBRO SUELDOS"
        End If

        If ptipoEmpresa = "2" Then
            If contadorReal = 0 Then
                Return "REAL"
            End If
        End If

        If ptipoEmpresa = "3" Then
            If contadorSocial = 0 Then
                Return "SOCIAL"
            End If
        End If
        Return "NINGUNO"
    End Function

    Public Shared Function SucursalesCantidadActivas(ByVal pNroCuenta As Int32) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        Dim sqlString As String = "SELECT COUNT(*)" & _
                                    "  FROM DOC_LABORAL.sucursales S" & _
                                    " WHERE S.nrocuenta = " + pNroCuenta.ToString() & _
                                    "   AND S.IDSUCURSAL <> '00'" & _
                                    "   AND S.FECHABAJA IS NULL"

        Dim cantidad As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
        Return cantidad

    End Function

    Public Shared Function SucursalesCantidadActivasDeOtrasProvincias(ByVal pNroCuenta As Int32) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        Dim sqlString As String = "SELECT COUNT(*)" & _
                                    "  FROM DOC_LABORAL.sucursales s" & _
                                    "  left join dom_manager.vt_domicilios_cond d" & _
                                    "    on s.id_vin = d.id_vin" & _
                                    " where S.nrocuenta = " + pNroCuenta.ToString() & _
                                    "   AND S.IDSUCURSAL <> '00'" & _
                                    "   AND S.FECHABAJA IS NULL" & _
                                    "   AND D.id_provincia <> 'X'"

        Dim cantidad As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
        Return cantidad

    End Function

    'CANTIDAD DE REPRESENTANTES/APODERADOS para un NROCUENTA dado
    Public Shared Function EmpresaCantidadDeApoderados(ByVal pNroCuenta As Int32) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        Dim sqlString As String = "SELECT COUNT(*)" & _
                                    "  FROM DOC_LABORAL.EMPRESASAPODERADOS EA" & _
                                    " WHERE EA.NROCUENTA = " + pNroCuenta.ToString()

        Dim cantidad As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
        Return cantidad
    End Function

    'EMAIL DEL REPRESENTANTE DE LA EMPRESA
    Public Shared Function EmailRepresentanteEmpresa(ByVal pNroCuenta As Int32) As String
        '
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        Dim sqlString As String = "SELECT NVL(EMAILREPRESENTANTE,' ')" & _
                                    "  FROM (SELECT (select nro_mail" & _
                                    "                  from t_comunes.t_comunicaciones" & _
                                    "                 where id_aplicacion = 197" & _
                                    "                   and id_tipo_comunicacion = 11" & _
                                    "                   and id_entidad = ea.id_sexo || ea.nro_documento ||" & _
                                    "                       ea.pai_cod_pais || ea.id_numero" & _
                                    "                   and rownum = 1) EmailRepresentante" & _
                                    "          from doc_laboral.empresasapoderados ea" & _
                                    "         WHERE EA.NROCUENTA = " & pNroCuenta.ToString.Trim & ")"

        Dim correo As String = ""

        Dim dt As DataTable = OracleHelper.ExecuteDataset(cad1, Data.CommandType.Text, sqlString).Tables(0)

        If dt.Rows.Count > 0 Then
            correo = dt.Rows(0)(0)
        End If

        Return correo

    End Function


    'CANTIDAD DE CONTACTOS para un NROCUENTA dado
    Public Shared Function EmpresaCantidadDeContactos(ByVal pNroCuenta As Int32) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        Dim sqlString As String = "SELECT COUNT(*)" & _
                                    "  FROM DOC_LABORAL.EMPRESASCONTACTOS EC" & _
                                    " WHERE EC.NROCUENTA = " + pNroCuenta.ToString()

        Dim cantidad As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
        Return cantidad
    End Function

    'CANTIDAD DE ACTIVIDADES para un NROCUENTA dado pudiendo elegir tipo: primaria, secundaria, total
    ' pTipoActividad -->  0 cuenta todas, 1 cuenta la actividad principal, 2 cuenta la actividad secundaria 
    Public Shared Function EmpresaCantidadDeActividades(ByVal pNroCuenta As Int32, ByVal pTipoActividad As Int32) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        Dim sqlString As String = "SELECT COUNT(*)" & _
                                    "  FROM DOC_LABORAL.EMPRESASACTIVIDADES EA" & _
                                    " WHERE EA.NROCUENTA = " + pNroCuenta.ToString()

        If pTipoActividad = 1 Then
            sqlString += "   AND EA.ID_TIPOA = 1"
        End If
        If pTipoActividad = 2 Then
            sqlString += "   AND EA.ID_TIPOA = 2"
        End If

        Dim cantidad As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
        Return cantidad

    End Function

    'CANTIDAD DE ACTIVIDADES OBSOLETAS
    Public Shared Function EmpresaCantidadDeActividadesObsoletas(ByVal pNroCuenta As Int32) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        'Dim sqlString As String = "SELECT count(*)" & _
        '                            "  FROM DOC_LABORAL.EMPRESAS E" & _
        '                            "  LEFT JOIN DOC_LABORAL.EMPRESASACTIVIDADES EA" & _
        '                            "    on e.nrocuenta = ea.nrocuenta" & _
        '                            "  LEFT JOIN T_COMUNES.VT_ACTIVIDADES A" & _
        '                            "    ON EA.ID_ACTIVIDAD = A.ID_ACTIVIDAD" & _
        '                            " WHERE (A.fecha_fin_act < SYSDATE and a.fecha_fin_act is not null)" & _
        '                            "   AND E.NROCUENTA = " + pNroCuenta.ToString()

        Dim sqlString As String = "WITH act as" & _
                                    " (select *" & _
                                    "    from t_comunes.vt_actividades" & _
                                    "   WHERE (t_comunes.vt_actividades.fecha_fin_act is null))" & _
                                    " select COUNT (*)" & _
                                    "  from (select ea.id_actividad actividad, act.id_actividad actividadg" & _
                                    "          from doc_laboral.empresasactividades ea" & _
                                    "          left join act" & _
                                    "            on TO_NUMBER(ea.id_actividad) = TO_NUMBER(act.id_actividad)" & _
                                    "         where ea.nrocuenta = " & +pNroCuenta.ToString() & ")" & _
                                    " where actividadg is null"

        Dim cantidad As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
        Return cantidad

    End Function

    'CANTIDAD DE EMPLEADOS MAL CARGADO SINDICATO Y/O CONVENIO - SE TOMAN EMPLEADOS ACTIVOS
    Public Shared Function TrabajadoresCantidadMalConvenioSindicato(ByVal pNroCuenta As Int32) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        Dim sqlString As String = "SELECT COUNT(*)" & _
                                    "  FROM DOC_LABORAL.EMPLEADOSEMPRESA EE" & _
                                    " WHERE EE.NROCUENTA  = " + pNroCuenta.ToString() & _
                                    "   AND (EE.IDSINDICATO = 0 OR EE.IDCONVENIO = 0)" & _
                                    "   AND EE.FEC_EGRESO IS NULL"

        Dim cantidad As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
        Return cantidad
    End Function

    'CANTIDAD DE EMPLEADOS SIN CATEGORIA - SE TOMAN EMPLEADOS ACTIVOS
    Public Shared Function TrabajadoresCantidadSinCategoria(ByVal pNroCuenta As Int32) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        Dim sqlString As String = "SELECT COUNT(*)" & _
                                    "  FROM DOC_LABORAL.EMPLEADOSEMPRESA EE" & _
                                    " WHERE EE.NROCUENTA = " + pNroCuenta.ToString() & _
                                    "   AND EE.FEC_EGRESO IS NULL" & _
                                    "   AND (EE.CARGO IS NULL OR TRIM(EE.CARGO) = '')"

        Dim cantidad As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
        Return cantidad
    End Function

    'CANTIDAD DE TRABAJADORES ACTIVOS
    Public Shared Function TrabajadoresCantidadActivos(ByVal pNroCuenta As Int32) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        Dim sqlString As String = "SELECT COUNT(*)" & _
                                    "  FROM DOC_LABORAL.EMPLEADOSEMPRESA EE" & _
                                    " WHERE EE.NROCUENTA = " + pNroCuenta.ToString() & _
                                    "   AND EE.FEC_EGRESO IS NULL"

        Dim cantidad As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
        Return cantidad
    End Function

    'VERIFICAR SI EMPRESA TIENE ART CARGADA
    Public Shared Function EmpresasIdART(ByVal pNroCuenta As Int32) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        Dim sqlString As String = "SELECT NVL(E.IDASEGURADORA, 0) IDASEGURADORA" & _
                                    "  FROM DOC_LABORAL.EMPRESAS E" & _
                                    " WHERE E.NROCUENTA = " + pNroCuenta.ToString()

        Dim mIdAseguradora As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
        Return mIdAseguradora
    End Function


    'VERIFICA SI HA CARGADO AL MENOS TELEFONO PRINCIPAL, TELEFONO SECUNDARIO Y/O TELEFONO CELULAR
    Private Shared Function telefonosObligatoriosCargados(ByVal pCuit As String) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        '
        If pCuit.Length = 11 Then
            Dim sqlString As String = "select count(*) " & _
                                        "  from t_comunes.vt_comunicaciones" & _
                                        " where id_entidad = '" & pCuit.Trim & "00'" & _
                                        "   and TRIM(tabla_origen) = 'EMPRESAS'" & _
                                        "   and (id_tipo_comunicacion = '01' or id_tipo_comunicacion = '03' or" & _
                                        "       id_tipo_comunicacion = '07')"
            '
            Dim cantidad As Int32 = OracleHelper.ExecuteScalar(cad1, Data.CommandType.Text, sqlString)
            Return cantidad
        Else
            Return 0
        End If
    End Function


    '******************************INSPECTOR DIGITAL******************************************
    'CONTROLA LAS PRESENTACIONES Y GENERA LISTADO DE FALTANTES Y/O INCUMPLIMIENTOS
    Public Shared Function inspectorDigital(ByVal pNroCuenta As String, ByVal pCuit As String) As String
        Dim strResultado As String = ""
        strResultado += "<ul style='text-align: left'>"

        '
        '-------------------EMPADRONAMIENTO
        '


        'Cotejo del Email del Representante contra Email CIDI (En caso de estar registrado)------------------------------------------

        'Numero de documento del representante
        Dim docrepresentante As String = clsUtiles.getNroDocumentoRepresentante(pNroCuenta)
        If docrepresentante.Trim <> "" Then
            'email del representante
            Dim eMailRepresentante As String = clsUtiles.EmailRepresentanteEmpresa(pNroCuenta)
            If eMailRepresentante.Trim <> "" Then
                'cuil del representante
                Dim cuilrepresentante As String = clsUtiles.getCuitFromNroDocumento(docrepresentante)
                If cuilrepresentante.Trim <> "" Then
                    Dim usuarioCidi As Usuario = clsUtiles.ObtieneUsuarioCidi(cuilrepresentante)
                    If usuarioCidi Is Nothing Then
                        strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO - El correo electrónico del representante no es un correo electrónico de Ciudadano Digital. Se recomienda que se registre como Ciudadano Digital (CIDI) y que el correo que registre en CIDI coincida con el mail del Paso 3 del Empadronamiento (representante). </li>"
                    Else
                        If usuarioCidi.Respuesta.Resultado = "OK" Then
                            If eMailRepresentante.ToLower.Trim <> usuarioCidi.Email.ToLower.Trim Then
                                strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO - Se verifica que siendo Ciudadano Digital (CIDI) su correo electrónico de CIDI: <b>" & usuarioCidi.Email & "</b>, no coincide con el registrado en el Paso 3 del Empadronamiento (representante del empleador): <b>" & eMailRepresentante & "</b>. Los mails deben coincidir, por lo tanto debe modificar alguno de los denunciados de forma que queden iguales. </li>"
                            End If
                        Else
                            strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO - El correo electrónico del representante no es un correo electrónico de Ciudadano Digital. Se recomienda que se registre como Ciudadano Digital (CIDI) y que el correo que registre en CIDI coincida con el mail del Paso 3 del Empadronamiento (representante).  </li>"
                        End If
                    End If
                Else
                    '***************** Para los casos de inconsitencia en RCIVIL (MAS DE UN REGISTRO CON EL MISMO DOCUMENTO - NO SE DA MENSAJE DE CORREOS DIFERENTES
                    'strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO - El correo electrónico del representante no es un correo electrónico de Ciudadano Digital. Se recomienda que se registre como Ciudadano Digital (CIDI) y que el correo que registre en CIDI coincida con el mail del Paso 3 del Empadronamiento (representante).</li>"
                End If
            Else
                strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO – Se verifica que no  ha registrado correo electrónico en el Paso 3 del Empadronamiento (representante). Se recomienda que se registre como Ciudadano Digital (CIDI) y que el correo que registre en CIDI coincida con el mail del Paso 3 del Empadronamiento (representante).</li>"
            End If
        End If



        'Dim eMailRepresentante As String = clsUtiles.EmailRepresentanteEmpresa(pNroCuenta)
        'If eMailRepresentante.Trim <> "" Then
        '    strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO - <b>" & eMailRepresentante.Trim & "</b></li>"
        'Else
        '    strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO - No se ha registrado correo electronico para el representante</li>"
        'End If

        '----------------------------------------------------------------------------------------------------------------------------




        'Carga de ART
        Dim idAseguradoraART As Integer = clsUtiles.EmpresasIdART(pNroCuenta)
        If idAseguradoraART = 0 Then
            strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO - Debe cargar ART seleccionada por la empresa</li>"
        End If

        'Telefonos "principales" cargados - Verifica que haya cargado al menos telefono 1 telefono 2 o telefono celular 
        Dim CantidadDeTelefonos As Integer = clsUtiles.telefonosObligatoriosCargados(pCuit)
        If CantidadDeTelefonos = 0 Then
            strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO - Debe cargar al menos un teléfono de los siguientes: Teléfono 1, teléfono 2, Celular</li>"
        End If


        'Apoderados
        Dim CantidadDeApoderados As Integer = clsUtiles.EmpresaCantidadDeApoderados(pNroCuenta)
        If CantidadDeApoderados = 1 Then
            'strResultado += "<li style='margin-top: 5px; color:Green'>Se ha cargado correctamente 1 apoderado</li>"
        Else
            strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO - Debe cargar 1 apoderado</li>"
        End If

        'Contactos
        Dim CantidadDeContactos As Integer = clsUtiles.EmpresaCantidadDeContactos(pNroCuenta)
        If (CantidadDeContactos >= 1 And CantidadDeContactos <= 3) Then
            'strResultado += "<li style='margin-top: 5px; color:Green'>Se ha cargado correctamente entre 1 y 3 contactos</li>"
        Else
            strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO - Debe cargar entre 1 y 3 apoderados/contactos</li>"
        End If

        'Actividades
        Dim CantidadDeActividades As Integer = clsUtiles.EmpresaCantidadDeActividades(pNroCuenta, 0)
        If CantidadDeActividades = 0 Then
            strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO - No ha cargado actividades</li>"
        Else
            Dim CantidadDeActividadesPrincipal As Integer = clsUtiles.EmpresaCantidadDeActividades(pNroCuenta, 1)
            If CantidadDeActividadesPrincipal = 0 Then
                strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO - No ha cargado una actividad principal</li>"
            Else
                'strResultado += "<li style='margin-top: 5px; color:Green'>Ha cargado correctamente al menos una actividad</li>"
            End If
        End If

        'Actividades obsoletas
        Dim CantidadDeActividadesObsoletas As Integer = clsUtiles.EmpresaCantidadDeActividadesObsoletas(pNroCuenta)
        If CantidadDeActividadesObsoletas > 0 Then
            strResultado += "<li style='margin-top: 5px; color:Red'>EMPADRONAMIENTO - Algunas de las actividades registradas no se corresponden con el nomenclador actualizado de la AFIP</li>"
        End If

        '
        ' D O M I C I L I O S 
        '

        'Cantidad de domicilios
        Dim cantidadDomicilios As Integer = clsUtiles.EmpresaCantidadDeDomicilios(pNroCuenta)
        If cantidadDomicilios = 5 Then

            'strResultado += "<li style='margin-top: 5px; color:Green'>Se cargaron los 5 domicilios solicitados</li>"

            '
            'Dim domiciliosCorrectos As Boolean = clsUtiles.EmpresasDomiciliosCorrectos(pNroCuenta, Me.txtCUIT.ToString.Substring(0, 1))
            Dim domiciliosCorrectos As Boolean = clsUtiles.EmpresasDomiciliosCorrectos(pNroCuenta, pCuit.ToString.Substring(0, 1))
            If Not domiciliosCorrectos Then
                strResultado += "<li style='margin-top: 5px; color:Red'>DOMICILIOS - Los tipos de domicilios especificados no son correctos</li>"
            End If

        Else
            strResultado += "<li style='margin-top: 5px; color:Red'>DOMICILIOS - Deben cargarse 5 domicilios</li>"
        End If


        '
        ' S U C U R S A L E S
        '

        'Sucursales activas
        Dim sucursalesActivas As Integer = clsUtiles.SucursalesCantidadActivas(pNroCuenta)
        If sucursalesActivas > 0 Then
            'strResultado += "<li style='margin-top: 5px; color:Green'>Ha cargado al menos un domicilio de explotación</li>"
        Else
            strResultado += "<li style='margin-top: 5px; color:Red'>SUCURSALES - No tiene registrado ningún domicilio de explotación</li>"
        End If

        'Domicilios de otras provincias en sucursales
        Dim sucursalesActivasOtrasProvincias As Integer = clsUtiles.SucursalesCantidadActivasDeOtrasProvincias(pNroCuenta)
        If sucursalesActivasOtrasProvincias > 0 Then
            strResultado += "<li style='margin-top: 5px; color:Red'>SUCURSALES - Se han registrado domicilios de explotacion de otras provincias</li>"
        End If


        '
        ' T R A B A J A D O R E S
        '

        '----------Trabajadores activos

        Dim TrabajadoresCantidadActivos As Integer = clsUtiles.TrabajadoresCantidadActivos(pNroCuenta)
        If TrabajadoresCantidadActivos > 0 Then
            'strResultado += "<li style='margin-top: 5px; color:Green'>Ha realizado carga de trabajadores</li>"
        Else
            strResultado += "<li style='margin-top: 5px; color:Red'>TRABAJADORES - No ha registrado ningún trabajador</li>"
        End If

        '----------Trabajadores mal cargados sindicato convenio

        Dim TrabajadoresCantidadMalConvenioSindicato As Integer = clsUtiles.TrabajadoresCantidadMalConvenioSindicato(pNroCuenta)
        If TrabajadoresCantidadMalConvenioSindicato > 0 Then
            strResultado += "<li style='margin-top: 5px; color:Red'>TRABAJADORES - Para algunos de sus trabajadores registrados no se ha especificado correctamente Sindicato y/o convenio</li>"
        End If

        '----------Trabajadores sin categoria

        Dim TrabajadoresCantidadSinCategoria As Integer = clsUtiles.TrabajadoresCantidadSinCategoria(pNroCuenta)
        If TrabajadoresCantidadSinCategoria > 0 Then
            strResultado += "<li style='margin-top: 5px; color:Red'>TRABAJADORES - Para algunos de sus trabajadores registrados no se ha especificado la categoria/cargo que revisten</li>"
        End If

        strResultado += "</ul>"

        Return strResultado
    End Function


    'DETALLE DE EMPLEADOS MAL CARGADO SINDICATO Y/O CONVENIO - SE TOMAN EMPLEADOS ACTIVOS
    Public Shared Function TrabajadoresDetalleCantidadMalConvenioSindicato(ByVal pNroCuenta As Int32) As DataTable
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        Dim dt As DataTable
        'Dim sqlString As String = "SELECT * " & _
        '                            "  FROM DOC_LABORAL.EMPLEADOSEMPRESA EE" & _
        '                            " WHERE EE.NROCUENTA  = " + pNroCuenta.ToString() & _
        '                            "   AND (EE.IDSINDICATO = 0 OR EE.IDCONVENIO = 0)" & _
        '                            "   AND EE.FEC_EGRESO IS NULL"


        Dim sqlString As String = "SELECT distinct ee.fec_ingreso, ee.nro_documento, v.APEYNOM Nombre, v.CUIL   FROM DOC_LABORAL.EMPLEADOSEMPRESA EE " _
       & " inner join doc_laboral.vt_empleados_empresa v on ee.id_empleado = v.ID_EMPLEADO  and ee.nrocuenta = v.nrocuenta " _
       & " WHERE EE.NROCUENTA = " + pNroCuenta.ToString() + " And (EE.IDSINDICATO = 0 Or EE.IDCONVENIO = 0) And EE.FEC_EGRESO Is NULL And ee.id_empleado <> 0"

        dt = OracleHelper.ExecuteDataset(cad1, Data.CommandType.Text, sqlString).Tables(0)
        Return dt
    End Function




    '******************************RECLAMOS INDIVIDUALES - AUDIENCIAS**************************************




    '******************************EVENTOS************************************


    Public Shared Function EventoActividadesInscriptos(ByVal pIdEvento As Int32, ByVal pCuil As String) As DataTable

        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

        Dim sqlString As String = "select E.ID_EVENTO," & _
                                    "       E.N_EVENTO," & _
                                    "       E.N_DESCRIPCION N_DESCRIPCION_EVENTO," & _
                                    "       E.N_TEMARIO," & _
                                    "       E.N_FECHAHORARIO N_FECHAHORARIO_EVENTO," & _
                                    "       E.N_OBSERVACIONES N_OBSERVACIONES_EVENTO," & _
                                    "       E.FECHA_DESDE," & _
                                    "       E.FECHA_HASTA," & _
                                    "       A.ID_ACTIVIDAD," & _
                                    "       A.N_ACTIVIDAD," & _
                                    "       A.N_OBLIGATORIO," & _
                                    "       A.N_DESCRIPCION," & _
                                    "       A.N_LOCACION," & _
                                    "       A.N_FECHAHORARIO N_FECHAHORARIO_ACTIVIDADES," & _
                                    "       A.FECHALIMITEINSCRIPCION," & _
                                    "       A.N_CUPO," & _
                                    "       A.N_INSCRIPTOSMAXIMO," & _
                                    "       A.N_NIVELCIDI," & _
                                    "       A.N_CAPACITACIONPUBLICA," & _
                                    "       A.N_HORASRELOJ," & _
                                    "       A.N_CONEXAMEN," & _
                                    "       A.ENTREGACERTIFICADO," & _
                                    "       I.CUIL," & _
                                    "       I.ESDISERTANTE," & _
                                    "       I.OBS," & _
                                    "       I.TOKEN," & _
                                    "       I.VALIDADO," & _
                                    "       I.FECHA_REGISTRO," & _
                                    "       I.ASISTIOSN," & _
                                    "       I.APROBOSN," & _
                                    "       I.NOTA," & _
                                    "       I.FECHA_REGISTRO" & _
                                    "  from doc_laboral.t_ev_inscriptos i" & _
                                    "  left join doc_laboral.t_ev_actividades a" & _
                                    "    on i.id_actividad = a.id_actividad" & _
                                    "  left join doc_laboral.t_ev_eventos e" & _
                                    "    on a.id_evento = e.id_evento" & _
                                    " where i.cuil = '" & pCuil & "'" & _
                                    "   and a.id_evento = " & pIdEvento

        Dim dt As DataTable = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlString).Tables(0)

        Return dt


    End Function

    Public Shared Function EventosCupoXActividad(ByVal pidact As String) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        Dim sqlString As String = "select ta.n_cupo from doc_laboral.t_ev_actividades ta where ta.id_actividad = " & pidact & ""
        Dim dt As DataTable = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlString).Tables(0)
        Return CInt(dt.Rows(0)(0))
    End Function

    Public Shared Function EventosInsValidoXActividad(ByVal pidact As String) As Integer
        Dim cencr As New ClaseEncripta("1234")
        Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
        Dim sqlString As String = "select count(*) from doc_laboral.t_ev_inscriptos t " _
                                 & " inner join doc_laboral.t_ev_actividades ta on t.id_actividad = ta.id_actividad " _
                                 & " where t.validado = 1 and t.id_actividad  = " & pidact & ""
        Dim dt As DataTable = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlString).Tables(0)
        Return CInt(dt.Rows(0)(0))
    End Function




End Class
