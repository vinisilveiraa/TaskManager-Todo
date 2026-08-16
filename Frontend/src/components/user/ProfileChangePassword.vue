<script setup>
import { ref } from "vue";
import BaseButton from "../ui/BaseButton.vue";
import BaseInput from "../ui/BaseInput.vue"
import UserAvatar from "./UserAvatar.vue";
import ProfileChangePassword from "./ProfileChangePassword.vue";
import { useUser } from "../../composables/useUser";

const { userId } = useUser();

const currentPassword = ref('');
const newPassword = ref('');
const confirmNewPassword = ref('');

function handleSubmit() {

}

function handleCancel() {
    currentPassword.value = "";
    newPassword.value = "";
    confirmNewPassword.value = "";
}

</script>

<template>
    <h1 class="text-2xl font-bold mb-5">
        Alterar Senha
    </h1>

    <div class="">


        <form class="w-full" @submit.prevent="handleSubmit">

            <div class="flex">
                <BaseInput type="password" label="Senha Atual" v-model="currentPassword" />
            </div>
            <div class="flex gap-5">
                <BaseInput type="password" label="Nova Senha" v-model="newPassword" />
                <BaseInput type="password" label="Confirmar Nova Senha" v-model="confirmNewPassword" />
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