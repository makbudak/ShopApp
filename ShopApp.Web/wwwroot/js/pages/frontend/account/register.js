$(function () {
    var txtName = $("#txtName").kendoTextBox().data("kendoTextBox");
    var txtSurname = $("#txtSurname").kendoTextBox().data("kendoTextBox");
    var txtEmailAddress = $("#txtEmailAddress").kendoTextBox().data("kendoTextBox");
    var txtPhone = $("#txtPhone").kendoMaskedTextBox({
        mask: "(999) 000-0000"
    }).data("kendoMaskedTextBox");
    var txtPassword = $("#txtPassword").kendoTextBox().data("kendoTextBox");
    var txtRePassword = $("#txtRePassword").kendoTextBox().data("kendoTextBox");
   
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

            axios.post("/user/register", data)
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
