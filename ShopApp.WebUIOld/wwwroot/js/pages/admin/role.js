var dataSource = new kendo.data.DataSource({
    transport: {
        read: {
            url: "/api/role",
            dataType: "json"
        },
    },
    pageSize: 5
});

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
    ],
    dataSource: dataSource,
    loaderType: "loadingPanel",
    pageable: {
        pageSize: 5,
        alwaysVisible: true
    }
});

function editRole(e) {

}

function deleteRole(e) {

}