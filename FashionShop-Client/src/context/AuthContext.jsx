import { createContext, useContext, useState } from "react";
import { getCustomerById } from "../services/api/CustomerService";
import { jwtDecode } from "jwt-decode";
import { tokenProtection } from "../services/api/TokenService";
import { useQuery } from "@tanstack/react-query";

export const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const {
    data: user,
    error,
    isLoading,
  } = useQuery({
    queryKey: ["user"], // Query key
    queryFn: async () => {
      const result = await tokenProtection();
      console.log(result);
      if (result?.token) {
        const customer = jwtDecode(result.token);
        if (customer) {
          const data = await getCustomerById(customer.sub);
          console.log("data", data);
          
          if (data.status === 401) {
            throw new Error("Unauthorized");
          }
          return data;
        }
      }
      return null; // Return null if no token
    },
    staleTime: 1000 * 60 * 5,
    refetchOnWindowFocus: false,
    enabled: isAuthenticated,
  });

  const value = {
    user,
    isLoading,
    error,
    setIsAuthenticated,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
