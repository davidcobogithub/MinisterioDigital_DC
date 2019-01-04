Imports CapaDatos
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsEmpadro
    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Public Function BuscarEstados() As DataTable
        Dim dt As New DataTable
        Dim ssql As String
        ssql = "Select idestado, estado From Estado Order By idestado ASC"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function BuscarLugarIdentifica() As DataTable
        Dim dt As New DataTable
        Dim ssql As String
        ssql = "Select idlugaridenti, lugaridentificacion From LugarIdentificacion Order By lugaridentificacion ASC"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function LlenaCboGrillaN() As DataTable
        Dim dt As New DataTable
        Dim ssql As String
        ssql = "Select id_catdocxempresa, Documento From CatDocxEmpresa Order By id_catdocxempresa ASC"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function LlenaCboGrillaOE() As DataTable
        Dim dt As New DataTable
        Dim ssql As String
        ssql = "Select idestado, Estado From Estado where idestado in (1,4,5,6,7,8,9,13)"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function LlenaCboGrillaD(ByVal pcuil As String) As DataTable
        Dim dt As New DataTable
        Dim ssql As String
        ssql = "select nrocuenta, nvl(D.Documento,'D1') Documento from empresas E " _
            & "left join CatDocxEmpresa D on E.Id_catdocxempresa = D.Id_catdocxempresa" _
            & " Where cuit = trim('" & pcuil & "')"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function ArmarSelect(ByVal pjoin As String, ByVal pwhere As String) As String
        Dim sql As String
        Dim sqlop As String

        sql = "select pj.Razon_social razon, Empresas.CUIT, nvl(Estado.Estado, 'Pendiente') Estado, turnosmatriz.ventanilla, to_char(empresas.fecalta, 'dd/mm/yyyy') Fecalta, decode(Acuerdo, '1', 'SI', '0', 'NO') Acuerdo, li.Lugaridentificacion LugarIdenti from empresas " 
        sqlop = pjoin & " WHERE " & pwhere & ""
        sql = (sql & sqlop & "  order by empresas.nrocuenta desc")

        Return sql
    End Function


    Public Function OpcionesJOIN() As String
        Dim strJOIN As String = ""
        strJOIN = "left join turnos on empresas.nrocuenta = turnos.nrocuenta " _
                & "left join turnosmatriz on turnos.idturnomatriz = turnosmatriz.idturnomatriz " _
                & "inner join estado on estado.idestado = empresas.idestado " _
                & "inner join empresasapoderados on empresas.nrocuenta =  empresasapoderados.nrocuenta " _
                & "inner join lugaridentificacion li on empresasapoderados.idlugaridenti = li.idlugaridenti " _
                & "inner join t_comunes.t_pers_juridica pj on empresas.cuit = pj.cuit "
        Return strJOIN
    End Function

    Public Function OpcionesWHERE(Optional ByVal chkfecha As Boolean = False, Optional ByVal Chkventanilla As Boolean = False, Optional ByVal Chkestado As Boolean = True, Optional ByVal ChkLugarIdenti As Boolean = False, _
                                  Optional ByVal TxtFecDesde As String = "", Optional ByVal TxtFecHasta As String = "", Optional ByVal CboVentDesde As String = "", Optional ByVal CboVentHasta As String = "", _
                                  Optional ByVal CboEstadoDesde As String = "", Optional ByVal CboEstadoHasta As String = "", Optional ByVal CboLugarIdenti As String = "", _
                                  Optional ByVal TxtCuit As String = "", Optional ByVal Txtdenom As String = "", Optional ByVal Chkcuit As Boolean = False, Optional ByVal Chkdenom As Boolean = False) As String

        Dim strWHERE As String = ""

        If chkfecha = True And Chkventanilla = False And Chkestado = False And ChkLugarIdenti = False Then
            strWHERE = "empresas.fecalta between trim(to_date(' " & TxtFecDesde.Trim & " ','dd/mm/rrrr'))  and trim(to_date(' " & TxtFecHasta.Trim & " ','dd/mm/rrrr')) and (turnos.idturno = (select max(idturno) from turnos where nrocuenta = empresas.nrocuenta)  or turnos.idturno is null  and rownum =1) "
        End If

        If chkfecha = True And Chkventanilla = True And Chkestado = False And ChkLugarIdenti = False Then
            strWHERE = "empresas.fecalta between trim(to_date(' " & TxtFecDesde.Trim & " ','dd/mm/rrrr'))  and trim(to_date('" & TxtFecHasta.Trim & "','dd/mm/rrrr'))AND" _
                & " turnosmatriz.ventanilla between trim('" & CboVentDesde.Trim & "') and trim('" & CboVentHasta.Trim & "') and (turnos.idturno = (select max(idturno) from turnos where nrocuenta = empresas.nrocuenta) or turnos.idturno is null and rownum =1) "
        End If

        If chkfecha = True And Chkventanilla = True And Chkestado = True And ChkLugarIdenti = False Then
            strWHERE = "empresas.fecalta between trim(to_date('" & TxtFecDesde.Trim & "','dd/mm/rrrr'))  and trim(to_date('" & TxtFecHasta.Trim & "','dd/mm/rrrr')) AND" _
                            & " turnosmatriz.ventanilla between trim('" & CboVentDesde.Trim & "') and trim('" & CboVentHasta.Trim & "') AND" _
                            & " Empresas.idEstado between trim('" & CboEstadoDesde & "') and  trim('" & CboEstadoHasta & "')  and (turnos.idturno = (select max(idturno) from turnos where nrocuenta = empresas.nrocuenta) or turnos.idturno is null and rownum =1)"
        End If

        If chkfecha = False And Chkventanilla = False And Chkestado = False And ChkLugarIdenti = True Then
            strWHERE = "li.LugarIdentificacion like '%" & CboLugarIdenti.Trim & "%'"
        End If

        If chkfecha = False And Chkventanilla = False And Chkestado = True And ChkLugarIdenti = True Then
            strWHERE = "Empresas.idEstado between trim('" & CboEstadoDesde & "') and  trim('" & CboEstadoHasta & "') AND" _
                     & " li.LugarIdentificacion like '%" & CboLugarIdenti.Trim & "%'"
        End If

        If chkfecha = False And Chkventanilla = True And Chkestado = True And ChkLugarIdenti = True Then
            strWHERE = "turnosmatriz.ventanilla between trim('" & CboVentDesde & "') and trim('" & CboVentHasta.Trim & "') AND" _
                    & " Empresas.idEstado between trim('" & CboEstadoDesde & "') and trim('" & CboEstadoHasta & "') AND" _
                    & " li.LugarIdentificacion like '%" & CboLugarIdenti.Trim & "%'"
        End If

        If chkfecha = True And Chkventanilla = False And Chkestado = False And ChkLugarIdenti = True Then
            strWHERE = "empresas.fecalta between trim(to_date('" & TxtFecDesde.Trim & "','dd/mm/rrrr'))  and trim(to_date('" & TxtFecHasta.Trim & "','dd/mm/rrrr')) AND" _
                     & " li.LugarIdentificacion like '%" & CboLugarIdenti.Trim & "%'"
        End If

        If chkfecha = False And Chkventanilla = True And Chkestado = True And ChkLugarIdenti = False Then
            strWHERE = "turnosmatriz.ventanilla between trim('" & CboVentDesde & "') and trim('" & CboVentHasta.Trim & "') AND" _
                    & " Empresas.idEstado between trim('" & CboEstadoDesde & "') and  trim('" & CboEstadoHasta & "')  and (turnos.idturno = (select max(idturno) from turnos where nrocuenta = empresas.nrocuenta) or turnos.idturno is null and rownum =1) "
        End If

        If chkfecha = True And Chkventanilla = True And Chkestado = True And ChkLugarIdenti = True Then
            strWHERE = "empresas.fecalta between trim(to_date('" & TxtFecDesde.Trim & "','dd/mm/rrrr')) and trim(to_date('" & TxtFecHasta.Trim & "','dd/mm/rrrr')) AND" _
                     & " turnosmatriz.ventanilla between trim('" & CboVentDesde & "') and trim('" & CboVentHasta.Trim & "') AND" _
                     & " Empresas.idEstado between trim('" & CboEstadoDesde & "') and  trim('" & CboEstadoHasta & "') AND" _
                     & " li.LugarIdentificacion like '%" & CboLugarIdenti.Trim & "%'"
        End If

        If chkfecha = True And Chkventanilla = False And Chkestado = True And ChkLugarIdenti = False Then
            strWHERE = "empresas.fecalta between trim(to_date('" & TxtFecDesde.Trim & "','dd/mm/rrrr'))  and trim(to_date('" & TxtFecHasta.Trim & "','dd/mm/rrrr')) AND" _
                     & " Empresas.idEstado between trim('" & CboEstadoDesde & "') and trim('" & CboEstadoHasta & "')  and (turnos.idturno = (select max(idturno) from turnos where nrocuenta = empresas.nrocuenta) or turnos.idturno is null and rownum =1)"
        End If

        If chkfecha = False And Chkventanilla = True And Chkestado = False And ChkLugarIdenti = True Then
            strWHERE = "turnosmatriz.ventanilla between trim('" & CboVentDesde & "') and trim('" & CboVentHasta.Trim & "') AND" _
                     & " li.LugarIdentificacion like '%" & CboLugarIdenti.Trim & "%'"
        End If

        If chkfecha = True And Chkventanilla = True And Chkestado = False And ChkLugarIdenti = True Then
            strWHERE = "empresas.fecalta between trim(to_date('" & TxtFecDesde.Trim & "','dd/mm/rrrr'))  and trim(to_date('" & TxtFecHasta.Trim & "','dd/mm/rrrr')) AND" _
                     & " turnosmatriz.ventanilla between trim('" & CboVentDesde & "') and trim('" & CboVentHasta.Trim & "') AND" _
                     & " EmpresasApoderados.LugarIdenti like '%" & CboLugarIdenti.Trim & "%'"
        End If

        If chkfecha = True And Chkventanilla = False And Chkestado = True And ChkLugarIdenti = True Then
            strWHERE = "empresas.fecalta between trim(to_date('" & TxtFecDesde.Trim & "','dd/mm/rrrr'))  and trim(to_date('" & TxtFecHasta.Trim & "','dd/mm/rrrr')) AND" _
                     & " Empresas.idEstado between trim('" & CboEstadoDesde & "') and  trim('" & CboEstadoHasta & "') AND" _
                     & " li.LugarIdentificacion like '%" & CboLugarIdenti.Trim & "%'"
        End If

        If chkfecha = False And Chkventanilla = False And Chkestado = True And ChkLugarIdenti = False Then
            strWHERE = "Empresas.idEstado between trim('" & CboEstadoDesde & "') and trim('" & CboEstadoHasta & "')  and (turnos.idturno = (select max(idturno) from turnos where nrocuenta = empresas.nrocuenta) or turnos.idturno is null and rownum =1) "
        End If

        If chkfecha = False And Chkventanilla = True And Chkestado = False And ChkLugarIdenti = False Then
            strWHERE = "turnosmatriz.ventanilla between trim('" & CboVentDesde & "') and trim('" & CboVentHasta.Trim & "')  and (turnos.idturno = (select max(idturno) from turnos where nrocuenta = empresas.nrocuenta) or turnos.idturno is null and rownum =1) "
        End If

        If Chkcuit = True Then
            strWHERE = "empresas.cuit = '" & TxtCuit & "' and (turnos.idturno = (select max(idturno) from turnos where nrocuenta = empresas.nrocuenta) or turnos.idturno is null and rownum =1) "
        End If

        If Chkdenom = True Then
            strWHERE = "pj.Razon_social like '%" & Txtdenom.ToUpper & "%' "
        End If

        Return strWHERE

    End Function

    Public Function ArmarConsulta(ByVal sqlselect As String) As DataTable
        Dim dt As New DataTable
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, sqlselect).Tables(0)
        Return dt
    End Function

    Public Function EmpadronarDatos(ByVal documento As String, ByVal clave As String, ByVal cuit As String) As String
        Dim ssql As String
        Dim nrodoc As Integer = CInt(BuscarCategoriaDoc(documento))
        ssql = "update empresas set clave_internet = '" & clave & "', idestado = 3, id_catdocxempresa = " & nrodoc & "  where cuit = ltrim(rtrim(' " & cuit & " ')) "
        Return ssql
    End Function
    Public Function EmpadronarDatosProv(ByVal documento As String, ByVal clave As String, ByVal cuit As String) As String
        Dim ssql As String
        Dim nrodoc As Integer = CInt(BuscarCategoriaDoc(documento))
        ssql = "update empresas set clave_internet = '" & clave & "', idestado = 12, id_catdocxempresa = " & nrodoc & "  where cuit = ltrim(rtrim(' " & cuit & " ')) "
        Return ssql
    End Function

    Public Function EmpadronarDatos(ByVal documento As String, ByVal cuit As String) As String
        Dim ssql As String
        Dim nrodoc As Integer = CInt(BuscarCategoriaDoc(documento))
        ssql = "update empresas set idestado = 2, id_catdocxempresa = " & nrodoc & "  where cuit = ltrim(rtrim(' " & cuit & " ')) "
        Return ssql
    End Function
    Public Function BuscarCategoriaDoc(ByVal documento As String) As Integer
        Dim ssql As String
        Dim Nrodoc As Integer
        ssql = "select id_catdocxempresa from catdocxempresa where trim(Documento) = trim(' " & documento & " ') "
        Nrodoc = OracleHelper.ExecuteScalar(cad1, CommandType.Text, ssql)
        Return Nrodoc
    End Function

    Public Sub AcuerdoUpdate(ByVal pvaracuerdo As String, ByVal pcuit As String)
        Dim ssql As String
        ssql = "UPDATE Empresas set Acuerdo = " & pvaracuerdo & " where cuit = ltrim(rtrim(' " & pcuit & " '))"
        OracleHelper.ExecuteNonQuery(cad1, CommandType.Text, ssql)
    End Sub

    Public Function BuscarMailEmpresa(ByVal pcuit As String) As DataTable
        Dim dt As DataTable
        Dim ssql As String
        Dim identidad As String = String.Empty
        ssql = "select cuit from empresas where cuit = '" & pcuit & "'"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        If dt.Rows.Count > 0 Then
            identidad = dt.Rows(0)("cuit").ToString.Trim & "00"
        End If
        ssql = "select nro_mail from  t_comunes.t_comunicaciones where id_entidad = '" & identidad & "' and id_tipo_comunicacion = 11 and id_aplicacion = 197"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function BuscarMailEstudio(ByVal pcuit As String) As DataTable
        Dim dt As DataTable
        Dim ssql As String
        Dim identidad As String = String.Empty
        ssql = "select cuit from empresas where cuit = '" & pcuit & "'"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        If dt.Rows.Count > 0 Then
            identidad = dt.Rows(0)("cuit").ToString.Trim & "00"
        End If
        ssql = "select nro_mail from  t_comunes.t_comunicaciones where id_entidad = '" & identidad & "' and id_tipo_comunicacion = 13 and id_aplicacion = 197"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

End Class
