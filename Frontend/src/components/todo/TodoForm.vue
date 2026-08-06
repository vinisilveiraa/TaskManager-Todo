<script setup>
import { ref } from "vue";
import { watch } from "vue";
import BaseInput from '../ui/BaseInput.vue';
import BaseButton from '../ui/BaseButton.vue';
import BaseModal from '../ui/BaseModal.vue';

const title = ref('');
const description = ref('');

const props = defineProps({
    loading: Boolean,
    modalTitle: String,
    showModal: Boolean,
    editingItem: {
        type: Object,
        default: null
    }
});

const emit = defineEmits(['close', 'submit']);

watch(
    () => props.editingItem,
    (item) => {
        if (item) {
            title.value = item.title;
            description.value = item.description;
        } else {
            title.value = '';
            description.value = '';
        }
    },
    { immediate: true }
)

function handleSubmit() {
    emit("submit", {
        id: props.editingItem?.id,
        title: title.value,
        description: description.value
    });
    title.value = '';
    description.value = '';
}

</script>

<template>

    <BaseModal v-if="showModal" @close="emit('close')" class="">
        <div class="mb-5">
            <h1 class="text-xl font-bold">{{ modalTitle }}</h1>
        </div>

        <form @submit.prevent="handleSubmit">

            <BaseInput :modelValue="props.editingItem?.title" label=" Título" v-model="title" class="mb-2" required>
            </BaseInput>
            <BaseInput :modelValue="props.editingItem?.description" label="Descrição" v-model="description">
            </BaseInput>

            <div class="flex justify-end gap-4 mt-5">
                <BaseButton type="submit" variant="primary" :disabled="props.loading">
                    {{ props.loading ? 'Carregando...' : 'Salvar' }}
                </BaseButton>
                <BaseButton type="button" @click="emit('close')" variant="disabled">
                    Cancelar
                </BaseButton>
            </div>

        </form>
    </BaseModal>

</template>