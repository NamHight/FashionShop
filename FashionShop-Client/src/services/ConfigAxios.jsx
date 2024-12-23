import axios from "axios";
import Cookies from "js-cookie";
import {refreshToken} from "./api/AuthServices";
import {setCookieHandler} from "../libs/Helpers/CookieConfig";


export const BASE_URL = "https://localhost:7068/api/";


export const authAxios = axios.create({
    baseURL: BASE_URL,
    headers: {
        "Content-Type": "application/json"
    }
});
authAxios.interceptors.request.use(
    async (config) => {
    const token = Cookies.get('access_token');
    if(token){
        config.headers.Authorization = `Bearer ${token}`;
    }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
)

authAxios.interceptors.response.use(
    (response) => response,
    async (error) => {
        if(error.response && error.response.status === 401){
            try {
                const refreshToken = Cookies.get('refresh_token');
                if(!refreshToken){
                    throw new Error("No refresh token found");
                }
            const newToken = refreshToken({refreshToken});
                if(!newToken){
                    return Promise.reject(error);
                }
                await setCookieHandler({refresh_token: newToken});
                const config = error.config;
                config.headers.Authorization = `Bearer ${newToken}`;
                return authAxios.request(config);

            }catch (error) {
                return Promise.reject(error);
            }
        }
    }
);

export const publicAxios = axios.create({
    baseURL: BASE_URL,
    headers: {
        "Content-Type": "application/json"
    }
});