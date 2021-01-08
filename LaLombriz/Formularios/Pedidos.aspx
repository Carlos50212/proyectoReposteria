<%@ Page Title="Pedidos" Language="C#" MasterPageFile="~/Formularios/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="LaLombriz.Formularios.Pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Estilos/PedidosStyles.css" rel="stylesheet" />
    <div id="orders-container">
        <div id="orders-secondContainer">
            <input type="hidden" name="hiddenIdDetailsOrder" id="hiddenIdDetailsOrder" value="" />
            <asp:Button runat="server" ID="btnDetailsOrder" OnClick="seeDetailsOnClick" style="display:none;"/>
            <input type="hidden" name="hiddenIdDeleteOrder" id="hiddenIdDeleteOrder" value="" />
            <asp:Button runat="server" ID="btnDeleteOrder" OnClick="deleteOrderOnClick" style="display:none;"/>
            <input type="hidden" name="hiddenIdDetailOldOrder" id="hiddenIdDetailOldOrder" value="" />
            <asp:Button runat="server" ID="btnDetailOldOrder" OnClick="seeDetailsOldOrderOnClick" style="display:none;"/>
            <asp:Button runat="server" ID="btnDownloadPDF" OnClick="downloadPDFOnClick" style="display:none;"/>
            <div id="title-container">
                <h1>¡Visualiza tus pedidos!</h1>
            </div>
            <div id="optionsOrders-container">
                <ul id="allOptionsList">
                    <li id="firstOption" class="listOptions">
                        <asp:LinkButton runat="server" ID="lkNew" CssClass="lkStyles" OnClick="lkNewOrdersOnClick" >Por entregar</asp:LinkButton>
                    </li>
                    <li id="secondOption" class="listOptions">
                        <asp:LinkButton runat="server" ID="lkOld" CssClass="lkStyles" OnClick="lkOldOrdersOnClick">Entregados</asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div runat="server" id="newOrders" style="display:none;">
                <div class='orders-containers'>
                    <asp:Literal runat="server" ID="tbNewOrders"></asp:Literal>
                </div>
            </div>
            <div runat="server" id="detailOrder" style="display:none;">
                <div style="width:100%" >
                    <asp:Literal runat="server" ID="tbOrderDetails"></asp:Literal>
                </div>
            </div>
            <div runat="server" id="oldOrders" style="display:none;">
                <div class='orders-containers'>
                    <asp:Literal runat="server" ID="tbOldOrders"></asp:Literal>
                </div>
            </div>
            <div runat="server" id="notNewOrders" class="infoContainer" style="display:none;">
                <div id="containerNotNewOrders">
                    <h2>No tienes pedidos</h2>
                    <img id="imgNotOrder" src="../Recursos/NotOrder.png" alt="Not orders" class="imgInfo" />
                    <p>¡Que esperas! Estás a un <a id="goMenu" style="text-decoration: none" href="Menu.aspx">click</a> de disfrutar nuestros productos</p>
                </div>
            </div>
            <div runat="server" id="notOldOrders" class="infoContainer" style="display:none;">
                <div id="containerNotOldOrders">
                    <h2>No cuentas con un historial de pedidos</h2>
                    <img id="imgNotOldOrder" src="../Recursos/NotOldOrder.png" alt="Not orders" class="imgInfo" />
                </div>
            </div>
        </div>
    </div>
    <script>
        /*Metodo para ver que pedido se seleccionó para ver sus detalles*/
        function onClickDetails(comp) {
            var idLink = comp.id;
            var idOrderDetails = idLink.split("_");
            console.log("Detalles: " + idOrderDetails[0]);
            document.getElementById('hiddenIdDetailsOrder').value = idOrderDetails[0];
            document.getElementById('<%=btnDetailsOrder.ClientID %>').click();
        }
        /*Metodo para ver que pedido se seleccionó para eliminar*/
        function onClickDelete(comp) {
            var idLink = comp.id;
            var idOrderDelete = idLink.split("_");
            document.getElementById('hiddenIdDeleteOrder').value = idOrderDelete[0];
            document.getElementById('<%=btnDeleteOrder.ClientID %>').click();
        }
        /*Metodo para ver que pedido entregado se seleccionó para ver sus detalles*/
        function onClickOldDetails(comp) {
            var idLink = comp.id;
            var idOrderDelete = idLink.split("_");
            document.getElementById('hiddenIdDetailOldOrder').value = idOrderDelete[0];
            document.getElementById('<%=btnDetailOldOrder.ClientID %>').click();
        }
        //Metodo para descargar pdf
        function downloadOption() {
            document.getElementById('<%=btnDownloadPDF.ClientID %>').click();
        }
    </script>
</asp:Content>