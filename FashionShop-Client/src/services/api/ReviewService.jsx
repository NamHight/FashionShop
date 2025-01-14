import { authAxios, publicAxios } from "../../libs/Config/AxiosConfig"

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
      console.log(END_POINT.GetReviewByProductId);
      const response = await publicAxios.post(END_POINT.GetReviewByProductId, {
        productId,
        rating,
        reviewText,
        customerId,
      });
      console.log(response.data)
      return response.data;
    } catch (error) {
      console.error("Error adding review:", error);
      throw error;
    }
  };
  export const checkIfPurchased = async (customerId, productId) => {
    try {
      const url = `${BASE_ORDERS_URL}?customerId=${customerId}&productId=${productId}`;
      console.log("Request URL:", url);
  
      const response = await authAxios.get(url);
  
      if (response?.status === 200) {
        const result = response.data;
        console.log("API Response:", result);
  
        // Trả về true nếu thông báo xác nhận rằng người dùng đã mua sản phẩm
        return result.message === 'You can review this product.';
      }
  
      // Nếu không phải status 200, trả về false
      return false;
    } catch (error) {
      console.error("Error checking purchase status:", error);
      // Trả về false nếu có lỗi
      return false;
    }
  };
  
  