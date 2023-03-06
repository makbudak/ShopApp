$(() => {
    var id = 0;
    var todoCategoryDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Admin/TodoCategory/List",
                dataType: "json"
            },
        }
    });

    var userDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/Admin/Lookup/GetAdmins",
                dataType: "json"
            },
        }
    });

    function selectTodoCategory(e) {
        var todoStatusDataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: `/Admin/TodoStatus/GetByCategory/${cmbTodoCategory.value()}`,
                    dataType: "json"
                }
            }
        });
        cmbTodoStatuses.setDataSource(todoStatusDataSource);
    };

    var cmbTodoCategory = $("#cmbTodoCategories").kendoDropDownList({
        optionLabel: "Kategori seçiniz.",
        dataTextField: "name",
        dataValueField: "id",
        dataSource: todoCategoryDataSource,
        select: selectTodoCategory
    }).data("kendoDropDownList");

    var cmbTodoStatuses = $("#cmbTodoStatuses").kendoDropDownList({
        optionLabel: "Durum seçiniz.",
        dataTextField: "name",
        dataValueField: "id"
    }).data("kendoDropDownList");

    var cmbUsers = $("#cmbUsers").kendoDropDownList({
        optionLabel: "Kullanıcı seçiniz.",
        dataTextField: "name",
        dataValueField: "id",
        dataSource: userDataSource
    }).data("kendoDropDownList");

    var txtTitle = $("#txtTitle").kendoTextBox({
        placeholder: "Başlık giriniz."
    }).data("kendoTextBox");

    var chckIsActive = $("#chckIsActive").kendoCheckBox({
        checked: true,
        label: "Aktif"
    }).data("kendoCheckBox");

    var editorDescription = $("#editorDescription").kendoEditor({        
        tools: [
            "bold",
            "italic",
            "underline",
            "undo",
            "redo",
            "justifyLeft",
            "justifyCenter",
            "justifyRight",
            "insertUnorderedList",
            "createLink",
            "unlink",
            "insertImage",
            "tableWizard",
            "createTable",
            "addRowAbove",
            "addRowBelow",
            "addColumnLeft",
            "addColumnRight",
            "deleteRow",
            "deleteColumn",
            "mergeCellsHorizontally",
            "mergeCellsVertically",
            "splitCellHorizontally",
            "splitCellVertically",
            "tableAlignLeft",
            "tableAlignCenter",
            "tableAlignRight",
            "formatting",
            {
                name: "fontName",
                items: [
                    { text: "Andale Mono", value: "\"Andale Mono\"" },
                    { text: "Arial", value: "Arial" },
                    { text: "Arial Black", value: "\"Arial Black\"" },
                    { text: "Book Antiqua", value: "\"Book Antiqua\"" },
                    { text: "Comic Sans MS", value: "\"Comic Sans MS\"" },
                    { text: "Courier New", value: "\"Courier New\"" },
                    { text: "Georgia", value: "Georgia" },
                    { text: "Helvetica", value: "Helvetica" },
                    { text: "Impact", value: "Impact" },
                    { text: "Symbol", value: "Symbol" },
                    { text: "Tahoma", value: "Tahoma" },
                    { text: "Terminal", value: "Terminal" },
                    { text: "Times New Roman", value: "\"Times New Roman\"" },
                    { text: "Trebuchet MS", value: "\"Trebuchet MS\"" },
                    { text: "Verdana", value: "Verdana" },
                ]
            },
            "fontSize",
            "foreColor",
            "backColor",
        ]
    });

    $("#btnAdd").click((e) => {
        e.preventDefault();
        $("#modalTodo").modal("show");
        $("#modalTodoLabel").text("Yapılacak Ekle");
    });

});