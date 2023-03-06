$(() => {
    var dataSource = new kendo.data.DataSource({
        type: "odata",
        transport: {
            read: {
                url: "https://demos.telerik.com/kendo-ui/service/Northwind.svc/Products"
            }
        },
        serverPaging: true,
        pageSize: 3
    });

    $("#scrollView").kendoScrollView({
        dataSource: dataSource,
        template: $("#scrollview-template").html(),
        contentHeight: "100%",
        enablePager: false,
        autoBind: true
    });

});

function setBackground(id) {
    return "url(https://demos.telerik.com/kendo-ui/content/web/foods/" + id + ".jpg)";
}
