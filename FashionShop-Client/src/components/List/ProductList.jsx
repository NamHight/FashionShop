import React, { useEffect, useState } from "react";
import { getAllProducts } from "../../services/api/ProductService";
import ButtonAddCart from "../ButtonAddCart/ButtonAddCart";
import Loading from "../Loading";
import  "../List/ProductList.css";
import { useAuth } from "../../context/AuthContext";
import { Button } from "@material-tailwind/react";
import { addFavorite, getFavoriteById,removeFavorite } from '../../services/api/FavoriteService';

const ProductList = () => {
  const { user } = useAuth();
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [favorites, setFavorites] = useState([]);
  useEffect(() => {
    const fetchProductsAndFavorites = async () => {
      try {
        // Fetch all products
        const data = await getAllProducts();
        setProducts(data);

        // Fetch favorites if user is logged in
        if (user) {
          const favoriteData = await getFavoriteById(user.customerId);
          setFavorites(favoriteData || []);  // Store favorites into state
        }
      } catch (err) {
        setError("Unable to fetch products.");
      } finally {
        setLoading(false);
      }
    };
  fetchProductsAndFavorites();
  }, [user]); 
  useEffect(() => {
    const fetchProducts= async () => {
     // Thiết lập interval để lấy dữ liệu mỗi 10 giây
     const intervalId = setInterval(() => {
      console.log("Fetching products...");
      fetchProducts();
    }, 10000); // 10 giây

    // Dọn dẹp interval khi component bị unmount
    return () => clearInterval(intervalId);
  }}, []);

    

  const handleAddToFavorite = async (productId) => {
    if (user) {
      try {
        if (isProductFavorite(productId)) {
          console.log("Removing favorite for product:", productId);
          await removeFavorite(user.customerId, productId);
  
          setFavorites((prevFavorites) =>
            prevFavorites.filter((item) => item.productId !== productId)
          );
          alert("Product removed from favorites!");
        } else {
          console.log("Adding favorite for product:", productId);
          const response = await addFavorite(productId, user.customerId);
          console.log("Add favorite response:", response);
  
          if (response.success) {
            setFavorites((prevFavorites) => [
              ...prevFavorites,
              { productId },
            ]);
            alert("Product added to favorites!");
          } else {
            throw new Error("Failed to add to favorites");
          }
        }
      } catch (error) {
        console.error("Error handling favorite:", error.message);
        alert(`Failed to update favorites: ${error.message}`);
      }
    } else {
      alert("Please log in to add or remove from favorites.");
    }
  };
  
  
  const isProductFavorite = (productId) => {
    return favorites.some((item) => item.productId === productId);
  };
  console.log(favorites);
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
              <p className="text-md text-gray-600 mb-4">
                Quantity: {product.quantity}
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
                </div>                
                {user && ( // Chỉ hiển thị nút "Yêu thích" nếu người dùng đã đăng nhập
                (() => {
                  // Kiểm tra xem sản phẩm có trong danh sách yêu thích không
                
                  return (
                    <Button
                    
                    className={`${
                      isProductFavorite(product.productId)
                        ? "bg-pink-500"
                        : "bg-gray-300"
                    } text-white p-2 rounded`}
                    onClick={() => handleAddToFavorite(product.productId)}
                  >
                    ♥{" "}
                    {isProductFavorite(product.productId)
                      ? "Remove from"
                      : "Add to"}{" "}
                    Favorite
                  </Button>
                  );
                })()
              )}
                 

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
