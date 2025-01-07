import React, { useEffect, useRef, useState } from 'react'
import { Button, Card, Typography } from "@material-tailwind/react";
import { Link } from 'react-router';
import {Pagination } from '../../components/Pagination/Pagination.jsx';
import { useCartConText } from '../../context/CartContext.jsx';
import {useAuth} from '../../context/AuthContext.jsx';

function Cart() {
    const {cart, decreaseQuantity, increaseQuantity, totalMoney, removeCart, removeAllCart, addCart, saveCart, totalPages} = useCartConText();
    const {user} = useAuth();
    const TABLE_HEAD = ["Product", "Name", "Price", "Quantity", "Amount", "Handle"];
    const TABLE_ROWS = cart;
 
    
  
  return (
    <div className="mt-10 mx-72 min-h-[700px]">
        <div className="">
           <div className=" flex justify-start w-full">
            <Button  variant="gradient" className="me-3">
                    <Link to="/">Home</Link>
            </Button>
            <Button  variant="gradient" onClick={saveCart}>
                Lưu Giỏ Hàng
            </Button>
           </div>
           <div className="text-center w-full my-5">
              <h2 className="font-bold text-4xl text-transparent bg-clip-content bg-gradient-to-r from-blue-500 to-orange-500 shadow-lg rounded-md"
              style={{color: "#fbbf24"}}>
                My Shopping Cart
              </h2>
          </div>
        </div>
        <Card className="h-full w-full overflow-scroll mt-10 mb-5">
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
              {TABLE_ROWS && TABLE_ROWS.map((item, index) => {
                const isLast = index === TABLE_ROWS.length - 1;
                const classes = isLast ? "p-4" : "p-4 border-b border-blue-gray-50";
                return (
                  <tr key={index} className="relative">
                    <td className="max-w-[8rem] p-3">
                        <img
                            className="max-w-64 rounded-xl object-cover object-center"
                            src={`assets/${item.banner}`}
                            alt="product image"
                        />
                    </td>
                    <td className={classes} style={{ maxWidth: '110px', wordWrap: 'break-word', whiteSpace: 'normal' }}>
                      <Typography
                        variant="small"
                        color="blue-gray"
                        className="font-normal"
                      >
                        {item.productName} {item.productId}
                      </Typography>
                    </td>
                    <td className={classes}>
                      <Typography
                        variant="small"
                        color="blue-gray"
                        className="font-normal"
                      >
                        {item.price}
                      </Typography>
                    </td>
                    <td className="max-w-[8rem] p-3 flex justify-between items-center align-middle h-full absolute">
                        <div className="me-2">
                          <Typography
                            variant="small"
                            color="blue-gray"
                            className="font-normal"
                          >
                            {item.quantity}
                          </Typography>
                        </div>
                        <button className="bg-gray-200 px-4 py-2 rounded-md me-2"
                            onClick={() => decreaseQuantity(item.productId)}
                        >
                            -
                        </button>
                        <button
                            className="bg-gray-200 px-4 py-2 rounded-md"
                            onClick={() => increaseQuantity(item.productId)}
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
                        {item.amount}
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
                        <button onClick={() => removeCart(item.productId, item.quantity)}>Delete</button>
                      </Typography>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </Card>
        {TABLE_ROWS && <Pagination/>}
        <div className="flex my-20 justify-between items-center bg-orange-100 p-6 rounded-lg shadow-lg">
          <div>
            <Typography variant="h5" color="blue-gray" className="font-bold">
              Tổng Tiền Thanh Toán: {totalMoney()}$
            </Typography>
          </div>
          <div className="flex gap-4">
            <Button
              className="flex items-center justify-center rounded-md bg-blue-600 py-3 px-6 text-white text-lg font-semibold transition hover:bg-blue-700 focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50"
              type="button"
            >
              <Link to="/payment"> Thanh Toán</Link>
            </Button>
            <Button
              className="flex items-center justify-center rounded-md bg-green-600 py-3 px-6 text-white text-lg font-semibold transition hover:bg-green-700 focus:ring-2 focus:ring-green-500 focus:ring-opacity-50"
              type="button"
              onClick={() =>
                addCart({
                  productId: 155,
                  productName: "Product 4",
                  banner: "banner4.jpg",
                  price: 180.25,
                  quantity: 2,
                })
              }
            >
              Thêm Vào Giỏ
            </Button>
            <Button
              className="flex items-center justify-center rounded-md bg-red-600 py-3 px-6 text-white text-lg font-semibold transition hover:bg-red-700 focus:ring-2 focus:ring-red-500 focus:ring-opacity-50"
              type="button"
              onClick={removeAllCart}
            >
              Xóa Giỏ Hàng
            </Button>
          </div>
        </div>
    </div>
  )
}

export default Cart



// rfce