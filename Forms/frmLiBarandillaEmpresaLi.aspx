<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false"
    CodeFile="frmLiBarandillaEmpresaLi.aspx.vb" Inherits="frmLiBarandillaEmpresaLi"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager> 
    <br />
    <div id="contenedorPrincipal" class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfSuc" runat="server" Value="" />
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <b>&nbsp;L I B R O &nbsp;&nbsp; D E &nbsp;&nbsp; I N S P E C C I O N</b>
                    </div>
                    <div class="well">
                        <div class="row">
                            <div class="col-md-5">
                                <asp:DropDownList ID="ddlSucursales" runat="server" CssClass="form-control input-sm"
                                    Width="100%" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group">
                                    <asp:TextBox ID="txtBuscarProcedimiento" runat="server" CssClass="form-control" placeholder="Ingrese fecha dd/mm/aaaa o Nro. de acta ..."></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button ID="btnBuscarProcedimiento" runat="server" Text=" >> " CssClass="form-control btn-info"
                                            Style="width: 50px;" />
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-1" style="padding-left: 10px">
                                <asp:Button ID="btnTodos" runat="server" Text="Todos" CssClass="form-control btn-info" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="grillaBandejaLibroInspeccionEmpresa" runat="server" CssClass="table table-condensed table-striped"
                                GridLines="None" Font-Bold="False" DataMember="DefaultView" TabIndex="34" EnableModelValidation="True"
                                Font-Size="X-Small" AllowPaging="True" Width="100%" AutoGenerateColumns="False"
                                DataKeyNames="ID_ACTA">
                                <Columns>
                                    <asp:BoundField DataField="NRO_ACTA" HeaderText="Número de Acta" />
                                    <asp:BoundField DataField="DOCUMENTO" HeaderText="Tipo Documento" />
                                    <asp:BoundField DataField="FECHA_HORA" HeaderText="Fecha y hora" />
                                    <asp:BoundField DataField="IDSUCURSAL" HeaderText="Sucursal" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
<%--                                            <asp:ImageButton ID="ImageButtonPdfActa" runat="server" ImageUrl="~/Images/Iconos/pdficon_small.gif"
                                                OnClick="ImageButtonPdfActa_Click" />--%>
                                            <asp:LinkButton ID="LinkButtonPdfActa" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                title="Ver documento en formato PDF ..." ForeColor="Red" 
                                                onclick="LinkButtonPdfActa_Click">
                                            <i class="fa fa-file-pdf-o fa-lg"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="gvpagination" Font-Names="Calibri" HorizontalAlign="Center" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
