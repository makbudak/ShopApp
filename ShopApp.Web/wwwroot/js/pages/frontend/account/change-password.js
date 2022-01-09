$(function () {
    $("#txtOldPassword").kendoTextBox();
    $("#txtNewPassword").kendoTextBox();
    $("#txtReNewPassword").kendoTextBox();

    var txtOldPassword = $("#txtOldPassword").data("kendoTextBox");
    var txtNewPassword = $("#txtNewPassword").data("kendoTextBox");
    var txtReNewPassword = $("#txtReNewPassword").data("kendoTextBox");

    var validator = $("#changePasswordForm").kendoValidator().data("kendoValidator");

    $("#btnChangePassword").click((event) => {
        event.preventDefault();

        if (validator.validate()) {
            var data = {
                oldPassword: txtOldPassword.value(),
                newPassword: txtNewPassword.value(),
                reNewPassword: txtReNewPassword.value()
            };

            axios.put("/customer/change-password", data)
                .then(res => {
                    successNotification("İşlem Başarılı!", "Şifre güncelleme işlemi başarıyla gerçekleşti.");
                    $("#changePasswordForm").trigger("reset");
                }, (err) => {
                    errorNotification("İşlem Başarısız", err.response.data.message);
                });
        }
    });
});

