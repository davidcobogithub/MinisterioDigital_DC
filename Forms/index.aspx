<%@ Page Language="VB" MasterPageFile="~/MasterBlog.master" AutoEventWireup="false" CodeFile="index.aspx.vb" Inherits="index" %>

<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="post col-md-12">
            <div class="post col-md-4">
                <div id="accesos">
                    <h2>
                        Accesos
                    </h2>
                    <ul>
                        <li><a href="LoginEmpresas.aspx">
                            <img src="Images/Iconos/empresas.png" />
                            Empresas </a></li>
                        <li>
                            <asp:HyperLink ID="HLK1" runat="server" OnDataBinding="HLK1_DataBinding" NavigateUrl="Index.aspx?pos=1">
                                         <img src="Images/Iconos/ministerio.png" /> Ministerio</asp:HyperLink>
                        </li>
                        <li><a href="frmMiRegistroLaboral.aspx">
                            <img src="Images/Iconos/trabajador.png" />
                            Trabajador</a> </li>
                        <li><a href="frmSitioOffline.aspx?par=25331" style="border: none; border-radius: 0 0 5px 5px;
                            -moz-border-radius: 0 0 5px 5px; -webkit-border-radius: 0 0 5px 5px">
                            <img src="Images/Iconos/Justicia.png" />Justicia </a></li>
                    </ul>
                </div>
            </div>
            <div class="post col-md-8">
                <!-- Slider -->
                <div id="myCarousel" class="carousel slide" data-ride="carousel">
                    <ol class="carousel-indicators">
                        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                        <li data-target="#myCarousel" data-slide-to="1"></li>
                        <li data-target="#myCarousel" data-slide-to="2"></li>
                        <li data-target="#myCarousel" data-slide-to="3"></li>
                        <li data-target="#myCarousel" data-slide-to="4"></li>
                        <li data-target="#myCarousel" data-slide-to="5"></li>
                        <li data-target="#myCarousel" data-slide-to="6"></li>
                        <li data-target="#myCarousel" data-slide-to="7"></li>
                    </ol>

                    <div class="carousel-inner" role="listbox">
                        <div class="item active">
                            <a href="">
                                <img src="Images/Blog/saludofiestas.png" />
                                <div class="carousel-caption">
