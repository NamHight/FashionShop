import React from 'react';
import {Button, Tooltip, Typography,Avatar} from "@material-tailwind/react";
import {Link, useNavigate} from "react-router";
import { TbArrowBigRightLines } from "react-icons/tb";
import {logout} from "../../services/api/AuthServices";
import {toast} from "react-toastify";

const PopoverUserInfo = ({name,avatar,id}) => {
    let navigate = useNavigate();
    const handleLogout = async () =>{
       try {
           var result = await logout(id);
           console.log(result);
           navigate("/",{replace: true});
           window.location.reload();
       }catch (e) {
           console.log(e);
           toast.error("Error logging out");
       }finally {
           toast(result);
       }
    }
    return (
        <Tooltip placement="bottom" interactive>
            <Tooltip.Trigger as={Button} className={'bg-emerald-400 outline-none border-none hover:bg-emerald-400 py-2 w-20 item-center flex gap-2'}>
                <Avatar src={avatar ? avatar : "https://dub.sh/TdSBP0D"} alt="profile-picture" />
            </Tooltip.Trigger>
            <Tooltip.Content className="z-[100000] flex flex-col w-52 border border-surface bg-background px-0 py-2.5 text-foreground">
                <div className={'border-b-2 gap-2 flex flex-col py-3'}>
                    <Typography className={'text-[1.2rem] px-2 font-serif font-semibold cursor-default'}>{name}</Typography>
                </div>
                <div className={'flex-1 flex flex-col gap-2 my-3 justify-start font-bold text-lg'}>
                    <Link to={'/'} className={'group flex items-center px-2 py-2 hover:text-red-500 hover:font-bold hover:opacity-80 hover:bg-gray-400 hover:bg-opacity-30 hover:outline-0'}>
                        <TbArrowBigRightLines className={'group-hover:inline-block hidden mr-1'} />My Profile
                    </Link>
                    <Link to={'/'} className={'group flex items-center px-2 py-2 hover:text-red-500 hover:font-bold hover:opacity-80 hover:bg-gray-400 hover:bg-opacity-30'}>
                        <TbArrowBigRightLines className={'group-hover:inline-block hidden mr-1'} />
                        Orders
                    </Link>
                </div>
                <Button className={'text-center font-bold text-lg mx-2 bg-emerald-400 border-none'} onClick={handleLogout}>
                    Log out
                </Button>
                <Tooltip.Arrow />
            </Tooltip.Content>
        </Tooltip>
    );
};

export default PopoverUserInfo;