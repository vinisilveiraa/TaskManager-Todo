<script setup>
import { onMounted, ref } from "vue";

import BaseButton from '../ui/BaseButton.vue';
import BaseInput from '../ui/BaseInput.vue'
import BaseBadge from '../ui/BaseBadge.vue'
import TodoFilters from '../todo/TodoFilters.vue'
import TodoForm from '../todo/TodoForm.vue'

import { useItems } from '../../composables/useItems.js'
const { create, loading } = useItems();

const showModal = ref(false);

async function handleCreated(data) {
    try {
        try {
            await create(data);
            showModal.value = false;
        } catch (error) {
            console.error(error);
        }
    } catch (error) {
        console.error(error);
    }
}

const emit = defineEmits(["change"]);
function handleChange(query) {
    emit("change", query);
}

</script>

<template>
    <div
        class="mx-auto mt-8 flex w-full max-w-4xl items-center justify-between rounded-2xl bg-slate-900 px-6 py-4 shadow-lg">

        <div class="flex items-center gap-4">
            <i class="fa-solid fa-search text-violet-400"></i>
            <BaseInput placeholder="Pesquisar tarefa..." />
        </div>

        <BaseButton @click="showModal = true">
            Nova tarefa
            <i class="fa-solid fa-plus"></i>
        </BaseButton>
    </div>

    <TodoFilters @change="handleChange" />
    <TodoForm :show-modal="showModal" modalTitle="Adicionar Nova Tarefa" @close="showModal = false" :loading="loading"
        @submit="handleCreated" />
</template>