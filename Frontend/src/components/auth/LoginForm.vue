<script setup>
import { ref } from 'vue';

import BaseInput from '../ui/BaseInput.vue';
import BaseButton from '../ui/BaseButton.vue';

import { login } from '../../services/authService';
import { useAuth } from '../../composables/useAuth';
import { useToast } from "../../composables/useToast.js";
import { useRouter } from 'vue-router';

const router = useRouter();

const { login: authLogin } = useAuth();
const { success, error } = useToast();

const username = ref('');
const password = ref('');

async function handleLogin() {
    try {
        const result = await login(username.value, password.value);
        authLogin(result);
        success(
            "Úsuario logado"
        )
        router.push("/todo");
    } catch (err) {
        console.log(err)
        error(
            "Erro ao logar",
            err.response?.data?.message ??
            "Ocorreu um erro inesperado."
        )
    }
}
</script>

<template>
    <div class="w-full max-w-md rounded-2xl bg-slate-900 p-8 shadow-2xl flex flex-col">
        <h1 class="mb-8 text-center text-3xl font-bold ">
            Login
        </h1>

        <form @submit.prevent="handleLogin" class="space-y-6">

            <BaseInput label="Usuário" v-model="username" required />
            <BaseInput label="Senha" type="password" v-model="password" required />

            <BaseButton type="submit" class="w-full"> Entrar </BaseButton>
        </form>

        <span class="  text-sm text-center mt-4">Não possui uma conta?
            <RouterLink to="/register" class="text-violet-500">Registre-se</RouterLink>
        </span>
    </div>
</template>