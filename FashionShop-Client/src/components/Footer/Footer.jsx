import React, {useEffect, useState} from 'react';
import {Button, IconButton, Input, Spinner, Typography} from "@material-tailwind/react";
import {Link} from "react-router";
import {FiFacebook, FiGithub, FiInstagram} from "react-icons/fi";
import { MdOutlineLocationOn } from "react-icons/md";
import { MdLocalPhone } from "react-icons/md";
import { MdAlternateEmail } from "react-icons/md";
import {useQuery} from "@tanstack/react-query";
import {getWebsiteInfo} from "../../services/api/WebsiteInfoService";
import Loading from "../Loading";
import {getLocalStorage, setLocalStorage} from "../../libs/Helpers/LocalStorageConfig";
const YEAR = new Date().getFullYear();
const ListMenu = [
    {
        name: "Home",
        path: "/",
        classname: "text-sm",
    },
    {
        name: "Men",
        path: "/men",
        classname: "text-sm",
    },
    {
        name: "Female",
        path: "/female",
        classname: "text-sm",
    },
    {
        name: "Kids",
        path: "/kids",
        classname: "text-sm",
    },
    {
        name: "Accessories",
        path: "/accessories",
        classname: "text-sm",
    },
    {
        name: "About us",
        path: "/about",
        classname: "text-sm",
    },
    {
        name: "Blog",
        path: "/blog",
        classname: "text-sm",
    },
    {
        name: "Contact",
        path: "/contact",
        classname: "text-sm",
    }
];
const keyLocalStorage = "websiteInfo";
const Footer = () => {
    const [enableWebsiteInfo, setEnableWebsiteInfo] = useState(false);
    const [loading, setLoading] = useState(true);
    const [websiteInfo, setWebsiteInfo] = useState({});
    const {data: responseData, isLoading, isError, isPending} = useQuery({
        queryKey: ['websiteInfo'],
        queryFn: async () =>{
            let result =  await getWebsiteInfo();
            if(result.status === 401) return null;
            if(result.status === 500) return null;
            if(result.status === 400) return null;
            const calexpiry = 3* 24 * 60;
            setLocalStorage(keyLocalStorage,result?.data,calexpiry);
            setLoading(false);
            console.log(result);
            return result?.data;
        },
        refetchIntervalInBackground: false,
        refetchOnWindowFocus: false,
        enabled:enableWebsiteInfo
    });
    useEffect(() => {
        let getWebsiteInfo = getLocalStorage(keyLocalStorage);

        if(getWebsiteInfo){
            setWebsiteInfo(getWebsiteInfo);
            setLoading(false);
        }else{
            setEnableWebsiteInfo(true);
        }
    }, []);
    if (isError) {
        return <p>Error loading categories</p>;
    }
    return (
        <footer className="w-full mt-3 bg-emerald-400 text-white max-w-full">
            <div className="container mx-auto pt-6">

                <div className="grid grid-cols-4 justify-between gap-x-6 gap-y-4">
                    <div className={"flex justify-center items-center"}>
                        {
                            isLoading || isLoading || loading ?  <Spinner /> : Object.keys(websiteInfo).length !== 0 ? <>
                                <Link to={'/'} className="">
                                    <img src={'assets/Logo.png'} alt="logo" className={'size-32'}/>
                                </Link>
                            </> : <>
                                <Link to={'/'} className="">
                                    <img src={'assets/Logo.png'} alt="logo" className={'size-32'}/>
                                </Link>
                            </>
                        }

                    </div>
                    <ul>
                        {
                            isLoading || isLoading || loading ? <div className={'flex justify-center items-center h-full'}>
                                <Spinner />
                            </div> : Object.keys(websiteInfo).length !== 0 ? <>
                                <Typography className="relative mb-2 font-bold text-white text-xl">
                                    {websiteInfo?.name}
                                    <span className="absolute left-0 bottom-0 w-1/3 border-b-2 border-white"></span>
                                </Typography>
                                <li className={'flex justify-start items-center mb-2'}>

                                    <Typography className={""}>
                                        {websiteInfo?.description}
                                    </Typography>
                                </li>
                                <li className={'flex justify-start items-center mb-2'}>
                                    <MdLocalPhone className={'size-5'}/>
                                    <Typography className={"ml-2"}>
                                        {websiteInfo?.phone}
                                    </Typography>
                                </li>
                                <li className={'flex justify-start items-center mb-2'}>
                                    <MdOutlineLocationOn className={'size-5'}/>
                                    <Typography className={"ml-2"}>
                                        {websiteInfo?.address}
                                    </Typography>
                                </li>
                                <li className={'flex justify-start items-center mb-2'}>
                                    <MdAlternateEmail className={'size-5'}/>
                                    <Typography className={"ml-2"}>
                                        {websiteInfo?.email}
                                    </Typography>
                                </li>
                            </> : <>
                                <Typography className="relative mb-2 font-bold text-white text-xl">
                                    {responseData?.name}
                                    <span className="absolute left-0 bottom-0 w-1/3 border-b-2 border-white"></span>
                                </Typography>
                                <li className={'flex justify-start items-center mb-2'}>
                                    <Typography className={""}>
                                        {responseData?.description}
                                    </Typography>
                                </li>
                                <li className={'flex justify-start items-center mb-2'}>
                                    <MdLocalPhone className={'size-5'}/>
                                    <Typography className={"ml-2"}>
                                        {responseData?.phone}
                                    </Typography>
                                </li>
                                <li className={'flex justify-start items-center mb-2'}>
                                    <MdOutlineLocationOn className={'size-5'}/>
                                    <Typography className={"ml-2"}>
                                        {responseData?.address}
                                    </Typography>
                                </li>
                                <li className={'flex justify-start items-center mb-2'}>
                                    <MdAlternateEmail className={'size-5'}/>
                                    <Typography className={"ml-2"}>
                                        {responseData?.email}
                                    </Typography>
                                </li>
                            </>
                        }

                    </ul>
                    <ul>
                        <Typography className="relative mb-2 font-bold text-white text-xl">
                            Menu
                            <span className="absolute left-0 bottom-0 w-1/3 border-b-2 border-white"></span>
                        </Typography>
                        {
                            ListMenu.map((item,index) => (
                                <li key={index}>
                                    <Link  to={item.path} className="py-2 mb-1 hover:text-red-500">
                                        {item.name}
                                    </Link>
                                </li>
                            ))
                        }

                    </ul>
                    <div>
                        <Typography className={'relative text-xl font-bold mb-2'}>
                        Sign up for newsletter
                            <span className="absolute left-0 bottom-0 w-1/3 border-b-2 border-white"></span>
                        </Typography>
                        <form
                            action="#"
                            className="flex w-full max-w-sm items-center justify-center gap-2 mb-2"
                        >
                            <Input type="email" placeholder="Enter Email" className={'bg-white'}/>
                            <Button type="submit">Subscribe</Button>
                        </form>
                        <Typography className={'mb-2 text-xl font-bold'}>
                            Follow us on
                        </Typography>
                        <div className="flex gap-3 sm:justify-center">
                            <IconButton
                                as="a"
                                href="#"
                                color="secondary"
                                variant="ghost"
                                size="sm"
                                className={'rounded-full bg-amber-300'}
                            >
                                <FiFacebook className="h-4 w-4"/>
                            </IconButton>
                            <IconButton
                                as="a"
                                href="#"
                                color="secondary"
                                variant="ghost"
                                size="sm"
                                className={'rounded-full bg-amber-300'}
                            >
                                <FiInstagram className="h-4 w-4"/>
                            </IconButton>
                            <IconButton
                                as="a"
                                href="#"
                                color="secondary"
                                variant="ghost"
                                size="sm"
                                className={'rounded-full bg-amber-300'}
                            >
                                <FiGithub className="h-4 w-4"/>
                            </IconButton>
                        </div>
                    </div>
                </div>
                <div
                    className="mt-10 flex w-full flex-col items-center justify-center gap-4 border-t border-surface py-4 md:flex-row md:justify-between">
                    <Typography type="small" className="text-center">
                        &copy; Copyright {YEAR}{" "} By
                        <Link to="/" className={'hover:text-red-500'}> Fashion Shop</Link>.
                    </Typography>
                </div>
            </div>
        </footer>
    );
};

export default Footer;