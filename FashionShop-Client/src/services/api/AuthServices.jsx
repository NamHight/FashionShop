import {authAxios, publicAxios} from "../../libs/Config/AxiosConfig";
const BASE_AUTHENTICATE_URL = "Authenticate/";


export const login = async (data,remember = false) =>{
    try {
        const {data:response} = await publicAxios.post(BASE_AUTHENTICATE_URL+`login?remember=${remember}`,data);
        return await response;
    }catch (e){
        return await e.response;
    }
}

export const logout = async (id) =>{
    try {
        const {data:response} = await authAxios.post(BASE_AUTHENTICATE_URL+`Logout?id=${id}`,null,{"Content-Type": "text/plain; charset=utf-8 "});
        return await response;
    }catch (e) {
        return await e.response?.data;
    }
}

export const signup = async (data) => {
    try {
        const {data:response} = await publicAxios.post(BASE_AUTHENTICATE_URL+`Register`,data);
        return await response;
    }catch (e) {
        return await e.response?.data;
    }
}

export const loginGoogle = async (data) =>{
  try {
      const response = await publicAxios.post(BASE_AUTHENTICATE_URL+`LoginGoogle`,data);
      return response;
  }  catch (e) {
      return await e.response;
  }
};