<script setup>

import { ref } from 'vue';
import { useUser } from '../../composables/useUser.js';
import UserAvatar from "../../components/user/UserAvatar.vue";

const { userName, userRoleString, avatarUrl, updateAvatar } = useUser();

const avatarHover = ref(null);
const fileInput = ref(null);

function openFilePicker() {
    fileInput.value.click();
}

function handleFileSelected(event) {
    const file = event.target.files[0];
    if (!file) return;

    updateAvatar(file);
}

</script>

<template>
    <aside class="w-64 shrink-0 rounded-xl bg-slate-800 p-6">
        <div class="flex flex-col items-center">

            <div class="group relative" @mouseenter="avatarHover = true" @mouseleave="avatarHover = false">

                <UserAvatar :avatarUrl="avatarUrl" size="profile" :editable="true" @click="openFilePicker" />

                <input ref="fileInput" type="file" accept="image/jpeg,image/png,image/webp" class="hidden"
                    @change="handleFileSelected" />
            </div>

            <h2 class="mt-4 text-xl font-semibold ">
                {{ userName }}
            </h2>
            <p class="text-sm text-slate-400">
                {{ userRoleString }}
            </p>
        </div>
    </aside>
</template>