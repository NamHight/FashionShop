import Cookies from 'js-cookie';
const expired = new Date(Date.now() + 1000 * 60 * 60 * 24 * 7);
export function setCookieHandler({access_token, refresh_token}) {
    Cookies.set('access_token',access_token,{expired: expired});
    Cookies.set('refresh_token',refresh_token, {expired: expired});
}
export function removeCookieHandler(){
    Cookies.remove('access_token');
    Cookies.remove('refresh_token');
}