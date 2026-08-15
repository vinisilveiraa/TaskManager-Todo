<script setup>
import { onMounted, ref } from "vue";
import { useItems } from "../../composables/useItems.js";

import TodoItem from "./TodoItem.vue";
import TodoForm from "./TodoForm.vue";
import BaseButton from "../ui/BaseButton.vue";
import BaseInput from "../ui/BaseInput.vue"

const { items, loading, loadMyItems, remove, patch, update } = useItems();

const showModal = ref(false);

const showEditModal = ref(false);
const editingItem = ref(null);

function openEditModal(item) {
    editingItem.value = { ...item };
    showModal.value = true;
}
function closeModal() {
    showModal.value = false;
    editingItem.value = null;
}

async function handleDelete(id) {
    await remove(id);
}

async function handlePatch(id) {
    await patch(id);
}

async function handleEdit(item) {
    await update(item.id, item);
    closeModal();
}

// cada vez q abrir refresh
onMounted(loadMyItems)
</script>

<template>
    <div class="mx-auto mt-8 w-full max-w-4xl rounded-xl rounded-b-none bg-slate-900 shadow-lg">
        <table class="w-full ">

            <thead class="border-b border-slate-700">
                <tr>
                    <th class="p-4 text-left">Título</th>
                    <th class="p-4 text-left">Descrição</th>
                    <th class="p-4 text-center">Estado</th>
                    <th class="p-4 text-center">Criado em</th>
                    <th class="p-4 text-center">Ações</th>
                </tr>
            </thead>

            <tbody>
                <TodoItem v-for="item in items" :key="item.id" :id="item.id" :title="item.title"
                    :description="item.description" :isCompleted="item.isCompleted" :created_at="item.created_At"
                    @delete="handleDelete" @patch="handlePatch" @edit="openEditModal" />
            </tbody>

        </table>
    </div>

    <TodoForm :show-modal="showModal" modalTitle="Editando Tarefa" :loading="loading" :editingItem="editingItem"
        @submit="handleEdit" @close="showModal = false" />


</template>