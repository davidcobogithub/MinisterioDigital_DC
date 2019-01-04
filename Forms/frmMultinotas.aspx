<%@ Page Title="Multinotas" Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="frmMultinotas.aspx.vb" Inherits="frmMultinotas" validateRequest="false" MaintainScrollPositionOnPostback="true"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <script type="text/javascript" language="javascript">
         function uploadError(sender, args) {
                 document.getElementById('lblStatus').innerText = args.get_fileName(), "<span style='color:red;'>" + args.get_errorMessage() + "</span>";
             }

             function StartUpload(sender, args) {
                 document.getElementById('lblStatus').innerText = 'Uploading Started.';
             }

             function UploadComplete(sender, args) {
                 var filename = args.get_fileName();
                 var contentType = args.get_contentType();
                 var text = "Size of " + filename + " is " + args.get_length() + " bytes";
                 if (contentType.length > 0) {
                     text += " and content type is '" + contentType + "'.";
                 }
                 document.getElementById('lblStatus').innerText = text;
             }


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
    
  <br />

  <div class="row">
   <div class="col-md-1"></div>
      <div class="col-md-10">
          <div class="panel panel-default">
            <div class="panel-heading">
                    <h3 class="panel-title">Multinotas</h3>
            </div>
            <div class="panel-body">
           
                       <asp:Panel ID="panelmultinota" runat="server">
                       
                        <asp:GridView ID="GrillaMultinota" runat="server"
                                              AutoGenerateColumns="False" 
                                              CellPadding="4"
                                              DataMember="DefaultView" 
                                              ForeColor="#333333" 
                                              GridLines="None" 
                                              EnableModelValidation="True" CssClass="table">

                                    <FooterStyle />
                                    <Columns>
                            
                                        <asp:BoundField DataField="id_multinota" HeaderText="Nro">
                                            <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="50px" Wrap="False" />
                                            <HeaderStyle Font-Size="Small" HorizontalAlign="Left" Width="50px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                            <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="150px" 
                                            Wrap="True" />
                                            <HeaderStyle Font-Size="Small" HorizontalAlign="Left" Width="150px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha">
                                           <HeaderStyle Font-Size="Small" HorizontalAlign="Left" Width="150px" Wrap="False" />
                                            <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="150px" Wrap="False" />
                                            <ControlStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mes" HeaderText="Mes">
                                           <HeaderStyle Font-Size="Small" HorizontalAlign="Left" Width="50px" Wrap="False" />
                                            <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="50px" Wrap="False" />
                                            <ControlStyle Width="50px" />
                                        </asp:BoundField>  
                                        <asp:BoundField DataField="ano" HeaderText="Año">
                                          <HeaderStyle Font-Size="Small" HorizontalAlign="Left" Width="50px" Wrap="False" />
                                            <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="50px" Wrap="False" />
                                            <ControlStyle Width="50px" />
                                        </asp:BoundField>  
                                        <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnmodeloeliminar" runat="server" CausesValidation="False" 
                                                    CommandName="Select" ImageUrl="~/Images/Iconos/eliminar.png" 
                                                     Text="Eliminar" OnClick = "OnConfirm"  OnClientClick = "Confirm()" 
                                                    CssClass="icons"/>
                                            </ItemTemplate>
                             
                                            <HeaderStyle Font-Size="Small" Width="100px" Wrap="False" />
                                            <ItemStyle Font-Size="X-Small" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Width="50px" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Editar" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnmodeloeditar" runat="server" 
                                                    ImageUrl="~/Images/editar.png" 
                                                    onclick="btnmodeloeditar_Click" CssClass="icons" />
                                            </ItemTemplate>
                                
                                            <HeaderStyle Font-Size="Small" Width="100px" Wrap="False" />
                                            <ItemStyle Font-Size="X-Small" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Width="50px" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjunto" ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgadjunto" runat="server" 
                                                    ImageUrl="~/Images/attached.png" OnClick="imgadjunto_Click" 
                                                    CssClass="icons" />
                                            </ItemTemplate>
                                 
                                            <HeaderStyle Font-Size="Small" Width="100px" Wrap="False" />
                                            <ItemStyle Font-Size="X-Small" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Width="50px" Wrap="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Enviar Bandeja">
                                            <ItemTemplate>
                                                <asp:Button ID="btnenviar" runat="server" onclick="btnenviar_Click" 
                                                    Text="Enviar" CssClass="btn btn-default"  
                                                    ValidationGroup="A" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                             <ControlStyle Height="30px" Width="100px" />
                                            <HeaderStyle Font-Size="Small" Width="100px" Wrap="False" />
                                            <ItemStyle Font-Size="X-Small" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Width="100px" Wrap="false" />
                                        </asp:TemplateField>
                           

                                    </Columns>
                        
                                   <%--  <RowStyle BackColor="#E3EAEB" Font-Names="Droid Sans"/> 
                                     <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Names="Droid Sans" />
                                     <HeaderStyle BackColor="#996699" Font-Bold="True" ForeColor="White" Font-Names="Droid Sans" />
                                     <AlternatingRowStyle BackColor="White" />--%>
                             </asp:GridView>
          
                     </asp:Panel>
           
                     
               
                       <asp:Panel ID="panelAcciones" runat="server" > 
          <div class="form-horizontal">
            <div class="form-group">
                     <div class="col-md-12">              
                                   <asp:Button ID="btnnuevo" runat="server" CssClass="btn btn-info" Text="Nuevo" Width="150px" />
                                   <asp:Button ID="btnadjuntar" runat="server" CssClass="btn  btn-default" Enabled="false" Text="Adjuntar" Width="150px" />
                                   <asp:Button ID="btnguardar" runat="server" Text="Guardar" Width="150px" Enabled="False" CssClass="btn btn-default"  /> 
                                   <asp:Button ID="btncancelar" runat="server" CssClass="btn btn-default" Enabled="false" Text="Cancelar" Width="150px" />
                           </div>
                     </div>
            </div>
           </asp:Panel>
          
                       <asp:Panel ID="PanelEditor" runat="server">
               
                <div class="form-horizontal">
                    
                    <div class="form-group">
                       <asp:Label ID="lblidmulmod" runat="server" Text="Multinota:" 
                            CssClass="col-md-3 control-label"></asp:Label> 
                         <div class="col-md-7">
                             <asp:TextBox ID="txtidmul" runat="server"  CssClass="form-control"  Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" CssClass="col-md-3 control-label"  Text="Tipo de Nota:"></asp:Label>
                        <div class="col-md-7">
                          <asp:DropDownList ID="cbomodelos" runat="server" CssClass="form-control"  AutoPostBack="True"> </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                       <asp:Label ID="Label2" runat="server" CssClass="col-md-3 control-label"   Text="Delegación:"></asp:Label>
                     <div class="col-md-7">
                          <asp:DropDownList ID="ddlDelegacion" runat="server" CssClass="form-control"  AutoPostBack="True">  </asp:DropDownList>
                      </div>
                    </div>                    
                    <div class="form-group">
                          <div class="col-md-12">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                ControlToValidate="ddlDelegacion" CssClass="Tit_validadores" 
                                                ErrorMessage="Seleccione una delegación" ValidationGroup="A" 
                                                InitialValue="--Seleccione Una Delegación--" 
                           Font-Bold="False">Seleccione una delegación</asp:RequiredFieldValidator>
                           </div>
                     </div>
                    
                    <div class="form-group">
                          <div class="col-md-12">  
                             <asp:TextBox ID="txtBox1" runat="server" Columns="50" Rows="10" Text=""  TextMode="MultiLine" />
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

                                    CKEDITOR.config.width = 700;
                                    CKEDITOR.config.height = 300;

                                    CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtBox1', config);

                                    CKEDITOR.config.resize_enabled = false;
                                         
                     </script>
                          </div>
                      </div>
                 </div>
            </asp:Panel>

                       
            
                       
     
               <%-- <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                     TargetControlID="btnoculto"
                     PopupControlID="panelgrillaadj" 
                     BackgroundCssClass="FondoAplicacion"
                     CancelControlID="btncerrar2"
                     DropShadow="false">
                </asp:ModalPopupExtender>--%>

              <%--  <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
                    TargetControlID="btnoculto"
                    PopupControlID="paneladjunto" 
                    BackgroundCssClass="FondoAplicacion"
                    CancelControlID="btncerrar"
                    DropShadow="false">
                </asp:ModalPopupExtender>--%>


       </div>
    
         </div>
          
          
          <!-- Modal Grilla Adjuntos-->
                <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" data-backdrop="static" data-keyboard="false">
                  <div class="modal-dialog" role="document">
                    <div class="modal-content">
                      <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Grilla Archivos Adjuntos</h4>
                      </div>

                      <div class="modal-body">
                        
                         <asp:Panel ID="panelgrillaadj" runat="server">
                         
                             <asp:GridView ID="GrillaAdj" runat="server"
                                               AutoGenerateColumns="False" DataMember="DefaultView" CellPadding="20"
                                 Font-Names="Droid Sans" Font-Size="Small" Font-Bold="False" 
                         EnableModelValidation="True" ForeColor="#333333" GridLines="None" CssClass="table">
                               

                          <Columns>
                        <asp:BoundField DataField="id_muladj" HeaderText="Nro" >
                        <HeaderStyle Width="200px" />
                       
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Adjunto">
                       <HeaderStyle Width="200px" />
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgpdf" runat="server" 
                                    ImageUrl="~/Images/Iconos/pdf.png" onclick="imgpdf_Click" 
                                    CssClass="icons" />
                            </ItemTemplate>
                           
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar">
                       
                            <ItemTemplate>
                                <asp:ImageButton ID="btneliminar" runat="server" 
                                    ImageUrl="~/Images/Iconos/eliminar.png" CssClass="icons" 
                                    onclick="btneliminar_Click"/>
                            </ItemTemplate>
                          
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />

                   </asp:GridView>
                   
                            </asp:Panel>
                        
                      </div>
                     
                      <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        
                      </div>
                    </div>
                </div>
                </div>
              
          <!-- Modal Subir Adjunto -->

          <div class="modal fade" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
              <div class="modal-dialog" role="document">
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H1">Subir Archivos</h4>
                  </div>
                  <div class="modal-body">
                        <asp:Panel ID="paneladjunto" runat="server" BackColor="White" Visible="False" 
                    Width="500px">

            <table>
                   <tr>
                       <td  colspan="2" align="right">
                           <asp:ImageButton ID="btncerrar" runat="server" ImageUrl="~/Images/delete_on.gif" />
                         </td>
                     </tr>
                   <tr>
                        <td>Seleccione un Archivo:</td>
                        <td> 
                            <asp:FileUpload ID="FileUpload1" runat="server"  class="multi" maxlength="3" accept="pdf"/>
                        </td>
                   </tr> 
     
                   <tr>
                       <td>
                           <asp:Label ID="Throbber" runat="server" Style="display: none">
                        <img src="Images/loading.gif" align="absmiddle" alt="loading" />
                    </asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblStatus" runat="server" Style="font-family: Droid Sans; font-size: small;"></asp:Label></td>
                       <td align="left">
                           <asp:Button ID="btnsubir" runat="server" Text="Subir" Width="129px" />
                       </td>
                   </tr>
     
                   <tr>
                       <td colspan="2">
                           &nbsp;</td>
          
                   </tr>
            </table>
            </asp:Panel>
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                  
                  </div>
                </div>
              </div>
            </div>
          
      </div>
      <div class="col-md-1"></div>
                       
    </div>

  
 

<asp:Button ID="btnoculto" runat ="server"  CssClass="Oculto" />

<asp:HiddenField ID="boxano" runat="server" />
<asp:HiddenField ID="boxmes" runat="server" />
<asp:HiddenField ID="boxaccion" runat="server" />
<asp:HiddenField ID="Boxmulidrel" runat="server" />
<asp:HiddenField ID="boxidmul" runat="server" />

      

</asp:Content>

