import {publicAxios} from "../../libs/Config/AxiosConfig";

const BASE_WEBSITE_INFO_URL = "WebsiteInfo";

export const getWebsiteInfo = async () =>{
    try {
        const {data: response} = await publicAxios.get(BASE_WEBSITE_INFO_URL);
        return response;
    }catch (e) {
        return e.response.data;
    }
}