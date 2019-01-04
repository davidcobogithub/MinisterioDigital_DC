<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="frmLIapertura.aspx.vb" Inherits="frmLIapertura" MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
 <div class="container">
   
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>

 <div class="row">
   <div class="col-md-12"> 
     
     <br />
      <asp:Panel ID="PanelSurcursales" runat="server">
     
             <div class="panel panel-default" >
                      <div class="panel-heading"><strong>SELECCIONE LA SUCURSAL EN LA QUE REALIZARA LA APERTURA Y/O CIERRE DEL LIBRO DE INSPECCION</strong></div> 
                            <div class="panel-body">

                            <asp:GridView ID="GrillaDomicilios" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4"  CssClass="table table-condensed" 
                                    DataMember="DefaultView" Font-Size="X-Small"
                                         GridLines="None" PageSize="5" TabIndex="34"
                                        Width="100%" DataKeyNames="idbarandilladocmanual,CierreLibro">
                                          <AlternatingRowStyle BackColor="Gainsboro" />
                                <FooterStyle />
                                        <Columns>
                                            <asp:BoundField DataField="item" HeaderText="Nº Sucursal">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="n_sede" HeaderText="Nombre Sucursal">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="calle" HeaderText="Calle">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="altura" HeaderText="Nro. Calle">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="torre" HeaderText="Torre">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="piso" HeaderText="Piso">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="depto" HeaderText="Depto/Loc/Of">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="localidad" HeaderText="Localidad">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cp" HeaderText="Cp">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="n_tipodom" HeaderText="Tipo Domicilio">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Cant_empleados" HeaderText="Empleados por Sucursal">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHAALTA" DataFormatString="{0:d}" HeaderText="Fec.Apertura">
                                                
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fechabaja" DataFormatString="{0:d}" HeaderText="Fec. Baja">
                                                
                                            </asp:BoundField>

                                             <asp:BoundField DataField="NroLibro" DataFormatString="{0:d}" HeaderText="Nro Libro">
                                                
                                            </asp:BoundField>

                                            <asp:BoundField DataField="CierreLibro" DataFormatString="{0:d}" 
                                                HeaderText="Fecha Cierre">
                                                
                                         
                                                
                                            </asp:BoundField>
                                           
                                                                                  
                                         
                                                <asp:TemplateField HeaderText="Acciones" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        
                                                          

                                                          <asp:LinkButton ID="LnkMotivo" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                            title="Ver Motivo de Cierre del Libro..." ForeColor="Aqua" onclick="LnkMotivo_Click">
                                                                    <i class="fa fa-comment-o fa-lg"></i>                                        
                                                        </asp:LinkButton>
                                                        &nbsp;


                                                        <asp:LinkButton ID="lnkApertura" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                            title="Realizar la Apertura del Libro de Inspección para la sucursal..." ForeColor="Green" OnClick="lnkApertura_Click">
                                                                    <i class="fa fa-folder-open-o fa-lg"></i>                                        
                                                        </asp:LinkButton>
                                                        &nbsp;
                                                        <asp:LinkButton ID="lnkCierre" runat="server"  data-placement="bottom"
                                                            title="Realizar el Cierre del Libro de Inspección para la sucursal ..." ForeColor="Red" OnClick="lnkCierre_Click">
                                                                    <i class="fa fa-folder-o fa-lg"></i>                                        
                                                        </asp:LinkButton>
                                                         &nbsp;
                                                        <asp:LinkButton ID="lnkHistorial" runat="server"  data-placement="bottom" Text="Ver Historial" 
                                                            title="Historial de Libros de Inspecciones abiertos ..." ForeColor="Blue" OnClick="lnkHistorial_Click">
                                                                    <i class="fa fa-history fa-lg"></i>                                        
                                                        </asp:LinkButton>
                                                        &nbsp;
                                                        <asp:LinkButton ID="lnkImpPdf" runat="server"  data-placement="bottom" Text="Reimprime Apertura" 
                                                            title="Reimprime la Apertura de Libros de Inspecciones abiertos ..." ForeColor="Red" OnClick="lnkImpPdf_Click">
                                                                    <i class="fa fa-file-pdf-o fa-lg"></i>                                        
                                                        </asp:LinkButton>

                                                  </ItemTemplate>
                                                    <HeaderStyle Font-Size="X-Small" />
                                               </asp:TemplateField>        


                                            
                                            
                                        </Columns>
                                          <PagerStyle BackColor="#666666" CssClass="gvpagination" Font-Names="Calibri" 
                                             HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EEEEEE" />      
                                        
                                    </asp:GridView>

                                <asp:Button ID="btnIrBandejaActuaciones" CssClass="btn btn-default"  runat="server" Text="Ir bandeja Actuaciones" />
                            </div>
      </div>
    
     </asp:Panel>

          
    
 <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="titcierre" runat="server">Confirmación Cierre Libro Inspección</h4>
        <h4 class="modal-title" id="tithistorial" runat="server">Historial Libro Inspección</h4>
        <h4 class="modal-title" id="titcierremotivos" runat="server">Motivos Cierre Libro Inspección</h4>
      </div>
             <div id="modalpanelCierre" runat="server">
               <div class="modal-body">
        
        <asp:TextBox ID="txtmotivosCierre" runat="server" Width="500px" Height="200px" 
                       TextMode="MultiLine" 
                       placeholder="Ingrese Aquí los Motivos del Cierre del Libro:" MaxLength="500"></asp:TextBox>
      </div>
               <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
        <asp:Button ID="btnAceptarCierre" runat="server" class="btn btn-success"  Text="Aceptar" OnClick="btnAceptarCierre_Click" />
      </div>
             </div>

             <div id="modalgvHistorial" runat="server">
                <div class="modal-body">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                    <ContentTemplate>
                     <asp:GridView ID="gvHistorial" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4"  CssClass="table table-condensed" DataMember="DefaultView" Font-Size="X-Small"
                                         GridLines="None" PageSize="15" TabIndex="34"
                                        Width="100%" DataKeyNames="idbarandilladocmanual">
                                        <FooterStyle />
                                        <Columns>
                                              
                                           
                                            <asp:BoundField DataField="FECHAALTA" DataFormatString="{0:d}" HeaderText="Fec.Apertura">
                                                
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="NroLibro" DataFormatString="{0:d}" HeaderText="Nro Libro">
                                                
                                            </asp:BoundField>
                                           
                                           <asp:BoundField DataField="CierreLibro" DataFormatString="{0:d}" HeaderText="Fecha Cierre - Motivo">
                                                
                                            </asp:BoundField>
                                        
                                        <asp:TemplateField HeaderText="Acciones" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                           <asp:LinkButton ID="lnkVerPdf" runat="server" data-toggle="tooltip" data-placement="bottom"
                                                            title="Ver PDF" ForeColor="Red" OnClick="lnkVerPdf_Click">
                                                                    <i class="fa fa-file-pdf-o fa-lg"></i>                                        
                                                        </asp:LinkButton>
                                                      </ItemTemplate>
                                         </asp:TemplateField>
                                       </Columns>
                 
                 </asp:GridView>
                 </ContentTemplate>
                </asp:UpdatePanel>

                 </div>
                 <div class="modal-footer"></div>
             </div>
    </div>
  </div>
