<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Nuevo.aspx.cs" Inherits="Facturacion.Nuevo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p style="font-size: 22px">
    &nbsp;</p>
<p style="font-size: 22px">
    <strong>Registro de Facturas y Boletas </strong>
</p>
<table class="nav-justified">
    <tr>
        <td style="width: 149px; font-size: 14px; text-align: left;"><b><span style="font-weight: normal">Numero </span></b></td>
        <td>
            <asp:TextBox ID="txtNumero" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 149px; font-size: 14px; text-align: left;"><b><span style="font-weight: normal">Fecha</span></b></td>
        <td>
            <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 149px; font-size: 14px; text-align: left;"><b><span style="font-weight: normal">Cliente </span></b></td>
        <td>
            <asp:DropDownList ID="cboCliente" runat="server" DataTextField="Razon_social" DataValueField="Cliente_id" Height="28px" Width="450px" AutoPostBack="True" OnSelectedIndexChanged="cboCliente_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 149px; font-size: 14px; height: 22px; text-align: left;"><b><span style="font-weight: normal">Direccion</span></b></td>
        <td style="height: 22px">
            <asp:Label ID="lblDireccion" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 149px; font-size: 14px; text-align: left;"><b><span style="font-weight: normal">RUC</span></b></td>
        <td>
            <asp:Label ID="lblRuc" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 149px; font-size: 14px; height: 20px; text-align: left;"><b><span style="font-weight: normal">Moneda</span></b></td>
        <td style="height: 20px">
            <asp:DropDownList ID="cboMoneda" runat="server" DataTextField="Descripcion" DataValueField="Moneda_id" Height="28px" Width="150px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 149px; font-size: 14px; text-align: left;"><b><span style="font-weight: normal">Producto</span></b></td>
        <td>
            <asp:DropDownList ID="cboProducto" runat="server" AutoPostBack="True" DataTextField ="Descripcion" DataValueField="Producto_id" Height="28px" Width="400px" OnSelectedIndexChanged="cboProducto_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 149px; font-size: 14px" class="text-left">Precio</td>
        <td>
            <asp:Label ID="lblPrecio" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 149px; font-size: 14px; text-align: left;"><b><span style="font-weight: normal">Cantidad</span></b></td>
        <td>
            <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
        &nbsp;<asp:Button ID="btnAdicionar" runat="server" OnClick="btnAdicionar_Click" Text="Adicionar" CssClass="btn btn-primary" Height="35px" Width="80px" />
&nbsp;<asp:Button ID="btnRemover" runat="server" OnClick="btnRemover_Click" Text="Remover" CssClass="btn btn-danger" Height="35px" Width="80px" />
        </td>
    </tr>
    <tr>
        <td style="width: 149px; height: 20px; font-size: 14px;" class="text-left">Detalle</td>
        <td style="height: 20px">
            <asp:PlaceHolder ID="phItems" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
    <tr>
        <td style="width: 149px; font-size: 14px; text-align: left;"><b><span style="font-weight: normal">Total Valor de Venta</span></b></td>
        <td>
            <asp:Label ID="lblTotalValorVenta" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 149px; font-size: 14px; height: 20px; text-align: left;"><b><span style="font-weight: normal">Total Impuestos</span></b></td>
        <td style="height: 20px">
            <asp:Label ID="lblTotalImpuestos" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 149px; font-size: 14px; text-align: left;"><b><span style="font-weight: normal">Total </span></b></td>
        <td>
            <asp:Label ID="lblTotalFactura" runat="server" style="font-weight: 700"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 149px; font-size: 14px" class="text-left">&nbsp;</td>
        <td>
            <asp:Button ID="btnGrabar" runat="server" OnClick="btnGrabar_Click" Text="Grabar" CssClass="btn btn-success" Height="35px" Width="70px" />
&nbsp;<asp:Button ID="btnSalir" runat="server" OnClick="btnSalir_Click" Text="Salir" Width="70px" CssClass="btn btn-warning" Height="35px" />
        </td>
    </tr>
</table>
<p style="font-size: xx-large">
    &nbsp;</p>
</asp:Content>
