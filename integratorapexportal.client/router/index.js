import {createRouter, createWebHistory} from "vue-router";
import {useAuthStore} from "../stores/authStore.js";

// COMPONENTS
import Home from '../components/Home.vue';

const routes = [
    {
        path: "/",
        name: "Home",
        component: Home
    },
    {
        path: "/login",
        name: "Login",
        component: () => import("../components/auth/Login.vue")
    },
    {
        path: "/register",
        name: "Register",
        component: () => import("../components/auth/Register.vue")
    },
    {
        path: "/banks",
        name: "Banks",
        component: () => import("../components/pages/Banks.vue")
    },
    {
        path: "/packs",
        name: "Packs",
        component: () => import("../components/pages/Packs.vue")
    },
    {
        path: "/returns",
        name: "Returns",
        component: () => import("../components/pages/Returns.vue")
    },
    {
        path: "/view/generated/returns/:id",
        name: "ViewGeneratedReturn",
        component: () => import("../components/generated/View.vue")
    },
    {
        path: "/generated/returns/submission/:submissionPack",
        name: "ViewGeneratedReturnsBySubmissionPack",
        component: () => import("../components/generated/ViewBySubmissionPack.vue")
    },
    {
        path: "/generated/returns/institution/:institutionId",
        name: "ViewGeneratedReturnsByInstitution",
        component: () => import("../components/generated/ViewByInstitution.vue")
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach((to, from, next) => {
    const store = useAuthStore()
    store.isLoading = true
    if (to.path === "/login" || to.path === "/register") {
        next();
    } else {
        if (store.isAuthenticated === false) {
            next('/login');
        } else {
            // Add institutionID to the all route params where institutionID is required
            to.params.institutionId = store.getInstitutionID();
            next();
        }
    }
});

router.afterEach((to, from) => {
    // Logging or analytics
    const store = useAuthStore();
    store.isLoading = false
});

export default router