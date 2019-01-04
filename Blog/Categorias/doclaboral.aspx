<%@ Page Language="VB" MasterPageFile="~/MasterBlog.master" AutoEventWireup="false" CodeFile="prensa.aspx.vb" Inherits="_Default" %>


<asp:Content ID="ContentPlaceHolder1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
    <div class="row">
        <div class="post col-md-12">
            <div class="post col-md-12">
                <h3>Novedades publicadas en Doc Laboral</h3>
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
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Blog/cambio-hojas-moviles.aspx" runat="server">
                        <asp:Image ID="Image2" ImageUrl="~/Images/Blog/libro-manual-hojas-moviles.png" runat="server" alt="provincia ayuda a La Cumbre" />
                        <h4>Cambio de Libro Manual a Hojas Moviles</h4>
                    </asp:HyperLink>
                    <p>
                        A los fines de facilitar la aplicación de la Res.04/2015, a partir de 24 de Abril hasta la completa implementación del sistema digital, se ha dispuesto el siguiente cambio: </p>
                        <span class="tag">Publicado 04-09-2015 :
                        <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Blog/Categorias/doclaboral.aspx" CssClass="doclaboral" runat="server">Doc Laboral </asp:HyperLink></span>
                </div>
            </div>
            <div class="post col-md-4">
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
            </div>
        </div>
    </div>
</asp:Content>
