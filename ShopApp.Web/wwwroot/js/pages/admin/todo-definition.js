$(() => {
    var type = 1;
    var categoryId = 0;
    var statusId = 0;

    $("#tabTodo").kendoTabStrip({
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    });

    $("#tabTodo").removeClass("d-none");

    var todoCategoryDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Admin/TodoCategory/List",
                dataType: "json"
            },
        },
        pageSize: 5
    });

    var todoStatusDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Admin/TodoStatus/List",
                dataType: "json"
            },
        },
        pageSize: 5,
    });

    var txtTodoCategoryName = $("#txtTodoCategoryName").kendoTextBox({
        placeholder: "Kategori adı giriniz."
    }).data("kendoTextBox");

    var txtTodoStatusName = $("#txtTodoStatusName").kendoTextBox({
        placeholder: "Durum adı giriniz."
    }).data("kendoTextBox");

    var cmbTodoCategory = $("#cmbTodoCategories").kendoDropDownList({
        optionLabel: "Kategori seçiniz.",
        dataTextField: "name",
        dataValueField: "id",
    }).data("kendoDropDownList");

    var txtDisplayOrder = $("#txtDisplayOrder").kendoNumericTextBox({
        format: "n0",
        min: 1,
        step: 1
    }).data("kendoNumericTextBox");

    $("#todoCategoryGrid").kendoGrid({
        dataSource: todoCategoryDataSource,
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
        rowTemplate: kendo.template($("#todoCategoryTemplate").html())
    });

    $("#todoStatusGrid").kendoGrid({
        dataSource: todoStatusDataSource,
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
        rowTemplate: kendo.template($("#todoStatusTemplate").html())
    });

    $("#btnAdd").click((e) => {
        if (type == 1) {
            e.preventDefault();
            $("#modalTodoCategory").modal("show");
            $("#modalTodoCategoryLabel").text("Yapılacak Kategorisi Ekle");
            $("#formTodoCategory").trigger("reset");
            categoryId = 0;
        } else {
            e.preventDefault();
            $("#modalTodoStatus").modal("show");
            $("#modalTodoStatusLabel").text("Yapılacak Durumu Ekle");
            $("#formTodoStatus").trigger("reset");
            todoCategoryDataSource.read();
            cmbTodoCategory.setDataSource(todoCategoryDataSource);
            statusId = 0;
        }
    });

    $(document).on('click', ".todoCategoryEdit", function () {
        categoryId = $(this).attr('data-id');
        $("#modalTodoCategory").modal("show");
        $("#modalTodoCategoryLabel").text("Yapılacak Kategori Düzenle");
        $.get("/Admin/TodoCategory/" + categoryId).done((res) => {
            txtTodoCategoryName.value(res.name);
        });
    });

    $(document).on('click', ".todoCategoryDelete", function () {
        id = $(this).attr('data-id');
        if (confirm("Silmek istediğinize emin misiniz?")) {
            $.ajax({
                url: "/Admin/TodoCategory/" + id,
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

    $(document).on('click', ".todoStatusEdit", function () {
        statusId = $(this).attr('data-id');
        $("#modalTodoStatus").modal("show");
        $("#modalTodoStatusLabel").text("Yapılacak Durumu Düzenle");
        $.get("/Admin/TodoStatus/" + statusId).done((res) => {
            todoCategoryDataSource.read();
            cmbTodoCategory.setDataSource(todoCategoryDataSource);
            txtTodoStatusName.value(res.name);
            cmbTodoCategory.value(res.todoCategoryId);
            txtDisplayOrder.value(res.displayOrder);
        });
    });

    $(document).on('click', ".todoStatusDelete", function () {
        statusId = $(this).attr('data-id');
        if (confirm("Silmek istediğinize emin misiniz?")) {
            $.ajax({
                url: "/Admin/TodoStatus/" + statusId,
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

    var todoCategoryValidator = $("#formTodoCategory").kendoValidator().data("kendoValidator");
    var todoStatusValidator = $("#formTodoStatus").kendoValidator().data("kendoValidator");

    $("#btnTodoCategorySave").click((e) => {
        e.preventDefault();
        if (todoCategoryValidator.validate()) {
            var data = {
                id: categoryId,
                name: txtTodoCategoryName.value()
            };

            if (categoryId == 0) {
                $.ajax({
                    url: "/Admin/TodoCategory",
                    dataType: "json",
                    type: "POST",
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8"
                }).done((res) => {
                    todoCategoryDataSource.read();
                    $("#modalTodoCategory").modal("hide");
                    successNotification("İşlem Başarılı!", "Kaydetme işlemi başarıyla gerçekleşti.");
                }).fail((err) => {
                    errorNotification("İşlem Başarısız", err.responseJSON.message);
                });
            } else {
                $.ajax({
                    url: "/Admin/TodoCategory",
                    dataType: "json",
                    type: "PUT",
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8"
                }).done((res) => {
                    todoCategoryDataSource.read();
                    $("#modalTodoCategory").modal("hide");
                    successNotification("İşlem Başarılı!", "Güncelleme işlemi başarıyla gerçekleşti.");
                }).fail((err) => {
                    errorNotification("İşlem Başarısız", err.responseJSON.message);
                });
            }
        }
    });

    $("#btnTodoStatusSave").click((e) => {
        e.preventDefault();
        if (todoStatusValidator.validate()) {
            var data = {
                id: statusId,
                todoCategoryId: cmbTodoCategory.value(),
                name: txtTodoStatusName.value(),
                displayOrder: txtDisplayOrder.value()
            };

            if (statusId == 0) {
                $.ajax({
                    url: "/Admin/TodoStatus",
                    dataType: "json",
                    type: "POST",
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8"
                }).done((res) => {
                    todoStatusDataSource.read();
                    $("#modalTodoStatus").modal("hide");
                    successNotification("İşlem Başarılı!", "Kaydetme işlemi başarıyla gerçekleşti.");
                }).fail((err) => {
                    errorNotification("İşlem Başarısız", err.responseJSON.message);
                });
            } else {
                $.ajax({
                    url: "/Admin/TodoStatus",
                    dataType: "json",
                    type: "PUT",
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8"
                }).done((res) => {
                    todoStatusDataSource.read();
                    $("#modalTodoStatus").modal("hide");
                    successNotification("İşlem Başarılı!", "Güncelleme işlemi başarıyla gerçekleşti.");
                }).fail((err) => {
                    errorNotification("İşlem Başarısız", err.responseJSON.message);
                });
            }
        }
    });

    $("#todoTab li").on("click", function () {
        type = $(this).attr("data-id");
    });
});