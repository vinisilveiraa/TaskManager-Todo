<script setup>
import { onMounted, ref } from "vue";

import BaseBadge from '../ui/BaseBadge.vue'

import ProfileStats from "./ProfileStats.vue";
import ProfileAside from "./ProfileAside.vue";

import WeeklyStatsChart from "./WeeklyStatsChart.vue"
import MonthlyStatsChart from "./MonthlyStatsChart.vue"
import { useUser } from "../../composables/useUser.js";

const { loadStats } = useUser();

onMounted(() => {
    loadStats();
});

const activeChart = ref('weekly')

function changeChart(chart) {
    activeChart.value = chart;
}

</script>

<template>
    <div class="flex items-start gap-6">
        <aside class="flex flex-col items-center justify-center">
            <ProfileAside />
            <ProfileStats />
        </aside>
        <section class="flex-1 rounded-xl bg-slate-800 p-8">
            <h1 class="text-2xl font-bold mb-4">
                Status
            </h1>

            <div class="flex justify-center gap-2">
                <BaseBadge @click="changeChart('weekly')" :variant="activeChart == 'weekly' ? 'primary' : 'disabled'">
                    Semanal
                </BaseBadge>
                <BaseBadge @click="changeChart('monthly')" :variant="activeChart == 'monthly' ? 'primary' : 'disabled'">
                    Mensal
                </BaseBadge>
            </div>

            <WeeklyStatsChart v-if="activeChart == 'weekly'" />
            <MonthlyStatsChart v-if="activeChart == 'monthly'" />
        </section>
    </div>
</template>