import React, { useEffect, useRef, useState } from 'react'
import { Button, Card, Typography } from "@material-tailwind/react";
import { Link } from 'react-router';

function Cart() {
    const TABLE_HEAD = ["Product", "Name", "Price", "Quantity", "Amount", "Handle"];
    const TABLE_ROWS = [
        {ProductId: 1, Banner: "test-01.jpg", ProductName: "May giat", Price: 33, Quantity: 3, Amount: 99 }, 
        {ProductId: 2, Banner: "test-01.jpg", ProductName: "Dieu hoa", Price: 25, Quantity: 2, Amount: 50 },
        {ProductId: 3, Banner: "test-01.jpg", ProductName: "May anh", Price: 88, Quantity: 1, Amount: 88 },
    ];
    const [data, setData] = useState(TABLE_ROWS);
    const [total, setTotal] = useState(0)
    const [isDelete, setIsDelete] = useState(false);
    const [isChangeQuantity, setIsChangeQuantity] = useState(false);

    const increaseQuantity = (id) => {
        console.log(id)
        data.map(item => item.ProductId ==id ? {...item, Quantity: item.Quantity+1} : item)
        setIsChangeQuantity(!isChangeQuantity);
    };
    
      // Hàm giảm giá trị
    const decreaseQuantity = (id) => {
        console.log(id)
        data.map(item => item.ProductId ==id ? {...item, Quantity: (item.Quantity > 1 ? item.Quantity-1 : 0)} : item)
        setIsChangeQuantity(!isChangeQuantity);
    };

    useEffect(()=>{
        setTotal(()=>{
           return  data.reduce((sum, {Amount})=> sum + Number(Amount), 0)
        })
    }, [isDelete, isChangeQuantity]);
   
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
          {data.map(({ ProductId, Banner, ProductName,Price, Quantity, Amount}, index) => {
            const isLast = index === TABLE_ROWS.length - 1;
            const classes = isLast ? "p-4" : "p-4 border-b border-blue-gray-50";
            return (
              <tr key={index}>
                <td className="max-w-[8rem] p-3">
                    <img
                        className="max-w-64 rounded-xl object-cover object-center"
                        src={`assets/${Banner}`}
                        alt="product image"
                    />
                </td>
                <td className={classes} style={{ maxWidth: '110px', wordWrap: 'break-word', whiteSpace: 'normal' }}>
                  <Typography
                    variant="small"
                    color="blue-gray"
                    className="font-normal"
                  >
                    {ProductName}
                  </Typography>
                </td>
                <td className={classes}>
                  <Typography
                    variant="small"
                    color="blue-gray"
                    className="font-normal"
                  >
                    {Price}
                  </Typography>
                </td>
                <td className="max-w-[8rem] p-3">
                    <button className="bg-gray-200 px-4 py-2 rounded-md"
                        onClick={() => decreaseQuantity(ProductId)}
                    >
                        -
                    </button>
                    <input
                        type="number"
                        inputMode="numeric"
                        className="bg-transparent text-center text-slate-700 text-sm border border-slate-200 rounded-md py-2 transition duration-300 ease focus:outline-none focus:border-slate-400 hover:border-slate-300 shadow-sm focus:shadow"
                        maxLength="5"
                        value={Quantity}
                        id="zipInput"
                    />
                    <button
                        className="bg-gray-200 px-4 py-2 rounded-md"
                        onClick={() => increaseQuantity(ProductId)}
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
                    {Amount}
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
                    Delete
                  </Typography>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
        </Card>

        <div className="flex my-20 justify-around">
                <div>Tong Tien Thanh Toan: {total}$</div>
                <Button class="rounded-md bg-slate-800 py-2 px-4 border border-transparent text-center text-sm text-white transition-all shadow-md hover:shadow-lg focus:bg-slate-700 focus:shadow-none active:bg-slate-700 hover:bg-slate-700 active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none ml-2" type="button">
                    Thanh Toan
                </Button>
        </div>
    </div>
  )
}

export default Cart



// rfce