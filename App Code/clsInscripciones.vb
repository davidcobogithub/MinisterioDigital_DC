Imports Microsoft.VisualBasic
Imports CapaDatos
Imports System.Data

Public Class clsInscripciones
    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Public Function inscripciones_sel(ByVal pIdEvento As Integer, ByVal pCUIL As String) As DataTable
        Try
            Dim param(2) As Object
            Dim dt As DataTable
            param(0) = pIdEvento
            param(1) = pCUIL
            dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.p_ev_inscripcion_x_eventos", param).Tables(0)
            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function mostrar_requisitos(ByVal pIdEvento As Integer) As DataTable
        Try
            Dim param(1) As Object
            param(0) = pIdEvento

            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.pr_ev_requisitos", param).Tables(0)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function mostrar_requisitos_cupos(ByVal pid_evento As Integer) As DataTable
        Try
            Dim param(1) As Object
            param(0) = pid_evento

            Return OracleHelper.ExecuteDataset(cad1, "doc_laboral.p_ev_eventos_cupos_sel", param).Tables(0)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function valida_inscriptos(ByVal pid_evento As Integer, ByVal pcuil As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim param(2) As Object
            param(0) = pid_evento
            param(1) = pcuil

            dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.p_ev_valida_inscripcion_sel", param).Tables(0)

            Return dt

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function inserta_modifica_inscripcion(ByVal pid_evento As Integer,
                                                 ByVal pid_actividad As Integer,
                                                 ByVal pcuil As String,
                                                 ByVal pvalidado As String)
        Try
            If pvalidado = "&nbsp;" Then
                pvalidado = "0"
            End If
            Dim param(10) As Object
            param(0) = DBNull.Value                             'observaciones
            param(1) = pcuil                                    'cuil
            param(2) = "0"                                      'esdisertante
            param(3) = pid_actividad                            'id_actividad
            param(4) = DBNull.Value                             'token
            param(5) = pvalidado                                'pvalidado
            param(6) = Date.Now.ToString()                      'fecha registro
            param(7) = DBNull.Value                             'asistion
            param(8) = "0"                                      'aprobo
            param(9) = DBNull.Value                             'nota
            param(10) = DBNull.Value                            'condicional

            OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.p_ev_inscriptos_ins", param)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function elimina_inscripcion(ByVal pid_actividad As Integer, ByVal pcuil As String)
        Try
            Dim param(1) As Object
            param(0) = pid_actividad
            param(1) = pcuil

            OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.p_ev_inscriptos_del", param)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
