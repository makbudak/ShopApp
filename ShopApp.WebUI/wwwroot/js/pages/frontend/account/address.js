app.controller("addressController", function ($scope, $http) {
    $scope.addresses = [];
    $scope.cities = [];
    $scope.districts = [];
    $scope.neighborhoods = [];
    $scope.address = {
        id: 0,
        title: "",
        nameSurname: "",
        postCode: "",
        address: "",
        phone: "",
        cityId: 0,
        districtId: 0,
        neighborhoodId: 0
    }

    allHideContent();
    getAll();

    function getAll() {
        $http.get("/customer-address")
            .then((res) => {
                if (res.data.length > 0) {
                    allHideContent();
                    $scope.showGrid = true;
                } else {
                    allHideContent();
                    $scope.showEmpty = true;
                }
                $scope.cardTitle = "Adreslerim";
                $scope.addresses = res.data;
            });
    }

    function getCities() {
        $scope.cities = {
            transport: {
                read: {
                    url: "/lookup/cities",
                }
            }
        };
    }

    function getDistricts(cityId) {
        $scope.districts = {
            transport: {
                read: {
                    url: "/lookup/districts/" + cityId,
                }
            }
        };
    }

    function getNeighborhoods(districtId) {
        $scope.neighborhoods = {
            transport: {
                read: {
                    url: "/lookup/neighborhoods/" + districtId,
                }
            }
        };
    }

    function allHideContent() {
        $scope.showForm = false;
        $scope.showGrid = false;
        $scope.showEmpty = false;
    }

    $scope.selectCity = function () {
        getDistricts($scope.address.cityId);
    }

    $scope.selectDistrict = function () {
        getNeighborhoods($scope.address.districtId);
    }

    $scope.add = function () {
        allHideContent();
        $scope.showForm = true;
        $scope.cardTitle = "Adres Ekle";
        getCities();
        $scope.address = {
            id: 0,
            title: "",
            nameSurname: "",
            postCode: "",
            address: "",
            phone: "",
            cityId: 0,
            districtId: 0,
            neighborhoodId: 0
        }
    };

    $scope.edit = function (e) {
        allHideContent();
        $scope.showForm = true;
        $scope.cardTitle = "Adres Düzenle";
        getCities();
        getDistricts(e.cityId);
        getNeighborhoods(e.districtId);
        $scope.address = e;
    }

    $scope.cancel = function () {
        allHideContent();
        getAll();
    }

    $scope.delete = function (e) {
        if (confirm("Adresi silmek istediğinize emin misiniz?")) {
            $http.delete("/customer-address/" + e.id)
                .then((res) => {
                    getAll();
                    alertShow("İşlem Başarılı", "Adres silme işlemi başarıyla gerçekleşti.");
                });
        }
    }

    var validator = $("#frmAddress").kendoValidator().data("kendoValidator");

    $("#frmAddress").submit(function (event) {
        event.preventDefault();
        if (validator.validate()) {
            if ($scope.address.id == 0) {
                $http.post("/customer-address", $scope.address)
                    .then((res) => {
                        getAll();
                        alertShow("İşlem Başarılı", "Yeni adres ekleme işlemi başarıyla gerçekleşti.");
                    });
            } else {
                $http.put("/customer-address", $scope.address)
                    .then((res) => {
                        getAll();
                        alertShow("İşlem Başarılı", "Adres güncelleme işlemi başarıyla gerçekleşti.");
                    });
            }
        }
    });
});