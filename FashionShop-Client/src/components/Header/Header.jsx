import React, {useEffect, useLayoutEffect, useState} from 'react';
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
import Cookies from "js-cookie";
import {getLocalStorage, setLocalStorage} from "../../libs/Helpers/LocalStorageConfig";
import {useDebounce} from "../../hooks/useDebounce";
import {searchProductByName} from "../../services/api/ProductService";
import ProductSearch from "../List/ProductSearch";

const Links = [
    {name: "BLOG", path: "/blog"},
    {name: "ABOUT", path: "/about"},
    {name: "CONTACT", path: "/contact"}
];
const dataTemp = [
    "Nam",
    "Tùng",
    "Huy",
    "Minh",
    "Phương",
    "Linh",
    "Anh",
    "Trung",
    "Lan",
    "Thảo"
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
    const [categoriesLocalStorage, setCategoriesLocalStorage] = useState([]);
    const [activeCategories, setActiveCategories] = useState(false);
    const [loading, setLoading] = useState(true);
    const [search, setSearch] = useState('');
    const searchTemp = useDebounce(search, 1000);
    const [activeSearch, setActiveSearch] = useState(false);
    const {data: categories, isLoading: isCategoryLoading, isPending, isError,} = useQuery({
        queryKey: ['category'],
        queryFn: async () => {
           let result = await getCategories();
           if(result.status === 401) return null;
           if(result.status === 500) return null;
           const calexpiry = 7* 24 * 60;
           setLoading(false);
           setLocalStorage("categories",result.data,calexpiry);
           return result?.data;
        },
        refetchIntervalInBackground: false,
        refetchOnWindowFocus: false,
        enabled: activeCategories
    });
    const {data: searchProducts , isLoading: isSearchLoading,isPending: isSearchPending,isError: isSearchError} = useQuery({
        queryKey: ['search',searchTemp],
        queryFn: async () => {
            let result = await searchProductByName(searchTemp,"desc");
            console.log(result);
            if(result?.status === 401) return [];
            if(result?.status === 400) return [];
            if(result?.status === 500) return [];
            return Array.isArray(result?.data) ? result.data : [];
        },
        refetchIntervalInBackground: false,
        refetchOnWindowFocus: false,
        enabled: activeSearch
    })
    useEffect(() => {
        let getCategoriesCookie = getLocalStorage("categories");
        if (getCategoriesCookie) {
            setLoading(false);
            setCategoriesLocalStorage(getCategoriesCookie);
        }else{
            setActiveCategories(true);
        }
    }, []);
    useEffect(() => {
        if(searchTemp.length > 0){
            setActiveSearch(true);
        }
    }, [searchTemp,searchProducts]);
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
        :  loading ? <Spinner className={'size-28'}/>
            :  categoriesLocalStorage?.length > 0 ? categoriesLocalStorage?.map((parent) => (
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
                                            <Link to={`/categories/${child?.slug.toLowerCase()}`}
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
        )) : categories?.map((parent) => (
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
    const handleSearch = (event) => {
        setSearch(event.target.value);
    }
    const handleRenderingSearch = () =>{
        return (
            searchTemp && <div>
                <div className={'xl:flex lg:flex md:hidden xl:max-h-[28rem] xl:overflow-y-auto lg:max-h-[28rem] lg:overflow-y-auto sm:hidden rounded flex-col text-[1.1rem] text-black font-bold gap-3 justify-start p-2 px-4 font-sans absolute top-10 left-0 bg-white w-full'}>
                    <div className={'border-b-2 flex justify-between items-center'}>
                        <p>Result: <span className={'text-emerald-500'}>{searchProducts?.length || 0}</span> </p>
                    </div>
                    {
                        isSearchLoading || isSearchPending
                            ? <Spinner/>
                            : Array.isArray(searchProducts) && searchProducts?.length > 0
                                ? searchProducts?.map((item) => (
                                    <ProductSearch productDetail={item}   key={item.productId}/>
                                )) : <p className={'tracking-normal font-sans px-2'}>Product not found</p>
                    }
                </div>
            </div>
        )
    }
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
                                onChange={(e) => handleSearch(e)}
                                className={' md:px-2 group-hover:pr-6 pl-3 group-hover:bg-white absolute shadow-none opacity-0 group-hover:opacity-100  border-0 group-hover:w-[20rem] transition-all duration-300 ease-in-out top-[-1.13rem] right-[-1rem] group-hover:flex flex group-hover:transition-all group-hover:duration-300 group-hover:ease-in-out justify-center items-center max-w-[20rem] rounded-l-3xl rounded-r-3xl border-t-0 outline-0 '}
                                type={'search'} placeholder={"Searching....."}/>
                            <div
                                className={'group-hover:rounded-l-none  transition-all  duration-300 ease-in-out  md:flex md:w-[2rem] md:h-[2.3rem] md:rounded-l-3xl md:rounded-r-3xl w-[2rem] md:justify-center md:items-center flex justify-center items-center absolute'}>
                                <IoSearch
                                    className="md:block group-hover:text-emerald-500 md:w-[2em] w-[2rem] h-auto ml-2 md:h-auto font-bold  hidden"/>
                            </div>
                            {
                                searchTemp && <div>
                                    <div
                                        className={'group-hover:flex md:hidden xl:hidden md:over lg:hidden hidden sm:flex md:max-h-96 md:overflow-y-auto sm:max-h-96 sm:overflow-y-auto md:w-[20rem] md:transition-all md:duration-300 md:ease-in rounded flex-col text-[1.1rem] group-hover:transition-all group-hover:duration-300 group-hover:ease-in-out text-black font-bold gap-3 justify-start p-2 px-4 font-sans absolute top-5 right-[-1rem] bg-white'}>
                                        <div className={'border-b-2 flex justify-between items-center'}>
                                            <p className={'md:text-[0.9rem] text-[0.7rem]'}>Result: <span
                                                className={'text-emerald-500'}>{searchProducts?.length || 0}</span></p>
                                        </div>
                                        {
                                            searchProducts?.length > 0 ? searchProducts?.map((item) => (
                                                <ProductSearch productDetail={item} key={item.productId + item.categoryId}/>
                                            )) : <p className={'tracking-normal font-sans px-2'}>Product not found</p>
                                        }
                                    </div>
                                </div>
                            }
                        </div>
                        <div className={'relative flex justify-center items-center w-full'}>
                        <Input
                                onChange={(e) => handleSearch(e)}
                                type="search" placeholder="Searching....."
                                className={' xl:block lg:block sm:hidden  md:hidden bg-white duration-300 ease-in-out  transition-all group-hover:outline-blue-300 hover:border-blue-300 focus:outline-blue-300 group-focus:border-blue-300'}>
                                <Input.Icon>
                                    <IoSearch
                                        className="xl:block lg:block md:hidden h-full w-full text-emerald-400 group-hover:text-blue-300 group-focus:text-blue-300 "/>
                                </Input.Icon>
                            </Input>
                            {
                                handleRenderingSearch()
                            }
                        </div>
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