</div>
     
      <asp:Panel ID="PanelApertura" runat="server">
           <div class="panel panel-default" >
                <div class="panel-heading"><strong><asp:Label ID="lblpaso1" runat="server"></asp:Label></strong></div> 

                               <div class="panel-body">

                                     <div id="muestra" class="form-horizontal">
                                        
                                      <div class="well">  
                                                               
                                        <div class="form-group">
                                            
                                              <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtrazon">Razón Social:</label>
                                              </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtrazon" runat="server" CssClass="form-control" MaxLength="8"
                                                        Width="500px" placeholder="Razón Social" ReadOnly="True"></asp:TextBox>
                                                    
                                                </div>
                                               </div>
                                            </div>
                                     
                                        
                                        <div class="form-group">
                                             <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtfantasia">Nombre Fantasia:</label>
                                                </div>
                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtfantasia" runat="server" CssClass="form-control" MaxLength="8"
                                                        Width="500px" placeholder="Nombre Fantasia" ReadOnly="True"></asp:TextBox>
                                                    
                                                </div>
                                        </div>
                                        </div>
                                        

                                        <div class="form-group">
                                            <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtcuit">CUIT :</label>
                                                </div>
                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtcuit" runat="server" CssClass="form-control" MaxLength="100"
                                                        Width="215px" placeholder="CUIT" ReadOnly="True"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                           
                                            <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtfecins">Fecha de Inscripción:</label>
                                                </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtfecins" runat="server" CssClass="form-control" MaxLength="100"
                                                        Width="217px" placeholder="Fecha de Inscripción" ReadOnly="True"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                          
                                            <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtnromat">Nro Matricula:</label>
                                                </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtnromat" runat="server" CssClass="form-control" MaxLength="100"
                                                        Width="215px" placeholder="Nro Matricula" ReadOnly="True"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                        </div>

                                           <div class="form-group">
                                            
                                              <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtrazon">Nro Libro:</label>
                                              </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtNroLibro" runat="server" CssClass="form-control"
                                                         Width="215px" placeholder="Nro Libro" ReadOnly="True"></asp:TextBox>
                                                    
                                                </div>
                                               </div>
                                            </div>


                                        <div class="form-group">
                                            
                                            <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtMedPres">Med.Presentación:</label>
                                                </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtMedPres" runat="server" CssClass="form-control" MaxLength="100"
                                                        Width="215px" placeholder="Nro Matricula" ReadOnly="True"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                        </div>



                                        <div class="form-group">
                                                <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtdom"><asp:Label ID="lbldom" runat="server" Text=""></asp:Label></label>
                                                </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtdom" runat="server" CssClass="form-control" MaxLength="11"
                                                        Width="500px" Height="70px" TextMode="MultiLine"  
                                                        placeholder="Domicilio" ReadOnly="True" ValidationGroup="g5"></asp:TextBox>
                                                    
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                        ControlToValidate="txtdom" 
                                                        ErrorMessage="Comuniquese al ministerio de trabajo ya que no se encuentra registrado el domicilio legal" 
                                                        ValidationGroup="g5">*</asp:RequiredFieldValidator>
                                                    
                                                </div>
                                            </div>
                                        </div>

                                            <div class="form-group">
                                                <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtdomLS">Domicilio Libro Sueldo:</label>
                                                </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtdomLS" runat="server" CssClass="form-control" MaxLength="11"
                                                        Width="500px" Height="70px" TextMode="MultiLine"  
                                                        placeholder="Domicilio Libro Sueldo" ReadOnly="True" ValidationGroup="g5"></asp:TextBox>
                                                    
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                        ControlToValidate="txtdomLS" 
                                                        ErrorMessage="Comuniquese al ministerio de trabajo ya que no se encuentra registrado el domicilio Libro Sueldo" 
                                                        ValidationGroup="g5">*</asp:RequiredFieldValidator>
                                                    
                                                </div>
                                            </div>
                                        </div>

                                            <div class="form-group">
                                            <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtActPrincipal">Actividad Principal:</label>
                                                </div>
                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtActPrincipal" runat="server" CssClass="form-control" 
                                                        Width="500px" Height="70px" TextMode="MultiLine"  
                                                        placeholder="Actividad Principal" ReadOnly="True"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                        </div>

                                          <div class="form-group">
                                            
                                            <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="lblnrocentral"><asp:Label ID="lbltipocentra" runat="server" Text=""/></label>
                                                </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="lblnrocentral" runat="server" CssClass="form-control" MaxLength="100"
                                                        Width="215px" placeholder="Nro Matricula" ReadOnly="True"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                        </div>


                                        </div>
                                       <%--<div style="visibility:hidden">
                                        <div class="form-group">
                                            
                                            <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtFecRubrica">Fecha de Rubrica:</label>
                                                </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtFecRubrica" runat="server" CssClass="form-control" MaxLength="100"
                                                        Width="215px" placeholder="Fecha Rúbrica"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            
                                            <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtAgeRubrica">Agente Que Rúbrica:</label>
                                                </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtAgeRubrica" runat="server" CssClass="form-control" MaxLength="100"
                                                        Width="215px" placeholder="Agente Que Rúbrica"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            
                                            <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtDelegacion">Delegación:</label>
                                                </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtDelegacion" runat="server" CssClass="form-control" MaxLength="100"
                                                        Width="215px" placeholder="Delegación"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            
                                            <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtNroLibroR">Número de Libro:</label>
                                                </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtNroLibroR" runat="server" CssClass="form-control" MaxLength="100"
                                                        Width="215px" placeholder="Nro de Libro"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                        </div>--%>



                                        <div class="form-group">
                                            <div class="col-sm-4">
                                            </div>
                                            <div class="col-sm-7 text-left">

                                                  <div class="form-horizontal">
                                                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-default"
                                                        ValidationGroup="g5" Width="105px" />
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-default"
                                                        Width="105px" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" CssClass="Tit_validadores"
                                        Style="margin-right: 113px" ValidationGroup="g5" ShowMessageBox="True" 
                                         ShowSummary="False" />
                                    <asp:Label ID="lblMsjModal" runat="server" ForeColor="Red"  Width="100%"></asp:Label>
                                </div>
          </div> 
      </asp:Panel>

      <asp:Panel ID="PADigitalPaso2" runat="server" Visible="false">
              <div class="panel panel-default" >
                <div class="panel-heading"><strong>APERTURA LIBRO INSPECCION DIGITAL - PASO 2 CONFIRMAR APERTURA</strong></div> 
                   <div class="panel-body">
                      <div class="well">
                         <h4>A Ud. se le ha habilitado el Libro Nro:</h4>
                         <h4><asp:Label ID="lblnrolibroDigital" runat="server" Text="" ForeColor="Red"></asp:Label></h4>    
                         <h4>Confirma la Apertura?</h4>             
                          <asp:Button ID="btnDigitalSI" runat="server" Text="SI" CssClass="btn btn-default" Width="50px" />
                          <asp:Button ID="BtnDigitalNO" runat="server" Text="NO" CssClass="btn btn-default" Width="50px" />
                           
                      </div>
                   </div> 
                </div>
       </asp:Panel>
       
      <asp:Panel ID="PADigitalPaso3" runat="server" Visible="false">
              <div class="panel panel-default" >
                <div class="panel-heading"><strong>APERTURA LIBRO INSPECCION DIGITAL - PASO 3 FIRMAR LIBRO</strong></div> 
                   <div class="panel-body">
                     <div class="well"> 
                         
                             <asp:ImageButton ID="ibFJava" runat="server" ToolTip="Firmar con Java" ImageUrl="~/Images/java.png" Width="110px" Height="33px"/>
                             <asp:ImageButton ID="ibFAct" runat="server" ToolTip="Firmar con ActiveX" ImageUrl="~/Images/activex.png" Width="110px" Height="33px"/>
                         
                     </div>
                     
                        
                     <div style="text-align:right">
                      <!--  <asp:LinkButton ID="btnPaso4Atras" class="btn btn-default btn-lg fa fa-arrow-circle-left" runat="server" Width="100px"><span style=" font-family:Calibri" > Atras</span></asp:LinkButton>-->
                      <asp:LinkButton ID="btnPaso4Sig"   class="btn btn-default btn-lg  fa fa-arrow-circle-right" runat="server" Width="100px"><span style=" font-family:Calibri" > Siguiente</span></asp:LinkButton>
                 
                      
                      </div>
                   </div>
                </div>
       </asp:Panel>

      <asp:Panel ID="PADigitalPaso4" runat="server" Visible="false">
           <div class="panel panel-default" >
                <div class="panel-heading"><strong>APERTURA LIBRO INSPECCION DIGITAL - PASO 4 IMPRIMIR ACUSES Y TASAS</strong></div> 
                   <div class="panel-body">
                     <div class="well"> 
                          <asp:Button ID="ButtonAcuseDeRecibo" CssClass="button" runat="server" Text="Ver Acuse de Recibo" style="font-family: Droid Sans" Width="250px" /> 
                          <asp:Button ID="ButtonComprobantePago" CssClass="button" runat="server" Text="Generar comprobante de pago" Width="250px" />
                          <asp:Label ID="lblmsjGenerar" runat="server" Visible="false" CssClass="message success" Text="La presentación esta fuera de término por lo cual no se aplica la tasa, presione siguiente para continuar."></asp:Label>
                     </div>
                       <div style="text-align:right">
                      
                      <asp:LinkButton ID="btnPaso5Sig"   class="btn btn-default btn-lg  fa fa-arrow-circle-right" runat="server" Width="100px"><span style=" font-family:Calibri" > Siguiente</span></asp:LinkButton>
                 </div>
           </div>
	   </asp:Panel>
           
      <asp:Panel ID="PADigitalPaso5" runat="server" Visible="false">
          <div class="panel panel-default" >
                <div class="panel-heading"><strong>APERTURA LIBRO INSPECCION DIGITAL - PASO 5 PRESENTAR LIBRO</strong></div> 
                   <div class="panel-body">
                     <div class="well"> 
                         <asp:Button ID="ButtonPresentar" CssClass="btn btn-success"  runat="server" Text="Presentar" style="font-family: Droid Sans" Width="250px" /> 
                         <br />
                         <br />
                         <asp:Label ID="lblMensajesS3" runat="server" CssClass="messagesuccess" Visible="False"></asp:Label>
                         <br />
                         <br />
                         <asp:Button ID="btnvolver" CssClass="btn btn-default"  runat="server" Text="Volver" Width="250px" Visible="false" /> 
                     </div>
                   </div>
                </div>
       </asp:Panel>
    
      <asp:Panel ID="PanelActuaciones" runat="server">
                <div class="panel panel-default" >
                      <div class="panel-heading"><strong>LIBRO DE INSPECCION</strong></div> 
                            <div class="panel-body">
                              <asp:GridView ID="gvActuaciones" runat="server" AutoGenerateColumns="False" 
                                    CssClass="table table-condensed table-striped" AllowPaging="True"
                                      GridLines="None" Font-Bold="False" DataMember="DefaultView"
                           EnableModelValidation="True" Font-Size="X-Small">
                                    
                                    
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                    <HeaderStyle BackColor="#666666" ForeColor="Black"  />
                                 <Columns>
                                      <asp:BoundField DataField="idbarandilladocumento" 
                                          HeaderText="idbarandilladoc" />
                                      <asp:BoundField DataField="id_acta" HeaderText="Nro" />
                                      <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                       <asp:BoundField DataField="documento" HeaderText="Documento" />
                                      <asp:BoundField DataField="estado" HeaderText="Estado" />
                                      <asp:BoundField DataField="idsucursal" HeaderText="NroSuc" />
                                      <asp:BoundField DataField="ano" HeaderText="Año" />
                                      <asp:BoundField DataField="mes" HeaderText="Mes" />
                                      <asp:TemplateField HeaderText="Acta">
                                <EditItemTemplate>
                                  <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                              </EditItemTemplate>
                              <ItemTemplate>
                                  <asp:ImageButton ID="imgbtn" runat="server" CssClass="icons" 
                                      ImageUrl="~/Images/Iconos/pdf.png" onclick="imgbtn_Click" />
                              </ItemTemplate>
                          </asp:TemplateField>
                      </Columns>
                      <PagerStyle BackColor="#666666" CssClass="gvpagination" Font-Names="Calibri" 
                                HorizontalAlign="Center" />
                            <RowStyle BackColor="#EEEEEE" />      
                  </asp:GridView>
                                <asp:Button ID="btnNuevo" CssClass="btn btn-success" runat="server" Text="Nueva Apertura" />
                                <asp:Button ID="btnBandeja" CssClass="btn btn-default" runat="server" 
                                    Text="Ir a Bandeja de Actuaciones" />
                  

                  </div>
              </div>
                  

    </asp:Panel>     
  
      <asp:Panel style="display: none;" id="pVolantePago" runat="server" Width="1000px"  CssClass="panel" Font-Names="Droid Sans">
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="hfConSel" runat="server" />
                                            <asp:HiddenField ID="hfFVigencia" runat="server" />
                                            <table id="comprobante">
                                                <tr class="false-col">
                                                    <td align="right">
                                                        <asp:Label ID="Label6" runat="server" Text="Concepto:"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <label id="lblConPadre" runat="server"></label>
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
                                                        <asp:UpdatePanel ID="updatePanel" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnGenerar" runat="server" CssClass="button green"  Text="Generar Comprobante" OnClick="btnGenerar_Click" Width="152px"/> <%--ValidationGroup="gbar" --%>
                                                                <asp:Button ID="btnGenerarPagoLink" runat="server" CssClass="button green"  Text="Generar Pago Link" OnClick="btnGenerarPagoLink_Click" Width="152px"/>

                                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                                                                    AssociatedUpdatePanelID="updatePanel" 
                                                                                    DisplayAfter="100">
                                                                        <ProgressTemplate>
                                                                            <div class="PantallaCargaFondo"></div>
                                                                            <div class="PantallaCarga">
                                                                                <div class="PantallaCargaPanel">
                                                                                    <div class="PantallaCargaTexto">
                                                                                        <asp:Label ID="lblLoader" runat="server" Text="Cargando... Por favor espere un momento."></asp:Label></div>
                                                                                    <div class="PantallaCargaImagen"><img src="Images/ajax-loader.gif" alt="" /></div>
                                                                                </div>
                                                                            </div>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>    
                                                                </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="width:150px">
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cerrar" OnClick="btnCancel_Click" Width="100px"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                        </asp:Panel> 

      <asp:Button id="btnOculto" runat="server" Text="Oculto" CssClass="Oculto"></asp:Button> 

      <asp:ModalPopupExtender ID="mpeVolante" runat="server" 
                                                BackgroundCssClass="FondoAplicacion" 
                                                PopupControlID="pVolantePago" 
                                                TargetControlID="btnOculto">
                        </asp:ModalPopupExtender> 

          
   </div>
  </div>
    
</div>

   <asp:HiddenField ID="hfnrocuenta" runat="server" />
   <asp:HiddenField ID="hfacuerdo" runat="server" />
     <asp:HiddenField ID="hfidsucursal" runat="server" />
   <asp:HiddenField ID="hfNroLibro" runat="server" />
   <asp:HiddenField ID="hfiddoc" runat="server" />
   <asp:HiddenField ID="hfforjuridica" runat="server" />
   <asp:HiddenField ID="hfVisibleforjuridica" runat="server" />
   <asp:HiddenField ID="hfOP" runat="server" />
   <asp:HiddenField ID="hfMotivoCierre" runat="server" />
   
 
 <%--<script type="text/javascript">
     $('#myModal').on('shown.bs.modal', function () {
         $('#myInput').focus()
     })
</script>--%>

<script type="text/javascript">
    function openModal() {
        $('#myModal').modal('show');
    }
</script>

</asp:Content>

