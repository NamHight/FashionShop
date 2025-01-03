import { createContext, useContext } from "react";
import { useQuery } from "@tanstack/react-query";
import { getFavoriteById } from "./../services/api/FavoriteService";
import { useAuth } from "./AuthContext";

export const FavoriteContext = createContext(null);

export const FavoriteProvider = ({ children }) => {
  const { user } = useAuth();
  const {
    data: favorite,
    error,
    isLoading,
  } = useQuery({
    queryKey: ["favorite"], // Query key
    queryFn: async () => {
      const data = await getFavoriteById(user.customerId);
      console.log("233", data);
      return data;
    },
  });

  const value = {
    favorite,
    isLoading,
    error,
  };

  return (
    <FavoriteContext.Provider value={value}>
      {children}
    </FavoriteContext.Provider>
  );
};

export const useFavoriteContext = () => {
  const context = useContext(FavoriteContext);
  if (!context) {
    throw new Error(
      "useFavoriteContext must be used within an FavoriteProvider"
    );
  }
  return context;
};
