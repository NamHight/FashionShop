import { useQuery } from "@tanstack/react-query";
import { useState } from "react";
import { GetReviewByProductId } from "../../../services/api/ReviewService";
import { Rating, Spinner } from "@material-tailwind/react";
import PaginationList from "../../Pagination/PaginationList";

const ReviewList = (props) => {
  const productId = props.productId;
  const PARAMPAGE = {
    page: 1,
    limit: 10,
    productId: productId,
    typeOrderBy: null,
    rating: 0,
  };
  const [page, setPage] = useState([]);
  const [paramPage, setParamPage] = useState(PARAMPAGE);
  const { data: ReviewListQuery, isLoading } = useQuery({
    queryKey: [
      "ReviewList",
      paramPage.page,
      paramPage.limit,
      paramPage.productId,
      paramPage.typeOrderBy,
      paramPage.rating,
    ],
    queryFn: async () => {
      const result = await GetReviewByProductId({ params: paramPage });
      if (result?.StatusCode === 404) {
        setPage([]);
        return result?.Message;
      }
      const paginationData = JSON.parse(result.headers["x-pagination"]);
      setPage(paginationData);
      console.log(result.data);
      return result.data;
    },
  });

  const HanndlePreOrNext = (check) => {
    console.log("check : ", check);
    if (check === true) {
      if (page.HasNextpage === true) {
        setParamPage((prevParamPage) => ({
          ...prevParamPage,
          page: prevParamPage.page + 1,
        }));
      }
    }
    if (check === false) {
      if (page.HasPreviouspage === true) {
        setParamPage((prevParamPage) => ({
          ...prevParamPage,
          page: prevParamPage.page - 1,
        }));
      }
    }
  };
  const handlePage = (number) => {
    console.log("i ", number);
    setParamPage((prevParamPage) => ({
      ...prevParamPage,
      page: number,
    }));
  };
  const handleOrderChange = (value) => {
    console.log("select : ", value);
    setParamPage((prevParamPage) => ({
      ...prevParamPage,
      typeOrderBy: value,
    }));
  };
  const handleStarChange = (value) => {
    setParamPage((prevParamPage) => ({
      ...prevParamPage,
      rating: value,
    }));
  };
  return (
    <>
      <div className="m-3">
        <select
          onChange={(e) => handleStarChange(e.target.value)}
          className="px-4 py-3 min-w-[150px] text-sm text-gray-500 border border-gray-300 rounded-md dark:bg-gray-700 dark:text-gray-200"
        >
          <option
            value="0"
            className="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
          >
            search for star
          </option>
          <option
            value="1"
            className="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
          >
            1 star
          </option>
          <option
            value="2"
            className="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
          >
            2 star
          </option>
          <option
            value="3"
            className="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
          >
            3 star
          </option>
          <option
            value="4"
            className="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
          >
            4 star
          </option>
          <option
            value="5"
            className="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
          >
            5 star
          </option>
        </select>
      </div>
      {!isLoading ? (
        typeof ReviewListQuery === "string" ? (
          <p>{ReviewListQuery}</p>
        ) : (
          <>
            <div className="float-right m-3">
              <select
                onChange={(e) => handleOrderChange(e.target.value)}
                className="px-4 py-3 w-full min-w-[150px] text-sm text-gray-500 border border-gray-300 rounded-md dark:bg-gray-700 dark:text-gray-200"
              >
                <option
                  value="desc"
                  className="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
                >
                  order new
                </option>
                <option
                  value="asc"
                  className="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
                >
                  order old
                </option>
              </select>
            </div>

            {ReviewListQuery.map((item) => {
              return (
                <div
                  key={item.reviewId}
                  className="w-full lg:p-8 p-5 bg-white rounded-3xl border border-gray-200 flex-col justify-start items-start flex"
                >
                  <div className="w-full flex-col justify-start items-start gap-3.5 flex">
                    <div className="w-full justify-between items-center inline-flex">
                      <div className="justify-start items-center gap-2.5 flex">
                        <div className="w-10 h-10 bg-stone-300 rounded-full justify-start items-start gap-2.5 flex">
                          <img
                            className="rounded-full object-cover"
                            src={
                              item?.customer.avatar
                                ? item?.customer.avatar.substring(0, 4) ===
                                  "http"
                                  ? item.customer.avatar
                                  : `/assets/images/customers/${
                                      item?.customer.avatar || "default.png"
                                    }`
                                : "/assets/images/customers/default.png"
                            }
                            alt={item?.customer.avatar}
                          />
                        </div>
                        <div className="flex-col justify-start items-start gap-1 inline-flex">
                          <h5 className="text-gray-900 text-sm font-semibold leading-snug">
                            {item.customer.customerName}
                          </h5>
                          <h6 className="text-gray-500 text-xs font-normal leading-5">
                            {item.reviewDate}
                          </h6>
                          <h6 className="text-gray-500 text-xs font-normal leading-5">
                            <Rating
                              value={Math.round(item.rating)}
                              color="warning"
                              readonly
                            />
                          </h6>
                        </div>
                      </div>
                    </div>
                    <p className="text-gray-800 text-sm font-normal leading-snug">
                      {item.reviewText}
                    </p>
                    <p className="text-gray-800 text-sm font-normal leading-snug">
                      {
                        item?.image !== 'null' ? (
                          <img src={`http://localhost:7068/assets/images/review/${item?.image}`} alt={item?.image} style={{ 
                            padding: "20px",
                            width: "300px",
                            height: "350px",
                          }}/>
                        ) : (null)
                      }
                    </p>
                  </div>
                </div>
              );
            })}
            <PaginationList
              TotalPages={page?.TotalPages}
              CurrentPage={page?.CurrentPage}
              HanndlePreOrNext={HanndlePreOrNext}
              handlePage={handlePage}
            />
          </>
        )
      ) : (
        <Spinner />
      )}
    </>
  );
};
export default ReviewList;
