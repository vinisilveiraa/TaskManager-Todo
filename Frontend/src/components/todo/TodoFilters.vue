<script setup>
import { ref } from "vue";
import BaseBadge from '../ui/BaseBadge.vue'

const filter = ref('all');
const sort = ref('newest');

const emit = defineEmits(["change"]);

function filterValue(value) {
    filter.value =
        filter.value === value
            ? "all"
            : value;
    emitChange();
}
function sortValue(value) {
    sort.value =
        sort.value === value
            ? "newest"
            : value;
    emitChange();
}

function emitChange() {
    emit("change", {
        filter: filter.value,
        sort: sort.value
    });
}


</script>
<template>
    <div class="flex w-full mt-2 gap-20 max-w-4xl mx-auto ">
        <div class="mx-auto w-full flex bg-slate-900 px-6 py-4 rounded-2xl gap-1 justify-center">
            <BaseBadge :variant="filter === ('all') ? 'primary' : 'disabled'" @click="filterValue('all')">
                Mostrar Todas
            </BaseBadge>
            <BaseBadge :variant="filter === ('completed') ? 'primary' : 'disabled'" @click="filterValue('completed')">
                Concluídas
            </BaseBadge>
            <BaseBadge :variant="filter === ('pending') ? 'primary' : 'disabled'" @click="filterValue('pending')">
                Pendentes
            </BaseBadge>
        </div>

        <div class="mx-auto w-full flex bg-slate-900 px-6 py-4 rounded-2xl gap-1 justify-center">
            <BaseBadge :variant="sort === ('newest') ? 'primary' : 'disabled'" @click="sortValue('newest')">
                Mais Recente
            </BaseBadge>
            <BaseBadge :variant="sort === ('oldest') ? 'primary' : 'disabled'" @click="sortValue('oldest')">
                Mais Antigo
            </BaseBadge>
        </div>
    </div>
</template>