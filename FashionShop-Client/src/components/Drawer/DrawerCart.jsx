import React from 'react';
import {Button, Drawer, IconButton, Typography,Chip} from "@material-tailwind/react";
import { IoCloseSharp } from "react-icons/io5";
import {MdShoppingCart} from "react-icons/md";
import { HiShoppingCart } from "react-icons/hi";

const DrawerCart = () => {
    return (
        <Drawer>
            <Drawer.Trigger as={Button} variant="ghost" className={'relative px-2.5'}>
                    <span className={'absolute top-0 right-1 bg-red-800 rounded-full h-[13px] p-2 w-[13px] flex justify-center items-center text-[12px] text-white'}>0</span>
                    <MdShoppingCart className="h-6 w-6 text-white hover:text-red-500" />
            </Drawer.Trigger>
            <Drawer.Overlay className={'bg-transparent'}>
                <Drawer.Panel placement="right" className={'p-0 w-[23rem]'}>
                    <div className="flex items-center justify-between gap-4 bg-emerald-400 w-full h-14 px-5 border-b-2 border-blue-400 shadow-blue-700 ">
                        <Typography type="h6" className={'text-white flex justify-center items-center gap-3'}><HiShoppingCart/>Cart</Typography>
                        <Drawer.DismissTrigger
                            as={IconButton}
                            size="sm"Ư
                            variant="ghost"
                            className=" group group-hover:text-red-500"
                            isCircular
                            color="secondary"
                        >
                            <IoCloseSharp className="h-5 w-5 group-hover:text-red-500" />
                        </Drawer.DismissTrigger>
                    </div>
                    <div className={'py-2 border-b-2'}>
                        <Chip>
                            <Chip.Label>Products</Chip.Label>
                        </Chip>
                    </div>
                    <Typography className="mb-6 mt-2 text-foreground">
                        Material Tailwind features multiple React and HTML components, all
                        written with Tailwind CSS classes and Material Design guidelines.
                    </Typography>
                    <div className="mb-1 flex items-center gap-2">
                        <Button>Get Started</Button>
                        <Button color="secondary">Documentation</Button>
                    </div>
                </Drawer.Panel>
            </Drawer.Overlay>
        </Drawer>
    );
};

export default DrawerCart;