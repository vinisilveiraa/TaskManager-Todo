<script setup>
import NavHeader from "../components/ui/NavHeader.vue";
</script>

<template>
    <main class="w-full max-w-6xl mx-auto px-10 py-8">

        <NavHeader :sections="[
            { label: 'Perfil', to: '/me/profile' },
            { label: 'Editar', to: '/me/edit' }
        ]" />

        <RouterView />

    </main>
</template>