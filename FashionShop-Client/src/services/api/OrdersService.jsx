import { authAxios, publicAxios } from "../../libs/Config/AxiosConfig";

var URL_OrderByIdStatus = "orders/";
var URL_OrderCancel = "orders/ordercancel"
export const getOrdersByIdAndStatus = async (id, status) => {
  try {
    const { data: response } = await publicAxios.get(
      URL_OrderByIdStatus + `${id}/${status}`
    );
    return response;
  } catch (e) {
    return await e.response;
  }
};

export const createOrderCancel = async (value) => {
  try{
    const {data: response} = await publicAxios.post(URL_OrderCancel,value);
    return response;
  }
  catch(ex){
    return await ex.response;
  }
}

export const addOrders = async (order) => {
  try {
    const response = await authAxios.post( URL_CUSTOMER + "addOrders", order);
    return response;
  } catch (e) {
    return await e.response;
  }
};

