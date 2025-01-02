import {createContext, useContext, useState} from "react";
import { getAllCarts, addCart, removeCarts, removeAllCarts, updateCarts } from "../services/api/CartService";
import { jwtDecode } from "jwt-decode";
import { tokenProtection } from "../services/api/TokenService";
import { useQuery } from '@tanstack/react-query';

export const CartContext = createContext(null);

export const CartContextProvider = ({ children }) => {

    const { data: cartQuery, error, isLoading } = useQuery({
        queryKey:['cart'], // Query key
        queryFn:async()=>
        {
           const allCarts = await getAllCarts();
           return allCarts;
        },
    });

    const [cart, setCart] = useState([
        {ProductId: 1, Banner: "test-01.jpg", ProductName: "May giat", Price: 33, Quantity: 3, Amount: 99 }, 
        {ProductId: 2, Banner: "test-01.jpg", ProductName: "Dieu hoa", Price: 25, Quantity: 2, Amount: 50 },
        {ProductId: 3, Banner: "test-01.jpg", ProductName: "May anh", Price: 88, Quantity: 1, Amount: 88 }]);

    const increaseQuantity = (id) =>{
        console.log("ID la", id);
        var currentCart = cart.find(item=> item.Quantity == id);
        if(currentCart){
            var newCart = [...cart].map(item => {
                if(item.ProductId==id){
                    return {...item, Quantity : item.Quantity +1, Amount: (item.Amount + item.Price)};
                }
                return item
            })
            console.log(newCart);
            setCart(newCart);
        }
    }

    const decreaseQuantity = (id) =>{
        var currentCart = cart.find(item=> item.ProductId === id);
        if(currentCart){
            if(currentCart.Quantity === 1){
                removeCart(id);
            }else{
                var newCart = [...cart].map(item => {
                    if(item.ProductId==id){
                        return {...item, Quantity : item.Quantity- 1, Amount: item.Amount - item.Price};
                    }
                    return item;
                    
                })
                setCart(newCart);
            }
        }
    }

    const addCart = (productId, banner, productName, price, quantity, amount) =>{
        var product = cart.find(item => item.ProductId == productId);
        if(product){
            var cartNew = [...cart].map(item =>{
                if(item.ProductId == productId) return {...item, Quantity: item.Quantity + quantity, Amount: item.Amount + item.Price*quantity};
                return item;
            })
            setCart(cartNew);
        }else{
            var cartNew = [...cart, {ProductId: productId, Banner: banner, ProductName: productName, Price: price, Quantity: quantity, Amount: amount}]
            setCart(cartNew);
        }
    }

    const removeCart = (id) =>{
        var newCart = cart.filter(item => item.ProductId !=id)
        console.log("danh sach sau khi xoa: ", newCart);
        setCart(newCart);
    }

    const removeAllCart = () =>{
        setCart([]);
    }

    const totalMoney = () =>{
        var total = cart.reduce((total, item)=> total + item.Amount, 0);
        return total;
    }

    const value = {
        cart: cart,
        isLoading: isLoading,
        error: error,
        increaseQuantity: increaseQuantity,
        decreaseQuantity: decreaseQuantity,
        addCart: addCart,
        removeCart: removeCart,
        removeAllCart: removeAllCart,
        totalMoney: totalMoney
    }

    return (
        <CartContext.Provider value={value}>
            {children}
        </CartContext.Provider>
    );
};

export const useCartConText = () => {
    const context = useContext(CartContext);
    return context;
};