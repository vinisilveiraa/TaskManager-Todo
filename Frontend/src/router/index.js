import { createRouter, createWebHistory } from 'vue-router';

import { getAccessToken } from '../utils/session.js';
import { useUser } from '../composables/useUser.js';

import HomeView from '../views/HomeView.vue';

import LoginView from '../views/LoginView.vue';
import RegisterView from '../views/RegisterView.vue';

import TodoView from '../views/TodoView.vue';

import DashboardView from '../views/DashboardView.vue';

import UserProfileView from '../views/UserProfileView.vue';
import UserProfile from '../components/user/UserProfile.vue';
import ProfileEdit from '../components/user/ProfileEdit.vue';

const { userRole, isAuthenticated } = useUser();

const router = createRouter({
    history: createWebHistory(),

    routes: [
        {
            path: '/',
            component: HomeView
        },
        {
            path: '/login',
            component: LoginView
        },
        {
            path: '/register',
            component: RegisterView
        },
        {
            path: '/todo',
            component: TodoView,
            meta: {
                requiresAuth: true
            }
        },
        {
            path: '/me',
            component: UserProfileView,
            children: [
                {
                    path: '',
                    redirect: '/me/profile'
                },
                {
                    path: 'profile',
                    name: 'profile',
                    component: UserProfile
                },
                {
                    path: 'edit',
                    name: 'edit',
                    component: ProfileEdit
                }
            ],
            meta: {
                requiresAuth: true
            }
        },
        {
            path: '/dashboard',
            component: DashboardView,
            meta: {
                requiresAuth: true,
                role: 1
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
    if (to.path === '/') {
        return (!isAuthenticated) ? "/login" : "/todo"
    }
});

export default router;