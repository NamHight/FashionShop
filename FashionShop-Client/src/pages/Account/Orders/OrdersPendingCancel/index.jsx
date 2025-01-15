import NotFound from "../../../../components/NotFound/NotFound";
import { MdOutlineError } from "react-icons/md";
import { FaCartArrowDown } from "react-icons/fa6";
import DataOrder from "../dataOrder";
import { CustomSpinner } from "../../../../components/CustomSpinner";
import { useAuth } from "../../../../context/AuthContext";
import { Spinner } from "@material-tailwind/react";

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
                      <p>Categpry: {item.categoryName}</p>
                      <p>Quantity x{item.quantity}</p>
                      <p>Price: ${item.price}</p>
                    </div>
                  </div>
                </div>
                <div className="w-36 text-xl flex justify-end items-center text-red-600 ">
                  ${item.totalPrice}
                </div>
              </div>
            );
          })}
        </div>
        <div className="px-5 py-5 flex mx-5 justify-between">
          <div>
            <p className="text-red-600 text-xl flex justify-center items-center border border-gray-900 px-10 py-3 rounded">
             <Spinner color="error" className="mr-3"/>  Pending....  
            </p>
          </div>
          <div className="text-xl flex items-center">
            <p className="font-bold flex items-center">Thành Tiền:</p>
            <p className="text-red-600 text-3xl font-normal ml-3">
              ${value.totalAmount}
            </p>
          </div>
        </div>
      </div>
    </>
  );
};

export default function OrdersPendingCancel() {
  const { user } = useAuth();
  const data = DataOrder(user.customerId, "pending cancel");
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
        {data.order.sort((a,b) => b.orderId - a.orderId).map((item) => {
          return <ListOrder key={item.orderId} value={item} />;
        })}
      </div>
    ) : (
      <NotFound
        text={"Order Pending Cancel Don't Have"}
        icon={<MdOutlineError size={80} />}
      />
    );
  };
  return <>{order()}</>;
}
