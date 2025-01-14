import React from "react";
import { Tabs } from "@material-tailwind/react";
import { Link, Outlet, useLocation } from "react-router";

const Orders = () => {
  const location = useLocation(); //lấy url hiện tại
  const currentTab = location.pathname.split("/").pop() //lấy phần tử cuối
  console.log(currentTab);
  
  return (
    <div>
      <Tabs defaultValue={currentTab} onTabChange={(tab) => console.log(tab)}>
        <Tabs.List className="w-full bg-emerald-400">
          <Link to="/account/orders" className="w-1/5" id="Processing">
            <Tabs.Trigger
              className={`w-full font-bold text-lg ${
                currentTab === "orders"
                  ? "bg-gray-50 rounded"
                  : "bg-transparent"
              }`}
              value="Processing"
            >
              Processing
            </Tabs.Trigger>
          </Link>
          <Link to="/account/orders/pendingcancel" className="w-1/5">
            <Tabs.Trigger
              className={`w-full font-bold text-lg ${
                currentTab === "pendingcancel"
                  ? "bg-gray-50 rounded"
                  : "bg-transparent"
              }`}
              value="PendingCancel"
            >
              Pending Cancel
            </Tabs.Trigger>
          </Link>
          <Link to="/account/orders/delivering" className="w-1/5">
            <Tabs.Trigger
              className={`w-full font-bold text-lg ${
                currentTab === "delivering"
                  ? "bg-gray-50 rounded"
                  : "bg-transparent"
              }`}
              value="Delivering"
            >
              Delivering
            </Tabs.Trigger>
          </Link>
          <Link to="/account/orders/completed" className="w-1/5">
            <Tabs.Trigger
              className={`w-full font-bold text-lg ${
                currentTab === "completed"
                  ? "bg-gray-50 rounded"
                  : "bg-transparent"
              }`}
              value="Completed"
            >
              Completed
            </Tabs.Trigger>
          </Link>
          <Link to="/account/orders/canceled" className="w-1/5">
            <Tabs.Trigger
              className={`w-full font-bold text-lg ${
                currentTab === "canceled"
                  ? "bg-gray-50 rounded"
                  : "bg-transparent"
              }`}
              value="Canceled"
            >
              Canceled
            </Tabs.Trigger>
          </Link>
          {/* <Tabs.TriggerIndicator /> */}
        </Tabs.List>
        <Outlet />
      </Tabs>
    </div>
  );
};

export default Orders;
