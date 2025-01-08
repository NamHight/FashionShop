import {authAxios} from "../../libs/Config/AxiosConfig";
import axios from "axios";
const API_URL = "http://localhost:5250/api/products"; // Đổi URL nếu backend chạy trên cổng khác

export const getAllProducts = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error("Error fetching products:", error);
    throw error;
  }
};