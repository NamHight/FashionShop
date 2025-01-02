import Sidebar from "./../../components/SideBar/index";
import { Outlet } from "react-router";

function Account() {
  return (
    <div className="grid grid-cols-5 mx-20 my-5">
      <div>
        <Sidebar />
      </div>
      <div className="col-span-4 rounded px-3">
        <Outlet />
      </div>
    </div>
  );
}

export default Account;
