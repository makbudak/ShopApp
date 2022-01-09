var menuItems = new kendo.data.HierarchicalDataSource({
    data: [{
        text: "İçerik Yönetimi",
        items: [
            {
                text: "Ürün Kategorileri",
                url: "/admin/product-category"
            },
            {
                text: "Ürünler",
                url: "/admin/product"
            }
        ]
    },
    {
        text: "Ayarlar",
        items: [
            {
                text: "Kullanıcılar",
                url: "/admin/user"
            },
            {
                text: "Roller",
                url: "/admin/role"
            }
        ]
    }]

});

function adjustSize() {
    if (screen.width < 800 || screen.height < 600) {
        this.maximize();
    }
}

var notification = $("#notification").kendoNotification({
    position: {
        pinned: true,
        top: 30,
        right: 30
    },
    autoHideAfter: 5000,
    stacking: "down",
    templates: [{
        type: "error",
        template: $("#errorTemplate").html()
    },
    {
        type: "success",
        template: $("#successTemplate").html()
    }]
}).data("kendoNotification");

function successNotification(title, message) {
    notification.show({
        title: title,
        message: message
    }, "success");
}

function errorNotification(title, message) {
    notification.show({
        title: title,
        message: message
    }, "error");
}

var leftMenu = $("#drawer-left-menu").kendoDrawer({
    template: "<div style='width:350px;' class='p-2'><div class='row w-100'><h4 class='col-10'>Menü</h4><div class='col-2 float-end'><span id='btnDrawerClose'><i class='k-icon k-i-close pt-2'></i><span></div></div><div class='pt-2' id='tree-left-menu'></div>",
    position: "left",
    mode: "overlay",
    width: 400,
    swipeToOpen: false,
    navigatable: true,
}).data().kendoDrawer;

$("#btnMenu").click((e) => {
    e.preventDefault();
    leftMenu.show();
});

$("#tree-left-menu").kendoTreeView({
    dataSource: menuItems
});

$("#btnDrawerClose").click((e) => {
    leftMenu.hide();
});