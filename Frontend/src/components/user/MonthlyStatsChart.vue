<script setup>
import { useUser } from '../../composables/useUser';
import { onMounted, computed } from 'vue';
import VueApexCharts from 'vue3-apexcharts';

const { stats, loadStats, loadingStats: loading } = useUser();


const formatDate = (dateStr) => {
    if (!dateStr) return '';
    const [year, month, day] = dateStr.split('T')[0].split('-');
    return `${day}/${month}`;
};

const activeDays = computed(() => {
    const list = stats.value?.monthlyActivity || [];
    return list.filter(item => item.completed > 0);
});

const chartOptions = computed(() => ({
    chart: {
        id: 'grafico-tarefas-mensal',
        type: 'bar',
        toolbar: { show: false },
        fontFamily: 'inherit',
        foreColor: '#94a3b8'
    },
    colors: ['#3b82f6'],
    plotOptions: {
        bar: {
            borderRadius: 6,
            columnWidth: '40%'
        }
    },
    dataLabels: {
        enabled: true,
        style: { colors: ['#fff'] }
    },
    grid: {
        borderColor: '#334155',
        strokeDashArray: 4
    },
    xaxis: {
        categories: activeDays.value.map(item => formatDate(item.date || item.Date)),
        axisBorder: { show: false },
        axisTicks: { show: false }
    },
    yaxis: {
        forceNiceScale: true,
        labels: {
            formatter: (val) => Math.floor(val)
        }
    },
    tooltip: {
        theme: 'dark',
        y: {
            formatter: (val) => `${val} tarefas concluídas`
        }
    }
}));

const series = computed(() => [
    {
        name: 'Tarefas Concluídas',
        data: activeDays.value.map(item => item.completed)
    }
]);
</script>

<template>
    <div class="w-full">
        <div v-if="loading" class="flex justify-center items-center h-64 text-slate-400">
            Carregando estatísticas...
        </div>

        <VueApexCharts v-else-if="activeDays.length > 0" width="100%" height="320" :type="chartOptions.chart.type"
            :options="chartOptions" :series="series" />

        <div v-else class="flex justify-center items-center h-64 text-slate-500">
            Nenhuma tarefa concluída neste período.
        </div>
    </div>
</template>