import {publicAxios} from "../../libs/Config/AxiosConfig";

const BASE_TOKEN_URL = "Token/";

export const tokenProtection = async () =>{
    try {
        const {data: response} = await publicAxios.get(BASE_TOKEN_URL+"ProtectData");
        return await response;
    }catch (e){
        return await e.response;
    }
}
export const refreshToken = async () => {
    try{
        const {data: response} = await publicAxios.post(BASE_TOKEN_URL+"RefreshToken");
        return await response;
    }catch (e) {
        return await e.response;
    }
}