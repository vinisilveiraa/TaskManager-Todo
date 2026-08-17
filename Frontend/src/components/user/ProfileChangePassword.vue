<script setup>
import { ref } from "vue";
import BaseButton from "../ui/BaseButton.vue";
import BaseInput from "../ui/BaseInput.vue"
import UserAvatar from "./UserAvatar.vue";
import ProfileChangePassword from "./ProfileChangePassword.vue";

import { useToast } from "../../composables/useToast.js";
import { useUser } from "../../composables/useUser";

const { success, error } = useToast();
const { changePassword } = useUser();

const oldPassword = ref('');
const newPassword = ref('');
const confirmNewPassword = ref('');

async function handleChangePassword() {

    if (newPassword.value !== confirmNewPassword.value) {
        error(
            "Senhas diferentes",
            "A confirmação da nova senha não corresponde."
        );
        return;
    }

    if (newPassword.value === oldPassword.value) {
        error(
            "Senhas iguais",
            "Sua nova senha deve ser diferente da atual."
        );
        return;
    }

    try {
        await await changePassword({
            oldPassword: oldPassword.value,
            newPassword: newPassword.value
        });;

        success(
            "Senha alterada",
            "Sua senha foi alterada com sucesso."
        );
        handleCancel();
    } catch (err) {
        error(
            "Erro ao alterar senha",
            err.response?.data?.message ??
            "Ocorreu um erro inesperado."
        );
    }
}

function handleCancel() {
    oldPassword.value = "";
    newPassword.value = "";
    confirmNewPassword.value = "";
}

</script>

<template>
    <h1 class="text-2xl font-bold mb-5">
        Alterar Senha
    </h1>

    <div class="">

        <form class="w-full" @submit.prevent="handleChangePassword">

            <div class="flex">
                <BaseInput type="password" label="Senha Atual" v-model="oldPassword" required />
            </div>
            <div class="flex gap-5">
                <BaseInput type="password" label="Nova Senha" v-model="newPassword" required />
                <BaseInput type="password" label="Confirmar Nova Senha" v-model="confirmNewPassword" required />
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
</template>