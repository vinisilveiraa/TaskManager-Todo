<script setup>

defineProps({
    modelValue: String,
    label: String,
    placeHolder: String,

    type: {
        type: String,
        default: "text"
    },

    variant: {
        type: String,
        default: "primary"
    }
});

const variants = {
    primary: "outline-0",
    error: "outline-1 outline-red-600 animate-pulse"
}

const emit = defineEmits(["update:modelValue"]);
</script>

<template>

    <div class="">
        <label v-if="label" class="block mb-2 text-slate-300">
            {{ label }}
        </label>

        <input :value="modelValue" :type="type" @input="emit('update:modelValue', $event.target.value)" v-bind="$attrs"
            :class="['w-full rounded-lg border border-slate-700 bg-slate-800 p-3 ', variants[variant] ?? variants.primary]" />
    </div>

</template>