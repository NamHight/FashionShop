import { useAuth } from "./../../../context/AuthContext";
import { useQuery } from "@tanstack/react-query";
import { getFavoriteById } from "./../../../services/api/FavoriteService";
import Favorite from "./Favorite";
import NotFound from "../../../components/NotFound/NotFound";
import { Input, Spinner } from "@material-tailwind/react";
import { IoSearch } from "react-icons/io5";
import { MdOutlineError } from "react-icons/md";
function ListFavorites() {
  const { user } = useAuth();
  const { data, isLoading, isError } = useQuery({
    queryKey: ["favorite"],
    queryFn: async () => {
      var data = await getFavoriteById(user.customerId);
      console.log(data);
      return data;
    },
  });
  const favorites = () => {
    console.log(data);
    return isLoading ? (
      <Spinner />
    ) : isError ? (
      <NotFound
        text={"Error get favorites"}
        icon={<MdOutlineError size={80} />}
      />
    ) : data.length > 0 ? (
      <div className="grid grid-cols-3 justify-items-center">
        {data?.map((item) => (
          <Favorite key={item.favoriteId} item={item} />
        ))}
      </div>
    ) : (
      <NotFound
        text={"No favorites products"}
        icon={<MdOutlineError size={80} />}
      />
    );
  };
  return (
    <>
      <div>
        <div className="w-full text-center text-2xl font-bold bg-slate-100 rounded py-2">
          <h1>Favorite</h1>
        </div>
        <div className="mt-2">
          <Input
            placeholder="Search....."
            className={
              "w-ful bg-white group-hover:outline-blue-300 hover:border-blue-300 focus:outline-blue-300 group-focus:border-blue-300"
            }
          >
            <Input.Icon>
              <IoSearch className="h-full w-full text-emerald-400 group-hover:text-blue-300 group-focus:text-blue-300 " />
            </Input.Icon>
          </Input>
        </div>
        <div>{favorites()}</div>
      </div>
    </>
  );
}

export default ListFavorites;
