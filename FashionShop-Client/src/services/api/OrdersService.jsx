import { publicAxios } from "../../libs/Config/AxiosConfig";

var URL_CUSTOMER = "orders/";
export const getOrdersByIdAndStatus = async (id, status) => {
  try {
    const { data: response } = await publicAxios.get(
      URL_CUSTOMER + `${id}/${status}`
    );
    return response;
  } catch (e) {
    return await e.response;
  }
};
