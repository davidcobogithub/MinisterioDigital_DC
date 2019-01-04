<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="frmLIaperturaInterviene.aspx.vb" Inherits="frmLIaperturaInterviene" MaintainScrollPositionOnPostback = "true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<div class="container">
    
 <div class="row">
   <div class="col-md-12"> 
     <br />
    
             <div class="panel panel-default" >
                      <div class="panel-heading"><strong>INTERVENCION DE LA APERTURA DEL LIBRO DE INSPECCION MANUAL</strong></div> 
                            <div class="panel-body">
                                <asp:Panel ID="panelbuscar" runat="server"  DefaultButton="btnBuscador" >
                                  <div class="input-group col-md-offset-3 col-md-pull-1">
                                       <asp:TextBox ID="txtBuscarEmpresa" runat="server" CssClass="form-control" placeholder="Ingrese El CUIT" ></asp:TextBox>
                                         <span class="input-group-btn">
                                          <asp:Button ID="btnBuscador" runat="server" Text="Buscar" CssClass="form-control btn-info" OnClick="btnBuscador_Click"
                                             Style="width: 100px;" />
                                        </span>
                                   </div>
                                   </asp:Panel>
                                
                                   <br />
                                
                                <asp:Panel ID="panelHabilita" runat="server">
                                      <div class="form-horizontal">   
                                
                                 
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

                                      <div class="well">
                                        <h5><strong>INTERVENCION Y/O HABILITACION DEL MINISTERIO DE TRABAJO DE CORDOBA- DOCUMENTACION LABORAL</strong></h5>

                                        <asp:Panel ID="PanelPregunta" runat="server">
                                         <div class="form-group">
                                            
                                              <div class="col-sm-1 text-right">
                                                   
                                              </div>

                                            <div class="col-sm-10 text-left">
                                                <div class="form-inline">
                                                   <div class="alert alert-info"><p style="text-align:center;"><strong>ES UN RELEVAMIENTO ANTERIOR?</strong></p></div>  
                                                   
                                                       
                                                </div>
                                               </div>
                                            </div>
                                         <div class="form-group">
                                            
                                              <div class="col-sm-1 text-right">
                                                   
                                              </div>

                                            <div class="col-sm-10 text-left">
                                                <div class="form-inline">
                                                    <div style="text-align:right">
                                                        <asp:Button ID="btnSIRelAnt" CssClass="btn btn-default" runat="server" Text="SI" Width="70" />
                                                        <asp:Button ID="btnNORelAnt" CssClass="btn btn-default" runat="server" Text="NO" Width="70" />
                                                    </div>
                                                </div>
                                               </div>
                                            </div>
                                        </asp:Panel>
                                      
                                        <asp:Panel ID="panelRA" runat="server" Visible="false">
                                               <div class="form-group">
                                            
                                              <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtfecAlta">Fecha Alta de Libro:</label>
                                              </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                  <div id="sandbox-container" class="input-group date">
                                                                <asp:TextBox ID="txtfecAlta" runat="server" CssClass="form-control" MaxLength="10"
                                                                Width="217px"    placeholder="Fecha Alta Libro"></asp:TextBox>
                                                    </div>
                                                </div>
                                               </div>
                                                  
                                            </div>
                                               <div class="form-group">
                                            
                                              <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtNroLibroManual">Nro Libro Manual:</label>
                                              </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtNroLibroManual" runat="server" CssClass="form-control" MaxLength="2"
                                                      Width="217px"    placeholder="Nro Libro Manual"></asp:TextBox>
                                                    
                                                </div>
                                               </div>
                                            </div>

                                            <div class="form-group">
                                            
                                              <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtUsuarioAnterior">Agente que Intervino:</label>
                                              </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtUsuarioAnterior" runat="server" CssClass="form-control" 
                                                      Width="217px"    placeholder="Agente que Intervino"></asp:TextBox>
                                                    
                                                </div>
                                               </div>
                                            </div>

                                            <div class="form-group">
                                            
                                              <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtDelegacionAnterior">Delegación que Intervino:</label>
                                              </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtDelegacionAnterior" runat="server" CssClass="form-control" 
                                                      Width="217px"    placeholder="Delegación que Intervino"></asp:TextBox>
                                                    
                                                </div>
                                               </div>
                                            </div>

                                        </asp:Panel>
                                       <asp:Panel ID="panelN" runat="server" Visible="false">
                                            <div class="form-group">
                                             
                                              <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtsticker">Ingrese el Nro de Sticker:</label>
                                              </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtsticker" runat="server" CssClass="form-control" MaxLength="20"
                                                       Width="500px" placeholder="Ingrese el Nro de Sticker"></asp:TextBox>
                                                    
                                                </div>
                                               </div>
                                            </div>
                                            <div class="form-group">
                                             
                                              <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtfechabi">Fecha de Habilitación:</label>
                                              </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                 
                                                    <asp:TextBox ID="txtfechabi" runat="server" CssClass="form-control" MaxLength="10"
                                                       Width="217px" placeholder="Fecha Habilitación" ReadOnly="True"></asp:TextBox>
                                                 
                                                </div>
                                               </div>
                                            </div>
                                            <div class="form-group">
                                             
                                              <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="cboAgente">Seleccione el Agente:</label>
                                              </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:DropDownList ID="cboAgente" runat="server" 
                                                        CssClass="form-control input-sm" MaxLength="20"
                                                       Width="217px" placeholder="Seleccione el Agente" AutoPostBack="True"></asp:DropDownList>
                                                    
                                                </div>
                                               </div>
                                            </div>
                                            <div class="form-group">
                                             
                                              <div class="col-sm-4 text-right">
                                                     <label class="control-label" for="txtDelegacion">Delegación:</label>
                                              </div>

                                            <div class="col-sm-7 text-left">
                                                <div class="form-inline">
                                                    <asp:TextBox ID="txtDelegacion" runat="server" CssClass="form-control" MaxLength="20"
                                                       Width="217px" placeholder="Delegación" ReadOnly="true"></asp:TextBox>
                                                    
                                                </div>
                                               </div>
                                            </div>
                                       </asp:Panel>
                                            <asp:Button ID="btnImprimir" runat="server" Text="Imprimir Acuse"  CssClass="btn btn-default"  />
                                            <asp:Button ID="btnGuardar" CssClass="btn btn-success" runat="server" Text="Guardar" Width="150px" />
                                            <asp:Button ID="btnBandeja" CssClass="btn btn-default" runat="server" 
                                              Text="Ir a Bandeja Libro Inspección" Width="227px" />

                                       </div> 

                                   </div>  
                                 </asp:Panel>

                                 <div class="modal fade" id="modalRP" tabindex="-1" role="dialog" aria-labelledby="modalRPLabel">
                              <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                  <asp:UpdatePanel ID="UpdatePanelRP" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                    <ContentTemplate>
                                  <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="H1">IMPRESION DE ETIQUETA INTERVENCION</h4>
                                  </div>
                                  
                                  <div class="modal-body">
                                        <div id="divPreg" runat="Server"><strong> POSICION DE LA HOJA DONDE SERA IMPRESA LA ETIQUETA? </strong></div>
                                        
                                    <asp:Button id="Btnsup" runat="server" class="btn btn-default btn-block" Text="IMPRIME PARTE SUPERIOR DE LA HOJA" OnClick="Btnsup_Click" OnClientClick="Closepopup()" /> 
                                    <asp:Button id="Btnmed" runat="server" class="btn btn-default btn-block" Text="IMPRIME AL MEDIO DE LA HOJA" OnClick="Btnmed_Click" OnClientClick="Closepopup()" />
                                  </div>

                                  <div class="modal-footer">
                                    
                                    <button type="button" class="btn btn-default" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">Cerrar</span></button>
                                    <asp:HiddenField ID="hfpos" runat="server" />
                                    </div>
                                  </ContentTemplate>
                                  </asp:UpdatePanel>
                                </div>
                              </div>
                            </div>

                                 <asp:Panel ID="panelBarandilla" runat="server">
                                    
           
                    <h5 id="Titulopanelbarandilla" runat="server"><strong>BANDEJA LIBRO DE INSPECCION MANUAL</strong></h5>
                        <asp:GridView ID="grdManual" runat="server" 
                        CssClass="table table-condensed" 
                        AutoGenerateColumns="False" 
                        DataMember="DefaultView" Font-Size="X-Small"
                        GridLines="None" PageSize="5" TabIndex="34"
                                        Width="100%"
                        AllowPaging="True" DataKeyNames="idsucursal">
                        <AlternatingRowStyle BackColor="Gainsboro" />
                            <Columns>
                                <asp:BoundField DataField="IDBARANDILLADOCMANUAL" 
                                    HeaderText="Id." />
                                <asp:BoundField DataField="DOCUMENTO" HeaderText="Documento" />
                                <asp:BoundField DataField="DESCRIPCIONESTADO" HeaderText="Estado" />
                                <asp:BoundField DataField="FECHAALTA" HeaderText="F.Creación" 
                                    DataFormatString="{0:d}" />
                                <asp:BoundField DataField="FECHAPRESENTACION" HeaderText="Intervención" 
                                    DataFormatString="{0:d}" />
                                <asp:BoundField DataField="ANO" HeaderText="Año" />
                                <asp:BoundField DataField="MES" HeaderText="Mes" />                              
                                <asp:BoundField DataField="nrolibro" HeaderText="Nro Libro" />
                                <asp:BoundField DataField="n_observaciones" HeaderText="Fecha Cierre - Obs" />
                                <asp:TemplateField HeaderText="Reporte">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPdf" runat="server" onclick="btnPdf_Click"><i style="color:Red" class="fa fa-file-pdf-o fa-2x"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Acción">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnControlar" runat="server" onclick="btnControlar_Click"><i style="color:Green" class="fa fa-file-o fa-2x"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnEditar" runat="server" onclick="btnEditar_Click"><i class="fa fa-pencil-square-o fa-2x"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnReimprime" runat="server" onclick="btnReimprime_Click"><i style="color:Black" class="fa fa-print fa-2x"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:BoundField DataField="USUARIORESPONSABLE" HeaderText="Responsable" />
                            <%--    <asp:TemplateField HeaderText="Ref.">
                                    <ItemTemplate>
                                        <asp:Image ID="imgAprobado" runat="server" 
                                            ImageUrl="~/Images/Iconos/aprobado24x24.png" ToolTip="Aprobado" 
                                            Visible="False" />
                                        <asp:Image ID="imgRechazado" runat="server" 
                                            ImageUrl="~/Images/Iconos/rechazado24x24.png" ToolTip="Rechazado" 
                                            Visible="False" />
                                        <asp:Image ID="imgFueraTermino" runat="server" 
                                            ImageUrl="~/Images/Iconos/advertencia.PNG" Visible="False" 
                                            ToolTip="Fuera de Término" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                               
                            </Columns>
                            <PagerStyle BackColor="#666666" CssClass="gvpagination" Font-Names="Calibri" 
                                HorizontalAlign="Center" />
                            <RowStyle BackColor="#EEEEEE" />                          
                        </asp:GridView>
                
        
                                  </asp:Panel>
                            </div>
             </div>

   </div>
 </div>
 
 </div>
    
    <asp:HiddenField ID="hfnrocuenta" runat="server" />
    <asp:HiddenField ID="hfidsucursal" runat="server" />
    <asp:HiddenField ID="hfidbarandilla" runat="server" />
    <asp:HiddenField ID="hfcoddelegacion" runat="server" />
    <asp:HiddenField ID="hffecalta" runat="server" />
    <asp:HiddenField ID="hfmodal" runat="server" />
    <asp:HiddenField ID="hfRA" runat="server" />
    <asp:HiddenField ID="hfAcuerdo" runat="server" />

 
   <script type="text/javascript">
       $(document).on('ready', function () {
           //codigo aquí
           $('#sandbox-container input').datepicker({
               format: "dd/mm/yyyy",
               language: "pt-BR",
               autoclose: true
           });
       });
    </script>

    
     
      <script type="text/javascript">
          function Closepopup() {
              debugger;
              $('#modalRP').modal('hide');
          }
      </script>


</asp:Content>

