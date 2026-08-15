<script setup>
import ProfileAside from "./ProfileAside.vue";

</script>

<template>
    <div class="flex gap-6">
        <ProfileAside />

        <section class="flex-1 rounded-xl bg-slate-800 p-8">
            <h1 class="text-2xl font-bold ">
                Meu perfil
            </h1>

            <p class="mt-2 text-slate-400">
                Informações da sua conta
            </p>

            <div class="mt-8">
                <!-- conteúdo do perfil -->
            </div>
        </section>
    </div>
</template>