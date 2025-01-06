import { publicAxios } from "../../libs/Config/AxiosConfig"
const END_POINT = {
    GetArticle : "/Articles",
}
export const GetPageAndSearchArticle = (paramArticleDto)=>{
    return publicAxios.get(`${END_POINT.GetArticle}`,paramArticleDto);
}