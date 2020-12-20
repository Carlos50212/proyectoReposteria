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
                <div  id="selectionContainer">
                    <h1>¡Selecciona tus productos!</h1>
                    <div id="cart-container">
                        <asp:LinkButton runat="server" ID="btnSeeCarOption" CssClass="linkCart">
                            <img src="../Recursos/cart.png" alt="" width="45" height="40" class="d-inline-block align-top">
                            (0)
                        </asp:LinkButton>
                    </div>
                    <div  runat="server" id="tableCake" class="row">
                        <div id="smallSize-container" class="col-xs-12 col-md-6">
                            <h1 style="font-style: oblique">Chico</h1>
                            <asp:GridView runat="server" ID="tbtSmallSizeG" AutoGenerateColumns="false" class="table table-dark table-striped" OnRowCommand="selecRowCommand">
                                <Columns>
                                    <asp:BoundField DataField="producto" HeaderText="Producto" />
                                    <asp:BoundField DataField="precio" HeaderText="Precio" />
                                    <asp:TemplateField HeaderText="Descripción">
                                        <ItemTemplate>
                                            <asp:Button  runat="server" ID="btnModalButton" class='btn btn-link' CommandArgument="<%#Container.DataItemIndex%>" CommandName="seeSmall" Text="Ver"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opción">
                                        <ItemTemplate>
                                            <asp:Button  runat="server" ID="btnModalAdd" data-bs-toggle='modal' class='btn btn-link' data-bs-target='#showDescription' CommandName="addSmallCake" Text="Agregar" OnClientClick="return false;"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div id="mediumSize-container" class="col-xs-12 col-md-6">
                            <h1 style="font-style: oblique">Mediano</h1>
                            <asp:GridView runat="server" ID="tbtMediumSizeP" AutoGenerateColumns="false" class="table table-dark table-striped" OnRowCommand="selecRowCommand">
                                <Columns>
                                    <asp:BoundField DataField="producto" HeaderText="Producto" />
                                    <asp:BoundField DataField="precio" HeaderText="Precio" />
                                    <asp:TemplateField HeaderText="Descripción">
                                        <ItemTemplate>
                                            <asp:Button  runat="server" ID="btnModal"  class='btn btn-link' CommandArgument="<%#Container.DataItemIndex%>" CommandName="seeMediumCake" Text="Ver" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opción">
                                        <ItemTemplate>
                                            <asp:Button  runat="server" ID="btnModalAdd" data-bs-toggle='modal' class='btn btn-link' data-bs-target='#showDescription' CommandName="addMediumCake" Text="Agregar" OnClientClick="return false;"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div id="bigSize-container" class="col-xs-12 col-md-12">
                            <h1 style="font-style: oblique">Grande</h1>
                            <asp:GridView runat="server" ID="tbtBigSizeP" AutoGenerateColumns="false" class="table table-dark table-striped" OnRowCommand="selecRowCommand">
                                <Columns>
                                    <asp:BoundField DataField="producto" HeaderText="Producto" />
                                    <asp:BoundField DataField="precio" HeaderText="Precio" />
                                    <asp:TemplateField HeaderText="Descripción">
                                        <ItemTemplate>
                                            <asp:Button  runat="server" ID="btnModal"  class='btn btn-link'  CommandArgument="<%#Container.DataItemIndex%>" CommandName="seeBigCake" Text="Ver"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opción">
                                        <ItemTemplate>
                                            <asp:Button  runat="server" ID="btnModalAdd" data-bs-toggle='modal' class='btn btn-link' data-bs-target='#showDescription' CommandName="addBigCake" Text="Agregar" OnClientClick="return false;"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <div class="modal fade" id="showDescription" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="singInTitle"><asp:Label runat="server" ID="lblHeader"></asp:Label></h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <label>Descripción</label>
                        <asp:Label runat="server" ID="lblDescription"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Entendido</button>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
