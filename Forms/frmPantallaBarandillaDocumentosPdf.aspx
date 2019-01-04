<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false"
    CodeFile="frmPantallaBarandillaDocumentosPdf.aspx.vb" Inherits="frmPantallaBarandillaDocumentosPdf"
    Title="BANDEJA DOCUMENTOS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hfIdSel" runat="server" />
    <asp:HiddenField ID="hfAnoSel" runat="server" />
    <asp:HiddenField ID="hfMesSel" runat="server" />
    <asp:HiddenField ID="hfCuit" runat="server" />
    <asp:HiddenField ID="hfNroCuenta" runat="server" />
    <asp:HiddenField ID="hfrazonsocial" runat="server" />
    <asp:HiddenField ID="hfdomsue" runat="server" />
    <br />
    <asp:Panel ID="PanelContenedorPrincipal" runat="server" Visible="False">
        <table style="width: 700px; font-family: Droid Sans;">
            <tr>
                <td style="width: 700px" align="center">
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:GridView ID="gvPeriodos" runat="server" CssClass="tablabandeja" ForeColor="#222222"
                                    Style="margin-right: 20px;" AutoGenerateColumns="False" BackColor="White" CellPadding="4"
                                    GridLines="Horizontal" Font-Names="Droid Sans" Font-Size="13px" Font-Bold="False"
                                    AllowPaging="True" PageSize="4">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgVer" runat="server" CssClass="icons" ImageUrl="~/Images/Iconos/nuevo.png"
                                                    OnClick="imgVer_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ano" HeaderText="A&#241;o">
                                            <ItemStyle Font-Size="13px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mes" HeaderText="Mes">
                                            <ItemStyle Font-Size="13px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Documento" HeaderText="Documento">
                                            <ItemStyle Font-Size="13px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad">
                                            <ItemStyle Font-Size="13px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IdDefinicionDocumento" HeaderText="Id">
                                            <ItemStyle Font-Size="13px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EEEEEE" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#996699" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                </asp:GridView>
                            </td>
                            <td valign="top">
                                <table style="width: 300px" cellspacing="0" class="tablabandeja">
                                    <tr>
                                        <th colspan="4" style="" align="left">
                                            <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="White" Text="Referencias">
                                            </asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px; background-color: #EEEEEE; height: 29px">
                                            <asp:Image ID="Image6" runat="server" CssClass="icons" ImageUrl="~/Images/Iconos/pdf.png" />
                                        </td>
                                        <td style="width: 250px; background-color: #EEEEEE;" align="left" cssclass="iconitem">
                                            <asp:Label ID="Label9" runat="server" CssClass="iconitem" Text="Abrir documento"
                                                Width="104px">
                                            </asp:Label>
                                        </td>
                                        <td style="width: 50px; background-color: #EEEEEE; height: 29px">
                                            <asp:Image ID="Image7" runat="server" CssClass="icons" ImageUrl="~/Images/Iconos/documento-firmado.png" />
                                        </td>
                                        <td style="width: 250px; background-color: #EEEEEE;" align="left" cssclass="iconitem">
                                            <asp:Label ID="Label12" runat="server" CssClass="iconitem" Text="Documento firmado digitalmente"
                                                Width="184px">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px; background-color: Gainsboro; height: 29px">
                                            <asp:Image ID="Image8" runat="server" CssClass="icons" ImageUrl="~/Images/Iconos/documento-sin-firmar.png" />
                                        </td>
                                        <td style="width: 250px; background-color: Gainsboro;" align="left">
                                            <asp:Label ID="Label13" runat="server" CssClass="iconitem" Text="Documento pendientes de firmar"
                                                Width="184px">
                                            </asp:Label>
                                        </td>
                                        <td style="width: 50px; background-color: Gainsboro; height: 30px">
                                            <asp:Image ID="Image4" runat="server" CssClass="icons" ImageUrl="~/Images/Iconos/firmado-ministerio.png" />
                                        </td>
                                        <td style="width: 250px; background-color: Gainsboro;" align="left">
                                            <asp:Label ID="Label1" runat="server" CssClass="iconitem" Text="Documento firmado por el ministerio"
                                                Width="208px">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px; background-color: #EEEEEE; height: 29px">
                                            <asp:Image ID="Image9" runat="server" CssClass="icons" ImageUrl="~/Images/Iconos/generar-volante.png" />
                                        </td>
                                        <td style="width: 250px; background-color: #EEEEEE;" align="left">
                                            <asp:Label ID="Label14" runat="server" CssClass="iconitem" Text="Generar volante banco">
                                            </asp:Label>
                                        </td>
                                        <td style="width: 50px; background-color: #EEEEEE; height: 29px">
                                            <asp:ImageButton ID="ImgObs1" runat="server" CssClass="icons" ImageUrl="~/Images/Iconos/documento-observaciones.png" />
                                        </td>
                                        <td style="width: 250px; background-color: #EEEEEE;" align="left">
                                            <asp:Label ID="Label2" runat="server" CssClass="iconitem" Text="Documento con observaciones">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px; background-color: Gainsboro; height: 30px">
                                            <asp:ImageButton ID="btnFecha" runat="server" CssClass="icons" ImageUrl="~/Images/Iconos/acuse-recibo.png" />
                                        </td>
                                        <td style="width: 250px; background-color: Gainsboro;" align="left">
                                            <asp:Label ID="Label3" runat="server" CssClass="iconitem" Text="Acuse de recibo"></asp:Label>
                                        </td>
                                        <td style="width: 50px; background-color: Gainsboro; height: 30px">
                                            <asp:ImageButton ID="ImageButton1" runat="server" CssClass="icons" Visible="True"
                                                ImageUrl="~/Images/Iconos/eliminar.png" />
                                        </td>
                                        <td style="width: 250px; background-color: Gainsboro;" align="left">
                                            <asp:Label ID="Label4" runat="server" CssClass="iconitem" Text="Eliminar Documento"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px; background-color: #EEEEEE; height: 29px">
                                            <asp:ImageButton ID="ibPagado" runat="server" CssClass="icons" Visible="True" ImageUrl="~/Images/Iconos/volante-pago-abonado.png" />
                                        </td>
                                        <td style="width: 250px; background-color: #EEEEEE;" align="left">
                                            <asp:Label ID="Label5" runat="server" CssClass="iconitem" Text="Volante de Pago Abonado">
                                            </asp:Label>
                                        </td>
                                        <td style="width: 50px; background-color: #EEEEEE; height: 29px">
                                            <asp:ImageButton ID="ImageButton2" runat="server" CssClass="icons" Visible="True"
                                                ImageUrl="~/Images/Iconos/advertencia.PNG" />
                                        </td>
                                        <td style="width: 250px; background-color: #EEEEEE;" align="left">
                                            <asp:Label ID="Label8" runat="server" CssClass="iconitem" Text="Documento fuera de término">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px; background-color: Gainsboro; height: 29px">
                                            <asp:ImageButton ID="ImageButton3" runat="server" CssClass="icons" Visible="True"
                                                ImageUrl="~/Images/Iconos/QR.png" />
                                        </td>
                                        <td style="width: 250px; background-color: Gainsboro;" align="left">
                                            <asp:Label ID="Label10" runat="server" CssClass="iconitem" Text="Código QR">
                                            </asp:Label>
                                        </td>
                                        <td style="width: 50px; background-color: Gainsboro; height: 29px">
                                            <asp:ImageButton ID="ImageButton4" runat="server" CssClass="icons" Visible="True"
                                                ImageUrl="~/Images/Iconos/diagrama-trabajo.png" />
                                        </td>
                                        <td style="width: 250px; background-color: Gainsboro;" align="left">
                                            <asp:Label ID="Label11" runat="server" CssClass="iconitem" Text="Diagrama de Trabajo">
                                            </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; width: 700px; height: 4px;">
                </td>
            </tr>
            <tr>
                <td style="border: 0;">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GrdPdf" runat="server" Font-Size="X-Small" Font-Bold="False" BackColor="White"
                                Font-Names="Droid Sans" AllowPaging="True" GridLines="Horizontal" CellPadding="3"
                                AutoGenerateColumns="False" EnableModelValidation="True" 
                                CssClass="tablabandeja">
                                <Columns>
                                    <asp:BoundField DataField="idbarandilladocumento">
                                        <HeaderStyle Font-Bold="True" Font-Names="Droid Sans" Font-Size="X-Small"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha">
                                        <HeaderStyle Font-Bold="False" Font-Names="Droid Sans" Font-Size="X-Small"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Documento" HeaderText="Documento">
                                        <HeaderStyle Font-Bold="False" Font-Names="Droid Sans" Font-Size="X-Small"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Ano" HeaderText="A&#241;o">
                                        <HeaderStyle Font-Bold="False" Font-Names="Droid Sans" Font-Size="X-Small"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Mes" HeaderText="Mes">
                                        <HeaderStyle Font-Bold="False" Font-Names="Droid Sans" Font-Size="X-Small"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipoLiquidacion" HeaderText="Tipo Liquidacion">
                                        <HeaderStyle Font-Bold="False" Font-Names="Droid Sans" Font-Size="X-Small"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField ItemStyle-Width="200px" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%--<asp:ImageButton id="btnFirmar" onclick="btnFirmar_Click" runat="server" Width="20px" Height="20px" ImageUrl="~/Images/Iconos/documento_sinfirma_grd.ico"></asp:ImageButton> --%>
                                            <asp:ImageButton ID="btnFirmar" runat="server" CssClass="icons" ImageUrl="~/Images/Iconos/documento-sin-firmar.png">
                                            </asp:ImageButton>&nbsp
                                            <asp:Image ID="ImgFirmado" runat="server" CssClass="icons" ImageUrl="~/Images/Iconos/documento-firmado.png">
                                            </asp:Image>&nbsp
                                            <asp:Image ID="imgFirmadoMin" runat="server" CssClass="icons" ImageUrl="~/Images/Iconos/firmado-ministerio.png">
                                            </asp:Image>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnPdf" CssClass="icons" runat="server" ImageUrl="~/Images/Iconos/pdf.png"
                                                OnClick="btnPdf_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Presentar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPresentar" runat="server" Text="" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="X-Small"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FechaPresentacion" HeaderText="Fecha de presentaci&#243;n">
                                        <HeaderStyle Font-Names="Droid Sans" Font-Size="X-Small"></HeaderStyle>
                                        <ItemStyle Font-Names="Droid Sans" Font-Size="X-Small"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Fecha de presentaci&#243;n" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFecha" runat="server" Font-Names="Droid Sans" Font-Size="X-Small"
                                                Text="Label" Visible="False"> </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Names="Droid Sans" Font-Size="X-Small"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ver">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnFecha" CssClass="icons" runat="server" Visible="False" ImageUrl="~/Images/Iconos/acuse-recibo.png"
                                                OnClick="btnFecha_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Names="Droid Sans" Font-Size="X-Small"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEmitirNota" CssClass="icons" runat="server" ImageUrl="~/Images/Iconos/generar-volante.png"
                                                OnClick="btnEmitirNota_Click" />
                                            <asp:ImageButton ID="ibPagado" CssClass="icons" runat="server" Visible="True" ImageUrl="~/Images/Iconos/volante-pago-abonado.png"
                                                OnClick="ibPagado_Click" />
                                            <asp:ImageButton ID="ibEliminar" CssClass="icons" runat="server" Visible="True" ImageUrl="~/Images/Iconos/eliminar.png"
                                                OnClick="ibEliminar_Click" />
                                            <asp:ImageButton ID="ibFT" CssClass="icons" runat="server" Visible="False" ImageUrl="~/Images/Iconos/advertencia.png" />
                                            <asp:ImageButton ID="btnQR" runat="server" ImageUrl="~/Images/Iconos/QR.png" OnClick="btnQR_Click"
                                                Visible="False" />
                                        </ItemTemplate>
                                        <ItemStyle Width="300px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Observaciones">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgObs" runat="server" CssClass="icons" Visible="false" ImageUrl="~/Images/Iconos/documento-observaciones.png"
                                                OnClick="ImgObs_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Diagrama de Trabajo">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibDiagTrab" runat="server" CssClass="icons" OnClick="ibDiagTrab_Click"
                                                Visible="false" ImageUrl="~/Images/Iconos/diagrama-trabajo.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="id_codigopago" HeaderText="id_codigopago" Visible="False">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EstadoDesc" HeaderText="Estado"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnAccion" runat="server" Text="Accion" Visible="False" OnClick="btnAccion_Click"
                                                CssClass="button" Width="60px"></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Firmar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkFirmar" runat="server" Visible="False" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="cantidadfirmas" HeaderText="cantidadfirmas" Visible="False">
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#996699" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle HorizontalAlign="Center" />
                            </asp:GridView>
                            <asp:Timer ID="timerGrilla" runat="server" OnTick="timerGrilla_Tick" Interval="10000">
                            </asp:Timer>
                            <asp:Panel Style="display: none;" ID="pVolantePago" runat="server" Width="1000px"
                                CssClass="panel" Font-Names="Droid Sans" HorizontalAlign="Center">
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td align="center">
                                            <asp:HiddenField ID="hfConSel" runat="server" />
                                            <asp:HiddenField ID="hfFVigencia" runat="server" />
                                            <table id="comprobante">
                                                <tr class="false-col">
                                                    <td align="right">
                                                        <asp:Label ID="Label6" runat="server" Text="Concepto:"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <label id="lblConPadre" runat="server">
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblTitConcepto" runat="server" Text="Sub-Concepto:"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblConcepto" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="false-col">
                                                    <td align="right">
                                                        <asp:Label ID="lblTitCant" runat="server" Text="Cantidad de Hojas:"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblCantHojas" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblTitPrecio" runat="server" Text="Costo:"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblPrecio" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="false-col">
                                                    <td align="right">
                                                        <asp:Label ID="lblEstadoTit" runat="server" Text="Estado Pago:"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblEstado" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnGenerar" runat="server" Text="Generar Comprobante" ValidationGroup="gbar"
                                                            OnClick="btnGenerar_Click" Width="152px" CssClass="button green" />&nbsp
                                                        <asp:Button ID="btnGenerarPagoLink" runat="server" Text="Generar Pago Link" ValidationGroup="gbar"
                                                            OnClick="btnGenerarPagoLink_Click" Width="152px" CssClass="button green" />
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cerrar" OnClick="btnCancel_Click"
                                                            CssClass="button" Width="100px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                    DisplayAfter="100">
                                    <ProgressTemplate>
                                        <div class="PantallaCargaFondo">
                                        </div>
                                        <div class="PantallaCarga">
                                            <div class="PantallaCargaPanel">
                                                <div class="PantallaCargaTexto">
                                                    <asp:Label ID="lblLoader" runat="server" Text="Cargando... Por favor espere un momento."></asp:Label></div>
                                                <div class="PantallaCargaImagen">
                                                    <img src="Images/ajax-loader.gif" alt="" /></div>
                                            </div>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                            <asp:Button ID="btnOculto" runat="server" Text="Oculto" CssClass="Oculto"></asp:Button>
                            <asp:HiddenField ID="hfDocSel" runat="server"></asp:HiddenField>
                            <asp:ModalPopupExtender ID="mpeVolante" runat="server" BackgroundCssClass="FondoAplicacion"
                                PopupControlID="pVolantePago" TargetControlID="btnOculto">
                            </asp:ModalPopupExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="right" style="text-align: right;">
                    <%--<asp:Button ID="Button1" runat="server" Text="Presentar" Font-Names="Arial" Font-Size="Small" style="font-size: 9pt; font-family: Arial" Visible="False" />--%>
                    <asp:ImageButton ID="ibPresentar" CssClass="icons" runat="server" ToolTip="Presentar"
                        ImageUrl="~/Images/chico-presentar.png" />
                    &nbsp;
                    <asp:ImageButton ID="ibFJava" runat="server" CssClass="icons" ToolTip="Firmar con Java"
                        ImageUrl="~/Images/chico-java.png" Width="92px" Height="33px" Visible="false" />
                    &nbsp;&nbsp;<asp:ImageButton ID="ibFAct" CssClass="icons" runat="server" ToolTip="Firmar con ActiveX"
                        ImageUrl="~/Images/chico-activex.png" Width="92px" Height="33px" Visible="false" />
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="ibFWeb" CssClass="icons" runat="server" ToolTip="Firmar con Web Star" ImageUrl="~/Images/chico-WebStart.png" Width="92px" Height="33px" Visible="false"/>
                </td>
            </tr>
            <tr>
                <td style="width: 700px; height: 21px">
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Names="Droid Sans"
                        Font-Size="Small" ForeColor="#C00000" Text="Documentos presentados correctamente"
                        Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
        <%--Panel Opción Digital  --------------------------  --%>
        <asp:Panel ID="panelOpcionDigital" runat="server" CssClass="wmodalPopup" style="display: none"  BackColor="White"
            Width="300px">
            <div class="header" >
                Confirmación
            </div>
            <div class="body">
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <div style="padding: 5px; text-align: left">
                                <p>
                                    ¿Desea cambiar al sistema de presentación digital con presentación en hojas moviles con uso de firma digital ?
                                </p>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="footer" align="right">
                <asp:Button ID="btnAceptarDigital" runat="server" Text="SI" CssClass="btn btn-primary" />
                &nbsp;&nbsp;
                <asp:Button ID="btnCancelarPopup" runat="server" Text="NO" CssClass="btn btn-primary" />
            </div>
        </asp:Panel>
        <asp:ModalPopupExtender ID="modalPanelOpcionDigital" runat="server" TargetControlID="btnoculto99"
            PopupControlID="panelOpcionDigital" BackgroundCssClass="modalBackground" DropShadow="false">
        </asp:ModalPopupExtender>
        <asp:Button ID="btnoculto99" runat="server" Text="btnoculto99" Style="display: none;" />

</asp:Content>
