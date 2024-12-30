import {authAxios} from "../../libs/Config/AxiosConfig";

const BASE_CATEGORY_URL = "Categories";


export const getCategories = async () =>{
    try {
        const response= await authAxios.get(BASE_CATEGORY_URL);
        return await response.data;
    }catch (e){
        return await e.response;
    }
}