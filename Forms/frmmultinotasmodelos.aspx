<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="frmmultinotasmodelos.aspx.vb" Inherits="frmmultinotasmodelos" ValidateRequest="false" Title="Multinotas Modelos" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Confirma Eliminar el Modelo Multinota?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>


    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Modelos de Multinotas</h3>
                </div>
                <div class="panel-body">

                    <asp:Panel ID="PanelModelos" runat="server">
                        <table>
                            <tr>
                                <td colspan="2" align="left">
                                    <asp:Button ID="btnnuevo" runat="server" Text="Nuevo" CssClass="btn btn-info" Width="80px" />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:GridView ID="GrillaModelos" runat="server"
                                        AutoGenerateColumns="False"
                                        CellPadding="4"
                                        DataMember="DefaultView"
                                        ForeColor="#333333"
                                        GridLines="None"
                                        Width="500px" EnableModelValidation="True" CssClass="table"
                                        AllowPaging="True" PageSize="4">
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <Columns>

                                            <asp:BoundField DataField="id_mulmod" HeaderText="Nro">
                                                <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="50px" Wrap="False" />
                                                <HeaderStyle Font-Size="Small" HorizontalAlign="Left" Width="50px" Wrap="False" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                                <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="250px" Wrap="True" />
                                                <HeaderStyle Font-Size="Small" HorizontalAlign="Left" Width="250px" Wrap="False" />

                                            </asp:BoundField>

                                            <asp:BoundField DataField="concepto" HeaderText="Cod_Tasa">
                                                <HeaderStyle Font-Size="Small" HorizontalAlign="Left" Width="50px" Wrap="False" />
                                                <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="50px" Wrap="False" />
                                                <ControlStyle Width="50px" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="n_concepto" HeaderText="Concepto">
                                                <HeaderStyle Font-Size="Small" HorizontalAlign="Left" Width="150px" Wrap="False" />
                                                <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="150px" Wrap="False" />
                                                <ControlStyle Width="150px" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnmodeloeliminar" runat="server" CausesValidation="False"
                                                        CommandName="Select" ImageUrl="~/Images/Iconos/tachito 2.ico"
                                                        Text="Eliminar" OnClick="OnConfirm" OnClientClick="Confirm()" CssClass="icons" />
                                                </ItemTemplate>
                                                <ControlStyle Height="20px" Width="20px" />
                                                <HeaderStyle Font-Size="Small" Width="100px" Wrap="False" />
                                                <ItemStyle Font-Size="X-Small" HorizontalAlign="Center" VerticalAlign="Middle"
                                                    Width="50px" Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Editar" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnmodeloeditar" runat="server"
                                                        ImageUrl="~/Images/Iconos/modificar.ico"
                                                        OnClick="btnmodeloeditar_Click" CssClass="icons" />
                                                </ItemTemplate>
                                                <ControlStyle Height="20px" Width="20px" />
                                                <HeaderStyle Font-Size="Small" Width="100px" Wrap="False" />
                                                <ItemStyle Font-Size="X-Small" HorizontalAlign="Center" VerticalAlign="Middle"
                                                    Width="50px" Wrap="false" />
                                            </asp:TemplateField>


                                        </Columns>
                                        <PagerStyle CssClass="gvpagination" Font-Names="Calibri" HorizontalAlign="Center" />

                                    </asp:GridView>
                                </td>
                            </tr>

                        </table>
                    </asp:Panel>


                </div>

            </div>
        </div>
    </div>

    <asp:Panel ID="PanelEditor" runat="server" Width="800px">

        <table style="background-color: #fff" class="table">
            <tr>
                <td align="right" colspan="2">
                    <asp:ImageButton ID="btncerrar" runat="server"
                        ImageUrl="~/Images/delete_on.gif" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblidmulmod" runat="server" Text="NroModelo: " CssClass="control-label"></asp:Label></td>
                <td align="left">
                    <asp:TextBox ID="txtidmul" runat="server"
                        CssClass="form-control" Width="70px" Enabled="False"></asp:TextBox></td>

            </tr>
            <tr>
                <td align="right" style="height: 26px">
                    <asp:Label ID="lblnombre" runat="server" Text="Nombre: " CssClass="control-label"></asp:Label></td>
                <td align="left" style="height: 26px">
                    <asp:TextBox ID="txtnombre" runat="server" CssClass="form-control" Width="690px"></asp:TextBox></td>
            </tr>

            <tr>
                <td align="right">
                    <asp:Label ID="lblConceptos" runat="server" Text="Conceptos: " CssClass="control-label"></asp:Label></td>
                <td align="left">
                    <asp:DropDownList ID="cboconceptos" runat="server"
                        Width="690px" CssClass="form-control">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtBox1" runat="server" Columns="80" Rows="10" Text=""
                        TextMode="MultiLine" />
                    <script type="text/javascript">

                        var config = {

                            toolbar:
                                    [
                                        ['Bold', 'Italic', 'Underline', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', '-', 'Image', 'Table'],
                                        ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
                                        ['UIColor'], '/',
                                        ['Styles', 'Font', 'FontSize', 'TextColor']
                                    ]

                        };

                        CKEDITOR.config.width = 800;
                        CKEDITOR.config.height = 200;



                        CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtBox1', config);

                        CKEDITOR.config.resize_enabled = true;

                    </script>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnguardar" runat="server" CssClass="btn btn-default" Text="Guardar" Width="80px" />
                </td>
                <td align="left">
                    <asp:RequiredFieldValidator ID="rqv1" runat="server"
                        ErrorMessage="Ingrese el Nombre de la Multinota Modelo" Text="*"
                        ControlToValidate="txtnombre" Enabled="false"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnoculto"
        PopupControlID="PanelEditor"
        BackgroundCssClass="FondoAplicacion"
        OkControlID="btncerrar"
        DropShadow="false">
    </asp:ModalPopupExtender>
    <asp:Button ID="btnoculto" runat="server" Text="btnoculto" Style="display: none;" />
    <asp:HiddenField ID="Boxidmul" runat="server" />
    <asp:HiddenField ID="Boxaccion" runat="server" />
    <asp:HiddenField ID="Boxiddef" runat="server" />




</asp:Content>


