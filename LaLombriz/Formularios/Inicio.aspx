<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Formularios/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="LaLombriz.Formularios.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Estilos/InicioStyles.css" rel="stylesheet" />
    <div id="menuContainer" class="containerOptions row">
        <div id="welcomeText" class="col-xs-12 col-md-12">
            <h1>Bienvenido</h1>
            <p>Empresa 100% mexicana, dedicada a la elaboración y venta de</p>
        </div>
        <div id="btnMenu" class="col-xs-12 col-md-6 btnOptions">
            <asp:LinkButton runat="server" ID="btnMenuOption" Text="Menú" OnClick="btnMenuOnClick"></asp:LinkButton>
        </div>
        <div id="btnPedidos" class="col-xs-12 col-md-6 btnOptions">
            <asp:LinkButton runat="server" ID="btnPedidosOption" Text="Pedidos" OnClick="btnPedidosOnClick"></asp:LinkButton>
        </div>
        <div id="btnCotizacion" class="col-xs-12 col-md-6 btnOptions">
            <asp:LinkButton runat="server" ID="btnCotizacionOption" Text="Cotización" OnClick="btnCotizacionOnClick"></asp:LinkButton>
        </div>
        <div id="btnGaleria" class="col-xs-12 col-md-6 btnOptions">
            <asp:LinkButton runat="server" ID="btnGaleriaOption" Text="Galería" OnClick="btnGaleriaOnClick"></asp:LinkButton>
        </div>
    </div>
</asp:Content>
