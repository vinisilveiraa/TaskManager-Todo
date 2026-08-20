<script setup>
import { onMounted } from "vue";

import ProfileAside from "./ProfileAside.vue";
import BaseSlot from "../ui/BaseSlot.vue"
import BaseChart from "../ui/BaseChart.vue"

import { useUser } from "../../composables/useUser.js";

const { stats, loadStats, loadingStats: loading } = useUser();

onMounted(() => {
    loadStats();
})

</script>

<template>
    <div class="flex gap-6">
        <aside class="flex flex-col items-center justify-center">
            <div class="w-64 shrink-0 rounded-xl bg-slate-800 p-6 mb-2">
                <ProfileAside />
            </div>

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
        </aside>


        <section class="flex-1 rounded-xl bg-slate-800 p-8">
            <h1 class="text-2xl font-bold ">
                Meus Status
            </h1>

            <BaseChart />
        </section>
    </div>
</template>