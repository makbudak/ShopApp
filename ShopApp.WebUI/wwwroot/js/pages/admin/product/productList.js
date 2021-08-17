app.controller("productController", function ($scope, $http) {
    $scope.dataSource = [];

    getAll();

    function getAll() {
        $scope.dataSource = {
            transport: {
                read: {
                    url: "/admin/product/list",
                    dataType: "json"
                },
            },
            pageSize: 5
        };
    }


    $scope.deleteProduct = function (e) {
        if (confirm("Ürünü silmek istediğinize emin misiniz?")) {

        }
    }

});