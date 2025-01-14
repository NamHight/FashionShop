import { publicAxios } from "../../libs/Config/AxiosConfig";

var URL_CUSTOMER = "favorite/";
export const  getFavoriteById = async (id) => {
  try {
    const { data: response } = await publicAxios.get(
      URL_CUSTOMER + `?id=${id}`
    );
    return await response;
  } catch (e) {
    return await e.response;
  }
};
export const addFavorite = async (productId, customerId) => {
  try {
    console.log("Sending request to add favorite:", { productId, customerId });
    const { data: response } = await publicAxios.post(URL_CUSTOMER, {
      productId: productId,
      customerId: customerId,
    });

    console.log("Response from addFavorite:", response);

    // Kiểm tra nếu phản hồi có message thành công
    if (response?.message === "Yêu thích sản phẩm thành công") {
      return { success: true };
    }

    throw new Error(response?.message || "Unexpected response from API");
  } catch (e) {
    console.error("Error in addFavorite:", e.response?.data || e.message);
    throw new Error(e.response?.data?.message || "Failed to add to favorites");
  }
};


export const removeFavorite = async (userId, productId) => {
  try {
    const response = await publicAxios.delete(URL_CUSTOMER, {
      params: { userId, productId }, // Chuyển sang body nếu server không hỗ trợ params
    });
    return response.data;
  } catch (error) {
    console.error("Failed to remove from favorites:", error.response?.data || error.message);
    throw new Error(error.response?.data?.message || "Failed to remove from favorites");
  }
};