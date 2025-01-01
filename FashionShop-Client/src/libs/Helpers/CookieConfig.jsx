import Cookies from 'js-cookie';
import {accessToken} from "../Config/TokenConfig";

export function setCookieHandler({access_token}) {
    Cookies.set('access_token',access_token,accessToken);
}
export function removeCookieHandler(){
    Cookies.remove('access_token');
}