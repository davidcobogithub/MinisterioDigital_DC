<%@ Page Language="VB" MasterPageFile="~/MasterPage1.master" AutoEventWireup="false" CodeFile="frmNotificacionEmpresas.aspx.vb" Inherits="frmNotificacionEmpresas" ValidateRequest="false" Title="Formulario Notificacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"/>--%>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <asp:Panel ID="panelBusquedas" runat="server">
        <div class="accordion" id="accordionExample">
            <asp:Panel ID="panel2" runat="server">
                <div class="card">
                    <div class="card-header" id="headingTwo">
                        <div class="panel-heading collapsed mb-0" data-toggle="collapse" data-target="#collapseTwo,#collapseThree" aria-expanded="false" aria-controls="collapseTwo" style="background-color: #00b1ec; cursor: pointer">
                            <h4 style="color: white">PASO 1.A - BÚSQUEDA PERSONALIZADA</h4>
                        </div>
                    </div>
                    <div id="collapseTwo" class="collapse in" aria-labelledby="headingTwo" data-parent="#accordionExample">
                        <div class="card-body">

                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-6 col-lg-12">

                                            <h2 class="h2">Buscar Empresas</h2>
                                            <h4>Filtros de búsqueda</h4>
                                            <div class="accordion" id="accordion2">
                                                <div class="card">
                                                    <div class="card-header" id="headingTwo2">
                                                        <div class="panel collapsed mb-0" data-toggle="collapse" data-target="#collapseTwo2, #collapseTwo3" aria-expanded="false" aria-controls="collapseTwo2" style="background-color: #00b1ec; cursor: pointer">
                                                            <h5 style="color: white; font-weight: bold">BUSCAR POR CUIT O RAZÓN SOCIAL</h5>
                                                        </div>
                                                    </div>
                                                    <div id="collapseTwo2" class="collapse in" aria-labelledby="headingTwo2" data-parent="#accordion2">
                                                        <div class="card-body">
                                                            <div class="row">

                                                                <div class="col-lg-6">
                                                                    <p>Buscar solo por CUIT</p>
                                                                    <asp:TextBox ID="TextBoxCuit" runat="server" MaxLength="20" CssClass="form-control" placeholder="CUIT"></asp:TextBox>

                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <p>Buscar solo por Razón Social</p>
                                                                    <asp:TextBox ID="TextBoxRazon" runat="server" MaxLength="20" CssClass="form-control" placeholder="RAZÓN SOCIAL"></asp:TextBox>

                                                                </div>

                                                            </div>
                                                            <br />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="card">
                                                    <div class="card-header" id="headingTwo3">
                                                        <div class="panel collapsed mb-0" data-toggle="collapse" data-target="#collapseTwo3, #collapseTwo2" aria-expanded="false" aria-controls="collapseTwo3" style="background-color: #00b1ec; cursor: pointer">
                                                            <h5 style="color: white; font-weight: bold">BUSCAR POR TIPO DE PRESENTACIÓN, DELEGACIÓN, CANTIDAD DE EMPLEADOS, TIPO DE PERSONERÍA, ESTADO Y ACTIVIDAD DE EMPRESA</h5>
                                                        </div>
                                                    </div>
                                                    <div id="collapseTwo3" class="collapse" aria-labelledby="headingTwo3" data-parent="#accordion2">
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="row" style="margin-left: 20px">
                                                                    <p>Busca por un campo o combina varios campos a la vez</p>
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <asp:DropDownList ID="DropDownListAcuerdo" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="col-lg-4">
                                                                    <asp:DropDownList ID="DropDownListDelegacion" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="col-lg-4">
                                                                    <div class="row">
                                                                        <div class="col-lg-5">
                                                                            <asp:TextBox ID="TextBoxCant1" runat="server" MaxLength="20" CssClass="form-control" placeholder="#Empleados <"></asp:TextBox>

                                                                        </div>
                                                                        <div class="col-lg-1" style="margin-right: 15px; margin-left: 15px">
                                                                            <h4 style="text-align: center">-</h4>
                                                                        </div>
                                                                        <div class="col-lg-5">
                                                                            <asp:TextBox ID="TextBoxCant2" runat="server" MaxLength="20" CssClass="form-control" placeholder="#Empleados >"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <br />
                                                            <div class="row">

                                                                <div class="col-lg-4">

                                                                    <asp:DropDownList ID="DropDownListTipoPersoneria" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="col-lg-4">
                                                                    <asp:DropDownList ID="DropDownListEstado" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>

                                                                </div>


                                                                <div class="col-lg-4">
                                                                    <div class="row">
                                                                        <div class="col-lg-5">
                                                                            <asp:TextBox ID="TextBoxIdArea1" runat="server" MaxLength="20" CssClass="form-control" placeholder="Id Actividad <"></asp:TextBox>

                                                                        </div>
                                                                        <div class="col-lg-1" style="margin-right: 15px; margin-left: 15px">
                                                                            <h4 style="text-align: center">-</h4>
                                                                        </div>
                                                                        <div class="col-lg-5">
                                                                            <asp:TextBox ID="TextBoxIdArea2" runat="server" MaxLength="20" CssClass="form-control" placeholder="Id Actividad >"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <br />
                                                            <div class="row">

                                                                <div class="col-lg-4">

                                                                    <asp:DropDownList ID="DropDownListRubro" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="col-lg-4">

                                                                    <asp:DropDownList ID="DropDownListActividades" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </div>

                                                            </div>
                                                            <br />
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>
                                            <br />

                                            <asp:Button runat="server" CssClass="btn btn-primary" Text="Buscar Empresas" ID="butBuscar" />

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <br />

        <asp:Panel ID="panel1" runat="server">
            <div class="card">
                <div class="card-header" id="headingThree">
                    <div class="panel-heading mb-0 collapsed" data-toggle="collapse" data-target="#collapseThree,#collapseTwo" aria-expanded="false" aria-controls="collapseThree" style="background-color: #00b1ec; cursor: pointer">
                        <h4 style="color: white">PASO 1.B - NOTIFICACIÓN MASIVA</h4>
                    </div>

                </div>

                <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordionExample">
                    <div class="card-body">

                        <div class="panel panel-default">
                            <div class="panel-body">

                                <div class="col-lg-6">
                                    <p>También puede subir un archivo de Excel para notificar a las empresas</p>

                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="" />
                                    <br />

                                    <asp:Button ID="btnUpload" CssClass="btn btn-primary" runat="server" Text="Subir Archivo" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>
    </asp:Panel>


    <br />

    <asp:Panel ID="panelNotificaciones" runat="server" Visible="false">
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #00b1ec">
                <h4>PASO 2</h4>
            </div>
            <div class="panel-body">

                <div class="col-md-6 col-lg-12">
                    <h2 class="h2">Notificaciones</h2>
                    <p>Por favor seleccione el origen de la notificación</p>
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:DropDownList ID="DropDownListAreasMin" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-4">
                            <asp:DropDownList ID="DropDownListNombreModeloNot" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <asp:DropDownList ID="dropOrigenNot" runat="server" Width="150px" CssClass="form-control">
                        <asp:ListItem>Agente</asp:ListItem>
                        <asp:ListItem>Área</asp:ListItem>
                        <asp:ListItem>Delegación</asp:ListItem>
                    </asp:DropDownList>

                    <br />

                    <%--<asp:Button runat="server" CssClass="btn btn-primary" Text="Enviar Notificación" ID="btnNotificar" />--%>
                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Generar Archivo PDF" ID="ButtonGenerarPDF" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <div class="row">
        <div class="col-md-6 col-lg-12" style="text-align: center; color: white">
            <asp:Button runat="server" CssClass="btn btn-lg" Text="Anterior" ID="ButtonPrev" BackColor="#00b1ec" Enabled="false"/>
            <asp:Button runat="server" CssClass="btn btn-lg" Text="Siguiente" ID="ButtonNext" BackColor="#00b1ec" />
        </div>
    </div>

    <!-- Modal -->


    <asp:Panel ID="PanelModal" runat="server" Width="800px" Height="500px" CssClass="modal-content">


        <div class="modal-header">
            <div class="row">
                <div class="col-lg-6">
                    <h5 class="modal-title" id="exampleModalLabel1">Modal</h5>
                </div>
                <div class="col-lg-6" style="text-align: right">

                    <asp:ImageButton ID="btncerrar" runat="server"
                        ImageUrl="~/Images/delete_on.gif" />

                </div>
            </div>
        </div>
        <asp:Panel ID="PanelEditor" runat="server" Width="797px" Height="445px" ScrollBars="Both">

            <div class="modal-body">
                <asp:UpdatePanel runat="server" ID="updatePanel"
                    UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-body">
                            <p>Por favor seleccione las empresas para enviar notificación</p>
                            <asp:GridView ID="GridView1" runat="server" CssClass="tablabandeja" Width="100%"
                                Font-Size="Small" TabIndex="34" GridLines="Horizontal">
                                <RowStyle BackColor="White" />
                                <HeaderStyle BackColor="#996699" Font-Bold="True" ForeColor="White" Font-Size="Medium" />

                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>

                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="chckchanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" OnCheckedChanged="OnSelectedIndexChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>
                            <br />
                            <asp:Button runat="server" CssClass="btn btn-primary" Text="Aceptar" ID="btnAceptarModal" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>

        </asp:Panel>

    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnoculto"
        PopupControlID="PanelModal" Drag="true" X="300" Y="100"
        BackgroundCssClass="FondoAplicacion"
        OkControlID="btncerrar"
        DropShadow="false">
    </asp:ModalPopupExtender>
    <asp:Button ID="btnoculto" runat="server" Text="btnoculto" Style="display: none;" />

</asp:Content>

