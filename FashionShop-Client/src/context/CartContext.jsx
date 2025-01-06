import {createContext, useContext, useEffect, useState} from "react";
import { getAllCartsService, addCartService, removeCartsService, saveCartsService, getPaginationAllCartsService } from "../services/api/CartService";
import { jwtDecode } from "jwt-decode";
import { tokenProtection } from "../services/api/TokenService";
import { useQuery, useSuspenseInfiniteQuery } from '@tanstack/react-query';
import { set } from "zod";

export const CartContext = createContext(null);

// Trong phần này sẽ tích hợp phân trang, dữ liệu phân trang sẽ được dựa vào useState cart (lưu trữ toàn bộ giỏ hàng).
// Nếu cart hoặc page thay đổi thì sẽ cập nhật lại cartPagination (chứa cart đã phân trang). 
// Nếu có bất cứ thay đổi nào như thêm, xóa cart, xóa toàn bộ sẽ cập nhật cart sau đó gọi api cập nhật lên server

export const CartContextProvider = ({ children }) => {

    const [page, setPage] = useState(1); // Lưu trang hiện tại mà người dùng đang trỏ tới

    const { data: cartQuery, refetch, error, isLoading } = useQuery({
        queryKey:['cart'], 
        queryFn: async () => { 
            const allCarts = await getAllCartsService();
            if(allCarts != null){
                console.log("Dữ liệu cart sau khi get API", allCarts);
                return allCarts;
            }
            console.log("du lieu null");
            return null;
        },
        refetchOnWindowFocus: false, // Không fetch lại khi focus tab
        staleTime: 1000 * 60 * 5, // Dữ liệu sẽ không bị xem là "cũ" trong 5 phút
        cacheTime: 1000 * 60 * 10, // Dữ liệu sẽ được giữ trong cache trong 10 phút
        enabled: true // Không fetch tự động
    });

    const [cart, setCart] = useState(cartQuery);
    const [cartPagination, setCartPagination] = useState(() =>{
        if(cart!=null){
           var x = cart.slice((page-1)*3, (page-1)*3 +3)
           console.log("du lieu khai bao cho cartPagination: ", x);
           return x;
        }
        return null;
    }) // Khởi tạo giá trị ban đầu cho cartPagination bằng 3 phần tử đầu tiên trong cart

    useEffect(()=>{
        if(cart!=null){
            setCartPagination(() => {
                const x = cart.slice((page-1)*3, (page-1)*3 +3)
                console.log("du lieu trong cartPagination:", x);
                return x;
            });
        }
    }, [page, cart]) // khi cart hoặc page thay đổi thì cập nhật lại cartPagination để hiển thị cho đúng phân trang

    useEffect(() => {  
        if (cartQuery!=null) {
            setCart(cartQuery);
        }
    }, [cartQuery]);  

    useEffect(()=>{
        localStorage.setItem("cart", JSON.stringify(cart))
    }, [cart]) // khi cart thay đổi thì lưu xuống localStorage

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

    const addCart = (data) =>{ // {productId, productName, banner, price, quantity}
        var product = cart.find(item => item.productId === data.productId);
        if(product){ // đã tồn tại trong giỏ hàng
            var cartNew = [...cart].map(item =>{
                if(item.productId === data.productId) return {...item, quantity: item.quantity + data.quantity, amount: item.amount + item.price*data.quantity};
                return item;
            })
            addCartService(data.productId, data.quantity);
            setCart(cartNew);
        }else{ // chưa tồn tại trong giỏ hàng
            var cartNew = [...cart, {productId: data.productId, banner: data.banner, productName: data.productName, price: data.price, quantity: data.quantity, amount: Number(data.price)*Number(data.quantity)}]
            addCartService(data.productId, data.quantity);
            setCart(cartNew);
        }
    }

    const removeCart = (id) =>{
        var newCart = cart.filter(item => item.productId !=id)
        console.log("danh sach sau khi xoa: ", newCart);
        removeCartsService(id);
        setCart(newCart);
    }

    const removeAllCart = () =>{
        setCart([]);
    }

    const totalMoney = () =>{
        if(cart) {
            var total = cart.reduce((total, item)=> total + item.amount, 0);
            return total;
        }
        return 0;
    }

    const  saveCart = () =>{
        saveCartsService(cart)
    }

    const totalCarts = () =>{
        if(cart!=null){
            return cart.length
        }
        return 0;
    }

    const totalPages = () =>{
        if(cart!= null){
            const data = cart.length;
            const totalPages = data % 3 >0 ? Math.ceil(data /3) : data /3;
            return totalPages ;
        }
       return 0;
    }

    const value = {
        cart: cartPagination,
        isLoading: isLoading,
        error: error,
        page: page,
        totalPages:totalPages(),
        increaseQuantity: increaseQuantity,
        decreaseQuantity: decreaseQuantity,
        addCart: addCart,
        removeCart: removeCart,
        removeAllCart: removeAllCart,
        totalMoney: totalMoney,
        saveCart: saveCart,
        setPage: setPage
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