import React from 'react'
import { Button } from "@material-tailwind/react";
import { useCartConText } from '../../context/CartContext';


const ButtonAddCart = ({css, productId, productName, banner, price, quantity =1}) => {
    const {addCart} = useCartConText();
  return (
     <Button className= {css}
        onClick={() => addCart({productId : productId, productName : productName,banner: banner, price: price, quantity: quantity})}
     >  Add To Cart
     </Button>
  )
}

export default ButtonAddCart
