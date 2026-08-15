<script setup>
defineProps({
    icon: String,
    active: {
        type: Boolean,
        default: true
    },
    variant: {
        type: String,
        default: "primary"
    },
});

const icons = {
    circle: "fa-circle",
    check: "fa-check-circle",
    xmark: "fa-xmark",
    warning: "fa-exclamation-triangle",
    info: "fa-circle-info",
    disabled: "fa-ban"
}

const variants = {
    primary: "bg-violet-600 hover:bg-violet-500",
    success: "bg-green-600 hover:bg-green-500",
    danger: "bg-red-600 hover:bg-red-500",
    warning: "bg-yellow-600 hover:bg-yellow-500",
    info: "bg-blue-600 hover:bg-blue-500",
    disabled: "bg-gray-600 hover:bg-gray-500"
}

</script>
<template>
    <span :class="['flex items-center justify-center gap-0.5 rounded-full px-3 py-1 text-sm  transition',
        variants[variant] ?? variants.primary
    ]">

        <i v-if="icon" :class="['fa-solid', icons[icon] ?? 'fa-circle']"></i>
        <slot></slot>
    </span>
</template>