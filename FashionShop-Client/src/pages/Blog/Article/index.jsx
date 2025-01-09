import React, { useState } from "react";
import { NavLinkBlog } from "../NavLinks/index";
import { useQuery } from "@tanstack/react-query";
import { GetPageAndSearchArticle } from "../../../services/api/ArticleService";
import { Spinner } from "@material-tailwind/react";

const BlogArticle = () => {
  const PARAMPAGE = { page: 1, limt: 9, nameSearch: null, categoryid: null };
  const [page, setPage] = useState(null);
  const [paramPage, setparamPage] = useState(PARAMPAGE);
  const { data: BlogArticleQuery, isLoading } = useQuery({
    queryKey: ["blogArticle", paramPage.page, paramPage.limt],
    queryFn: async () => {
      const result = await GetPageAndSearchArticle({ params: paramPage });
      const paginationData = JSON.parse(result.headers["x-pagination"]);
      setPage(paginationData);
      console.log("headers: ", JSON.parse(result.headers["x-pagination"]));
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
  const ListPagination = (TotalPages, CurrentPage) => {
    const pages = [];
    for (
      let i = Math.max(1, CurrentPage - 2);
      i <= Math.min(TotalPages, CurrentPage + 2);
      i++
    ) {
      pages.push(i);
    }
    return (
      <div className="flex items-center justify-end">
        <div className="flex items-center justify-between border-t border-gray-200 bg-white px-4 py-3 px-6">
          <div className="flex flex-1 items-center justify-between">
            <button
              onClick={() => {
                HanndlePreOrNext(false);
              }}
              className="inline-flex items-center rounded-l-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0"
            >
              Previous
            </button>
            {pages &&
              pages.map((item) => {
                return (
                  <button
                    className={`${
                      item === CurrentPage ? "bg-blue-500" : ""
                    } inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-blue-500 focus:z-20 focus:outline-offset-0`}
                    onClick={() => {
                      handlePage(item);
                    }}
                    key={item}
                  >
                    {item}
                  </button>
                );
              })}

            <button
              onClick={() => {
                HanndlePreOrNext(true);
              }}
              className="inline-flex items-center rounded-r-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0"
            >
              Next
            </button>
          </div>
        </div>
      </div>
    );
  };

  return (
    <div className="container mx-auto">
      <div className="flex flex-1 items-center justify-between">
        <NavLinkBlog />
      </div>
      <h4>Danh sách bài viết</h4>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {!isLoading ? (
          BlogArticleQuery ? (
            BlogArticleQuery.map((item, key) => {
              return (
                <div
                  className="col-span-1 bg-white border rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300 p-4"
                  key={item.articleId}
                >
                  <img
                    src={`/assets/images/articles/${item.image}`}
                    alt={item.image}
                    className="w-full h-48 object-cover rounded-t-lg mb-4"
                  />
                  <h3 className="text-lg font-semibold">{item.articlesName}</h3>
                  <p className="text-sm text-gray-500 mb-2">{item.createdAt}</p>
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
      {ListPagination(page?.TotalPages, page?.CurrentPage)}
    </div>
  );
};
export default BlogArticle;
