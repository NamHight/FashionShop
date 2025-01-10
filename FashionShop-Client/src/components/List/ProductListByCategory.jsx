import React from "react";
import { useParams } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import { getProductsBySlug } from "../../services/api/ProductService";
import Loading from "../Loading";
import ButtonAddCart from "../ButtonAddCart/ButtonAddCart";
const ProductListByCategory = () => {
  const { categorySlug } = useParams();

  const { data: products, isLoading, isError } = useQuery({
    queryKey: ["productsBySlug", categorySlug], // Sử dụng object
    queryFn: () => getProductsBySlug(categorySlug), // Hàm fetch dữ liệu
  });
  
  

  if (isLoading) return <Loading />;
  if (isError) return <div>Error loading products.</div>;

  return (
    <div className="max-w-screen-xl mx-auto p-4">
      <h1 className="text-3xl font-bold mb-6">
        Products in Category: {categorySlug}
      </h1>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
        {products.length > 0 ? (
          products.map((product) => (
            <div
              key={product.productId}
              className="bg-white border rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300 p-4"
            >
              <img
                src={`http://localhost:7068/images/${product.banner}`}
                alt={product.productName}
                className="w-full h-48 object-cover rounded-t-lg mb-4"
              />
              <h3 className="text-lg font-semibold">{product.productName}</h3>
              <p className="text-lg font-bold text-red-500 mb-4">
                {product.price} VND
              </p>
              <button className="bg-blue-500 text-white py-2 px-4 rounded-lg hover:bg-blue-600 transition-colors">
                Add to Cart
              </button>
            </div>
          ))
        ) : (
          <p className="col-span-full text-center text-lg text-gray-600">
            No products found in this category.
          </p>
        )}
      </div>
    </div>
  );
};

export default ProductListByCategory;
