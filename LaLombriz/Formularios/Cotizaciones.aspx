<%@ Page Title="Cotizaciones" Language="C#" MasterPageFile="~/Formularios/CotizacionesUsuario.Master" AutoEventWireup="true" CodeBehind="Cotizaciones.aspx.cs" Inherits="LaLombriz.Formularios.Cotizaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CotizacionesOptions" runat="server">
    <link href="../Estilos/CotizacionStyles.css" rel="stylesheet" />
    <div id="cotizacion-container" class="row">
        <div id="text-container" class="col-xs-12 col-ms-12">
            <h2>¿No encontraste lo que querías en nuestro menú?</h2><br />
            <p>No te preocupes, mandanos un mensaje con la descripción de lo que buscas y nosotros nos pondremos en contacto contigo, recuerda, el único límite es tu imaginación</p>
        </div>
        <div id="description-container" class="col-xs-12 col-ms-12">
            <div id="form" class="form-floating">
                <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" class="form-control" placeholder="Deja tus ideas aquí" Style="height: 300px"></asp:TextBox>
                <label for="txtDescription">Deja tus ideas aquí</label>
            </div>
            <div id="btn-container">
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Enviar" OnClick="btnEnviarCorreoOnClick"/> 
            </div>
        </div>
    </div>
    <script src="../JS/Cotizaciones.js"></script>
</asp:Content>
