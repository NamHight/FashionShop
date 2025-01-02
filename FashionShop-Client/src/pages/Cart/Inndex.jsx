import React, { useEffect, useRef, useState } from 'react'
import { Button, Card, Typography } from "@material-tailwind/react";
import { Link } from 'react-router';
import {Pagination } from '../../components/Pagination/Pagination.jsx';
import { useCartConText } from '../../context/CartContext.jsx';

function Cart() {
    const {cart, decreaseQuantity, increaseQuantity, totalMoney, removeCart, removeAllCart} = useCartConText();
    const TABLE_HEAD = ["Product", "Name", "Price", "Quantity", "Amount", "Handle"];
    const TABLE_ROWS = cart;
 
    
  
  return (
    <div class="mt-20 mx-72">
        <div className="flex justify-center w-full align-center">
           <div className=" flex justify-start w-full">
            <Button  variant="gradient">
                    <Link to="/">Home</Link>
            </Button>
           </div>
            <div className=" flex justify-start w-full">
                <h2 className="text-center text-blue-900 ">Trang Gio Hang</h2>
            </div>
        </div>
        <Card className="h-full w-full overflow-scroll mt-10">
      <table className="w-full min-w-max table-auto text-left">
        <thead>
          <tr >
            {TABLE_HEAD.map((head) => (
              <th
                key={head}
                className="border-b border-blue-gray-100 bg-blue-gray-50 p-4"
              >
                <Typography
                  variant="small"
                  color="blue-gray"
                  className=" leading-none opacity-70 font-bold"
                >
                  {head}
                </Typography>
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {TABLE_ROWS.map((item, index) => {
            const isLast = index === TABLE_ROWS.length - 1;
            const classes = isLast ? "p-4" : "p-4 border-b border-blue-gray-50";
            return (
              <tr key={index} className="relative">
                <td className="max-w-[8rem] p-3">
                    <img
                        className="max-w-64 rounded-xl object-cover object-center"
                        src={`assets/${item.Banner}`}
                        alt="product image"
                    />
                </td>
                <td className={classes} style={{ maxWidth: '110px', wordWrap: 'break-word', whiteSpace: 'normal' }}>
                  <Typography
                    variant="small"
                    color="blue-gray"
                    className="font-normal"
                  >
                    {item.ProductName} {item.ProductId}
                  </Typography>
                </td>
                <td className={classes}>
                  <Typography
                    variant="small"
                    color="blue-gray"
                    className="font-normal"
                  >
                    {item.Price}
                  </Typography>
                </td>
                <td className="max-w-[8rem] p-3 flex justify-between items-center align-middle h-full absolute">
                    <div className="me-2">
                      <Typography
                        variant="small"
                        color="blue-gray"
                        className="font-normal"
                      >
                        {item.Quantity}
                      </Typography>
                    </div>
                    <button className="bg-gray-200 px-4 py-2 rounded-md me-2"
                        onClick={() => decreaseQuantity(item.ProductId)}
                    >
                        -
                    </button>
                    <button
                        className="bg-gray-200 px-4 py-2 rounded-md"
                        onClick={() => increaseQuantity(item.ProductId)}
                    >
                        +
                    </button>
                </td>
                <td className={classes}>
                  <Typography
                    variant="small"
                    color="blue-gray"
                    className="font-normal"
                  >
                    {item.Amount}
                  </Typography>
                </td>
                <td className={classes}>
                  <Typography
                    as="a"
                    href="#"
                    variant="small"
                    color="blue-gray"
                    className="font-medium"
                  >
                    <button onClick={() => removeCart(item.ProductId)}>Delete</button>
                  </Typography>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
        </Card>
        <div className="flex my-20 justify-around">
                <div> 
                    <Typography
                      variant="small"
                      color="blue-gray"
                      className="font-bold"
                    >
                      Tong Tien Thanh Toan: {totalMoney()}$
                    </Typography>
                </div>
                <Button class="rounded-md bg-slate-800 py-2 px-4 border border-transparent text-center text-sm text-white transition-all shadow-md hover:shadow-lg focus:bg-slate-700 focus:shadow-none active:bg-slate-700 hover:bg-slate-700 active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none ml-2" type="button">
                    Thanh Toan
                </Button>
        </div>
    </div>
  )
}

export default Cart



// rfce