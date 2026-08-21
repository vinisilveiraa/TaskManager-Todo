<script setup>
import { ref } from "vue";
import BaseButton from "../ui/BaseButton.vue";
import BaseInput from "../ui/BaseInput.vue";
import UserAvatar from "./UserAvatar.vue";
import ProfileChangePassword from "./ProfileChangePassword.vue";

import { useUser } from "../../composables/useUser";
import { useToast } from "../../composables/useToast.js";

const { success, error } = useToast();
const { userId, userName, avatarUrl, updateAvatar, update } = useUser();

const fileInput = ref(null);
const newUsername = ref(userName.value || '');

const avatarPreview = ref(null);
const selectedFile = ref(null);

function openFilePicker() {
    fileInput.value?.click();
}

function handleFileSelected(event) {
    const file = event.target.files[0];
    if (!file) return;

    selectedFile.value = file;
    avatarPreview.value = URL.createObjectURL(file);
}

async function handleSubmit() {
    try {
        const usernameChanged = newUsername.value !== userName.value;
        const avatarChanged = selectedFile.value !== null;

        if (!usernameChanged && !avatarChanged) return;

        if (avatarChanged) {
            await updateAvatar(selectedFile.value);
            URL.revokeObjectURL(avatarPreview.value);
            avatarPreview.value = null;
            selectedFile.value = null;
        }

        if (usernameChanged) {
            await update(userId.value, { UserName: newUsername.value });
        }

        success(
            "Perfil atualizado",
            "Suas informações foram atualizadas com sucesso."
        );
    } catch (err) {
        error(
            "Erro ao atualizar perfil",
            err.response?.data?.message ?? "Não foi possível atualizar seu perfil."
        );
    }
}

function handleCancel() {
    newUsername.value = userName.value;
    if (avatarPreview.value) {
        URL.revokeObjectURL(avatarPreview.value);
        avatarPreview.value = null;
    }
    selectedFile.value = null;
}
</script>

<template>
    <div class="space-y-6 max-w-4xl mx-auto">

        <section class="rounded-2xl bg-slate-800 p-6 md:p-8 border border-slate-700/60 shadow-lg">
            <h1 class="text-xl font-bold text-white mb-6 border-b border-slate-700/60 pb-3">
                Editar Perfil
            </h1>

            <form class="space-y-6" @submit.prevent="handleSubmit">
                <div class="flex flex-col sm:flex-row items-center gap-6">
                    <div class="flex flex-col items-center gap-2">
                        <UserAvatar :avatar-url="avatarUrl" :avatar-preview="avatarPreview" :editable="true"
                            @click="openFilePicker" />
                        <span class="text-xs text-slate-400">Clique para alterar</span>
                    </div>

                    <div class="flex-1 w-full">
                        <BaseInput label="Nome de Usuário" v-model="newUsername" required />
                    </div>

                    <input ref="fileInput" type="file" accept="image/jpeg,image/png,image/webp" class="hidden"
                        @change="handleFileSelected" />
                </div>

                <div class="flex justify-end gap-3 pt-4 border-t border-slate-700/40">
                    <BaseButton @click="handleCancel" type="button" variant="danger">
                        Cancelar
                    </BaseButton>
                    <BaseButton type="submit" variant="primary">
                        Salvar Alterações
                    </BaseButton>
                </div>
            </form>
        </section>

        <section class="rounded-2xl bg-slate-800 p-6 md:p-8 border border-slate-700/60 shadow-lg">
            <ProfileChangePassword />
        </section>
    </div>
</template>