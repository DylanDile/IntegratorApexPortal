import { defineStore } from "pinia";
export const useAuthStore = defineStore({
    id: "storeAuth",
    state: () => {
        return {
            authenticated: false,
            user: {},
            test: false
        }
    },
    persist: true,
    getters: {
        isAuthenticated(state){
            return state.authenticated;
        },
        loggedUser(state){
            return state.user;
        }
    },
    actions: {
        // Initialize Keycloak OAuth
        async initOauth(keycloak, clearData = true) {
            if(clearData) { await this.clearUserData(); }
            this.authenticated = keycloak.authenticated;
            this.user.username = keycloak.idTokenParsed?.preferred_username ?? "";
            this.user.email = keycloak.idTokenParsed?.email ?? "";
            this.user.name = keycloak.idTokenParsed?.family_name ?? "";
            this.user.last_name = keycloak.idTokenParsed?.given_name ?? "";
            this.user.token = keycloak.token;
            this.user.refToken = keycloak.refreshToken;
        },
        // Logout user
        async logout() {
           
        },
        // Refresh user's token
        async refreshUserToken() {
            
        },
        // Clear user's store data
        clearUserData() {
            this.authenticated = false;
            this.user = {};
        }
    }
});