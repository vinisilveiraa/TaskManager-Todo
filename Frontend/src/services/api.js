import axios from "axios";
import { getAccessToken, getRefreshToken, saveSession, clearSession } from "../utils/session";

const api = axios.create({
    baseURL: "http://localhost:5122/api"
});

api.interceptors.request.use(config => {
    const token = getAccessToken();

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
});

api.interceptors.response.use(
    response => response,

    async error => {
        if (error.response?.status !== 401) {
            return Promise.reject(error);
        }

        try {
            const originalRequest = error.config;

            if (originalRequest.url.includes("/Auth/login") ||
                originalRequest.url.includes("/Auth/logout") ||
                originalRequest.url.includes("/Auth/refresh")
            ) {
                return Promise.reject(error);
            }

            const response = await axios.post(
                "http://localhost:5122/api/Auth/refresh",
                {
                    refreshToken: getRefreshToken()
                }
            );

            saveSession(response.data);
            originalRequest.headers.Authorization = `Bearer ${getAccessToken()}`;
            return api(originalRequest);

        } catch (err) {
            clearSession();
            return Promise.reject(err);
        }
    }
);


export default api;