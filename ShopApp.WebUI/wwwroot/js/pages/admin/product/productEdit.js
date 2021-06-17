$(function () {
    var id = $("#productId").val();

    var categoryDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/api/category",
                dataType: "json"
            },
        }
    });

    $("#cmbCategories").kendoMultiSelect({
        placeholder: "Kategori seçiniz",
        dataTextField: "name",
        dataValueField: "categoryId",
        dataSource: categoryDataSource
    });

    $.ajax({
        url: "/api/product/" + id,
        type: "GET",
        complete: function (res) {
            console.log(res);
        }
    });
});