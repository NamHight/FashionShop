import {createContext, useContext, useEffect, useState} from "react";
import { getAllCartsService, addCartService, removeCartsService, removeAllCartsService, updateCartsService } from "../services/api/CartService";
import { jwtDecode } from "jwt-decode";
import { tokenProtection } from "../services/api/TokenService";
import { useQuery } from '@tanstack/react-query';

export const CartContext = createContext(null);


export const CartContextProvider = ({ children }) => {

    const { data: cartQuery, refetch, error, isLoading } = useQuery({
        queryKey:['cart'], 
        queryFn: async () => { 
            const allCarts = await getAllCartsService();
            console.log("Dữ liệu cart sau khi get API", allCarts);
            return allCarts;
        },
        refetchOnWindowFocus: false, // Không fetch lại khi focus tab
        staleTime: 1000 * 60 * 5, // Dữ liệu sẽ không bị xem là "cũ" trong 5 phút
        cacheTime: 1000 * 60 * 10, // Dữ liệu sẽ được giữ trong cache trong 10 phút
        // enabled: false // Không fetch tự động
    });

    const data = [
    {productId: 1, banner: "test-01.jpg", productName: "May giat", price: 33, quantity: 3, amount: 99 }, 
    {productId: 2, banner: "test-01.jpg", productName: "Dieu hoa", price: 25, quantity: 2, amount: 50 },
    {productId: 3, banner: "test-01.jpg", productName: "May anh", price: 88, quantity: 1, amount: 88 }]

    const [cart, setCart] = useState(() => cartQuery? cartQuery : data);

    useEffect(() => {
        if (cartQuery) {
          setCart(cartQuery); // Cập nhật cart khi cartQuery có dữ liệu
        }
      }, [cartQuery]); // Chạy mỗi khi cartQuery thay đổi

    const increaseQuantity = (id) =>{
        console.log("ID la", id, "danh sach cart ",cart);
        var currentCart = cart.find(item=> item.productId === id);
        console.log("Phan tu muon tang so luong", currentCart);
        if(currentCart){
            var newCart = [...cart].map(item => {
                if(item.productId === id){
                    return {...item, quantity : item.quantity +1, amount: (item.amount + item.price)};
                }
                return item
            })
            console.log(newCart);
            setCart(newCart);
        }else{
            console.log("tang so luong that bai");
        }
    }

    const decreaseQuantity = (id) =>{
        var currentCart = cart.find(item=> item.productId === id);
        if(currentCart){
            if(currentCart.quantity === 1){
                removeCart(id);
            }else{
                var newCart = [...cart].map(item => {
                    if(item.productId===id){
                        return {...item, quantity : item.quantity- 1, amount: item.amount - item.price};
                    }
                    return item;
                    
                })
                setCart(newCart);
            }
        }
    }

    const addCart = (productId, banner, productName, price, quantity) =>{
        var product = cart.find(item => item.productId === productId);
        if(product){ // đã tồn tại trong giỏ hàng
            var cartNew = [...cart].map(item =>{
                if(item.productId === productId) return {...item, quantity: item.quantity + quantity, amount: item.amount + item.price*quantity};
                return item;
            })
            addCartService(productId, quantity);
            setCart(cartNew);
        }else{ // chưa tồn tại trong giỏ hàng
            var cartNew = [...cart, {productId: productId, banner: banner, productName: productName, price: price, quantity: quantity, amount: Number(price)*Number(quantity)}]
            addCartService(productId, quantity);
            setCart(cartNew);
        }
    }

    const removeCart = (id) =>{
        var newCart = cart.filter(item => item.productId !=id)
        console.log("danh sach sau khi xoa: ", newCart);
        setCart(newCart);
    }

    const removeAllCart = () =>{
        setCart([]);
    }

    const totalMoney = () =>{
        var total = cart.reduce((total, item)=> total + item.amount, 0);
        return total;
    }

    // const  saveCart = () =>{
    //     setCart([]);
    // }

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