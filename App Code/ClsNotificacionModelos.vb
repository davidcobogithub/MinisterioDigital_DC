Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OracleClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports System.Diagnostics
Imports System.Windows.Forms


Public Class ClsNotificacionModelos


    'Dim cencr As New ClaseEncripta("1234")
    'Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())


    Public Shared Sub TNotificacionesModelos_insert(ByVal id_not_modelo As Integer, ByVal nom As String,
                                                    ByVal model As String, ByVal param As Integer, ByVal id_tipo As Integer)

        Try
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)

            conexion.Open()

            Dim query As String = "insert into DOC_LABORAL.T_NOTIFICACION_MODELOS (ID_MODELO,NOMBRE,MODELO, PARAMETROS, ID_TIPO)  values " _
                                   & "('" & id_not_modelo.ToString() & "', '" & nom & "' , '" & model & "', '" _
                                   & param.ToString() & "', '" & id_tipo.ToString() & "')"

            Dim comm As New OracleCommand(query, conexion)
            comm.ExecuteNonQuery()
            comm.Dispose()
            conexion.Close()
        Catch ex As Exception
            Throw

        End Try
    End Sub


    Public Shared Sub TNotificacionesModelos_update(ByVal id_not_modelo As Integer, ByVal nom As String,
                                                    ByVal model As String, ByVal param As Integer, ByVal id_tipo As Integer)

        Try

            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim query As String = "update DOC_LABORAL.T_NOTIFICACION_MODELOS set NOMBRE = '" & nom & "', MODELO = '" _
                                   & model & "', PARAMETROS = " & param.ToString() & ", ID_TIPO = " & id_tipo.ToString() _
                                   & "where ID_MODELO = " & id_not_modelo.ToString()

            Dim comm As New OracleCommand(query, conexion)
            comm.ExecuteNonQuery()
            comm.Dispose()
            conexion.Close()
        Catch ex As Exception
            Throw

        End Try
    End Sub


    Public Shared Function TNotificacionesModelos_findByID(ByVal id_not_model As Integer) As ArrayList

        Dim listaResultado As New ArrayList


        Try

            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim query As String = "select * from DOC_LABORAL.T_NOTIFICACION_MODELOS where ID_MODELO = '" + id_not_model + "'"

            Dim command As New OracleCommand(query, conexion)
            Dim reader As OracleDataReader = command.ExecuteReader()

            While reader.Read()

                For i As Integer = 0 To reader.FieldCount

                    listaResultado.Add(reader.GetValue(i).ToString().Trim())
                Next

            End While

            reader.Close()
            command.Dispose()
            conexion.Close()
        Catch ex As Exception
            Throw

        End Try
        Return listaResultado
    End Function


    Public Shared Function mostrarNotificacionesModelosPorNombre() As ArrayList

        Dim listaResultado As New ArrayList

        Try
            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim command As New OracleCommand("select nombre from doc_laboral.t_notificacion_modelos order by nombre ASC", conexion)
            Dim reader As OracleDataReader = command.ExecuteReader()

            While reader.Read()

                listaResultado.Add(reader.GetValue(0).ToString().Trim())

            End While

            reader.Close()
            command.Dispose()

        Catch ex As Exception
            Throw

        End Try
        Return listaResultado
    End Function

    Public Shared Function buscarContenidoModeloNotificacion(ByVal nombre As String) As String

        Dim resultado As String = ""

        Try

            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim command As New OracleCommand("select modelo from doc_laboral.t_notificacion_modelos where nombre = '" & nombre & "'", conexion)
            Dim reader As OracleDataReader = command.ExecuteReader()
            reader.Read()
            resultado = reader.GetValue(0).ToString().Trim()

            reader.Close()
            command.Dispose()

        Catch ex As Exception
            Throw

        End Try
        Return resultado
    End Function


    Public Shared Function buscarParametrosModeloNotificacion(ByVal nombre As String) As ArrayList

        Dim resultado As New ArrayList

        Try

            Dim cad1 As String = "DATA SOURCE=Encode_MIN_DC;USER ID=SYSTEM;PASSWORD = admin"
            Dim conexion As New OracleConnection(cad1)
            conexion.Open()

            Dim command As New OracleCommand("select parametros from doc_laboral.t_notificacion_modelos where nombre = '" & nombre & "'", conexion)
            Dim reader As OracleDataReader = command.ExecuteReader()
            reader.Read()
            Dim arreglo() As String = reader.GetValue(0).ToString().Split(";")

            For Each item As String In arreglo
                resultado.Add(item)
            Next

            reader.Close()
            command.Dispose()

        Catch ex As Exception
            Throw

        End Try
        Return resultado
    End Function

    Public Shared Sub htmlToPDFMemory(ByVal pHTML As String, ByVal image As String)

        'Dim bPDF As Byte()
        Dim imageTemplatePDF As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(image)
        Dim MS As New MemoryStream()
        Dim txtReader As New StringReader(pHTML)
        Dim strFileShortName As String = "testPDFMin.pdf"
        Dim strFileName As String = HttpContext.Current.Server.MapPath("~/PDFTest/" & strFileShortName)
        '1 create Object of a itextsharp document class
        Dim doc As New Document(PageSize.A4, 50.0F, 50.0F, 100.0F, 0.0F)

        '2 we create a itextsharp pdfwriter that listens to the document And directs a XML-stream to a file
        Dim oPdfWriter As PdfWriter
        'oPdfWriter = PdfWriter.GetInstance(doc, MS)
        oPdfWriter = PdfWriter.GetInstance(doc, New FileStream(strFileName, FileMode.Create))
        ' 3 we create a worker parse the document
        Dim HTMLWorker As New HTMLWorker(doc)

        '4 we open document And start the worker on the document
        oPdfWriter.PageEvent = New HeaderFooter(imageTemplatePDF)
        doc.Open()
        HTMLWorker.StartDocument()

        ' 5 parse the html into the document
        HTMLWorker.Parse(txtReader)

        Process.Start(strFileName)

        '6 close the document And the worker
        HTMLWorker.EndDocument()
        HTMLWorker.Close()
        doc.Close()

    End Sub

End Class


