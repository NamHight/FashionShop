import {createContext, useContext, useEffect, useState} from "react";
import {getCustomerByEmail, getCustomerById} from "../services/api/CustomerService";
import { jwtDecode } from "jwt-decode";
import { tokenProtection } from "../services/api/TokenService";
import { useQuery } from '@tanstack/react-query';
import {loginGoogle} from "../services/api/AuthServices";

export const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
    const { data:user, error, isLoading,status, } = useQuery({
        queryKey:['user'], // Query key
        queryFn:async()=>
    {
        const result = await tokenProtection();
        if (result?.token) {
            const customer = jwtDecode(result.token);
            if (customer) {
                const response = await getCustomerByEmail(customer?.email);
                console.log(response);
                if (response.status === 401)return null;
                if(response.status === 400) return null
                if(response.status === 500) return null
                if(response.data?.statusCode === 404) return null
                return response?.data;
            }
        }
        return null; // Return null if no token
    },
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false,
});

    const value = {
        user,
        isLoading,
        error,
    };

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error("useAuth must be used within an AuthProvider");
    }
    return context;
};