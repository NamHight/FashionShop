import React from 'react';
import {Link} from "react-router";
import {IconButton, Input, List, Spinner, Tooltip, Typography} from "@material-tailwind/react";
import {IoIosArrowDown} from "react-icons/io";
import ModalLoginRegister from "../Modal/ModalLoginRegister";
import PopoverCart from "../Popover/PopoverCart";
import {IoSearch} from "react-icons/io5";
import {useAuth} from "../../context/AuthContext";
import PopoverUserInfo from "../Popover/PopoverUserInfo";
import {useQuery} from "@tanstack/react-query";
import {getCategories} from "../../services/api/CategoryService";
import {InformationCircleIcon} from "@heroicons/react/16/solid";

const Links = [
    {name: "BLOG", path: "/blog"},
    {name: "ABOUT", path: "/about"},
    {name: "CONTACT", path: "/contact"}
];

function NavLinks() {
    return (
        <>
            {
                Links.map((item) => (
                    <List.Item key={item.name} className={'hover:bg-emerald-400'}>
                        <Link to={item.path} className={'text-white hover:text-red-500'}>{item.name}</Link>
                    </List.Item>
                ))
            }
        </>
    );
}

const Header = () => {
    const {user, isLoading, error} = useAuth();
    const {data: categories, isLoading: isCategoryLoading, isPending, isError,} = useQuery({
        queryKey: ['category'],
        queryFn: async () => await getCategories(),
        refetchIntervalInBackground: false,
        refetchOnWindowFocus: false,
    });
    const userInfo = isLoading ? <Spinner/> : user != null ? (
        <PopoverUserInfo name={user?.customerName} avatar={user?.avatar} id={user?.customerId}/>
    ) : (
        <ModalLoginRegister/>
    )

    const categoriesList = isError ?
        <Tooltip>
            <Tooltip.Trigger as={IconButton} variant="ghost">
                <InformationCircleIcon className="h-5 w-5 text-white"/>
            </Tooltip.Trigger>
            <Tooltip.Content className="w-80 px-2.5 py-1.5 text-black bg-white">
                <Typography type="small" className="opacity-90">
                    Error fetching categories
                </Typography>
                <Tooltip.Arrow/>
            </Tooltip.Content>
        </Tooltip>
        : isLoading || isPending ? <Spinner className={'size-28'}/>
            : categories?.length > 0 && categories?.map((parent) => (
            <div key={parent.name}>
                {
                    <Tooltip placement="bottom" interactive className={'flex justify-center items-center w-auto'}>
                        <Tooltip.Trigger className={'flex justify-center items-center'}>
                            <List.Item
                                className={'flex justify-center items-center hover:bg-emerald-400 group text-white'}>
                                <Link to={"/male"} className={'group-hover:text-red-500'}>{parent.name}</Link>
                                <List.ItemEnd className="ps-1">
                                    <IoIosArrowDown
                                        className={'text-[17px] mt-0.5 group-data-[open=true]:rotate-180 group-hover:text-red-500'}/>
                                </List.ItemEnd>
                            </List.Item>
                        </Tooltip.Trigger>
                        <Tooltip.Content
                            className="text-black z-[100000] grid  rounded-lg border border-surface bg-background p-3 shadow-xl shadow-surface/10 dark:border-surface dark:bg-background">
                            <div className={'flex flex-col text-[1.1rem] font-bold gap-3 justify-start px-2'}>
                                {
                                    parent.categories?.length > 0 && parent.categories?.map((child, index) => (
                                        <List.Item key={index} className={'flex justify-start items-center '}>
                                            <Link to={child?.categoryName.toLowerCase()}
                                                  className={"hover:text-red-500 hover:font-bold w-full "}>{child?.categoryName}</Link>
                                        </List.Item>
                                    ))
                                }
                            </div>
                            <Tooltip.Arrow/>
                        </Tooltip.Content>
                    </Tooltip>
                }
            </div>
        ));
    return (
        <div className={'container flex justify-center items-center mx-auto z-100 xl:h-[5rem] lg:h-[4.5rem] md:h-[4rem] h-[3.5rem] '}>
            <div className={'flex justify-center items-center'}>
                <Link to="/" className={'md:w-[4rem] xl:w-[7rem] sm:w-[3rem] lg:w-[5rem]'}>
                    <img src={`/assets/Logo.png`} alt={'logo'}
                         className={"w-[7rem] md:w-[4rem] xl:w-[7rem] sm:w-[3rem] lg:w-[5rem] h-fit"}/>
                </Link>
            </div>
            <div className={'flex justify-center items-center w-full md:w-[80%] lg:w-[80%] xl:w-[75%]'}>
                <List
                    className={'grid grid-cols-[7fr,5fr] md:grid-cols-[8fr,5fr] xl:grid-cols-[7fr,5fr] lg:grid-cols-[7fr,5fr]  gap-2 w-full h-full justify-center items-center font-bold text-white text-[0.8rem] xl:text-[0.8rem] lg:text-[0.7rem] md:text-[0.6rem]'}>
                    <div className={'flex justify-center items-center '}>
                        <List.Item className={'hover:bg-emerald-400'}>
                            <Link to={'/'} className={'text-white hover:text-red-500'}>HOME</Link>
                        </List.Item>
                        {categoriesList}
                        <NavLinks/>
                    </div>
                    <li className={'group w-full flex justify-center items-center '}>
                        <div className={'hidden sm:flex md:flex xl:hidden lg:hidden justify-center items-center w-full relative group md:w-[1rem]'}>
                            <Input
                                className={' md:px-2 group-hover:pr-6 pl-3 group-hover:bg-white absolute shadow-none opacity-0 group-hover:opacity-100  border-0 group-hover:w-[20rem] transition-all duration-300 ease-in-out top-[-1.13rem] right-[-1rem] group-hover:flex flex group-hover:transition-all group-hover:duration-300 group-hover:ease-in-out justify-center items-center max-w-[20rem] rounded-l-3xl rounded-r-3xl border-t-0 outline-0 '}
                                type={'search'} placeholder={"Searching....."}/>
                            <div
                                className={'group-hover:rounded-l-none  transition-all  duration-300 ease-in-out  md:flex md:w-[2rem] md:h-[2.3rem] md:rounded-l-3xl md:rounded-r-3xl w-[2rem] md:justify-center md:items-center flex justify-center items-center absolute'}>
                                <IoSearch
                                    className="md:block group-hover:text-emerald-500 md:w-[2em] w-[2rem] h-auto md:h-auto font-bold  hidden"/>
                            </div>
                        </div>
                        <Input
                            type="search" placeholder="Searching....."
                            className={' xl:block lg:block sm:hidden  md:hidden bg-white group-hover:outline-blue-300 hover:border-blue-300 focus:outline-blue-300 group-focus:border-blue-300'}>
                            <Input.Icon>
                                <IoSearch
                                    className="xl:block lg:block md:hidden h-full w-full text-emerald-400 group-hover:text-blue-300 group-focus:text-blue-300 "/>
                            </Input.Icon>
                        </Input>
                    </li>
                </List>
            </div>
            <div className={"flex justify-center items-center gap-1 px-2 "}>
                <Link to="/cart"> <PopoverCart/> </Link>
                {userInfo}
            </div>
        </div>
    );
};

export default Header;