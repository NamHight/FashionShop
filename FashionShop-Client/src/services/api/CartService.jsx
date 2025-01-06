import {authAxios} from "../../libs/Config/AxiosConfig";

const BASE_CART_URL = "Carts/";


export const getAllCartsService = async () =>{
    try {
        const response= await authAxios.get(BASE_CART_URL+'getAllCarts');
        console.log("goi api de lay danh sach gio hang");
        return await response.data;
    }catch (e){
        return await e.response;
    }
}

export const addCartService = async (id, quantity) =>{
    try {
        const response= await authAxios.post(BASE_CART_URL + 'addCart' + `?id=${id}&quantity=${quantity}`);
        return await response.data;
    }catch (e){
        return await e.response;
    }
}

export const removeCartsService = async (id) =>{
    try {
        const response= await authAxios.delete(BASE_CART_URL + 'removeCarts/'+`${id}`);
        return await response.data;
    }catch (e){
        return await e.response;
    }
}

export const removeAllCartsService = async () =>{
    try {
        const response= await authAxios.post(BASE_CART_URL + 'removeAllCarts');
        return await response.data;
    }catch (e){
        return await e.response;
    }
}

export const saveCartsService = async (data) =>{
    try {
        const response= await authAxios.post(BASE_CART_URL + 'updateCart', data);
        return await response.data;
    }catch (e){
        return await e.response;
    }
}