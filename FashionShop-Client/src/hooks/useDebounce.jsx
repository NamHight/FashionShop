import{useEffect, useState} from 'react';


export const useDebounce = (text , delay= 1000) => {
    const [value, setValue] = useState(text);
    useEffect(() => {
        const handler = setTimeout(() => {
            setValue(text);
        }, delay);
        return () => {

            clearTimeout(handler);
        }
    }, [delay, text]);
    return value
};