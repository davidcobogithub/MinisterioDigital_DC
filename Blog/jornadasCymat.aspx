<%@ Page Title="" Language="VB" MasterPageFile="~/MasterBlog.master" AutoEventWireup="false" CodeFile="jornadasCymat.aspx.vb" Inherits="Blog_jornadasCymat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>

<article class="post"> 
    <span class="tag">02 de Mayo de 2016 <asp:HyperLink ID="HyperLink4" NavigateUrl="~/Blog/Categorias/prensa.aspx" CssClass="prensa" runat="server">Prensa </asp:HyperLink> </span> 
<a>    <h2>Jornadas de Salud y Seguridad Ocupacional <br /> Trabajo Sano y Seguro.</h2></a>

    <br />
    <br />
    <div style="text-align: center"  >    
        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/Images/Blog/Programajornadas.jpg"> <img src="../Images/Blog/Programajornadas.jpg" width="800px" height="800px"/></asp:HyperLink>      
    </div>
    <br />
    <br />
    <div class="post-entry">  
        <p style="text-align: justify;">
            Durante los días 26 y 27 de Abril del corriente año se llevaron a cabo las 
            Jornadas de Salud y Seguridad Ocupacional &quot;Trabajo Sano y Seguro&quot; organizados 
            por el Ministerio de Trabajo de la Provincia de Córdoba y la Superintendencia 
            del Riesgo del Trabajo.           
        </p>
        <p style="text-align: justify;">
            Durante el trascurso del mismo se trataron temas como los siguientes:            
        </p>
        <table>
            <%--HOSTIGAMIENTO--%>
            <tr>
                <td class="style1">
                    <b>Hostigamiento Laboral: Alcances y limites.</b>
                    <br />
                    <i><u>Disertantes:</u></i><br />
                </td>
                <td>
                </td>
            </tr>
            <tr style="height:25px;">
                <td class="style1">
                    <i> Médica Psiquiatra Grisel de Pascuale Arias</i><br />
                </td>
                <td>
                    <asp:HyperLink ID="HyperLinkHostigamiento1" 
                        NavigateUrl="~/Images/Blog/PPTHostigamiento02.pptx" CssClass="prensa" runat="server">Descargar Presentación</asp:HyperLink>
                </td>
            </tr>
            <tr style="height:25px;">
                <td class="style1">
                    <i>Dr. Carlos Toselli, vocal de la Cámara del Trabajo</i>
                </td>
                <td>
                    <asp:HyperLink ID="HyperLinkHostigamiento2" NavigateUrl="~/Images/Blog/PPTHostigamiento01.ppt" CssClass="prensa" runat="server">Descargar Presentación</asp:HyperLink>
                </td>
            </tr>
            <tr>
            <td>
            <br />
            </td>
            <td></td>
            </tr>
            <%--ELECTRICO--%>
            <tr>
                <td class="style1">
                    <b>Similitudes y Diferencias en la aplicación de la Resolución SRT No. 3068/14 (Reglamento para trabajos con tensión hasta 1000v) y Ley Provincial de Seguridad Eléctrica.</b>
                    <br />
                    <i><u>Disertantes:</u></i><br />
                </td>
                <td>
                </td>
            </tr>
            <tr  style="height:25px;">
                <td class="style1">
                    <i>Ing. Carlos Plencovich por la E.P.E.C.</i>
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Images/Blog/PPTElectrico03PLENCOVICH.ppt" CssClass="prensa" runat="server">Descargar Presentación</asp:HyperLink>
                </td>
            </tr>
            <tr  style="height:25px;">
                <td class="style1">
                    <i>Ing. Roberto García de la S.R.T.</i>
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Images/Blog/PPTElectrico01GARCIA.pptx" CssClass="prensa" runat="server">Descargar Presentación</asp:HyperLink>
                </td>
            </tr>
            <tr  style="height:25px;">
                <td class="style1">
                    <i>Ing. Cristian Miotti en representacion de ERSEP</i>
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink5" NavigateUrl="~/Images/Blog/PPTElectrico02MIOTTI.pptx" CssClass="prensa" runat="server">Descargar Presentación</asp:HyperLink>
                </td>
            </tr>
            <tr  style="height:25px;">
                <td class="style1">
                    <i>Ing. Miguel Piumetto, Director del Departamento de Electrotecnia de la UNC.</i>
                </td>
                <td>
                    <span style="font-weight: bold; color: #FF0000">***</span>                    
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td>
                </td>
            </tr>
            <%--RURAL--%>
            <tr>
                <td class="style1">
                    <b>Condiciones y Medio Ambiente del Trabajo en el Sector Rural</b>
                    <br />
                    <i><u>Disertantes:</u></i><br />
                </td>
                <td>
                </td>
            </tr>
            <tr  style="height:25px;">
                <td class="style1">
                    <i>Bioq. Marcela Petrillo repreentante de UATRE</i>
                </td>
                <td>
                    <span style="font-weight: bold; color: #FF0000">***</span>                    
                </td>
            </tr>
            <tr  style="height:25px;">
                <td class="style1">
                    <i>Dra. Elisa Lubrano Lavadera representante de UATRE</i>
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink6" NavigateUrl="~/Images/Blog/PPTRURALLUBRANOLAVADERA.pptx" CssClass="prensa" runat="server">Descargar Presentación</asp:HyperLink>                                       
                </td>
            </tr>
            <tr  style="height:25px;">
                <td class="style1">
                    <i>Francisco Demarchi, Vicepresidente de la Sociedad Rural de Rio Cuarto.</i>
                </td>
                <td>
                    <span style="font-weight: bold; color: #FF0000">***</span>                     
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td>
                </td>
            </tr>
            <%--MEDIO AMBIENTE--%>
            <tr>
                <td class="style1">
                    <b>Interrelación entre el Medio Ambiente y el Ambiente de Trabajo.</b>
                    <br />
                    <i><u>Disertantes:</u></i><br />
                </td>
                <td>
                </td>
            </tr>
            <tr  style="height:25px;">
                <td class="style1">
                    <i>Ing. Santiago Reina del Ministerio de Agua, Ambiente y Servicios Públicos.</i>
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink7" NavigateUrl="~/Images/Blog/PPTMEDIOAMBIENTEREYNA.pptx" CssClass="prensa" runat="server">Descargar Presentación</asp:HyperLink>                                       
                </td>
            </tr>
            <tr  style="height:25px;">
                <td class="style1">
                    <i>Dra. Marcela Miravet, Secretaria de Prevención y Promoción de la Salud del Ministerio de Salud.</i>
                </td>
                <td>
                    <asp:HyperLink ID="HyperLink8" NavigateUrl="~/Images/Blog/PPTMEDIOAMBIENTEMIRAVET.pptx" CssClass="prensa" runat="server">Descargar Presentación</asp:HyperLink>                                       
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                </td>
                <td>
                </td>
            </tr>
            <%--PRIMEROS AUXILIOS--%>
            <tr>
                <td class="style1">
                    <b>Primeros Auxilios: Entrenamiento para una comunidad organizada.</b>
                    <br />
                    <i><u>Disertantes:</u></i><br />
                </td>
                <td>
                </td>
            </tr>
            <tr  style="height:25px;">
                <td class="style1">
                    <i>Enf. Prof. Darío Polanco de la Fundación GEER</i>
                </td>
                <td>
                    <span style="font-weight: bold; color: #FF0000">***</span>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="color: #FF0000">
                        <b>*** </b>
                        Debido al tamaño, estas presentaciones se pueden obtener en Rivadavia 646, en las oficinas de CYMAT 
                        del Ministerio de Trabajo de Córdoba. Concurrir munido de un PENDRIVE.
                    </span>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />

</article>
</div>
<div>

</div>

<style type="text/css">           
    .article img {
        max-width: 100%;
        border-radius: 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px;
        border: 1px solid #bbb;
}
    .style1
    {
        width: 450px;
    }
</style>

</asp:Content>

