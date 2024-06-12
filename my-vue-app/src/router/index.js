// router/index.js
import { createRouter, createWebHistory } from "vue-router";

import AboutPage from "../views/About.vue";
import ListNhanVien from "../views/NhanVien/List.vue";
import ListPhongBan from "../views/PhongBan/List.vue";
import ListChucVu from "../views/ChucVu/List.vue";
const routes = [
  {
    path: "/About",
    name: "AboutPage",
    component: AboutPage,
  },
  {
    path: "/",
    name: "ListNhanVien",
    component: ListNhanVien,
  },

  {
    path: "/ListPhongBan",
    name: "ListPhongBan",
    component: ListPhongBan,
  },
  {
    path: "/ListChucVu",
    name: "ListChucVu",
    component: ListChucVu,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
