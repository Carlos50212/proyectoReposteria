$(document).ready(function () {
    $(".selected").removeClass("selected");
    $("#admin-link-sells").addClass("selected");

    $(".selectedLinkOrders").removeClass("selectedLinkOrders");
    $("#secondOption").addClass("selectedLinkOrders");

    $(".selectedOptionOrders").removeClass("selectedOptionOrders");
    $("#lkMonth").addClass("selectedOptionOrders");

    const fecha = new Date();
    var month = getMonth(fecha);
    $('#month').text(month);

    getSellsMonth();

});
function getSellsMonth() {
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "VentasMes.aspx/getAllSellsMonth",
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
        productsFinal.push({ id_producto: productTmp['id_producto'], nombre: productTmp['nombre'], unidades: productTmp['unidades'], fecha: productTmp['fecha'] ,total: productTmp['total'] });
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

    $('#gridSellsPerMonth').kendoGrid({
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
                field: 'fecha',
                title: 'Fecha',
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
function getMonth(fecha){
    var month = fecha.getMonth();
    var arrayMonths = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"]
    for (var i = 1; i <= 12; i++) {
        if (i == month) {
            return arrayMonths[i];
        }
    }
}