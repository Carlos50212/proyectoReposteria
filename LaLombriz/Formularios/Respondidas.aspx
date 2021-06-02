<%@ Page Title="Respondidas - LaLombriz" Language="C#" MasterPageFile="~/Formularios/CotizacionesUsuario.master" AutoEventWireup="true" CodeBehind="Respondidas.aspx.cs" Inherits="LaLombriz.Formularios.Respondidas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CotizacionesOptions" runat="server">
    <link href="../Estilos/Respondidas.css" rel="stylesheet"/>
    <div id="mainContainerR">
        <div id="notCotizaciones" runat="server" style="display: none;">
            <div id="containerNotCotizaciones">
                <h2>No cuentas con un historial de cotizaciones respondidas</h2>
                <img id="imgNotOldOrder" src="../Recursos/NotOldOrder.png" alt="Not orders" class="imgInfo" />
            </div>
        </div>
        <div runat="server" id="contestadas" class="cotizacionesContestadas-containe">
            <asp:Literal ID="ltCotizacionesRespondidas" runat="server"></asp:Literal>
        </div>
    </div>
    <script src="../JS/Respondidas.js"></script>
</asp:Content>
