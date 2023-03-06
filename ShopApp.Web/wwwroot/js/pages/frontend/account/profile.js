$(() => {
    var txtEmailAddress = $("#txtEmailAddress").kendoTextBox({
        enable: false
    }).data("kendoTextBox");

    var txtName = $("#txtName").kendoTextBox().data("kendoTextBox");

    var txtSurname = $("#txtSurname").kendoTextBox().data("kendoTextBox");

    var txtPhone = $("#txtPhone").kendoMaskedTextBox({
        mask: "(999) 000-0000"
    }).data("kendoMaskedTextBox");

    getProfile();

    function getProfile() {
        $.get("/user/profile", (res => {
            txtEmailAddress.value(res.emailAddress);
            txtName.value(res.name);
            txtSurname.value(res.surname);
            txtPhone.value(res.phone);
        }));
    }

    var validator = $("#loginForm").kendoValidator().data("kendoValidator");

    $("#btnProfile").click((event) => {
        event.preventDefault();

        if (validator.validate()) {
            var data = {
                emailAddress: txtEmailAddress.value(),
                name: txtName.value(),
                surname: txtSurname.value(),
                phone: txtPhone.value()
            };
            axios.put("/user/update-profile", data)
                .then(res => {
                    successNotification("İşlem Başarılı!", "Kaydetme işlemi başarıyla gerçekleşti.");
                }, (err) => {
                    errorNotification("İşlem Başarısız", err.response.data.message);
                });
        }
    });
});