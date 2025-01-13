import React, { useEffect, useState } from "react";
import { getAllProducts } from "../../services/api/ProductService";
import ButtonAddCart from "../ButtonAddCart/ButtonAddCart";
import Loading from "../Loading";
import  "../List/ProductList.css";
const ProductList = () => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const data = await getAllProducts();
        setProducts(data);
      } catch (err) {
        setError("Unable to fetch products.");
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, []);

  if (loading) return <Loading />;
  if (error)
    return (
      <div className="bg-gray-100 px-2 text-center">
        <div className="h-screen flex flex-col justify-center items-center">
          <h1 className="text-8xl font-extrabold text-red-500">500</h1>
          <p className="text-4xl font-medium text-gray-800">
            Internal Server Error
          </p>
          <p className="text-xl text-gray-800 mt-4">
            We apologize for the inconvenience. Please try again later.
          </p>
        </div>
      </div>
    );

  return (
    <div className="max-w-screen-xl mx-auto py-8">
      <h1 className="text-3xl font-bold mb-6">Product List</h1>
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
                <a href={`/${product.category.slug}/${product.slug}`}>
                {product.productName}
                </a>
              </h3>
              <p className="text-sm text-gray-500 mb-2">
                {product.description}
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
            No products found.
          </p>
        )}
      </div>
    </div>
  );
};

export default ProductList;
