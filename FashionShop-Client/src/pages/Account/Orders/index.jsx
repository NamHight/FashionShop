import React from "react";
import { Tabs } from "@material-tailwind/react";
import OrdersAll from "./All/index";
import OrdersCancel from "./OrdersCancel/index";
import OrdersCompleted from "./OrdersCompleted/index";
import OrdersPending from "./OrdersPending/index";

const Orders = () => {
  return (
    <>
      <Tabs defaultValue="All">
        <Tabs.List className="w-full">
          <Tabs.Trigger className="w-full font-bold text-lg" value="All">
            All
          </Tabs.Trigger>
          <Tabs.Trigger className="w-full font-bold text-lg" value="Pending">
            Pending
          </Tabs.Trigger>
          <Tabs.Trigger className="w-full font-bold text-lg" value="Completed">
            Completed
          </Tabs.Trigger>
          <Tabs.Trigger className="w-full font-bold text-lg" value="Cancel">
            Cancel
          </Tabs.Trigger>
          <Tabs.TriggerIndicator />
        </Tabs.List>
        <Tabs.Panel value="All">
          <OrdersAll />
        </Tabs.Panel>
        <Tabs.Panel value="Pending">
          <OrdersPending />
        </Tabs.Panel>
        <Tabs.Panel value="Completed">
          <OrdersCompleted />
        </Tabs.Panel>
        <Tabs.Panel value="Cancel">
          <OrdersCancel />
        </Tabs.Panel>
      </Tabs>
    </>
  );
};

export default Orders;
