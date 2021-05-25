var modal = document.getElementById("modalZoom");
$(document).ready(function () {
    $(".selected").removeClass("selected");
    $("#user-link-galeria").addClass("selected");
})

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