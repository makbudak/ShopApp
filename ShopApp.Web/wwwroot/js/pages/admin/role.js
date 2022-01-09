$(function () {
    var id = 0;
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/admin/role/list",
                dataType: "json"
            },
        },
        pageSize: 5
    });

    var txtRoleName = $("#txtRoleName").kendoTextBox({
        placeholder: "Rol adı giriniz."
    }).data("kendoTextBox");

    var windowRole = $("#windowRole").kendoWindow({
        width: "600px",
        height: "300px",
        modal: true,
        visible: false,
        animation: false,
        open: adjustSize,
        scrollable: true
    }).data("kendoWindow");

    $("#grid").kendoGrid({
        columns: [
            {
                command: [
                    { text: "", name: "edit", iconClass: "k-icon k-i-edit", click: editRole },
                    { text: "", name: "remove", iconClass: "k-icon k-i-trash", click: deleteRole }
                ],
                title: " ",
                width: "120px"
            },
            {
                field: "name",
                title: "Rol Adı",
            },
            {
                template: kendo.template($("#isActive-template").html()),
                title: "Aktif"
            }
        ],
        dataSource: dataSource,
        loaderType: "loadingPanel",
        pageable: {
            pageSize: 5,
            alwaysVisible: true
        }
    });

    $("#btnAdd").click((e) => {
        event.preventDefault();
        $("#roleForm").trigger("reset");
        windowRole.center().open();
        $(".k-window-title").text("Rol Ekle");
        id = 0;
    });


    function editRole(e) {
        windowRole.center().open();
        $(".k-window-title").text("Rol Düzenle");
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

        txtRoleName.value(dataItem.name);
        $("#chckIsActive").prop("checked", dataItem.isActive);
        id = dataItem.id;
    }

    function deleteRole(e) {
        if (confirm("Silmek istediğinize emin misiniz?")) {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            axios.delete(`/admin/role/${dataItem.id}`)
                .then(res => {
                    dataSource.read();
                    successNotification("İşlem Başarılı!", "Silme işlemi başarıyla gerçekleşti.");
                }, (err) => {
                    errorNotification("İşlem Başarısız", err.response.data.message);
                });
        }
    }

    $("#btnClose").click(() => {
        windowRole.close();
    });

    var validator = $("#roleForm").kendoValidator().data("kendoValidator");

    $("#btnSave").click((event) => {
        event.preventDefault();
        if (validator.validate()) {
            var data = {
                id: id,
                name: txtRoleName.value(),
                isActive: $("#chckIsActive").prop("checked")
            };

            if (id == 0) {
                axios.post("/admin/role", data)
                    .then(res => {
                        dataSource.read();
                        windowRole.close();
                        successNotification("İşlem Başarılı!", "Kaydetme işlemi başarıyla gerçekleşti.");
                    }, (err) => {
                        errorNotification("İşlem Başarısız", err.response.data.message);
                    });
            } else {
                axios.put("/admin/role", data)
                    .then(res => {
                        dataSource.read();
                        windowRole.close();
                        successNotification("İşlem Başarılı!", "Güncelleme işlemi başarıyla gerçekleşti.");
                    }, (err) => {
                        errorNotification("İşlem Başarısız", err.response.data.message);
                    });
            }
        }
    });
});