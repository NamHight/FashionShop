import { authAxios } from "../../libs/Config/AxiosConfig";
const END_POINT = {
    GetPromotion : "/Promotions",
}
export const getAllPromotion = (paramPage) => {
    return authAxios.get(`${END_POINT.GetPromotion}`,paramPage);
}
export const getByIdPromotion = (id) =>{
    return authAxios.get(`${END_POINT.GetPromotion}/${id}`);
}