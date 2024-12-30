import {authAxios} from "../../libs/Config/AxiosConfig";

const URL_CUSTOMER = "customer/";
export const getCustomerById = async (id) =>{
    try {
        const {data: response} = await authAxios.get(URL_CUSTOMER+`?id=${id}`);
        return await response;
    }catch (e) {
        return await e.response;
    }
}