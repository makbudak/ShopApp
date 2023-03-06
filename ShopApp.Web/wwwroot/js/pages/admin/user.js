$(() => {
    var id = 0;
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Admin/User/List",
                dataType: "json"
            },
        },
        pageSize: 5
    });

    $("#grid").kendoGrid({
        columns: [
            {
                command: [
                    { text: "", name: "edit", className: "bg-secondary text-white", iconClass: "k-icon k-i-edit", click: editUser },
                    { text: "", name: "remove", className: "bg-danger text-white", iconClass: "k-icon k-i-trash", click: deleteUser }
                ],
                title: " ",
                width: "120px"
            },
            {
                field: "name",
                title: "Adı",
            },
            {
                field: "surname",
                title: "Soyadı",
            },
            {
                field: "email",
                title: "Email Adresi",
            },
            {
                field: "phone",
                title: "Telefon No",
            },
            {
                template: kendo.template($("#isEmailConfirmed-template").html()),
                title: "Email Adresi Onaylandı mı?",
                width: "240px"
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

    var txtName = $("#txtName").kendoTextBox({
        placeholder: "Adı giriniz."
    }).data("kendoTextBox");

    var txtSurname = $("#txtSurname").kendoTextBox({
        placeholder: "Soyadı giriniz."
    }).data("kendoTextBox");

    var txtEmailAddress = $("#txtEmailAddress").kendoTextBox({
        placeholder: "Email adresi giriniz."
    }).data("kendoTextBox");

    var txtPhone = $("#txtPhone").kendoMaskedTextBox({
        mask: "(999) 000-0000"
    }).data("kendoMaskedTextBox");

    var chckIsActive = $("#chckIsActive").kendoCheckBox({
        checked: true,
        label: "Aktif"
    }).data("kendoCheckBox");

    $("#btnAdd").click((e) => {
        event.preventDefault();
        $("#userModal").modal("show");
        $("#userForm").trigger("reset");
        $("#userModalLabel").text("Kullanıcı Ekle");
        id = 0;
    });

    function editUser(e) {
        $("#userModal").modal("show");
        $("#userModalLabel").text("Kullanıcı Düzenle");

        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        txtName.value(dataItem.name);
        txtSurname.value(dataItem.surname);
        txtEmailAddress.value(dataItem.email);
        txtPhone.value(dataItem.phone);
        chckIsActive.value(dataItem.isActive);
        id = dataItem.id;
    }

    function deleteUser(e) {
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

    }

    var validator = $("#userForm").kendoValidator().data("kendoValidator");

    $("#btnSave").click((event) => {
        event.preventDefault();
        if (validator.validate()) {
            var data = {
                id: id,
                name: txtName.value(),
                surname: txtSurname.value(),
                email: txtEmailAddress.value(),
                phone: txtPhone.value(),
                isActive: chckIsActive.value()
            };

            if (id == 0) {
                $.ajax({
                    url: "/admin/user",
                    dataType: "json",
                    type: "POST",
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8"
                }).done((res) => {
                    dataSource.read();
                    $("#userModal").modal("hide");
                    successNotification("İşlem Başarılı!", "Kaydetme işlemi başarıyla gerçekleşti.");
                }).fail((err) => {
                    console.log(err);
                    //errorNotification("İşlem Başarısız", err.response.data.message);
                });
            } else {
                $.ajax({
                    url: "/admin/user",
                    dataType: "json",
                    type: "PUT",
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8"
                }).done((res) => {
                    dataSource.read();
                    $("#userModal").modal("hide");
                    successNotification("İşlem Başarılı!", "Güncelleme işlemi başarıyla gerçekleşti.");
                }).fail((err) => {
                    console.log(err);
                    //errorNotification("İşlem Başarısız", err.response.message);
                });
            }
        }
    });
});