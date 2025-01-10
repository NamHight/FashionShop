import { useQuery } from "@tanstack/react-query";
import { getOrdersByIdAndStatus } from "../../../services/api/OrdersService";

export default function DataOrder (id, status){
    const { data:order, isLoading, isError } = useQuery({
      queryKey: ["orderStatus"],
      queryFn: async () => {
        var data = await getOrdersByIdAndStatus(id,status);
        return data;
      },
    });
    const value = {
      order,
      isLoading,
      isError
    };
    return value;
  };