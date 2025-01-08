import React, { useEffect, useState } from "react";
import { getAllProducts } from "../../services/api/ProductService";
import Loading from "../Loading";
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

  if (loading) return <Loading/>;
  if (error) return <div>{error}</div>;

  return (
    <div className="max-w-screen-xl mx-auto p-4">
    <h1 className="text-3xl font-bold text-center mb-6">Product List</h1>
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
      {products.length > 0 ? (
        products.map((product) => (
          <div
            key={product.productId}
            className="bg-white border rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300 p-4"
          >
            <img
               src={`${process.env.PUBLIC_URL}/assets/images/products/${product.banner}`}
              alt={product.productName}
              className="w-full rounded-t-lg mb-4"
            />
            <h3 className="text-lg font-semibold">{product.productName}</h3>
            <p className="text-sm text-gray-500 mb-2">{product.description}</p>
            <p className="text-lg font-bold text-red-500 mb-4">Price: ${product.price}</p>
            <div className="flex justify-between">
              <button className="bg-blue-500 text-white py-2 px-4 rounded-lg hover:bg-blue-600 transition-colors">
                Add to Cart
              </button>
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
