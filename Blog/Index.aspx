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
<%--                <asp:HyperLink ID="HyperLinka" NavigateUrl="~/Blog/contra-trabajo-infantil.aspx"
                    runat="server">
                    <asp:Image ID="Image1" ImageUrl="../Images/Blog/trabajo-infantil.png" runat="server"
                    <img src="" />
                    <h4>
                        Córdoba se suma al Día Mundial Contra el Trabajo Infantil</h4>
                </asp:HyperLink>--%>
                <asp:HyperLink ID="HyperLink4" NavigateUrl="~/Blog/contra-trabajo-infantil.aspx"
                    runat="server">
                    <asp:Image ID="Image1" ImageUrl="~/Images/Blog/trabajo-infantil.png" 
                        runat="server" alt="Jornadas CyMat" />

                    <h4>
                       Jornadas de Salud y Seguridad Ocupacional - Trabajo Sano y Seguro.</h4>
                </asp:HyperLink>
                <p>
                    Bajo el lema &#8220;Voces y música por una infancia con derechos, NO al trabajo infantil&#8221;,
                    alumnos de distintas escuelas compartirán una suelta de globos, coros y otras actividades
                    para celebrar sus derechos.</p>
                <span class="tag">Publicado 11-06-2015 en :
                    <asp:HyperLink ID="HyperLinkb" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa"
                        runat="server">Prensa </asp:HyperLink>
                </span>
            </div>
        </li>
        <li data-type="prensa" data-id="id-2" class="post col-md-4">
            <div class="article">
                <asp:HyperLink ID="HyperLinkd" NavigateUrl="~/Blog/jornadasCymat.aspx"
                    runat="server">
                    <asp:Image ID="Image6" ImageUrl="~/Images/Blog/Afiche-Jornadas.jpg" 
                        runat="server" alt="Jornadas CyMat" />

                    <h4>
                       Jornadas de Salud y Seguridad Ocupacional - Trabajo Sano y Seguro.</h4>
                </asp:HyperLink>
                <p>
                    El Señor Ministro de Trabajo, Dr. Omar Sereno, tiene el agrado de invitar a usted a participar en las 
                    JORNADAS DE SALUD Y SEGURIDAD OCUPACION</p>
                <span class="tag">Publicado 20-04-2016 en :
                    <asp:HyperLink ID="HyperLinkc" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa"
                        runat="server">Prensa </asp:HyperLink>
                </span>
            </div>
        </li>
        <li data-type="prensa" data-id="id-3" class="post col-md-4">
            <div class="article">
                <asp:HyperLink ID="HyperLinke" NavigateUrl="~/Blog/nuevo-gabinete.aspx"
                    runat="server">
                    <asp:Image ID="Image7" ImageUrl="~/Images/Blog/Asuncion-ministro-sereno.jpg" runat="server" alt="Schiaretti puso en funciones al nuevo gabinete provincial" />
                    <h4>Schiaretti puso en funciones al nuevo gabinete provincial</h4>
                </asp:HyperLink>
                <p>En el acto que se desarrolla en el Centro Cívico de la ciudad de Córdoba asumen los miembros del gabinete que acompañará al gobernador Juan Schiaretti al frente de la gestión provincial.</p>
                <span class="tag">Publicado 10-12-2015 en :
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
        <li data-type="prensa" data-id="id-5" class="post col-md-4">
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