import {publicAxios} from "../ConfigAxios";


export const refreshToken = async (data) => {
    try{
    const response = await publicAxios.post("auth/refresh-token",data,{
        headers:{
            "Content-Type": "application/json"
        }
    });
    return await response.data();
    }catch (e) {
        console.log(e);
        throw new Error(e);
    }
}