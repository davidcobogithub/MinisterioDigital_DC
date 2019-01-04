<%@ Page Language="VB" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" CodeFile="LoginEmpresas.aspx.vb" Inherits="LoginEmpresas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>ingreso Empresas</title>
    <link rel="stylesheet" type="text/css" href="Css/fonts.css" />
    <link rel="stylesheet" type="text/css" href="Css/animate.min.css" />
    <link href="font-awesome-4.3.0/css/font-awesome.css" rel="stylesheet" type="text/css" /> 
    <link href="css/bootstrap-3.1.1.min.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-custom.css" rel="stylesheet" type="text/css" />
    <link href="css/styles.css" rel="stylesheet" type="text/css" />
    <script src='<%=ResolveClientUrl("~/Js/jquery-1.11.2.min.js") %>' type="text/javascript"></script>
    <script src='<%=ResolveClientUrl("~/Js/bootstrap.min.js") %>' type="text/javascript"></script>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->

<style type="text/css">
    .container {
        max-width: 960px;    
    }
        
    #wrap {
        padding-top: 160px; 
    }                  
</style>

</head>
<body>
    <div id="top"></div>
    <nav class="navbar navbar-inverse navbar-fixed-top" id="TopBar" role="navigation"> 
        <div class="container text-center"> 
            <div class="navbar-header" style="float: none; padding: 20px 10px;">
                <a class="navbar-brand" style="float: none;" href="http://trabajo.cba.gov.ar/index.aspx">
                    <img src="Images/logo.png" alt="Ministerio de Trabajo digital - Documentacion Laboral" />
                </a>
            </div>
        </div>
    </nav>
    <!--<header></header>-->

    <div id="wrap">
        <form id="form1" runat="server" class="container">
            <div class="panel panel-default animated zoomIn" style="max-width: 390px; margin: 0 auto;">
                <div class="panel-heading text-center">
                   <h4>
                        <strong style="color: #4e2d5a;">INGRESO EMPRESAS</strong>
                   </h4>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <h5><strong>CUIT</strong></h5>
                        <div style="position: relative; float: none">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-user fa-fw"></i></span>
                                <asp:TextBox ID="txtCuit" runat="server" MaxLength="11" CssClass="form-control" placeholder="CUIT"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <h5><strong>CLAVE</strong></h5>
                        <div style="position: relative; float: none;">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-lock fa-fw"></i></span>
                                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" placeholder="CLAVE" TextMode="Password"> </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-warning" Text="Aceptar" style="width: 100%" />
                        <br/>
                        <asp:Label ID="lblMensajeUsuarioNoEncontrado" runat="server" Style="font-weight: bold; font-size: 10pt; color: #614261; font-family: Droid Sans; font-style: italic; text-align: center" Text="Cuit o Clave Inválida" Width="315px"> </asp:Label>
                        <br />
                        <asp:LinkButton ID="LinkPrimerIngreso" runat="server" class="btn btn-success"  style="width: 100%" >Empadronamiento</asp:LinkButton>
                        <br /><br />
                        <asp:LinkButton ID="LinkFiscalizacionPCP" runat="server" class="btn btn-success"  style="width: 100%" >Fiscalización PCP</asp:LinkButton>
                    </div>
                </div>
            </div>
        </form><!-- Cierra Container -->
    </div><!-- Cierra #wrap -->

    <footer id="footer" class="footer text-center">
        <div class="container footer-container">
            <div class="row">
                <div class="col-sm-6 col-lg-6 col-md-6">
                    <h4> Novedades</h4>
                    <ul>
<%--                        <li>
                            <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Blog/servicio-normal-autobus.aspx" runat="server">
                                <i class="fa fa-chevron-right"></i>Es normal el servicio de Autobuses Santa Fe
                            </asp:HyperLink>
                        </li>--%>
                        <li> 
                            <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Blog/contra-trabajo-infantil.aspx" runat="server">
                                <i class="fa fa-chevron-right"></i>Córdoba se suma al Día Mundial Contra el Trabajo Infantil
                            </asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Blog/nuevo-gabinete.aspx" runat="server">
                                <i class="fa fa-chevron-right"></i>
                               Schiaretti puso en funciones al nuevo gabinete provincial
                            </asp:HyperLink>
                        </li>
                        <li>
                            <asp:HyperLink ID="Hyperlink5" NavigateUrl="~/Blog/cambio-hojas-moviles.aspx" runat="server">
                                <i class="fa fa-chevron-right"></i>Cambio de Libro Manual a Hojas Moviles
                            </asp:HyperLink>
                        </li>
                         <li>
                            <asp:HyperLink ID="Hyperlink6" NavigateUrl="~/Blog/ministro-cuarto-intermedio.aspx" runat="server">
                                <i class="fa fa-chevron-right"></i>Trabajo dispuso cuarto intermedio en el conflicto entre Municipalidad y Suoem
                            </asp:HyperLink>
                        </li>
                    </ul>
                </div>
                <div class="col-sm-3 col-lg-3 col-md-3">
                    <h4>Aplicativos</h4>
                    <ul>
                         <li><a href="frmMiRegistroLaboral.aspx"><i class="fa fa-chevron-right"></i>Mi registro Laboral</a></li>
                        <li><a href="frmConstanciaEmpleador.aspx"><i class="fa fa-chevron-right"></i>Constancia de Inscripción Laboral</a></li>
                        <li><a href="frmVencimientos.aspx"><i class="fa fa-chevron-right"></i>Vencimientos</a></li>
                        <li><a href="frmNumerosDelMinisterio.aspx"><i class="fa fa-chevron-right"></i>Estadísticas</a></li>
                        <li><a href="frmFaq.aspx"><i class="fa fa-chevron-right"></i>Preguntas Frecuentes</a></li>      
                    </ul>
                </div>
                <div class="col-sm-3 col-lg-3 col-md-3">
                    <h4>Links de interés</h4>
                    <ul>
                        <li><a href="http://www.cba.gov.ar/" target="_blank"><i class="fa fa-chevron-right"></i>Gobierno de la Provincia</a></li>
                        <li>
                            <a href="http://www.trabajo.gob.ar/" target="_blank"><i class="fa fa-chevron-right"></i>Ministerio de Trabajo, Empleo y Seguridad Social de la Nación </a>
                        </li>
                        <li><a href="http://www.afip.gob.ar/home/index.html" target="_blank"><i class="fa fa-chevron-right"></i>AFIP</a></li>
                        <li><a href="http://www.srt.gob.ar/" target="_blank"><i class="fa fa-chevron-right"></i>Super Intendencia de Riesgos de Trabajo</a></li>    
                         <li><a href="frmLinkInteres.aspx"><i class="fa fa-chevron-right"></i>Más...</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div id="bottom-bar"> 
            <div class="container" style="display: table;">
                <p>
                    <span>Ministerio de Trabajao de Córdoba</span> 
                    todos los derecheos Reservados.
                </p>
                <a href="#top" id="goTop">VOLVER AL INICIO<i class="fa fa-arrow-circle-up fa-large"></i></a>
            </div>
        </div>
   </footer>    

   
    
   <script>
       $('a[href^="#"]').on('click', function (event) {
           var target = $($(this).attr('href'));
           if (target.length) {
               event.preventDefault();
               $('html, body').animate({ scrollTop: target.offset().top }, 1000);
               }
        });
           //@ sourceURL=pen.js
    </script>

    
</body>
</html>

