<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Pruebas.aspx.vb" Inherits="Pruebas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="JS/jquery-1.8.3.min.js" type="text/javascript"></script>
       <link href="Css/wickedpicker.css" rel="stylesheet" type="text/css" />
    <script src="JS/wickedpicker.js" type="text/javascript"></script>
    

</head>
<body>
   
    

    <form id="form1" runat="server">
 <asp:panel runat="server" visible="false">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>
 
 
    <asp:Button ID="Button4" runat="server" Text="QR" />

     <asp:Button ID="btnMarcaAgua" runat="server" Text="Marca de Agua" />
    <asp:Button ID="btnMarcaAgua0" runat="server" Text="Ejemplo" />

     <asp:Button ID="btnNotificaciones" runat="server" Text="NOTIFICACIONES" />

     <asp:updatepanel ID="updatepanel1" runat="server"  UpdateMode="Conditional">
        <ContentTemplate>
           <textarea name="msg" rows="10" cols="40">assdimdhskjksdlasd</textarea>
                <br> 
            <div id="qr" runat="server"></div>
          <asp:Image ID="ruta" runat="server" />
          </ContentTemplate>
      </asp:updatepanel>

    <table>
       <tr><td><asp:Button ID="Button1" runat="server" Text="APERTURA LIBRO INSPECCION"  style="height: 26px" /></td></tr>
       <tr><td><asp:Button ID="Button2" runat="server" Text="ACTA DE INFRACCION" /></td></tr>
        <tr><td><asp:Button ID="Button5" runat="server" Text="ACTA DE CONSTATACION" 
                Width="216px" /></td></tr>
        <tr><td><asp:Button ID="Button8" runat="server" Text="BANDEJA LIBRO INSPECCION" /></td></tr>
       <tr><td><asp:Button ID="Button3" runat="server" Text="DETECTAR BROWSER" /></td></tr>
      

       <tr><td><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td></tr>
       <tr><td><asp:Button ID="Button7" runat="server" Text="AUTORIZADOS" /></td></tr>

       
    
    </table>
    
    <asp:Button ID="Button6" runat="server" Text="Insertar Turnos" />

    <asp:Button ID="btnValidaHora" runat="server" Text="Function Validar Hora" />
    <asp:Button ID="Button9" runat="server" Text="Cambio Pass Empresas" />
    <div>Hora Inicio: <asp:TextBox ID="txthorainicio" runat="server"></asp:TextBox></div>
    <div>Hora Fin: <asp:TextBox ID="txthorafin" runat="server"></asp:TextBox></div>
    
     <input type="text" id="timepicker" name="timepicker" class="timepicker"/>

     

         <script type="text/javascript">
             var f = new Date();
             cad = f.getHours() + ":" + f.getMinutes();
             $('.timepicker').wickedpicker({now: cad, twentyFour: true, title:
                    'Seleccione La Hora', showSeconds: false 
             });
             //    $('.timepicker-24').wickedpicker({twentyFour: true});
        </script>

       
 

    <br />

        <asp:Button ID="BtnServerMap" runat="server" Text="Server.MapPath" />
 
        <asp:Button ID="btnPSP" runat="server" Text="PSP" />

    <asp:Button ID="btnpcpreporte" runat="server" Text="PCP-ReporteEmpleadores" />
    <asp:Button ID="btnpcpreporteTrabajadores" runat="server" 
        Text="PCP-ReporteTrabajadores" />

    <asp:Button ID="btnInsertaDom" runat="server" Text="InsertaDomicilio" />

    <asp:Button ID="Button10" runat="server" Text="Transaccion Walter" />
    <asp:HiddenField ID="hfIdEmpleador" runat="server" />
    <asp:HiddenField ID="hfCuilTrabajador" runat="server" />
   
    </asp:panel>
    <asp:Button ID="Button11" runat="server" Text="Siceramiento Domicilio" />
    <asp:TextBox ID="txtcuit" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="Button12" runat="server" Text="Eventos" />
    <asp:Button ID="btnEnviomails" runat="server" Text="Envio Mails" />
    <asp:TextBox ID="txtemail" runat="server" Width="258px"></asp:TextBox>
    <asp:Button ID="BtnEmpleados" runat="server" Text="EMPLEADOS ABM" />
    <asp:Button ID="Button13" runat="server" Text="Acredita Pagos" />
    <br />

    </form>

</body>
</html>
