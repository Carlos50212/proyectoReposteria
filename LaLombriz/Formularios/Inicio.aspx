<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Formularios/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="LaLombriz.Formularios.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="menuContainer" class="containerOptions row">
        <div id="welcomeText" class="col-xs-12 col-md-12">
            <h1>Bienvenido</h1>
            <p>Empresa 100% mexicana, dedicada a la elaboración y venta de postres.</p>
        </div>
        <div id="carousel-container"">
            <div id="carouselPromo" class="carousel slide carouselStyles" data-bs-ride="carousel">
                <div class="carousel-inner carouselStyles">
                    <div class="carousel-item active carouselStyles">
                        <img src="../Recursos/Promociones/promo1.png" class="d-block w-100 carouselStyles" alt="promocion">
                    </div>
                    <div class="carousel-item carouselStyles">
                        <img src="../Recursos/Promociones/promo2.png" class="d-block w-100 carouselStyles" alt="promocion">
                    </div>
                    <div class="carousel-item carouselStyles">
                        <img src="../Recursos/Promociones/promo3.png" class="d-block w-100 carouselStyles" alt="promocion">
                    </div>
                </div>
                <button class="carousel-control-prev btnCarouselStyles" type="button" data-bs-target="#carouselPromo" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Previous</span>
                </button>
                <button class="carousel-control-next btnCarouselStyles" type="button" data-bs-target="#carouselPromo" data-bs-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Next</span>
                </button>
            </div>
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
    <script src="../JS/Index.js"></script>
</asp:Content>