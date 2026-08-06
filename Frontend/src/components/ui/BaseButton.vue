<script setup>
defineProps({
    type: {
        type: String,
        default: "button",
    },
    variant: {
        type: String,
        default: "primary",
    },
});
</script>

<template>
    <button :type="type" :class="[
        'rounded-lg py-3 px-3 font-semibold text-white transition flex items-center justify-center gap-2 cursor-pointer',
        variant === 'primary' && 'bg-violet-600 hover:bg-violet-500',
        variant === 'success' && 'bg-green-600 hover:bg-green-500',
        variant === 'danger' && 'bg-red-600 hover:bg-red-500',
        variant === 'info' && 'bg-blue-600 hover:bg-blue-500',
        variant === 'disabled' && 'bg-gray-600 hover:bg-gray-500',
        variant === 'warning' && ' bg-yellow-600 hover:bg-yellow-500',
    ]">
        <slot />
    </button>
</template>
