import { createWebHistory, createRouter } from "vue-router";

// COMPONENTS
import Home from '../components/Home.vue';

const routes = [
    {
        path: "/",
        name: "Home",
        component: Home
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

export default router