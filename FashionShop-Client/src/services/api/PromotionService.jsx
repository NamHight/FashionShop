import { publicAxios } from "../../libs/Config/AxiosConfig";
const END_POINT = {
    GetPromotion : "/Promotions",
}
export const getAllPromotion = async (paramPromotionDto) => {
    return await publicAxios.get(`${END_POINT.GetPromotion}`,paramPromotionDto);
}
export const getByIdPromotion = async (id) =>{
    return await publicAxios.get(`${END_POINT.GetPromotion}/${id}`);
}