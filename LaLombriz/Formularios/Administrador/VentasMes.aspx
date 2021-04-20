<%@ Page Title="Ventas mensuales" Language="C#" MasterPageFile="~/Formularios/Administrador/VentasMaster.master" AutoEventWireup="true" CodeBehind="VentasMes.aspx.cs" Inherits="LaLombriz.Formularios.Administrador.VentasMes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PedidosOptions" runat="server">
    <div id="sellPerMonth-container">
        <div id="date-container">
            <h5>Mes: <b id="month" class="text-muted">{month}</b></h5>
        </div>
        <div id="grid-container">
            <div id="gridSellsPerMonth"></div>
        </div>
    </div>
    <script src="../../JS/VentasMes.js"></script>
</asp:Content>
