const app = {
    data() {
        return {
            customer: {
                emailAddress: "",
                name: "",
                surname: "",
                phone: ""
            },
            rules:
            {
                name: [
                    {
                        required: true,
                        message: "Adı zorunludur.",
                        trigger: "blur",
                    },
                ],
                surname: [
                    {
                        required: true,
                        message: "Soyadı zorunludur.",
                        trigger: "blur",
                    },
                ]
            }
        }
    },
    created() {
        this.getProfile();
    },
    methods: {
        getProfile() {
            axios.get("/customer/profile")
                .then(res => {
                    this.customer = res.data;
                });
        },
        onSubmit(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    axios.put("/customer/update-profile", this.customer)
                        .then(res => {
                            this.$message({
                                type: "success",
                                message: "Kaydetme işlemi başarıyla gerçekleşti."
                            });
                        });
                }
            });
        },
    }
}