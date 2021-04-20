<%@ Page Title="Pendientes" Language="C#" MasterPageFile="~/Formularios/Administrador/CotizacionesAdmin.master" AutoEventWireup="true" CodeBehind="Pendientes.aspx.cs" Inherits="LaLombriz.Formularios.Administrador.CPendientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CotizacionesOptions" runat="server">

    <link href="../../Estilos/CPendientes.css" rel="stylesheet" />

    <div id="cotizacionesPendientes-container">
        <div id="gridPendientes"></div>
    </div>
    <!-- Modal ver y responder cotización -->
        <div id="modal-see-cotizacion" class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <input type="hidden" name="txtIdHidden" id="txtIdHidden"/>
                        <h5 class="modal-title">Cotización <b id="modal-title-cotizacion-id" class="text-muted">{cotizacionId}</b></h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div id="question-container">
                            <p><b>Cotización recibida</b></p>
                            <div id="question-textarea">
                                 <textarea class="form-control" id="taQuestion" rows="5" readonly="readonly"></textarea>
                            </div>
                        </div>
                        <div id="answer-container">
                            <p><b>Respuesta</b></p>
                            <div id="answer-textarea">
                                 <textarea  class="form-control" rows="5" id="taAnswer"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        <button type="button" class="btn btn-primary" onclick="sendAnswer();">Responder</button>
                    </div>
                </div>
            </div>
        </div>
    <script src="../../JS/CPendientes.js"></script>
</asp:Content>
