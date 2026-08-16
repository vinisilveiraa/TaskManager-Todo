<script setup>
import { computed } from 'vue';

const props = defineProps({
    avatarUrl: String,
    avatarPreview: String,
    editable: {
        type: Boolean,
        default: false
    },
    size: {
        type: String,
        default: "profile"
    }
});

const emit = defineEmits(["click"]);

const sizes = {
    icon: "w-7 h-7 text-md",
    profile: "w-32 h-32 text-5xl"
}

const displayedAvatar = computed(() => {
    return props.avatarPreview || props.avatarUrl;
});

const imageSrc = computed(() => {
    if (!displayedAvatar.value) return null;

    //
    if (displayedAvatar.value.startsWith("blob:")) {
        return displayedAvatar.value;
    }

    return `http://localhost:5122${displayedAvatar.value}`
})

</script>

<template>
    <div :class="['group relative bg-white rounded-full overflow-hidden flex items-center justify-center',
        sizes[size],
        editable && 'cursor-pointer'
    ]" @click="editable && emit('click')">

        <img v-if="imageSrc" :src="imageSrc" alt="Avatar" class="h-full w-full object-cover" />
        <i v-else class="fa-solid fa-user text-slate-700 m-0"></i>



        <div v-if="editable"
            class="absolute inset-0 flex flex-col items-center justify-center bg-black/60  opacity-0 transition-opacity duration-200 group-hover:opacity-100">
            <i class="fa-solid fa-camera text-2xl"></i>
            <span class="mt-1 text-xs">Alterar foto</span>
        </div>
    </div>
</template>