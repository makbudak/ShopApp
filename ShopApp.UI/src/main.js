import ElementPlus from "element-plus";
import "element-plus/theme-chalk/index.css";
import { createApp } from "vue";
import App from "./App.vue";
import axios from "axios";
import VueAxios from "vue-axios";

var app = createApp(App);
app.use(VueAxios, axios);
app.use(ElementPlus).mount("#app");
