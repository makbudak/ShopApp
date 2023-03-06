$(() => {
    var id = 0;
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Admin/BlogCategory/List",
                dataType: "json"
            },
        },
        pageSize: 5
    });

    var txtBlogCategoryName = $("#txtBlogCategoryName").kendoTextBox({
        placeholder: "Kategori adı giriniz."
    }).data("kendoTextBox");

    var chckIsActive = $("#chckIsActive").kendoCheckBox({
        checked: true,
        label: "Aktif"
    }).data("kendoCheckBox");

    $("#btnAdd").click((e) => {
        event.preventDefault();
        $("#formBlogCategory").trigger("reset");
        $("#blogCategoryModal").modal("show");
        $("#blogCategoryModalLabel").text("Blog Kategorisi Ekle");
        id = 0;
    });

    $("#grid").kendoGrid({
        dataSource: dataSource,
        loaderType: "loadingPanel",
        pageable: {
            pageSize: 5,
            messages: {
                empty: "Kayıt Yok.",
                display: "Toplam {2} kayıt"
            }
        },
        navigatable: true,
        noRecords: {
            template: "<p class='p-2'>Kayıt Yok.</p>"
        },
        scrollable: {
            virtual: "columns"
        },
        rowTemplate: kendo.template($("#template").html())
    });

    $(document).on('click', ".blogCategoryEdit", function () {
        id = $(this).attr('data-id');
        $("#blogCategoryModal").modal("show");
        $("#blogCategoryModalLabel").text("Blog Kategorisi Düzenle");
        $.get("/Admin/BlogCategory/" + id).done((res) => {
            txtBlogCategoryName.value(res.name);
            chckIsActive.value(res.isActive);
        });
    });

    $(document).on('click', ".blogCategoryDelete", function () {
        id = $(this).attr('data-id');
        if (confirm("Silmek istediğinize emin misiniz?")) {
            $.ajax({
                url: "/Admin/BlogCategory/" + id,
                dataType: "json",
                type: "DELETE"
            }).done((res) => {
                dataSource.read();
                successNotification("İşlem Başarılı!", "Silme işlemi başarıyla gerçekleşti.");
            }).fail((err) => {
                errorNotification("İşlem Başarısız", err.responseJSON.message);
            });
        }
    });

    var validator = $("#formBlogCategory").kendoValidator().data("kendoValidator");

    $("#btnSave").click((event) => {
        event.preventDefault();
        if (validator.validate()) {
            var data = {
                id: id,
                name: txtBlogCategoryName.value(),
                isActive: chckIsActive.value()
            };

            if (id == 0) {
                $.ajax({
                    url: "/Admin/BlogCategory",
                    dataType: "json",
                    type: "POST",
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8"
                }).done((res) => {
                    dataSource.read();
                    $("#blogCategoryModal").modal("hide");
                    successNotification("İşlem Başarılı!", "Kaydetme işlemi başarıyla gerçekleşti.");
                }).fail((err) => {
                    errorNotification("İşlem Başarısız", err.responseJSON.message);
                });
            } else {
                $.ajax({
                    url: "/Admin/BlogCategory",
                    dataType: "json",
                    type: "PUT",
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8"
                }).done((res) => {
                    dataSource.read();
                    $("#blogCategoryModal").modal("hide");
                    successNotification("İşlem Başarılı!", "Güncelleme işlemi başarıyla gerçekleşti.");
                }).fail((err) => {
                    errorNotification("İşlem Başarısız", err.responseJSON.message);
                });
            }
        }
    });

});