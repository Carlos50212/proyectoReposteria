var productos = new Array();
var inventarioId;
$(document).ready(function () {
    $(".selected").removeClass("selected");
    $("#admin-link-inventarios").addClass("selected");

    $("#txtName").kendoTextBox({
        placeholder: "Nombre producto"
    });
    $("#txtSize").kendoTextBox({
        placeholder: "Tamaño"
    });
    $("#txtDescription").kendoTextArea({
        rows: 5,
        placeholder: "Descripción"
    });
    $("#txtPrice").kendoTextBox({
        placeholder: "Precio"
    });
    $("#txtStock").kendoTextBox({
        placeholder: "Disponible"
    });

    getProducts();

    $('#gridInventario').on('click', '.btn-link', function (e) {
        e.preventDefault();
        inventarioId = $(e.target).data('inventario-id');

        $('#txtIdHidden').val(inventarioId);
        $('#modal-title-inventario-id').text(inventarioId);

        buildModal(inventarioId);

        var seeInventarioModal = new bootstrap.Modal(document.getElementById('modal-edit-product'), {
            keyboard: false,
            backdrop: 'static'
        });

        seeInventarioModal.show();
    })
});
function getProducts() {
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "Inventarios.aspx/getInventario",
        dataType: "json",
        success: function (result) {
            var data = result.d;
            var inventario = $.parseJSON('[' + data + ']');
            buildGrid(inventario[0]);
        },
        error: function (result) {
            console.log("ERROR AL OBTENER LAS COTIZACIONES");
        }
    });
}
function buildGrid(inventario) {
    var i = 0;
    for (var inv in inventario) {
        var inventarioTmp = JSON.parse(inventario[i]);
        productos.push({ id_producto: inventarioTmp['id_producto'], nombre: inventarioTmp['nombre'], descripcion: inventarioTmp['descripcion'], tamanio: inventarioTmp['tamanio'], precio: inventarioTmp['precio'], stock: inventarioTmp['stock']});
        i++;
    }


    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                options.success(productos);
            }
        },
        pageSize: 10
    });

    $('#gridInventario').kendoGrid({
        dataSource: dataSource,
        pageable: true,
        sortable: true,
        reorderable: true,
        toolbar: ["search"],
        columns: [
            {
                field: 'id_producto',
                title: 'ID producto',
                width: "150px"
            },
            {
                field: 'nombre',
                title: 'Nombre',
                width: "150px"
            },
            {
                field: 'tamanio',
                title: 'Tamaño',
                width: "150px"
            },
            {
                field: 'stock',
                title: 'Disponible',
                width: "150px"
            },
            {
                title: 'Detalles',
                width: "150px",
                template:
                    "<div class='actions-container'>" +
                    "    <a class='btn btn-link' data-inventario-id='#: id_producto #'>Ver</a>" +
                    "</div>"
            }
        ]
    });
}
function buildModal(inventarioId) {
    $('#txtName').val(productos[inventarioId-1]['nombre']);
    $('#txtSize').val(productos[inventarioId - 1]['tamanio']);
    $('#txtDescription').val(productos[inventarioId - 1]['descripcion']);
    $('#txtPrice').val(productos[inventarioId - 1]['precio']);
    $('#txtStock').val(productos[inventarioId - 1]['stock']);
}
function updateProduct(){
    var descripcion = $('#txtDescription').val();
    var precio = $('#txtPrice').val();
    var stock = $('#txtStock').val();
    if (descripcion === "" || precio === "" || stock === "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Favor de no dejar campos vacíos'
        });
    }
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "Inventarios.aspx/updateProduct",
        dataType: "json",
        data: "{idProducto:'" + inventarioId + "', descripcion: '" + descripcion + "', precio:'" + precio + "', disponible:'" + stock + "'}",
        success: function (result) {
            console.log("FUNCIONA");
            Swal.fire({
                icon: 'success',
                title: 'Producto actualizado correctamente',
                showConfirmButton: true
            }).then(function () {
                location.reload();
            });
        },
        error: function (result) {
            console.log("NO FUNCIONA" + result);
        }
    });
}