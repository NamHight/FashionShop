import { useAuth } from "./../../../context/AuthContext";
import { useQuery } from "@tanstack/react-query";
import { getFavoriteById } from "./../../../services/api/FavoriteService";
import Favorite from "./Favorite";
import NotFound from "../../../components/NotFound/NotFound";
import { Input } from "@material-tailwind/react";
import { IoSearch } from "react-icons/io5";
import { MdOutlineError } from "react-icons/md";
import { CustomSpinner } from "../../../components/CustomSpinner";
import { useState } from "react";
function ListFavorites() {
  const { user } = useAuth();
  const [search, setSearch] = useState([]);

  const { data, isLoading, isError } = useQuery({
    queryKey: ["favorite"],
    queryFn: async () => {
      var data = await getFavoriteById(user.customerId);
      console.log(data);
      return data;
    },
  });
  console.log("getFavoriteById", data, isLoading, isError);
  console.log("search", search);
  const handleSearch = () => {
    var result = data.filter((item) =>
      item.productName.toLowerCase().includes(search)
    );
    console.log("result", result);

    return result;
  };

  const favorites = () => {
    console.log(data);
    return isLoading ? (
      <CustomSpinner />
    ) : isError ? (
      <NotFound
        text={"Error get favorites"}
        icon={<MdOutlineError size={80} />}
      />
    ) : data.length > 0 ? (
      <>
        {handleSearch().length > 0 ? (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-5">
            {handleSearch().map((item) => (
              <Favorite key={item.favoriteId} item={item} />
            ))}
          </div>
        ) : (
          <NotFound
            text={"No favorites products"}
            icon={<MdOutlineError size={80} />}
          />
        )}
      </>
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
        <div className="w-full text-center text-2xl font-bold bg-emerald-400 text-white rounded py-2">
          <h1>Favorite</h1>
        </div>
        <div className="my-2 flex">
          <Input
            type="search"
            placeholder="Search....."
            className={
              "bg-white group-hover:outline-blue-300 hover:border-blue-300 focus:outline-blue-300 group-focus:border-blue-300"
            }
            onChange={(e) => setSearch(e.target.value)}
          >
            <Input.Icon>
              <IoSearch className="h-full w-full text-emerald-400 group-hover:text-blue-300 group-focus:text-blue-300 " />
            </Input.Icon>
          </Input>
          
        </div>
        <div className="bg-slate-100 rounded">{favorites()}</div>
      </div>
    </>
  );
}

export default ListFavorites;
