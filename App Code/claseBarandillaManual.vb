Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OracleClient
Imports CapaDatos

Public Class claseBarandillaManual
    Dim cencr As New ClaseEncripta("1234")
    Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
    '
    Public midbarandilladocmanual As Int32
    Public mfechaalta As Date
    Public mnrocuenta As Int32
    Public midsucursal As String
    Public mano As Int16
    Public mmes As Int16
    Public mcodigodelegacion As String
    Public midtipopresentacion As Int16
    Public middefiniciondocumentos As Int16
    Public mforiginal As Byte()
    Public midestadodocmanual As Int16
    Public mfechapresentacion As Date
    Public mresponsable As Int32
    Public mstickerseguridad As String
    Public mnrolibro As Integer
    Public mnrolibroant As Integer
    Public mfechaintervencion As Date
    Public mdatosanteriores As String
    '
    Public Function recuperaTiposDocumentoBM(ByVal pTipoPresentacion As Integer) As DataTable
        Dim dt As DataTable
        Dim ssql As String = "select iddefiniciondocumento, documento from doc_laboral.definiciondocumentos where idtipopresentacion = " & Str(pTipoPresentacion)
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function recuperaTiposPresentacionBM() As DataTable
        Dim dt As DataTable
        Dim ssql As String = "select idtipopresentacion, descripcion from doc_laboral.tipopresentacion where idtipopresentacion = 1"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Function recuperaEstadosDocumentoBM() As DataTable
        Dim dt As DataTable
        Dim ssql As String = "select idestadodocmanual, descripcion from doc_laboral.estadodocmanual"
        dt = OracleHelper.ExecuteDataset(cad1, CommandType.Text, ssql).Tables(0)
        Return dt
    End Function

    Public Sub altaDocumentoManual(ByVal pCuit As String, _
                                   ByVal pUsuario As String, _
                                   ByVal pTexto As String, _
                                   ByVal pIdTipoAnt As Int32, _
                                   ByVal pIdFormato As Int32, _
                                   ByVal pRechazado As Int32)
        Dim conexion As New OracleConnection(cad1)
        conexion.Open()
        Dim cmd As New OracleCommand("Doc_laboral.SP_BARANDILLA_MANUAL_ALTA", conexion)
        cmd.CommandType = CommandType.StoredProcedure
        'cmd.Parameters.Add("v_idbarandilladocmanual", OracleType.VarChar).Value = midbarandilladocmanual
        cmd.Parameters.Add("v_fechaalta", OracleType.DateTime).Value = mfechaalta
        cmd.Parameters.Add("v_nrocuenta", OracleType.Number).Value = mnrocuenta
        cmd.Parameters.Add("v_idsucursal", OracleType.VarChar).Value = midsucursal
        cmd.Parameters.Add("v_ano", OracleType.Number).Value = mano
        cmd.Parameters.Add("v_mes", OracleType.Number).Value = mmes
        cmd.Parameters.Add("v_codigodelegacion", OracleType.VarChar).Value = mcodigodelegacion
        cmd.Parameters.Add("v_idtipopresentacion", OracleType.Number).Value = midtipopresentacion
        cmd.Parameters.Add("v_iddefiniciondocumentos", OracleType.Number).Value = middefiniciondocumentos
        'cmd.Parameters.Add("v_foriginal", OracleType.Number).Value = mforiginal
        cmd.Parameters.Add("v_idestadodocmanual", OracleType.Number).Value = midestadodocmanual
        cmd.Parameters.Add("v_fechapresentacion", OracleType.DateTime).Value = mfechapresentacion
        cmd.Parameters.Add("v_responsable", OracleType.Number).Value = mresponsable
        cmd.Parameters.Add("v_stickerseguridad", OracleType.VarChar).Value = mstickerseguridad
        '
        cmd.Parameters.Add("pCuit", OracleType.VarChar).Value = pCuit
        cmd.Parameters.Add("pUsuario", OracleType.VarChar).Value = pUsuario
        cmd.Parameters.Add("pTexto", OracleType.VarChar).Value = pTexto
        cmd.Parameters.Add("pIdTipoAnt", OracleType.VarChar).Value = pIdTipoAnt
        cmd.Parameters.Add("pIdFormato", OracleType.VarChar).Value = pIdFormato
        cmd.Parameters.Add("pRechazado", OracleType.Number).Value = pRechazado
        '
        cmd.ExecuteNonQuery()
        conexion.Close()
    End Sub

    Public Function BarandilladocManual_Insert() As Integer

        Dim res As Integer = 0
        Dim conexion As New OracleConnection(cad1)
        conexion.Open()
        Dim cmd As New OracleCommand("Doc_laboral.PR_BARANDOCMANUAL_INSERT", conexion)
        cmd.CommandType = CommandType.StoredProcedure
        'cmd.Parameters.Add("v_idbarandilladocmanual", OracleType.VarChar).Value = midbarandilladocmanual
        cmd.Parameters.Add("v_fechaalta", OracleType.DateTime).Value = mfechaalta
        cmd.Parameters.Add("v_nrocuenta", OracleType.Number).Value = mnrocuenta
        cmd.Parameters.Add("v_idsucursal", OracleType.VarChar).Value = midsucursal
        cmd.Parameters.Add("v_ano", OracleType.Number).Value = mano
        cmd.Parameters.Add("v_mes", OracleType.Number).Value = mmes
        cmd.Parameters.Add("v_codigodelegacion", OracleType.VarChar).Value = mcodigodelegacion
        cmd.Parameters.Add("v_idtipopresentacion", OracleType.Number).Value = midtipopresentacion
        cmd.Parameters.Add("v_iddefiniciondocumentos", OracleType.Number).Value = middefiniciondocumentos
        'cmd.Parameters.Add("v_foriginal", OracleType.Number).Value = mforiginal
        cmd.Parameters.Add("v_idestadodocmanual", OracleType.Number).Value = midestadodocmanual
        'cmd.Parameters.Add("v_fechapresentacion", OracleType.DateTime).Value = mfechapresentacion
        cmd.Parameters.Add("v_responsable", OracleType.Number).Value = mresponsable
        cmd.Parameters.Add("v_stickerseguridad", OracleType.VarChar).Value = mstickerseguridad

        cmd.Parameters.Add("o_id_barandilla", OracleType.Number).Direction = ParameterDirection.Output
        cmd.ExecuteNonQuery()
        res = cmd.Parameters("o_id_barandilla").Value

        conexion.Close()
        Return res
    End Function

    Public Function BarandilladocManual_Insert(ByVal conexion As OracleConnection, ByVal trx As OracleTransaction) As Integer

        Dim res As Integer = 0
        Dim cmd As New OracleCommand("Doc_laboral.PR_BARANDOCMANUAL_INSERT", conexion, trx)
        cmd.CommandType = CommandType.StoredProcedure
        'cmd.Parameters.Add("v_idbarandilladocmanual", OracleType.VarChar).Value = midbarandilladocmanual
        cmd.Parameters.Add("v_fechaalta", OracleType.DateTime).Value = mfechaalta
        cmd.Parameters.Add("v_nrocuenta", OracleType.Number).Value = mnrocuenta
        cmd.Parameters.Add("v_idsucursal", OracleType.VarChar).Value = midsucursal
        cmd.Parameters.Add("v_ano", OracleType.Number).Value = mano
        cmd.Parameters.Add("v_mes", OracleType.Number).Value = mmes
        cmd.Parameters.Add("v_codigodelegacion", OracleType.VarChar).Value = mcodigodelegacion
        cmd.Parameters.Add("v_idtipopresentacion", OracleType.Number).Value = midtipopresentacion
        cmd.Parameters.Add("v_iddefiniciondocumentos", OracleType.Number).Value = middefiniciondocumentos
        'cmd.Parameters.Add("v_foriginal", OracleType.Number).Value = mforiginal
        cmd.Parameters.Add("v_idestadodocmanual", OracleType.Number).Value = midestadodocmanual
        'cmd.Parameters.Add("v_fechapresentacion", OracleType.DateTime).Value = mfechapresentacion
        cmd.Parameters.Add("v_responsable", OracleType.Number).Value = mresponsable
        cmd.Parameters.Add("v_stickerseguridad", OracleType.VarChar).Value = mstickerseguridad

        cmd.Parameters.Add("o_id_barandilla", OracleType.Number).Direction = ParameterDirection.Output
        cmd.ExecuteNonQuery()
        res = cmd.Parameters("o_id_barandilla").Value

        Return res
    End Function

    Public Function BarandilladocManual_Insert_LI(ByVal conexion As OracleConnection, ByVal trx As OracleTransaction) As Integer

        Dim res As Integer = 0
        Dim cmd As New OracleCommand("Doc_laboral.PR_BARANDOCMANUAL_INSERT_LI", conexion, trx)
        cmd.CommandType = CommandType.StoredProcedure
        'cmd.Parameters.Add("v_idbarandilladocmanual", OracleType.VarChar).Value = midbarandilladocmanual
        cmd.Parameters.Add("v_fechaalta", OracleType.DateTime).Value = mfechaalta
        cmd.Parameters.Add("v_nrocuenta", OracleType.Number).Value = mnrocuenta
        cmd.Parameters.Add("v_idsucursal", OracleType.VarChar).Value = midsucursal
        cmd.Parameters.Add("v_ano", OracleType.Number).Value = mano
        cmd.Parameters.Add("v_mes", OracleType.Number).Value = mmes
        cmd.Parameters.Add("v_codigodelegacion", OracleType.VarChar).Value = mcodigodelegacion
        cmd.Parameters.Add("v_idtipopresentacion", OracleType.Number).Value = midtipopresentacion
        cmd.Parameters.Add("v_iddefiniciondocumentos", OracleType.Number).Value = middefiniciondocumentos
        'cmd.Parameters.Add("v_foriginal", OracleType.Number).Value = mforiginal
        cmd.Parameters.Add("v_idestadodocmanual", OracleType.Number).Value = midestadodocmanual
        'cmd.Parameters.Add("v_fechapresentacion", OracleType.DateTime).Value = mfechapresentacion
        cmd.Parameters.Add("v_responsable", OracleType.Number).Value = mresponsable
        cmd.Parameters.Add("v_stickerseguridad", OracleType.VarChar).Value = mstickerseguridad
        cmd.Parameters.Add("v_nrolibro", OracleType.Number).Value = mnrolibro

        cmd.Parameters.Add("o_id_barandilla", OracleType.Number).Direction = ParameterDirection.Output
        cmd.ExecuteNonQuery()
        res = cmd.Parameters("o_id_barandilla").Value

        Return res
    End Function

    Public Sub BarandilladocManual_Update_LI()
        Dim res As Integer = 0
        Dim conexion As New OracleConnection(cad1)
        Try

            conexion.Open()
            Dim cmd As New OracleCommand("Doc_laboral.PR_LI_BARANDOCMANUAL_UPDATE", conexion)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("v_idbarandilladocmanual", OracleType.VarChar).Value = midbarandilladocmanual
            cmd.Parameters.Add("v_fechapresentacion", OracleType.DateTime).Value = mfechaintervencion
            cmd.Parameters.Add("v_codigodelegacion", OracleType.VarChar).Value = mcodigodelegacion
            cmd.Parameters.Add("v_responsable", OracleType.Number).Value = mresponsable
            cmd.Parameters.Add("v_stickerseguridad", OracleType.VarChar).Value = mstickerseguridad
            cmd.Parameters.Add("v_fechaintervencion", OracleType.DateTime).Value = mfechaintervencion


            cmd.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            conexion.Close()
        End Try

    End Sub

    Public Sub BarandilladocManual_Update_LI_Relevamiento()
        Dim res As Integer = 0
        Dim conexion As New OracleConnection(cad1)
        Try

            conexion.Open()
            Dim cmd As New OracleCommand("Doc_laboral.PR_LI_BARANDOCMANUAL_UPDATE_R", conexion)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("v_idbarandilladocmanual", OracleType.VarChar).Value = midbarandilladocmanual
            cmd.Parameters.Add("v_codigodelegacion", OracleType.VarChar).Value = mcodigodelegacion
            cmd.Parameters.Add("v_fechapresentacion", OracleType.DateTime).Value = mfechaintervencion
            cmd.Parameters.Add("v_responsable", OracleType.Number).Value = mresponsable
            cmd.Parameters.Add("v_stickerseguridad", OracleType.VarChar).Value = mstickerseguridad
            cmd.Parameters.Add("v_fechaintervencion", OracleType.DateTime).Value = mfechaintervencion
            cmd.Parameters.Add("v_nrolibroant", OracleType.Number).Value = mnrolibroant
            cmd.Parameters.Add("v_datosAnteriores", OracleType.VarChar).Value = mdatosanteriores

            cmd.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            conexion.Close()
        End Try

    End Sub

    Public Function ReporteLibroManualSueldo(ByVal pidbarandilladocmanual As Integer) As DataTable
        Dim dt As DataTable
        Dim obj(1) As Object
        obj(0) = pidbarandilladocmanual
        obj(1) = ""
        dt = OracleHelper.ExecuteDataset(cad1, "Doc_laboral.Pr_ReporteLibroManualSueldo", obj).Tables(0)
        Return dt
    End Function

    Public Function BarandilladocManual_SELECT_LI_Relevamiento(ByVal pidbarandilladocmanual As Integer) As DataTable
        Dim dt As DataTable
        Dim obj(1) As Object
        obj(0) = pidbarandilladocmanual
        obj(1) = ""
        dt = OracleHelper.ExecuteDataset(cad1, "Doc_laboral.PR_LI_Relevamiento_SELECT", obj).Tables(0)
        Return dt
    End Function


End Class
