import { useParams } from 'react-router-dom';
import { useQuery } from '@tanstack/react-query';
import { getProductDetails,increaseProductView, getProductStats  } from '../../services/api/ProductService'; // Assuming you have this service for API call
import { useState, useEffect  } from 'react';
import { useAuth } from "../../context/AuthContext";
import { GrView } from "react-icons/gr";
import { FaStar,FaRegStar, FaStarHalfAlt } from 'react-icons/fa';
import { FaHeart } from "react-icons/fa";
import ButtonAddCart from '../ButtonAddCart/ButtonAddCart';

function ProductDetail() {
    const [activeTab, setActiveTab] = useState('tab1');
    
      // Function to handle tab change
      const handleTabChange = (tabId) => {
        setActiveTab(tabId);
      };
      const handleScroll = (direction) => {
        const container = document.querySelector('.overflow-x-auto');
        if (direction === 'left') {
          container.scrollLeft -= 200;  // Di chuyển 200px sang trái
        } else {
          container.scrollLeft += 200;  // Di chuyển 200px sang phải
        }
      };
    const { user } = useAuth();
    const { categorySlug, productSlug } = useParams(); // Get URL parameters
    const [productStats, setProductStats] = useState(null);
    const [error, setError] = useState(null);
    const { data: product, isLoading, isError } = useQuery({
        queryKey: ['productDetails', categorySlug, productSlug], // Query key as an array
        queryFn: () => getProductDetails(categorySlug, productSlug), // Function to fetch product details
        onSuccess: (data) => {
            console.log('API Response:', data); // Kiểm tra dữ liệu trả về
        }
    });

    const [quantity, setQuantity] = useState(1);
   
    const generateSessionId = () => {
        return 'session-' + new Date().getTime();  // Đây là ví dụ đơn giản, bạn có thể thay đổi cách tạo sessionId
    };
    // Handle quantity change
    const handleQuantityChange = (event) => {
        const value = Math.max(1, event.target.value); // Ensure quantity can't be less than 1
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
      
        if (product?.productId) {  // Ensure product is available before fetching stats
          fetchProductStats();
        }
      }, [product]);  // Dependency on product so it fetches when product data is available
      



    useEffect(() => {
        const fetchProductData = async () => {
            try {
                if (product?.productId) { // Ensure product is defined before trying to access productId
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

        // Trigger the fetchProductData only when product is available
        if (product?.productId) {
            fetchProductData();
        }
    }, [product, user, productSlug, categorySlug]);

        if (isLoading) return <div>Loading...</div>;
        if (isError || error) return <div>Error loading product details</div>;

        if (!product) {
            return <div>No product data found</div>;
        }

    
    
    return (
        <div>



<ol className="breadcrumb flex flex-wrap p-4 bg-[#FFFDFC]">
  <li className="breadcrumb-item text-base font-medium text-gray-700 capitalize">
    <a href="/" className="hover:text-blue-500">Trang chủ</a>
  </li>

  {/* Category breadcrumb */}
  <li className="breadcrumb-item text-base font-medium text-gray-700 capitalize border-l pl-2 ml-2">
    <a
      href={`/${categorySlug || 'default-category'}`} // Fallback in case categorySlug is missing
      className="hover:text-blue-500"
    >
      {product.category.categoryName || 'Category'}
    </a>
  </li>

  {/* Product Name breadcrumb */}
  <li className="breadcrumb-item text-base font-medium text-gray-700 capitalize border-l pl-2 ml-2">
    <a
      href={`/${categorySlug}/${productSlug}`} // Fallback in case productSlug is missing
      className="hover:text-blue-500"
    >
      {product.productName || 'Product Name'}
    </a>
  </li>
</ol>



<div className="flex justify-center p-8">
    <div className="flex flex-wrap space-x-8 gap-8 max-w-screen-lg w-full">
        {/* Left: Product Image */}
        <div className="flex-1 max-w-[500px]">
            <div className="w-full">
                <img
                    className="w-full h-auto rounded-lg object-contain"
                    // Check if product.banner is a valid array or string
                    src={product.banner ? `/assets/images/products/${product.banner}` : 'https://media.fmplus.com.vn/uploads/products/2406AKUT0042501/1a77582a-cfec-4ac6-83d8-65c9b1d0cbba.webp'}
                    alt={product.productName || 'Product Image'}
                />
            </div>
        </div>

        {/* Right: Product Details and Stats */}
        <div className="flex-1 max-w-[600px]">     

            {/* Product Details */}
            <h1 className="text-3xl font-semibold mb-4">{product.productName}</h1>
            <p className="text-lg text-gray-600 mb-4">Product ID: <span className="font-medium">{product.productId}</span></p>
            <p className="text-2xl font-bold text-red-600 mb-4">{product.price} VND</p>

            <div className="mb-4 grid grid-cols-1 md:grid-cols-2 gap-4">
                {/* Left: Remaining Stock and Sold Count */}
                <div>
                    <p className="text-lg font-medium">Remaining stock: {product.quantity}</p>
                    <p className="text-lg font-medium">Sold: {product.soldCount}</p>
                </div>

                {/* Right: Stats - Favorites Count, Views Count, Average Review */}
                <div>
                {/* Display Favorites Count and Views Count on the same row */}
                <div className="flex space-x-4">
                    <p className="text-lg font-medium flex items-center">
                        <FaHeart className="mr-1"/> {productStats?.favoritesCount ?? 'N/A'}
                    </p>
                    <p className="text-lg font-medium flex items-center">
                        <GrView className="mr-1" /> {productStats?.viewsCount ?? 'N/A'}
                    </p>
                </div>

                {/* Display Average Review with stars */}
                <p className="text-lg font-medium">
                    <div className="flex items-center">
                        {/* Loop through 5 stars and fill them based on average review */}
                        {[...Array(5)].map((_, index) => {
                            // Check for full star
                            if (productStats?.averageReview >= index + 1) {
                                return <FaStar key={index} className="mr-1 text-yellow-500" />;
                            }
                            // Check for half star
                            if (productStats?.averageReview >= index + 0.5) {
                                return <FaStarHalfAlt key={index} className="mr-1 text-yellow-500" />;
                            }
                            // Otherwise, show empty star
                            return <FaRegStar key={index} className="mr-1 text-gray-300" />;
                        })}
                    </div>
                    <span className="ml-2">{productStats?.averageReview ?? 'N/A'}</span>
                </p>
            </div>
            </div>

            {/* Quantity Input */}
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

            {/* Action Buttons */}
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

            {/* Shipping and Warranty Policies */}
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


<div className="tab-container px-8"> {/* Thêm padding xung quanh để tạo khoảng cách bên trái và bên phải */}
  {/* Tab navigation */}
  <ul className="flex border-b-2 border-gray-300 mb-4">
    <li className="mr-4">
      <button
        onClick={() => handleTabChange('tab1')}
        className={`text-lg font-semibold px-4 py-2 ${activeTab === 'tab1' ? 'border-b-2 border-red-500 text-red-500' : 'text-gray-600'}`}
      >
        Mô tả sản phẩm
      </button>
    </li>
    <li className="mr-4">
      <button
        onClick={() => handleTabChange('tab2')}
        className={`text-lg font-semibold px-4 py-2 ${activeTab === 'tab2' ? 'border-b-2 border-red-500 text-red-500' : 'text-gray-600'}`}
      >
        Đánh giá
      </button>
    </li>
  </ul>

  {/* Tab Contents */}
  <div>
    {/* Tab 1 - Mô tả sản phẩm */}
    <div
      id="tab1"
      className={`tab-content ${activeTab === 'tab1' ? 'block' : 'hidden'}`}
    >
      <div className="product-description rte">
        <div className="product-info-brand">
          <div className="product-details" style={{ padding: '30px 0px' }}>
           {product.description}
          </div>
        </div>
      </div>
    </div>

    {/* Tab 2 - Đánh giá */}
    <div
      id="tab2"
      className={`tab-content ${activeTab === 'tab2' ? 'block' : 'hidden'}`}
    >
      <div className="tab-detail-product-review">
        <div className="empty-view">Sản phẩm hiện chưa có đánh giá nào</div>
      </div>
    </div>
  </div>
</div>

<div className="py-8">
  <h2 className="text-2xl font-semibold mb-6 px-8">Sản phẩm có liên quan</h2>
  
  {/* Container với overflow-x-auto */}
  <div className="flex items-center">
    {/* Nút di chuyển sang trái */}
    <button
      className="absolute left-0 z-10 bg-gray-500 text-white p-2 rounded-full hover:bg-gray-600"
      onClick={() => handleScroll('left')}
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
          <span className="price">445.000</span> <span className="currency">VND</span>
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
          <span className="price">445.000</span> <span className="currency">VND</span>
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
          <span className="price">445.000</span> <span className="currency">VND</span>
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
          <span className="price">445.000</span> <span className="currency">VND</span>
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
          <span className="price">445.000</span> <span className="currency">VND</span>
        </div>
      </div>
    </div>

    {/* Nút di chuyển sang phải */}
    <button
      className="absolute right-0 z-10 bg-gray-500 text-white p-2 rounded-full hover:bg-gray-600"
      onClick={() => handleScroll('right')}
    >
      &#8594;
    </button>
  </div>
</div>




</div>
    );
}

export default ProductDetail;
