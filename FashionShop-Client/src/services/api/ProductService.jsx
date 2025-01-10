//import {authAxios} from "../../libs/Config/AxiosConfig";
import axios from "axios";
const API_URL = "http://localhost:7068/api/Products"; // Đổi URL nếu backend chạy trên cổng khác


export const getAllProducts = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error("Error fetching products:", error);
    throw error;
  }
};
export const getProductDetails = async (categorySlug, productSlug) => {
  const response = await axios.get(`${API_URL}/${categorySlug}/${productSlug}`);  // Include API_URL here
  return response.data;
};