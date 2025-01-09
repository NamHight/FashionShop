import { publicAxios } from "../../libs/Config/AxiosConfig"
const END_POINT = {
    GetArticle : "/Articles",
}
export const GetPageAndSearchArticle = async (paramArticleDto)=>{
    try {
        const response = await publicAxios.get(`${END_POINT.GetArticle}`,paramArticleDto);
        return response;
    } catch (error) {
        console.log("error service: ",error.response.data);
        return await error.response.data;
    }
}