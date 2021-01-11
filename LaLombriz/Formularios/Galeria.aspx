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
    <script>
        var modal = document.getElementById("modalZoom");
        function getImage(comp) {
            document.getElementsByTagName("html")[0].style.overflow = "hidden"; /*Quitamos scroll principal*/
            var imgSelected = document.getElementById(comp.id);
            var imgModal = document.getElementById("imgSelection");
            modal.style.display = "block";
            imgModal.src = imgSelected.src;
            var close = document.getElementsByClassName("close")[0];
            close.onclick = function () {
                modal.style.display = "none";
                document.getElementsByTagName("html")[0].style.overflow = "auto"; /*Aparecer scroll principal*/
            }
        }
    </script>
</asp:Content>
