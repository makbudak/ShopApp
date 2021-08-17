var app = angular.module("myApp", ['dx']);
app.controller("loginController", function ($scope, $http) {

    $scope.txtEmailAddress = {
        bindingOptions: {
            value: "user.email"
        }
    }

    $scope.txtPassword = {
        mode: "password",
        bindingOptions: {
            value: "user.password",
        }
    }

    $scope.emailValidationRules = {
        validationRules: [{
            type: "required",
            message: "Email adresi zorunludur."
        },
        {
            type: "email",
            message: "Email adresi formatı uygun değil."
        }]
    }

    $scope.passwordValidationRules = {
        validationRules: [{
            type: "required",
            message: "Şifre zorunludur."
        }]
    };

    $scope.btnLogin = {
        text: 'Giriş Yap',
        useSubmitBehavior: true
    };

    $scope.login = function (e) {
        e.preventDefault();
        $http.post("/Admin/Login", $scope.user)
            .then((res) => {
                window.location.href = "/admin/home";
            }, (err) => {
                DevExpress.ui.notify({
                    message: err.data.message,
                }, "error", 5000);
            });
    };
});