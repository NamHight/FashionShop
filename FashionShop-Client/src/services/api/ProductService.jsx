//import {authAxios} from "../../libs/Config/AxiosConfig";
import {publicAxios} from "../../libs/Config/AxiosConfig"; // Đổi URL nếu backend chạy trên cổng khác
const BASE_PRODUCT_URL = "Products";

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
  const response = await publicAxios.get(`${BASE_PRODUCT_URL}/${categorySlug}/${productSlug}`);  // Include API_URL here
  return response.data;
};
export const getProductsBySlug = async (categorySlug) => {
  const response = await publicAxios.get(`http://localhost:7068/Products/${categorySlug}`);
  return response.data;
};
export const searchProductByName = async (name,sortOrder = "desc") => {
  try {
    const response = await publicAxios.get(`${BASE_PRODUCT_URL}/SearchProductName?searchTerm=${name}&sortOrder=${sortOrder}`);
    return response;
  }catch (e) {
    return e.response;
  }
}
