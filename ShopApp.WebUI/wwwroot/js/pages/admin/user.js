var dataSource = new kendo.data.DataSource({
    transport: {
        read: {
            url: "/api/user",
            dataType: "json"
        },
    },
    pageSize: 5
});

$("#grid").kendoGrid({
    columns: [
        {
            command: [
                { text: "", name: "edit", iconClass: "k-icon k-i-edit", click: editUser },
                { text: "", name: "remove", iconClass: "k-icon k-i-trash", click: deleteUser }
            ],
            title: " ",
            width: "120px"
        },
        {
            field: "userName",
            title: "Kullanıcı Adı",
        },
        {
            field: "firstName",
            title: "Adı",
        },
        {
            field: "lastName",
            title: "Soyadı",
        },
        {
            field: "email",
            title: "E-mail Adresi",
        },
        {
            title: "Email Onay",
            template: '#=dirtyField(data,"emailConfirmed")#<input type="checkbox" #= emailConfirmed ? \'checked="checked"\' : "" # class="chkbx k-checkbox" />',
            width: 120
        },
        {
            field: "phone",
            title: "Telefon No"
        }
    ],
    dataSource: dataSource,
    loaderType: "loadingPanel",
    pageable: {
        pageSize: 5,
        alwaysVisible: true
    }
});

function editUser(e) {

}

function deleteUser(e) {

}

$("#grid .k-grid-content").on("change", "input.chkbx", function (e) {
    var grid = $("#grid").data("kendoGrid");
    dataItem = grid.dataItem($(e.target).closest("tr"));
    dataItem.set("emailConfirmed", this.checked);
});

function dirtyField(data, fieldName) {
    var hasClass = $("[data-uid=" + data.id + "]").find(".k-dirty-cell").length < 1;
    if (data.dirty && data.dirtyFields[fieldName] && hasClass) {
        return "<span class='k-dirty'></span>"
    }
    else {
        return "";
    }
}