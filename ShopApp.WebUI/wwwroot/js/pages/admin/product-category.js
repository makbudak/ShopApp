const app = {
    data() {
        return {
            productCategories: [],
            showGrid: true,
            showForm: false,
            title: "Ürün Kategorileri",
            productCategory: {},
            filterText: "",
            defaultProps: {
                children: "items",
                label: "name",
            },
            rules:
            {

            }
        }
    },
    created() {
        this.getAll();
        this.reset();
    },
    methods: {
        getAll() {
            axios.get("/admin/product-category/list").then(res => {
                this.productCategories = res.data;
            });
        },
        addProductCategory() {
            this.title = "Yeni Kategori Ekle";
            this.reset();
        },
        editProductCategory(e) {
            this.title = "Kategori Düzenle";
        },
        deleteProductCategory(e) {
            this.$confirm(
                'Silmek istediğinize emin misiniz?',
                'Silme Onayı',
                {
                    confirmButtonText: 'Evet',
                    cancelButtonText: 'Hayır',
                    type: 'danger',
                }
            ).then(() => {
                axios.delete(`/admin/product-category/delete/${e.id}`)
                    .then(res => {
                        this.getAll();
                        this.$message({
                            type: 'success',
                            message: 'Silme işlemi başarıyla gerçekleşti.',
                        });
                    });
            });
        },
        onSubmit(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    if (this.productCategory.id === 0) {
                        $http.post("/admin/product-category/create", $scope.productCategory)
                            .then(res => {
                                getAll();
                            });
                    } else {
                        $http.put("/admin/product-category/update", $scope.productCategory)
                            .then(res => {
                                getAll();
                            });
                    }
                };
            });
        },
        reset() {
            this.title = "Ürün Kategorileri";
            this.showForm = false;
            this.showGrid = true;
            this.productCategory = {
                id: 0,
                name: "",
                url: "",
                parentId: null
            };
        },
        filterNode(value, data) {
            if (!value) return true
            return data.label.indexOf(value) !== -1
        }
    },
    watch: {
        filterText(val) {
            this.$refs.tree.filter(val)
        },
    },
};