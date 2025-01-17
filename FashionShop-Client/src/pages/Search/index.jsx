import Content from "./Content";
import { Filter } from "./Filter";
import { useQuery } from "@tanstack/react-query";
import { useSearchParams } from "react-router";
import { useState } from "react";
import { searchProductName } from "../../services/api/ProductService";
import NotFound from "../../components/NotFound/NotFound";
import Loading from "../../components/Loading";
import { Button, IconButton } from "@material-tailwind/react";
import { NavArrowLeft, NavArrowRight } from "iconoir-react";

function Search() {
  let [searchParams] = useSearchParams();
  const [paginate, setPaginate] = useState({});
  const [page, setPage] = useState(1);
  const { data, isLoading, isError, isPlaceholderData } = useQuery({
    queryKey: ["searchFound"],
    queryFn: async () => {
      const productName = searchParams.get("searchProduct");
      const categoryName = searchParams.get("categoryName");
      const page = searchParams.get("pageNumber");
      const limit = searchParams.get("pageSize");
      const minPrice = searchParams.get("minPrice");
      const maxPrice = searchParams.get("maxPrice");
      const dataDto = {
        productName: productName,
        categoryName: categoryName,
        page: page,
        limit: limit,
        minPrice: minPrice,
        maxPrice: maxPrice,
      };
      let result = await searchProductName(dataDto);
      let header = result.headers["x-pagination"];
      if (header) {
        console.log(header);
        setPaginate(JSON.parse(header));
      }
      return result;
    },
    refetchOnWindowFocus: false,
    refetchIntervalInBackground: false,
  });
  console.log("daat", data);
  console.log("paginat", paginate.TotalPages);
  return (
    <div className="bg-slate-100 w-full mt-4 mx-20 rounded-xl">
      {isLoading ? (
        <Loading />
      ) : isError ? (
        <NotFound
          classText="text-center font-bold text-4xl mt-6"
          text="Error not Found!"
          icon={
            <img
              src="assets/Logo.png"
              className="size-52 border border-emerald-400 ring-2 ring-gray-300 rounded-full"
            />
          }
        />
      ) : data.data.length > 0 ? (
        <div>
          <div>
            <Filter />
          </div>
          <div className="mt-2">
            <Content data={data.data} paginate={paginate} />

            <div className="flex justify-center items-center gap-1 my-5">
              <Button variant="ghost">
                <NavArrowLeft className="mr-1.5 h-4 w-4 stroke-2" />
                Previous
              </Button>
              <IconButton variant="ghost">1</IconButton>
              <IconButton>2</IconButton>
              <IconButton variant="ghost">3</IconButton>
              <IconButton variant="ghost">4</IconButton>
              <IconButton variant="ghost">5</IconButton>
              <Button variant="ghost">
                Next
                <NavArrowRight className="ml-1.5 h-4 w-4 stroke-2" />
              </Button>
            </div>
          </div>
        </div>
      ) : (
        <NotFound
          classText="text-center font-bold text-4xl mt-6"
          text="Product not Found!"
          icon={
            <img
              src="assets/Logo.png"
              className="size-52 border border-emerald-400 ring-2 ring-gray-300 rounded-full"
            />
          }
        />
      )}
    </div>
  );
}

export default Search;
