<script setup>

const props = defineProps({
    sections: {
        type: Array,
        required: true
    }
});

const css = "border-b-2 border-transparent px-4 py-2 text-slate-400 transition hover:text-white";

</script>

<template>

    <nav class="flex gap-2 border-b border-slate-700 mb-5">

        <RouterLink v-for="section in sections" :key="section.to" :to="section.to" :class="[css]"
            active-class="border-violet-400 text-violet-400">
            {{ section.label }}
        </RouterLink>

    </nav>

</template>