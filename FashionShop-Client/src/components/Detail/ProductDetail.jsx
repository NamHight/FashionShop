import { useParams } from "react-router";
import { useQuery } from "@tanstack/react-query";
import { getProductDetails } from "../../services/api/ProductService"; // Assuming you have this service for API call
import { useState } from "react";
import ReviewList from "./Review";
import { Breadcrumb } from "@material-tailwind/react";
import { FcHome } from "react-icons/fc";
function ProductDetail() {
  // State to manage the active tab
  const [activeTab, setActiveTab] = useState("tab1");

  // Function to handle tab change
  const handleTabChange = (tabId) => {
    setActiveTab(tabId);
  };
  const { categorySlug, productSlug } = useParams(); // Get URL parameters
  const {
    data: product,
    isLoading,
    isError,
  } = useQuery({
    queryKey: ["productDetails", categorySlug, productSlug], // Query key as an array
    queryFn: () => getProductDetails(categorySlug, productSlug), // Function to fetch product details
  });
  // State for selected image and quantity
  const [selectedImage, setSelectedImage] = useState(
    "https://media.fmplus.com.vn/uploads/products/2406AKUT0042501/1a77582a-cfec-4ac6-83d8-65c9b1d0cbba.webp"
  ); // Default image
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
      <Breadcrumb>
      <Breadcrumb.Link href="/">
        <FcHome className="h-[18px] w-[18px]" />
      </Breadcrumb.Link>
      <Breadcrumb.Separator />
      <Breadcrumb.Link href={`/${categorySlug || "default-category"}`}>{product.category.categoryName || "Category"}</Breadcrumb.Link>
      <Breadcrumb.Separator />
      <Breadcrumb.Link href={`/${categorySlug}/${productSlug}`}>{product.productName || "Product Name"}</Breadcrumb.Link>
    </Breadcrumb>
      

      <div className="flex p-8 space-x-8">
        {/* Left: Product Images and Thumbnail List */}
        <div className="flex flex-col space-y-2 w-1/4">
          {product.images?.map((image, index) => (
            <label key={index} className="cursor-pointer flex-1">
              <img
                className="w-full h-auto object-cover rounded-lg"
                src={
                  image.url ||
                  "https://media.fmplus.com.vn/uploads/products/2406AKUT0042501/1a77582a-cfec-4ac6-83d8-65c9b1d0cbba.webp"
                } // Default to placeholder if image is missing
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
              src={
                selectedImage ||
                "https://media.fmplus.com.vn/uploads/products/2406AKUT0042501/1a77582a-cfec-4ac6-83d8-65c9b1d0cbba.webp"
              }
              alt={product.productName || "Product Image"}
            />
          </div>
        </div>

        {/* Product Details */}
        <div className="flex-1.5">
          <h1 className="text-3xl font-semibold mb-4">{product.productName}</h1>
          <p className="text-lg text-gray-600 mb-4">
            Mã sản phẩm:{" "}
            <span className="font-medium">{product.productId}</span>
          </p>
          <p className="text-2xl font-bold text-red-600 mb-4">
            {product.price} VND
          </p>

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
            <button className="px-6 py-3 bg-green-500 text-white rounded-lg hover:bg-green-600">
              Thêm vào giỏ hàng
            </button>
            <button className="px-6 py-3 bg-blue-500 text-white rounded-lg hover:bg-blue-600">
              Mua ngay
            </button>
          </div>

          {/* Shipping and Warranty Policies */}
          <div className="mt-8">
            <h3 className="text-xl font-semibold mb-2">
              Chính sách vận chuyển
            </h3>
            <p className="text-gray-600">
              Thông tin chi tiết về chính sách vận chuyển...
            </p>
          </div>

          <div className="mt-8">
            <h3 className="text-xl font-semibold mb-2">Bảo hành & Đổi trả</h3>
            <p className="text-gray-600">
              Thông tin chi tiết về bảo hành và đổi trả...
            </p>
          </div>
        </div>
      </div>
      <div className="tab-container px-8">
        {" "}
        {/* Thêm padding xung quanh để tạo khoảng cách bên trái và bên phải */}
        {/* Tab navigation */}
        <ul className="flex border-b-2 border-gray-300 mb-4">
          <li className="mr-4">
            <button
              onClick={() => handleTabChange("tab1")}
              className={`text-lg font-semibold px-4 py-2 ${
                activeTab === "tab1"
                  ? "border-b-2 border-red-500 text-red-500"
                  : "text-gray-600"
              }`}
            >
              Mô tả sản phẩm
            </button>
          </li>
          <li className="mr-4">
            <button
              onClick={() => handleTabChange("tab2")}
              className={`text-lg font-semibold px-4 py-2 ${
                activeTab === "tab2"
                  ? "border-b-2 border-red-500 text-red-500"
                  : "text-gray-600"
              }`}
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
            className={`tab-content ${
              activeTab === "tab1" ? "block" : "hidden"
            }`}
          >
            <div className="product-description rte">
              <div className="product-info-brand">
                <h2 className="product-description-title">Mô tả sản phẩm</h2>
                <div
                  className="product-details"
                  style={{ padding: "30px 0px" }}
                >
                  <h2>Mô tả sản phẩm</h2>
                  <h3>
                    <strong>* THÔNG TIN SẢN PHẨM</strong>
                  </h3>
                  <ul>
                    <li>
                      - Tên sản phẩm: Chân Váy Nữ Dáng Xoè đũi lthun 2 tầng
                    </li>
                    <li>- Độ tuổi: &gt; 14 tuổi</li>
                    <li>- Phù hợp: Mặc đi học, đi làm, đi chơi.</li>
                    <li>- Chất liệu: Vải cotton lạnh</li>
                    <li>- Họa tiết: Trơn</li>
                    <li>
                      - Xuất xứ: Tự thiết kế và sản xuất bởi FM Style tại Việt
                      Nam
                    </li>
                  </ul>
                  <h3>
                    <strong>* HƯỚNG DẪN CHỌN SIZE CHO BẠN</strong>
                  </h3>
                  <ul>
                    <li>- Size S: Eo 64cm, mông 84cm, dài 103cm</li>
                    <li>- Size M: Eo 68cm, mông 88cm, dài 103cm</li>
                    <li>- Size L: Eo 70cm, mông 92cm, dài 104cm</li>
                  </ul>
                  <h3>
                    <strong>* HƯỚNG DẪN BẢO QUẢN VÀ SỬ DỤNG</strong>
                  </h3>
                  <ul>
                    <li>
                      - Lần đầu chỉ xả nước lạnh rồi phơi khô để đảm bảo chất và
                      màu của sản phẩm.
                    </li>
                    <li>
                      - Nhớ lộn trái sản phẩm khi giặt và không giặt ngâm.
                    </li>
                    <li>- Không giặt máy trong 2 tuần đầu.</li>
                    <li>- Không sử dụng thuốc tẩy.</li>
                    <li>
                      - Khi phơi lộn trái và không phơi trực tiếp dưới ánh nắng
                      mặt trời.
                    </li>
                  </ul>
                </div>
              </div>
            </div>
          </div>

          {/* Tab 2 - Đánh giá */}
          <div
            id="tab2"
            className={`tab-content ${
              activeTab === "tab2" ? "block" : "hidden"
            }`}
          >
            <div className="tab-detail-product-review">
              <div className="empty-view">
                <ReviewList productId={product.productId} />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ProductDetail;
