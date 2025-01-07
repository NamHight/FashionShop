import React from 'react';
import {Button, IconButton, Input, Typography} from "@material-tailwind/react";
import {Link} from "react-router";
import {FiFacebook, FiGithub, FiInstagram} from "react-icons/fi";
import { MdOutlineLocationOn } from "react-icons/md";
import { MdLocalPhone } from "react-icons/md";
import { MdAlternateEmail } from "react-icons/md";
import {useQuery} from "@tanstack/react-query";
import {getWebsiteInfo} from "../../services/api/WebsiteInfoService";
const YEAR = new Date().getFullYear();
const List = [
    {
        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ab doloremque, " +
            "earum enim libero necessitatibus nobis non quasi quibusd am quidem recusandae repellat, voluptas voluptate? " +
            "Culpa distinctio dolorum impedit iure quidem sapiente?",
        classname: "text-sm",
        title: "description",
    },
    {
        description: "+0123 456 789",
        classname: "ml-2",
        title: "phone",
        icon: <MdLocalPhone className={'size-5'}/>,
    },
    {
        description: "123 Street, New York, USA",
        classname: "ml-2",
        title: "address",
        icon: <MdOutlineLocationOn className={'size-5'}/>,
    },
    {
        description: " WQ6pN@example.com",
        classname: "ml-2",
        title: "email",
        icon: <MdAlternateEmail className={'size-5'}/>,
    }
];
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
const Footer = () => {
    const {data: response, isLoading, isError, isPending} = useQuery({
        queryKey: ['websiteInfo'],
        queryFn: async () => await getWebsiteInfo(),
        refetchOnWindowFocus: false,
        refetchIntervalInBackground: false
    });
    return (
        <footer className="w-full mt-3 bg-emerald-400 text-white max-w-full">
            <div className="mx-72 pt-6">
                <div className="grid grid-cols-4 justify-between gap-x-6 gap-y-4">
                    <div className={"flex justify-center items-center"}>
                        <Link to={'/'} className="">
                            <img src={response?.logo} alt="logo" className={'size-32'}/>
                        </Link>
                    </div>
                    <ul>
                        <Typography className="relative mb-2 font-bold text-white text-xl">
                            {response?.name}
                            <span className="absolute left-0 bottom-0 w-1/3 border-b-2 border-white"></span>
                        </Typography>
                        <li className={'flex justify-start items-center mb-2'}>

                            <Typography className={""}>
                                {response?.description}
                            </Typography>
                        </li>
                        <li className={'flex justify-start items-center mb-2'}>
                            <MdLocalPhone className={'size-5'}/>
                            <Typography className={"ml-2"}>
                                {response?.phone}
                            </Typography>
                        </li>
                        <li className={'flex justify-start items-center mb-2'}>
                            <MdOutlineLocationOn className={'size-5'}/>
                            <Typography className={"ml-2"}>
                                {response?.address}
                            </Typography>
                        </li>
                        <li className={'flex justify-start items-center mb-2'}>
                            <MdAlternateEmail className={'size-5'}/>
                            <Typography className={"ml-2"}>
                                {response?.email}
                            </Typography>
                        </li>
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