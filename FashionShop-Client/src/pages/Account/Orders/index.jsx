import React from "react";
import { Tabs } from "@material-tailwind/react";
import OrdersCancel from "./OrdersCancel/index";
import OrdersCompleted from "./OrdersCompleted/index";
import OrdersPending from "./OrdersPending/index";
import { useAuth } from "../../../context/AuthContext";

const Orders = () => {
  const { user } = useAuth();
  return (
    <div>
      <Tabs defaultValue="Pending">
        <Tabs.List className="w-full">
          <Tabs.Trigger className="w-full font-bold text-lg" value="Pending">
            Pending
          </Tabs.Trigger>
          <Tabs.Trigger className="w-full font-bold text-lg" value="Completed">
            Completed
          </Tabs.Trigger>
          <Tabs.Trigger className="w-full font-bold text-lg" value="Canceled">
            Canceled
          </Tabs.Trigger>
          <Tabs.TriggerIndicator />
        </Tabs.List>
        <Tabs.Panel value="Pending">
          <OrdersPending userId={user.customerId} />
        </Tabs.Panel>
        <Tabs.Panel value="Completed">
          <OrdersCompleted userId={user.customerId} />
        </Tabs.Panel>
        <Tabs.Panel value="Canceled">
          <OrdersCancel userId={user.customerId} />
        </Tabs.Panel>
      </Tabs>
    </div>
  );
};

export default Orders;
