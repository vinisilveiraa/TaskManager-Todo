import { createApp, onMounted } from 'vue'
import { useUser } from './composables/useUser.js';

const { initializeUser } = useUser();

import './style.css'
import "@fortawesome/fontawesome-free/css/all.min.css";

import App from './App.vue'
import router from './router'

await initializeUser();

createApp(App)
    .use(router)
    .mount('#app')