<%--                                    <h3>
                                        Trabajo dispuso cuarto intermedio en el conflicto entre Municipalidad y Suoem</h3>--%>
                                </div>
                            </a>
                        </div>
                        <div class="item">
                            <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="https://www.facebook.com/ministeriodetrabajocordoba/" Target="_blank">
                                    <img src="Images/Blog/fanPageMinisterio.png" />
                                    <div class="carousel-caption">
                                    <h4 >                                       
                                        Ya está en línea la Fan Page del Ministerio de Trabajo de la Provincia de 
                                        Córdoba con el objeto de compartir información, notas, actividades 
                                        y todo lo referido a las distintas áreas y programas de la cartera laboral. 
                                    </h4>
                                    </div>
                            </asp:HyperLink>
                        </div>
                        <div class="item">
                            <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="https://youtu.be/yDcERX6aRJo" Target="_blank">
                                    <img src="Images/Blog/videoconsejofederal.jpg" />
                                    <div class="carousel-caption">
                                    <h4 >                                       
                                        El Consejo Federal del Trabajo vuelve a sesionar en Córdoba después de 12 años
                                    </h4>
                                    </div>
                            </asp:HyperLink>
                        </div>
                        <div class="item">
                             <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="https://youtu.be/v-gZp0Uh7zY" Target="_blank">                                                                                       
                                    <img src="Images/Blog/videplataformadigital.jpg" />
                                    <div class="carousel-caption">
                                    <h4 >                                       
                                        Plataforma Digital del Ministerio de Trabajo de Córdoba 
                                    </h4>
                                    </div>
                             </asp:HyperLink>
                        </div>
                        <div class="item">
                            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Blog/jornadasCymat.aspx">
                                        <img src="Images/Blog/Afiche-Jornadas.jpg" />
                                    <div class="carousel-caption">
                                    <h4 >
                                        Durante los días 26 y 27 de Abril del corriente año se llevaron a cabo las Jornadas de Salud y Seguridad Ocupacional "Trabajo Sano y Seguro" organizados por el Ministerio de Trabajo de la Provincia de Córdoba y la Superintendencia del Riesgo del Trabajo.
                                    </h4>
                                    </div>
                            </asp:HyperLink>
                        </div>
                        <div class="item">
                            <a href="">
                                <img src="Images/Blog/Reunion-SUOEM2.jpg" />
                                <div class="carousel-caption">
                                    <h3>
                                        Trabajo dispuso cuarto intermedio en el conflicto entre Municipalidad y Suoem</h3>
                                </div>
                            </a>
                        </div>
                        <div class="item">
                            <a href="" target="_parent">
                                <img src="Images/Blog/cordoba-despapeliza.png" alt="cordoba despapeliza Libros de Sueldos" />
                                <div class="carousel-caption">
                                    <%--<h3>Es normal el servicio de Autobuses Santa Fe</h3>--%>
                                </div>
                            </a>
                        </div>
                        <div class="item">
                            <a href="#">
                                <img src="Images/Blog/Asuncion-ministro-sereno.jpg" alt="Schiaretti puso en funciones al nuevo gabinete provincial" />
                                <div class="carousel-caption">
                                    <h3>
                                        Schiaretti puso en funciones al nuevo gabinete provincial
                                    </h3>
                                </div>
                            </a>
                        </div>
                    </div>
                    <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
                        <span class="glyphicon-chevron-left" aria-hidden="true"><i class="fa fa-angle-left">
                        </i></span></a><a class="right carousel-control" href="#myCarousel" role="button"
                            data-slide="next"><span class="glyphicon-chevron-right" aria-hidden="true"><i class="fa fa-angle-right">
                            </i></span></a>
                </div>
                <!-- End slider-->
            </div>
        </div>
    </div>
    <!-- End ROW-->
    <div class="row">
        <br>
    </div>

    <div class="row">
        <div class="post col-md-12 BottomMenu">
            <div class="post col-md-2">
                <div class="item" class="item">
                    <a href="frmMiregistroLaboral.aspx">Mi Registro Laboral</a></div>
            </div>
            <div class="post col-md-2">
                <div class="item">
                    <a href="frmConstanciaEmpleador.aspx">Constancia de Inscripción Laboral</a></div>
            </div>
            <div class="post col-md-2">
                <div class="item">
                    <a href="frmVencimientos.aspx">Vencimientos</a></div>
            </div>
            <div class="post col-md-2">
                <div class="item">
                    <a href="frmNumerosDelMinisterio.aspx">Estadisticas</a></div>
            </div>
            <%--                        <div class="post col-md-2">
                            <div class="item"><a href="#" class="disabled">Sistema de Turnos</a></div>
                        </div>--%>
            <div class="post col-md-2">
                <div class="item">
                    <a href="FrmConsultaInfoUtil.aspx">Normativa Vigente</a></div>
            </div>
            <div class="post col-md-2">
                <div class="item">
                    <a href="frmFaq.aspx">Preguntas Frecuentes</a></div>
            </div>
        </div>
    </div>

    <div class="row">
            <br />
    </div>

    <div class="row">
        <div class="post col-md-12 BottomMenu">
            <div class="post col-md-2">
                <div class="item">
                    <a href="#" data-toggle="modal" data-target="#largeModal">Descargas Útiles</a></div>
            </div>
            <div class="post col-md-2">
                <div class="item">
                    <a href="frmCymat.aspx">Cymat</a></div>
            </div>
            <div class="post col-md-2">
                <div class="item">
                    <a href="frmServiciosDigitales.aspx">Servicios Digitales</a></div>
            </div>
            <div class="post col-md-2">
                <div class="item">
                    <a href="frmTrabajoInfantil.aspx">Trabajo Infantil</a></div>
            </div>
            <div class="post col-md-2">
                <div class="item">
                    <a href="frmInspeccionDenuncias.aspx">Denuncias Inspección</a></div>
            </div>
            <div class="post col-md-2">
                <div class="item">
                    <a href="frmSitioOffline.aspx?par=25331">Turnos</a></div>
            </div>
        </div>
    </div>

    <div class="row"><br><br></div>

    <div class="row">
        <div class="post col-md-12">
            <div class="post col-md-4">
                <div class="article thumb">
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Blog/jornadasCymat.aspx" runat="server">
                        <asp:Image ID="Image1" ImageUrl="~/Images/Blog/Afiche-Jornadas.jpg" runat="server"
                            alt="Trabajo Infantil" />
                        <h4>
                            Jornadas de Salud y Seguridad Ocupacional - Trabajo Sano y Seguro.</h4>
                    </asp:HyperLink>
                    <p>
                        Durante los días 26 y 27 de Abril del corriente año se llevaron a cabo las Jornadas
                        de Salud y Seguridad Ocupacional &quot;Trabajo Sano y Seguro&quot; organizados por
                        el Ministerio de Trabajo de la Provincia de Córdoba y la Superintendencia del Riesgo
                        del Trabajo.</p>
                    <span class="tag">Publicado 02-05-2016 en :
                        <asp:HyperLink ID="HyperLink5" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa"
                            runat="server">Prensa </asp:HyperLink>
                    </span>
                </div>
            </div>
            <div class="post col-md-4">
                <div class="article thumb">
                    <asp:HyperLink ID="Hyperlink11" NavigateUrl="~/Blog/ministro-cuarto-intermedio.aspx"
                        runat="server">
                        <asp:Image ID="Image2" ImageUrl="~/Images/Blog/Reunion-SUOEM2.jpg" runat="server"
                            alt="Ministro Trabajo Reunión SUOEM" />
                        <h4>
                            Trabajo dispuso cuarto intermedio en el conflicto entre Municipalidad y Suoem
                        </h4>
                    </asp:HyperLink>
                    <p>
                        El dictamen tiene por objetivo que las partes puedan continuar con el diálogo iniciado
                        hoy, comprometiéndose ambas al mantenimiento de la paz social y laboral, contribuyendo
                        al mejor ámbito de negociación procurando el avenimiento.</p>
                    <span class="tag">Publicado 16-12-2015 en:
                        <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa"
                            runat="server">Prensa </asp:HyperLink></span>
                    <%--<asp:HyperLink ID="HyperLink2" NavigateUrl="~/Blog/servicio-normal-autobus.aspx" runat="server">
                                    <asp:Image ID="Image2" ImageUrl="~/Images/Blog/ministro-brito-en-planta-industrial-23.jpg" runat="server" alt="Brito en planta industrial" />
                                    <h4>Es normal el servicio de Autobuses Santa Fe</h4>
                                </asp:HyperLink>
                                <p>El titular de Trabajo, Adrián Brito, encabezó las negociaciones para garantizar el retorno de los choferes a sus labores y la reanudación del servicio que presta Autobuses Santa Fe en los corredores 4, 5, 7, 500 y 600, interrumpido hoy a las 4 de la madrugada.</p>
                                <span class="tag"> Publicado 13-07-2015 en : <asp:HyperLink ID="HyperLink4" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa" runat="server">Prensa </asp:HyperLink> </span> --%>
                </div>
            </div>
            <div class="post col-md-4">
                <div class="article thumb">
                    <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Blog/nuevo-gabinete.aspx" runat="server">
                        <asp:Image ID="Image3" ImageUrl="~/Images/Blog/Asuncion-ministro-sereno.jpg" runat="server"
                            alt="Schiaretti puso en funciones al nuevo gabinete provincial" />
                        <h4>
                            Schiaretti puso en funciones al nuevo gabinete provincial</h4>
                    </asp:HyperLink>
                    <p>
                        En el acto que se desarrolla en el Centro Cívico de la ciudad de Córdoba asumen
                        los miembros del gabinete que acompañará al gobernador Juan Schiaretti al frente
                        de la gestión provincial.</p>
                    <span class="tag">Publicado 10-12-2015 en :
                        <asp:HyperLink ID="HyperLink6" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa"
                            runat="server">Prensa </asp:HyperLink>
                    </span>
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
                    $(document).ready(function () {
                        $('[data-toggle="tooltip"]').tooltip();
                    });
    </script>

</asp:Content>

       