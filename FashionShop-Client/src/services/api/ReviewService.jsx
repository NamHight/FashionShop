import { publicAxios } from "../../libs/Config/AxiosConfig"


const END_POINT = {
    GetReviewByProductId : "/Reviews",
    GetTotalReviewRating : "/Reviews/TotalReviewRating",
}

export const GetReviewByProductId = async (paramReview) => {
    try{
        const response = await publicAxios.get(`${END_POINT.GetReviewByProductId}`,paramReview);
        return response;
    }catch(error){
        console.log("error get reviewbyproductid :" ,error.response.data);
        return error.response.data;
    }
}

export const GetTotalReviewRating = async (productId) => {
    try{
        const response = await publicAxios.get(`${END_POINT.GetTotalReviewRating}/${productId}`);
        return response;
    }
    catch(error){
        console.log("error get totalReviewRating: ", error.response.data);
        return error.response.data;
    }
}
