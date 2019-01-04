Imports System.Data
Imports System.Data.OracleClient
Imports CapaDatos
Imports Microsoft.VisualBasic

Public Class clsAntecedentes
    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

#Region "TiposAntecedentes"

    Public Function BuscatTiposAntecedentes() As DataTable
        Dim dt As DataTable
        Dim ssql As String = "select Id_tipoantecedente, Nombre from Antecedentes_tipos order by Nombre"
        dt = OracleHelper.ExecuteDataset(Cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function BuscatTiposAntecedentes(ByVal Nom As String) As DataTable
        Dim dt As DataTable
        Dim ssql As String
        ssql = "select Id_tipoantecedente, Nombre from Antecedentes_tipos Where Nombre = '" & Nom & "'"
        dt = OracleHelper.ExecuteDataset(Cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function BuscatTiposAntecedentesNOTIN() As DataTable
        Dim dt As DataTable
        Dim ssql As String
        ssql = "select Id_tipoantecedente, Nombre from antecedentes_tipos where id_tipoantecedente not in (12,13,14,15,16)"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Sub InsertarTipoAntecedentes(ByVal pnombre As String)
        Dim sqlInsert As String
        sqlInsert = "INSERT INTO Antecedentes_tipos (id_tipoantecedente,Nombre) Values " _
                  & " (sec_antecedentestipo.nextval, '" & pnombre.ToUpper.Trim & "') "
        OracleHelper.ExecuteNonQuery(Cad1, CommandType.Text, sqlInsert)
    End Sub

    Public Sub EliminarTipoAntecedentes(ByVal pnombre As String)
        Dim ssql As String
        ssql = "DELETE FROM Antecedentes_tipos WHERE Nombre = '" & pnombre.Trim & "'"
        OracleHelper.ExecuteNonQuery(Cad1, CommandType.Text, ssql)
    End Sub

    Public Sub ModificarTipoAntecedentes(ByVal pnombre As String, ByVal pid As String)
        Dim sqlUpdate As String
        sqlUpdate = "UPDATE Antecedentes_tipos SET Nombre = '" & pnombre.ToUpper.Trim & "' where Id_tipoantecedente = trim('" & pid.Trim & "') "
        OracleHelper.ExecuteNonQuery(Cad1, CommandType.Text, sqlUpdate)
    End Sub
#End Region

#Region "FormatosAntecedentes"

    Public Function BuscarFormatos() As DataTable
        Dim ssql As String
        Dim dt As DataTable
        ssql = "select Id_formato, Nombre, substr(formato,1,30) || '...' formato from Antecedentes_formatos order by Nombre"
        dt = OracleHelper.ExecuteDataset(Cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function
    Public Function BuscarFormatos(ByVal Nom As String) As DataTable
        Dim ssql As String
        Dim dt As DataTable
        ssql = "select Id_formato, Nombre, formato from Antecedentes_formatos where Nombre = '" & Nom & "' order by Nombre"
        dt = OracleHelper.ExecuteDataset(Cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Sub InsertarFormatos(ByVal pnombre As String, ByVal pformato As String)
        Dim sqlInsert As String
        sqlInsert = "INSERT INTO Antecedentes_Formatos (id_formato, Nombre, Formato) Values " _
        & " (sec_antecedentesformato.nextval,'" & pnombre.ToUpper.Trim & "', '" & pformato.ToUpper.Trim & "') "
        OracleHelper.ExecuteNonQuery(Cad1, CommandType.Text, sqlInsert)
    End Sub

    Public Sub EliminarFormatos(ByVal pnombre As String)
        Dim sqlDelete As String
        sqlDelete = "DELETE FROM Antecedentes_Formatos WHERE Nombre = '" & pnombre.Trim & "'"
        OracleHelper.ExecuteNonQuery(Cad1, CommandType.Text, sqlDelete)
    End Sub

    Public Sub ModificaFormatos(ByVal pnombre As String, ByVal pformato As String, ByVal pid As String)
        Dim sqlUpdate As String
        sqlUpdate = "UPDATE Antecedentes_Formatos SET Nombre = '" & pnombre.ToUpper.Trim & "', Formato = '" & pformato.Trim & "'  where Id_formato = '" & pid.Trim & "' "
        OracleHelper.ExecuteNonQuery(Cad1, CommandType.Text, sqlUpdate)
    End Sub
#End Region

#Region "Antecedentes"

    Public Function BuscarAntecedentes(ByVal pcuit As String) As DataTable
        Dim ssql As String
        Dim dt As DataTable
        ssql = "select A.Id_antecedentes id, A.cuit, to_char(A.fecha,'DD/MM/YYYY') fecha, A.usuario, AT.Nombre Texto " _
             & "from Antecedentes A " _
             & "inner join antecedentes_tipos AT on A.id_tipoAntecedente = AT.id_tipoantecedente " _
             & "where cuit = '" & pcuit.Trim & "' order by A.id_antecedentes DESC"
        dt = OracleHelper.ExecuteDataset(Cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function BuscarAntecedentesPorCuitUsuario(ByVal pcuit As String, ByVal pusuario As String) As DataTable
        Dim dt As DataTable
        Dim ssql As String
        ssql = "select A.id_antecedentes id, A.cuit, to_char(A.fecha,'DD/MM/YYYY')  fecha, U.usuario, AT.nombre Texto from antecedentes A " _
                         & "inner join Antecedentes_Tipos AT on A.id_tipoantecedente = AT.id_tipoantecedente " _
                         & "left join usuarios U on A.idusuario = U.idusuario " _
                         & "where a.cuit = '" & pcuit.Trim & "' and u.usuario = '" & pusuario.Trim & "'"
        dt = OracleHelper.ExecuteDataset(Cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function BuscarAntecedentesPorCuitAnte(ByVal pcuit As String, ByVal pantecedente As String) As DataTable
        Dim dt As New DataTable
        Dim ssql As String
        ssql = "select A.id_antecedentes id, A.cuit, to_char(A.fecha,'DD/MM/YYYY') fecha, A.usuario, AT.Nombre Texto  from antecedentes A " _
                & "inner join Antecedentes_Tipos AT on A.id_tipoantecedente = AT.id_tipoantecedente " _
                & "left join usuarios U on A.idusuario = U.idusuario " _
                & "where a.cuit = '" & pcuit.Trim & "' and AT.Nombre = '" & pantecedente.ToUpper.Trim & "'"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function BuscarAntecedentesPorCuitUsusAnte(ByVal pcuit As String, ByVal pantecedente As String, ByVal pusuario As String) As DataTable
        Dim dt As DataTable
        Dim ssql As String
        ssql = "select A.id_antecedentes id, A.cuit, to_char(A.fecha,'DD/MM/YYYY') fecha, A.usuario, AT.Nombre Texto from antecedentes A " _
                 & "inner join Antecedentes_Tipos AT on A.id_tipoantecedente = AT.id_tipoantecedente " _
                 & "left join usuarios U on A.usuario = U.usuario " _
                 & "where a.cuit = '" & pcuit.Trim & "' and u.usuario = '" & pusuario.Trim & "' and AT.Nombre = '" & pantecedente.Trim & "' "
        dt = OracleHelper.ExecuteDataset(Cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Sub InsertarAntecedentes(ByVal pcuit As String, ByVal pusuario As String, ByVal pformato As String, ByVal pidtipo As integer, ByVal pidformato As integer)
        Dim sqlInsert As String
        sqlInsert = "INSERT INTO Antecedentes (id_antecedentes, Cuit, Fecha, usuario, Texto, id_tipoAntecedente, id_formato) Values " _
                      & " (sec_antecedentesid.nextval,'" & pcuit.Trim & "', sysdate, '" & pusuario & "', '" & pformato & "', " _
                      & " " & pidtipo & ", " & pidformato & ") "
        OracleHelper.ExecuteNonQuery(Cad1, CommandType.Text, sqlInsert)
    End Sub

    Public Sub LoguearAntecedentes(ByVal pcuit As String, ByVal pusuario As String, ByVal ptexto As String, ByVal pidtipoante As Integer, ByVal pidformato As Integer)
        Dim ssql As String
        ssql = "INSERT INTO antecedentes (id_antecedentes, Cuit,Fecha,usuario,Texto,id_tipoAntecedente,id_formato)" _
             & "VALUES(sec_antecedentesid.nextval, " & pcuit.Trim & ",sysdate, '" & pusuario & "','" & ptexto & "'," & pidtipoante & "," & pidformato & ")"
        OracleHelper.ExecuteNonQuery(Cad1, CommandType.Text, ssql)
    End Sub

    Public Function BuscarAntecedentesPorUsuario(ByVal pusuario As String) As DataTable
        Dim dt As DataTable
        Dim ssql As String
        ssql = "select A.id_antecedentes id, A.cuit, to_char(A.fecha,'DD/MM/YYYY')  fecha, U.usuario, AT.nombre Texto, (trim(u.nombre) || ' ' || trim(u.apellido)) Nombreusuario from antecedentes A " _
                         & "inner join Antecedentes_Tipos AT on A.id_tipoantecedente = AT.id_tipoantecedente " _
                         & "inner join usuarios U on trim(A.usuario) = trim(U.usuario)  " _
                         & "where  u.usuario = '" & pusuario.Trim & "' order by id desc"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

#End Region


End Class
