import React, { useEffect, useState } from "react";
import { getAllPromotion } from "../../services/api/PromotionService";
import { keyframes } from "motion";

const Blog = () => {
  const [promotions, setPromotions] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPage, setTotalPage] = useState(1);
  const pageNumber = 1;
  const getPromotions = async (page) => {
    const result = await getAllPromotion({
      params: {
        page: page,
        limit: 9,
      },
    });
    console.log(result.data.item1);
    setPromotions(result.data.item1);
    setCurrentPage(result.data.item2.currentPage);
    setTotalPage(result.data.item2.totalPages);

    console.log("promotion ", promotions); // Kiểm tra khi promotions thay đổi
  };
  useEffect(() => {
    getPromotions(currentPage);
  }, [currentPage]);

  return (
    <div className="container mx-auto">
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {promotions && promotions.length > 0 ? (
          promotions.map((item, key) => {
            return (
              <div className="col-span-1" key={key}>
                {item.promotionName}
              </div>
            );
          })
        ) : (
          <p className="col-span-1">Dữ liệu đang xử lý</p>
        )}
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
export default Blog;
