import { createRouter, createWebHistory } from 'vue-router';

import LoginView from '../views/LoginView.vue';
import TodoView from '../views/TodoView.vue';
import DashboardView from '../views/DashboardView.vue';
import { getAccessToken } from '../utils/session.js';
import { useUser } from '../composables/useUser.js';

const { userRole } = useUser();

const router = createRouter({
    history: createWebHistory(),

    routes: [
        {
            path: '/',
            component: TodoView
        },
        {
            path: '/login',
            component: LoginView
        },
        {
            path: '/todo',
            component: TodoView,
            meta: {
                requiresAuth: true
            }
        },
        {
            path: '/dashboard',
            component: DashboardView,
            meta: {
                requiresAuth: true,
                role: "Admin"
            }
        }
    ]

});

// beforeEach adiciona um "navigation guard" que executa a cada navegacao
router.beforeEach((to) => {
    if (to.meta.requiresAuth && !getAccessToken()) {
        return "/login";
    }
    if (to.meta.role && userRole.value !== to.meta.role) {
        return "/todo";
    }
});

export default router;