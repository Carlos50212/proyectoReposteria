$(document).ready(function () {
    $(".selected").removeClass("selected");
    $("#admin-link-sells").addClass("selected");

    $(".selectedLinkOrders").removeClass("selectedLinkOrders");
    $("#firstOption").addClass("selectedLinkOrders");

    $(".selectedOptionOrders").removeClass("selectedOptionOrders");
    $("#lkDay").addClass("selectedOptionOrders");

    const fecha = new Date();
    $('#day').text(fecha.getDate() + "/" + (fecha.getMonth() + 1) + "/" + fecha.getFullYear());

    getSellsDay();

});
function getSellsDay() {
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "VentasDia.aspx/getAllSellsDay",
        dataType: "json",
        success: function (result) {
            var data = result.d;
            var products = $.parseJSON('[' + data + ']');
            buildGrid(products[0]);
        },
        error: function (result) {
            console.log("ERROR AL OBTENER LAS VENTAS");
        }
    });
}
function buildGrid(products) {

    var i = 0;
    var productsFinal = new Array;
    for (var product in products) {
        var productTmp = JSON.parse(products[i]);
        productsFinal.push({ id_producto: productTmp['id_producto'], nombre: productTmp['nombre'], unidades: productTmp['unidades'], total: productTmp['total'] });
        i++;
    }


    var dataSource = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                options.success(productsFinal);
            }
        },
        pageSize: 10
    });

    $('#gridSellsPerDay').kendoGrid({
        dataSource: dataSource,
        pageable: true,
        columns: [
            {
                field: 'id_producto',
                title: 'ID',
                width: "150px"
            },
            {
                field: 'nombre',
                title: 'Nombre',
                width: "150px"
            },
            {
                field: 'unidades',
                title: 'Unidades',
                width: "150px"
            },
            {
                field: 'total',
                title: 'Total',
                width: "150px"
            }
        ]
    });
}