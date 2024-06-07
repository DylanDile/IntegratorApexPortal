<template>
    <div>
        <NavBar />
        <div class="az-content az-content-dashboard">
            <div class="container">
                <div class="az-content-body">
                    <router-view />
                </div>
            </div>
        </div>
        <Footer />
    </div>
</template>
<script setup lang="ts">
    import NavBar from "./components/partials/NavBar.vue";
    import Footer from "./components/partials/Footer.vue";
</script>