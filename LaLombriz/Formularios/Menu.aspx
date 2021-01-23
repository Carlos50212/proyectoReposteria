<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Formularios/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="LaLombriz.Formularios.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="allContainer">
        <div id="second-container">
            <input type="hidden" name="hiddenIdProduct" id="hiddenIdProduct" value="" />
            <input type="hidden" name="hiddenSizeProduct" id="hiddenSizeProduct" value="" />
            <input type="hidden" name="hiddenQuantityProduct" id="hiddenQuantityProduct" value="1" />
            <asp:Button runat="server" ID="btnAddOrder" OnClick="btnAddOnClick" style="display:none;"/>
            <asp:Button runat="server" ID="btnDeleteProduct" OnClick="btnDeleteOnClick" style="display:none;"/>
            <div runat="server" id="optionsContainer" class="containerOptionsMenu row">
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
                        <asp:LinkButton runat="server" ID="btnSeeCarOption" CssClass="linkCart" OnClick="btnSeeCarOptionOnClick">
                            <img src="../Recursos/cart.png" alt="" width="45" height="40" class="d-inline-block align-top">
                            <asp:Label runat="server" ID="lblConteoCarro" Text=""></asp:Label>
                        </asp:LinkButton>
                    </div>
                    <div runat="server" id="products" class="row">
                        <asp:Literal ID="ltProduct" runat="server"></asp:Literal>
                        <asp:Literal ID="ltProductSpecial" runat="server"></asp:Literal>
                        
                    </div>
                    <div runat="server" id="detailCart" class="detailCartStyle" style="display: none;">
                       <div class="input-group date" data-provide="datepicker" style="width:23%; padding-left:60px;">
                            <input id="calendario" type="text" class="form-control" runat="server">
                            <div class="input-group-addon">
                                <span class="glyphicon glyphicon-th"></span>
                            </div>
                        </div>
                        <div class='cartProductsContainer'>
                            <asp:Literal runat="server" ID="tbProductsCart"></asp:Literal>
                        </div>
                        <div class="btnReturnClass">
                            <asp:Button runat="server" ID="btnReturnMenu" CssClass="btn btn-secondary" Text="Regresar" OnClick="btnReturnMenuOnlick"/>
                            <asp:Button runat="server" ID="btnCreateProduct" CssClass="btn btn-primary" Text="Comprar" OnClick="btnComprar"/>
                        </div>
                    </div>
                    <div runat="server" id="notProductsCart" class="infoContainer" style="display: none;">
                        <div id="containerNotNewOrders">
                            <h2>No tienes productos</h2>
                            <img id="imgNotOrder" src="../Recursos/NotOrder.png" alt="Not orders" class="imgInfo" />
                            <p>¡Que esperas! Selecciona nuevos productos <a id="goMenu" style="text-decoration: none" href="Menu.aspx">aquí</a></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Funcion para mostrar modal y obtener producto que se seleccionó-->
    <script>
        function getID(comp) {
            /*Traemos los valores de los componentes*/
            var log = comp.id
            var log2 = log.split('_').join(' ');
            /*RadioButton seleccionado */
            var sizeSelect = $('input:radio[name=' + log + ']:checked').val();
            var quantity = $('#'+log +'Quantity').val();
            console.log("Producto seleccionado: "+log2);
            console.log("Tamaño seleccionado: " + sizeSelect);
            console.log("Cantidad solicitada: " + quantity);

            //Traer los datos de pasteles, macarons y otros
            document.getElementById('hiddenIdProduct').value = log2;
            document.getElementById('hiddenSizeProduct').value = sizeSelect;
            document.getElementById('hiddenQuantityProduct').value = quantity;
            document.getElementById('<%=btnAddOrder.ClientID %>').click();
        }
        function deleteProduct(comp) {
            var idProduct = comp.id;
            console.log("Producto eliminado: " + idProduct);
            document.getElementById('hiddenIdProduct').value = idProduct;
            document.getElementById('<%=btnDeleteProduct.ClientID %>').click();
        }

    </script>
</asp:Content>
