<%@ Page Title="Contestadas" Language="C#" MasterPageFile="~/Formularios/Administrador/CotizacionesAdmin.master" AutoEventWireup="true" CodeBehind="Contestadas.aspx.cs" Inherits="LaLombriz.Formularios.Administrador.Contestadas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CotizacionesOptions" runat="server">

    <link href="../../Estilos/Contestadas.css" rel="stylesheet" />

    <div id="cotizacionesContestadas-container">
        <div id="gridContestadas"></div>
    </div>
    <!-- Modal ver detalles cotizacion -->
        <div id="modal-see-cotizacion" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <input type="hidden" name="txtIdHidden" id="txtIdHidden"/>
                        <h5 class="modal-title">Cotización <b id="modal-title-cotizacion-id" class="text-muted">{cotizacionId}</b></h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div id="admin-container">
                            <div id="header-admin">
                                <p><b>Información del personal que respondió</b></p>
                            </div>
                            <div id="content-admin">
                                <label>Nombre</label>
                                <input id="txtName" class="inputsData" readonly="readonly">
                                <label>Correo</label>
                                <input id="txtEmail" class="inputsData" readonly="readonly">
                                <label>Teléfono</label>
                                <input id="txtPhone" class="inputsData" readonly="readonly">
                            </div>
                        </div>
                        <div id="user-container">
                            <div id="header-user">
                                <p><b>Información del cliente</b></p>
                            </div>
                            <div id="content-user">
                                <label>Nombre</label>
                                <input id="txtNameUser" class="inputsData" readonly="readonly">
                                <label>Correo</label>
                                <input id="txtEmailUser" class="inputsData" readonly="readonly">
                                <label>Teléfono</label>
                                <input id="txtPhoneUser" class="inputsData" readonly="readonly">
                            </div>
                        </div>
                        <div id="messages-container">
                            <div id="message-container" class="tMessages">
                                <p><b>Cotizacion</b></p>
                                <div id="message-textarea">
                                    <textarea class="form-control"  rows="6" id="taMessage" readonly="readonly"></textarea>
                                </div>
                            </div>
                            <div id="answer-container" class="tMessages">
                                <p><b>Respuesta</b></p>
                                <div id="answer-textarea">
                                    <textarea class="form-control" rows="6" id="taAnswer" readonly="readonly"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    <script src="../../JS/Contestadas.js"></script>
</asp:Content>
