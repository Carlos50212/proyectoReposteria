<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Formularios/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="LaLombriz.Formularios.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--Estilos-->
    <link href="../Estilos/MenuStyles.css" rel="stylesheet" />
    <div id="allContainer">
        <div id="second-container">
            <div runat="server" id="optionsContainer" class="containerOptions row">
                <div id="cakeBefore" class="btnOptions col-xs-12 col-md-6">
                    <asp:LinkButton runat="server" ID="btnCake" Text="Pasteles" OnClick="btnCakeOnClick">
                    </asp:LinkButton>
                </div>
                <div id="burgerBefore" class="btnOptions col-xs-12 col-md-6">
                    <asp:LinkButton runat="server" ID="btnBurger" Text="Macarons" OnClick="btnBurgerOnClick"></asp:LinkButton>
                </div>
                <div id="packBefore" class="btnOptions col-xs-12 col-md-6">
                    <asp:LinkButton runat="server" ID="btnPack" Text="Mesa de dulces" OnClick="btnPackOnClick"></asp:LinkButton>
                </div>
                <div id="otherBefore" class="btnOptions col-xs-12 col-md-6">
                    <asp:LinkButton runat="server" ID="btnOther" Text="Otros" OnClick="btnOtherOnClick"></asp:LinkButton>
                </div>
            </div>
            <div runat="server" id="productsContainer">
                <div id="selection-container">
                    <asp:Table runat="server" ID="tbtProduct">
                        <asp:TableRow>
                            <asp:TableCell>Productos</asp:TableCell>
                            <asp:TableCell>Tamaño</asp:TableCell>
                            <asp:TableCell>Descripción</asp:TableCell>
                            <asp:TableCell>Precio</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlProducts"></asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlSize"></asp:DropDownList> 
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button runat="server" ID="btnDescription" Text="Descripción"/>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblCost">HOLA</asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
