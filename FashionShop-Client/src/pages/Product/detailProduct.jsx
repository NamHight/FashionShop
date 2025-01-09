import React , { useState }from 'react';
import ProductDetail from "../../components/Detail/ProductDetail";
const DetailProduct = () => {
    // State to manage the active tab
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

    return (
    <div>  

    <ProductDetail/>  

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