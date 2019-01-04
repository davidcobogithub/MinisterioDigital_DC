<%@ Page Title="Ministerio de Trabajo" Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false"
    CodeFile="frmLiBarandillaLi.aspx.vb" Inherits="frmLiBarandillaLi" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    <div id="contenedorPrincipal" class="container">
        <%-- <span class="badge badge-accion bg-success"> --%>
        <div class="panel panel-default">
            <div class="panel-heading">
            <b>A C T U A C I O N E S&nbsp;&nbsp; -&nbsp;&nbsp; L I B R O&nbsp;&nbsp; D E &nbsp;&nbsp; I N S P E C C I O N &nbsp;&nbsp; D I G I T A L </b>
<%--                
<b>B A R A N D I L L A &nbsp;&nbsp; L I B R O &nbsp;&nbsp; D E &nbsp;&nbsp; I N S P E C C I O N</b>
--%>            </div>
            <div class="well">
                <%--                                      <%# Eval("MOVIMIENTOS", "{0}")%>  --%>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlAgentes" CssClass="form-control input-sm" runat="server"
                                Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-7">
                        <div class="input-group">
                            <asp:TextBox ID="txtBuscarProcedimiento" runat="server" CssClass="form-control" placeholder="Ingrese CUIT (sin guiones), Razón Social o Número Proc..."></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="btnBuscarProcedimiento" runat="server" Text="Buscar" CssClass="form-control btn-info"
                                    Style="width: 100px;" />
                            </span>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="btnTodos" runat="server" Text="Todos" CssClass="form-control btn-info" />
                    </div>
                    <div class="col-md-1">
                        <strong>
                            <asp:Button ID="btnLibretasSinUsar" runat="server" Text="+" CssClass="form-control btn-success" data-toggle="tooltip" data-placement="bottom" title="Añadir una nueva carpeta..." /></strong>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <%--<br />--%>
                    <%--Grilla   C A R P E T A S --%>
                    <asp:GridView ID="grillaBandejaLibroInspeccion" runat="server" CssClass="table table-condensed"
                        GridLines="None" Font-Bold="False" DataMember="DefaultView" TabIndex="34" EnableModelValidation="True"
                        Font-Size="X-Small" AllowPaging="True" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="ID_CARPETA,NROCUENTA,IDUSUARIO">
                        <Columns>
                            <%--<asp:BoundField DataField="ID_CARPETA" HeaderText="ID" />--%>
                            <asp:BoundField DataField="NRO_CARPETA" HeaderText="Proced." />
                            <%--<asp:BoundField DataField="NROCUENTA" HeaderText="NROCUENTA" />--%>
                            <asp:BoundField DataField="CUIT" HeaderText="CUIT" />
                            <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Razón Social" />
                            <asp:BoundField DataField="MOVIMIENTOS" HeaderText="Mov." />
                            <asp:BoundField DataField="PM" HeaderText="1er. Mov." DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="UM" HeaderText="Ult. Mov." DataFormatString="{0:dd-MM-yyyy}" />
                            <%--                            <asp:BoundField DataField="IDUSUARIO" HeaderText="IDUSUARIO" />--%>
                            <asp:BoundField DataField="AGENTE" HeaderText="Agente" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonActuaciones" runat="server" OnClick="LinkButtonActuaciones_Click"
                                        data-toggle="tooltip" data-placement="bottom" title="Añadir una nueva actuación...">
                                    <%-- <span class="badge badge-accion bg-success"> --%>
