$(() => {
    var id = 0;
    var todoCategoryDataSource;

    var txtTitle = $("#txtTitle").kendoTextBox({
        placeholder: "Başlık giriniz."
    }).data("kendoTextBox");

    var txtShortDescription = $("#txtShortDescription").kendoTextArea({
        rows: 2,
        maxLength: 500,
        placeholder: "Kısa açıklama giriniz."
    });

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

    var cmbTodoCategory = $("#cmbTodoCategories").kendoDropDownList({
        optionLabel: "Kategori seçiniz.",
        dataTextField: "name",
        dataValueField: "id",
        dataSource: todoCategoryDataSource
    }).data("kendoDropDownList");

    var txtUrl = $("#txtUrl").kendoTextBox().data("kendoTextBox");

    var txtDisplayOrder = $("#txtDisplayOrder").kendoNumericTextBox();

    var chckIsActive = $("#chckIsActive").kendoCheckBox({
        checked: true,
        label: "Aktif"
    }).data("kendoCheckBox");

    var chckPublished = $("#chckPublished").kendoCheckBox({
        checked: true,
        label: "Yayında mı?"
    }).data("kendoCheckBox");

    $("#btnAdd").click((e) => {
        e.preventDefault();
        $("#modalBlog").modal("show");
        $("#modalBlogLabel").text("Blog Ekle");
    });


});