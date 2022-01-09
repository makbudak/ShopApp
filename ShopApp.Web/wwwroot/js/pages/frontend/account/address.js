$(function () {
    var id = 0;

    var template = kendo.template($("#template").html());

    var customerAddressDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/customer-address",
                dataType: "json"
            }
        },
        change: function (e) {
            if (this.data().length > 0) {
                $("#grid").addClass("d-block").removeClass("d-none");
                $("#noData").addClass("d-none").removeClass("d-block");
                $("#gridAddress").html(kendo.render(template, this.view()));
            } else {
                $("#grid").addClass("d-none").removeClass("d-block");
                $("#noData").addClass("d-block").removeClass("d-none");
            }
        }
    });

    customerAddressDataSource.read();


    var cityDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/lookup/cities",
                dataType: "json"
            }
        }
    });

    var txtTitle = $("#txtTitle").kendoTextBox({
        placeholder: "Adres başlığı giriniz."
    }).data("kendoTextBox");

    var txtNameSurname = $("#txtNameSurname").kendoTextBox({
        placeholder: "Adı Soyadı giriniz."
    }).data("kendoTextBox");

    var txtPhone = $("#txtPhone").kendoMaskedTextBox({
        placeholder: "Telefon giriniz.",
        mask: "(999) 000-0000"
    }).data("kendoMaskedTextBox");

    var cmbCity = $("#cmbCity").kendoComboBox({
        filter: "contains",
        placeholder: "İl seçiniz.",
        dataTextField: "name",
        dataValueField: "id",
        dataSource: cityDataSource,
        select: selectCity,
    }).data("kendoComboBox");

    var cmbDistrict = $("#cmbDistrict").kendoComboBox({
        filter: "contains",
        placeholder: "İlçe seçiniz.",
        dataTextField: "name",
        dataValueField: "id",
        select: selectDistrict,
        autoBind: false,
    }).data("kendoComboBox");

    var cmbNeighborhood = $("#cmbNeighborhood").kendoComboBox({
        filter: "contains",
        placeholder: "Mahalle/Köy seçiniz.",
        dataTextField: "name",
        dataValueField: "id",
    }).data("kendoComboBox");

    var txtPostCode = $("#txtPostCode").kendoTextBox({
        placeholder: "Posta kodu giriniz."
    }).data("kendoTextBox");

    var txtAddress = $("#txtAddress").kendoTextArea({
        rows: 5,
        maxLength: 500,
        placeholder: "Adres giriniz."
    }).data("kendoTextArea");

    var windowAddress = $("#windowAddress").kendoWindow({
        width: "600px",
        height: "95%",
        modal: true,
        visible: false,
        animation: false,
        open: adjustSize,
        scrollable: true
    }).data("kendoWindow");

    function selectCity(e) {
        var districtDataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: `/lookup/districts/${cmbCity.value()}`,
                    dataType: "json"
                }
            }
        });
        cmbDistrict.setDataSource(districtDataSource);
    };

    function selectDistrict(e) {
        var neighborhoodDataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: `/lookup/neighborhoods/${cmbDistrict.value()}`,
                    dataType: "json"
                }
            }
        });
        cmbNeighborhood.setDataSource(neighborhoodDataSource);
    };

    $("#btnAddAddress").click((event) => {
        $("#addressForm").trigger("reset");
        id = 0;
        event.preventDefault();
        windowAddress.center().open();
        $(".k-window-title").text("Adres Ekle");
    });

    $("#btnClose").click(() => {
        windowAddress.close();
    })

    $(document).on('click', ".btnEditAddress", function () {
        id = $(this).attr('data-id');
        axios.get(`/customer-address/${id}`)
            .then(res => {
                windowAddress.center().open();
                $(".k-window-title").text("Adres Düzenle");
                txtTitle.value(res.data.title);
                txtNameSurname.value(res.data.nameSurname);
                txtPostCode.value(res.data.postCode);
                txtAddress.value(res.data.address);
                txtPhone.value(res.data.phone);
                cmbCity.value(res.data.cityId);
                selectCity(res.data.cityId);
                cmbDistrict.value(res.data.districtId);
                selectDistrict(res.data.districtId);
                cmbNeighborhood.value(res.data.neighborhoodId);
            });
    });

    $(document).on('click', ".btnDeleteAddress", function () {
        id = $(this).attr('data-id');
        if (confirm("Silmek istediğinize emin misiniz?")) {
            axios.delete(`/customer-address/${id}`)
                .then(res => {
                    customerAddressDataSource.read();
                    successNotification("İşlem Başarılı!", "Silme işlemi başarıyla gerçekleşti.");
                }, (err) => {
                    errorNotification("İşlem Başarısız", err.response.data.message);
                });
        }
    });

    var validator = $("#addressForm").kendoValidator().data("kendoValidator");

    $("#btnSave").click((event) => {
        event.preventDefault();
        if (validator.validate()) {
            var data = {
                id: id,
                title: txtTitle.value(),
                nameSurname: txtNameSurname.value(),
                postCode: txtPostCode.value(),
                address: txtAddress.value(),
                phone: txtPhone.value(),
                cityId: cmbCity.value(),
                districtId: cmbDistrict.value(),
                neighborhoodId: cmbNeighborhood.value()
            };

            if (id == 0) {
                axios.post("/customer-address", data)
                    .then(res => {
                        customerAddressDataSource.read();
                        windowAddress.close();
                        successNotification("İşlem Başarılı!", "Kaydetme işlemi başarıyla gerçekleşti.");
                    }, (err) => {
                        errorNotification("İşlem Başarısız", err.response.data.message);
                    });
            } else {
                axios.put("/customer-address", data)
                    .then(res => {
                        customerAddressDataSource.read();
                        windowAddress.close();
                        successNotification("İşlem Başarılı!", "Güncelleme işlemi başarıyla gerçekleşti.");
                    }, (err) => {
                        errorNotification("İşlem Başarısız", err.response.data.message);
                    });
            }
        }
    });
});