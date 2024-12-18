import React from 'react';
import {Button, Tooltip, Typography} from "@material-tailwind/react";
import {MdShoppingCart} from "react-icons/md";
import CardCart from "../Card/CardCart";
const cart = [
    { name: "Product1", price: 1000, quantity: 2, size: "xl", color: "red" },
    { name: "Product2", price: 500, quantity: 1, size: "m", color: "blue" },
    { name: "Product3", price: 750, quantity: 3, size: "l", color: "green" },
    { name: "Product4", price: 300, quantity: 2, size: "s", color: "yellow" },
    { name: "Product5", price: 1200, quantity: 1, size: "xl", color: "black" },
    { name: "Product6", price: 900, quantity: 4, size: "m", color: "white" },
    { name: "Product7", price: 650, quantity: 2, size: "l", color: "red" },
    { name: "Product8", price: 400, quantity: 1, size: "s", color: "blue" },
    { name: "Product9", price: 800, quantity: 3, size: "xxl", color: "green" },
    { name: "Product10", price: 1100, quantity: 2, size: "xl", color: "yellow" },
];
const PopoverCart = () => {
    return (
        <Tooltip placement="bottom-end" interactive>
            <Tooltip.Trigger as={Button} variant="ghost" className={'relative px-2.5 group'}>
                <span className={'absolute top-0 right-1 bg-red-800 group-hover:text-red-500 rounded-full h-[13px] p-2 w-[13px] flex justify-center items-center text-[12px] text-white'}>0</span>
                <MdShoppingCart className="h-6 w-6 text-white group-hover:text-red-500" />
            </Tooltip.Trigger>
            <Tooltip.Content className={'bg-white w-[28rem] max-w-[28rem] text-black h-[41rem] max-h-[41rem] relative'}>
                <div className={'flex justify-between items-center p-2 border-b-2'}>
                    <div className={'flex justify-center items-center'}>
                        <MdShoppingCart className={'text-xl mr-1'}/>
                        <Typography className={'font-bold text-xl'}>
                            Cart
                        </Typography>
                    </div>
                    <div className={'flex justify-center items-center'}>
                        <span className={'mr-2 text-emerald-500 text-xl'}>0</span>
                        <Typography className={'text-xl font-bold'}>Products</Typography>
                    </div>
                </div>
                <div className={'h-[28rem] max-h-[28rem] p-2 overflow-y-auto my-2'}>
                    {
                        cart.map((item,index) => {
                            return (
                                <CardCart key={index} Image={"https://images.unsplash.com/photo-1522202176988-66273c2fd55f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1471&q=80"}
                                        Price={item.price} Color={item.color} Name={item.name} Quantity={item.quantity} Size={item.size}
                                />
                            )
                        })
                    }
                </div>
                <div className={'flex justify-between items-center border-t-2 p-2'}>
                    <Typography className={'text-sm font-semibold'}>Total Price</Typography>
                    <span className={'text-sm font-semibold'}>111000$</span>
                </div>
                <div className={'my-2 flex justify-center items-center w-full'}>
                    <Button className={'w-full mx-2 bg-emerald-400 border-none font-bold'}>Check Out</Button>
                </div>
                <div className={'my-2 flex justify-center items-center w-full'}>
                    <Button className={'w-full mx-2 bg-emerald-400 border-none font-bold' }>Cart Detail</Button>
                </div>
                <Tooltip.Arrow className={'border-white'}/>
            </Tooltip.Content>
        </Tooltip>
    );
};

export default PopoverCart;