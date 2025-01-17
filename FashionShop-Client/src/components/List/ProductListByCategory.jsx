import React, { useEffect, useState } from "react";
import { useParams,Link } from "react-router";
import { getProductsBySlug } from "../../services/api/ProductService";
import ButtonAddCart from "../ButtonAddCart/ButtonAddCart";
import Loading from "../Loading";
import "../List/ProductList.css";

const ProductListByCategory = () => {
  const { categorySlug } = useParams();
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const data = await getProductsBySlug(categorySlug);
        setProducts(data);
      } catch (err) {
        setError("Unable to fetch products.");
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
     // Thiết lập interval để lấy dữ liệu mỗi 10 giây
     const intervalId = setInterval(() => {
      console.log("Fetching products...");
      fetchProducts();
    }, 10000); // 10 giây

    // Dọn dẹp interval khi component bị unmount
    return () => clearInterval(intervalId);
  }, [categorySlug]);

  if (loading) return <Loading />;
  if (error)
    return (
      <div className=" px-2 text-center">
        <div className="h-screen flex flex-col justify-center items-center">
          <h1 className="text-6xl font-extrabold text-gray-500">Don't have any product!</h1>
          <p className="text-4xl my-8 font-medium text-gray-800">
          We apologize for the inconvenience.
          </p>
          <p>
          <Link to={'/'} className="px-8 py-4 bg-gradient-to-r from-blue-500 to-purple-500 text-white font-bold rounded-full transition-transform transform-gpu hover:-translate-y-1 hover:shadow-lg">
            Back to Home
            </Link>
            </p>
        </div>
      </div>
    );

  return (
    <div className="max-w-screen-xl mx-auto p-4">
      <h1 className="text-3xl font-bold mb-6">{categorySlug}</h1>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
        {products.length > 0 ? (
          products.map((product) => (
            <div
              key={product.productId}
              className="bg-white border rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300 p-4"
            >
              <div className="aspect-w-1 aspect-h-1 aspect-rectangle">
                <img
                  src={`${process.env.PUBLIC_URL}/assets/images/products/${product.banner}`}
                  alt={product.productName}
                  className="w-full h-full object-cover rounded-t-lg"
                />
              </div>
              <h3 className="text-lg font-semibold mt-4">
                {product.category && product.category.slug && product.slug ? (
                    <a href={`/${product.category.slug}/${product.slug}`}>
                    {product.productName}
                    </a>
                ) : (
                    <span>{product.productName}</span> // Hiển thị tên sản phẩm nếu không có category hoặc slug
                )}
                </h3>
              <p className="text-sm text-gray-500 mb-2">{product.description}</p>
              <p className="text-md text-gray-600 mb-4">
                Quantity: {product.quantity}
              </p>
              <p className="text-lg font-bold text-red-500 mb-4">
                Price: ${product.price}
              </p>
              <div className="flex justify-between">
                <ButtonAddCart
                  css="bg-emerald-500 text-white py-2 px-4 rounded-lg hover:bg-blue-600 transition-colors"
                  productId={product.productId}
                  productName={product.productName}
                  banner={product.banner}
                  price={product.price}
                  quantity={1}
                />
                <button className="bg-gray-200 text-gray-700 py-2 px-4 rounded-lg hover:bg-gray-300 transition-colors">
                  ♥ Favorite
                </button>
              </div>
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
