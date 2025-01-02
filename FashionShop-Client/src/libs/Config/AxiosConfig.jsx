import axios from "axios";
import Cookies from "js-cookie";
import {setCookieHandler} from "../Helpers/CookieConfig";
import {refreshToken, tokenProtection} from "../../services/api/TokenService";


export const BASE_URL = "https://localhost:7068/api/";


export const authAxios = axios.create({
    baseURL: BASE_URL,
    headers: {
        "Content-Type": "application/json"
    },
    withCredentials: true,
});
authAxios.interceptors.request.use(
    async (config) => {
    const result = localStorage.getItem("token");
    if(result?.token){
        config.headers.Authorization = `Bearer ${result?.token}`;
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
        console.log(error.response);
        if(error.response && error.response.status === 401){
            try {
                const refresh = await refreshToken();
                if(!refresh.token){
                    return Promise.reject(error);
                }
                localStorage.setItem("token",refresh?.token);
                const config = error.config;
                config.headers.Authorization = `Bearer ${refresh.token}`;
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
    },
    withCredentials: true
});
publicAxios.interceptors.response.use(
    (response) => response,
     (error) => {
        if(axios.isAxiosError(error)){
            return Promise.reject(error);
        }
    });