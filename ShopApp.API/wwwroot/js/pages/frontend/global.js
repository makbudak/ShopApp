﻿const navbar = {
    data() {
        return {

        }
    },
    methods: {
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