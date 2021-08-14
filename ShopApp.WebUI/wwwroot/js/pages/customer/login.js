app.controller("loginController", function ($scope, $http) {
    var validator = $("#frmLogin").kendoValidator().data("kendoValidator");

    $("#frmLogin").submit(function (event) {
        event.preventDefault();
        if (validator.validate()) {
            $http.post("/customer/login", $scope.customer)
                .then((res) => {
                    window.location.href = "/";
                }, (err) => {
                    console.log(err);
                    alertShow("İşlem Başarısız", err.data.message, "danger");
                });
        }
    });
});