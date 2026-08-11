import api from './api';

export async function login(userName, password) {
    const response = await api.post("/Auth/login", {
        userName, password
    });

    return response.data;
}

export async function logoutApi() {
    await api.post("/Auth/logout");
}

export async function register(username, password) {
    const response = await api.post("/Auth/register", {
        username, password
    });

    return response.data
}