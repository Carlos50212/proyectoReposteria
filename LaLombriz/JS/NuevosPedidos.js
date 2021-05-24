﻿$(document).ready(function () {
    $(".selected").removeClass("selected");
    $("#admin-link-orders").addClass("selected");

    $(".selectedLinkOrders").removeClass("selectedLinkOrders");
    $("#firstOption").addClass("selectedLinkOrders");

    $(".selectedOptionOrders").removeClass("selectedOptionOrders");
    $("#lkNew").addClass("selectedOptionOrders");

    getNewOrders();

    $('#gridNewOrders').on('click', '.btn-link', function (e) {
        e.preventDefault();
        var orderId = $(e.target).data('order-id');

        $('#modal-title-order-id').text(orderId);

        getInfoProducts(orderId);

        var seeOrderModal = new bootstrap.Modal(document.getElementById('modal-see-order'), {
            keyboard: false,
            backdrop: 'static'
        });

        seeOrderModal.show();
    })

    $('#gridNewOrders').on('click', '.btnDelivered', function (e) {
        e.preventDefault();
        var orderId = $(e.target).data('order-id');

        deliverOrder(orderId);
    })

    $('#gridNewOrders').on('click', '.btnCancel', function (e) {
        e.preventDefault();
        var orderId = $(e.target).data('order-id');

        cancelOrder(orderId);
    })


});

function getNewOrders() {
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "NewOrders.aspx/getAllNewOrders",
        dataType: "json",
        success: function (result) {
            var data = result.d;
            var orders = $.parseJSON('[' + data + ']');
            buildGrid(orders[0]);
        },
        error: function (result) {
            console.log("ERROR AL OBTENER LOS PEDIDOS");
        }
    });
}

function buildGrid(orders) {

    var i = 0;
    var ordersFinal = new Array;
    for (var order in orders) {
        var orderTmp = JSON.parse(orders[i]);
        ordersFinal.push({ id_pedido: orderTmp['id_pedido'], id_usuario: orderTmp['id_usuario'], fecha_entrega: orderTmp['fecha_entrega'], fecha_creacion: orderTmp['fecha_creacion'], precio: orderTmp['precio'] });
        i++;
    }


    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                options.success(ordersFinal);
            }
        },
        pageSize: 10
    });

    $('#gridNewOrders').kendoGrid({
        dataSource: dataSource,
        pageable: true,
        columns: [
            {
                field: 'id_pedido',
                title: 'ID',
                width: "150px"
            },
            {
                field: 'fecha_entrega',
                title: 'Fecha entrega',
                width: "150px"
            },
            {
                field: 'fecha_creacion',
                title: 'Fecha creación',
                width: "150px"
            },
            {
                field: 'precio',
                title: 'Precio',
                width: "150px"
            },
            {
                title: 'Detalles',
                width: "150px",
                template: 
			    		"<div class='actions-container'>" +
			    		"    <a class='btn btn-link' data-order-id='#: id_pedido #'>Ver</a>" +
			    		"</div>"
            },
            {
                title: 'Acciones',
                width: "350px",
                attributes: {
                    style: "text-align:center"
                },
                template:
                    "<div class='actions-container'>" +
                    "    <a class='btn btn-info btnDelivered' style='color:white' data-order-id='#: id_pedido #'>Entregado</button>" +
                    "    <a class='btn btn-danger btn-delete-indexetest btnCancel' data-order-id='#: id_pedido #'>Cancelar</a>" +
                    "</div>"
            }
        ]
    });

}

function getInfoProducts(orderId) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "NewOrders.aspx/getAllProducts",
        dataType: "json",
        data: "{idPedido:'"+orderId+"'}",
        success: function (result) {
            var data = result.d;
            var details = $.parseJSON('[' + data + ']');
            buildModalDetails(details[0]);
        },
        error: function (result) {
            //console.log("ERROR AL OBTENER LOS PEDIDOS");
        }
    });
}

function buildModalDetails(details) {
    var i = 0;
    var detailsFinal = new Array;
    for (var detail in details) {
        var detailTmp = JSON.parse(details[i]);
        detailsFinal.push({ id_producto: detailTmp['id_producto'], nombre_producto: detailTmp['nombre_producto'], tamanio: detailTmp['tamanio'], descripcion: detailTmp['descripcion'], cantidad: detailTmp['cantidad'] });
        i++;
    }

    detailsContent = document.getElementById("productos-container");

    for (var i = 0; i < detailsFinal.length;i++)
    {
        var content = "<b>Producto: " + (i + 1) + "</b><br><textarea class='form-control' id='txArea" + (i + 1) + "' rows='3'>Producto: " + detailsFinal[i]['nombre_producto'] + "\nTamaño: " + detailsFinal[i]['tamanio'] + "\nCantidad: " + detailsFinal[i]['cantidad'] + "\nDescripción: " + detailsFinal[i]['descripcion'] + "</textarea>";
        const contentItem = detailsContent.innerHTML;
        detailsContent.innerHTML = contentItem + content;
    }


}

function deliverOrder(orderId) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "NewOrders.aspx/deliverOrder",
        dataType: "json",
        data: "{idPedido:'" + orderId + "'}",
        success: function (result) {
            Swal.fire({
                icon: 'success',
                title: 'Estado cambiado correctamente',
                showConfirmButton: true
            }).then(function () {
                location.reload();
            });
        },
        error: function (result) {
            //console.log("ERROR AL OBTENER LOS PEDIDOS");
        }
    });
}

function cancelOrder(orderId) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "NewOrders.aspx/cancelOrder",
        dataType: "json",
        data: "{idPedido:'" + orderId + "'}",
        success: function (result) {
            Swal.fire({
                icon: 'success',
                title: 'Pedido cancelado correctamente',
                showConfirmButton: true
            }).then(function () {
                location.reload();
            });
        },
        error: function (result) {
            console.log(result);
        }
    });
}