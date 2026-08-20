<script setup>

import { ref } from 'vue';
import { useUser } from '../../composables/useUser.js';
import UserAvatar from "../../components/user/UserAvatar.vue";

const { userName, userRoleString, avatarUrl, updateAvatar } = useUser();


</script>

<template>
        <div class="flex flex-col items-center">

            <div class="group relative">
                <UserAvatar :avatarUrl="avatarUrl" size="profile" />

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
</template>