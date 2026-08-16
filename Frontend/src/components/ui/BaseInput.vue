<script setup>
import { computed, ref } from "vue";

const props = defineProps({
    modelValue: String,
    label: String,
    placeHolder: String,

    type: {
        type: String,
        default: "text",
    },

    variant: {
        type: String,
        default: "primary",
    },
});
const showPassword = ref(false);

const emit = defineEmits(["update:modelValue"]);

const variants = {
    primary: "outline-0",
    error: "outline-1 outline-red-600 animate-pulse",
};

const inputType = computed(() => {
    if (props.type !== "password") {
        return props.type;
    }

    return showPassword.value ? "text" : "password";
});

function togglePassword() {
    showPassword.value = !showPassword.value;
}
</script>

<template>
    <div>
        <label v-if="label" class="block mb-2 text-slate-300">
            {{ label }}
        </label>

        <div class="relative">
            <input :value="modelValue" :type="inputType" @input="emit('update:modelValue', $event.target.value)"
                v-bind="$attrs" :class="[
                    'w-full rounded-lg border border-slate-700 bg-slate-800 p-3',
                    'border border-slate-600 focus:border-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500/30',
                    variants[variant] ?? variants.primary,
                ]" />

            <button v-if="type === 'password'" type="button" @click="togglePassword"
                class="absolute right-3 top-1/2 -translate-y-1/2 text-slate-400 hover:text-slate-200">
                <i :class="['fa-solid', showPassword ? 'fa-eye-slash' : 'fa-eye']"></i>
            </button>
        </div>
    </div>
</template>
