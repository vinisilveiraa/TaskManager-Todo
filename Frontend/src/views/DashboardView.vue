<script setup>
import { useUser } from '../composables/useUser';

const { userName } = useUser();

</script>

<template>
    <main class="flex flex-col min-h-screen items-center justify-self-auto bg-slate-950 py-10 text-white">
        <h1 class="text-2xl">Bem vindo <strong>{{ userName }}</strong></h1>
    </main>
</template>