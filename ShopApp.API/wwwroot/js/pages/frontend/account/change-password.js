const app = {
    data() {
        return {
            customer: {
                oldPassword: "",
                newPassword: "",
                reNewPassword: ""
            },
            rules:
            {
                oldPassword: [
                    {
                        required: true,
                        message: "Eski şifre zorunludur.",
                        trigger: "blur",
                    },
                ],
                newPassword: [
                    {
                        required: true,
                        message: "Yeni şifre zorunludur.",
                        trigger: "blur",
                    },
                ],
                reNewPassword: [
                    {
                        required: true,
                        message: "Yeni Şifre Tekrar zorunludur.",
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
                    axios.put("/customer/change-password", this.customer)
                        .then(res => {
                            this.customer = {
                                oldPassword: "",
                                newPassword: "",
                                reNewPassword: ""
                            };
                            this.$message({
                                type: "success",
                                message: "Şifre güncelleme işlemi başarıyla gerçekleşti."
                            });
                        });
                }
            });
        },
    }
}
