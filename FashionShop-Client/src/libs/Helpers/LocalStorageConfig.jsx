export const setLocalStorage = (key, value, expiry) => {
   const expiryDate = new Date();
   const item = {
       data : value,
       expiry : expiryDate.getTime() + expiry * 60 * 1000
   }
   localStorage.setItem(key,JSON.stringify(item));
}
export const getLocalStorage = (key) => {
    const itemStr = localStorage.getItem(key);
    if(!itemStr) return null;
    const item = JSON.parse(itemStr);
    const now = new Date();
    if(now.getTime() > item.expiry) {
        localStorage.removeItem(key);
        return null;
    }
    return item.data;
}