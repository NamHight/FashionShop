import { authAxios } from "../../libs/Config/AxiosConfig";
const END_POINT = {
    GetPromotion : "/Promotions",
}
export const getAllPromotion = (paramPromotionDto) => {
    return authAxios.get(`${END_POINT.GetPromotion}`,paramPromotionDto);
}
export const getByIdPromotion = (id) =>{
    return authAxios.get(`${END_POINT.GetPromotion}/${id}`);
}