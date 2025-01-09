import NotFound from "../../../../components/NotFound/NotFound";
import { MdOutlineError } from "react-icons/md";
import { FaCartArrowDown } from "react-icons/fa6";
import DataOrder from "../dataOrder";
import { CustomSpinner } from "../../../../components/CustomSpinner";
import { Input } from "@material-tailwind/react";
import { IoSearch } from "react-icons/io5";

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
                      src="https://th.bing.com/th/id/OIP.Ci-Qe31tIUR4AcmesfCB8AHaHa?w=194&h=194&c=7&r=0&o=5&dpr=1.3&pid=1.7"
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
          <div className="text-xl mx-2 w-1/2 md:text-start">
            <button className="border border-slate-500 px-9 py-3 rounded hover:bg-red-600 hover:text-white">
              Buy Back
            </button>
          </div>
          <div className="text-xl w-1/2 flex justify-end items-center">
            <p className="font-bold flex items-center">
              Thành Tiền:
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

function OrdersCancel({ userId }) {
  const data = DataOrder(userId, "canceled");
  console.log("OrdersCancel", data);

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
            placeholder="Search....."
            className={
              "w-ful bg-white group-hover:outline-blue-300 hover:border-blue-300 focus:outline-blue-300 group-focus:border-blue-300"
            }
          >
            <Input.Icon>
              <IoSearch className="h-full w-full text-emerald-400 group-hover:text-blue-300 group-focus:text-blue-300 " />
            </Input.Icon>
          </Input>
        </div>
        {data.order.map((item) => (
          <ListOrder key={item.orderId} value={item} />
        ))}
      </div>
    ) : (
      <NotFound
        text={"Order Canceled Don't Have"}
        icon={<MdOutlineError size={80} />}
      />
    );
  };
  return <>{order()}</>;
}

export default OrdersCancel;
