app.controller("changePasswordController", function ($scope, $http) {

    var validator = $("#frmChangePassword").kendoValidator().data("kendoValidator");

    $("#frmChangePassword").submit(function (event) {
        event.preventDefault();
        if (validator.validate()) {
            $http.put("/customer/change-password", $scope.customer)
                .then((res) => {
                    alertShow("İşlem Başarılı", "Şifre güncelleme işlemi başarıyla gerçekleşti.", "success");
                    $scope.customer = null;
                }, (err) => {
                    alertShow("İşlem Başarısız", err.data.message, "danger");
                });
        }
    });
});