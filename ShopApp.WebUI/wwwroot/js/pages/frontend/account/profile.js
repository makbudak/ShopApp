app.controller("profileController", function ($scope, $http) {

    $http.get("/customer/profile")
        .then((res) => {
            $scope.customer = res.data;
        });

    var validator = $("#frmProfile").kendoValidator().data("kendoValidator");

    $("#frmProfile").submit(function (event) {
        event.preventDefault();
        if (validator.validate()) {
            $http.put("/customer/update-profile", $scope.customer)
                .then((res) => {
                    alertShow("İşlem Başarılı", "Kullanıcı bilgileri güncelleme işlemi başarıyla gerçekleşti.", "success");
                }, (err) => {
                    alertShow("İşlem Başarısız", err.data.message, "danger");
                });
        }
    });
});