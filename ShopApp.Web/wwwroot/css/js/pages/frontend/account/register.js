const app = {
    data() {
        return {
            customer: {
                name: "",
                surname: "",
                email: "",
                phone: "",
                password: "",
                rePassword: ""
            },
            rules:
            {
                name: [
                    {
                        required: true,
                        message: "Adı zorunludur.",
                        trigger: "blur",
                    }
                ],
                surname: [
                    {
                        required: true,
                        message: "Soyadı zorunludur.",
                        trigger: "blur",
                    }
                ],
                email: [
                    {
                        required: true,
                        message: "Email adresi zorunludur.",
                        trigger: "blur",
                    },
                    {
                        type: "email",
                        message: "Lütfen geçerli email adresi giriniz.",
                        trigger: ["blur", "change"]
                    }
                ],
                password: [
                    {
                        required: true,
                        message: "Şifre zorunludur.",
                        trigger: "blur",
                    },
                ],
                rePassword: [
                    {
                        required: true,
                        message: "Şifre Tekrar zorunludur.",
                        trigger: "blur",
                    },
                ],
            }
        }
    },
    methods: {
        onSubmit(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    axios.post("/customer/register", this.customer)
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