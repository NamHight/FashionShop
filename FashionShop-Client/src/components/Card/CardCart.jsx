import React from 'react';
import {Button, Card, Typography} from "@material-tailwind/react";
import { MdDeleteForever } from "react-icons/md";

const CardCart = ({Image, Name,Price,Quantity,Color,Size}) => {
    return (
        <Card className="flex h-[9rem] w-full max-w-[48rem] flex-row mb-1">
            <Card.Header className="m-0 h-full w-2/5 shrink-0 rounded-r-none">
                <img
                    src={Image}
                    alt="card-image"
                    className="h-full w-full object-cover"
                />
            </Card.Header>
            <Card.Body className="p-4 h-full w-full">
                <Typography className="mb-1 text-[.8rem] font-semibold uppercase text-foreground">
                    {Name}
                </Typography>
                <div className={'flex flex-col justify-center items-start gap-2 mt-2'}>
                    <div className={'grid grid-cols-2 items-center w-full'}>
                        <Typography className="text-[1rem] font-semibold float-end">
                            Price
                        </Typography>
                        <span className={'ml-1 font-semibold' }>{Price}$</span>
                    </div>
                    <div className={'grid grid-cols-2 items-center w-full'}>
                        <Typography className="text-[1rem] font-semibold float-end">
                            Quantity
                        </Typography>
                        <span className={'ml-1 font-semibold'}>{Quantity}</span>
                    </div>
                    <div className={'grid grid-cols-2 items-center w-full'}>
                        <Typography className="text-[1rem] font-semibold float-end">
                            Color
                        </Typography>
                        <span className={'ml-1 font-semibold'}>{Color}</span>
                    </div>
                    <div className={'grid grid-cols-2 items-center w-full'}>
                        <Typography className="text-[1rem] font-semibold float-end">
                            Size
                        </Typography>
                        <span className={'ml-1 font-semibold'}>{Size}</span>
                    </div>
                </div>
                <div className={'relative'}>
                    <Button className={'absolute bottom-[-.4rem] px-2 right-[-.5rem] bg-white border-none text-red-700 hover:bg-red-500 hover:text-white text-xl'}><MdDeleteForever/></Button>
                </div>
            </Card.Body>
        </Card>
    );
};

export default CardCart;