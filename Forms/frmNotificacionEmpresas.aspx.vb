Imports System.Data
Imports System.Data.OracleClient
Imports System.Drawing
Imports CapaDatos
Imports System.Net.Mail
Imports System.Data.OleDb
Imports Microsoft.Office.Interop
Imports System
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Microsoft.Office.Interop.Excel

Partial Class frmnotificacionempresas
    Inherits System.Web.UI.Page

    'Private Property pathName As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            Try
                Session("variable") = ""
                cargardropdownlista(sender, e)
                cargardropdownlistaAcuerdos(sender, e)
                cargardropdownAreasMinistaerio(sender, e)
                cargardropdownlistaTipoPersoneria(sender, e)
                cargardropdownlistaDelegacion(sender, e)
                cargardropdownlistaEstados(sender, e)
                cargardropdownlistaRubros(sender, e)
                cargardropdownlistaNombreModelosNotificaciones(sender, e)

            Catch ex As Exception
                messageboxshow(ex.Source)
                messageboxshow(ex.Message)
            End Try
        End If
    End Sub


    Protected Sub cargardropdownlista(sender As Object, e As EventArgs)

        DropDownListActividades.Items.Clear()

        Try
            DropDownListActividades.Items.Insert(0, "SELECCIONE ACTIVIDAD")

            Dim lista As ArrayList = ClsNotificaciones.mostrarNombreActividadesEmpresas()

            For Each item As String In lista

                DropDownListActividades.Items.Add(New ListItem(item.ToString(), item.ToString()))

            Next
        Catch ex As Exception

            messageboxshow(ex.Message)
        End Try
    End Sub

    Protected Sub cargardropdownlistaEstados(sender As Object, e As EventArgs)

        DropDownListEstado.Items.Clear()

        Try
            DropDownListEstado.Items.Insert(0, "SELECCIONE ESTADO")

            Dim lista As ArrayList = ClsNotificaciones.mostrarNombreEstadosEmpresas()

            For Each item As String In lista

                If Not item.ToString().Equals("EnProceso") And Not item.ToString().Equals("Filtrado") Then

                    DropDownListEstado.Items.Add(New ListItem(item.ToString(), item.ToString()))

                End If

            Next
        Catch ex As Exception

            messageboxshow(ex.Message)
        End Try
    End Sub

    Protected Sub cargardropdownAreasMinistaerio(sender As Object, e As EventArgs)

        DropDownListAreasMin.Items.Clear()

        Try
            DropDownListAreasMin.Items.Insert(0, "SELECCIONE EL ÁREA")

            Dim lista As ArrayList = ClsNotificaciones.mostrarAreasMinisterio()

            For Each item As String In lista

                DropDownListAreasMin.Items.Add(New ListItem(item.ToString(), item.ToString()))

            Next
        Catch ex As Exception

            messageboxshow(ex.Message)
        End Try
    End Sub

    Protected Sub cargardropdownlistaAcuerdos(sender As Object, e As EventArgs)

        DropDownListAcuerdo.Items.Clear()

        Try
            DropDownListAcuerdo.Items.Insert(0, "SELECCIONE TIPO PRESENTACIÓN")
            DropDownListAcuerdo.Items.Insert(1, "MANUAL")
            DropDownListAcuerdo.Items.Insert(2, "DIGITAL")

        Catch ex As Exception
            messageboxshow(ex.Source)
            messageboxshow(ex.Message)
        End Try
    End Sub

    Protected Sub cargardropdownlistaTipoPersoneria(sender As Object, e As EventArgs)

        DropDownListTipoPersoneria.Items.Clear()

        Try
            DropDownListTipoPersoneria.Items.Insert(0, "SELECCIONE TIPO PERSONERÍA")
            DropDownListTipoPersoneria.Items.Insert(1, "FÍSICA")
            DropDownListTipoPersoneria.Items.Insert(2, "JURÍDICA")

        Catch ex As Exception
            messageboxshow(ex.Source)
            messageboxshow(ex.Message)
        End Try
    End Sub

    Protected Sub cargardropdownlistaDelegacion(sender As Object, e As EventArgs)

        DropDownListDelegacion.Items.Clear()

        Try
            DropDownListDelegacion.Items.Insert(0, "SELECCIONE DELEGACIÓN")

            Dim lista As ArrayList = ClsNotificaciones.mostrarDelegaciones()

            For Each item As String In lista

                DropDownListDelegacion.Items.Add(New ListItem(item.ToString(), item.ToString()))

            Next

        Catch ex As Exception
            messageboxshow(ex.Source)
            messageboxshow(ex.Message)
        End Try
    End Sub


    Protected Sub cargardropdownlistaRubros(sender As Object, e As EventArgs)

        DropDownListRubro.Items.Clear()

        Try
            DropDownListRubro.Items.Insert(0, "SELECCIONE RUBRO")

            Dim lista As ArrayList = ClsNotificaciones.mostrarRubros()

            For Each item As String In lista

                DropDownListRubro.Items.Add(New ListItem(item.ToString(), item.ToString()))

            Next

        Catch ex As Exception
            messageboxshow(ex.Source)
            messageboxshow(ex.Message)
        End Try
    End Sub

    Protected Sub cargardropdownlistaNombreModelosNotificaciones(sender As Object, e As EventArgs)

        DropDownListNombreModeloNot.Items.Clear()

        Try
            DropDownListNombreModeloNot.Items.Insert(0, "SELECCIONE MODELO NOTIFICACIÓN")

            Dim lista As ArrayList = ClsNotificacionModelos.mostrarNotificacionesModelosPorNombre()
            For Each item As String In lista

                DropDownListNombreModeloNot.Items.Add(New ListItem(item.ToString(), item.ToString()))

            Next

        Catch ex As Exception
            messageboxshow(ex.Source)
            messageboxshow(ex.Message)
        End Try
    End Sub

    Public Sub messageboxshow(message As String)

        Response.Write("<script type='text/javascript'> alert('" + message + "'); </script>")

    End Sub

    Protected Sub cargarGridView(ByVal sender As Object, ByVal e As System.EventArgs) Handles butBuscar.Click

        Try

            Dim dt As New Data.DataTable()

            dt = ClsNotificaciones.cargarTablaNotificaciones(TextBoxCuit.Text, TextBoxRazon.Text, TextBoxCant1.Text, TextBoxCant2.Text, DropDownListActividades.SelectedItem.Text, DropDownListDelegacion.Text, DropDownListEstado.SelectedItem.Text, DropDownListAcuerdo.SelectedItem.Text, DropDownListTipoPersoneria.SelectedItem.Text, DropDownListRubro.SelectedItem.Text, TextBoxIdArea1.Text, TextBoxIdArea2.Text)

            If Not dt Is Nothing Then

                GridView1.DataSource = dt
                GridView1.DataBind()

                If GridView1.Rows.Count = 0 Then
                    messageboxshow("No se encontraron registros para la búsqueda")
                    LimpiarCampos()
                    Exit Sub
                End If
                ModalPopupExtender1.Show()
            Else
                GridView1.DataSource = dt
                GridView1.DataBind()
                messageboxshow("ERROR, debes llenar los campos de búsqueda")

            End If
            'modalPanel.Visible = True

            LimpiarCampos()

        Catch ex As Exception

            messageboxshow("error")
            messageboxshow(ex.Source)
            messageboxshow(ex.Message)
        End Try

    End Sub



    'Protected Sub btnnuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnuevo.Click
    '    'Boxaccion.Value = acciones.Insertar
    '    Dim nromul As Integer
    '    'nromul = OracleHelper.ExecuteScalar(cad1, CommandType.Text, "select doc_laboral.seq_multinotasmodelos.nextval  from dual")
    '    'Boxidmul.Value = nromul
    '    'txtidmul.Text = nromul
    '    'txtBox1.Text = ""
    '    txtnombre.Text = ""
    '    ModalPopupExtender1.Show()
    'End Sub

    Protected Sub chckchanged(sender As Object, e As EventArgs)

        Try

            Dim chckheader As WebControls.CheckBox = CType(GridView1.HeaderRow.FindControl("CheckBox1"), WebControls.CheckBox)

            For Each row As GridViewRow In GridView1.Rows
                Dim chckrw As WebControls.CheckBox = CType(row.FindControl("CheckBox2"), WebControls.CheckBox)
                If chckheader.Checked = True Then
                    chckrw.Checked = True
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2")

                Else
                    chckrw.Checked = False
                    row.BackColor = Color.White

                End If

            Next
        Catch ex As Exception
            messageboxshow(ex.Source)
            messageboxshow(ex.Message)
        End Try

    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)

        Try

            For Each row As GridViewRow In GridView1.Rows
                Dim chckrw As WebControls.CheckBox = CType(row.FindControl("CheckBox2"), WebControls.CheckBox)

                If chckrw.Checked = True Then

                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2")

                Else
                    chckrw.Checked = False
                    row.BackColor = Color.White
                End If
            Next

        Catch ex As Exception
            messageboxshow(ex.Source)
            messageboxshow(ex.Message)
        End Try

    End Sub

    'Protected Sub PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView2.PageIndexChanging

    '    GridView1.PageIndex = e.NewPageIndex
    '    GridView1.DataBind()

    'End Sub


    'Public Sub agregar()

    '    Try

    '        Dim dataTable As New Data.DataTable()

    '        dataTable.Columns.Add("Nombre")
    '        dataTable.Columns.Add("Sexo")

    '        Renglon es la variable que adicionara renglones a mi datatable
    '        Dim row As DataRow = dataTable.NewRow()
    '        row("Nombre") = "Luis"
    '        row("Sexo") = "Masculino"
    '        dataTable.Rows.Add(row)

    '        por último envió mi datatable a un gridview para visualizarlo en pantalla
    '        Me.GridView2.DataSource = dataTable
    '        Me.GridView2.DataBind()

    '    Catch ex As Exception
    '        messageboxshow(ex.Source)
    '        messageboxshow(ex.Message)
    '    End Try

    'End Sub


    Public Sub LimpiarCampos()

        TextBoxCuit.Text = ""
        TextBoxRazon.Text = ""
        TextBoxCant1.Text = ""
        TextBoxCant2.Text = ""
        TextBoxIdArea1.Text = ""
        TextBoxIdArea2.Text = ""
        DropDownListRubro.SelectedIndex = 0
        DropDownListDelegacion.SelectedIndex = 0
        DropDownListEstado.SelectedIndex = 0
        DropDownListActividades.SelectedIndex = 0
        DropDownListAcuerdo.SelectedIndex = 0
        DropDownListTipoPersoneria.SelectedIndex = 0

    End Sub


    'Public Sub DownloadPDF()

    '    ' Dim HTMLContent As String = "Hello <b>World</b>"
    '    Dim HTMLContent As String = "<html><head></head><body style='font-family: Arial, Helvetica, sans-serif'>" _
    '               & "<div><h2 style='text-align: center; color: #808080;'>SOLICITUD DE APERTURA DE LIBRO DE INSPECCION</h2>" _
    '               & "<table style='border: thin solid #CCCCCC; padding: 10px; width: 100%;'>" _
    '               & "<tr>" _
    '               & "<td style='padding: 5px; width: 150px;'>RAZON SOCIAL: </td>" _
    '               & "<td style='padding: 5px'><b> PY Razón </b></td> " _
    '               & "</tr>" _
    '               & "</table>" _
    '               & "<br/>" _
    '               & "<div style='text-align:right;'>Firma Empleador</div>" _
    '               & "</div></body></html>"
    '    Response.Clear()
    '    Response.ContentType = "application/pdf"
    '    Response.AddHeader("content-disposition", "attachment;filename=" + "PDFfile.pdf")
    '    Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '    Response.BinaryWrite(ClsNotificaciones.htmlToPDFMemory(HTMLContent))
    '    Response.End()
    'End Sub
    Protected Function armarDocumentoPDFNotificacionesEmpresasTest() As String

        Dim HTMLContent As String = ""

        'If DropDownListNombreModeloNot.SelectedItem.Text.Equals("Prueba Notificacion") Then

        'Dim contenido As String = ClsNotificacionModelos.buscarContenidoModeloNotificacion(DropDownListNombreModeloNot.SelectedItem.Text)
        'Dim contenido As String = ClsNotificacionModelos.buscarContenidoModeloNotificacion("Prueba Notificacion")

        Dim contenido As String = convertidorDeContenidoNotificacion()

        HTMLContent = "<html><head></head><body style='font-family: Arial, Helvetica, sans-serif'><img style='width 300px; height: 100px; margin-left: 15%; margin-top: 5%; border-style: solid' />" _
      & "<br><div style='margin-left 50%;text-align:right'></div>" _
      & "<br><div style='margin-left 10%;text-align:left; font-weight: bold;'></div>" _
      & "<br><div style='text-align: center;'><p style='width 80%;margin-left: 10%;' align='justify'><small>" _
      & contenido _
      & "</small></p></div>" _
      & "<div><br><br><p></p>" _
      & "<img  style='width 100px; height: 100px; margin-left: 50%; margin-bottom: 50px; border-style: solid'/></div></body></html>"
        'NOTIFICADO.............................
        'End If

        Return HTMLContent
    End Function

    Protected Function convertidorDeContenidoNotificacion() As String

        Dim mensaje As String = ClsNotificacionModelos.buscarContenidoModeloNotificacion("Prueba Notificacion")
        Dim parametros As ArrayList = ClsNotificacionModelos.buscarParametrosModeloNotificacion("Prueba Notificacion")
        Dim razon As String = ""
        Dim nroCuenta As String = ""
        Dim delegacion As String = ""

        If Not GridView1.HeaderRow Is Nothing Then
            For Each row As GridViewRow In GridView1.Rows
                Dim chckrw As WebControls.CheckBox = CType(row.FindControl("CheckBox2"), WebControls.CheckBox)

                If chckrw.Checked = True Then

                    razon = row.Cells(2).Text
                    nroCuenta = row.Cells(row.Cells.Count - 1).Text
                    delegacion = row.Cells(4).Text

                End If
            Next

        End If

        For i As Integer = 0 To parametros.Count - 1

            If parametros(i).Equals("%fecha%") Then
                mensaje = mensaje.Replace("%fecha%", DateTime.Today)
            End If
            If parametros(i).Equals("%numeroExpte%") Then
                mensaje = mensaje.Replace("%numeroExpte%", i)
            End If
            If parametros(i).Equals("%razon_social%") Then
                mensaje = mensaje.Replace("%razon_social%", razon)
            End If
            If parametros(i).Equals("%nombre_pers_fisica%") Then
                mensaje = mensaje.Replace("%nombre_pers_fisica%", razon)
            End If
            If parametros(i).Equals("%num_cuenta%") Then
                mensaje = mensaje.Replace("%num_cuenta%", nroCuenta)
            End If
            If parametros(i).Equals("%delegacion_empresa%") Then
                mensaje = mensaje.Replace("%delegacion_empresa%", delegacion)
            End If
            If parametros(i).Equals("%cba%") Then
                mensaje = mensaje.Replace("%cba%", DateTime.Today)
            End If
            If parametros(i).Equals("%fecha_citacion%") Then
                mensaje = mensaje.Replace("%fecha_citacion%", DateTime.Now.ToShortDateString())
            End If
            If parametros(i).Equals("%hora_citacion%") Then
                mensaje = mensaje.Replace("%hora_citacion%", DateTime.Now.ToShortTimeString())
            End If
            If parametros(i).Equals("%seccion_compa%") Then
                mensaje = mensaje.Replace("%seccion_compa%", DateTime.Today)
            End If
            If parametros(i).Equals("%num_secretaria%") Then
                mensaje = mensaje.Replace("%num_secretaria%", DateTime.Today)
            End If
            If parametros(i).Equals("%nombre_actora%") Then
                mensaje = mensaje.Replace("%nombre_actora%", DateTime.Today)
            End If
        Next

        Return mensaje
    End Function


    'Protected Sub btnNotificar_Click(sender As Object, e As EventArgs) Handles btnNotificar.Click

    '    Dim Message As New System.Net.Mail.MailMessage()
    '    Dim SMTP As New System.Net.Mail.SmtpClient

    '    'Dim correoEnvia As String = "davip.9512052013@gmail.com"
    '    Dim correoEnvia As String = "davip.95@hotmail.com"

    '    Dim correoRecibe As String = "davip.95@hotmail.com"
    '    Dim password As String = "05121995"
    '    'Dim password As String = "davidicesi"

    '    'CONFIGURACIÓN DEL STMP
    '    SMTP.Credentials = New System.Net.NetworkCredential(correoEnvia, password)
    '    SMTP.Host = "smtp.live.com"
    '    SMTP.Port = 25
    '    SMTP.EnableSsl = True

    '    ' CONFIGURACION DEL MENSAJE
    '    Message.[To].Add(correoRecibe) 'Cuenta de Correo al que se le quiere enviar el e-mail
    '    Message.From = New System.Net.Mail.MailAddress(correoEnvia, "David PY", System.Text.Encoding.UTF8) 'Quien lo envía
    '    Message.Subject = "Prueba de Correo MIN_CODE" 'Sujeto del e-mail
    '    Message.SubjectEncoding = System.Text.Encoding.UTF8 'Codificacion
    '    Message.Body = "Hola este es un mesaje de prueba MIN_ENCO" 'contenido del mail
    '    Message.BodyEncoding = System.Text.Encoding.UTF8
    '    Message.Priority = System.Net.Mail.MailPriority.Normal
    '    Message.IsBodyHtml = True

    '    'ENVIO
    '    Try
    '        SMTP.Send(Message)
    '        messageboxshow("Mensaje enviado correctamene !Exito!")

    '    Catch ex As System.Net.Mail.SmtpException
    '        messageboxshow("Error!")
    '        messageboxshow(ex.Source)
    '        messageboxshow(ex.Message)

    '    End Try

    ' End Sub

    Protected Sub cargarArchivos(sender As Object, e As EventArgs) Handles btnUpload.Click

        ' pathName = ""
        Dim path As String = Server.MapPath("~/Archivos/")
        Dim fileOK As Boolean = False
        If FileUpload1.HasFile Then
            Dim fileExtension As String
            fileExtension = System.IO.Path.
                GetExtension(FileUpload1.FileName).ToLower()
            Dim allowedExtensions As String() =
                {".xlsx", ".xlsm", ".xls", ".xltm", ".xltx"}
            For i As Integer = 0 To allowedExtensions.Length - 1
                If fileExtension = allowedExtensions(i) Then
                    fileOK = True
                End If
            Next
            If fileOK Then
                Try
                    FileUpload1.PostedFile.SaveAs(path &
                         FileUpload1.FileName)

                    'pathName = FileUpload1.FileName
                    Session("variable") = FileUpload1.FileName
                    importarExcel(FileUpload1.FileName)

                Catch ex As Exception
                    messageboxshow("Se produjo un error, el archivo no fue subido")

                End Try
            Else
                messageboxshow("No se aceptan archivos de este tipo o formato")
            End If
        Else
            messageboxshow("Debe seleccionar el archivo para subirlo")
        End If

    End Sub

    Protected Sub importarExcel(ByVal nombreArchivo As String)

        'Dim dt As New Data.DataTable()

        Dim excel As Application = Nothing   'Contains an instance of Microsft Excel.
        Dim dataTable(,) As Object = {}           'Contains a worksheets data.
        Dim workBook As Workbook = Nothing   'Contains a Microsoft Excel workbook.
        Dim cantRegistros As Integer = 1
        Try
            'Starts a new instance of Microsoft Excel and loads a workbook.
            excel = New Application

            workBook = excel.Workbooks.Open(nombreArchivo)
            'workBook = excel.Workbooks.Open("C:\Users\davip\Downloads\MinisterioDigital_DESA\Archivos\" & nombreArchivo)

            'Loops through each worksheet and display its contents.
            For Each workSheet As Worksheet In workBook.Worksheets

                'Stores the worksheet's data in a two dimensional array.
                dataTable = DirectCast(workSheet.UsedRange().Value(XlRangeValueDataType.xlRangeValueDefault), Object(,))

                messageboxshow("Se han cargado exitosamente " & dataTable.GetUpperBound(0) - 1 & " registros de la hoja " & cantRegistros & " del archivo")
                cantRegistros += 1

            Next workSheet

        Catch ex As Exception
            messageboxshow(ex.Message)
            messageboxshow(ex.Source)
        Finally
            'Closes the worksheet and Microsoft Excel when done.
            workBook.Close(SaveChanges:=False)
            excel.Quit()
        End Try


    End Sub



    Protected Sub loadPaginaNotificaciones(sender As Object, e As EventArgs) Handles ButtonNext.Click

        Try
            If Not GridView1.HeaderRow Is Nothing Then

                Dim chckheader As WebControls.CheckBox = CType(GridView1.HeaderRow.FindControl("CheckBox1"), WebControls.CheckBox)

                For Each row As GridViewRow In GridView1.Rows

                    Dim chckrw As WebControls.CheckBox = CType(row.FindControl("CheckBox2"), WebControls.CheckBox)

                    If chckrw.Checked = True Then
                        panelNotificaciones.Visible = True
                        panelBusquedas.Visible = False
                        ButtonPrev.Enabled = True
                        Exit Sub
                    End If

                Next

                If Not CStr(Session("variable")).Equals("") = True Then

                    panelNotificaciones.Visible = True
                    panelBusquedas.Visible = False
                    ButtonPrev.Enabled = True
                    Session("variable") = ""
                    Exit Sub

                End If

                If chckheader.Checked = True Then

                    panelNotificaciones.Visible = True
                    panelBusquedas.Visible = False
                    ButtonPrev.Enabled = True
                    Exit Sub
                Else
                    messageboxshow("No has seleccionado ninguna empresa a notificar. Para continuar debes seleccionar las empresas a notificar")
                    ModalPopupExtender1.Show()
                End If

            Else

                If Not CStr(Session("variable")).Equals("") = True Then

                    panelNotificaciones.Visible = True
                    panelBusquedas.Visible = False
                    ButtonPrev.Enabled = True
                    Session("variable") = ""
                    Exit Sub
                Else
                    messageboxshow("Error, no has hecho la búsqueda personalizada o no has subido el archivo para notificación masiva. Para continuar debes elegir alguna de las opciones para notificar")

                End If

            End If
        Catch ex As Exception

            messageboxshow(ex.Message)
            messageboxshow(ex.Source)
        End Try

    End Sub

    Protected Sub loadPaginaBusquedas(sender As Object, e As EventArgs) Handles ButtonPrev.Click

        Try
            panelNotificaciones.Visible = False
            panelBusquedas.Visible = True
            ButtonPrev.Enabled = False
            ModalPopupExtender1.Show()
        Catch ex As Exception
            messageboxshow(ex.Message)
            messageboxshow(ex.Source)
        End Try

    End Sub


    Protected Sub loadPagina(sender As Object, e As EventArgs)

        Response.Redirect("frmNotificacionEmpresas.aspx")

    End Sub

    Protected Sub btnAceptarModal_Click(sender As Object, e As EventArgs) Handles btnAceptarModal.Click
        ModalPopupExtender1.Hide()
    End Sub

    Private Sub ButtonGenerarPDF_Click(sender As Object, e As EventArgs) Handles ButtonGenerarPDF.Click
        ClsNotificacionModelos.htmlToPDFMemory(armarDocumentoPDFNotificacionesEmpresasTest(), Server.MapPath(".") + "\Images\logo.png")
    End Sub
End Class
