var app = angular.module("myApp", ["kendo.directives"]);

function alertShow(title, message, type) {
    var content = "";
    if (type == "success") {
        content = "<div class='text-center w-100'><i class='k-icon k-i-success k-icon-xl k-color-success pb-3'></i><p style='width:300px;'>" + message + "</p></div>";
    } else if (type == "info") {
        content = "<div class='text-center w-100'><p class='k-icon k-i-info k-icon-xl k-color-info'></p><p style='width:300px;'>" + message + "</p></div>";
    } else if (type == "danger") {
        content = "<div class='text-center w-100'><p class='k-icon k-i-error k-icon-xl k-color-error'></p><p style='width:300px;'>" + message + "</p></div>";
    } else if (type == "warning") {
        content = "<div class='text-center w-100'><p class='k-icon k-i-warning k-icon-xl k-color-warning'></p><p style='width:300px;'>" + message + "</p></div>";
    }

    $("<div></div>").kendoAlert({
        title: title,
        content: content,
        messages: {
            okText: "Tamam"
        }
    }).data("kendoAlert").open();
}

app.controller("navbarController", function ($scope, $http) {

});