document.onkeydown = function (evt) {
    return (evt ? evt.which : event.keyCode) != 13;
}
function showCreateForm() {
    console.log("CLICK");
}
function FiltroNumeros(e) { //función para solo permitir números en los txtBox
    var keynum = window.event ? window.event.keyCode : e.which;
    if ((keynum == 8 || keynum == 48))
        return true;
    if (keynum <= 47 || keynum >= 58) return false;
    return /\d/.test(String.fromCharCode(keynum));
}

function FiltroLetras(evt) { //función para solo permitir letras en los txtBox
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode == 164 || charCode == 165 || charCode == 32 || (charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122))
        return true;
    return false;
}
function Correo(evt) {
    campo = evt.target;
    valido = document.getElementById('validador');

    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Filtramos el correo con el formato
    if (emailRegex.test(campo.value)) {
        validador.innerText = "";
    } else {
        validador.innerText = "Formato de correo incorrecto";
    }
}
function CorreoDos(evt) {
    campo = evt.target;
    valido = document.getElementById('validador2');

    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Filtramos el correo con el formato
    if (emailRegex.test(campo.value)) {
        validador2.innerText = "";
    } else {
        validador2.innerText = "Formato de correo incorrecto";
    }
}
function CorreoTres(evt) {
    campo = evt.target;
    valido = document.getElementById('validador3');

    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Filtramos el correo con el formato
    if (emailRegex.test(campo.value)) {
        validador3.innerText = "";
    } else {
        validador3.innerText = "Formato de correo incorrecto";
    }
}
