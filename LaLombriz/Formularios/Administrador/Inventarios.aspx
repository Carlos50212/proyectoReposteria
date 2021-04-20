<%@ Page Title="Inventarios" Language="C#" MasterPageFile="~/Formularios/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="Inventarios.aspx.cs" Inherits="LaLombriz.Formularios.Administrador.Inventarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainAdminContent" runat="server">

    <link href="../../Estilos/Inventarios.css" rel="stylesheet" />

    <div id="inventarios-content">
        <div id="inventarios-container">
            <div id="gridInventario"></div>
        </div>
    </div>

    <!-- Modal editar producto -->
        <div id="modal-edit-product" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <input type="hidden" name="txtIdHidden" id="txtIdHidden"/>
                        <h5 class="modal-title">Producto <b id="modal-title-inventario-id" class="text-muted">{InventarioId}</b></h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div id="data-container">
                            <label>Nombre</label>
                            <input id="txtName" class="inputsData" readonly="readonly">
                            <label>Tamaño</label>
                            <input id="txtSize" class="inputsData" readonly="readonly">
                            <label>Descripción</label>
                            <textarea id="txtDescription" class="inputsData"></textarea>
                            <label>Precio</label>
                            <input id="txtPrice" class="inputsData">
                            <label>Disponible</label>
                            <input id="txtStock" class="inputsData">
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        <button type="button" class="btn btn-primary" onclick="updateProduct();">Actualizar</button>
                    </div>
                </div>
            </div>
        </div>

    <script src="../../JS/Inventarios.js"></script>

</asp:Content>
