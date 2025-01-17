import NotFound from "../../../../components/NotFound/NotFound";
import { MdOutlineError } from "react-icons/md";
import { FaCartArrowDown } from "react-icons/fa6";
import DataOrder from "../dataOrder";
import { CustomSpinner } from "../../../../components/CustomSpinner";
import { Input } from "@material-tailwind/react";
import { IoSearch } from "react-icons/io5";
import { useAuth } from "../../../../context/AuthContext";
import ReviewButton from "../../../../components/Detail/ReviewButton/ReviewButton"; 

const ListOrder = ({ value }) => {
  
  return (
    <>
      <div className="bg-slate-100 rounded mt-3">
        <div className="flex items-center text-lg mx-5 py-3 border-b border-gray-500">
          <div className="mr-2">
            <FaCartArrowDown />
          </div>
          <h1 className="font-bold">{value.customerName}</h1>
        </div>
        <div className="border-b border-gray-500 mx-5">
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
                      <p>Category: {item.categoryName}</p>
                      <p>Quantity x{item.quantity}</p>
                      <p>Price: ${item.price}</p>
                    </div>
                  </div>
                  <ReviewButton
                  productId={item.productId} 
                />
                </div>
                <div className="w-36 text-xl flex justify-end items-center text-red-600 ">
                  ${item.totalPrice}
                </div>
              </div>
              
            );
          })}
        </div>
        <div className="px-5 py-5 mx-5 text-center md:flex">
          <div className="text-xl w-80 flex justify-end items-center">
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

function OrdersCompleted() {
  const { user } = useAuth();
  const data = DataOrder(user.customerId, "completed");
  console.log("OrdersCompleted", data);
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
        {data.order.sort((a,b) => b.orderId - a.orderId).map((item) => (
          <ListOrder key={item.orderId} value={item} />
        ))}
      </div>
    ) : (
      <NotFound
        text={"Order Completed Don't Have"}
        icon={<MdOutlineError size={80} />}
      />
    );
  };
  return <>{order()}</>;
}

export default OrdersCompleted;
