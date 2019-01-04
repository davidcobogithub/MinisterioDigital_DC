<%@ Page Language="VB" MasterPageFile="~/MasterBlog.master" AutoEventWireup="false" CodeFile="prensa.aspx.vb" Inherits="_Default" %>


<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">
    <div class="row">
        <div class="post col-md-12">
            <div class="post col-md-12">
                <h3>
                    Novedades publicadas en Prensa</h3>
            </div>
        </div>
    </div>
    <div class="post col-md-12">
        <br>
    </div>
    <div class="row">
        <div class="post col-md-12">
            <div class="post col-md-4">
                <div class="article">
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Blog/jornadasCymat.aspx"
                        runat="server">
                        <asp:Image ID="Image1" ImageUrl="~/Images/Blog/Afiche-Jornadas.jpg" runat="server"
                            alt="Jornadas CyMat" />
                        <h4>
                             Jornadas de Salud y Seguridad Ocupacional - Trabajo Sano y Seguro.</h4>
                    </asp:HyperLink>
                    <p>
                         El Señor Ministro de Trabajo, Dr. Omar Sereno, tiene el agrado de invitar a usted a participar en las 
                        JORNADAS DE SALUD Y SEGURIDAD OCUPACION
                    </p>
                    <span class="tag">Publicado 20-04-2016 en :
                        <asp:HyperLink ID="HyperLink6" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa"
                            runat="server">Prensa </asp:HyperLink>
                    </span>
                </div>
            </div>
<%--            <div class="post col-md-4">
                <div class="article">
                    <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Blog/servicio-normal-autobus.aspx"
                        runat="server">
                        <asp:Image ID="Image2" ImageUrl="~/Images/Blog/ministro-brito-en-planta-industrial-23.jpg"
                            runat="server" alt="Brito en planta industrial" />
                        <h4>
                            Es normal el servicio de Autobuses Santa Fe</h4>
                    </asp:HyperLink>
                    <p>
                        El titular de Trabajo, Adrián Brito, encabezó las negociaciones para garantizar
                        el retorno de los choferes a sus labores y la reanudación del servicio que presta
                        Autobuses Santa Fe en los corredores 4, 5, 7, 500 y 600, interrumpido hoy a las
                        4 de la madrugada.</p>
                    <span class="tag">Publicado 13-07-2015 en :
                        <asp:HyperLink ID="HyperLink4" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa"
                            runat="server">Prensa </asp:HyperLink>
                    </span>
                </div>
            </div>--%>

            <div class="post col-md-4">
                            <div class="article thumb">

                <asp:HyperLink ID="Hyperlink11" NavigateUrl="~/Blog/ministro-cuarto-intermedio.aspx"  runat="server">
                    <asp:Image ID="Image2" ImageUrl="~/Images/Blog/Reunion-SUOEM2.jpg" runat="server" alt="Ministro Trabajo Reunión SUOEM" />
                    <h4>
                        Trabajo dispuso cuarto intermedio en el conflicto entre Municipalidad y Suoem
                    </h4>
                    </asp:HyperLink>
                    <p>El dictamen tiene por objetivo que las partes puedan continuar con el diálogo iniciado hoy, comprometiéndose ambas al mantenimiento de la paz social y laboral, contribuyendo al mejor ámbito de negociación procurando el avenimiento.</p>
                <span class="tag">Publicado 16-12-2015 en: <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa" runat="server">Prensa </asp:HyperLink></span>

                                <%--<asp:HyperLink ID="HyperLink2" NavigateUrl="~/Blog/servicio-normal-autobus.aspx" runat="server">
                                    <asp:Image ID="Image2" ImageUrl="~/Images/Blog/ministro-brito-en-planta-industrial-23.jpg" runat="server" alt="Brito en planta industrial" />
                                    <h4>Es normal el servicio de Autobuses Santa Fe</h4>
                                </asp:HyperLink>
                                <p>El titular de Trabajo, Adrián Brito, encabezó las negociaciones para garantizar el retorno de los choferes a sus labores y la reanudación del servicio que presta Autobuses Santa Fe en los corredores 4, 5, 7, 500 y 600, interrumpido hoy a las 4 de la madrugada.</p>
                                <span class="tag"> Publicado 13-07-2015 en : <asp:HyperLink ID="HyperLink4" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa" runat="server">Prensa </asp:HyperLink> </span> --%>
                            </div>
                        </div>
         
            <div class="post col-md-4">
                <div class="article">
                    <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Blog/nuevo-gabinete.aspx" runat="server">
                        <asp:Image ID="Image3" ImageUrl="~/Images/Blog/Asuncion-ministro-sereno.jpg" runat="server" alt="Schiaretti puso en funciones al nuevo gabinete provincial" />
                        <h4>
                            Schiaretti puso en funciones al nuevo gabinete provincial</h4>
                    </asp:HyperLink>
                    <p>
                        En el acto que se desarrolla en el Centro Cívico de la ciudad de Córdoba asumen los miembros del gabinete que acompañará al gobernador Juan Schiaretti al frente de la gestión provincial.</p>
                    <span class="tag">Publicado 10-12-2015 en :
                        <asp:HyperLink ID="HyperLink5" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa"
                            runat="server">Prensa </asp:HyperLink>
                    </span>
                </div>
            </div>
       
   </div>
</asp:Content>
