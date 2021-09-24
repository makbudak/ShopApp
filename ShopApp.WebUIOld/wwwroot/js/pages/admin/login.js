const App = {
    data() {
        return {
            login: {
                email: '',
                password: ''
            },
            rules:
            {
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
                password: [
                    {
                        required: true,
                        message: 'Şifre zorunludur.',
                        trigger: 'blur',
                    },
                ]
            }
        }
    },
    methods: {
        onSubmit(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    axios.post("/admin/login", this.login)
                        .then(res => {
                            if (res.status == 200) {
                                location.href = "/admin/home";
                            }
                        });
                }
            });
        },
    },
}

const app = Vue.createApp(App);
app.use(ElementPlus);
app.mount("#app");