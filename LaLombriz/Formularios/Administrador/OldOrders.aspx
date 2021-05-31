<%@ Page Title="Entregados" Language="C#" MasterPageFile="~/Formularios/Administrador/PedidosMaster.master" AutoEventWireup="true" CodeBehind="OldOrders.aspx.cs" Inherits="LaLombriz.Formularios.Administrador.OldOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PedidosOptions" runat="server">
    <div id="OldOrders-container">
        <div id="gridOldOrders"></div>

        <!-- Modal ver productos -->
        <div id="modal-see-order" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Detalles pedido <b id="modal-title-order-id" class="text-muted">{orderId}</b></h5>
                        <button type="button" class="btn-close" onclick="cleanInterface();" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div id="productos-container"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" onclick="cleanInterface();">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <script src="../../JS/OldOrders.js"></script>
</asp:Content>
