Imports CapaDatos
Imports System.Data
Imports System.Data.OracleClient
Imports System.IO
Imports System.Text
Imports System.Resources
Imports System.Globalization

Partial Class frmMultinotasEdit
    Inherits System.Web.UI.Page

    Dim cencr As New ClaseEncripta("1234")
    Dim cad1 As String = cencr.DecryptData(ConfigurationManager.ConnectionStrings("cadConOra").ToString())

    Enum acciones
        Insertar
        Modificar
        Eliminar
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("Acuerdo") = "NO" Then
            Response.Redirect("~/frmPantallaInicio1.aspx")
        End If

        If Not IsPostBack Then
            obtenermodelos_all()
            cargarDelegaciones()

            If Request.QueryString("Id") IsNot Nothing Then
                Dim clsEncr As New ClaseEncripta("1234")
                Dim str As String

                str = clsEncr.DecryptData(Request.QueryString("Id"))

                hfId.Value = str

                LoadMultinota(hfId.Value)
            Else
                Response.Redirect("~/frmPantallaInicio1.aspx")
            End If


            btnguardar.Enabled = True
            btnadjuntar.Enabled = True
            btncancelar.Enabled = True
            cbomodelos.Enabled = True
            ddlDelegacion.Enabled = True
            boxaccion.Value = acciones.Modificar

           
        End If
    End Sub

    Private Sub LoadMultinota(ByVal pId As String)
        Try
            Dim obj(1) As Object
            obj(0) = pId
            obj(1) = ""

            Dim dt As New DataTable
            dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.multinota_select_idDoc", obj).Tables(0)

            If dt.Rows.Count > 0 Then
                txtidmul.Text = dt.Rows(0)("id_multinota")
                boxidmul.Value = txtidmul.Text
                txtBox1.Text = IIf(IsDBNull(dt.Rows(0)("nota")), "", dt.Rows(0)("nota"))
                txtfecha.Text = dt.Rows(0)("fecha")
                txtano.Text = dt.Rows(0)("ano")
                txtmes.Text = dt.Rows(0)("mes")
                Me.cbomodelos.SelectedValue = dt.Rows(0)("id_mulmod")


              

                For i = 0 To dt.Rows.Count - 1

                    If BuscarAdjutnos(CInt(Session("nrocuenta")), CInt(txtidmul.Text)).Rows.Count > 0 Then
                        imgadjunto.Visible = True
                    Else
                        imgadjunto.Visible = False
                    End If


                    If dt.Rows(i)("abandeja") = 0 Then
                        btnenviar.Enabled = True
                        imgadjunto.Enabled = True
                        btnmodeloeditar.Enabled = True
                    Else
                        btnenviar.Enabled = False
                        imgadjunto.Enabled = False
                        btnmodeloeditar.Enabled = False
                    End If

                Next
                Else

            End If



        Catch ex As Exception

        End Try

    End Sub

    Private Sub obtenermodelos_all()
        Dim dt As New DataTable
        Dim obj(0) As Object
        obj(0) = ""
        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.multinotasmodelos_selectall", obj).Tables(0)
        If dt.Rows.Count > 0 Then
            cbomodelos.DataSource = dt
            cbomodelos.DataTextField = "Nombre"
            cbomodelos.DataValueField = "id_mulmod"
            cbomodelos.DataBind()
        End If
    End Sub

    Private Sub obtenermodelo(ByVal p_mulmod As Integer)
        Dim dt As New DataTable
        Dim obj(1) As Object
        obj(0) = p_mulmod
        obj(1) = ""
        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.multinotasmodelos_select", obj).Tables(0)
        If dt.Rows.Count > 0 Then
            'txtidmul.Text = dt.Rows(0)("id_mulmod")
            txtBox1.Text = IIf(IsDBNull(dt.Rows(0)("modelo")), "", dt.Rows(0)("modelo"))
            Boxmulidrel.Value = dt.Rows(0)("iddefiniciondocrel")
        End If
    End Sub

    

    Protected Sub btnguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguardar.Click
        If boxaccion.Value = acciones.Insertar Then
            Insertar(CInt(txtidmul.Text), cbomodelos.SelectedValue, txtBox1.Text, CInt(Session("nrocuenta")))
        End If
        If boxaccion.Value = acciones.Modificar Then
            Modificar(CInt(txtidmul.Text), cbomodelos.SelectedValue, txtBox1.Text, Session("nrocuenta"))
        End If


        btnguardar.Enabled = False
        btnadjuntar.Enabled = False
        btncancelar.Enabled = False
        LoadMultinota(hfId.Value)

    End Sub

    Private Sub Insertar(ByVal p_id_multinota As Integer, ByVal p_id_mul As Integer, ByVal p_nota As String, ByVal p_nrocuenta As Integer)
        'Try


        '    'If nota.Length < 4000 Then
        '    '    nota = p_nota
        '    'Else
        '    '    nota = p_nota.Substring(1, 3998)
        '    'End If

        '    OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.multinotas_inserts", p_id_multinota, p_id_mul, p_nota, p_nrocuenta)
        'Catch ex As Exception
        'End Try
        Dim conexion As New OracleConnection()

        Try

            conexion.ConnectionString = cad1
            conexion.Open()
            Dim comando As New OracleCommand()
            comando.Connection = conexion
            comando.CommandType = CommandType.StoredProcedure
            comando.CommandText = "doc_laboral.multinotas_inserts"


            Dim parIdMul As OracleParameter = New OracleParameter("p_id_multinota", System.Data.OracleClient.OracleType.Int32)
            parIdMul.Direction = ParameterDirection.InputOutput
            parIdMul.Value = p_id_multinota


            Dim parIdMulMod As OracleParameter = New OracleParameter("p_id_mulmod", System.Data.OracleClient.OracleType.Int32)
            parIdMulMod.Direction = ParameterDirection.Input
            parIdMulMod.Value = p_id_mul

            Dim parNota As System.Data.OracleClient.OracleParameter
            parNota = New System.Data.OracleClient.OracleParameter()
            parNota.ParameterName = "p_nota"
            parNota.OracleType = OracleType.Clob
            parNota.Size = p_nota.Length
            parNota.Value = p_nota
            parNota.Direction = ParameterDirection.Input

            Dim parNroCuenta As OracleParameter = New OracleParameter("p_nrocuenta", System.Data.OracleClient.OracleType.Int32)
            parIdMulMod.Direction = ParameterDirection.Input
            parNroCuenta.Value = p_nrocuenta

            comando.Parameters.Add(parIdMul)
            comando.Parameters.Add(parIdMulMod)
            comando.Parameters.Add(parNota)
            comando.Parameters.Add(parNroCuenta)


            comando.ExecuteNonQuery()

        Catch ex As Exception

        Finally
            conexion.Close()
        End Try



    End Sub

    Private Sub Eliminar(ByVal p_id_multinota As Integer, ByVal p_nrocuenta As Integer)
        Try
            OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.multinotas_delete", p_id_multinota, p_nrocuenta)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Modificar(ByVal p_id_multinota As Integer, ByVal p_id_mulmod As Integer, ByVal p_nota As String, ByVal p_nrocuenta As Integer)


        Dim conexion As New OracleConnection
        Try
            conexion.ConnectionString = cad1
            conexion.Open()
            Dim comando As New OracleCommand()
            comando.Connection = conexion
            comando.CommandType = CommandType.StoredProcedure
            comando.CommandText = "doc_laboral.multinotas_update"



            Dim parIdMul As OracleParameter = New OracleParameter("p_id_multinota", System.Data.OracleClient.OracleType.Int32)
            parIdMul.Direction = ParameterDirection.InputOutput
            parIdMul.Value = p_id_multinota


            Dim parIdMulMod As OracleParameter = New OracleParameter("p_id_mulmod", System.Data.OracleClient.OracleType.Int32)
            parIdMulMod.Direction = ParameterDirection.Input
            parIdMulMod.Value = p_id_mulmod

            Dim parNota As System.Data.OracleClient.OracleParameter
            parNota = New System.Data.OracleClient.OracleParameter()
            parNota.ParameterName = "p_nota"
            parNota.OracleType = OracleType.Clob
            parNota.Size = p_nota.Length
            parNota.Value = p_nota
            parNota.Direction = ParameterDirection.Input

            Dim parNroCuenta As OracleParameter = New OracleParameter("p_nrocuenta", System.Data.OracleClient.OracleType.Int32)
            parIdMulMod.Direction = ParameterDirection.Input
            parNroCuenta.Value = p_nrocuenta

            comando.Parameters.Add(parIdMul)
            comando.Parameters.Add(parIdMulMod)
            comando.Parameters.Add(parNota)
            comando.Parameters.Add(parNroCuenta)


            comando.ExecuteNonQuery()

        Catch ex As Exception

        Finally
            conexion.Close()
        End Try



    End Sub

    Protected Sub OnConfirm(ByVal sender As Object, ByVal e As EventArgs)
        boxaccion.Value = acciones.Eliminar
        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim idMul As Integer
        idMul = Convert.ToInt32(row.Cells(0).Text)
        boxidmul.Value = idMul

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue.Substring(0, 2) = "Ye" Then

            If EstaEnBandejaMultinota(Session("nrocuenta"), idMul) = 0 Then
                Eliminar(boxidmul.Value, CInt(Session("nrocuenta")))
                btnguardar.Enabled = False
                btnadjuntar.Enabled = False
                cbomodelos.Enabled = False
                txtidmul.Text = ""
            Else
                AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "script1", "alert('No se Puede Eliminar La Multinota Ya que fue Presentada');", True)



            End If

        Else

        End If
    End Sub

    Protected Sub btnmodeloeditar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmodeloeditar.Click
        boxaccion.Value = acciones.Modificar

        Dim idMul As Integer
        idMul = Convert.ToInt32(txtidmul.Text)
        boxidmul.Value = idMul
        obtenernotaxid(idMul, Session("nrocuenta"))
        btnguardar.Enabled = True
        btnadjuntar.Enabled = True
        btncancelar.Enabled = True
        cbomodelos.Enabled = True
        ddlDelegacion.Enabled = True

    End Sub

  

    Protected Sub btnenviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnenviar.Click

        Dim idMul As Integer
        idMul = Convert.ToInt32(txtidmul.Text)
        boxidmul.Value = idMul
        boxmes.Value = Convert.ToInt32(txtmes.Text)
        boxano.Value = Convert.ToInt32(txtano.Text)
        Session("mulmes") = boxmes.Value
        Session("mulano") = boxano.Value
        'Session("contenidomultinota") = parseRTFtoTEXT.StripHTML(txtBox1.Text)
        Session("contenidomultinota") = txtBox1.Text
        Session("idmul") = idMul
        Session("mulidrel") = Boxmulidrel.Value
        Session("IdBarandillaMul") = hfId.Value
        btnenviar.Enabled = False
        imgadjunto.Enabled = False

        Dim popupScript4 As String
        popupScript4 = String.Empty

        popupScript4 = "<script language='JavaScript'>"
        popupScript4 += "window.open('frmReportemultinota.aspx?coddel=" + ddlDelegacion.SelectedValue + "&acc=1', 'CustomPopUp2', "
        popupScript4 += "'top=15,left=250 ,width=850, height=600, menubar=yes, scrollbars=yes ,resizable=no');"
        popupScript4 += "</script>"

        AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "CustomPopUp2", popupScript4, False)




    End Sub

    Private Sub obtenernotaxid(ByVal p_idnota As Integer, ByVal p_nrocuenta As Integer)
        Dim dt As New DataTable
        Dim obj(2) As Object
        obj(0) = p_nrocuenta
        obj(1) = p_idnota
        obj(2) = ""
        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.multinota_select_id_xcta", obj).Tables(0)
        If dt.Rows.Count > 0 Then
            txtidmul.Text = dt.Rows(0)("id_multinota")
            boxidmul.Value = txtidmul.Text
            txtBox1.Text = IIf(IsDBNull(dt.Rows(0)("nota")), "", dt.Rows(0)("nota"))
            Me.cbomodelos.SelectedValue = dt.Rows(0)("id_mulmod")
        End If
    End Sub



    Private Sub InsertarMultinotaEnBandeja()


        Dim fileBytes1 As New BinaryReader(New MemoryStream(File.ReadAllBytes(txtBox1.Text.ToString())))
        Dim multinotabyte(fileBytes1.BaseStream.Length) As Byte


        Dim tempBuff(fileBytes1.BaseStream.Length) As Byte
        tempBuff = multinotabyte



        Dim cadena As String = cad1

        Dim campoid As String = "Id"
        Dim campocontenido As String = "FOriginal"
        Dim camponombrearchivo As String = "Id"
        Dim tabla As String = "Doc_laboral.BarandillaDocumentoPDF"


        If guardarDocEnBD1(tempBuff, cad1, tabla, Session("nrocuenta"), boxano.Value, boxmes.Value, 16, "", 0, "NULL") = True Then

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mensaje", "alert('Documento importado correctamente.');", True)
        Else

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mensaje", "alert('Error al importar documento.');", True)
            Return

        End If


    End Sub

    Public Function guardarDocEnBD1(ByVal pContenido() As Byte, ByVal pcadenaCon As String, ByVal ptabla As String, ByVal pNrocuenta As String, ByVal pAno As String, ByVal pMes As String, ByVal pIdDefinicionDocumento As String, ByVal pIdsucursal As String, ByVal pSecuencia As String, ByVal pIdCodigoPago As String) As Boolean
        Dim errorString As String
        Dim VarError As Boolean = True
        Dim id As Int64 = 0
        Dim b As Boolean = True
        Dim cantHojas As Int64 = 0

        Dim trx As OracleTransaction = Nothing

        Try

            Dim cConfBaneja As New ClaseConfBandeja
            Dim conexion As New OracleConnection(pcadenaCon)
            Dim comando As New OracleCommand("INSERT INTO " & ptabla & " (idbarandilladocumento,FOriginal,nrocuenta,ano,mes,IdDefinicionDocumentos,Fecha,idSucursal,Secuencia,Id_CodigoPago,IDTIPOLIQDOC,observacion,canthojas) values (doc_laboral.SEC_BARANDILLADOC.nextval,:pContenido," & pNrocuenta & "," & pAno & "," & pMes & "," & pIdDefinicionDocumento & ",sysdate,'" & pIdsucursal & "','" & pSecuencia & "'," & pIdCodigoPago & "," & "" & ",'" & "" & "'," + cantHojas.ToString() + ") returning idbarandilladocumento into :p1", conexion)
            comando.CommandType = CommandType.Text


            Dim param As System.Data.OracleClient.OracleParameter
            param = New System.Data.OracleClient.OracleParameter()
            param.ParameterName = ":pContenido"
            param.OracleType = OracleType.Blob
            param.Size = pContenido.Length
            param.Value = pContenido

            Dim p1 As OracleParameter = New OracleParameter("p1", System.Data.OracleClient.OracleType.Int32)
            p1.Direction = ParameterDirection.Output

            comando.Parameters.Add(p1)
            comando.Parameters.Add(param)

            conexion.Open()

            trx = conexion.BeginTransaction()
            comando.Transaction = trx

            comando.ExecuteNonQuery()
            id = p1.Value

            comando.Parameters.Clear()

            'pContenido = insertarTextoEnDocumento(pContenido, "Documento Nro:" & id.ToString())

            'cConfBaneja.asignarDocumentoAUsuario(cmbDocumentoASubir.SelectedValue, id, ddlDelegacion.SelectedValue, trx)

            trx.Commit()

            errorString = ""
            conexion.Close()

        Catch ex As Exception
            errorString = ex.Message
            trx.Rollback()
            b = False
        Finally
            trx.Dispose()
        End Try

        Return b

    End Function


    Public Function guardarAdjunto(ByVal pContenido() As Byte, ByVal pcadenaCon As String, ByVal ptabla As String, ByVal pNrocuenta As String, ByVal pAno As String, ByVal pMes As String, ByVal pIdmultinota As String) As Boolean
        Dim errorString As String
        Dim VarError As Boolean = True
        Dim id As Int64 = 0
        Dim b As Boolean = True
        Dim cantHojas As Int64 = 0

        Dim trx As OracleTransaction = Nothing

        Try

            Dim cConfBaneja As New ClaseConfBandeja
            Dim conexion As New OracleConnection(pcadenaCon)
            Dim comando As New OracleCommand("INSERT INTO " & ptabla & " (id_muladj, id_multinota, nrocuenta, adjunto, mes, ano) values (doc_laboral.seq_multinotasadjuntos.nextval, " & pIdmultinota & " ," & pNrocuenta & ", :pContenido," & pMes & "," & pAno & ") returning id_muladj into :p1", conexion)
            comando.CommandType = CommandType.Text

            Dim p1 As OracleParameter = New OracleParameter("p1", System.Data.OracleClient.OracleType.Int32)
            p1.Direction = ParameterDirection.Output

            Dim param As System.Data.OracleClient.OracleParameter
            param = New System.Data.OracleClient.OracleParameter()
            param.ParameterName = ":pContenido"
            param.OracleType = OracleType.Blob
            param.Size = pContenido.Length
            param.Value = pContenido

            comando.Parameters.Add(p1)
            comando.Parameters.Add(param)

            conexion.Open()

            trx = conexion.BeginTransaction()
            comando.Transaction = trx

            comando.ExecuteNonQuery()
            id = p1.Value

            comando.Parameters.Clear()

            trx.Commit()

            errorString = ""
            conexion.Close()

        Catch ex As Exception
            errorString = ex.Message
            trx.Rollback()
            b = False
        Finally
            trx.Dispose()
        End Try

        Return b

    End Function

    Protected Sub cbomodelos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbomodelos.SelectedIndexChanged
        '  obtenermodelo(cbomodelos.SelectedValue)
    End Sub

    Protected Sub cbomodelos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbomodelos.Load
        'If Not IsPostBack Then
        '    obtenermodelo(cbomodelos.SelectedValue)
        'End If
    End Sub

    Protected Sub btnadjuntar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadjuntar.Click
        Me.paneladjunto.Visible = True
        'ModalPopupExtender2.Show()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModal", "$('#myModal2').modal()", True)
    End Sub

    Protected Sub btnsubir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubir.Click
        Dim files As HttpFileCollection = Request.Files
        For i As Integer = 0 To files.Count - 1
            Dim postedFile As HttpPostedFile = files(i)
            If postedFile.ContentLength > 0 Then

                postedFile.SaveAs(Server.MapPath("PDFtemporal\") & Path.GetFileName(Session("nrocuenta").ToString() & postedFile.FileName))

                'Dim tempBuff(FileUpload1.FileBytes.Length) As Byte
                'tempBuff = FileUpload1.FileBytes

                Dim tempBuff() As Byte
                tempBuff = FileToByteArray(Server.MapPath("PDFtemporal\") & Path.GetFileName(Session("nrocuenta").ToString() & postedFile.FileName))

                Dim cadena As String = cad1

                Dim campoid As String = "Id_muladj"
                Dim campomultinota As String = "id_multinota"
                Dim camponrocuenta As String = "nrocuenta"
                Dim campocontenido As String = "Adjunto"
                Dim campomes As String = "mes"
                Dim campoano As String = "Ano"
                Dim tabla As String = "doc_laboral.multinotasadjuntos"

                If System.IO.Path.GetExtension(FileUpload1.FileName.Trim()).Trim = ".pdf" Then
                    If guardarAdjunto(tempBuff, cad1, tabla, Session("nrocuenta"), Today.Year, Today.Month, boxidmul.Value) = True Then

                        File.Delete((Server.MapPath("PDFtemporal\") & Path.GetFileName(Session("nrocuenta").ToString() & postedFile.FileName)))

                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "mensaje", "alert('Documento Adjuntado correctamente.');", True)
                    Else

                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "mensaje", "alert('Error al Adjuntar documento.');", True)
                        Return

                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "mensaje", "alert('El Documento debe ser un PDF.');", True)
                End If

            End If
        Next i
        paneladjunto.Visible = False
        btnadjuntar.Enabled = False

    End Sub


    Public Function FileToByteArray(ByVal _FileName As String) As Byte()
        Dim _Buffer As Byte() = Nothing

        Try
            ' Open file for reading
            Dim _FileStream As New System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)

            ' attach filestream to binary reader
            Dim _BinaryReader As New System.IO.BinaryReader(_FileStream)

            ' get total byte length of the file
            Dim _TotalBytes As Long = New System.IO.FileInfo(_FileName).Length

            ' read entire file into buffer
            '_Buffer = _BinaryReader.ReadBytes(DirectCast(_TotalBytes, Int32))
            _Buffer = _BinaryReader.ReadBytes(CInt(_TotalBytes))

            ' close file reader
            _FileStream.Close()
            _FileStream.Dispose()
            _BinaryReader.Close()
        Catch _Exception As Exception
            ' Error
            Console.WriteLine("Exception caught in process: {0}", _Exception.ToString())
        End Try

        Return _Buffer
    End Function

    Protected Sub btncerrar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btncerrar.Click
        paneladjunto.Visible = False
    End Sub

    Private Function BuscarAdjutnos(ByVal p_nrocuenta As Integer, ByVal p_idmultinota As Integer) As DataTable
        Dim dt As New DataTable
        Dim param(2) As Object
        param(0) = p_nrocuenta
        param(1) = p_idmultinota
        param(2) = ""
        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.multinotasadj_selectall", param).Tables(0)
        Return dt
    End Function
    Private Function BuscarAdjuntosxID() As DataTable
        Dim dt As New DataTable
        Return dt
    End Function

    Protected Sub imgadjunto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgadjunto.Click
        Dim idMul As Integer
        idMul = Convert.ToInt32(txtidmul.Text)
        boxidmul.Value = idMul
        'ModalPopupExtender1.Show()
        GrillaAdj.DataSource = BuscarAdjutnos(Session("nrocuenta"), idMul)
        GrillaAdj.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModal", "$('#myModal').modal()", True)
    End Sub

    Protected Sub imgpdf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim idAdj As Integer
        idAdj = Convert.ToInt32(row.Cells(0).Text)

        Dim contenido As Byte() = Nothing
        Dim dt As New DataTable
        Dim param(3) As Object
        param(0) = Session("nrocuenta")
        param(1) = boxidmul.Value
        param(2) = idAdj
        param(3) = ""
        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.multinotasadj_selectID", param).Tables(0)
        contenido = dt.Rows(0)("adjunto")

        'Response.AddHeader("content-disposition", contenido.ToString())
        Response.AddHeader("content-disposition", "attachment; filename=Adjunto.pdf")
        Response.ContentType = "application/pdf"
        Response.BinaryWrite(contenido)
        Response.End()
    End Sub


    Protected Sub btneliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim btn As ImageButton = CType(sender, ImageButton)
        Dim row As GridViewRow = CType(btn.NamingContainer, GridViewRow)
        Dim idAdj As Integer
        idAdj = Convert.ToInt32(row.Cells(0).Text)
        OracleHelper.ExecuteNonQuery(cad1, "doc_laboral.multinotasadjuntos_DELETE", idAdj, Session("nrocuenta"))
        GrillaAdj.DataSource = BuscarAdjutnos(Session("nrocuenta"), boxidmul.Value)
        GrillaAdj.DataBind()
        ' ModalPopupExtender1.Show()
    End Sub

    Protected Sub btncancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancelar.Click
        btnguardar.Enabled = False
        btnadjuntar.Enabled = False
        btncancelar.Enabled = False
        cbomodelos.Enabled = False
        ddlDelegacion.Enabled = False
        paneladjunto.Visible = False
    End Sub


    Public Sub cargarDelegaciones()
        Dim dt As New DataTable
        Dim obj(0) As Object
        obj(0) = ""

        dt = OracleHelper.ExecuteDataset(cad1, "doc_laboral.delegacionGetAllEnabled", obj).Tables(0)

        ddlDelegacion.DataTextField = "Delegacion"
        ddlDelegacion.DataValueField = "coddelegacion"
        ddlDelegacion.DataSource = dt
        ddlDelegacion.DataBind()
        ddlDelegacion.Items.Insert(0, "--Seleccione Una Delegación--")
        ddlDelegacion.SelectedIndex = 0

        Dim obj1(1) As Object
        obj1(0) = Session("nrocuenta")
        obj1(1) = ""
        Dim dt1 As New DataTable
        dt1 = OracleHelper.ExecuteDataset(cad1, "doc_laboral.delegacionPorDefectoMultinota", obj1).Tables(0)

        Dim delDefecto As String = ""
        If dt1.Rows.Count > 0 Then
            delDefecto = dt1.Rows(0)(0).ToString()
        End If

        ddlDelegacion.SelectedValue = delDefecto

    End Sub

    Public Function EstaEnBandejaMultinota(ByVal pnrocuenta As Integer, ByVal pidmul As Integer) As Integer

        Dim rta As Integer
        Dim conexion As New OracleConnection(cad1)
        Dim comando As New OracleCommand()

        Try

            comando.CommandType = CommandType.StoredProcedure
            comando.Connection = conexion
            comando.CommandText = "F_EnbandejaMultinota"
            conexion.Open()

            Dim v_nrocuenta As OracleParameter = New OracleParameter("pnrocuenta", System.Data.OracleClient.OracleType.Int32)
            v_nrocuenta.Value = pnrocuenta
            comando.Parameters.Add(v_nrocuenta)

            Dim v_idmul As OracleParameter = New OracleParameter("pidmul", System.Data.OracleClient.OracleType.Int32)
            v_idmul.Value = pidmul
            comando.Parameters.Add(v_idmul)

            Dim o_resultado As OracleParameter = New OracleParameter("o_resultado", System.Data.OracleClient.OracleType.Int32)
            o_resultado.Direction = ParameterDirection.ReturnValue
            comando.Parameters.Add(o_resultado)

            comando.ExecuteNonQuery()
            rta = o_resultado.Value.ToString().Trim()

        Catch ex As Exception

        Finally
            comando.Dispose()
            conexion.Close()
            conexion.Dispose()
        End Try

        Return rta
    End Function

   
    
   
End Class



