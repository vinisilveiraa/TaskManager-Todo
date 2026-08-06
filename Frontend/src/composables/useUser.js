import { ref, computed } from "vue";
import { getAccessToken } from "../utils/session";
import { jwtDecode } from "jwt-decode";

const user = ref(null);

export function useUser() {

    function initializeUser() {
        const token = getAccessToken();

        if (!token) {
            user.value = null;
            return;
        }

        try {
            user.value = jwtDecode(token);
        } catch {
            user.value = null;
        }

        user.value = jwtDecode(token);
    }

    function setUser(token) {
        try {
            user.value = jwtDecode(token);
        }
        catch {
            user.value = null;
        }
    }

    function clearUser() {
        user.value = null;
    }

    const userId = computed(() => {
        return user.value?.["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    });

    const userName = computed(() => {
        return user.value?.["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    });

    const userRole = computed(() => {
        return user.value?.["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    });

    const isAdmin = computed(() => {
        return userRole.value === "Admin";
    });

    const isAuthenticated = computed(() => {
        return user.value !== null
    })

    return {
        user,

        userId,
        userName,
        userRole,

        isAdmin,
        isAuthenticated,

        initializeUser,
        setUser,
        clearUser,
    }
}