<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Formularios/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="LaLombriz.Formularios.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--Estilos-->
    <link href="../Estilos/MenuStyles.css" rel="stylesheet" />
        <div runat="server" id="optionsContainer" class="containerOptions row">
            <div id="cakeBefore" class="btnOptions col-xs-12 col-md-6">
                <span class="square">
                <asp:LinkButton runat="server" ID="btnCake" class="before after" Text="Pasteles">
                </asp:LinkButton>
                </span>
            </div>
            <div id="burgerBefore" class="btnOptions col-xs-12 col-md-6">
                <asp:LinkButton runat="server" ID="btnBurger" Text="Macarons"></asp:LinkButton>
            </div>
            <div id="packBefore" class="btnOptions col-xs-12 col-md-6">
                <asp:LinkButton runat="server" ID="btnPack" Text="Mesa de dulces"></asp:LinkButton>
            </div>
            <div id="otherBefore" class="btnOptions col-xs-12 col-md-6">
                <asp:LinkButton runat="server" ID="btnOther" Text="Otros"></asp:LinkButton>
            </div>
        </div>
        <div runat="server" id="productsContainer">
            <p>Productos de la seleccion</p>
        </div>
</asp:Content>
