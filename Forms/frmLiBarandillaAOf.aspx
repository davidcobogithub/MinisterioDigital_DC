<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false"
    CodeFile="frmLiBarandillaAOf.aspx.vb" Inherits="frmLiBarandillaAOf" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div id="contenedorPrincipal" class="container">
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>B A R A N D I L L A &nbsp;&nbsp; A L T A S &nbsp;&nbsp; D E &nbsp;&nbsp; O F I C
                    I O </b>
            </div>
            <div class="well">
                <div class="row">
                    <div class="col-md-10">
                        <div class="input-group">
                            <asp:TextBox ID="txtBuscarProcedimiento" runat="server" CssClass="form-control" placeholder="Ingrese CUIT (sin guiones), Razón Social o Número de acta manual ..."></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="btnBuscarProcedimiento" runat="server" Text="Buscar" CssClass="form-control btn-info"
                                    Style="width: 100px;" />
                            </span>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnTodos" runat="server" Text="Todos" CssClass="form-control btn-info"
                            Style="width: 100px;" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <%--Grilla   Altas de Oficio --%>
                    <asp:GridView ID="grillaBandejaAltasOficio" runat="server" CssClass="table table-condensed"
                        GridLines="None" Font-Bold="False" DataMember="DefaultView" TabIndex="34" EnableModelValidation="True"
                        Font-Size="X-Small" AllowPaging="True" Width="100%" AutoGenerateColumns="False"
                        PageSize="5">
                        <Columns>
                            <asp:BoundField DataField="ID_ACTAS_TEMP" HeaderText="ID." />
                            <asp:BoundField DataField="CUIT" HeaderText="CUIT" />
                            <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Razón Social" />
                            <asp:BoundField DataField="DOCUMENTO" HeaderText="Tipo Documento" />
                            <asp:BoundField DataField="nrodocumentomanual" HeaderText="Nro. Acta" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                            <asp:BoundField DataField="PLAZO" HeaderText="Plazo" />
                            <asp:BoundField DataField="N_TIPO_INTIMACION" HeaderText="Tipo Intimación" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonDetalleActaManual" runat="server" data-toggle="tooltip"
                                        data-placement="bottom" title="Ver documento en detalle ..." OnClick="LinkButtonDetalleActaManual_Click">
                                        <i class="fa fa-search fa-lg"></i>                                        
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="gvpagination" Font-Names="Calibri" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div id="panelDetalle" runat="server">
            <asp:UpdatePanel ID="UpdatePanelGeneral" runat="server" ChildrenAsTriggers="false"
                UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <b>D E T A L L E&nbsp;&nbsp; A C T U A C I O N &nbsp;&nbsp; M A N U A L</b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table class="table table-bordered" width="100%">
                                        <tr>
                                            <td style="vertical-align: middle">
                                                CUIT:
                                            </td>
                                            <td style="vertical-align: middle">
                                                <asp:TextBox ID="txtCuit" runat="server" class="form-control" Style="width: 100%"
                                                    placeholder="CUIT del empleador (sin guiones) ..." Enabled="False" BackColor=" #FFFFCC"
                                                    Font-Bold="True" Width="100%">
                                                </asp:TextBox>
                                                <%--                            <asp:TextBox ID="txtEstado" runat="server" Enabled="False" 
                                CssClass="form-control input-sm" BackColor="#666699" Font-Bold="True" 
                                ForeColor="White" Width="100%"></asp:TextBox>--%>
                                            </td>
                                            <td style="vertical-align: middle">
                                                Razón Social
                                            </td>
                                            <td colspan="3" style="vertical-align: middle">
                                                <asp:TextBox ID="txtRazon" runat="server" class="form-control" Style="width: 100%"
                                                    placeholder="Razón Social ..." Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: middle">
                                                Domicilio
                                            </td>
                                            <td colspan="5" style="vertical-align: middle">
                                                <asp:TextBox ID="txtDomicilio" runat="server" class="form-control" Style="width: 100%"
                                                    placeholder="Domicilio Completo ..." Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: middle">
                                                Actividad
                                            </td>
                                            <td colspan="5" style="vertical-align: middle">
                                                <asp:TextBox ID="txtActividad" runat="server" class="form-control" Style="width: 100%"
                                                    placeholder="Actividad ..." Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: middle">
                                                Tipo Documento
                                            </td>
                                            <td colspan="2" style="vertical-align: middle">
                                                <asp:TextBox ID="txtTipoDocumento" runat="server" class="form-control" Style="width: 100%"
                                                    placeholder="Tipo documento ..." Enabled="False"></asp:TextBox>
                                            </td>
                                            <td style="vertical-align: middle">
                                                Nro. Documento
                                            </td>
                                            <td colspan="2" style="vertical-align: middle">
                                                <asp:TextBox ID="txtNroActaManual" runat="server" class="form-control" Style="width: 100%"
                                                    placeholder="Número de documento manual ..." Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: middle">
                                                Inspector
                                            </td>
                                            <td colspan="5" style="vertical-align: middle">
                                                <asp:TextBox ID="txtAgente" runat="server" class="form-control" Style="width: 100%"
                                                    placeholder="Tipo documento ..." Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: middle">
                                                Fecha Actuación:
                                            </td>
                                            <td style="vertical-align: middle">
                                                <asp:TextBox ID="txtFechaActuacion" runat="server" MaxLength="10" TabIndex="4" CssClass="form-control"
                                                    placeholder="Fecha actuación" Width="100%" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td style="vertical-align: middle">
                                                Plazo
                                            </td>
                                            <td style="vertical-align: middle">
                                                <asp:TextBox ID="txtPlazo" runat="server" class="form-control" Style="width: 100%"
                                                    placeholder="Plazo en dias ..." Enabled="False"></asp:TextBox>
                                            </td>
                                            <td style="vertical-align: middle">
                                                Tipo
                                            </td>
                                            <td style="vertical-align: middle">
                                                <asp:TextBox ID="txtTipoIntimacion" runat="server" class="form-control" Style="width: 100%"
                                                    placeholder="Tipo documento ..." Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <%--                                
                                    <tr style="background-color: #FFFFCC">
                                    </tr>
                                        --%>
                                    </table>
                                </div>
                                <!-- ALTA O MODIFICACION DATOS EMPRESA-->
                            </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
