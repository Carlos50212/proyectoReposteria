<%@ Page Title="Mas vendidos" Language="C#" MasterPageFile="~/Formularios/Administrador/VentasMaster.master" AutoEventWireup="true" CodeBehind="MasVendidos.aspx.cs" Inherits="LaLombriz.Formularios.Administrador.MasVendidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PedidosOptions" runat="server">
    <link href="../../Estilos/MasVendidos.css" rel="stylesheet" />

    <div id="sellPerDay-container">
        <div id="date-container">
            <h5>Productos mas vendidos al dia de hoy <b id="day" class="text-muted">{day}</b></h5>
        </div>
        <div id="grid-container">
            <div id="gridSellsPerDay"></div>
        </div>
    </div>
    <div id="sellPerMonth-container">
        <div id="month-container">
            <h5>Productos mas vendidos en el mes de <b id="month" class="text-muted">{month}</b></h5>
        </div>
        <div id="gridMonth-container">
            <div id="gridSellsPerMonth"></div>
        </div>
    </div>
    <script src="../../JS/MasVendidos.js"></script>
</asp:Content>
