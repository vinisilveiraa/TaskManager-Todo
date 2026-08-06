<script setup>
import BaseButton from '../ui/BaseButton.vue';
import { useRouter } from "vue-router";
import { useAuth } from "../../composables/useAuth";
import { useUser } from '../../composables/useUser.js';

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
            <RouterLink to="/todo" class="flex items-center gap-3 text-white font-semibold text-xl">
                <i class="fa-solid fa-list-check text-violet-400"></i>
                ToDo
            </RouterLink>

            <div class="flex items-center gap-6">

                <RouterLink v-if="isAuthenticated" to="/todo" class="text-slate-300 hover:text-white transition">
                    Home
                </RouterLink>

                <RouterLink v-if="isAdmin" to="/dashboard" class="text-slate-300 hover:text-white transition">
                    Dashboard
                </RouterLink>

                <RouterLink v-if="!isAuthenticated" to="/login"
                    class="rounded-lg text-white bg-violet-600 px-4 py-2 hover:bg-violet-500 transition">
                    Login
                </RouterLink>

                <button v-else @click="handleLogout"
                    class="rounded-lg text-white bg-red-600 px-4 py-2 hover:bg-red-500 transition">
                    Logout
                </button>
            </div>
        </div>

    </nav>
</template>