import React, { useEffect, useRef, useState } from 'react'
import { Button, Card, Typography } from "@material-tailwind/react";
import { Link } from 'react-router';
import {Pagination } from '../../components/Pagination/Pagination.jsx';
import { useCartConText } from '../../context/CartContext.jsx';
import {useAuth} from '../../context/AuthContext.jsx';
import {toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function Cart() {
    const {cart, decreaseQuantity, increaseQuantity, totalMoney, removeCart, removeAllCart, addCart, saveCart, totalCarts, totalPages} = useCartConText();
    const {user} = useAuth();
    const TABLE_HEAD = ["Product", "Name", "Price", "Quantity", "Amount", "Handle"];
    const TABLE_ROWS = cart;

    const CustomToast = ({ closeToast, id}) => (
      <div>
        <p>If you agree, the product in the cart will be removed. Do you agree ?</p>
        <div className='flex items-center justify-end space-x-3 mt-3'>
          <button onClick={() => handleOk(closeToast, id)} style={{ marginRight: '10px' }} className=''>OK</button>
          <button onClick={() => handleCancel(closeToast)} className=''>Cancel</button>
        </div>
      </div>
    );

    const handleOk = (closeToast, id) => {
      closeToast(); // Đóng toast
      removeCart(id); // Xoá sản phẩm khỏi giỏ hàng khi người dùng đã confirm OK
    };

    const handleCancel = (closeToast) => {
      closeToast(); // Đóng toast, và k làm gì cả vì người dùng không muốn xoá sản phẩm
    };
    
    const handleCheckQuantity =(id, quantity) =>{
      // Neu quantity = 1 thi hien thi thong bao ban co muon tiep tuc => neu chon ok thi bam xoa san pham : k lam gi
      if(quantity===1){
        toast(<CustomToast id={id} />);
      }else{
        decreaseQuantity(id);
      }
    }
  
  return (
    <div className="mt-10 mx-72 min-h-[700px] w-full">
        <div className="">
           <div className=" flex justify-start w-full">
            <Button  variant="gradient" className="me-3">
                    <Link to="/">Home</Link>
            </Button>
            <Button  variant="gradient" onClick={saveCart}>
                Save Cart
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
                            src={`assets/images/products/${item.banner}`}
                            alt={item.productName}
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
                            id = "quantity"
                          >
                            {item.quantity}
                          </Typography>
                        </div>
                        <button className="bg-gray-200 px-4 py-2 rounded-md me-2"
                            onClick={() => handleCheckQuantity(item.productId, item.quantity)}
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
              Total Payment Amount: {totalMoney()}$
            </Typography>
          </div>
          <div className="flex gap-4">
            {
              totalCarts > 0 ?  
              <Button
                className="flex items-center justify-center rounded-md bg-blue-600 py-3 px-6 text-white text-lg font-semibold transition hover:bg-blue-700 focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50"
                type="button"
                onClick={()=> saveCart()}
              >
                <Link  to="/payment"> Payment </Link>
              </Button>  :

              <Button
               className="flex items-center justify-center rounded-md bg-blue-600 py-3 px-6 text-white text-lg font-semibold transition hover:bg-blue-700 focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50"
               type="button"
              >
               <Link  to="/"> Please make a purchase </Link>
             </Button>
            }
            <Button
              className="flex items-center justify-center rounded-md bg-red-600 py-3 px-6 text-white text-lg font-semibold transition hover:bg-red-700 focus:ring-2 focus:ring-red-500 focus:ring-opacity-50"
              type="button"
              onClick={removeAllCart}
            >
              Delete All Cart
            </Button>
          </div>
        </div>
    </div>
  )
}

export default Cart



// rfce