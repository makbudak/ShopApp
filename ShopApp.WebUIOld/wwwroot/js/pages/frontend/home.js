const app = {
    data() {
        return {
            productCategories: [],
            filterText: "",
            defaultProps: {
                children: "items",
                label: "name",
            },
        }
    },
    created() {
        this.getParoductCategories();
    },
    watch: {
        filterText(val) {
            this.$refs.tree.filter(val)
        },
    },
    methods: {
        getParoductCategories() {
            axios.get("/product-category/list")
                .then(res => {
                    this.productCategories = res.data;
                })
        },
        filterNode(value, data) {
            if (!value) return true
            return data.label.indexOf(value) !== -1
        }
    }
};