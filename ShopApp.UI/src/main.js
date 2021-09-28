import { createApp } from "vue";
import App from "./App.vue";
import axios from "axios";
import VueAxios from "vue-axios";
import Antd from 'ant-design-vue';
import "bootstrap/dist/css/bootstrap.min.css"
import "ant-design-vue/dist/antd.css";
import router from "./router";
import "bootstrap";


const app = createApp(App).use(Antd);
app.use(VueAxios, axios);
app.use(router);
app.mount("#app");