<%--                                      <%# Eval("MOVIMIENTOS", "{0}")%>  --%>
                                          <i class="fa fa-plus-circle fa-lg"></i>           
                                    </asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton ID="LinkButtonDetalleActuaciones" runat="server" OnClick="LinkButtonDetalleActuaciones_Click"
                                        data-toggle="tooltip" data-placement="bottom" title="Detalle de actuaciones...">
                                        <i class="fa fa-list fa-lg"></i>                                        
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--                            <asp:TemplateField>
                                <ItemTemplate>

                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                        <PagerStyle CssClass="gvpagination" Font-Names="Calibri" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>
            <%--
                <div class="panel panel-default">
                <div class="panel-heading">
            --%>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblTituloGrillaActasDetalle" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <%--Grilla D E T A L L E  A C T A S--%>
        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="grillaCarpetaDetalle" runat="server" CssClass="table table-condensed table-striped"
                    GridLines="None" Font-Bold="False" DataMember="DefaultView" TabIndex="34"
                    Font-Size="X-Small" Width="100%" AutoGenerateColumns="False" 
                    DataKeyNames="n_resultado_notifica,n_obs_notifica">
                    <Columns>
                        <%--                                <asp:BoundField DataField="ABREVIATURA" HeaderText="T.D." />--%>
                        <%--                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButtonPdfActa" runat="server" ImageUrl="~/Images/Iconos/pdficon_small.gif"
                                            OnClick="ImageButtonPdfActa_Click" ToolTip="Ver Documento en formato PDF" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                        <%--                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButtonEditarActa" runat="server" Height="24px" ImageUrl="~/Images/Iconos/acuse-recibo.png"
                                            OnClick="ImageButtonEditarActa_Click" ToolTip="Continuar Edición del documento"
                                            Width="24px" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                        <asp:BoundField DataField="ID_ACTA" HeaderText="#" />
                        <asp:BoundField DataField="NRO_ACTA" HeaderText="Acta" />
                        <asp:BoundField DataField="FECHA_HORA" HeaderText="Fecha" />
                        <asp:BoundField DataField="N_ESTADO_ACTA" HeaderText="Estado" />
                        <asp:BoundField DataField="DOCUMENTO" HeaderText="Documento" />
                       
                        <asp:TemplateField  HeaderText="Notificación" >
                        <ItemTemplate>
                                <asp:Label ID="lblresNotifica" data-toggle="tooltip" data-placement="bottom"
                                    title=""  runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="T.D.">
                            <ItemTemplate>
                                <asp:Label ID="lblTipoDocu" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonPdfActa" runat="server" data-toggle="tooltip" data-placement="bottom"
                                    title="Ver documento en formato PDF ..." ForeColor="Red" OnClick="LinkButtonPdfActa_Click">
                                            <i class="fa fa-file-pdf-o fa-lg"></i>                                        
                                </asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton ID="LinkButtonEditarActa" runat="server" data-toggle="tooltip" data-placement="bottom"
                                    title="Continuar Edición del documento ..." OnClick="LinkButtonEditarActa_Click">
                                            <i class="fa fa-pencil-square-o fa-lg"></i>                                        
                                </asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton ID="LinkButtonSuacSticker" runat="server" data-toggle="tooltip" data-placement="bottom"
                                    title="SUAC ..." ForeColor="Aquamarine"> 
                                                &nbsp;<strong>Suac</strong>&nbsp;
                                </asp:LinkButton>

                                 &nbsp;
                                <asp:LinkButton ID="LinkButtonNotifica" runat="server" data-toggle="tooltip" data-placement="bottom"
                                    title="Como se Notifico el documento..." OnClick="LinkButtonNotifica_Click">
                                            <i class="fa fa-files-o fa-lg"></i>                                        
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
                <asp:HiddenField ID="hfidacta" runat="server" />
                
            </div>
        </div>
    </div>
    <!--

            ----------------------------------------------- 
            -------------------- Modal --------------------
            -----------------------------------------------

            -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <!-- Hidden Field -->
                    <asp:HiddenField ID="hfIdCarpeta" runat="server" />
                    <asp:HiddenField ID="hfNroCarpeta" runat="server" />
                    <asp:HiddenField ID="hfNroCuenta" runat="server" />
                    <asp:HiddenField ID="hfNroCuit" runat="server" />
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" Text="">ALTA ACTUACION</asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12 text-left">
                                    <div class="alert alert-success" role="alert">
                                        Agente Interviniente: <strong>
                                            <asp:Label ID="lblModalNombreAgente" runat="server" Text="Label"></asp:Label>
                                        </strong>
                                        <br />
                                        Añadir a la Carpeta: <strong>
                                            <asp:Label ID="lblModalNroCarpeta" runat="server" Text="Label"></asp:Label>
                                        </strong>
                                    </div>
                                </div>
                            </div>
                            <div id="buscarCuitArea" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtCuitModal" runat="server" CssClass="form-control" placeholder="Ingrese CUIT del Empleador ..."></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:Button ID="btnBuscaCuilModal" runat="server" Text="Buscar" CssClass="form-control btn-info"
                                                    Style="width: 100px;" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                                </div>
                            </div>
                            <div id="NoEncuentraCuit" runat="server" visible="False">
                                <div class="alert-danger">
                                    <br />
                                    <p>
                                        <asp:Label ID="lblCuitNoEncontrado" runat="server" Text="Label"></asp:Label>
                                    </p>
                                    <br />
                                </div>
                            </div>
                            <div id="CombosArea" runat="server">
                                <div class="row">
                                    <div class="col-md-12 text-left">
                                        <%--<br />--%>
                                        <div class="alert alert-success" role="alert">
                                            <asp:Label ID="lblDatosEmpresa" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <!-- COMBOS     -->
                                <div class="row">
                                    <div class="col-md-12" style="text-align: left">
                                        <asp:DropDownList ID="DropDownListDel" CssClass="form-control input-sm" runat="server"
                                            Width="100%">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblErrorComboDelegacion" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="text-align: left">
                                        <asp:DropDownList ID="DropDownListTD" CssClass="form-control input-sm" runat="server"
                                            Width="100%">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblErrorComboTipoDocumento" runat="server" Text="" ForeColor="Red"
                                            Font-Bold="True"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="text-align: left">
                                        <asp:DropDownList ID="DropDownListSuc" CssClass="form-control input-sm" runat="server"
                                            Width="100%">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblErrorComboSucursal" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>
                                <!-- FIN COMBOS -->
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">
                                Descartar</button>
                            <asp:Button ID="btnGuardar" runat="server" Text="Nueva Actuación" CssClass="btn btn-info"
                                Enabled="False" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade" id="myModalNotifica" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanelNotifica" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <!-- Hidden Field -->
                  <div class="modal-content">
                        <div class="modal-header"><strong>RESULTADO DE NOTIFICACIONES</strong> 
                       <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                      </div> 
                          <div class="panel-body">
                                                   
                                      <div class="form-horizontal">   
                                        <div class="well">
                                          
                                           <div class="form-group">
                                            <div class="col-sm-3 text-right">
                                                     <label class="control-label" for="txtopcion">Opción:</label>
                                                </div>
                                            <div class="col-sm-8 text-left">
                                                <div class="form-inline">
                                                    <asp:DropDownList ID="cboOpcion" runat="server" AutoPostBack="True" Width="400px">
                                                        <asp:ListItem Value="1">OPCION SI </asp:ListItem>
                                                        <asp:ListItem Value="0">OPCION NO</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                       
                                           <div class="form-group">
                                            
                                              <div class="col-sm-3 text-right">
                                                     <label class="control-label" for="txtnombre">Mnemotécnico:</label>
                                              </div>

                                            <div class="col-sm-8 text-left">
                                                <div class="form-inline">
                                                     <asp:DropDownList ID="cbonombre" runat="server" CssClass="form-control" 
                                                        Width="400px" placeholder="Nombre Mnemotécnico" AutoPostBack="True"></asp:DropDownList>
                                                    
                                                </div>
                                               </div>
                                            </div>
                                       
                                           <div class="form-group">
                                             <div class="col-sm-3 text-right">
                                                     <label class="control-label" for="txtfantasia">Descripción:</label>
                                                </div>
                                            <div class="col-sm-8 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtdescripción" runat="server" CssClass="form-control" 
                                                        Width="400px"  Height="200px" placeholder="Descripción" TextMode="MultiLine"></asp:TextBox>
                                                    
                                                </div>
                                        </div>
                                        </div>
                                        
                                           <div class="form-group">
                                             <div class="col-sm-3 text-right">
                                              
                                                </div>
                                            <div class="col-sm-8 text-left">
                                                <div class="form-inline">
                                                    <asp:Button ID="btnAceptarNoti" CssClass="btn btn-default"  runat="server" Text="Aceptar" Width="70px"/>
                                                    <asp:Button ID="btnCancelarNoti" CssClass="btn btn-default" data-dismiss="modal" runat="server" Text="Cancelar" Width="70px"/> 
                                                    
                                                </div>
                                        </div>
                                        </div>

                                        </div>
                                    </div>                   
                                             
                           
                          </div>

                     </div>
               
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%--        </div>
    </div>--%>
</asp:Content>
