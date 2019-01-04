<%@ Page Title="Multinotas" Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="frmMultinotasEdit.aspx.vb" Inherits="frmMultinotasEdit" ValidateRequest = "False" %>

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
    
   

   <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
 <br />
 <div class="row">
   <div class="col-md-1"></div>
      <div class="col-md-10">
          <div class="panel panel-default">
            <div class="panel-heading">
                    <h3 class="panel-title">Multinotas</h3>
            </div>
          <div class="panel-body">

  <asp:Panel ID="PanelEditor" runat="server">
   
    <table Class="table table-bordered" width="600px">
       <tr>
        <td align="right"><asp:Label ID="lblidmulmod" runat="server" Text="Multinota: " 
                CssClass="control-label"></asp:Label></td>
        <td align="left"><asp:TextBox ID="txtidmul" runat="server" 
                CssClass="form-control" Width="70px" Enabled="False"></asp:TextBox></td>
        <td align="right"><asp:Label ID="Label3" runat="server" Text="Fecha: " 
                CssClass="control-label"></asp:Label> </td>
        <td align="left">
            <asp:TextBox ID="txtfecha" runat="server" 
                CssClass="form-control" Width="166px" Enabled="False"></asp:TextBox></td>
    </tr>
       <tr>
       <td align="right" style="height: 26px">
           <asp:Label ID="Label1" runat="server" CssClass="control-label"
               Text="Seleccione el Tipo de Nota: "></asp:Label>
        </td>
       <td align="left" style="height: 26px">
           <asp:DropDownList ID="cbomodelos" runat="server" CssClass="form-control"
               Width="300px" AutoPostBack="True">
           </asp:DropDownList>
        </td>

         <td align="right" style="height: 26px">
           <asp:Label ID="Label4" runat="server" CssClass="control-label"
               Text="Año: "></asp:Label>
        </td>

         <td align="left">
             <asp:TextBox ID="txtano" runat="server" 
                CssClass="form-control" Width="166px" Enabled="False"></asp:TextBox></td>


    </tr>
       <tr>
       <td align="right" style="height: 26px">
           <asp:Label ID="Label2" runat="server" CssClass="control-label"
               Text="Seleccione La Delegación: "></asp:Label>
        </td>
       <td align="left" style="height: 26px">
           <asp:DropDownList ID="ddlDelegacion" runat="server" CssClass="form-control"
               Width="300px" AutoPostBack="True">
           </asp:DropDownList>

            
        </td>
          <td align="right" style="height: 26px">
           <asp:Label ID="Label5" runat="server" CssClass="control-label"
               Text="Mes: "></asp:Label>
        </td>

         <td align="left">
             <asp:TextBox ID="txtmes" runat="server" 
                CssClass="form-control" Width="166px" Enabled="False"></asp:TextBox></td>


    </tr>
       <tr>
            <td colspan="4">
                <asp:TextBox ID="txtBox1" runat="server" Columns="50" Rows="10" Text="" 
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

                   CKEDITOR.config.width = 750;
                   CKEDITOR.config.height = 150;

                   CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtBox1', config);

                   CKEDITOR.config.resize_enabled = false;
                                         
         </script> 
              
            </td>
        </tr>
       <tr>
        <td  align="center" colspan="4"> 
         
            <!--<asp:ImageButton ID="btnmodeloeditar1" runat="server" ImageUrl="~/Images/editar.png" onclick="btnmodeloeditar_Click" CssClass="icons"/>-->
            <asp:Button ID="btnmodeloeditar" runat="server" CssClass="btn btn-default" 
                Enabled="false" Text="Editar" Width="110px"/>
            <asp:Button ID="btnadjuntar" runat="server" CssClass="btn btn-default" 
                Enabled="false" Text="Adjuntar" Width="110px"/>
            <asp:Button ID="btnguardar" runat="server" CssClass="btn btn-default"  
                Text="Guardar" Enabled="False" Width="110px"/> 
            <asp:Button ID="btncancelar" runat="server" CssClass="btn btn-default" 
                Enabled="false" Text="Cancelar" Width="110px"/>
            <!--<asp:ImageButton ID="imgadjunto1" runat="server" ImageUrl="~/Images/attached.png" OnClick="imgadjunto_Click"  CssClass="icons" />-->
             <asp:Button ID="imgadjunto" runat="server" CssClass="btn btn-default" 
                Enabled="false" Text="Ver Adjuntos" Width="110px" OnClick="imgadjunto_Click"/>
            <asp:Button ID="btnenviar" runat="server" onclick="btnenviar_Click"  
                Text="Enviar" CssClass="btn btn-success" Width="110px"  ValidationGroup="A" />

          </td>
       </tr>
        
   </table>
   
</asp:Panel>  

         
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ControlToValidate="ddlDelegacion" CssClass="Tit_validadores" 
                                    ErrorMessage="Seleccione una delegación" ValidationGroup="A" 
                                    InitialValue="--Seleccione Una Delegación--" 
               Font-Bold="False">Seleccione una delegación</asp:RequiredFieldValidator> 
    

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
            <HeaderStyle Width="50px" />
            <ItemStyle Width="50px" HorizontalAlign ="Center"   />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Adjunto">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="imgpdf" runat="server" 
                        ImageUrl="~/Images/Iconos/pdf.png" onclick="imgpdf_Click" 
                        CssClass="icons"/>
                </ItemTemplate>
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" HorizontalAlign ="Center"  />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Eliminar">
                <ItemTemplate>
                    <asp:ImageButton ID="btneliminar" runat="server" 
                        ImageUrl="~/Images/Iconos/eliminar.png" CssClass="icons" 
                        onclick="btneliminar_Click"/>
                </ItemTemplate>
                 <ItemStyle Width="50px" HorizontalAlign ="Center"  />
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
     


    

      

    <%--<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
         TargetControlID="btnoculto"
        PopupControlID="panelgrillaadj" 
        BackgroundCssClass="FondoAplicacion"
        CancelControlID="btncerrar2"
        DropShadow="false">
                          
    </asp:ModalPopupExtender>--%>

    <asp:Button ID="btnoculto" runat ="server"  CssClass="Oculto" />

            <asp:HiddenField ID="boxano" runat="server" />
            <asp:HiddenField ID="boxmes" runat="server" />
            <asp:HiddenField ID="boxaccion" runat="server" />
            <asp:HiddenField ID="Boxmulidrel" runat="server" />
            <asp:HiddenField ID="boxidmul" runat="server" />
            <asp:HiddenField ID="hfId" runat="server" />
             
          </div>
       </div>
     </div>
  </div>                 

</asp:Content>

