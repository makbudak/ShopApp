import { createWebHistory, createRouter } from "vue-router";
import Layout from "./frontend/Layout";
import Home from "./frontend/Home";
import Blog from "./frontend/Blog";

const routes = [
  {
    path: "/",
    component: Layout,
    children: [
      {
        path: "",
        component: Home,
      },
      {
        path: "blog",
        component: Blog,
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
