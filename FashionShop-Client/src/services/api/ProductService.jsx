import {authAxios} from "../../libs/Config/AxiosConfig";
const BASE_Products_URL = "Products";

export const getAllProducts = async () => {
  try {
    const response = await authAxios.get(BASE_Products_URL);
    return response.data;
  } catch (error) {
    console.error("Error fetching products:", error);
    throw error;
  }
};
export const getProductDetails = async (categorySlug, productSlug) => {
  const response = await authAxios.get(`${BASE_Products_URL}/${categorySlug}/${productSlug}`);  // Include API_URL here
  return response.data;
};
export const increaseProductView = async (productId, customerId = null, sessionId = null) => {
  try {
    // Gửi yêu cầu tăng lượt xem tới API
    const response = await authAxios.post(`${BASE_Products_URL}/${productId}/increment-view`, null, {
      params: {
        customerId,  // Nếu người dùng đã đăng nhập
        sessionId    // Nếu người dùng chưa đăng nhập
      }
    });

    return response.data;
  } catch (error) {
    console.error('Error increasing product view:', error);
    throw error;
  }
};

export const getProductStats = async (productId) => {
  try {
    const response = await authAxios.get(`${BASE_Products_URL}/${productId}/stats`);
    return response.data; // Trả về dữ liệu thống kê
  } catch (error) {
    console.error('Error fetching product stats:', error);
    throw error;
  }
};
