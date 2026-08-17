import { ref, computed } from "vue";
import { getAccessToken } from "../utils/session";
import { jwtDecode } from "jwt-decode";
import { getCurrentUser, setAvatar, putUser, patchPassword } from '../services/userService'

const user = ref(null);

export function useUser() {

    async function loadUser() {
        try {
            user.value = await getCurrentUser();
        } catch (error) {
            console.error("Erro ao carregar usuário:", error);
            user.value = null;
        }
    }

    async function initializeUser() {
        const token = getAccessToken();

        if (!token) {
            user.value = null;
            return;
        }

        try {
            await loadUser();
        } catch {
            user.value = null;
        }
    }

    function clearUser() {
        user.value = null;
    }

    const userId = computed(() => user.value?.id);
    const userName = computed(() => user.value?.userName);
    const userRole = computed(() => user.value?.role);
    const avatarUrl = computed(() => user.value?.avatarUrl);

    const isAdmin = computed(() => userRole.value === 1);
    const isAuthenticated = computed(() => user.value !== null);

    const userRoleString = computed(() =>
        userRole.value === 1 ? "Admin" : userRole.value === 0 ? "User" : "Guest"
    );

    async function update(id, user) {
        await putUser(id, user);
        await loadUser();
    }

    async function updateAvatar(file) {
        await setAvatar(file);
        await loadUser();
    }

    async function changePassword(request) {
        await patchPassword(request);
        await loadUser();
    }

    return {
        user,

        loadUser,
        initializeUser,
        clearUser,

        userId,
        userName,
        userRole,
        avatarUrl,
        userRoleString,

        isAdmin,
        isAuthenticated,

        update,
        updateAvatar,
        changePassword,
    };
}