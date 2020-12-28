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
                    <div id="products" class="row">
                        <asp:Literal ID="ltProduct" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <div class="modal fade" id="selectOptions" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Label runat="server" ID="lblNameProduct" Text="Título" style="font-weight:bold; font-size:large;"></asp:Label>
                        <input type="hidden" name="hidden" id="hidden" value="" />
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="form-floating mb-3">
                            <asp:TextBox runat="server" ID="txtTamanio" class="form-control" placeholder="Tamaño" ></asp:TextBox>
                            <label for="txtTamanio">Tamaño</label>
                        </div>
                        <div class="form-floating mb-3">
                            <asp:TextBox runat="server" ID="txtCantidad" class="form-control" placeholder="1" ></asp:TextBox>
                            <label for="txtCantidad">Cantidad</label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                        <asp:Button runat="server" ID="btnAddCart" Text="Agregar" class='btn btn-primary' OnClick="btnAddOnClick"/>
                    </div>
                </div>
            </div>
        </div>
    <!--Funcion para mostrar modal y obtener producto que se seleccionó-->
    <script>
        function getID(comp) {
            var log = comp.id
            var log2 = log.split('_').join(' ');
            document.getElementById('hidden').value = log2;
            $('#<%=lblNameProduct.ClientID%>').text(log2);
            $('#selectOptions').modal('show');
        }
    </script>
</asp:Content>
