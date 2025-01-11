//import {authAxios} from "../../libs/Config/AxiosConfig";
import axios from "axios";
import {publicAxios} from "../../libs/Config/AxiosConfig";
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

export const searchProductByName = async (name,sortOrder = "desc") => {
  try {
    const response = await publicAxios.get(`${API_URL}/SearchProductName?searchTerm=${name}&sortOrder=${sortOrder}`);
    return response;
  }catch (e) {
    return e.response;
  }
}