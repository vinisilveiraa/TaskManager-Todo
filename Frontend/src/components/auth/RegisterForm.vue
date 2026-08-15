<script setup>
import { ref } from 'vue';

import { login, register } from '../../services/authService';
import { useAuth } from '../../composables/useAuth';
import { useRouter } from 'vue-router';

import BaseInput from '../ui/BaseInput.vue';
import BaseButton from '../ui/BaseButton.vue';

const router = useRouter();

const { login: authLogin } = useAuth();

const username = ref('');
const password = ref('');
const confirmPassword = ref('');

const passwordCheck = ref(true);

async function handleRegister() {
    if (password.value !== confirmPassword.value) {
        passwordCheck.value = false;
        return
    }
    try {
        await register(username.value, password.value);

        const result = await login(username.value, password.value);
        authLogin(result);
        router.push("/todo");
    } catch (error) {
        console.error(error);
    }
}
</script>

<template>
    <div class="w-full max-w-md rounded-2xl bg-slate-900 p-8 shadow-2xl flex flex-col">
        <h1 class="mb-8 text-center text-3xl font-bold ">
            Registre-se
        </h1>

        <form @submit.prevent="handleRegister" class="space-y-6">

            <BaseInput label="Usuário" v-model="username" required />

            <div class="flex gap-5">
                <BaseInput label="Senha" type="password" v-model="password" required
                    :variant="passwordCheck === false ? 'error' : ''" />
                <BaseInput label="Confirmar Senha" type="password" v-model="confirmPassword" required
                    :variant="passwordCheck === false ? 'error' : ''" />
            </div>

            <BaseButton type="submit" class="w-full"> Registrar </BaseButton>
        </form>

        <span class="  text-sm text-center mt-4">Já possui uma conta?
            <RouterLink to="/register" class="text-violet-500">Entrar</RouterLink to="/register">
        </span>
    </div>
</template>