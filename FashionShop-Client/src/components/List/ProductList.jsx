import React, { useEffect, useState } from "react";
import { getAllProducts } from "../../services/api/ProductService";
import ButtonAddCart from "../ButtonAddCart/ButtonAddCart";
import Loading from "../Loading";
import "../List/ProductList.css";
import { useAuth } from "../../context/AuthContext";
import { Button } from "@material-tailwind/react";
import { addFavorite, getFavoriteById, removeFavorite } from '../../services/api/FavoriteService';

const ProductList = () => {
  const { user } = useAuth();
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [favorites, setFavorites] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const productsPerPage = 16;

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const data = await getAllProducts();
        setProducts(data);

        if (user) {
          const favoriteData = await getFavoriteById(user.customerId);
          setFavorites(favoriteData || []);
        }
      } catch (err) {
        setError("Unable to fetch products.");
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, [user]);

  const handleAddToFavorite = async (productId) => {
    // Xử lý thêm hoặc xóa sản phẩm yêu thích
    if (user) {
      try {
        if (isProductFavorite(productId)) {
          await removeFavorite(user.customerId, productId);
          setFavorites((prevFavorites) =>
            prevFavorites.filter((item) => item.productId !== productId)
          );
        } else {
          const response = await addFavorite(productId, user.customerId);
          if (response.success) {
            setFavorites((prevFavorites) => [...prevFavorites, { productId }]);
          } else {
            throw new Error("Failed to add to favorites");
          }
        }
      } catch (error) {
        console.error("Error handling favorite:", error.message);
      }
    } else {
      alert("Please log in to add or remove from favorites.");
    }
  };

  const isProductFavorite = (productId) => {
    return favorites.some((item) => item.productId === productId);
  };

  // Lấy sản phẩm hiện tại cho trang
  const indexOfLastProduct = currentPage * productsPerPage;
  const indexOfFirstProduct = indexOfLastProduct - productsPerPage;
  const currentProducts = products.slice(indexOfFirstProduct, indexOfLastProduct);

  const totalPages = Math.ceil(products.length / productsPerPage);

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  if (loading) return <Loading />;
  if (error) return <div>Error: {error}</div>;

  return (
    <div className="max-w-screen-xl mx-auto py-8">
      <h1 className="text-3xl font-bold mb-6">Product List</h1>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
        {currentProducts.length > 0 ? (
          currentProducts.map((product) => (
            <div
              key={product.productId}
              className="bg-white border rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300 p-4"
            >
              <div className="aspect-w-1 aspect-h-1 aspect-rectangle">
                <a href={`/${product.category.slug}/${product.slug}`}>
                  <img
                    src={`${process.env.PUBLIC_URL}/assets/images/products/${product.banner}`}
                    alt={product.productName}
                    className="w-full h-full object-cover rounded-t-lg"
                  />
                </a>
              </div>
              <h3 className="text-lg font-semibold mt-4">
                <a href={`/${product.category.slug}/${product.slug}`}>
                  {product.productName}
                </a>
              </h3>
              <p className="text-sm text-gray-500 mb-2">{product.description}</p>
              <p className="text-lg font-bold text-red-500 mb-4">Price: ${product.price}</p>
              <div className="flex items-center justify-between space-x-4">
                <ButtonAddCart
                  css="bg-emerald-500 text-white py-2 px-4 rounded-lg hover:bg-blue-600 transition-colors flex-1 h-12 min-w-[120px]"
                  productId={product.productId}
                  productName={product.productName}
                  banner={product.banner}
                  price={product.price}
                  quantity={1}
                />
                {user && (
                  <Button
                    className={`${
                      isProductFavorite(product.productId) ? "bg-pink-500" : "bg-gray-300"
                    } text-white py-2 px-4 rounded-lg flex-1 h-12 min-w-[120px] hover:opacity-80 transition-opacity`}
                    onClick={() => handleAddToFavorite(product.productId)}
                  >
                    ♥ {isProductFavorite(product.productId) ? "Remove from" : "Add to"} Favorite
                  </Button>
                )}
              </div>
            </div>
          ))
        ) : (
          <p>No products found.</p>
        )}
      </div>
      {/* Phân trang */}
      <div className="flex justify-center mt-8 space-x-2">
        {Array.from({ length: totalPages }, (_, index) => (
          <button
            key={index}
            className={`px-4 py-2 rounded-lg ${
              currentPage === index + 1 ? "bg-emerald-500 text-white" : "bg-gray-200"
            }`}
            onClick={() => handlePageChange(index + 1)}
          >
            {index + 1}
          </button>
        ))}
      </div>
    </div>
  );
};

export default ProductList;
