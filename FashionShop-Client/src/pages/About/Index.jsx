import React from "react";
import SupplierList from "../../components/List/SuppilerList";
const About = () => {
  return (
    <div>

      <section className="sm:flex items-center max-w-screen-xl">
        <div className="sm:w-1/2 p-10">
          <div className="image object-center text-center">
            <img src="/assets/Logo.png" alt="Logo của Fashion Shop" />
          </div>
        </div>
        <div className="sm:w-1/2 p-5">
          <div className="text">
            <span className="text-gray-500 border-b-2 border-indigo-600 uppercase">
              Introduction about the company
            </span>
            <h2 className="my-4 font-bold text-3xl  sm:text-4xl ">
              Company <span className="text-indigo-600">Fashion Shop</span>
            </h2>
            <h3 className="text-gray-500 font-bold text-xl">
              Where high fashion meets elegance.
            </h3>
            <p className="text-gray-500 text-xl">
              Fashion Shop was established in 2025 with the goal of providing
              high-quality fashion products that keep up with trends and suit
              every style.
            </p>
            <p className="text-gray-500 text-xl">
              Bringing confidence and a unique style to every customer.
            </p>
            <p className="text-gray-500 text-xl">
              Email : fashionShop@gmail.com
            </p>
            <p className="text-gray-500 text-xl">Phone : 0979456148</p>
            <p className="text-gray-500 text-xl">
              Address : 123 Street, New York, VietNam
            </p>
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
              Store
            </p>
          </div>
          <div className="flex flex-col justify-center items-center bg-[#FFF6F3] px-4 h-[126px] w-[100%] md:w-[281px] md:h-[192px] rounded-lg justify-self-center">
            <div className="flex flex-row justify-center items-center">
              <p className="font-bold text-3xl sm:text-4xl lg:text-5xl leading-9 text-primary ml-2">
                1
              </p>
            </div>
            <p className="font-medium text-base sm:text-lg leading-6 mt-3 md:mt-6 text-center">
              Province/City
            </p>
          </div>
          <div className="flex flex-col justify-center items-center bg-[#FFF6F3] px-4 h-[126px] w-[100%] md:w-[281px] md:h-[192px] rounded-lg justify-self-center">
            <div className="flex flex-row justify-center items-center">
              <p className="font-bold text-3xl sm:text-4xl lg:text-5xl leading-9 text-primary ml-2">
                6+
              </p>
            </div>
            <p className="font-medium text-base sm:text-lg leading-6 mt-3 md:mt-6 text-center">
              Personnel
            </p>
          </div>
        </div>
      </section>
      <section
        id="services"
        className="px-6 sm:px-10 md:px-16 py-12 bg-white dark:bg-gray-800"
      >
        <h2 className="text-indigo-500 text-3xl sm:text-4xl font-semibold mb-6">
          Core Values
        </h2>
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
          <div className="bg-gray-100 dark:bg-gray-600 rounded-lg p-6 flex flex-col items-center text-center">
            <i className="fas fa-paint-brush text-indigo-500 text-3xl mb-4"></i>
            <h3 className="text-black dark:text-white text-2xl font-semibold mb-2">
              Product Quality
            </h3>
            <p className="text-black-600 dark:text-white text-xl">
              Fashion Shop is committed to providing fashion products of the
              highest standards, from materials to craftsmanship. Every product
              is thoroughly inspected to ensure absolute satisfaction for our
              customers.
            </p>
          </div>
          <div className="bg-gray-100 dark:bg-gray-600 rounded-lg p-6 flex flex-col items-center text-center">
            <i className="fas fa-code text-indigo-500 text-3xl mb-4"></i>
            <h3 className="text-black dark:text-white text-2xl font-semibold mb-2">
              Innovation
            </h3>
            <p className="text-black dark:text-white text-xl">
              Continuously striving to update and create new trends in the
              fashion industry, Fashion Shop takes pride in offering modern,
              unique collections tailored to each customer's personal style.
            </p>
          </div>
          <div className="bg-gray-100 dark:bg-gray-600 rounded-lg p-6 flex flex-col items-center text-center">
            <i className="fas fa-mobile-alt text-indigo-500 text-3xl mb-4"></i>
            <h3 className="text-black dark:text-white text-2xl font-semibold mb-2">
              Customer-Centric Approach
            </h3>
            <p className="text-black dark:text-white text-xl">
              Fashion Shop always places customer experience and satisfaction at
              the heart of its mission. We listen and cater to every need to
              deliver the best service, from fashion consulting to post-purchase
              customer care
            </p>
          </div>
        </div>
      </section>
      <SupplierList />
    </div>
  );
};

export default About;
