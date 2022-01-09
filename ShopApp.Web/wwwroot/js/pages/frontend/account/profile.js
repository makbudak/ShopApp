$(function () {
    $("#txtEmailAddress").kendoTextBox({
        enable: false
    });
    $("#txtName").kendoTextBox();
    $("#txtSurname").kendoTextBox();
    $("#txtPhone").kendoMaskedTextBox({
        mask: "(999) 000-0000"
    });

    var txtEmailAddress = $("#txtEmailAddress").data("kendoTextBox");
    var txtName = $("#txtName").data("kendoTextBox");
    var txtSurname = $("#txtSurname").data("kendoTextBox");
    var txtPhone = $("#txtPhone").data("kendoMaskedTextBox");

    getProfile();

    function getProfile() {
        axios.get("/customer/profile")
            .then(res => {
                txtEmailAddress.value(res.data.emailAddress);
                txtName.value(res.data.name);
                txtSurname.value(res.data.surname);
                txtPhone.value(res.data.phone);
            });
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
            axios.put("/customer/update-profile", data)
                .then(res => {
                    successNotification("İşlem Başarılı!", "Kaydetme işlemi başarıyla gerçekleşti.");
                }, (err) => {
                    errorNotification("İşlem Başarısız", err.response.data.message);
                });
        }
    });

});