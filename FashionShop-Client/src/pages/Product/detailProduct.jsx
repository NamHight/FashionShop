import React from "react";
import ProductDetail from "../../components/Detail/ProductDetail";
const DetailProduct = () => {

  const handleScroll = (direction) => {
    const container = document.querySelector(".overflow-x-auto");
    if (direction === "left") {
      container.scrollLeft -= 200; // Di chuyển 200px sang trái
    } else {
      container.scrollLeft += 200; // Di chuyển 200px sang phải
    }
  };

  return (
    <div>
      <ProductDetail />



      <div className="py-8">
        <h2 className="text-2xl font-semibold mb-6 px-8">
          Sản phẩm có liên quan
        </h2>

        {/* Container với overflow-x-auto */}
        <div className="flex items-center">
          {/* Nút di chuyển sang trái */}
          <button
            className="absolute left-0 z-10 bg-gray-500 text-white p-2 rounded-full hover:bg-gray-600"
            onClick={() => handleScroll("left")}
          >
            &#8592;
          </button>

          {/* Sản phẩm với khả năng di chuyển qua lại */}
          <div className="flex overflow-x-auto space-x-4 px-4 w-full">
            {/* Sản phẩm 1 */}
            <div className="flex flex-col items-center bg-white shadow-lg rounded-lg p-4 w-1/5 min-w-[200px]">
              <img
                className="w-full h-auto rounded-lg mb-4"
                src="https://media.fmplus.com.vn/uploads/products/2212KKUFMF0201/ba00aa57-0be2-4251-9310-cf879c8219f3.webp"
                alt="Áo Khoác Mangto nữ Dáng Dài"
              />
              <a
                className="text-center text-lg font-semibold text-gray-800 truncate mb-2"
                title="Áo Khoác Mangto nữ Dáng Dài"
                href="/ao-khoac-mangto/ao-khoac-mang-to-dang-dai"
              >
                Áo Khoác Mangto nữ Dáng Dài
              </a>
              <div className="text-center text-xl font-semibold text-red-600">
                <span className="price">445.000</span>{" "}
                <span className="currency">VND</span>
              </div>
            </div>

            {/* Sản phẩm 2 */}
            <div className="flex flex-col items-center bg-white shadow-lg rounded-lg p-4 w-1/5 min-w-[200px]">
              <img
                className="w-full h-auto rounded-lg mb-4"
                src="https://media.fmplus.com.vn/uploads/products/2212KKUFMF0201/ba00aa57-0be2-4251-9310-cf879c8219f3.webp"
                alt="Áo Khoác Mangto nữ Dáng Dài"
              />
              <a
                className="text-center text-lg font-semibold text-gray-800 truncate mb-2"
                title="Áo Khoác Mangto nữ Dáng Dài"
                href="/ao-khoac-mangto/ao-khoac-mang-to-dang-dai"
              >
                Áo Khoác Mangto nữ Dáng Dài
              </a>
              <div className="text-center text-xl font-semibold text-red-600">
                <span className="price">445.000</span>{" "}
                <span className="currency">VND</span>
              </div>
            </div>

            {/* Sản phẩm 3 */}
            <div className="flex flex-col items-center bg-white shadow-lg rounded-lg p-4 w-1/5 min-w-[200px]">
              <img
                className="w-full h-auto rounded-lg mb-4"
                src="https://media.fmplus.com.vn/uploads/products/2212KKUFMF0201/ba00aa57-0be2-4251-9310-cf879c8219f3.webp"
                alt="Áo Khoác Mangto nữ Dáng Dài"
              />
              <a
                className="text-center text-lg font-semibold text-gray-800 truncate mb-2"
                title="Áo Khoác Mangto nữ Dáng Dài"
                href="/ao-khoac-mangto/ao-khoac-mang-to-dang-dai"
              >
                Áo Khoác Mangto nữ Dáng Dài
              </a>
              <div className="text-center text-xl font-semibold text-red-600">
                <span className="price">445.000</span>{" "}
                <span className="currency">VND</span>
              </div>
            </div>

            {/* Sản phẩm 4 */}
            <div className="flex flex-col items-center bg-white shadow-lg rounded-lg p-4 w-1/5 min-w-[200px]">
              <img
                className="w-full h-auto rounded-lg mb-4"
                src="https://media.fmplus.com.vn/uploads/products/2212KKUFMF0201/ba00aa57-0be2-4251-9310-cf879c8219f3.webp"
                alt="Áo Khoác Mangto nữ Dáng Dài"
              />
              <a
                className="text-center text-lg font-semibold text-gray-800 truncate mb-2"
                title="Áo Khoác Mangto nữ Dáng Dài"
                href="/ao-khoac-mangto/ao-khoac-mang-to-dang-dai"
              >
                Áo Khoác Mangto nữ Dáng Dài
              </a>
              <div className="text-center text-xl font-semibold text-red-600">
                <span className="price">445.000</span>{" "}
                <span className="currency">VND</span>
              </div>
            </div>

            {/* Sản phẩm 5 */}
            <div className="flex flex-col items-center bg-white shadow-lg rounded-lg p-4 w-1/5 min-w-[200px]">
              <img
                className="w-full h-auto rounded-lg mb-4"
                src="https://media.fmplus.com.vn/uploads/products/2212KKUFMF0201/ba00aa57-0be2-4251-9310-cf879c8219f3.webp"
                alt="Áo Khoác Mangto nữ Dáng Dài"
              />
              <a
                className="text-center text-lg font-semibold text-gray-800 truncate mb-2"
                title="Áo Khoác Mangto nữ Dáng Dài"
                href="/ao-khoac-mangto/ao-khoac-mang-to-dang-dai"
              >
                Áo Khoác Mangto nữ Dáng Dài
              </a>
              <div className="text-center text-xl font-semibold text-red-600">
                <span className="price">445.000</span>{" "}
                <span className="currency">VND</span>
              </div>
            </div>
          </div>

          {/* Nút di chuyển sang phải */}
          <button
            className="absolute right-0 z-10 bg-gray-500 text-white p-2 rounded-full hover:bg-gray-600"
            onClick={() => handleScroll("right")}
          >
            &#8594;
          </button>
        </div>
      </div>
    </div>
  );
};
export default DetailProduct;
