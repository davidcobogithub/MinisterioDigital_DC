<%@ Page Language="VB" MasterPageFile="~/MasterPage1.master" AutoEventWireup="false" CodeFile="frmNotificaciones.aspx.vb" Inherits="frmNotificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="height: 339px; width: 562px">

        <table class="style1">

            <tr>
                <td class="style2">

                    <label for="inputEmail3" class="col-form-label">ID Busq Notificacion</label></td>
                <td>
                    <asp:TextBox ID="TextBoxBusNot" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="style2">

                    <label for="inputEmail3" class="col-form-label">ID Busq Notificacion_modelo</label></td>
                <td>
                    <asp:TextBox ID="TextBoxBusModelo" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="style2">

                    <label for="inputEmail3" class="col-form-label">ID Busq Notificacion_tipos</label></td>
                <td>
                    <asp:TextBox ID="TextBoxBusTipos" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>


            <tr>
                <td class="style2">

                    <label for="inputEmail3" class="col-form-label">Id Notificación</label></td>
                <td>
                    <asp:TextBox ID="TextBoxIdNot" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <label for="inputPassword3" class="col-form-label">Id Orden</label></td>
                <td>
                    <asp:TextBox ID="TextBoxIdOrd" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="style2">
                    <label for="inputEmail3" class="col-form-label">Nro Cuenta</label></td>
                <td>
                    <asp:TextBox ID="TextBoxNCuenta" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <label for="inputPassword3" class="col-form-label">ENVIADO</label></td>
                <td>
                    <asp:TextBox ID="TextBoxEnv" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <label for="inputEmail3" class="col-form-label">FECHA</label></td>
                <td>
                    <asp:TextBox ID="TextBoxfecha" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <label for="inputPassword3" class="col-form-label">FECHA_LECTURA</label></td>
                <td>
                    <asp:TextBox ID="TextBoxfechaLec" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="style2">
                    <label for="inputPassword3" class="col-form-label">ID ID_BARANDILLADOCUMENTOPDF</label></td>
                <td>
                    <asp:TextBox ID="TextBoxBarandi" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <label for="inputEmail3" class="col-form-label">ID_AREAS</label></td>
                <td>
                    <asp:TextBox ID="TextBoxIdAr" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <label for="inputEmail3" class="col-form-label">ID_MODELO</label></td>
                <td>
                    <asp:TextBox ID="TextBoxIdModel" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="style2">
                    <label for="inputPassword3" class="col-form-label">ID_USUARIO</label></td>
                <td>
                    <asp:TextBox ID="TextBoxIdUsu" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="style2">

                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Agregar" ID="butAgre" />
                    <asp:Button runat="server" CssClass="btn btn-info" Text="Buscar" ID="butBusca" />
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Modificar" ID="butMod" />
                    <asp:Button runat="server" CssClass="btn btn-success" Text="Limpiar" ID="butLim" />
                </td>

            </tr>

        </table>

    </div>



    

</asp:Content>
