<%@ Page Language="VB" MasterPageFile="~/MasterPage3.master" AutoEventWireup="false" CodeFile="Prueba_DC_DB.aspx.vb" Inherits="Prueba_DC_DB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
        <div style="height: 339px; width: 562px">

            <table class="style1">
                <tr>
                    <td class="style2">Nombre</td>
                    <td>
                        <asp:TextBox ID="txtNom" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td class="style2">Apellido</td>
                    <td>
                        <asp:TextBox ID="txtApe" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="style2">&nbsp;</td>
                    <td>
                        <asp:Button ID="Button3" runat="server" Text="Insertar" Width="142px" CssClass="btn btn-primary" />
                    </td>
                </tr>

            </table>

        </div>

</asp:Content>
