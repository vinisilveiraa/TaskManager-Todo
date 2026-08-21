<script setup>
import { useRouter } from "vue-router";
import { useAuth } from "../../composables/useAuth";
import { useUser } from "../../composables/useUser";

import NavbarDropdown from "./NavbarDropdown.vue";

const { logout } = useAuth();
const { isAdmin, isAuthenticated } = useUser();

const router = useRouter();

async function handleLogout() {
    try {
        await logout();
        router.push("/login");
    } catch (error) {
        console.error(error);
    }
}
</script>

<template>
    <nav class="bg-slate-900 border-b border-slate-800 shadow-lg">
        <div class="mx-auto flex max-w-6xl items-center justify-between px-8 py-4">

            <RouterLink to="/todo" class="flex items-center gap-3  font-semibold text-xl">
                <i class="fa-solid fa-list-check text-violet-400"></i>
                ToDo
            </RouterLink>

            <div class="flex items-center gap-5">

                <RouterLink v-if="isAuthenticated" to="/todo" class="text-slate-300 hover: transition">
                    Home
                </RouterLink>

                <RouterLink v-if="isAdmin" to="/dashboard" class="text-slate-300 hover: transition">
                    Dashboard
                </RouterLink>

                <div v-if="!isAuthenticated" class="flex gap-2">
                    <RouterLink to="/register"
                        class="rounded-lg  bg-violet-600 px-4 py-2 hover:bg-violet-500 transition">
                        Registrar
                    </RouterLink>

                    <RouterLink to="/login" class="rounded-lg  bg-violet-600 px-4 py-2 hover:bg-violet-500 transition">
                        Login
                    </RouterLink>
                </div>

                <NavbarDropdown v-else @logout="handleLogout" />

            </div>
        </div>
    </nav>
</template>