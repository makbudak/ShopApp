const navbar = {
    data() {
        return {
            leftMenuDrawer: false
        }
    },
    methods: {
        leftMenuShow() {
            this.leftMenuDrawer = true;
        },
        goCart() {
            location.href = "/cart";
        }
    }
};

const navbarApp = Vue.createApp(navbar);
navbarApp.use(ElementPlus);
navbarApp.mount("#navbar");

if (app) {
    const _app = Vue.createApp(app);
    _app.use(ElementPlus);
    _app.mount("#app");
}