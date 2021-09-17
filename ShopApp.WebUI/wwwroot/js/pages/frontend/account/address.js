const app = {
    data() {
        return {
            addresses: [],
            cities: [],
            districts: [],
            neighborhoods: [],
            cardTitle: "Adreslerim",
            showGrid: true,
            showForm: false,
            showEmpty: false,
            addressData: {
                id: 0,
                title: "",
                nameSurname: "",
                postCode: "",
                address: "",
                phone: "",
                cityId: null,
                districtId: null,
                neighborhoodId: null
            },
            rules:
            {
                title: [
                    {
                        required: true,
                        message: "Adres Başlığı zorunludur.",
                        trigger: "blur",
                    },
                ],
                nameSurname: [
                    {
                        required: true,
                        message: "Adı Soyadı zorunludur.",
                        trigger: "blur",
                    },
                ],
                address: [
                    {
                        required: true,
                        message: "Adres zorunludur.",
                        trigger: "blur",
                    },
                ],
                phone: [
                    {
                        required: true,
                        message: "Telefon zorunludur.",
                        trigger: "blur",
                    },
                ],
                cityId: [
                    {
                        required: true,
                        message: "İl seçmek zorunludur.",
                        trigger: "blur",
                    },
                ],
                districtId: [
                    {
                        required: true,
                        message: "İlçe seçmek zorunludur.",
                        trigger: "blur",
                    },
                ],
                neighborhoodId: [
                    {
                        required: true,
                        message: "Mahalle/Köy seçmek zorunludur.",
                        trigger: "blur",
                    },
                ],
            }
        }
    },
    created() {
        this.getAll();
    },
    methods: {
        getAll() {
            axios.get("/customer-address")
                .then((res) => {
                    if (res.data.length > 0) {
                        this.allHideContent();
                        this.showGrid = true;
                    } else {
                        this.allHideContent();
                        this.showEmpty = true;
                    }
                    this.cardTitle = "Adreslerim";
                    this.addresses = res.data;
                });
        },
        getCities() {
            axios.get(`/lookup/cities`)
                .then(res => {
                    this.cities = res.data;
                });
        },
        getDistricts(cityId) {
            axios.get(`/lookup/districts/${cityId}`)
                .then(res => {
                    this.districts = res.data;
                });
        },
        getNeighborhoods(districtId) {
            axios.get(`/lookup/neighborhoods/${districtId}`)
                .then(res => {
                    this.neighborhoods = res.data;
                });
        },
        allHideContent() {
            this.showForm = false;
            this.showGrid = false;
            this.showEmpty = false;
        },
        selectCity() {
            this.getDistricts(this.addressData.cityId);
        },
        selectDistrict() {
            this.getNeighborhoods(this.addressData.districtId);
        },
        addAddress() {
            this.allHideContent();
            this.showForm = true;
            this.cardTitle = "Adres Ekle";
            this.getCities();
            this.addressData = {
                id: 0,
                title: "",
                nameSurname: "",
                postCode: "",
                address: "",
                phone: "",
                cityId: null,
                districtId: null,
                neighborhoodId: null
            };
        },
        editAddress(e) {
            this.allHideContent();
            this.showForm = true;
            this.cardTitle = "Adres Düzenle";
            this.getCities();
            this.getDistricts(e.cityId);
            this.getNeighborhoods(e.districtId);
            this.addressData = e;
        },
        cancel() {
            this.allHideContent();
            this.getAll();
        },
        deleteAddress(e) {
            if (confirm("Adresi silmek istediğinize emin misiniz?")) {
                axios.delete(`/customer-address/${e.id}`)
                    .then((res) => {
                        this.getAll();
                        this.$message({
                            type: "success",
                            message: "Adres silme işlemi başarıyla gerçekleşti."
                        });
                    });
            }
        },
        onSubmit(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    if (this.addressData.id == 0) {
                        axios.post("/customer-address", this.addressData)
                            .then(res => {
                                this.cancel();
                                this.$message({
                                    type: "success",
                                    message: "Adres ekleme işlemi başarıyla gerçekleşti."
                                });
                            });
                    } else {
                        axios.put("/customer-address", this.addressData)
                            .then(res => {
                                this.cancel();
                                this.$message({
                                    type: "success",
                                    message: "Adres güncelleme işlemi başarıyla gerçekleşti."
                                });
                            });
                    }
                }
            });
        },
    }
}