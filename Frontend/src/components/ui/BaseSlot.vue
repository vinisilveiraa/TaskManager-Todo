<script setup>

defineProps({
    title: String,
    loading: Boolean
})

</script>

<template>
    <div class="w-full shrink-0 rounded-xl bg-slate-800 px-2 py-4 text-center flex flex-col">
        <span class="text-md font-semibold"> {{ title ?? 'title' }} </span>
        <span class="text-2xl font-bold text-violet-400 flex justify-center">
            <slot v-if="!loading" />
            <div v-else class="animate-pulse bg-slate-700 h-8 rounded-xl"></div>
        </span>
    </div>
</template>