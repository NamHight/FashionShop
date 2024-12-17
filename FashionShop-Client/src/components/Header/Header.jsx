import React from 'react';
import {Link} from "react-router";
import {List,Input, Tooltip, IconButton, Button,Typography} from "@material-tailwind/react";
import { IoSearch } from "react-icons/io5";
import { MdShoppingCart } from "react-icons/md";
import { RxAvatar } from "react-icons/rx";
import { IoIosArrowDown } from "react-icons/io";
import ModalLogin from "../Modal/ModalLogin";

const Links = [
    {name: "BLOG", path: "/blog"},
    {name: "ABOUT", path: "/about"},
    {name: "CONTACT", path: "/contact"},
];
function NavLinks(){
    return (
      <>
          {
           Links.map((item)=>(
             <List.Item key={item.name} className={'hover:bg-emerald-400'}>
                 <Link to={item.path} className={'text-white hover:text-red-500'}>{item.name}</Link>
             </List.Item>
           ))
          }
      </>
    );
}

const Header = () => {

    return (
            <div className={'flex justify-between items-center mx-72 h-full'}>
                <div className={'flex justify-center items-center mx-12'}>
                    <Link to="/">
                        <img src={`/assets/Logo.png`} alt={'logo'} className={"size-28"}/>
                    </Link>
                </div>
                <div className={'w-full flex justify-between items-center'}>
                    <List className={'flex flex-row justify-center items-center gap-4 text-white text-lg font-bold'}>
                        <List.Item className={'hover:bg-emerald-400 '}>
                            <Link to={'/'} className={'text-white hover:text-red-500'}>HOME</Link>
                        </List.Item>
                        <Tooltip placement="bottom" interactive>
                            <Tooltip.Trigger className={'flex justify-center items-center'}>
                                <List.Item className={'hover:bg-emerald-400 group '}>
                                    <Link to={'/about'} className={'group-hover:text-red-500'}>PRODUCTS</Link>
                                    <List.ItemEnd className="ps-1">
                                        <IoIosArrowDown className={'text-[17px] mt-0.5 group-data-[open=true]:rotate-180 group-hover:text-red-500'}/>
                                    </List.ItemEnd>
                                </List.Item>
                            </Tooltip.Trigger>
                            <Tooltip.Content className="z-[100000] grid max-w-screen-xl rounded-lg border border-surface bg-background p-2 shadow-xl shadow-surface/10 dark:border-surface dark:bg-background">
                                <ul className="grid grid-cols-3 gap-y-2 text-black w-36">{"Tuấn"}</ul>
                                <Tooltip.Arrow />
                            </Tooltip.Content>
                        </Tooltip>
                        <NavLinks/>
                    </List>
                    <div className={'group'}>
                        <Input placeholder="Searching....." className={'w-96 bg-white group-hover:outline-blue-300 hover:border-blue-300 focus:outline-blue-300 group-focus:border-blue-300'}>
                            <Input.Icon>
                                <IoSearch
                                    className="h-full w-full text-emerald-400 group-hover:text-blue-300 group-focus:text-blue-300 "/>
                            </Input.Icon>
                        </Input>
                    </div>
                </div>
                <div className={"flex justify-center items-center gap-1 ml-3"}>
                    <IconButton variant="ghost" className={'relative'}>
                        <span className={'absolute top-0 right-1 bg-red-800 rounded-full h-[13px] p-2 w-[13px] flex justify-center items-center text-[12px] text-white'}>0</span>
                        <MdShoppingCart className="h-6 w-6 text-white hover:text-red-500" />
                    </IconButton>
                    <ModalLogin/>
                </div>
            </div>
    );
};

export default Header;