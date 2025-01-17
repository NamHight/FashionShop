import {Typography } from "@material-tailwind/react";
import Sidebar from "./../../components/SideBar/index";
import { Outlet } from "react-router";
import { useAuth } from "../../context/AuthContext";

function Account() {
  const { user } = useAuth();
  return (
    <div className="grid grid-cols-5 w-full my-5">
      <div>
        <div className="bg-emerald-400 rounded flex justify-center items-center text-white mb-2 py-[0.4rem] w-full">
          <div className="justify-center flex items-center">
            <div className="mr-4 w-10 h-9 flex justify-center items-center rounded">
              <img
                alt={user?.avatar}
                src={
                  user.avatar
                    ? "/assets/images/avatar/" + user.avatar
                    : "/assets/images/Logo.png"
                }
                className="border border-red-600 ring-4 ring-blue-500/20 object-cover cursor-pointer rounded-full"
              />
            </div>
            <Typography className="font-bold cursor-default ">
              {user?.customerName}
            </Typography>
          </div>
        </div>
        <Sidebar />
      </div>
      <div className="col-span-4 rounded pl-3">
        <Outlet />
      </div>
    </div>
  );
}

export default Account;
