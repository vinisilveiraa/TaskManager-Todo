<script setup>
import { ref } from 'vue';
import { login } from '../../services/authService';
import { useAuth } from '../../composables/useAuth';
import { useRouter } from 'vue-router';
import BaseInput from '../ui/BaseInput.vue';
import BaseButton from '../ui/BaseButton.vue';

const router = useRouter();
const { login: authLogin } = useAuth();

const username = ref('');
const password = ref('');

async function handleLogin() {
    try {
        console.log('login acionado')
        const result = await login(username.value, password.value);
        authLogin(result);
        router.push("/todo");
        
        console.log('login terminado')
    } catch (error) {
        console.error(error);
    }
}
</script>

<template>
    <div class="w-full max-w-md rounded-2xl bg-slate-900 p-8 shadow-2xl">
        <h1 class="mb-8 text-center text-3xl font-bold text-white">
            Login
        </h1>

        <form @submit.prevent="handleLogin" class="space-y-6">

            <BaseInput label="Usuário" v-model="username" />
            <BaseInput label="Senha" type="password" v-model="password" />

            <BaseButton type="submit" class="w-full"> Entrar </BaseButton>
        </form>
    </div>
</template>