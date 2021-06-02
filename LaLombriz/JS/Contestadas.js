var cotizacionFinal = new Array;
$(document).ready(function () {
    $(".selected").removeClass("selected");
    $("#admin-link-cotizaciones").addClass("selected");

    $(".selectedLinkOrders").removeClass("selectedLinkOrders");
    $("#secondOption").addClass("selectedLinkOrders");

    $(".selectedOptionOrders").removeClass("selectedOptionOrders");
    $("#lkContestadas").addClass("selectedOptionOrders");

    $("#txtName").kendoTextBox({
        placeholder: "Nombre administrador"
    });
    $("#txtEmail").kendoTextBox({
        placeholder: "Email administrador"
    });
    $("#txtPhone").kendoTextBox({
        placeholder: "Teléfono administrador"
    });

    $("#txtNameUser").kendoTextBox({
        placeholder: "Nombre usuario"
    });
    $("#txtEmailUser").kendoTextBox({
        placeholder: "Email usuario"
    });
    $("#txtPhoneUser").kendoTextBox({
        placeholder: "Teléfono usuario"
    });

    getCotizacionesContestadas();

    $('#gridContestadas').on('click', '.btn-link', function (e) {
        e.preventDefault();
        var cont = 0;
        var cotizacionId = $(e.target).data('cotizacion-id');
        for (const cotizacion in cotizacionFinal) {
            var cotizacionTmp = cotizacionFinal[cont];
            if (cotizacionTmp['id_cotizacion'] == cotizacionId) {
                var idUsuario = cotizacionFinal[cont]['id_cliente'];
                var idAdmin = cotizacionFinal[cont]['id_personal'];
                break;
            }
            cont++;
        }
        
        getInfoCliente(idUsuario, idAdmin, cotizacionId);

        $('#modal-title-cotizacion-id').text(cotizacionId);


        var seeCotizacionModal = new bootstrap.Modal(document.getElementById('modal-see-cotizacion'), {
            keyboard: false,
            backdrop: 'static'
        });

        seeCotizacionModal.show();
    })


});
function getCotizacionesContestadas() {
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "Contestadas.aspx/getCotizacionesContestadas",
        dataType: "json",
        success: function (result) {
            var data = result.d;
            var cotizacion = $.parseJSON('[' + data + ']');
            buildGrid(cotizacion[0]);
        },
        error: function (result) {
            console.log("ERROR AL OBTENER LAS COTIZACIONES");
        }
    });
}
function getInfoCliente(idUser, idAdmin, cotizacionId) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Contestadas.aspx/getInfoUser",
        dataType: "json",
        data: "{idCliente:'" + idUser + "', idAdmin: '" + idAdmin + "'}",
        success: function (result) {
            console.log("FUNCIONA");
            var data = result.d;
            var user = $.parseJSON('[' + data + ']');
            buildModal(user[0], cotizacionId);
        },
        error: function (result) {
            console.log("NO FUNCIONA" + result);
        }
    });
}

function buildGrid(cotizacion) {
    var i = 0;
    for (var cot in cotizacion) {
        var cotizacionTmp = JSON.parse(cotizacion[i]);
        cotizacionFinal.push({ id_cotizacion: cotizacionTmp['id_cotizacion'], id_cliente: cotizacionTmp['id_cliente'], id_personal: cotizacionTmp['id_personal'], correo: cotizacionTmp['correo'], fecha_contacto: cotizacionTmp['fecha_contacto'], fecha_respuesta: cotizacionTmp['fecha_respuesta'],mensaje: cotizacionTmp['mensaje'], respuesta: cotizacionTmp['respuesta'] });
        i++;
    }


    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                options.success(cotizacionFinal);
            }
        },
        pageSize: 10
    });

    $('#gridContestadas').kendoGrid({
        dataSource: dataSource,
        pageable: true,
        columns: [
            {
                field: 'id_cotizacion',
                title: 'ID cotizacion',
                width: "150px"
            },
            {
                field: 'correo',
                title: 'Correo cliente',
                width: "150px"
            },
            {
                field: 'fecha_contacto',
                title: 'Fecha de contacto',
                width: "150px"
            },
            {
                field: 'fecha_respuesta',
                title: 'Fecha de respuesta',
                width: "150px"
            },
            {
                title: 'Detalles',
                width: "150px",
                template:
                    "<div class='actions-container'>" +
                    "    <a class='btn btn-link' data-cotizacion-id='#: id_cotizacion #'>Ver</a>" +
                    "</div>"
            }
        ]
    });
}
function buildModal(usuario, cotizacionId) {
    var i = 0;
    var usuarioFinal = new Array;
    for (var cot in usuario) {
        var usuarioTmp = JSON.parse(usuario[i]);
        usuarioFinal.push({ nombre: usuarioTmp['nombre'], correo: usuarioTmp['correo'], telefono: usuarioTmp['telefono'] });
        i++;
    }

    $('#txtName').val(usuarioFinal[1]['nombre']);
    $('#txtEmail').val(usuarioFinal[1]['correo']);
    $('#txtPhone').val(usuarioFinal[1]['telefono']);

    $('#txtNameUser').val(usuarioFinal[0]['nombre']);
    $('#txtEmailUser').val(usuarioFinal[0]['correo']);
    $('#txtPhoneUser').val(usuarioFinal[0]['telefono']);

    var cont = 0;
    for (const cotizacion in cotizacionFinal) {
        var cotizacionTmp = cotizacionFinal[cont];
        if (cotizacionTmp['id_cotizacion'] == cotizacionId) {
            $('#taMessage').val(cotizacionFinal[cont]['mensaje']);
            $('#taAnswer').val(cotizacionFinal[cont]['respuesta']);
            break;
        }
        cont++;
    }
}