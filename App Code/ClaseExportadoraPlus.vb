Imports Microsoft.VisualBasic
Imports Microsoft.ReportingServices.ReportRendering
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Reporting
Imports System.IO
Imports System.Data
Imports System.Data.OracleClient
Imports System.Net
Imports System.Web.ui
Imports System.Web.UI.Page
Imports System.Reflection
Imports System.Runtime.Remoting
Imports System.Xml
Imports CapaDatos
Imports System.Web.HttpContext


Public Class ClaseExportadoraPlus
    Dim cencr As New ClaseEncripta("1234")
    Private cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())
    Public parametros As New ArrayList
    Dim parametrosReporte As New Collections.Generic.List(Of Microsoft.Reporting.WebForms.ReportParameter)
    Dim reporte As New LocalReport()
    'Agregar subreporte
    Dim subreportes(3) As Collections.Generic.List(Of String)
    Dim cantParametros As Integer
    Dim cadena As String = ""
    Dim subreporte As Boolean = False

    Public Property cadenaConexion() As String
        Get
            Return cadena
        End Get
        Set(ByVal value As String)
            cadena = value
        End Set
    End Property

    Public Sub New(ByVal PathReporte As String)
        reporte.ReportPath = PathReporte
        subreportes(0) = New Collections.Generic.List(Of String)
        subreportes(1) = New Collections.Generic.List(Of String)
        subreportes(2) = New Collections.Generic.List(Of String)

    End Sub

    Private Function crearObjeto(ByVal tipoObjeto As String) As Object
        Dim cadenaObjeto As String = "" & tipoObjeto
        Dim ensamblado As Assembly = Assembly.GetExecutingAssembly
        Dim ManipularObjeto As Object
        Try
            ManipularObjeto = AppDomain.CurrentDomain.CreateInstance(ensamblado.FullName, cadenaObjeto)
            Return ManipularObjeto
        Catch ex As Exception
            'MsgBox("Error al generar el objeto: " & ex.Message)
            Throw ex
            Return Nothing
        End Try

    End Function

    Public Sub agregarDataSource(ByVal nombreDataSourceInforme As String)
        Dim nombreDataSource As String = nombreDataSourceInforme.Replace("_", ".")
        Dim rs As New ReportDataSource(nombreDataSourceInforme, GetData(nombreDataSource))
        'rs.DataSourceId = nombreDataSourceInforme

        reporte.DataSources.Add(rs)
    End Sub

    Public Sub agregarDataSource(ByVal nombreDataSourceInforme As String, ByVal isplan As Boolean)
        Dim nombreDataSource As String = nombreDataSourceInforme.Replace("_", ".")
        Dim rs As New ReportDataSource(nombreDataSourceInforme, GetData(nombreDataSource, isplan))
        'rs.DataSourceId = nombreDataSourceInforme

        reporte.DataSources.Add(rs)
    End Sub

    Private Function devolverDataSourceSubreporte(ByVal nombreDataSourceSubreporte As String) As ReportDataSource
        Dim nombreDataSource As String = nombreDataSourceSubreporte.Replace("_", ".")
        Dim rs As New ReportDataSource(nombreDataSourceSubreporte, GetData(nombreDataSource))
        'rs.DataSourceId = nombreDataSourceInforme

        Return (rs)
    End Function

    Public Sub agregarDataSourceDesconectado(ByVal nombreDataSourceInforme As String, ByVal consulta As String)
        Dim nombreDataSource As String = nombreDataSourceInforme.Replace("_", ".")
        Dim rs As New ReportDataSource(nombreDataSourceInforme, GetDataDesconectado(nombreDataSource, consulta))
        'rs.DataSourceId = nombreDataSourceInforme
        reporte.DataSources.Add(rs)
    End Sub

    Public Sub agregarDataSourceDesconectado(ByVal nombreDataSourceInforme As String, ByVal dt As DataTable)
        Dim nombreDataSource As String = nombreDataSourceInforme.Replace("_", ".")
        Dim rs As New ReportDataSource(nombreDataSourceInforme, dt)
        'rs.DataSourceId = nombreDataSourceInforme
        reporte.DataSources.Add(rs)
    End Sub

    Private Function GetData(ByVal nombreDS As String) As DataTable

        Dim nombreTA = nombreDS.Replace(".", "TableAdapters.") & "TableAdapter"
        Dim vretorno As Integer
        Dim consulta As String = buscarConsultaEnAdapter(nombreDS, nombreTA)


        Dim ds As New DataSet

        Dim par(parametros.Count - 1) As OracleParameter

        Dim i As Integer
        For i = 0 To parametros.Count - 1
            par(i) = parametros.Item(i)
        Next


        vretorno = cad1.ToUpper.IndexOf("DESA")

        If vretorno > 1 Then
            consulta = consulta.Replace("apj.fecha_incio", "apj.fecha_inicio")
        End If

        ds = OracleHelper.ExecuteDataset(Me.cadena, CommandType.Text, consulta, par)

        parametros.Clear()

        'nombreDS = nombreDS.Replace(".", "+")

        'nombreDS &= "DataTable"

        ds.Tables(0).TableName = nombreDS

        'ds.Tables(0).Namespace = nombreDS & "DataTable"

        Return ds.Tables(0)

    End Function

    Private Function GetData(ByVal nombreDS As String, ByVal isplan As Boolean) As DataTable

        Dim nombreTA = nombreDS.Replace(".", "TableAdapters.") & "TableAdapter"

        Dim consulta As String = buscarConsultaEnAdapter(nombreDS, nombreTA)
        Dim vretorno As String
        Dim ds As New DataSet

        Dim par(parametros.Count - 1) As OracleParameter
        Dim vnrocuenta As String

        Dim i As Integer
        For i = 0 To parametros.Count - 1
            par(i) = parametros.Item(i)
        Next

        vretorno = cadena.ToLower.IndexOf("CBA1")
        If vretorno > 1 Then
            consulta = consulta.Replace("fecha_inicio", "fecha_incio")
        End If


        ds = OracleHelper.ExecuteDataset(Me.cadena, CommandType.Text, consulta, par)

        parametros.Clear()

        'nombreDS = nombreDS.Replace(".", "+")

        'nombreDS &= "DataTable"

        ds.Tables(0).TableName = nombreDS

        'ds.Tables(0).Namespace = nombreDS & "DataTable"

        Return ds.Tables(0)

    End Function

    Private Function GetDataDesconectado(ByVal nombreDS As String, ByVal consulta As String) As DataTable

        Dim ds As New DataSet

        Dim par(parametros.Count) As OracleParameter

        Dim i As Integer
        For i = 0 To parametros.Count - 1
            par(i) = parametros.Item(i)
        Next

        ds = OracleHelper.ExecuteDataset(Me.cadena, CommandType.Text, consulta, par)

        parametros.Clear()

        'nombreDS = nombreDS.Replace(".", "+")

        'nombreDS &= "DataTable"

        ds.Tables(0).TableName = nombreDS

        'ds.Tables(0).Namespace = nombreDS & "DataTable"


        Return ds.Tables(0)

    End Function

    Public Function generarInforme(ByVal anchoEnPulgadas As Decimal, ByVal altoEnPulgadas As Decimal) As Byte()

        Dim reportType As String = "PDF"
        Dim mimeType As String = "application/pdf"
        Dim encoding As String = Nothing
        Dim fileNameExtension As String = "pdf"
        Dim deviceInfo As String = Nothing
        'Dim tamHoja As System.Drawing.Printing.PaperSize
        'tamHoja = reporte.GetDefaultPageSettings.PaperSize
        Dim ancho As String = Str(anchoEnPulgadas)
        Dim alto As String = Str(altoEnPulgadas)
        deviceInfo = "<DeviceInfo>"
        deviceInfo = deviceInfo & "<OutputFormat>PDF</OutputFormat>"
        deviceInfo = deviceInfo & "<PageWidth>" & ancho & "in</PageWidth>"
        deviceInfo = deviceInfo & "<PageHeight>" & alto & "in</PageHeight>"
        'deviceInfo = deviceInfo & "<MarginTop>0.2in</MarginTop>"
        deviceInfo = deviceInfo & "<MarginLeft>0.2in</MarginLeft>"
        'deviceInfo = deviceInfo & "<MarginRight>0.2in</MarginRight>"
        'deviceInfo = deviceInfo & "<MarginBottom>0.2in</MarginBottom>"
        deviceInfo = deviceInfo & "</DeviceInfo>"

        Dim warnings() As Warning = Nothing

        Dim streams() As String = Nothing

        Dim renderedBytes() As Byte

        Try

            If Me.subreporte = True Then
                AddHandler Me.reporte.SubreportProcessing, AddressOf EventoSubReporte
            End If

            renderedBytes = reporte.Render(reportType, deviceInfo, mimeType, encoding, fileNameExtension, streams, warnings)
        Catch ex As Exception
            'MsgBox(ex.InnerException.InnerException.Message)
            Throw ex
        End Try

        'guardarPlanillaHorariosEnBD(renderedBytes, 1, nroCuenta, ParametroAno, ParametroMes, IdPlanilla)

        Return renderedBytes

        'Response.Clear()
        'Response.ContentType = "application/pdf"
        'Response.AddHeader("content-disposition", "attachment; filename=Archivo.pdf")

        'Response.BinaryWrite(renderedBytes)
        'Response.End()
    End Function

    Private Function buscarConsultaEnAdapter(ByVal dataSource As String, ByVal nombreAdapter As String) As String

        Dim consulta As String = ""
        Dim nombreDataset As String = Split(dataSource, ".")(0) & ".xsd"
        Dim nombreAdapterXml As String = Split(nombreAdapter, ".")(1)
        Dim path As String = System.Web.Hosting.HostingEnvironment.MapPath("~/BCK/")

        Dim Xml As XmlDocument
        Dim NodeList As XmlNodeList
        Dim NodeList1 As XmlNodeList
        Dim Node As XmlNode
        Dim node2 As XmlNode
        Dim node3 As XmlNode
        Dim node4 As XmlNode
        Dim atributo As XmlAttribute
        'Dim xmlO As New XmlDocument

        Try
            Xml = New XmlDocument()
            Xml.Load(path & nombreDataset)

            NodeList = Xml.GetElementsByTagName("Tables")
            Node = NodeList.Item(0)

            Dim i As Integer
            For i = 0 To Node.ChildNodes.Count - 1
                node2 = Node.ChildNodes.Item(i)
                If node2.Attributes.Count > 0 Then
                    If node2.Attributes("DataAccessorName").Value = nombreAdapterXml Then
                        node3 = buscarNodoPorNombre("MainSource", node2)
                        node4 = buscarNodoPorNombre("DbSource", node3)
                        node3 = buscarNodoPorNombre("SelectCommand", node4)
                        node4 = buscarNodoPorNombre("DbCommand", node3)
                        node3 = buscarNodoPorNombre("CommandText", node4)
                        consulta = node3.InnerText
                    End If
                End If
            Next
        Catch ex As Exception
            'MsgBox(ex.GetType.ToString & vbNewLine & ex.Message.ToString)
            Throw ex
        End Try
        Return consulta
    End Function

    Private Function buscarNodoPorNombre(ByVal nombreNodo As String, ByVal xml As XmlNode) As XmlNode

        Dim i As Integer
        For i = 0 To xml.ChildNodes.Count - 1
            If xml.ChildNodes(i).Name = nombreNodo Then
                Return xml.ChildNodes(i)
            End If
        Next

    End Function

    Public Sub agregarParametro(ByVal nombreParametro As String, ByVal valor As String)
        Dim param As OracleParameter
        param = New OracleParameter
        param.ParameterName = nombreParametro
        'param.SqlDbType = SqlDbType.Image
        param.Size = valor.Length
        param.Value = valor
        parametros.Add(param)
    End Sub

    Public Sub agregarParametroLocalReport(ByVal nombreParametro As String, ByVal valor As String)
        Dim param As New Microsoft.Reporting.WebForms.ReportParameter
        param.Name = nombreParametro
        param.Values.Add(valor)
        parametrosReporte.Add(param)
        reporte.SetParameters(parametrosReporte)
    End Sub

    Public Function guardarDocEnBD(ByVal campoContenido As String, ByVal contenidoPDF() As Byte, ByVal otrosCamposYValores As String) As Integer
        Dim respuesta As Integer = 0
        Dim parametros() As String = Split(otrosCamposYValores, ",")
        Dim i As Integer
        For i = 0 To parametros.Length - 1
            Dim par() As String = Split(parametros(i), "=")
            par(0) = par(0).Trim
            par(1) = par(1).Trim
        Next
        Dim consulta As New StringBuilder
        consulta.Append("INSERT INTO BarandillaDocumentoPDF (")
        consulta.Append(campoContenido)
        consulta.Append(",")
        For i = 0 To parametros.Length - 1
            Dim par() As String = Split(parametros(i), "=")
            consulta.Append(par(0))
            consulta.Append(",")
        Next
        consulta.Remove(consulta.Length - 1, 1)
        consulta.Append(") values (@Contenido,")
        For i = 0 To parametros.Length - 1
            Dim par() As String = Split(parametros(i), "=")
            consulta.Append(par(1))
            consulta.Append(",")
        Next
        consulta.Remove(consulta.Length - 1, 1)
        consulta.Append(") SELECT @@IDENTITY")
        Try
            Dim conexion As New OracleConnection(cadena)
            Dim comando As New OracleCommand(consulta.ToString, conexion)
            comando.CommandType = CommandType.Text

            Dim param As OracleParameter
            param = New OracleParameter()
            param.ParameterName = "@Contenido"
            param.DbType = SqlDbType.Image
            param.Size = contenidoPDF.Length
            param.Value = contenidoPDF

            comando.Parameters.Add(param)

            conexion.Open()
            respuesta = comando.ExecuteNonQuery()
            conexion.Close()

        Catch ex As Exception
            'MsgBox("Error al insertar en la base de datos: " & ex.Message)
            Throw ex
        End Try

        Return respuesta
    End Function

    Public Function guardarDocEnBDOracle(ByVal campoContenido As String, ByVal contenidoPDF() As Byte, ByVal otrosCamposYValores As String) As Integer
        Dim respuesta As Integer = 0
        Dim parametros() As String = Split(otrosCamposYValores, ",")
        Dim i As Integer
        For i = 0 To parametros.Length - 1
            Dim par() As String = Split(parametros(i), "=")
            par(0) = par(0).Trim
            par(1) = par(1).Trim
        Next
        Dim consulta As New StringBuilder
        consulta.Append("INSERT INTO BarandillaDocumentoPDF (")
        consulta.Append(campoContenido)
        consulta.Append(",")
        For i = 0 To parametros.Length - 1
            Dim par() As String = Split(parametros(i), "=")
            consulta.Append(par(0))
            consulta.Append(",")
        Next
        consulta.Remove(consulta.Length - 1, 1)
        consulta.Append(") values (:p_contenido,")
        For i = 0 To parametros.Length - 1
            Dim par() As String = Split(parametros(i), "=")
            consulta.Append(par(1))
            consulta.Append(",")
        Next
        consulta.Remove(consulta.Length - 1, 1)
        'consulta.Append(") SELECT @@IDENTITY")
        consulta.Append(") ")

        'Dim tx As OracleTransaction
        Try
            Dim conexion As New OracleConnection(cadena)
            conexion.Open()

            Dim comando As New OracleCommand()


            'tx = conexion.BeginTransaction()

            comando.Connection = conexion
            comando.CommandType = CommandType.Text
            'comando.Transaction = tx

            'comando.CommandText = "declare xx blob; begin dbms_lob.createtemporary(xx, false, 0); :tempblob := xx; end;"
            'comando.Parameters.Add(New OracleParameter("tempblob", OracleType.Blob)).Direction = ParameterDirection.Output
            'comando.ExecuteNonQuery()

            'Dim tempLob As OracleLob
            'tempLob = comando.Parameters(0).Value
            'tempLob.BeginBatch(OracleLobOpenMode.ReadWrite)
            'tempLob.Write(contenidoPDF, 0, contenidoPDF.Length)
            'tempLob.EndBatch()

            'comando.Parameters.Clear()

            comando.CommandText = consulta.ToString()

            comando.Parameters.Add(New OracleParameter("p_contenido", OracleType.Blob)).Value = contenidoPDF

            'Dim param As OracleParameter
            'param = New OracleParameter()
            'param.ParameterName = "@Contenido"
            'param.DbType = SqlDbType.Image
            'param.Size = contenidoPDF.Length
            'param.Value = contenidoPDF

            'comando.Parameters.Add(param)

            'conexion.Open()

            'respuesta = comando.ExecuteNonQuery()
            comando.ExecuteNonQuery()

            'tx.Commit()

            conexion.Close()

        Catch ex As Exception
            'tx.Rollback()
            'MsgBox("Error al insertar en la base de datos: " & ex.Message)
            Throw ex
        End Try

        Return respuesta
    End Function

    'Manejo de subreportes

    Private Sub agregarParametroSubreportesInterno(ByVal nombreParametro As String, ByVal valor As String, ByRef parametrosSR As System.Collections.ArrayList)

        Dim param As OracleParameter
        param = New OracleParameter()
        param.ParameterName = nombreParametro
        'param.SqlDbType = SqlDbType.Image
        param.Size = valor.Length
        param.Value = valor
        parametrosSR.Add(param)
    End Sub

    Public Sub agregarParametroSubreportes(ByVal nombreParametro As String, ByVal valor As String, ByVal nombreDataSourceSubreporte As String)
        Me.subreporte = True
        subreportes(0).Add(nombreParametro)
        subreportes(1).Add(valor)
        subreportes(2).Add(nombreDataSourceSubreporte)
    End Sub

    Public Sub EventoSubReporte(ByVal sender As Object, ByVal e As Microsoft.Reporting.WebForms.SubreportProcessingEventArgs)
        Dim i As Integer = 0
        Dim j As Integer = 0

        'Subreportes (0) nombre parametro
        'Subreportes (1) valor parametro
        'Subreportes (3) nombre datasource
        For i = 0 To e.DataSourceNames.Count - 1
            Dim nombreDS As String = e.DataSourceNames.Item(i)
            Dim parametrosDS As New System.Collections.ArrayList

            For j = 0 To subreportes(2).Count - 1
                If subreportes(2).Item(j) = nombreDS Then
                    agregarParametroSubreportesInterno(subreportes(0).Item(j), subreportes(1).Item(j), parametros)
                End If
            Next
            e.DataSources.Add(Me.devolverDataSourceSubreporte(nombreDS))
        Next

    End Sub



End Class
