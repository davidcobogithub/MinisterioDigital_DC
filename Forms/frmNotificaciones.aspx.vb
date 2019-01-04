Imports System.Data
Imports System.Data.OracleClient
Imports CapaDatos

Partial Class frmNotificaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub butAgre_Click(sender As Object, e As EventArgs) Handles butAgre.Click

        Try

            ClsNotificaciones.TNotificaciones_insert(TextBoxIdNot.Text, TextBoxIdOrd.Text, TextBoxNCuenta.Text, TextBoxEnv.Text,
                                                     DateTime.Today, DateTime.Today, TextBoxBarandi.Text, TextBoxIdAr.Text,
                                                        TextBoxIdModel.Text, TextBoxIdUsu.Text)

            'ClsNotificacionModelos.TNotificacionesModelos_insert(TextBoxIdNot.Text, TextBoxIdOrd.Text, TextBoxNCuenta.Text, TextBoxEnv.Text, TextBoxfecha.Text)

            'ClsNotificacionTipos.TNotificacionesTipos_insert(TextBoxIdNot.Text, TextBoxIdOrd.Text)

            MessageBoxShow("Se agregó notificación correctamente")

        Catch ex As Exception

            MessageBoxShow("Error!! No se pudo agregar notificación")

        End Try

    End Sub

    Protected Sub butMod_Click(sender As Object, e As EventArgs) Handles butMod.Click

        Try

            ClsNotificaciones.TNotificaciones_update(TextBoxBusNot.Text, TextBoxIdOrd.Text, TextBoxNCuenta.Text, TextBoxEnv.Text,
                                                     DateTime.Today, DateTime.Today, TextBoxBarandi.Text, TextBoxIdAr.Text,
                                                        TextBoxIdModel.Text, TextBoxIdUsu.Text)

            'ClsNotificacionModelos.TNotificacionesModelos_update(TextBoxBusModelo.Text, TextBoxEnv.Text,
            'TextBoxfecha.Text, TextBoxfechaLec.Text, TextBoxBarandi.Text)

            'ClsNotificacionTipos.TNotificacionesTipos_update(TextBoxBusTipos.Text, TextBoxEnv.Text)

            MessageBoxShow("Se modificó notificación correctamente")

        Catch ex As Exception

            MessageBoxShow("Error!! No se pudo modificar notificación")

        End Try

    End Sub
    Protected Sub butBusca_Click(sender As Object, e As EventArgs) Handles butBusca.Click

        Try

            ClsNotificaciones.TNotificaciones_findByID(1)

            ClsNotificacionModelos.TNotificacionesModelos_findByID(1)

            ClsNotificacionTipos.TNotificacionesTipos_findByID(1)

            MessageBoxShow("Se buscó notificación correctamente")

        Catch ex As Exception

            MessageBoxShow("Error!! No se pudo buscar notificación")

        End Try
    End Sub

    Public Sub MessageBoxShow(message As String)

        Response.Write("<script type='text/javascript'> alert('" + message + "') </script>")

    End Sub

    Public Sub LimpiarCampos()

        TextBoxIdNot.Text = ""
        TextBoxIdOrd.Text = ""
        TextBoxNCuenta.Text = ""
        TextBoxEnv.Text = ""
        TextBoxfecha.Text = ""
        TextBoxfechaLec.Text = ""
        TextBoxBarandi.Text = ""
        TextBoxIdAr.Text = ""
        TextBoxIdModel.Text = ""
        TextBoxIdUsu.Text = ""

    End Sub

    Protected Sub butLim_Click(sender As Object, e As EventArgs) Handles butLim.Click
        LimpiarCampos()
    End Sub


End Class
