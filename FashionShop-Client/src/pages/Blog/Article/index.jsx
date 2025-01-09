import React, { useState } from "react";
import { NavLinkBlog } from "../NavLinks/index";
import { useQuery } from "@tanstack/react-query";
import { GetPageAndSearchArticle } from "../../../services/api/ArticleService";
import { Spinner } from "@material-tailwind/react";
import PaginationList from "../../../components/Pagination/PaginationList";

const BlogArticle = () => {
  const PARAMPAGE = { page: 1, limit: 9, nameSearch: null, categoryid: null };
  const [page, setPage] = useState(null);
  const [paramPage, setparamPage] = useState(PARAMPAGE);
  const { data: BlogArticleQuery, isLoading } = useQuery({
    queryKey: ["blogArticle", paramPage.page, paramPage.limit],
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
  return (
    <div className="container mx-auto">
      <div className="border-b mb-5 flex text-sm ">
        <NavLinkBlog />
      </div>
      <h4>Danh sách bài viết</h4>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-10">
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
                    style={{ padding: '20px', width: '450px', height: '300px' }}
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
      <PaginationList TotalPages={page?.TotalPages} CurrentPage={page?.CurrentPage} HanndlePreOrNext={HanndlePreOrNext} handlePage={handlePage} />
    </div>
  );
};
export default BlogArticle;
