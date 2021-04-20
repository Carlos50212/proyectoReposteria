<%@ Page Title="Ventas diarias" Language="C#" MasterPageFile="~/Formularios/Administrador/VentasMaster.master" AutoEventWireup="true" CodeBehind="VentasDia.aspx.cs" Inherits="LaLombriz.Formularios.Administrador.VentasDia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PedidosOptions" runat="server">
    <div id="sellsPerDay-container">
        <div id="date-container">
            <h5>Día: <b id="day" class="text-muted">{day}</b></h5>
        </div>
        <div id="gridDay-container">
            <div id="gridSellsPerDay"></div>
        </div>
    </div>
    <script src="../../JS/VentasDia.js"></script>
</asp:Content>
