<script setup>
import { onMounted, onBeforeUnmount, ref } from "vue";

const props = defineProps({
    title: {
        type: String,
        default: "Sucesso"
    },

    message: {
        type: String,
        default: ""
    },

    type: {
        type: String,
        default: "success"
    },

    duration: {
        type: Number,
        default: 4000
    }
});

const emit = defineEmits(["close"]);

const progress = ref(100);
let interval = null;
let timeout = null;

const styles = {
    success: {
        icon: "fa-solid fa-circle-check",
        iconColor: "text-green-400",
        border: "border-green-500",
        progress: "bg-green-500"
    },

    error: {
        icon: "fa-solid fa-circle-xmark",
        iconColor: "text-red-400",
        border: "border-red-500",
        progress: "bg-red-500"
    },

    warning: {
        icon: "fa-solid fa-triangle-exclamation",
        iconColor: "text-yellow-400",
        border: "border-yellow-500",
        progress: "bg-yellow-500"
    },

    info: {
        icon: "fa-solid fa-circle-info",
        iconColor: "text-blue-400",
        border: "border-blue-500",
        progress: "bg-blue-500"
    }
};

const currentStyle = styles[props.type] ?? styles.success;

function close() {
    clearInterval(interval);
    clearTimeout(timeout);

    emit("close");
}

onMounted(() => {
    const step = 100 / (props.duration / 50);

    interval = setInterval(() => {
        progress.value -= step;

        if (progress.value <= 0) {
            progress.value = 0;
            clearInterval(interval);
        }
    }, 50);

    timeout = setTimeout(() => {
        close();
    }, props.duration);
});

onBeforeUnmount(() => {
    clearInterval(interval);
    clearTimeout(timeout);
});
</script>

<template>
    <div @click="close"
        class="fixed bottom-6 right-6 z-50 w-96 overflow-hidden rounded-xl border border-slate-700 bg-slate-800 shadow-xl cursor-pointer"
        :class="currentStyle.border">

        <div class="flex items-start gap-3 p-4">

            <i :class="[
                currentStyle.icon,
                currentStyle.iconColor,
                'text-xl mt-0.5'
            ]"></i>

            <div class="flex-1">
                <h3 class="font-semibold text-white">
                    {{ title }}
                </h3>

                <p v-if="message" class="mt-1 text-sm text-slate-300">
                    {{ message }}
                </p>
            </div>

            <button type="button" @click.stop="close" class="text-slate-500 hover:text-slate-300 transition">
                <i class="fa-solid fa-xmark"></i>
            </button>

        </div>

        <!-- Barra de duração -->
        <div class="h-1 w-full bg-slate-700">
            <div :class="currentStyle.progress" class="h-full transition-[width] duration-50"
                :style="{ width: `${progress}%` }"></div>
        </div>

    </div>
</template>