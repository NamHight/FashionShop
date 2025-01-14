import { publicAxios } from "../../libs/Config/AxiosConfig"

const BASE_ORDERS_URL = "Orders";
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

export const addReview = async (productId, rating, reviewText,customerId) => {
    try {
      const response = await publicAxios.post(END_POINT.GetReviewByProductId, {
        productId,
        rating,
        reviewText,
        customerId,
      });
      return response.data;
    } catch (error) {
      console.error("Error adding review:", error);
      throw error;
    }
  };
  export const checkIfPurchased = async (customerId, productId) => {
    try {
      const response = await fetch(`${BASE_ORDERS_URL}?customerId=${customerId}&productId=${productId}`);
      if (!response.ok) {
        throw new Error('Failed to fetch purchase data');
      }
      const result = await response.json();
      if (result && result.status === 'completed') {
        return true;
      }
      return false;
    } catch (error) {
      console.error("Error checking purchase status:", error);
      throw error;
    }
  };
