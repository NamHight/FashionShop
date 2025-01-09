import React, { useState } from "react";
import { getAllPromotion } from "../../../services/api/PromotionService";
import { NavLinkBlog } from "../NavLinks/index";
import { Spinner } from "@material-tailwind/react";
import { useQuery } from "@tanstack/react-query";
import PaginationList from "../../../components/Pagination/PaginationList";

const Blog = () => {
  const PARAMPAGE = { page: 1, limit: 9 };
  const [page, setPage] = useState(null);
  const [paramPage, setparamPage] = useState(PARAMPAGE);
  const { data: BlogPromotionQuery, isLoading } = useQuery({
    queryKey: ["blogPromotion",, paramPage.page, paramPage.limit],
    queryFn: async () => {
      const result = await getAllPromotion({params : paramPage});
      const paginationData = JSON.parse(result.headers["x-pagination"]);
      setPage(paginationData);
      console.log("headers promotions: ", JSON.parse(result.headers["x-pagination"]));
      console.log("set page: ", page);
      return result.data;
    },
  });
  const HanndlePreOrNext = (check) => {
    console.log("check : ", check);
    if (check === true) {
      if (page.HasNextpage === true) {
        setparamPage((prevParamPage) => ({
          ...prevParamPage,
          page: prevParamPage.page + 1,
        }));
      }
    }
    if (check === false) {
      if (page.HasPreviouspage === true) {
        setparamPage((prevParamPage) => ({
          ...prevParamPage,
          page: prevParamPage.page - 1,
        }));
      }
    }
  };
  const handlePage = (number) => {
    console.log("i ", number);
    setparamPage((prevParamPage) => ({
      ...prevParamPage,
      page: number,
    }));
  };
  return (
    <div className="container mx-auto">
      <div className="border-b mb-5 flex text-sm ">
        <NavLinkBlog />
      </div>
      <h4>Danh sách khuyến mãi</h4>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {!isLoading ? (
          BlogPromotionQuery ? (
            BlogPromotionQuery.map((item, key) => {
              return (
                <div
                  className="col-span-1 bg-white border rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300 p-4"
                  key={item.promotionId}
                >
                  <img
                    src={`/assets/images/promotions/${item.image}`}
                    style={{ padding: "20px", width: "450px", height: "300px" }}
                    alt={item.image}
                    className="w-full h-48 object-cover rounded-t-lg mb-4"
                  />
                  <h3 className="text-lg font-semibold">
                    {item.promotionName}
                  </h3>
                  <p className="text-sm text-gray-500 mb-2">
                    {item.description}
                  </p>
                </div>
              );
            })
          ) : (
            <Spinner />
          )
        ) : (
          <Spinner />
        )}
      </div>
      <PaginationList TotalPages={page?.TotalPages} CurrentPage={page?.CurrentPage} HanndlePreOrNext={HanndlePreOrNext} handlePage={handlePage} />
    </div>
  );
};
export default Blog;
