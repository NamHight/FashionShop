import { List } from '@material-tailwind/react';
import { Link} from 'react-router';
import { FaUserCircle,FaClipboardList,FaHeart  } from "react-icons/fa";
const Links = [
    {name: "My Profile", path: "/account", icon: <FaUserCircle size={30}/>},
    {name: "Orders", path: "/account/orders", icon: <FaClipboardList size={30}/>},
    {name: "List Favorite", path: "/account/listfavorite", icon: <FaHeart size={30}/>},

];
function Sidebar(){
    return (
      <div className='bg-slate-100 rounded'>
          {
           Links.map((item)=>(
             <List.Item key={item.name}>
                 <Link to={item.path} className='flex items-center text-xl w-full'><span className='mr-3'>{item.icon}</span>{item.name}</Link>
             </List.Item>
           ))
          }
      </div>
    );
}

export default Sidebar;
