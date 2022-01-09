$(function () {
    $("#txtName").kendoTextBox();
    $("#txtSurname").kendoTextBox();
    $("#txtEmailAddress").kendoTextBox();
    $("#txtPhone").kendoMaskedTextBox({
        mask: "(999) 000-0000"
    });
    $("#txtPassword").kendoTextBox();
    $("#txtRePassword").kendoTextBox();

    var txtName = $("#txtName").data("kendoTextBox");
    var txtSurname = $("#txtSurname").data("kendoTextBox");
    var txtEmailAddress = $("#txtEmailAddress").data("kendoTextBox");
    var txtPhone = $("#txtPhone").data("kendoMaskedTextBox");
    var txtPassword = $("#txtPassword").data("kendoTextBox");
    var txtRePassword = $("#txtRePassword").data("kendoTextBox");

    var validator = $("#registerForm").kendoValidator().data("kendoValidator");

    $("#btnRegister").click((event) => {
        event.preventDefault();

        if (validator.validate()) {

            var data = {
                name: txtName.value(),
                surname: txtSurname.value(),
                email: txtEmailAddress.value(),
                phone: txtPhone.value(),
                password: txtPassword.value(),
                rePassword: txtRePassword.value()
            };

            axios.post("/customer/register", data)
                .then((res) => {
                    successNotification("İşlem Başarılı", "Kaydetme işlemi başarılıyla gerçekleşti.");

                    setTimeout(() => {
                        location.href = "/account/login";
                    }, 1000);
                }, (err) => {
                    errorNotification("İşlem Başarısız", err.response.data.message);
                });
        }
    });
});
