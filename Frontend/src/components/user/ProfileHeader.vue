<script setup>

defineProps({
    active: {
        type: String,
        default: 'profile'
    }
});

const headerButton =
    'py-2 px-4 font-medium  transition flex items-center justify-center gap-2 cursor-pointer border-b-2 border-transparent';

const emit = defineEmits(['change'])

</script>

<template>

    <div class="w-full px-5 mb-8">
        <div class="flex gap-2 border-b border-slate-700">

            <span :class="[
                headerButton,
                active === 'profile'
                    ? 'border-b-2 border-violet-400 text-violet-400'
                    : 'text-slate-400 hover:'
            ]" @click="emit('change', 'profile')">
                Perfil
            </span>

            <span :class="[headerButton, active === 'edit' ? 'border-b-2 border-violet-400 text-violet-400'
                : 'text-slate-400 hover:'
            ]" @click="emit('change', 'edit')">
                Editar
            </span>

        </div>
    </div>

</template>