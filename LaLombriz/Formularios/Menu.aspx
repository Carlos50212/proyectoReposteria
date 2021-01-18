<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Formularios/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="LaLombriz.Formularios.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--Estilos-->
    <link href="../Estilos/MenuStyles.css" rel="stylesheet" />
    <div id="allContainer">
        <div id="second-container">
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
                        <asp:LinkButton runat="server" ID="btnSeeCarOption" CssClass="linkCart">
                            <img src="../Recursos/cart.png" alt="" width="45" height="40" class="d-inline-block align-top">
                            <asp:Label runat="server" ID="lblConteoCarro" Text=""></asp:Label>
                        </asp:LinkButton>
                    </div>
                    <div id="products" class="row">
                        <asp:Literal ID="ltProduct" runat="server"></asp:Literal>
                        <asp:Literal ID="ltProductSpecial" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="selectOptions" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label runat="server" ID="lblNameProduct" Text="Título" Style="font-weight: bold; font-size: large;"></asp:Label>
                    <!--Quitar hidden-->
                    <input type="hidden" name="hidden" id="hidden" value="" />
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="form-floating mb-3">
                        <asp:DropDownList runat="server" ID="ddlTamanio" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" ID="txtCantidad" class="form-control" placeholder="1"></asp:TextBox>
                        <label for="txtCantidad">Cantidad</label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button runat="server" ID="btnAddCart" Text="Agregar" class='btn btn-primary' OnClick="btnAddOnClick" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="selectOptionsPack" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label runat="server" ID="lblPackTitle" Text="Título" Style="font-weight: bold; font-size: large;"></asp:Label>
                    <!--Quitar hidden-->
                    <input type="hidden" name="hiddenPack" id="hiddenPack" value="" />
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" ID="TextBox2" class="form-control" placeholder="1"></asp:TextBox>
                        <label for="txtCantidad">Cantidad</label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button runat="server" ID="Button1" Text="Agregar" class='btn btn-primary' OnClick="btnAddOnClick" />
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
            /*Función ajax para mandar los datos obtenidos del lado del cliente, al lado del servidor */
            $.ajax({
                type: "POST",                                              // tipo de request que estamos generando
                url: 'Menu.aspx/getInformationSelected',                    // URL al que vamos a hacer el pedido
                data: "{nameProduct: '" + log2 + "', sizeSelected: '" + sizeSelect + "', quantity: '" + quantity + "'}", // data es un arreglo JSON que contiene los parámetros que van a ser recibidos por la función del servidor
                contentType: "application/json; charset=utf-8",            // tipo de contenido
                dataType: "json",                                          // formato de transmición de datos
                async: true,                                               // si es asincrónico o no
                success: function (resultado) {                            // función que va a ejecutar si el pedido fue exitoso
                    console.log(resultado);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) { // función que va a ejecutar si hubo algún tipo de error en el pedido
                    var error = eval("(" + XMLHttpRequest.responseText + ")");
                    alert(error.Message);
                }
            });

            //Traer los datos de pasteles, macarons y otros
            document.getElementById('hidden').value = log2;
            $('#<%=lblNameProduct.ClientID%>').text(log2);
            $('#selectOptions').modal('show');
        }
        function getIDPack(comp) {
            var idBtn = comp.id
            var idBtnT = idBtn.split('_').join(' ');
            var quantity = $('#' + idBtn + 'Quantity').val();
            console.log("Producto seleccionado: " + idBtnT);
            console.log("Cantidad solicitada: " + quantity);

            /*Función ajax para mandar los datos obtenidos del lado del cliente, al lado del servidor */
            $.ajax({
                type: "POST",                                              // tipo de request que estamos generando
                url: 'Menu.aspx/getInformationSelected',                    // URL al que vamos a hacer el pedido
                data: "{nameProduct: '" + idBtnT + "', sizeSelected: '', quantity: '" + quantity + "'}", // data es un arreglo JSON que contiene los parámetros que van a ser recibidos por la función del servidor
                contentType: "application/json; charset=utf-8",            // tipo de contenido
                dataType: "json",                                          // formato de transmición de datos
                async: true,                                               // si es asincrónico o no
                success: function (resultado) {                            // función que va a ejecutar si el pedido fue exitoso
                    console.log(resultado);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) { // función que va a ejecutar si hubo algún tipo de error en el pedido
                    var error = eval("(" + XMLHttpRequest.responseText + ")");
                    alert(error.Message);
                }
            });

            //Trae los datos de las mesas de dulces
            document.getElementById('hiddenPack').value = idBtnT;
            $('#<%=lblPackTitle.ClientID%>').text(idBtnT);
            $('#selectOptionsPack').modal('show');
        }
    </script>
</asp:Content>
