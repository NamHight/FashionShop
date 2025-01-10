import React from "react";

const About = () => {
  return (
    <div>
      <section className="sm:flex items-center max-w-screen-xl">
        <div className="sm:w-1/2 p-10">
          <div className="image object-center text-center">
            <img src="/assets/Logo.png" alt="Logo của Fashion Shop"/>
          </div>
        </div>
        <div className="sm:w-1/2 p-5">
          <div className="text">
            <span className="text-gray-500 border-b-2 border-indigo-600 uppercase">
              Giới thiệu về công ty
            </span>
            <h2 className="my-4 font-bold text-3xl  sm:text-4xl ">
              Công ty <span className="text-indigo-600">Fashion Shop</span>
            </h2>
            <h3 className="text-gray-500 font-bold text-xl">
              Nơi thời trang đẳng cấp gặp sự tinh tế.
            </h3>
            <p className="text-gray-500 text-xl">
              Fashion Shop được thành lập vào năm 2025 với mục tiêu mang đến
              những sản phẩm thời trang chất lượng, bắt kịp xu hướng và phù hợp
              với mọi phong cách.
            </p>
            <p className="text-gray-500 text-xl">
              Mang đến sự tự tin và phong cách riêng cho mọi khách hàng.
            </p>
            <p className="text-gray-500 text-xl">Email : fashionShop@gmail.com</p>
            <p className="text-gray-500 text-xl">Số điện thoại : 0979456148</p>
            <p className="text-gray-500 text-xl">Địa chỉ : 123 Street, New York, VietNam</p>
          </div>
        </div>
      </section>
      <section className="flex flex-col mt-20">
        <div className="mt-10 grid grid-cols-2 lg:grid-cols-3 gap-y-5 lg:gap-y-0 gap-x-5 place-items-center w-full mx-auto max-w-7xl px-5">
          <div className="flex flex-col justify-center items-center bg-[#FFF6F3] px-4 h-[126px] w-[100%] md:w-[281px] md:h-[192px] rounded-lg justify-self-center">
            <div className="flex flex-row justify-center items-center">
              <p className="font-bold text-3xl sm:text-4xl lg:text-5xl leading-9 text-primary ml-2">
                1
              </p>
            </div>
            <p className="font-medium text-base sm:text-lg leading-6 mt-3 md:mt-6 text-center">
              Cửa hàng
            </p>
          </div>
          <div className="flex flex-col justify-center items-center bg-[#FFF6F3] px-4 h-[126px] w-[100%] md:w-[281px] md:h-[192px] rounded-lg justify-self-center">
            <div className="flex flex-row justify-center items-center">
              <p className="font-bold text-3xl sm:text-4xl lg:text-5xl leading-9 text-primary ml-2">
                1
              </p>
            </div>
            <p className="font-medium text-base sm:text-lg leading-6 mt-3 md:mt-6 text-center">
              Tỉnh thành
            </p>
          </div>
          <div className="flex flex-col justify-center items-center bg-[#FFF6F3] px-4 h-[126px] w-[100%] md:w-[281px] md:h-[192px] rounded-lg justify-self-center">
            <div className="flex flex-row justify-center items-center">
              <p className="font-bold text-3xl sm:text-4xl lg:text-5xl leading-9 text-primary ml-2">
                6+
              </p>
            </div>
            <p className="font-medium text-base sm:text-lg leading-6 mt-3 md:mt-6 text-center">
              Nhân sự
            </p>
          </div>
        </div>
      </section>
      <section
        id="services"
        className="px-6 sm:px-10 md:px-16 py-12 bg-white dark:bg-gray-800"
      >
        <h2 className="text-indigo-500 text-3xl sm:text-4xl font-semibold mb-6">
          Giá trị cốt lỗi
        </h2>
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
          <div className="bg-gray-100 dark:bg-gray-600 rounded-lg p-6 flex flex-col items-center text-center">
            <i className="fas fa-paint-brush text-indigo-500 text-3xl mb-4"></i>
            <h3 className="text-black dark:text-white text-2xl font-semibold mb-2">
              Chất lượng sản phẩm
            </h3>
            <p className="text-black-600 dark:text-white text-xl">
              Fashion Shop cam kết cung cấp các sản phẩm thời trang đạt tiêu
              chuẩn cao nhất, từ chất liệu đến đường may. Mỗi sản phẩm đều được
              kiểm tra kỹ lưỡng để mang lại sự hài lòng tuyệt đối cho khách
              hàng.
            </p>
          </div>
          <div className="bg-gray-100 dark:bg-gray-600 rounded-lg p-6 flex flex-col items-center text-center">
            <i className="fas fa-code text-indigo-500 text-3xl mb-4"></i>
            <h3 className="text-black dark:text-white text-2xl font-semibold mb-2">
              Đổi mới sáng tạo
            </h3>
            <p className="text-black dark:text-white text-xl">
              Không ngừng nỗ lực để cập nhật và tạo ra những xu hướng mới trong
              ngành thời trang. Fashion Shop tự hào mang đến các bộ sưu tập hiện
              đại, độc đáo, và phù hợp với phong cách cá nhân của từng khách
              hàng.
            </p>
          </div>
          <div className="bg-gray-100 dark:bg-gray-600 rounded-lg p-6 flex flex-col items-center text-center">
            <i className="fas fa-mobile-alt text-indigo-500 text-3xl mb-4"></i>
            <h3 className="text-black dark:text-white text-2xl font-semibold mb-2">
              Khách hàng là trung tâm
            </h3>
            <p className="text-black dark:text-white text-xl">
              Fashion Shop luôn đặt trải nghiệm và sự hài lòng của khách hàng
              làm trọng tâm. Chúng tôi lắng nghe và đáp ứng mọi nhu cầu để mang
              đến dịch vụ tốt nhất, từ tư vấn thời trang đến chăm sóc khách hàng
              sau mua.
            </p>
          </div>
        </div>
      </section>
    </div>
  );
};

export default About;
