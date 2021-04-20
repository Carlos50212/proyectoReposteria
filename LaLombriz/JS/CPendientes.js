var mensajes = new Array;
$(document).ready(function () {
    $(".selected").removeClass("selected");
    $("#admin-link-cotizaciones").addClass("selected");

    $(".selectedLinkOrders").removeClass("selectedLinkOrders");
    $("#firstOption").addClass("selectedLinkOrders");

    $(".selectedOptionOrders").removeClass("selectedOptionOrders");
    $("#lkPendientes").addClass("selectedOptionOrders");

    getCotizaciones();

    $('#gridPendientes').on('click', '.btn-link', function (e) {
        e.preventDefault();
        var cotizacionId = $(e.target).data('cotizacion-id');

        $('#txtIdHidden').val(cotizacionId);
        $('#modal-title-cotizacion-id').text(cotizacionId);
        $('#taQuestion').val(mensajes[cotizacionId - 1]);


        var seeCotizacionModal = new bootstrap.Modal(document.getElementById('modal-see-cotizacion'), {
            keyboard: false,
            backdrop: 'static'
        });

        seeCotizacionModal.show();
    })

});
function getCotizaciones() {
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "Pendientes.aspx/getCotizacionesPendientes",
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
function buildGrid(cotizacion) {
    var i = 0;
    var cotizacionFinal = new Array;
    for (var cot in cotizacion) {
        var cotizacionTmp = JSON.parse(cotizacion[i]);
        cotizacionFinal.push({id_cotizacion: cotizacionTmp['id_cotizacion'], id_cliente: cotizacionTmp['id_cliente'], correo: cotizacionTmp['correo'], fecha_contacto: cotizacionTmp['fecha_contacto'] });
        mensajes.push(cotizacionTmp['mensaje']);
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

    $('#gridPendientes').kendoGrid({
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
function sendAnswer() {
    var cotizacionId = $('#txtIdHidden').val();
    var answer = $('#taAnswer').val();

    if (answer === "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Favor de ingresar una respuesta'
        });
    } else {
        sendDataAnswer(cotizacionId, answer);
    }
}
function sendDataAnswer(cotizacionId, answer) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Pendientes.aspx/sendAnswer",
        dataType: "json",
        data: "{idCotizacion:'" + cotizacionId + "', respuesta:'" + answer + "'}",
        beforeSend: function () {
            Swal.fire({
                title: 'Enviando respuesta',
                showConfirmButton: false,
                willOpen: () => {
                    Swal.showLoading()
                },
            });
        },
        success: function (result) {
            console.log("FUNCIONA");
            swal.close();
            Swal.fire({
                icon: 'success',
                title: 'Cotización respondida correctamente',
                showConfirmButton: false,
                timer: 1500
            });
        },
        error: function (result) {
            console.log("NO FUNCIONA"+result);
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Error al responder la cotización'
            });
        }
    });
}