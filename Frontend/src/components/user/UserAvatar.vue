<script setup>

defineProps({
    avatarUrl: String,
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

</script>

<template>
    <div :class="['group relative bg-white rounded-full overflow-hidden flex items-center justify-center',
        sizes[size],
        editable && 'cursor-pointer'
    ]" @click="editable && emit('click')">

        <img v-if="avatarUrl" :src="`http://localhost:5122${avatarUrl}`" alt="Avatar"
            class="h-full w-full object-cover" />
        <i v-else class="fa-solid fa-user text-slate-700 m-0"></i>



        <div v-if="editable"
            class="absolute inset-0 flex flex-col items-center justify-center bg-black/60  opacity-0 transition-opacity duration-200 group-hover:opacity-100">
            <i class="fa-solid fa-camera text-2xl"></i>
            <span class="mt-1 text-xs">Alterar foto</span>
        </div>
    </div>
</template>