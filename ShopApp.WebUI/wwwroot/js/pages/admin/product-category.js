app.controller("productCategoryController", function ($scope, $http) {
    var categoryModal = new bootstrap.Modal($("#categoryModal"));
    $scope.title = "";
    $scope.dataSource = getAll();
    reset();


    function getAll() {
        //$http.get("/admin/product-category/list")
        //    .then(res => {
        //        $scope.dataSource = res.data;
        //    });
        var data = new kendo.data.HierarchicalDataSource({
            transport: {
                read: {
                    url: "/admin/product-category/list",
                    dataType: "json"
                },
            },
            schema: {
                model: {
                    children: "items"
                }
            }
        });
        return data;
    }

    $scope.add = function () {
        $scope.title = "Yeni Kategori Ekle";
        categoryModal.show();
        reset();
    }

    $scope.edit = function (e) {
        //categoryModal.show();
        $scope.title = "Kategori Düzenle";
        $scope.categoryDataSource = getAll();
        console.log(e.parentId);
        debugger;
        if (e.parentId == null) {
            $scope.parentId = 0;
        } else {
            $scope.parentId = e.parentId;
        }

        //$http.get("/admin/product-category/list/" + e.id)
        //    .then(res => {
        //        $scope.productCategory = {
        //            id: res.data.id,
        //            name: res.data.name,
        //            url: res.data.url,
        //            parentId: res.data.parentId
        //        };
        //    });
    }

    $scope.delete = function (e) {
        kendo.confirm("Silme istediğinize emin misiniz?").then(function () {
            $http.delete("/admin/product-category/delete/" + e.id)
                .then(res => {
                    getAll();
                });
        });
    }

    var validator = $("#frmCategory").kendoValidator().data("kendoValidator");

    $("#frmCategory").submit(function (event) {
        if (validator.validate()) {
            if ($scope.productCategory.id === 0) {
                $http.post("/admin/product-category/create", $scope.productCategory)
                    .then(res => {
                        getAll();
                        categoryModal.hide();
                    });
            } else {
                $http.put("/admin/product-category/update", $scope.productCategory)
                    .then(res => {
                        getAll();
                        categoryModal.hide();
                    });
            }
        };
    });

    function reset() {
        $scope.productCategory = {
            id: 0,
            name: "",
            url: "",
            parentId: null
        };
    }
});