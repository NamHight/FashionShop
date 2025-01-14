import React, { useState } from "react";
import { NavLinkBlog } from "../NavLinks/index";
import { useQuery } from "@tanstack/react-query";
import { GetPageAndSearchArticle } from "../../../services/api/ArticleService";
import { Spinner } from "@material-tailwind/react";
import PaginationList from "../../../components/Pagination/PaginationList";
import { useForm } from "react-hook-form";

const BlogArticle = () => {
  const PARAMPAGE = { page: 1, limit: 9, nameSearch: null, categoryId: null };
  const [page, setPage] = useState(null);
  const [paramPage, setparamPage] = useState(PARAMPAGE);
  const [listCategory, setListCategory] = useState([]);
  const { data: BlogArticleQuery, isLoading } = useQuery({
    queryKey: [
      "blogArticle",
      paramPage.page,
      paramPage.limit,
      paramPage.nameSearch,
      paramPage.categoryId
    ],
    queryFn: async () => {
      const result = await GetPageAndSearchArticle({ params: paramPage });
      if (result?.StatusCode === 404) {
        setPage([]);
        return result?.Message;
      }
      const paginationData = JSON.parse(result.headers["x-pagination"]);
      setPage(paginationData);
      console.log("headers: ", JSON.parse(result.headers["x-pagination"]));
      console.log("set page: ", page);
      console.log("status code: ", result);
      if (listCategory.length === 0) {
        setListCategory(result.data);
      }
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
  //function search
  const { register: searchRegister, handleSubmit: handleSearch } = useForm({
    defaultValues: {
      nameSearch: "",
    },
  });
  function formActionSearch(data) {
    const category = data.categoryId || null
    setparamPage((prevParamPage) => ({
      ...prevParamPage,
      page: 1,
      nameSearch: data.nameSearch,
      categoryId: category,
    }));
    console.log("ttt", data.nameSearch);
    console.log("ttt categoryId", category);
  }
  return (
    <div className="container mr-px">
      <div className="border-b mb-5 flex text-sm ">
        <NavLinkBlog />
      </div>

      {!isLoading ? (
        <>
          <form
            className="max-w-md ml-auto"
            onSubmit={handleSearch(formActionSearch)}
          >
            <div className="flex items-center">
              <select
                name="categoryId"
                id="categoryId"
                {...searchRegister("categoryId")}
                className="px-4 py-3 w-full min-w-[150px] text-sm text-gray-500 border border-gray-300 rounded-md dark:bg-gray-700 dark:text-gray-200"
              >
                <option
                  value=""
                  className="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
                >
                  Search for keywork
                </option>
                {listCategory.map((item, key) => {
                  return (
                    <option
                      key={item.category.categoryId}
                      value={item.category.categoryId}
                      className="hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
                    >
                      {item.category.categoryName}
                    </option>
                  );
                })}
              </select>
              <input
                type="search"
                id="nameSearch"
                name="nameSearch"
                value="null"
                {...searchRegister("nameSearch")}
                className="block w-full px-4 py-3 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                placeholder="Search Name ..."
                required
              />
              <button
                type="submit"
                className="text-white end-2.5 bottom-2.5 bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-3 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
              >
                Search
              </button>
            </div>
          </form>

          {typeof BlogArticleQuery === "string" ? (
            <h3 className="text-lg font-semibold">
              The entered value was not found, please enter another keyword!
            </h3>
          ) : (
            <>
              <h2 className="text-lg font-semibold">Articles List</h2>
              <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-10">
                {BlogArticleQuery && BlogArticleQuery.map((item, key) => {
                  return (
                    <div
                      className="col-span-1 bg-white border rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300 p-4"
                      key={item.articleId}
                    >
                      <img
                        src={`/assets/images/articles/${item.image}`}
                        style={{
                          padding: "20px",
                          width: "450px",
                          height: "300px",
                        }}
                        alt={item.image}
                        className="w-full h-48 object-cover rounded-t-lg mb-4"
                      />
                      <h3 className="text-lg font-semibold">
                        {item.articlesName}
                      </h3>
                      <p className="text-sm text-gray-500 mb-2">
                        {item.createAt}
                      </p>
                      <p className="text-sm text-gray-500 mb-2">
                        {item.description}
                      </p>
                    </div>
                  );
                })}
              </div>
              <PaginationList
                TotalPages={page?.TotalPages}
                CurrentPage={page?.CurrentPage}
                HanndlePreOrNext={HanndlePreOrNext}
                handlePage={handlePage}
              />
            </>
          )}
        </>
      ) : (
        <Spinner />
      )}
    </div>
  );
};
export default BlogArticle;
