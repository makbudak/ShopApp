$(() => {
    var txtEmailAddress = $("#txtEmailAddress").kendoTextBox().data("kendoTextBox");
    var txtPassword = $("#txtPassword").kendoTextBox().data("kendoTextBox");

    var validator = $("#loginForm").kendoValidator().data("kendoValidator");

    $("#btnLogin").click((event) => {
        event.preventDefault();

        if (validator.validate()) {

            var data = {
                email: txtEmailAddress.value(),
                password: txtPassword.value()
            };

            $.ajax({
                url: "/user/login",
                dataType: "json",
                type: "POST",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8"
            }).done((res) => {
                location.href = "/";
            }).fail((err) => {
                errorNotification("İşlem Başarısız", err.responseJSON.message);
            });
        }
    });
});
