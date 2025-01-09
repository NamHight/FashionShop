import { publicAxios } from "../../libs/Config/AxiosConfig"
const END_POINT = {
    GetArticle : "/Articles",
}
export const GetPageAndSearchArticle = async (paramArticleDto)=>{
    return await publicAxios.get(`${END_POINT.GetArticle}`,paramArticleDto);
}