import api from './api';

export async function getUsers() {
    const response = await api.get("/User");
    return response.data;
}