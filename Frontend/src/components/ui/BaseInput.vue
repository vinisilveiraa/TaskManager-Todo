<script setup>

defineProps({
    modelValue: String,
    label: String,
    placeHolder: String,

    type: {
        type: String,
        default: "text"
    }
});

const emit = defineEmits(["update:modelValue"]);
</script>

<template>

    <div class="">
        <label v-if="label" class="block mb-2 text-slate-300">
            {{ label }}
        </label>

        <input :value="modelValue" :type="type" @input="emit('update:modelValue', $event.target.value)" v-bind="$attrs"
            class="w-full rounded-lg border border-slate-700 bg-slate-800 p-3 text-white" />
    </div>

</template>