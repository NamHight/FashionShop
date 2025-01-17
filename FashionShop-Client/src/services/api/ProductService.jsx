import { authAxios } from "../../libs/Config/AxiosConfig";
import { publicAxios } from "../../libs/Config/AxiosConfig"; // Đổi URL nếu backend chạy trên cổng khác
const BASE_PRODUCT_URL = "Products";
const BASE_PRODUCT_SEARCH = "search";
export const getAllProducts = async () => {
  try {
    const response = await authAxios.get(BASE_PRODUCT_URL);
    return response.data;
  } catch (error) {
    console.error("Error fetching products:", error);
    throw error;
  }
};

export const getProductDetails = async (categorySlug, productSlug) => {
  const response = await publicAxios.get(
    `${BASE_PRODUCT_URL}/${categorySlug}/${productSlug}`
  ); // Include API_URL here
  return response.data;
};
export const getProductsBySlug = async (categorySlug) => {
  const response = await publicAxios.get(
    `http://localhost:7068/Products/${categorySlug}`
  );
  return response.data;
};
export const increaseProductView = async (
  productId,
  customerId = null,
  sessionId = null
) => {
  try {
    // Gửi yêu cầu tăng lượt xem tới API
    const response = await authAxios.post(
      `${BASE_PRODUCT_URL}/${productId}/increment-view`,
      null,
      {
        params: {
          customerId, // Nếu người dùng đã đăng nhập
          sessionId, // Nếu người dùng chưa đăng nhập
        },
      }
    );

    return response.data;
  } catch (error) {
    console.error("Error increasing product view:", error);
    throw error;
  }
};

export const getProductStats = async (productId) => {
  try {
    const response = await authAxios.get(
      `${BASE_PRODUCT_URL}/${productId}/stats`
    );
    return response.data; // Trả về dữ liệu thống kê
  } catch (error) {
    console.error("Error fetching product stats:", error);
    throw error;
  }
};

export const searchProductByName = async (name, sortOrder = "desc") => {
  try {
    const response = await publicAxios.get(
      `${BASE_PRODUCT_URL}/SearchProductName?searchTerm=${name}&sortOrder=${sortOrder}`
    );
    return response;
  } catch (e) {
    return e.response;
  }
};

export const searchProductName = async ({productName, categoryName,page,limit, minPrice, maxPrice}) => {
  try {
    const response = await publicAxios.get(`${BASE_PRODUCT_URL}/search?searchProduct=${productName}${categoryName ? '&categoryName=' + categoryName : ''}${minPrice ? '&minPrice='+minPrice : ''}${maxPrice ? '&maxPrice=' + maxPrice : ''}${page ? '&pageNumber='+page : ''}${limit ? '&pageSize='+limit : ''}`)
    return response;
  } catch (ex) {
    return ex.response;
  }
};
