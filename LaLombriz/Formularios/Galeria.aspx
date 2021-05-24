<%@ Page Title="Galería" Language="C#" MasterPageFile="~/Formularios/Site.Master" AutoEventWireup="true" CodeBehind="Galeria.aspx.cs" Inherits="LaLombriz.Formularios.Galeria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="galleryMainContainer">
        <div class="titleGallery">
            <h1>¡Ve nuestros productos!</h1>
        </div>
        <div class="galleryContainer">
            <asp:Literal runat="server" ID="galleryGrid"></asp:Literal>
        </div>        
    </div>
    <div id="modalZoom" class="modalZoom">
        <span class="close">&times;</span>
        <img class="modalContent" id="imgSelection" src="/Recursos/imagen.png"/>
    </div>
    <script src="../JS/Galeria.js"></script>
</asp:Content>
