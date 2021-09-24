$(function () {
    var id = 0;
    var categoryModal = new bootstrap.Modal($("#categoryModal"));

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/api/category",
                dataType: "json"
            },
        },
        pageSize: 5
    });

});