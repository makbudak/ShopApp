var id = $("#productId").val();

var categoryDataSource = new kendo.data.DataSource({
    transport: {
        read: {
            url: "/api/category",
            dataType: "json"
        },
    }
});

$("#cmbCategories").kendoMultiSelect({
    placeholder: "Kategori seçiniz",
    dataTextField: "name",
    dataValueField: "categoryId",
    dataSource: categoryDataSource
});

$("#txtName").kendoTextBox({
    placeholder: "Ürün Adı",
});

$("#txtUrl").kendoTextBox({
    placeholder: "URL",
});

$("#txtPrice").kendoNumericTextBox({
    placeholder: "Fiyatı",
    format: "c",
    culture: "tr-TR"
});


$("#txtDescription").kendoEditor({
    tools: [
        "bold",
        "italic",
        "underline",
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
                { text: "Andale Mono", value: "Andale Mono" },
                { text: "Arial", value: "Arial" },
                { text: "Arial Black", value: "Arial Black" },
                { text: "Book Antiqua", value: "Book Antiqua" },
                { text: "Comic Sans MS", value: "Comic Sans MS" },
                { text: "Courier New", value: "Courier New" },
                { text: "Georgia", value: "Georgia" },
                { text: "Helvetica", value: "Helvetica" },
                { text: "Impact", value: "Impact" },
                { text: "Symbol", value: "Symbol" },
                { text: "Tahoma", value: "Tahoma" },
                { text: "Terminal", value: "Terminal" },
                { text: "Times New Roman", value: "Times New Roman" },
                { text: "Trebuchet MS", value: "Trebuchet MS" },
                { text: "Verdana", value: "Verdana" },
            ]
        },
        "fontSize",
        "foreColor",
        "backColor",
    ]
});


$.ajax({
    url: "/api/product/" + id,
    type: "GET",
    complete: function (res) {
        var data = res.responseJSON;
        $("#txtName").data("kendoTextBox").value(data.name);
        $("#txtUrl").data("kendoTextBox").value(data.url);
        $("#cmbCategories").data("kendoMultiSelect").value(data.productCategories);
        $("#txtDescription").data("kendoEditor").value(data.description);
        $("#txtPrice").data("kendoNumericTextBox").value(data.price);
    }
});
