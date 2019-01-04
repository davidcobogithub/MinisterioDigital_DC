<%@ Page Language="VB" MasterPageFile="~/MasterBlog.master" AutoEventWireup="false" CodeFile="Index.aspx.vb" Inherits="_Default" %>


<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<style>
     .filter {}
</style>

<div class="">
  <hr>
  <ul class="filter nav nav-pills">
    <li data-value="all"><a href="#">Todas</a></li>
    <li data-value="prensa"><a href="#1" style="background: #4eb073; color: #fff;">Prensa</a></li>
    <li data-value="doclaboral"><a href="#2" style="background: #e5bc5a; color: #fff;">Doclaboral</a></li>
  </ul>

  <hr>
    <ul class="thumbnails post col-md-12" style="padding-left: 0">
        <li data-type="prensa" data-id="id-1" class="post col-md-4">
            <div class="article">
                <asp:HyperLink ID="HyperLinka" NavigateUrl="~/Blog/contra-trabajo-infantil.aspx"
                    runat="server">
                    <asp:Image ID="Image1" ImageUrl="~/Images/Blog/trabajo-infantil.png" runat="server"
                        alt="Trabajo Infantil" />
                    <h4>
                        Córdoba se suma al Día Mundial Contra el Trabajo Infantil</h4>
                </asp:HyperLink>
                <p>
                    Bajo el lema “Voces y música por una infancia con derechos, NO al trabajo infantil”,
                    alumnos de distintas escuelas compartirán una suelta de globos, coros y otras actividades
                    para celebrar sus derechos.</p>
                <span class="tag">Publicado 11-06-2015 en :
                    <asp:HyperLink ID="HyperLinkb" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa"
                        runat="server">Prensa </asp:HyperLink>
                </span>
            </div>
        </li>
        <%--<li data-type="prensa" data-id="id-2" class="post col-md-4">
            <div class="article">
                <asp:HyperLink ID="HyperLinkd" NavigateUrl="~/Blog/servicio-normal-autobus.aspx"
                    runat="server">
                    <asp:Image ID="Image6" ImageUrl="~/Images/Blog/ministro-brito-en-planta-industrial-23.jpg"
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
                    <asp:HyperLink ID="HyperLinkc" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa"
                        runat="server">Prensa </asp:HyperLink>
                </span>
            </div>
        </li>--%>
        <li data-type="prensa" data-id="id-3" class="post col-md-4">
            <div class="article">
                <asp:HyperLink ID="HyperLinke" NavigateUrl="~/Blog/provincia-auditara-cumbre.aspx"
                    runat="server">
                    <asp:Image ID="Image7" ImageUrl="~/Images/Blog/ministro-de-gob.jpg" runat="server" alt="provincia ayuda a La Cumbre" />
                    <h4> La Provincia ayudará a La Cumbre y auditará sus finanzas</h4>
                </asp:HyperLink>
                <p>
                    El Ministro de Gobierno y Seguridad, Marcos Farina, y su par de Trabajo, Adrián
                    Brito, se lo informaron al intendente de La Cumbre, Carlos Engel en una reunión
                    mantenida esta tarde.</p>
                <span class="tag">Publicado 04-09-2015 en :
                    <asp:HyperLink ID="HyperLinkf" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa"
                        runat="server">Prensa </asp:HyperLink>
                </span>
            </div>
        </li>
        <%--<li data-type="doclaboral" data-id="id-4" class="post col-md-4">
            <div class="article">
                <asp:HyperLink ID="HyperLink7" NavigateUrl="~/Blog/depuracion-base-datos.aspx" runat="server">
                    <asp:Image ID="Image4" ImageUrl="~/Images/Blog/base-de-datos.png" runat="server"
                        alt="provincia ayuda a La Cumbre" />
                    <h4>
                        Depuración de Bases de Datos</h4>
                </asp:HyperLink>
                <p>
                   En  virtud  de  la  implementa
ción  de  la  Res.  04/2015  y    los  fines  de 
proceder a depurar la base, a partir del día 4 de M
ayo de 2015, se correrán los siguientes 
procedimientos: </p>
                <span class="tag">Publicado 04-09-2015 :
                    <asp:HyperLink ID="HyperLink8" NavigateUrl="~/Blog/Categorias/doclaboral.aspx" CssClass="doclaboral"
                        runat="server">Doc Laboral </asp:HyperLink></span>
            </div>
        </li>--%>
        <li data-type="doclaboral" data-id="id-4" class="post col-md-4">
            <div class="article">
                <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Blog/cambio-hojas-moviles.aspx" runat="server">
                    <asp:Image ID="Image2" ImageUrl="~/Images/Blog/libro-manual-hojas-moviles.png" runat="server"
                        alt="provincia ayuda a La Cumbre" />
                    <h4>
                        Cambio de Libro Manual a Hojas Moviles</h4>
                </asp:HyperLink>
                <p>
                  A  los  fines  de  facilitar  la  aplicación  de  la  Res.04
