const user = {
    data() {
        return {
            userList: [],
            userTypes: [],
            showGrid: true,
            showForm: false,
            title: "Kullanıcılar",
            pageNumber: 1,
            total: 0,
            user: {
                userType: null,
                name: "",
                surname: "",
                email: "",
                phone: "",
                isActive: true
            },
            rules:
            {
                userType: [
                    {
                        required: true,
                        message: 'Kullanıcı tipi seçiniz.',
                        trigger: ['blur', 'change']
                    },
                ],
                name: [
                    {
                        required: true,
                        message: 'Adı zorunludur.',
                        trigger: 'blur',
                    },
                ],
                surname: [
                    {
                        required: true,
                        message: 'Soyadı zorunludur.',
                        trigger: 'blur',
                    },
                ],
                email: [
                    {
                        required: true,
                        message: 'Email adresi zorunludur.',
                        trigger: 'blur',
                    },
                    {
                        type: 'email',
                        message: 'Lütfen geçerli email adresi giriniz.',
                        trigger: ['blur', 'change']
                    }

                ],
            },
            filterModel: {
                name: "",
                surname: "",
                email: ""
            }
        }
    },
    created() {
        this.getUsers();
        this.getUserTypes();
    },
    methods: {
        getUsers() {
            var query = `/admin/user/list?pageNumber=${this.pageNumber}`;
            if (this.filterModel.name) {
                query = `${query}&name=${this.filterModel.name}`;
            }
            if (this.filterModel.surname) {
                query = `${query}&surname=${this.filterModel.surname}`;
            }
            if (this.filterModel.email) {
                query = `${query}&email=${this.filterModel.email}`;
            }
            axios.get(query).then(res => {
                this.userList = res.data.list;
                this.total = res.data.total;
            })
        },
        getUserTypes() {
            axios.get("/admin/lookup/user-types").then(res => {
                this.userTypes = res.data;
            });
        },
        editUser(e) {
            this.showForm = true;
            this.showGrid = false;
            this.title = "Kullanıcı Düzenle";
            this.user = e;
        },
        deleteUser(e) {
            this.$confirm(
                'Silmek istediğinize emin misiniz?',
                'Silme Onayı',
                {
                    confirmButtonText: 'Evet',
                    cancelButtonText: 'Hayır',
                    type: 'danger',
                }
            ).then(() => {
                axios.delete(`/admin/user/${e.id}`)
                    .then(res => {
                        this.getUsers();
                        this.$message({
                            type: 'success',
                            message: 'Silme işlemi başarıyla gerçekleşti.',
                        });
                    });
            });
        },
        addUser() {
            this.showForm = true;
            this.showGrid = false;
            this.title = "Kullanıcı Ekle";
        },
        saveUser(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    if (this.user.id == 0) {
                        axios.post("/admin/user", this.user)
                            .then(res => {
                                this.reset();
                                this.getUsers();
                            });
                    } else {
                        axios.put("/admin/user", this.user)
                            .then(res => {
                                this.reset();
                                this.getUsers();
                            });
                    }
                } else {
                    return false;
                }
            })
        },
        reset() {
            this.showForm = false;
            this.showGrid = true;
            this.pageNumber = 1;
            this.user = {
                userType: null,
                name: "",
                surname: "",
                email: "",
                phone: "",
                isActive: true
            };
        },
        pageChange(val) {
            this.pageNumber = val;
            this.getUsers();
        },
        filter() {
            this.getUsers();
        },
        filterReset() {
            this.pageNumber = 1;
            this.filterModel = {
                name: "",
                surname: "",
                email: ""
            };
            this.getUsers();
        }
    }
};

const userApp = Vue.createApp(user);
userApp.use(ElementPlus);
userApp.mount("#app");
