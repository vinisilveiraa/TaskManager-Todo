<script setup>
import { onMounted } from 'vue';
import { useUser } from '../../composables/useUser';

import BaseSlot from '../ui/BaseSlot.vue'

const { stats, loadingStats: loading } = useUser();

</script>

<template>
    <div class="w-64 grid grid-cols-2 gap-2">
        <BaseSlot :title="'Total'" :loading="loading">
            {{ stats?.totalTasks ?? 0 }}
        </BaseSlot>
        <BaseSlot :title="'Rate%'" :loading="loading">
            {{ stats?.completionRate ?? 0 }}<span class="text-sm">%</span>
        </BaseSlot>
        <BaseSlot :title="'Pendentes'" :loading="loading">
            {{ stats?.pendingTasks ?? 0 }}
        </BaseSlot>
        <BaseSlot :title="'Completas'" :loading="loading">
            {{ stats?.completedTasks ?? 0 }}
        </BaseSlot>
    </div>
</template>