/2015,  a  partir  de  24  de  abril  hasta  la 
completa implementación del sistema digital, se ha 
dispuesto el siguiente cambio:  </p>
                <span class="tag">Publicado 04-09-2015 :
                    <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Blog/Categorias/doclaboral.aspx" CssClass="doclaboral"
                        runat="server">Doc Laboral </asp:HyperLink></span>
            </div>
        </li>
        <li data-type="prensa" data-id="id-3" class="post col-md-4">
            <div class="article">
                <asp:HyperLink ID="Hyperlink11" NavigateUrl="~/Blog/ministro-cuarto-intermedio.aspx"  runat="server">
                    <asp:Image ID="Image3" ImageUrl="~/Images/Blog/Reunion-SUOEM2.jpg" runat="server" alt="Ministro Trabajo Reunión SUOEM" />
                    <h4>
                         Trabajo dispuso cuarto intermedio en el conflicto entre Municipalidad y Suoem
                    </h4>
                    </asp:HyperLink>
                    <p>El dictamen tiene por objetivo que las partes puedan continuar con el diálogo iniciado hoy, comprometiéndose ambas al mantenimiento de la paz social y laboral, contribuyendo al mejor ámbito de negociación procurando el avenimiento.</p>
                <span class="tag">Publicado 16-12-2015 en: <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa" runat="server">Prensa </asp:HyperLink></span>
             </div>
         </li>
         <li data-type="doclaboral" data-id="id-4" class="post col-md-4">
            <div class="article">
                <asp:HyperLink ID="Hyperlink4" NavigateUrl="http://comercioyjusticia.info/blog/profesionales/cordoba-despapeliza-libros-sueldos-desde-mayo/#content"  runat="server" Target="_blank">
                    <asp:Image ID="Image4" ImageUrl="~/Images/Blog/cordoba-despapeliza.png" runat="server" alt="Cordoba Despapeliza" />
                    <h4>
                        Córdoba Despapeliza libros de sueldos desde Mayo
                    </h4>
                    </asp:HyperLink>
                    <p>El ministro Adrián Brito detalló cómo se implementará la resolución 4/2015, que obliga a todas las empresas que llevan hojas móviles a pasarse al nuevo sistema.</p>
                <span class="tag">Publicado 02-09/2015 en: <asp:HyperLink ID="HyperLink5" NavigateUrl="~/Blog/Categorias/doclaboral.aspx" CssClass="doclaboral" runat="server">DocLaboral </asp:HyperLink></span>
             </div>
         </li>
    </ul>
<%--    <ul class="thumbnails post col-md-12">
        
    </ul>--%>


<%--
  <ul class="thumbnails">
    <li data-type="dog" data-id="id-1" class="span3">
      <a href="#" class="thumbnail" id="dog1"><div class="red">sdf</div></a>
    </li>
    <li data-type="cat" data-id="id-2" class="span3">
      <a href="#" class="thumbnail" id="cat1"><div class="blue">sdf</div></a>
    </li>
    <li data-type="bird" data-id="id-3" class="span3">
      <a href="#" class="thumbnail" id="bird1"><div class="green">sdf</div></a>
    </li>
    <li data-type="dog" data-id="id-4" class="span3">
      <a href="#" class="thumbnail" id="dog2"><div class="red">sdf</div></a>
    </li>
    <li data-type="cat" data-id="id-5" class="span3">
      <a href="#" class="thumbnail" id="cat2"><div class="blue">sdf</div></a>
    </li>
    <li data-type="bird" data-id="id-6" class="span3">
      <a href="#" class="thumbnail" id="bird2"><div class="green">sdf</div></a>
    </li>
    <li data-type="dog" data-id="id-7" class="span3">
      <a href="#" class="thumbnail" id="dog3"><div class="red">sdf</div></a>
    </li>
    <li data-type="cat" data-id="id-8" class="span3">
      <a href="#" class="thumbnail" id="cat3"><img src="images/cat3.jpg" alt=""></a>
    </li>
    <li data-type="bird" data-id="id-9" class="span3">
      <a href="#" class="thumbnail" id="bird3"><img src="images/bird3.jpg" alt=""></a>
    </li>
    <li data-type="dog" data-id="id-10" class="span3">
      <a href="#" class="thumbnail" id="dog4"><img src="images/dog4.jpg" alt=""></a>
    </li>
    <li data-type="cat" data-id="id-11" class="span3">
      <a href="#" class="thumbnail" id="cat4"><img src="images/cat4.jpg" alt=""></a>
    </li>
    <li data-type="bird" data-id="id-12" class="span3">
      <a href="#" class="thumbnail" id="bird4"><img src="images/bird4.jpg" alt=""></a>
    </li>
    <li data-type="dog" data-id="id-13" class="span3">
      <a href="#" class="thumbnail" id="dog5"><img src="images/dog5.jpg" alt=""></a>
    </li>
    <li data-type="cat" data-id="id-14" class="span3">
      <a href="#" class="thumbnail" id="cat5"><img src="images/cat5.jpg" alt=""></a>
    </li>
    <li data-type="bird" data-id="id-15" class="span3">
      <a href="#" class="thumbnail" id="bird5"><img src="images/bird5.jpg" alt=""></a>
    </li>
  </ul>--%>

</div>
           

<!--<script type='text/javascript' src='https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js'></script>-->
<script type='text/javascript' src='../JS/jquery-1.8.3.min.js'></script>
<script type="text/javascript" src="js/jquery.quicksand.js"></script>
<script>
  function gallery() {
    }
    var $itemsHolder = $('ul.thumbnails');
    var $itemsClone = $itemsHolder.clone();
    var $filterClass = "";
    $('ul.filter li').click(function (e) {
        e.preventDefault();
        $filterClass = $(this).attr('data-value');
        if ($filterClass == 'all') { var $filters = $itemsClone.find('li'); }
        else { var $filters = $itemsClone.find('li[data-type=' + $filterClass + ']'); }
        $itemsHolder.quicksand(
          $filters,
          { duration: 1000 },
          gallery
          );
    });
    $(document).ready(gallery);
</script>

</asp:Content>