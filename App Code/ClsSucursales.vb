Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OracleClient
Imports CapaDatos


Public Class ClsSucursales
    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Private conexion As New OracleConnection(cad1)
    Private cmd As New OracleCommand
    Public idsucursal As String
    Public ubicacion As String
    Public principal As String
    Public fechaalta As Date
    Public fechabaja As Date
    Public ordensucursal As Integer
    Public codigodelegacion As String
    Public id_tipoubicacion As Integer
    Public nrocuenta As Integer
    Public id_vin As Integer
    Public cuit As String
    Public id_sede As String
    Public calle As String
    Public altura As Integer
    Public depto As String
    Public piso As String
    Public torre As String
    Public cantidadEmpleados As Integer
    Public id_provincia As String
    Public id_departamento As Integer
    Public id_localidad As Integer
    Public id_barrio As Integer
    Public tipo_accion As String
    Public id_tipodom As Integer
    Public codigo_postal As Integer
    Public cantidad_empleados As Integer
    Public n_sede As String
    Public activas As Integer
    Public observaciones As String


    Public Function inserta_sucursal()
        Dim mensaje As String
        conexion.Open()
        Dim cmd As New OracleCommand("P_SUCURSALES_INS", conexion)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("P_IDSUCURSAL", OracleType.VarChar).Value = idsucursal
        cmd.Parameters.Add("P_UBICACION", OracleType.VarChar).Value = ubicacion
        cmd.Parameters.Add("P_PRINCIPAL", OracleType.VarChar).Value = principal
        cmd.Parameters.Add("P_FECHAALTA", OracleType.DateTime).Value = fechaalta
        cmd.Parameters.Add("P_FECHABAJA", OracleType.DateTime).Value = fechabaja
        cmd.Parameters.Add("P_ORDENSUCURSAL", OracleType.Number).Value = ordensucursal
        cmd.Parameters.Add("P_CODIGODELEGACION", OracleType.VarChar).Value = codigodelegacion
        cmd.Parameters.Add("P_ID_TIPOUBICACION", OracleType.Number).Value = id_tipoubicacion
        cmd.Parameters.Add("P_NROCUENTA", OracleType.Number).Value = nrocuenta
        cmd.Parameters.Add("P_ID_VIN", OracleType.Number).Value = id_vin
        cmd.Parameters.Add("P_CUIT", OracleType.VarChar).Value = cuit
        cmd.Parameters.Add("P_ID_SEDE", OracleType.VarChar).Value = id_sede

        cmd.ExecuteNonQuery()
        conexion.Close()

    End Function

    Public Function InsertaModifica()

        Dim mensaje As String

        conexion.Open()
        Dim cmd As New OracleCommand("DOC_LABORAL.P_SUCURSALES_INSUPD", conexion)
        cmd.CommandType = CommandType.StoredProcedure

        Try
            cmd.Parameters.Add("PIDSUCURSAL", OracleType.VarChar).Value = IIf(String.IsNullOrEmpty(idsucursal), DBNull.Value, idsucursal)
            cmd.Parameters.Add("PUBICACION", OracleType.VarChar).Value = IIf(IsNothing(ubicacion), DBNull.Value, ubicacion)
            cmd.Parameters.Add("PPRINCIPAL", OracleType.VarChar).Value = IIf(IsNothing(principal), DBNull.Value, principal)

            'Fecha de apertura
            cmd.Parameters.Add("PFECHAALTA", OracleType.DateTime).Value = IIf(IsDate(fechaalta), fechaalta, DBNull.Value)

            cmd.Parameters.Add("PFECHABAJA", OracleType.DateTime).Value = DBNull.Value
            cmd.Parameters.Add("PORDENSUCURSAL", OracleType.Number).Value = IIf(IsNothing(ordensucursal), DBNull.Value, ordensucursal)
            cmd.Parameters.Add("PCODIGODELEGACION", OracleType.VarChar).Value = IIf(IsNothing(codigodelegacion), DBNull.Value, codigodelegacion)
            cmd.Parameters.Add("PID_TIPOUBICACION", OracleType.Number).Value = IIf(IsNothing(id_tipoubicacion), DBNull.Value, id_tipoubicacion)
            cmd.Parameters.Add("PID_VIN", OracleType.Number).Value = IIf(IsNothing(id_vin), DBNull.Value, id_vin)
            cmd.Parameters.Add("PCUIT", OracleType.VarChar).Value = cuit
            cmd.Parameters.Add("PID_SEDE", OracleType.VarChar).Value = id_sede
            cmd.Parameters.Add("PTIPO_ACCION", OracleType.VarChar).Value = IIf(IsNothing(tipo_accion), DBNull.Value, tipo_accion)
            cmd.Parameters.Add("PID_LOCALIDAD", OracleType.Number).Value = id_localidad
            cmd.Parameters.Add("PID_BARRIO", OracleType.Number).Value = id_barrio
            cmd.Parameters.Add("PID_DEPARTAMENTO", OracleType.Number).Value = id_departamento
            cmd.Parameters.Add("PID_PROVINCIA", OracleType.VarChar).Value = id_provincia
            cmd.Parameters.Add("PID_TIPODOM", OracleType.Number).Value = id_tipodom
            cmd.Parameters.Add("PCALLE", OracleType.VarChar).Value = calle
            cmd.Parameters.Add("PALTURA", OracleType.Number).Value = IIf(IsNothing(altura), DBNull.Value, altura)
            cmd.Parameters.Add("PPISO", OracleType.VarChar).Value = IIf(IsNothing(piso), DBNull.Value, piso)
            cmd.Parameters.Add("PDEPTO", OracleType.VarChar).Value = IIf(IsNothing(depto), DBNull.Value, depto)
            cmd.Parameters.Add("PTORRE", OracleType.VarChar).Value = IIf(IsNothing(torre), DBNull.Value, torre)
            cmd.Parameters.Add("PCODIGO_POSTAL", OracleType.Number).Value = IIf(IsNothing(codigo_postal), DBNull.Value, codigo_postal)
            cmd.Parameters.Add("PCANT_EMPLEADOS", OracleType.Number).Value = IIf(IsNothing(cantidad_empleados), DBNull.Value, cantidad_empleados)
            cmd.Parameters.Add("PNROCUENTA", OracleType.Number).Value = nrocuenta
            cmd.Parameters.Add("PSEDE", OracleType.VarChar).Value = n_sede
            cmd.ExecuteNonQuery()
            conexion.Close()

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Function mostrarSucursales() As DataTable
        Dim param(2) As Object
        Dim tSucursales As DataTable

        Try
            param(0) = nrocuenta
            param(1) = cuit
            param(2) = ""
            tSucursales = OracleHelper.ExecuteDataset(cad1, "doc_laboral.empleadossucursales_sel", param).Tables(0)
            Return tSucursales
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function empleadosSucursales() As DataTable
        Dim param(2) As Object
        Try
            param(0) = nrocuenta
            param(1) = cuit
            param(2) = ""
            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.empleadossucursales_sel", param).Tables(0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Sub bajaSucursales()
        Dim mensaje As String
        conexion.Open()
        Dim cmd As New OracleCommand("doc_laboral.sp_sucursales_baja", conexion)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("p_nrocuenta", OracleType.Number).Value = nrocuenta
        cmd.Parameters.Add("p_idsucursal", OracleType.Number).Value = idsucursal
        cmd.Parameters.Add("p_observaciones", OracleType.VarChar).Value = observaciones
        cmd.Parameters.Add("p_fechabaja", OracleType.DateTime).Value = fechabaja
        cmd.ExecuteNonQuery()
        conexion.Close()

    End Sub


End Class
