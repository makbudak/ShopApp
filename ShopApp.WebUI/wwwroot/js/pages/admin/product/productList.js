$(function () {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/api/product",
                dataType: "json"
            },
        },
        pageSize: 5
    });

    $("#grid").kendoGrid({
        rowTemplate: kendo.template($("#rowTemplate").html()),
        dataSource: dataSource,
        loaderType: "loadingPanel",
        pageable: {
            pageSize: 5,
            alwaysVisible: true
        }
    });

    function editProduct(e) {

    }

    function deleteProduct(e) {

    }

});