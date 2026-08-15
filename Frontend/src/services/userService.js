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