import { authAxios, publicAxios } from "../../libs/Config/AxiosConfig";

var URL_CUSTOMER = "favorite/"
export const getFavoriteById = async (id) => {
    try{
        const {data: response} = await publicAxios.get(URL_CUSTOMER +`?id=${id}`)
        return await response;
    }
    catch(e){
        return await e.response;
    }
}