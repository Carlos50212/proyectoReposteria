<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Formularios/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="LaLombriz.Formularios.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="allContainer">
        <div id="second-container">
            <input type="hidden" name="hiddenIdProduct" id="hiddenIdProduct" value="" />
            <input type="hidden" name="hiddenSizeProduct" id="hiddenSizeProduct" value="" />
            <input type="hidden" name="hiddenQuantityProduct" id="hiddenQuantityProduct" value="1" />
            <input type="hidden" name="hiddenIdAddOrder" id="hiddenIdAddOrder" value="" />
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
                    <asp:LinkButton runat="server" ID="btnPack" Text="Paquetes" OnClick="btnPackOnClick"></asp:LinkButton>
                </div>
                <div id="otherBefore" class="btnOptions col-xs-12 col-md-6">
                    <asp:LinkButton runat="server" ID="btnOther" Text="Otros" OnClick="btnOtherOnClick"></asp:LinkButton>
                </div>
            </div>
            <div runat="server" id="productsContainer">
                <div id="selectionContainer">
                    <div runat="server" id="notOrders" class="infoContainer" style="display: none;">
                        <div id="notProducts-container">
                            <h2>¡Lo sentimos!. No hay productos en existencia</h2>
                            <img id="imgNotPrudct" src="../Recursos/NotOrder.png" alt="Not products" class="imgInfo" />
                            <p>Favor de regresar más tarde</p>
                        </div>
                    </div>
                    <div id="ordersContainer" runat="server">
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
                            <div class='cartProductsContainer'>
                                <div class="inputDate">
                                    <p>Seleccione la fecha de entrega</p>
                                    <div class='input-group date' id='datetimepicker1'>
                                        <input id="calendario" />
                                        <input type='text' name='ValorCalendario' id="ValorCalendario" class="form-control" style="display:none" />
                                    </div>
                                </div>
                                <asp:Literal runat="server" ID="tbProductsCart"></asp:Literal>
                            </div>
                            <div class="btnReturnClass">
                                <asp:Button runat="server" ID="btnReturnMenu" CssClass="btn btn-secondary" Text="Regresar" OnClick="btnReturnMenuOnlick" />
                                <asp:Button runat="server" ID="btnCreateProduct" Style="display: none;" />
                                <button type="button" class="btn btn-primary" onclick="Comprar();">Siguiente </button>
                                <asp:Button runat="server" ID="btnPasarDatos" Style="display: none" OnClick="btnPasarDatosOnClick" />
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
    </div>

    <script>
        $(document).ready(function () {
            console.log("ENTRO");
            $("#calendario").kendoDatePicker({
                format: "yyyy-MM-dd"
            });
        });
        function getID(comp) {
            /*Traemos los valores de los componentes*/
            var log = comp.id
            var log2 = log.split('_').join(' ');
            /*RadioButton seleccionado */
            var sizeSelect = $('input:radio[name=' + log + ']:checked').val();
            var quantity = $('#' + log + 'Quantity').val();
            console.log("Producto seleccionado: " + log2);
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
        /*Metodo para guardar variable de agregar pedido*/
        function Comprar() {

            Swal.fire({
                title: 'Confirmar Compra',
                text: "¿Estás seguro de realizar el pedido?, recuerda que en pedidos cuya fecha de entrega es menor a 10 días no existe posibilidad de cancelar el pedido.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                cancelButtonText: 'Cancelar',
                confirmButtonText: 'Confirmar Pedido'
            }).then((result) => {
                if (result.isConfirmed) {
                    checkDate();
                }
            })
        }
        function checkDate() {
            var date = document.getElementById('calendario').value;
            var dateFormat = /^\d{4}-\d{2}-\d{2}$/;
            if (date != "") {
                if (!date.match(dateFormat)) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Favor de ingresar un formato aceptable de fecha'
                    });
                } else {
                    const today = new Date();
                    const todayFormat = today.toISOString();
                    const arrayDate = todayFormat.split("T");
                    const todayDate = arrayDate[0];

                    if (date < todayDate) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'Favor de ingresar una fecha superior a la fecha actual'
                        });
                    } else {
                        document.getElementById('ValorCalendario').value = date;
                        document.getElementById('hiddenIdAddOrder').value = "1";
                        document.getElementById('<%=btnPasarDatos.ClientID%>').click();
                    }

                }
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Favor de ingresar la fecha de entrega'
                });
            }
        }
    </script>

    <script src="../JS/Menu.js"></script>
</asp:Content>
