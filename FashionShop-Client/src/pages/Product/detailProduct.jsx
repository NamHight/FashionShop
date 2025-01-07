import React , { useState }from 'react';

const DetailProduct = () => {
    // State to manage the active tab
  const [activeTab, setActiveTab] = useState('tab1');

  // Function to handle tab change
  const handleTabChange = (tabId) => {
    setActiveTab(tabId);
  };
  const [selectedImage, setSelectedImage] = useState('https://media.fmplus.com.vn/uploads/products/2404CVUX2450102/f1f537b9-5c50-4da5-8441-bf3cdaec24a7.webp');
  
  // State for quantity
  const [quantity, setQuantity] = useState(1);

  // Handle the image change when selecting a thumbnail
  const handleImageChange = (imageUrl) => {
    setSelectedImage(imageUrl);
  };
  const handleQuantityChange = (event) => {
    const value = Math.max(1, event.target.value); // Ensure quantity can't be less than 1
    setQuantity(value);
  };
  const handleScroll = (direction) => {
    const container = document.querySelector('.overflow-x-auto');
    if (direction === 'left') {
      container.scrollLeft -= 200;  // Di chuyển 200px sang trái
    } else {
      container.scrollLeft += 200;  // Di chuyển 200px sang phải
    }
  };

    return (
    <div>  
        <ol className="breadcrumb flex flex-wrap p-4 bg-[#FFFDFC]">
      <li className="breadcrumb-item text-base font-medium text-gray-700 capitalize">
        <a href="/" className="hover:text-blue-500">trang chủ</a>
      </li>
      <li className="breadcrumb-item text-base font-medium text-gray-700 capitalize border-l pl-2 ml-2">
        <a href="/nu/chan-vay" className="hover:text-blue-500">category-slug</a>
      </li>
      <li className="breadcrumb-item text-base font-medium text-gray-700 capitalize border-l pl-2 ml-2">
        <a href="/nu/update" className="hover:text-blue-500">detailproduct-slug</a>
      </li>
    </ol>


    <div className="flex p-8 space-x-8">
      {/* Bên trái: Hình ảnh sản phẩm và danh sách mẫu */}
      <div className="flex p-8 space-x-8">
        {/* Bên trái: Hình ảnh mẫu */}
        <div className="flex flex-col space-y-2 w-1/4 h-full">
          {/* Danh sách hình ảnh mẫu */}
          <label htmlFor="sample1" className="cursor-pointer flex-1">
            <img
              className="w-2/3 h-full object-cover rounded-lg"
              src="https://media.fmplus.com.vn/uploads/products/2404CVUX2450102/f1f537b9-5c50-4da5-8441-bf3cdaec24a7.webp"
              alt="Sample 1"
              onClick={() => handleImageChange('https://media.fmplus.com.vn/uploads/products/2404CVUX2450102/f1f537b9-5c50-4da5-8441-bf3cdaec24a7.webp')}
            />
          </label>
          <label htmlFor="sample2" className="cursor-pointer flex-1">
            <img
              className="w-2/3 h-full object-cover rounded-lg"
              src="https://media.fmplus.com.vn/uploads/products/2404CVUX2450102/38cf6a71-25bd-47ff-90e7-c1d3d6911e69.webp"
              alt="Sample 2"
              onClick={() => handleImageChange('https://media.fmplus.com.vn/uploads/products/2404CVUX2450102/38cf6a71-25bd-47ff-90e7-c1d3d6911e69.webp')}
            />
          </label>
          <label htmlFor="sample3" className="cursor-pointer flex-1">
            <img
              className="w-2/3 h-full object-cover rounded-lg"
              src="https://media.fmplus.com.vn/uploads/products/2404CVUX2450102/73bb9420-0d3c-451c-9aa6-d39168ec44dd.webp"
              alt="Sample 3"
              onClick={() => handleImageChange('https://media.fmplus.com.vn/uploads/products/2404CVUX2450102/73bb9420-0d3c-451c-9aa6-d39168ec44dd.webp')}
            />
          </label>
          <label htmlFor="sample4" className="cursor-pointer flex-1">
            <img
              className="w-2/3 h-full object-cover rounded-lg"
              src="https://media.fmplus.com.vn/uploads/products/2404CVUX2450102/7bd2265f-fe4e-4abc-8bff-c87cf36ff6ee.webp"
              alt="Sample 4"
              onClick={() => handleImageChange('https://media.fmplus.com.vn/uploads/products/2404CVUX2450102/7bd2265f-fe4e-4abc-8bff-c87cf36ff6ee.webp')}
            />
          </label>
        </div>

        {/* Bên phải: Hình ảnh sản phẩm chính */}
        <div className="flex-1">
          <div className="w-full">
            {/* Hình ảnh sản phẩm chính */}
            <div className="w-full h-auto">
              <img
                className="w-full h-auto rounded-lg"
                src={selectedImage}
                alt="Chân Váy Nữ Dáng Xoè Đũi Lthun 2 Tầng HL.3888"
              />
            </div>
          </div>
        </div>
      </div>

      {/* Bên phải: Thông tin chi tiết sản phẩm */}
      <div className="flex-1.5">
        <h1 className="text-3xl font-semibold mb-4">
          Chân Váy Nữ Dáng Xoè Đũi Lthun 2 Tầng HL.3888
        </h1>
        <p className="text-lg text-gray-600 mb-4">
          Mã sản phẩm: <span className="font-medium">2404CVUX2450102</span>
        </p>
        <p className="text-2xl font-bold text-red-600 mb-4">199.000 VND</p>

        <div className="mb-4">
          <p className="text-lg font-medium">Còn lại: 10</p>
          <p className="text-lg font-medium">Đã bán: 90</p>
        </div>

        <div className="mb-4">
          <div className="mb-4">
            <label className="block text-lg font-medium mb-2">Màu sắc:</label>
            <select className="w-full p-2 border border-gray-300 rounded-lg">
              <option value="pink">Hồng 0</option>
            </select>
          </div>

          <div>
            <label className="block text-lg font-medium mb-2">Kích thước:</label>
            <select className="w-full p-2 border border-gray-300 rounded-lg">
              <option value="freesize">Freesize</option>
            </select>
          </div>
        </div>

        {/* Phần số lượng */}
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

        {/* Các nút hành động */}
        <div className="space-x-4">
          <button className="px-6 py-3 bg-green-500 text-white rounded-lg hover:bg-green-600">
            Thêm vào giỏ hàng
          </button>
          <button className="px-6 py-3 bg-blue-500 text-white rounded-lg hover:bg-blue-600">
            Mua ngay
          </button>
          <button className="px-6 py-3 bg-yellow-500 text-white rounded-lg hover:bg-yellow-600">
            Tìm tại cửa hàng
          </button>
        </div>

        {/* Chính sách vận chuyển và bảo hành */}
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
          <h2 className="product-description-title">Mô tả sản phẩm</h2>
          <div className="product-details" style={{ padding: '30px 0px' }}>
            <h2>Mô tả sản phẩm</h2>
            <h3><strong>* THÔNG TIN SẢN PHẨM</strong></h3>
            <ul>
              <li>- Tên sản phẩm: Chân Váy Nữ Dáng Xoè đũi lthun 2 tầng</li>
              <li>- Độ tuổi: &gt; 14 tuổi</li>
              <li>- Phù hợp: Mặc đi học, đi làm, đi chơi.</li>
              <li>- Chất liệu: Vải cotton lạnh</li>
              <li>- Họa tiết: Trơn</li>
              <li>- Xuất xứ: Tự thiết kế và sản xuất bởi FM Style tại Việt Nam</li>
            </ul>
            <h3><strong>* HƯỚNG DẪN CHỌN SIZE CHO BẠN</strong></h3>
            <ul>
              <li>- Size S: Eo 64cm, mông 84cm, dài 103cm</li>
              <li>- Size M: Eo 68cm, mông 88cm, dài 103cm</li>
              <li>- Size L: Eo 70cm, mông 92cm, dài 104cm</li>
            </ul>
            <h3><strong>* HƯỚNG DẪN BẢO QUẢN VÀ SỬ DỤNG</strong></h3>
            <ul>
              <li>- Lần đầu chỉ xả nước lạnh rồi phơi khô để đảm bảo chất và màu của sản phẩm.</li>
              <li>- Nhớ lộn trái sản phẩm khi giặt và không giặt ngâm.</li>
              <li>- Không giặt máy trong 2 tuần đầu.</li>
              <li>- Không sử dụng thuốc tẩy.</li>
              <li>- Khi phơi lộn trái và không phơi trực tiếp dưới ánh nắng mặt trời.</li>
            </ul>
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
};
export default DetailProduct;