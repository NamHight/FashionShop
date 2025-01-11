import { useParams } from 'react-router';
import { useQuery } from '@tanstack/react-query';
import { getProductDetails } from '../../services/api/ProductService'; // Assuming you have this service for API call
import { useState } from 'react';

function ProductDetail() {
    const { categorySlug, productSlug } = useParams(); // Get URL parameters
    const { data: product, isLoading, isError } = useQuery({
        queryKey: ['productDetails', categorySlug, productSlug], // Query key as an array
        queryFn: () => getProductDetails(categorySlug, productSlug), // Function to fetch product details
    });
    // State for selected image and quantity
    const [selectedImage, setSelectedImage] = useState('https://media.fmplus.com.vn/uploads/products/2406AKUT0042501/1a77582a-cfec-4ac6-83d8-65c9b1d0cbba.webp'); // Default image
    const [quantity, setQuantity] = useState(1);

    // Handle image change when selecting a thumbnail
    const handleImageChange = (imageUrl) => {
        setSelectedImage(imageUrl);
    };

    // Handle quantity change
    const handleQuantityChange = (event) => {
        const value = Math.max(1, event.target.value); // Ensure quantity can't be less than 1
        setQuantity(value);
    };

    if (isLoading) return <div>Loading...</div>;
    if (isError) return <div>Error loading product details</div>;

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



        <div className="flex p-8 space-x-8">
    {/* Left: Product Images and Thumbnail List */}
    <div className="flex flex-col space-y-2 w-1/4">
        {product.images?.map((image, index) => (
            <label key={index} className="cursor-pointer flex-1">
                <img
                    className="w-full h-auto object-cover rounded-lg"
                    src={image.url || 'https://media.fmplus.com.vn/uploads/products/2406AKUT0042501/1a77582a-cfec-4ac6-83d8-65c9b1d0cbba.webp'} // Default to placeholder if image is missing
                    alt={`Thumbnail ${index + 1}`}
                    onClick={() => handleImageChange(image.url)}
                />
            </label>
        ))}
    </div>

    {/* Right: Main Product Image */}
    <div className="flex-1">
        <div className="w-full">
            <img
                className="w-full h-auto rounded-lg"
                src={selectedImage || 'https://media.fmplus.com.vn/uploads/products/2406AKUT0042501/1a77582a-cfec-4ac6-83d8-65c9b1d0cbba.webp'}
                alt={product.productName || 'Product Image'}
            />
        </div>
    </div>

    {/* Product Details */}
    <div className="flex-1.5">
        <h1 className="text-3xl font-semibold mb-4">{product.productName}</h1>
        <p className="text-lg text-gray-600 mb-4">Mã sản phẩm: <span className="font-medium">{product.productId}</span></p>
        <p className="text-2xl font-bold text-red-600 mb-4">{product.price} VND</p>

        <div className="mb-4">
            <p className="text-lg font-medium">Còn lại: {product.quantity}</p>
            <p className="text-lg font-medium">Đã bán: {product.soldCount}</p>
        </div>

        {/* Color and Size Selection (Uncomment if needed) */}
        {/* <div className="mb-4">
            <label className="block text-lg font-medium mb-2">Màu sắc:</label>
            <select className="w-full p-2 border border-gray-300 rounded-lg">
                {product.colors?.map((color, index) => (
                    <option key={index} value={color}>{color}</option>
                ))}
            </select>
        </div>

        <div className="mb-4">
            <label className="block text-lg font-medium mb-2">Kích thước:</label>
            <select className="w-full p-2 border border-gray-300 rounded-lg">
                {product.sizes?.map((size, index) => (
                    <option key={index} value={size}>{size}</option>
                ))}
            </select>
        </div> */}

        {/* Quantity Input */}
        <div className="mb-4">
            <label className="block text-lg font-medium mb-2">Số lượng:</label>
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
            <button className="px-6 py-3 bg-green-500 text-white rounded-lg hover:bg-green-600">Thêm vào giỏ hàng</button>
            <button className="px-6 py-3 bg-blue-500 text-white rounded-lg hover:bg-blue-600">Mua ngay</button>
        </div>

        {/* Shipping and Warranty Policies */}
        <div className="mt-8">
            <h3 className="text-xl font-semibold mb-2">Chính sách vận chuyển</h3>
            <p className="text-gray-600">Thông tin chi tiết về chính sách vận chuyển...</p>
        </div>

        <div className="mt-8">
            <h3 className="text-xl font-semibold mb-2">Bảo hành & Đổi trả</h3>
            <p className="text-gray-600">Thông tin chi tiết về bảo hành và đổi trả...</p>
        </div>
    </div>
</div>
</div>
    );
}

export default ProductDetail;
