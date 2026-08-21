<script setup>
import { onMounted, onUnmounted, ref } from "vue";

import BaseButton from "../ui/BaseButton.vue";
import { useUser } from "../../composables/useUser.js";
import UserAvatar from "../user/UserAvatar.vue";

const { userName, avatarUrl } = useUser();

const dropdown = ref(false);
const dropdownRef = ref(null);

const emit = defineEmits(["logout"]);

function toggleDropdown() {
    dropdown.value = !dropdown.value;
}

function closeDropdown() {
    dropdown.value = false;
}

function handleOutsideClick(event) {
    if (
        dropdownRef.value &&
        !dropdownRef.value.contains(event.target)
    ) {
        closeDropdown();
    }
}

function handleLogout() {
    emit("logout");
    closeDropdown();
}

onMounted(() => {
    window.addEventListener("click", handleOutsideClick);
});

onUnmounted(() => {
    window.removeEventListener("click", handleOutsideClick);
});
</script>

<template>
    <div ref="dropdownRef" class="relative">
        <button @click="toggleDropdown"
            class="rounded-lg  outline px-4 py-2 bg-slate-800 hover:opacity-80 transition flex items-center gap-2">
            {{ userName }}

            <UserAvatar :avatarUrl="avatarUrl" size="icon" />
        </button>

        <div v-if="dropdown"
            class="absolute right-0 mt-2 w-48 bg-slate-800  rounded-md shadow-lg border border-slate-700 py-2 px-3 z-50 flex flex-col gap-2">
            <RouterLink to="/me" class="px-4 py-2 bg-slate-700 hover:bg-slate-600 rounded-xl">
                <i class="fa-solid fa-user mr-1"></i>
                Perfil
            </RouterLink>

            <BaseButton variant="danger" @click="handleLogout">
                Logout
            </BaseButton>
        </div>
    </div>
</template>