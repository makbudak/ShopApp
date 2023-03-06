$(() => {
    var id = 0;
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Admin/Role/List",
                dataType: "json"
            },
        },
        pageSize: 5
    });

    var txtRoleName = $("#txtRoleName").kendoTextBox({
        placeholder: "Rol adı giriniz."
    }).data("kendoTextBox");

    var chckIsActive = $("#chckIsActive").kendoCheckBox({
        checked: true,
        label: "Aktif"
    }).data("kendoCheckBox");

    $("#grid").kendoGrid({
        columns: [
            {
                command: [
                    { text: "", name: "edit", className: "bg-secondary text-white", iconClass: "k-icon k-i-edit", click: editRole },
                    { text: "", name: "remove", className: "bg-danger text-white", iconClass: "k-icon k-i-trash", click: deleteRole }
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
        $("#formRole").trigger("reset");
        $("#roleModal").modal("show");
        $("#roleModalLabel").text("Rol Ekle");
        id = 0;
    });

    function editRole(e) {
        $("#roleModal").modal("show");
        $("#formRole").trigger("reset");
        $("#roleModalLabel").text("Rol Düzenle");
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

        txtRoleName.value(dataItem.name);
        chckIsActive.value(dataItem.isActive);
        id = dataItem.id;
    }

    function deleteRole(e) {
        if (confirm("Silmek istediğinize emin misiniz?")) {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            axios.delete(`/Admin/Role/${dataItem.id}`)
                .then(res => {
                    dataSource.read();
                    successNotification("İşlem Başarılı!", "Silme işlemi başarıyla gerçekleşti.");
                }, (err) => {
                    errorNotification("İşlem Başarısız", err.response.data.message);
                });
        }
    }

    var validator = $("#formRole").kendoValidator().data("kendoValidator");

    $("#btnSave").click((event) => {
        event.preventDefault();
        if (validator.validate()) {
            var data = {
                id: id,
                name: txtRoleName.value(),
                isActive: chckIsActive.value()
            };

            if (id == 0) {
                axios.post("/Admin/Role", data)
                    .then(res => {
                        dataSource.read();
                        $("#roleModal").modal("hide");
                        successNotification("İşlem Başarılı!", "Kaydetme işlemi başarıyla gerçekleşti.");
                    }, (err) => {
                        errorNotification("İşlem Başarısız", err.response.data.message);
                    });
            } else {
                axios.put("/Admin/Role", data)
                    .then(res => {
                        dataSource.read();
                        $("#roleModal").modal("hide");
                        successNotification("İşlem Başarılı!", "Güncelleme işlemi başarıyla gerçekleşti.");
                    }, (err) => {
                        errorNotification("İşlem Başarısız", err.response.data.message);
                    });
            }
        }
    });
});