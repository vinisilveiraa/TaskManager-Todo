import { ref } from "vue";
import { logoutApi } from "../services/authService";
import { saveSession, clearSession, getAccessToken } from "../utils/session";
import { useUser } from "./useUser";
import { jwtDecode } from "jwt-decode";

export function useAuth() {
    const { loadUser, clearUser } = useUser();

    function login(session) {
        saveSession(session);
        loadUser();
    }

    async function logout() {
        try {
            await logoutApi();
        } finally {
            clearSession();
            clearUser();
        }
    }

    return {
        login,
        logout
    };
}