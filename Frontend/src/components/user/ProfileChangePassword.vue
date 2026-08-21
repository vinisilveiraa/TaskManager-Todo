<script setup>
import { ref } from "vue";
import BaseButton from "../ui/BaseButton.vue";
import BaseInput from "../ui/BaseInput.vue";

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
        await changePassword({
            oldPassword: oldPassword.value,
            newPassword: newPassword.value
        });

        success(
            "Senha alterada",
            "Sua senha foi alterada com sucesso."
        );
        handleCancel();
    } catch (err) {
        error(
            "Erro ao alterar senha",
            err.response?.data?.message ?? "Ocorreu um erro inesperado."
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
    <div>
        <h2 class="text-xl font-bold text-white mb-6 border-b border-slate-700/60 pb-3">
            Alterar Senha
        </h2>

        <form class="space-y-4" @submit.prevent="handleChangePassword">
            <div>
                <BaseInput type="password" label="Senha Atual" v-model="oldPassword" required />
            </div>

            <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                <BaseInput type="password" label="Nova Senha" v-model="newPassword" required />
                <BaseInput type="password" label="Confirmar Nova Senha" v-model="confirmNewPassword" required />
            </div>

            <div class="flex justify-end gap-3 pt-4 border-t border-slate-700/40">
                <BaseButton @click="handleCancel" type="button" variant="danger">
                    Cancelar
                </BaseButton>
                <BaseButton type="submit" variant="primary">
                    Atualizar Senha
                </BaseButton>
            </div>
        </form>
    </div>
</template>