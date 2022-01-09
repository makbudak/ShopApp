$(function () {
    $("#txtEmailAddress").kendoTextBox();
    $("#txtPassword").kendoTextBox();

    var txtEmailAddress = $("#txtEmailAddress").data("kendoTextBox");
    var txtPassword = $("#txtPassword").data("kendoTextBox");

    var validator = $("#loginForm").kendoValidator().data("kendoValidator");

    $("#btnLogin").click((event) => {
        event.preventDefault();

        if (validator.validate()) {

            var data = {
                email: txtEmailAddress.value(),
                password: txtPassword.value()
            };

            axios.post("/customer/login", data).then((res) => {
                location.href = "/";
            }, (err) => {
                errorNotification("İşlem Başarısız", err.response.data.message);
            });
        }
    });
});
