import {authAxios, publicAxios} from "../../libs/Config/AxiosConfig";

const BASE_CATEGORY_URL = "Categories";


export const getCategories = async () =>{
    try {
        const response= await publicAxios.get(BASE_CATEGORY_URL+'/All');
        return await response.data;
    }catch (e){
        return await e.response;
    }
}
