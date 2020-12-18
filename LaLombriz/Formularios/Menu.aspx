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
                    <h1>¡Selecciona tus productos!</h1>
                    <div id="buttons-container">
                        <div id="backOption">
                            <asp:Button runat="server" ID="btnBack" class="btn btn-primary" Text="Regresar" />
                        </div>
                        <div id="addOption">
                            <asp:Button runat="server" ID="btnCarrito" class="btn btn-primary" Text="Carrito  " />
                        </div>
                    </div>
                    <div id="table-container">
                        <Table ID="tbtProduct" class="table">
                            <tr>
                                <th>Productos</th>
                                <th>Tamaño</th>
                                <th>Descripción</th>
                                <th>Precio</th>
                            </tr>
                            <tr>
                                <th>
                                    <asp:DropDownList runat="server" ID="ddlProducts" class="btn btn-secondary dropdown-toggle"></asp:DropDownList>
                                </th>
                                <th>
                                    <asp:DropDownList runat="server" ID="ddlSize" class="btn btn-secondary dropdown-toggle"></asp:DropDownList>
                                </th>
                                <th>
                                    <asp:Button runat="server" ID="btnDescription" Text="Ver" class="btn btn-link" />
                                </th>
                                <th>
                                    <asp:Label runat="server" ID="lblCost">$0.0</asp:Label>
                                </th>
                                <th style="border:0;">
                                    <asp:Button runat="server" ID="btnAdd" Text="Agregar" CssClass="btn btn-light" />
                                </th>
                            </tr>
                        </Table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
