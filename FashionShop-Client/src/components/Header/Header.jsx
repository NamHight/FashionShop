import React, {useEffect} from 'react';
import {Link} from "react-router";
import {List, Input, Tooltip, Spinner, IconButton, Typography} from "@material-tailwind/react";
import { IoIosArrowDown } from "react-icons/io";
import ModalLoginRegister from "../Modal/ModalLoginRegister";
import PopoverCart from "../Popover/PopoverCart";
import {IoSearch} from "react-icons/io5";
import {useAuth} from "../../context/AuthContext";
import PopoverUserInfo from "../Popover/PopoverUserInfo";
import {useQuery} from "@tanstack/react-query";
import {getCategories} from "../../services/api/CategoryService";
import NotFound from "../NotFound/NotFound";
import {MdOutlineError} from "react-icons/md";
import {InformationCircleIcon} from "@heroicons/react/16/solid";

const Links = [
    {name: "BLOG", path: "/blog"},
    {name: "ABOUT", path: "/about"},
    {name: "CONTACT", path: "/contact"}
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
    const {user,isLoading,error} = useAuth(); 
    const {data: categories , isLoading: isCategoryLoading, isPending, isError,} = useQuery({
       queryKey: ['category'],
       queryFn: async () => await getCategories(),
        refetchIntervalInBackground:false,
        refetchOnWindowFocus: false,
    });
    const userInfo = isLoading ? <Spinner /> : user != null ? (
        <PopoverUserInfo name={user?.customerName} avatar={user?.avatar} id={user?.customerId}/>
    ) : (
        <ModalLoginRegister/>
    )

    const categoriesList = isError ?
        <Tooltip>
            <Tooltip.Trigger as={IconButton} variant="ghost">
                <InformationCircleIcon className="h-5 w-5 text-white" />
            </Tooltip.Trigger>
            <Tooltip.Content className="w-80 px-2.5 py-1.5 text-black bg-white">
                <Typography type="small" className="opacity-90">
                    Error fetching categories
                </Typography>
                <Tooltip.Arrow />
            </Tooltip.Content>
        </Tooltip>
        : isLoading ? <Spinner className={'size-28'}/>
        : categories?.length > 0 && categories?.map((parent) => (
            <div key={parent.name}>
                {
                    <Tooltip placement="bottom" interactive className={'flex justify-center items-center'}>
                        <Tooltip.Trigger className={'flex justify-center items-center'}>
                            <List.Item className={'flex justify-center items-center hover:bg-emerald-400 group text-white'}>
                                <Link to={"/male"} className={'group-hover:text-red-500'}>{parent.name}</Link>
                                <List.ItemEnd className="ps-1">
                                    <IoIosArrowDown
                                        className={'text-[17px] mt-0.5 group-data-[open=true]:rotate-180 group-hover:text-red-500'}/>
                                </List.ItemEnd>
                            </List.Item>
                        </Tooltip.Trigger>
                        <Tooltip.Content className="text-black z-[100000] grid  rounded-lg border border-surface bg-background p-3 shadow-xl shadow-surface/10 dark:border-surface dark:bg-background">
                            <div className={'flex flex-col text-[1.1rem] font-bold gap-3 justify-start px-2'}>
                                    {
                                        parent.categories?.length > 0 && parent.categories?.map((child,index) => (
                                            <List.Item key={index} className={'flex justify-start items-center '}>
                                                <Link to={child?.categoryName.toLowerCase()} className={"hover:text-red-500 hover:font-bold w-full "}>{child?.categoryName}</Link>
                                            </List.Item>
                                        ))
                                    }
                            </div>
                            <Tooltip.Arrow/>
                        </Tooltip.Content>
                    </Tooltip>
                }
            </div>
        ))  ;
    return (
            <div className={'flex justify-between items-center mx-72 h-full'}>
                <div className={'flex justify-center items-center mx-12'}>
                    <Link to="/">
                        <img src={`/assets/Logo.png`} alt={'logo'} className={"size-28"}/>
                    </Link>
                </div>
                <div className={'w-full flex justify-between items-center'}>
                    <List className={'flex flex-row justify-center items-center gap-1 text-white text-[0.8rem] font-bold'}>
                        <List.Item className={'hover:bg-emerald-400 '}>
                            <Link to={'/'} className={'text-white hover:text-red-500'}>HOME</Link>
                        </List.Item>
                        {categoriesList}
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
                    <Link to="/cart"> <PopoverCart/> </Link>
                    {userInfo}
                </div>
            </div>
    );
};

export default Header;