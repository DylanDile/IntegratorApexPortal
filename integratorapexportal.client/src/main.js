import { createApp } from 'vue'

import { createPinia } from "pinia";
import piniaPluginPersistence from 'pinia-plugin-persistedstate';
import router from "./router";
import App from './App.vue'

import "./core/bootstrap.js";
import "./core/toasters.js";
import "vue-awesome-paginate/dist/style.css";
import "vue3-toastify/dist/index.css";


const pinia = createPinia();
pinia.use(piniaPluginPersistence);

const renderApp = () => {
    const app = createApp(App);
    app.use(pinia);
    app.use(router);
    app.mount('#app');
}

renderApp();