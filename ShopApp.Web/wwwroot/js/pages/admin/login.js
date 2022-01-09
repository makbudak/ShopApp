$(function () {
    $("#txtEmailAddress").kendoTextBox();
    $("#txtPassword").kendoTextBox();

    var txtEmailAddress = $("#txtEmailAddress").data("kendoTextBox");
    var txtPassword = $("#txtPassword").data("kendoTextBox");

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

    var validator = $("#loginForm").kendoValidator().data("kendoValidator");

    $("#btnLogin").click((event) => {
        event.preventDefault();

        if (validator.validate()) {

            var data = {
                email: txtEmailAddress.value(),
                password: txtPassword.value()
            };

            axios.post("/admin/login", data).then((res) => {
                notification.show({
                    title: "Hoşgeldiniz!",
                    message: res.data.data.nameSurname
                }, "success");

                setTimeout(() => {
                    location.href = res.data.data.returnUrl;
                }, 500);
            });           
        }
    });
});
