$(document).ready(function () {
    $(".selected").removeClass("selected");
    $("#admin-link-sells").addClass("selected");

    $(".selectedLinkOrders").removeClass("selectedLinkOrders");
    $("#lastOption").addClass("selectedLinkOrders");

    $(".selectedOptionOrders").removeClass("selectedOptionOrders");
    $("#lkMost").addClass("selectedOptionOrders");

    const fecha = new Date();
    var month = getMonth(fecha);
    $('#day').text(fecha.getDate() + "/" + (fecha.getMonth() + 1) + "/" + fecha.getFullYear());
    $('#month').text(month);

    getSells();

});
function getSells() {
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "MasVendidos.aspx/getAllSells",
        dataType: "json",
        success: function (result) {
            var data = result.d;
            var products = $.parseJSON('[' + data + ']');
            console.log(products[0]);
            buildGrid(products[0]);
        },
        error: function (result) {
            console.log("ERROR AL OBTENER LAS VENTAS");
        }
    });
}
function buildGrid(products) {
    var i = 0;
    var productsFinalMonth = new Array;
    var productsFinalDay = new Array;
    for (var product in products) {
        var productTmp = JSON.parse(products[i]);
        for (var productMonth in productTmp['productosMes']) {
            var productMonthTmp = JSON.parse(productTmp['productosMes'][productMonth]);
            productsFinalMonth.push({ id_producto: productMonthTmp['id_producto'], nombre: productMonthTmp['nombre'], unidades: productMonthTmp['unidades'], fecha: productMonthTmp['fecha'], total: productMonthTmp['total'] });
        }
        for (var productDay in productTmp['productosDia']) {
            var productDayTmp = JSON.parse(productTmp['productosDia'][productDay]);
            productsFinalDay.push({ id_producto: productDayTmp['id_producto'], nombre: productDayTmp['nombre'], unidades: productDayTmp['unidades'], fecha: productDayTmp['fecha'], total: productDayTmp['total'] });
        }
        i++;
    }

    var dataSourceMonth = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                options.success(productsFinalMonth);
            }
        },
        pageSize: 10
    });
    var dataSourceDay = new kendo.data.DataSource({
        transport: {
            read: function (options) {
                options.success(productsFinalDay);
            }
        },
        pageSize: 10
    });

    $('#gridSellsPerMonth').kendoGrid({
        dataSource: dataSourceMonth,
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

    $('#gridSellsPerDay').kendoGrid({
        dataSource: dataSourceDay,
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
function getMonth(fecha) {
    var month = fecha.getMonth();
    var arrayMonths = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"]
    for (var i = 1; i <= 12; i++) {
        if (i == month) {
            return arrayMonths[i];
        }
    }
}
