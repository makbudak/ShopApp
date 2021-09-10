const navbar = {
    data() {
        return {
            showDrawer: false,
            direction: "ltr",
            filterText: "",
            menuItems: [{
                label: "İçerik Yönetimi",
                children: [
                    {
                        label: "Ürün Kategorileri",
                        url: "/admin/product-category"
                    },
                    {
                        label: "Ürünler",
                        url: "/admin/product"
                    }
                ]
            },
            {
                label: "Ayarlar",
                children: [
                    {
                        label: "Kullanıcılar",
                        url: "/admin/user"
                    },
                    {
                        label: "Roller",
                        url: "/admin/role"
                    }
                ]
            }],
            defaultProps: {
                children: "children",
                label: "label",
            },
        }
    },
    watch: {
        filterText(val) {
            this.$refs.tree.filter(val)
        },
    },
    methods: {
        menuShow() {
            this.showDrawer = true;
        },
        filterNode(value, data) {
            if (!value) return true
            return data.label.indexOf(value) !== -1
        }
    }
}
var navbarApp = Vue.createApp(navbar);
navbarApp.use(ElementPlus);
navbarApp.mount("#navbar");