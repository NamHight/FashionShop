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