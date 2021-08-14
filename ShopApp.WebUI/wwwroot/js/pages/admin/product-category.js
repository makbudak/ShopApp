var id = 0;
var categoryModal = new bootstrap.Modal($("#categoryModal"));

var dataSource = new kendo.data.DataSource({
    transport: {
        read: {
            url: "/api/product-category",
            dataType: "json"
        },
    },
    pageSize: 5
});

$("#grid").kendoGrid({
    columns: [
        {
            command: [
                { text: "", name: "edit", iconClass: "k-icon k-i-edit", click: editCategory },
                { text: "", name: "remove", iconClass: "k-icon k-i-trash", click: deleteCategory }
            ],
            title: " ",
            width: "120px"
        },
        {
            field: "name",
            title: "Kategori Adı",
        },
        {
            field: "url",
            title: "URL",
        }],
    dataSource: dataSource,
    loaderType: "loadingPanel",
    pageable: {
        pageSize: 5,
        alwaysVisible: true
    }
});

$("#txtName").kendoTextBox({
    placeholder: "Kategori Adı",
});

$("#txtUrl").kendoTextBox({
    placeholder: "URL",
});

$("#btnAdd").click(function () {
    $("#modalTitle").text("Yeni Kategori Ekle");
    categoryModal.show();
    reset();
});

function editCategory(e) {
    $("#modalTitle").text("Kategori Düzenle");
    $("#categoryModal").modal("show");
    e.preventDefault();
    var data = this.dataItem($(e.currentTarget).closest("tr"));
    $("#txtName").data("kendoTextBox").value(data.name);
    $("#txtUrl").data("kendoTextBox").value(data.url);
    id = data.categoryId;
}

function deleteCategory(e) {
    e.preventDefault();
    var data = this.dataItem($(e.currentTarget).closest("tr"));
    kendo.confirm("Silme istediğinize emin misiniz?").then(function () {
        $.ajax({
            url: "/api/category/" + data.categoryId,
            type: "DELETE",
            complete: function (res) {
                dataSource.read();
            }
        });
    });
}

var validator = $("#frmCategory").kendoValidator().data("kendoValidator");

$("#frmCategory").submit(function (event) {
    event.preventDefault();
    var type = "";
    if (validator.validate()) {
        var data = {
            categoryId: id,
            name: $("#txtName").data("kendoTextBox").value(),
            url: $("#txtUrl").data("kendoTextBox").value()
        };
        if (id == 0) {
            type = "POST";
        } else {
            type = "PUT";
        }
        $.ajax({
            url: "/api/category",
            type: type,
            data: JSON.stringify(data),
            dataType: "json",
            contentType: "application/json",
            complete: function (res) {
                categoryModal.hide();
                dataSource.read();
            }
        });
    }
});

function reset() {
    id = 0;
    $("#txtName").data("kendoTextBox").value("");
    $("#txtUrl").data("kendoTextBox").value("");
}
