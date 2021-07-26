function alertShow(title, content) {
    $("<div></div>").kendoAlert({
        title: title,
        content: "<p style='padding:10px;'>" + content + "</p>",
        messages: {
            okText: "Tamam"
        }
    }).data("kendoAlert").open();
}