<script setup>
import { formatDate } from '../../utils/date';
import BaseButton from '../ui/BaseButton.vue';

const props = defineProps({
    id: Number,
    title: String,
    description: String,
    isCompleted: Boolean,
    created_at: String,
    completed_at: String
});

const emit = defineEmits([
    "delete",
    "patch",
    "edit"
]);
</script>

<template>
    <tr class="border-b border-slate-800 hover:bg-slate-800 transition">
        <td class="p-4 max-w-xs">
            <div class="truncate" :title="title">
                {{ title }}
            </div>
        </td>

        <td class="p-4 text-slate-400 max-w-xs">
            <div class="line-clamp-2 break-all" :title="description">
                {{ description }}
            </div>
        </td>

        <td class="p-4 text-center">
            <span v-if="isCompleted"
                class="rounded inline-block text-center w-22 bg-green-500/20 px-2 py-1 text-green-400">
                Concluída
            </span>

            <span v-else class="rounded inline-block text-center w-22 bg-yellow-500/20 px-2 py-1 text-yellow-400">
                Pendente
            </span>
        </td>
        <td class="p-4 text-center">
            <span v-if="!isCompleted" class="text-slate-400 whitespace-nowrap">{{ formatDate(created_at) }}</span>
            <span v-else class="text-slate-400 whitespace-nowrap">{{ formatDate(completed_at) }}</span>
        </td>

        <td class="text-center align-middle p-2">
            <div class="flex justify-center items-center gap-2">
                <BaseButton v-if="!isCompleted" variant="success" @click="emit('patch', props.id)">
                    <i class="fa-solid fa-check"></i>
                </BaseButton>

                <BaseButton v-else variant="danger" @click="emit('patch', props.id)">
                    <i class="fa-solid fa-x"></i>
                </BaseButton>

                <BaseButton variant="danger" @click="emit('delete', props.id)">
                    <i class="fa-solid fa-trash"></i>
                </BaseButton>

                <BaseButton v-if="!isCompleted" variant="info" @click="emit('edit', props)">
                    <i class="fa-solid fa-pencil"></i>
                </BaseButton>
            </div>
        </td>
    </tr>
</template>