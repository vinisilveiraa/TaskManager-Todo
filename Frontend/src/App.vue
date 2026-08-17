<script setup>
import { RouterView } from 'vue-router'
import Navbar from './components/layout/Navbar.vue';
import BaseToast from './components/ui/BaseToast.vue';
import { useToast } from './composables/useToast.js';

const { toast, close } = useToast();

</script>

<template>

  <Navbar />
  <RouterView />

  <BaseToast v-if="toast" :type="toast.type" :title="toast.title" :message="toast.message" :duration="toast.duration"
    @close="close" />
</template>
