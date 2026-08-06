<script setup>
import TodoList from "../components/todo/TodoList.vue"
import TodoHeader from "../components/todo/TodoHeader.vue"
import { useItems } from "../composables/useItems.js";

const { setQuery } = useItems();

</script>

<template>
    <main class="flex flex-col min-h-screen items-center justify-self-auto bg-slate-950 py-10 text-white">
        <TodoHeader @change="setQuery" />
        <TodoList />
    </main>
</template>