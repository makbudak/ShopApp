$(() => {
    var txtOldPassword = $("#txtOldPassword").kendoTextBox().data("kendoTextBox");

    var txtNewPassword = $("#txtNewPassword").kendoTextBox().data("kendoTextBox");

    var txtReNewPassword = $("#txtReNewPassword").kendoTextBox().data("kendoTextBox");

    var validator = $("#changePasswordForm").kendoValidator().data("kendoValidator");

    $("#btnChangePassword").click((event) => {
        event.preventDefault();

        if (validator.validate()) {
            var data = {
                oldPassword: txtOldPassword.value(),
                newPassword: txtNewPassword.value(),
                reNewPassword: txtReNewPassword.value()
            };

            axios.put("/user/change-password", data)
                .then(res => {
                    successNotification("İşlem Başarılı!", "Şifre güncelleme işlemi başarıyla gerçekleşti.");
                    $("#changePasswordForm").trigger("reset");
                }, (err) => {
                    errorNotification("İşlem Başarısız", err.response.data.message);
                });
        }
    });
});