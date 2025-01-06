import React from "react";
import { NavLinkBlog } from "../NavLinks/index";
import { useQuery } from "@tanstack/react-query";
import { GetPageAndSearchArticle } from "../../../services/api/ArticleService";
import { Spinner } from "@material-tailwind/react";

const BlogArticle = () => {
    const {data : BlogArticleQuery, refetch, error, isLoading} = useQuery({
        queryKey: ['blogArticle'],
        queryFn: async () => {
            const result = await GetPageAndSearchArticle();
            console.log("Dữ liệu get article api :",result);
            return result.data;
        }
    })
    
  return (
    <div className="container mx-auto">
      <div className="flex flex-1 items-center justify-between">
        <NavLinkBlog />
      </div>
      <h4>Danh sách bài viết</h4>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {
            !isLoading ? (
                BlogArticleQuery.item1.map((item, key) => {
                    return (
                      <div
                        className="col-span-1 bg-white border rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300 p-4"
                        key={item.articleId}
                      >
                        <img
                          src={`/assets/images/promotions/${item.image}`}
                          alt={item.image}
                          className="w-full h-48 object-cover rounded-t-lg mb-4"
                        />
                        <h3 className="text-lg font-semibold">{item.articleName}</h3>
                        <p className="text-sm text-gray-500 mb-2">{item.description}</p>
                        <p className="text-sm text-gray-500 mb-2">{item.createdAt}</p>
                      </div>
                    );
                  })
            ) : (
                <Spinner />
            )
        }
      </div>
      <div className="flex items-center justify-end">
        <div className="flex items-center justify-between border-t border-gray-200 bg-white px-4 py-3 px-6">
          <div className="flex flex-1 items-center justify-between">
            <div className="relative inline-flex items-center rounded-l-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0">
              Previous
            </div>
            {/* { totalPage ? (
                      [...Array(totalPage)].map((_, key) => {
                        const currentNumber = pageNumber++;
                        return (
                        <div key={key} className="relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0">{currentNumber}</div>
                        );
                      })
                    ) : (
                      <div>1</div>
                    )
                    } */}

            <div className="relative inline-flex items-center rounded-r-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0">
              Next
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
export default BlogArticle;
