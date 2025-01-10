import NotFound from "../../../../components/NotFound/NotFound";
import { MdOutlineError } from "react-icons/md";
import { FaCartArrowDown } from "react-icons/fa6";
import DataOrder from "../dataOrder";
import { Input } from "@material-tailwind/react";
import { CustomSpinner } from "../../../../components/CustomSpinner";
import { IoSearch } from "react-icons/io5";
import { useState } from "react";

const ListOrder = ({ value }) => {
  return (
    <>
      <div className="bg-slate-100 rounded mt-3">
        <div className="flex items-center text-lg pl-5 py-3 border-b border-gray-500">
          <div className="mr-2">
            <FaCartArrowDown />
          </div>
          <h1 className="font-bold">{value.customerName}</h1>
        </div>
        <div className="border-b border-gray-500">
          {value.ordersdetails.map((item) => {
            return (
              <div className="p-5 flex" key={item.orderDetailId}>
                <div className="w-full md:flex">
                  <div>
                    <img
                      src={
                        item.banner
                          ? "/assets/images/products/" + item.banner
                          : "/assets/Logo.png"
                      }
                      alt={item.banner}
                      width={150}
                    />
                  </div>
                  <div className="ml-5 text-xl w-full h-full">
                    <div>
                      <h1 className="font-bold">{item.productName}</h1>
                    </div>
                    <div>
                      <p>Categpry: {item.categoryName}</p>
                      <p>Quantity x{item.quantity}</p>
                      <p>Price: ${item.price}</p>
                    </div>
                  </div>
                </div>
                <div className="w-36 text-xl flex justify-center items-center text-red-600 ">
                  ${item.totalPrice}
                </div>
              </div>
            );
          })}
        </div>
        <div className="px-5 py-5 text-center md:flex">
          <div className="text-xl mx-2 w-full md:text-start">
            <button className="border border-slate-500 px-9 py-3 rounded hover:bg-red-600 hover:text-white">
              Cancel Order
            </button>
          </div>
          <div className="text-xl w-80 flex justify-center items-center">
            <p className="font-bold flex items-center">
              Thành Tiền:{" "}
              <span className="text-red-600 text-3xl font-normal ml-3">
                ${value.totalAmount}
              </span>
            </p>
          </div>
        </div>
      </div>
    </>
  );
};

export default function OrdersPending({ userId }) {
  const data = DataOrder(userId, "pending");
  const [search, setSearch] = useState([]);
  const handleSearch = () => {
    var result = data.order.map((item) =>
      item.ordersdetails.filter((item) =>
        item.productName.toLowerCase().includes(search)
      )
    );
    // return result;
    console.log("result", result);
  };

  console.log("OrdersPending", data);
  const order = () => {
    return data.isLoading ? (
      <CustomSpinner />
    ) : data.isError ? (
      <NotFound
        text={"Error get favorites"}
        icon={<MdOutlineError size={80} />}
      />
    ) : data.order.length > 0 ? (
      <div>
        <div>
          <Input
            type="search"
            placeholder="Search....."
            className={
              "bg-white group-hover:outline-blue-300 hover:border-blue-300 focus:outline-blue-300 group-focus:border-blue-300"
            }
            onChange={(e) => setSearch(e.target.value)}
          >
            <Input.Icon>
              <IoSearch className="h-full w-full text-emerald-400 group-hover:text-blue-300 group-focus:text-blue-300 " />
            </Input.Icon>
          </Input>
        </div>
        {handleSearch()}
        {data.order.map((item) => {
          return <ListOrder key={item.orderId} value={item} />;
        })}
      </div>
    ) : (
      <NotFound
        text={"Order Pending Don't Have"}
        icon={<MdOutlineError size={80} />}
      />
    );
  };
  return <>{order()}</>;
}
