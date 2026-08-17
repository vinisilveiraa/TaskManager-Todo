<script setup>
import { ref } from "vue";

import BaseButton from "../ui/BaseButton.vue";
import BaseInput from "../ui/BaseInput.vue"
import UserAvatar from "./UserAvatar.vue";
import ProfileChangePassword from "./ProfileChangePassword.vue";

import { useUser } from "../../composables/useUser";
import { useToast } from "../../composables/useToast.js";

const { success, error } = useToast();
const { userId, userName, avatarUrl, updateAvatar, update } = useUser();

const fileInput = ref(null);
const newUsername = ref(userName.value);

const avatarPreview = ref(null);
const selectedFile = ref(null);

function openFilePicker() {
    fileInput.value.click();
}

function handleFileSelected(event) {
    const file = event.target.files[0];
    if (!file) return;

    selectedFile.value = file;
    avatarPreview.value = URL.createObjectURL(file);
}

async function handleSubmit() {
    try {
        const usernameChanged =
            newUsername.value !== userName.value;

        const avatarChanged =
            selectedFile.value !== null;

        if (!usernameChanged && !avatarChanged) {
            return;
        }

        if (avatarChanged) {
            await updateAvatar(selectedFile.value);

            URL.revokeObjectURL(avatarPreview.value);

            avatarPreview.value = null;
            selectedFile.value = null;
        }

        if (usernameChanged) {
            await update(userId.value, {
                UserName: newUsername.value
            });
        }

        success(
            "Perfil atualizado",
            "Suas informações foram atualizadas com sucesso."
        );

    } catch (err) {
        error(
            "Erro ao atualizar perfil",
            err.response?.data?.message ??
            "Não foi possível atualizar seu perfil."
        );
    }
}

function handleCancel() {
    window.location.reload();
}

</script>

<template>
    <section class="flex-1 rounded-xl bg-slate-800 p-8 mb-5">
        <h1 class="text-2xl font-bold mb-5">
            Editar Perfil
        </h1>

        <div class="">
            <form class="w-full" @submit.prevent="handleSubmit">

                <div class="flex gap-5">
                    <UserAvatar :avatar-url="avatarUrl" :avatar-preview="avatarPreview" :editable="true"
                        @click="openFilePicker" />

                    <BaseInput label="Nome de Usuario" :modelValue="userName" v-model="newUsername" 0 required />
                    <!-- <BaseInput label="Email" :modelValue="userName" /> -->

                    <input ref="fileInput" type="file" accept="image/jpeg,image/png,image/webp" class="hidden"
                        @change="handleFileSelected" />
                </div>

                <div class="flex justify-end gap-3 mx-4">
                    <BaseButton @click="handleCancel" type="button" variant="danger">
                        Cancelar
                    </BaseButton>
                    <BaseButton type="submit" variant="success">
                        Salvar
                    </BaseButton>
                </div>
            </form>
        </div>
    </section>
    <section class="flex-1 rounded-xl bg-slate-800 p-8 ">

        <ProfileChangePassword />

    </section>
</template>