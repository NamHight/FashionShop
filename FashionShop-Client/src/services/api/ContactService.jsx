import { authAxios } from "../../libs/Config/AxiosConfig"

const END_POINT = {
    ADDCONTACT : "Contacts",
}

export const postAddContact = (contact) =>{
    return authAxios.post(`${END_POINT.ADDCONTACT}`,contact);
}