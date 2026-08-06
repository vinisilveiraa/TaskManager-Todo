import api from '../services/api';

export async function getItems() {
    const response = await api.get("/Item");
    return response.data;
}

export async function getMyItems(query) {
    const response = await api.get("/Item/me", { params: query });
    return response.data;
}

export async function createItem(item) {
    const response = await api.post("/Item", item);
    return response.data;
}

export async function deleteItem(id) {
    const response = await api.delete(`/Item/${id}`);
    return response.data;
}

export async function patchItemStatus(id) {
    const response = await api.patch(`/Item/${id}`);
    return response.data;
}

export async function putItem(id, item) {
    const response = await api.put(`/item/${id}`, item)
    return response.data;
}