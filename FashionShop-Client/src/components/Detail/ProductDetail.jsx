import { useParams } from 'react-router';
import { useQuery } from '@tanstack/react-query';
import { getProductDetails, increaseProductView, getProductStats,getProductsBySlug  } from '../../services/api/ProductService'; // API call service
import { useState, useEffect } from 'react';
import { useAuth } from "../../context/AuthContext";
import { GrView } from "react-icons/gr";
import { FaStar, FaRegStar, FaStarHalfAlt } from 'react-icons/fa';
import { FaHeart } from "react-icons/fa";
import ButtonAddCart from '../ButtonAddCart/ButtonAddCart';
import ReviewList from "./Review";
import { Breadcrumb } from "@material-tailwind/react";
import { FcHome } from "react-icons/fc";
import { Button } from "@material-tailwind/react";
import { addReview,checkIfPurchased } from "../../services/api/ReviewService";

function ProductDetail() {
    const [activeTab, setActiveTab] = useState('tab1');
    const { user } = useAuth();
    const { categorySlug, productSlug } = useParams();
    const [productStats, setProductStats] = useState(null);
    const [error, setError] = useState(null);
    const { data: product, isLoading, isError } = useQuery({
        queryKey: ['productDetails', categorySlug, productSlug],
        queryFn: () => getProductDetails(categorySlug, productSlug),
        onSuccess: (data) => console.log('API Response:', data),
    });

    const [quantity, setQuantity] = useState(1);

    const generateSessionId = () => 'session-' + new Date().getTime();

    const handleQuantityChange = (event) => {
        const value = Math.max(1, event.target.value);
        setQuantity(value);
    };

      useEffect(() => {
          const fetchProductStats = async () => {
              try {
                  if (!product?.productId) {
                      setError('Product ID is missing');
                      return;
                  }
                  const stats = await getProductStats(product.productId);
                  setProductStats(stats);
              } catch (error) {
                  setError('Error loading product stats');
                  console.error('Error loading product stats:', error);
              }
          };

          if (product?.productId) fetchProductStats();
      }, [product]);

      useEffect(() => {
          const fetchProductData = async () => {
              try {
                  if (product?.productId) {
                      const customerId = user ? user.customerId : null;
                      const sessionId = sessionStorage.getItem('sessionId') || generateSessionId();
                      await increaseProductView(product.productId, customerId, sessionId);

                      if (!sessionStorage.getItem('sessionId')) {
                          sessionStorage.setItem('sessionId', sessionId);
                      }
                  }
              } catch (error) {
                  console.error('Error when increasing product view:', error);
              }
          };

          if (product?.productId) fetchProductData();
      }, [product, user]);
    


      const [canReview, setCanReview] = useState(false);
      const [rating, setRating] = useState(0);
      const [reviewText, setReviewText] = useState("");
      
    
      const handleRatingChange = (value) => {
        setRating(value);
      };
    
      const handleReviewTextChange = (e) => {
        setReviewText(e.target.value);
      };
      
      const [imageUrl, setImageUrl] = useState(null);
      const [selectedImage, setSelectedImage] = useState(null);
      const [imageError, setImageError] = useState(null);
      const handleImageChange = (event) => {
        const file = event.target.files[0];
        if (file) {
          if (file.type.startsWith("image/")) {
            setImageError(null); 
            setSelectedImage(file); 
            const reader = new FileReader();
            reader.onloadend = () => {
              setImageUrl(reader.result); 
            };
            reader.readAsDataURL(file); 
          } else {
            setImageError("Please upload a valid image file.");
            setImageUrl(null);
            setSelectedImage(null);
          }
        }
      };

      useEffect(() => {
        const checkPurchaseStatus = async () => {
          if (user && product && product.productId) {
            try {
              const result = await checkIfPurchased(user.customerId, product.productId);
              console.log(result);
              setCanReview(result);
            } catch (error) {
              console.error('Error checking purchase status:', error);
            }
          } else {
            console.log('Missing user or product data');
          }
        };
        if (user && product) {
          checkPurchaseStatus();
        }
      }, [user, product]); 
    
      const handleSubmitReview = async (event) => {
        event.preventDefault();
      
        if (!canReview) {
          alert('You need to purchase this product before reviewing!');
          return;
        }
      
        try {
          const response = await addReview(
            product.productId,
            rating,
            reviewText,
            user.customerId,
            selectedImage 
          );
      
          console.log('Review submitted:', response);
      
          alert('Thank you for your review!');
      
        } catch (error) {
          console.error('Error submitting review:', error);
          alert('Failed to submit review. Please try again.');
        }
      };
      


      const [products, setProducts] = useState([]);
      const [currentPage, setCurrentPage] = useState(1);
      const productsPerPage = 5;

      useEffect(() => {
        const fetchProducts = async () => {
          if (!product || !product.productId) {
            return;
          }
          const data = await getProductsBySlug(categorySlug);
      
          const currentProductId = product.productId;
      
          const filteredProducts = data.filter(product => product.productId !== currentProductId);
          setProducts(filteredProducts);
        };
      
        fetchProducts();
      }, [categorySlug, product]); 

      const indexOfLastProduct = currentPage * productsPerPage;
      const indexOfFirstProduct = indexOfLastProduct - productsPerPage;
      const currentProducts = products.slice(indexOfFirstProduct, indexOfLastProduct);

      const handleNextPage = () => {
        if (currentPage * productsPerPage < products.length) {
          setCurrentPage(currentPage + 1);
        }
      };

      const handlePrevPage = () => {
        if (currentPage > 1) {
          setCurrentPage(currentPage - 1);
        }
      };


      if (isLoading) return <div>Loading...</div>;
      if (isError) return <div>Error loading product details</div>;
      if (error) {
          return <div className="text-red-500">{error}</div>;
      }
      if (!product) return <div>No product data found</div>;

    return (
        <div>
            <Breadcrumb className="py-4 px-8">
                <Breadcrumb.Link href="/">
                    <FcHome className="h-[18px] w-[18px]" />
                </Breadcrumb.Link>
                <Breadcrumb.Separator />
                <Breadcrumb.Link href={`/categories/${categorySlug || "default-category"}`}>{product.category.categoryName || "Category"}</Breadcrumb.Link>
                <Breadcrumb.Separator />
                <Breadcrumb.Link href={`/${categorySlug}/${productSlug}`}>{product.productName || "Product Name"}</Breadcrumb.Link>
            </Breadcrumb>

            <div className="flex justify-center p-8">
                <div className="flex flex-wrap gap-8 max-w-screen-lg w-full">
                    <div className="flex-1 max-w-[500px]">
                        <img
                            className="w-full h-auto rounded-lg object-contain"
                            src={product.banner ? `/assets/images/products/${product.banner}` : 'https://media.fmplus.com.vn/uploads/products/2406AKUT0042501/1a77582a-cfec-4ac6-83d8-65c9b1d0cbba.webp'}
                            alt={product.productName || 'Product Image'}
                        />
                    </div>
                    <div className="flex-1.5">
                        <h1 className="text-3xl font-semibold mb-4">{product.productName}</h1>
                        <p className="text-lg text-gray-600 mb-4">Product ID: <span className="font-medium">{product.productId}</span></p>
                        <p className="text-2xl font-bold text-red-600 mb-4">{product.price} VND</p>
                        <div className="mb-4 grid grid-cols-1 md:grid-cols-2 gap-4">
                            <div>
                                <p className="text-lg font-medium">Remaining stock: {product.quantity}</p>
                                <p className="text-lg font-medium">Sold: {product.soldCount}</p>
                            </div>
                            <div className="flex space-x-4">
                                <p className="text-lg font-medium flex items-center">
                                    <FaHeart className="mr-1" /> {productStats?.favoritesCount ?? '0'}
                                </p>
                                <p className="text-lg font-medium flex items-center">
                                    <GrView className="mr-1" /> {productStats?.viewsCount ?? '0'}
                                </p>
                            </div>

                            <p className="text-lg font-medium">
                                <div className="flex items-center">
                                    {[...Array(5)].map((_, index) => {
                                        if (productStats?.averageReview >= index + 1) {
                                            return <FaStar key={index} className="mr-1 text-yellow-500" />;
                                        }
                                        if (productStats?.averageReview >= index + 0.5) {
                                            return <FaStarHalfAlt key={index} className="mr-1 text-yellow-500" />;
                                        }
                                        return <FaRegStar key={index} className="mr-1 text-gray-300" />;
                                    })}
                                </div>
                                <span className="ml-2">{productStats?.averageReview ?? '0'}</span>
                            </p>
                        </div>
                        <div className="mb-4">
                            <label className="block text-lg font-medium mb-2">Quantity:</label>
                            <input
                                type="number"
                                value={quantity}
                                min="1"
                                onChange={handleQuantityChange}
                                className="w-full p-2 border border-gray-300 rounded-lg"
                            />
                        </div>

                        <div className="space-x-4">
                            <ButtonAddCart
                                css="bg-emerald-500 text-white py-2 px-4 rounded-lg hover:bg-blue-600 transition-colors"
                                productId={product.productId}
                                productName={product.productName}
                                banner={product.banner}
                                price={product.price}
                                quantity={quantity}
                            />
                            <button className="px-6 py-3 bg-blue-500 text-white rounded-lg hover:bg-blue-600">Buy Now</button>
                        </div>

                        <div className="mt-8">
                            <h3 className="text-xl font-semibold mb-2">Shipping Policy</h3>
                            <p className="text-gray-600">Detailed information about our shipping policy...</p>
                        </div>
                        <div className="mt-8">
                            <h3 className="text-xl font-semibold mb-2">Warranty & Returns</h3>
                            <p className="text-gray-600">Detailed information about warranty and returns...</p>
                        </div>
                    </div>
                </div>
            </div>

            {/* Tab Contents */}
            <div className="px-8">
                <ul className="flex border-b-2 border-gray-300 mb-4">
                    <li className="mr-4">
                        <button
                            onClick={() => setActiveTab("tab1")}
                            className={`text-lg font-semibold px-4 py-2 ${activeTab === "tab1" ? "border-b-2 border-red-500 text-red-500" : "text-gray-600"}`}
                        >
                            Product Description
                        </button>
                    </li>
                    <li className="mr-4">
                        <button
                            onClick={() => setActiveTab("tab2")}
                            className={`text-lg font-semibold px-4 py-2 ${activeTab === "tab2" ? "border-b-2 border-red-500 text-red-500" : "text-gray-600"}`}
                        >
                            Reviews
                        </button>
                    </li>
                </ul>

                <div>
                    {activeTab === "tab1" && (
                        <div>
                            <h3 className="text-xl font-semibold mb-4">Product Description</h3>
                            <p className="text-gray-700">{product.description}</p>
                        </div>
                    )}
                    {activeTab === "tab2" && (  
                        <div>
                            <div className="bg-white p-6 rounded-lg shadow-md">
      {/* Hiển thị phần yêu cầu đăng nhập nếu chưa đăng nhập */}
      {!user ? (
        <div className="text-center text-gray-600 mb-4">
          <p>Please log in to leave a review.</p>
        </div>
        ) : !canReview ? (
          <div className="text-center text-gray-600 mb-4">
            <p>You need to purchase this product before reviewing!</p>
          </div>
      ) : (
        <div>

          <h3 className="text-xl font-semibold mb-4">Leave a Review</h3>
          {error && (
            <div className="text-red-500 text-sm mb-4">{error}</div>
          )}
          <form onSubmit={handleSubmitReview}>
          
            <div className="mb-4">
              <label className="block text-gray-700 font-medium mb-2">Rating</label>
              <div className="flex space-x-2">
                {[1, 2, 3, 4, 5].map((value) => (
                  <button
                    key={value}
                    type="button"
                    onClick={() => handleRatingChange(value)}
                    className={`${
                      value <= rating ? "text-yellow-500" : "text-gray-300"
                    } text-2xl`}
                  >
                    ★
                  </button>
                ))}
              </div>
            </div>

 
            <div className="mb-4">
              <label className="block text-gray-700 font-medium mb-2">Your Review</label>
              <textarea
                value={reviewText}
                onChange={handleReviewTextChange}
                rows="4"
                className="w-full p-2 border border-gray-300 rounded-md"
                placeholder="Write your review here..."
              ></textarea>
            </div>

            <div className="mb-4">
              <label className="block text-gray-700 font-medium mb-2">Upload Image</label>
              <input
                type="file"
                onChange={handleImageChange}
                className="border border-gray-300 p-2 rounded-md"
              />
              {imageError && <p className="text-red-500 text-sm mt-2">{imageError}</p>}
            </div>
            {imageUrl && (
              <div className="mt-4">
                <h4 className="text-lg font-semibold">Uploaded Image</h4>
                <img src={imageUrl} alt="Review" className="mt-2 max-w-full" />
              </div>
            )}
            <div className="flex justify-end">
              <Button
                type="submit"
                className="bg-emerald-500 text-white px-4 py-2 rounded-lg"
                
              >
            Submit
              </Button>
            </div>
          </form>
        </div>
      )}
    </div>
                            <ReviewList productId={product.productId}/>
                        </div>
                    )}
                </div>
            </div>



            <div className="py-8">
        <h2 className="text-2xl font-semibold mb-6 px-8">
          Relevant products
        </h2>

        
        <div>
      <div className="flex overflow-x-auto space-x-4 px-4 w-full">
        {currentProducts.map((product, index) => (
          <div key={index} className="flex flex-col items-center bg-white shadow-lg rounded-lg p-4 w-1/5 min-w-[200px]">
            <a href={`/${categorySlug}/${productSlug}`} >
            <img className="w-full h-auto rounded-lg mb-4" src={product.banner ? `/assets/images/products/${product.banner}` : 'https://media.fmplus.com.vn/uploads/products/2406AKUT0042501/1a77582a-cfec-4ac6-83d8-65c9b1d0cbba.webp'} alt={product.productName} />
            </a>
            <a
              className="text-center text-lg font-semibold text-gray-800 truncate mb-2"
              title={product.productName}
              href={`/${categorySlug}/${productSlug}`}
            >
              {product.productName}
            </a>
            <div className="text-center text-xl font-semibold text-red-600">
              <span className="price">{product.price}</span>{" "}
              <span className="currency">VND</span>
            </div>
          </div>
        ))}
      </div>

      <div className="flex justify-center mt-4">
        <button
          onClick={handlePrevPage}
          className="px-4 py-2 bg-gray-300 text-gray-800 rounded-md"
          disabled={currentPage === 1}
        >
          Previous
        </button>
        <button
          onClick={handleNextPage}
          className="px-4 py-2 bg-gray-300 text-gray-800 rounded-md ml-2"
          disabled={currentPage * productsPerPage >= products.length}
        >
          Next
        </button>
      </div>
    </div>


      </div>
    </div>


     
    );
}

export default ProductDetail;
