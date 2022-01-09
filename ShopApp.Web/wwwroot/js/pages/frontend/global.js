
$("#loginMenu").kendoMenu({
    direction: "bottom right"
});

$("#customerMenu").kendoMenu({
    direction: "bottom right"
});

$("#profileMenu").kendoExpansionPanel({
    title: 'Hesabım',
    expanded: true
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