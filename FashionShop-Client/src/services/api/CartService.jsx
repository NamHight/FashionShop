import {authAxios} from "../../libs/Config/AxiosConfig";

const BASE_CART_URL = "carts/";


export const getAllCarts = async () =>{
    try {
        const response= await authAxios.get(BASE_CART_URL+'getAllCarts');
        return await response.data;
    }catch (e){
        return await e.response;
    }
}

export const addCart = async (data) =>{
    try {
        const response= await authAxios.post(BASE_CART_URL + 'addCart', data);
        return await response.data;
    }catch (e){
        return await e.response;
    }
}

export const removeCarts = async (data) =>{
    try {
        const response= await authAxios.delete(BASE_CART_URL + 'removeCarts', data);
        return await response.data;
    }catch (e){
        return await e.response;
    }
}

export const removeAllCarts = async () =>{
    try {
        const response= await authAxios.post(BASE_CART_URL + 'removeAllCarts');
        return await response.data;
    }catch (e){
        return await e.response;
    }
}

export const updateCarts = async (data) =>{
    try {
        const response= await authAxios.post(BASE_CART_URL + 'updateCarts', data);
        return await response.data;
    }catch (e){
        return await e.response;
    }
}