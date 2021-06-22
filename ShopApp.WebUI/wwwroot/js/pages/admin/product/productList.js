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
    dataSource: dataSource,
    loaderType: "loadingPanel",
    columns: [
        {
            command: [
                { text: "", name: "edit", iconClass: "k-icon k-i-edit", click: editProduct },
                { text: "", name: "remove", iconClass: "k-icon k-i-trash", click: deleteProduct }
            ],
            title: " ",
            width: 120
        },
        {
            field: "name",
            title: "Ürün Adı",
        },
        {
            field: "price",
            title: "Fiyatı"
        },
        {
            title: "Onaylı",
            template: '#=dirtyField(data,"isApproved")#<input type="checkbox" #= isApproved ? \'checked="checked"\' : "" # class="chkbxIsApproved k-checkbox" />',
            width: 100
        },
        {
            title: "Anasayfa",
            template: '#=dirtyField(data,"isHome")#<input type="checkbox" #= isHome ? \'checked="checked"\' : "" # class="chkbxIsHome k-checkbox" />',
            width: 100
        },
        {
            field: "url",
            title: "URL",
        }],
    pageable: {
        pageSize: 5,
        alwaysVisible: true
    }
});

function editProduct(e) {
    var data = this.dataItem($(e.currentTarget).closest("tr"));
    location.href = "/admin/product/edit/" + data.id;
}

function deleteProduct(e) {

}

$("#grid .k-grid-content").on("change", "input.chkbxIsApproved", function (e) {
    var grid = $("#grid").data("kendoGrid"),
        dataItem = grid.dataItem($(e.target).closest("tr"));
    dataItem.set("isApproved", this.checked);
});


$("#grid .k-grid-content").on("change", "input.chkbxIsHome", function (e) {
    var grid = $("#grid").data("kendoGrid"),
        dataItem = grid.dataItem($(e.target).closest("tr"));
    dataItem.set("isHome", this.checked);
});

function dirtyField(data, fieldName) {
    var hasClass = $("[data-uid=" + data.id + "]").find(".k-dirty-cell").length < 1;
    if (data.dirty && data.dirtyFields[fieldName] && hasClass) {
        return "<span class='k-dirty'></span>"
    }
    else {
        return "";
    }
}
