const app = {
    data() {
        return {
            productCategories: [],
            showGrid: true,
            showForm: false,
            title: "Ürün Kategorileri",
            productCategory: {},
            filterText: "",
            querySearch: "",
            defaultProps: {
                children: "items",
                label: "name",
            },
            rules:
            {

            },
            props: {
                value: "id",
                label: "name",
                children: "items"
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
            this.title = "Yeni Ürün Kategorisi Ekle";
            this.reset();
            this.showForm = true;
            this.showGrid = false;
        },
        editProductCategory(e) {
            this.title = "Ürün Kategorisi Düzenle";
            this.showForm = true;
            this.showGrid = false;
            this.productCategory = e;
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
                        axios.post("/admin/product-category/create", this.productCategory)
                            .then((res) => {
                                this.reset();
                                this.getAll();
                                this.$notify({
                                    title: 'İşlem Başarılı',
                                    message: 'Kaydetme işlemi başarıyla gerçekleşti.',
                                    type: 'success',
                                });
                            });
                    } else {
                        axios.put("/admin/product-category/update", this.productCategory)
                            .then((res) => {
                                this.reset();
                                this.getAll();
                                this.$notify({
                                    title: 'İşlem Başarılı',
                                    message: 'Güncelleme işlemi başarıyla gerçekleşti.',
                                    type: 'success',
                                });
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
                parentId: [],
                items: []
            };
        },
        filterNode(value, data) {
            if (!value) return true
            return data.label.indexOf(value) !== -1
        },
        handleSelect(item) {
            console.log(item);
            console.log(this.querySearch);
        }
    },
    watch: {
        filterText(val) {
            this.$refs.tree.filter(val)
        },
    },
};