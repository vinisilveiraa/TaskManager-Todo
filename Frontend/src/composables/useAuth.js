import { ref } from "vue";
import { logoutApi } from "../services/authService";
import { saveSession, clearSession, getAccessToken } from "../utils/session";
import { useUser } from "./useUser";
import { jwtDecode } from "jwt-decode";


export function useAuth() {
    const { setUser, clearUser } = useUser();

    function login(session) {
        saveSession(session);
        setUser(session.accessToken);
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