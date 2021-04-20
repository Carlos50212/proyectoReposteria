<%@ Page Title="Por entregar" Language="C#" MasterPageFile="~/Formularios/Administrador/PedidosMaster.master" AutoEventWireup="true" CodeBehind="NewOrders.aspx.cs" Inherits="LaLombriz.Formularios.Administrador.NewOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PedidosOptions" runat="server">
    <div id="newOrders-container">
        <div id="gridNewOrders"></div>

        <!-- Modal ver productos -->
        <div id="modal-see-order" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Detalles pedido <b id="modal-title-order-id" class="text-muted">{orderId}</b></h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div id="productos-container"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <script src="../../JS/NuevosPedidos.js"></script>
</asp:Content>
