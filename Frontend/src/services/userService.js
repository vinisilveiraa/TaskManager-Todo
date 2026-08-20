import api from './api';

export async function getUsers() {
    const response = await api.get("/User");
    return response.data;
}

export async function getCurrentUser() {
    const response = await api.get("/User/me")
    return response.data
}

export async function setAvatar(file) {
    const formData = new FormData();
    formData.append("file", file); // transforma o file em uma requisicao multipart/form-data

    const response = await api.post("/User/avatar", formData);
    return response.data;
}

export async function putUser(id, user) {
    const response = await api.put(`/User/${id}`, user);
    return response.data;
}

export async function patchPassword(request) {
    const response = await api.patch("/User/me/password", request);
    return response.data;
}

export async function getUserStats() {
    const response = await api.get("/Profile/me/stats")
    return response.data;
}