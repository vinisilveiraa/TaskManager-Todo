<script setup>
import { ref } from "vue";
import ProfileHeader from "../components/user/ProfileHeader.vue";
import UserProfile from "../components/user/UserProfile.vue";
import ProfileEdit from "../components/user/ProfileEdit.vue";

const activeTab = ref('profile');

function changeTab(tab) {
    activeTab.value = tab;
}

</script>

<template>
    <main class="w-full max-w-6xl mx-auto px-10 py-8">
        <ProfileHeader @change="changeTab" :active="activeTab" />

        <UserProfile v-if="activeTab === 'profile'" />
        <ProfileEdit v-if="activeTab === 'edit'" />

    </main>
</template>