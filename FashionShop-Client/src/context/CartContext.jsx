import {createContext, useContext, useEffect, useState} from "react";
import { getAllCartsService, addCartService, removeCartsService, saveCartsService, removeAllCartsService } from "../services/api/CartService";
import { jwtDecode } from "jwt-decode";
import { tokenProtection } from "../services/api/TokenService";
import { useQuery, useSuspenseInfiniteQuery } from '@tanstack/react-query';
import { set } from "zod";

export const CartContext = createContext(null);

// Trong phần này sẽ tích hợp phân trang, dữ liệu phân trang sẽ được dựa vào useState cart (lưu trữ toàn bộ giỏ hàng).
// Nếu cart hoặc page thay đổi thì sẽ cập nhật lại cartPagination (chứa cart đã phân trang). 
// Nếu có bất cứ thay đổi nào như thêm, xóa cart, xóa toàn bộ sẽ cập nhật cart sau đó gọi api cập nhật lên server

export const CartContextProvider = ({ children }) => {

    var paypalClientIDx = "";

    const [page, setPage] = useState(1); // Lưu trang hiện tại mà người dùng đang trỏ tới

    const { data: cartQuery, refetch, error, isLoading } = useQuery({
        queryKey:['cart'], 
        queryFn: async () => { 
            const allCarts = await getAllCartsService();
            if(allCarts != null){
                paypalClientIDx = allCarts.paypalClientID
                console.log("Dữ liệu cart sau khi get API", allCarts.carts);
                return allCarts.carts;
            }
            console.log("du lieu null");
            return null;
        },
        refetchOnWindowFocus: false, // Không fetch lại khi focus tab
        staleTime: 1000 * 60 * 5, // Dữ liệu sẽ không bị xem là "cũ" trong 5 phút
        cacheTime: 1000 * 60 * 10, // Dữ liệu sẽ được giữ trong cache trong 10 phút
        enabled: true // Không fetch tự động
    });

    const [carts, setCarts] = useState(cartQuery); // danh sách cart lưu tại client
    const [cartPagination, setCartPagination] = useState(() =>{
        if(carts!=null){
           var x = carts.slice((page-1)*3, (page-1)*3 +3)
           console.log("du lieu khai bao cho cartPagination: ", x);
           return x;
        }
        return null;
    }) // Khởi tạo giá trị ban đầu cho cartPagination bằng 3 phần tử đầu tiên trong cart

    useEffect(()=>{
        if(carts!=null){
            setCartPagination(() => {
                const x = carts.slice((page-1)*3, (page-1)*3 +3)
                console.log("du lieu trong cartPagination:", x);
                return x;
            });
        }
    }, [page, carts]) // khi cart hoặc page thay đổi thì cập nhật lại cartPagination để hiển thị cho đúng phân trang

    useEffect(() => {  
        if (cartQuery!=null) {
            setCarts(cartQuery);
        }
    }, [cartQuery]);  

    useEffect(()=>{
        localStorage.setItem("cart", JSON.stringify(carts))
    }, [carts]) // khi cart thay đổi thì lưu xuống localStorage

    const increaseQuantity = (id) =>{
        console.log("ID la", id, "danh sach cart ",carts);
        var currentCart = carts.find(item=> item.productId === id);
        console.log("Phan tu muon tang so luong", currentCart);
        if(currentCart){
            var newCart = [...carts].map(item => {
                if(item.productId === id){
                    return {...item, quantity : item.quantity +1, amount: (item.amount + item.price)};
                }
                return item
            })
            console.log(newCart);
            setCarts(newCart);
        }else{
            console.log("tang so luong that bai");
        }
    }

    const decreaseQuantity = (id) =>{
        var currentCart = carts.find(item=> item.productId === id);
        if(currentCart){
            var newCart = [...carts].map(item => {
                if(item.productId===id){
                    return {...item, quantity : item.quantity- 1, amount: item.amount - item.price};
                }
                return item;
            })
            setCarts(newCart);
        }
    }

    const addCart = (data) =>{ // {productId, productName, banner, price, quantity}
        if(carts === null){ // nếu giỏ hàng trống thì thêm phần từ vào cuối mảng = đầu mảng
            carts.push({productId: data.productId, banner: data.banner, productName: data.productName, price: data.price, quantity: data.quantity, amount: Number(data.price)*Number(data.quantity)});
            var cartNew = [...carts];
            addCartService(data.productId, data.quantity);
            setCarts(cartNew);
        }else{ // nếu giỏ hàng không trống
            var product = carts.find(item => item.productId === data.productId);
            if(product){ // phần tử đã thêm đã tồn tại trong giỏ hàng
                var cartNew = [...carts].map(item =>{
                    if(item.productId === data.productId) return {...item, quantity: item.quantity + data.quantity, amount: item.amount + item.price*data.quantity};
                    return item;
                })
                addCartService(data.productId, data.quantity);
                setCarts(cartNew);
            }else{ // phần tử đã thêm chưa tồn tại trong giỏ hàng
                var cartNew = [...carts, {productId: data.productId, banner: data.banner, productName: data.productName, price: data.price, quantity: data.quantity, amount: Number(data.price)*Number(data.quantity)}]
                addCartService(data.productId, data.quantity);
                setCarts(cartNew);
            }
        }
    }

    const removeCart = (id) =>{
        var newCart = carts.filter(item => item.productId !=id)
        console.log("danh sach sau khi xoa: ", newCart);
        removeCartsService(id);
        setCarts(newCart);
    }

    const removeAllCart = async() =>{
        setCarts([]);
        await removeAllCartsService();
    }

    const totalMoney = () =>{
        if(carts) {
            var total = carts.reduce((total, item)=> total + item.amount, 0);
            return total;
        }
        return 0;
    }

    const  saveCart = () =>{
        saveCartsService(carts)
    }

    const totalCarts = () =>{
        if(carts!=null){
            return carts.length
        }
        return 0;
    }

    const totalPages = () =>{
        if(carts!= null){
            const data = carts.length;
            const totalPages = data % 3 >0 ? Math.ceil(data /3) : data /3;
            return totalPages ;
        }
       return 1;
    }

    const value = {
        carts: carts,
        cart: cartPagination,
        isLoading: isLoading,
        error: error,
        page: page,
        totalPages:totalPages(),
        totalCarts: totalCarts(),
        increaseQuantity: increaseQuantity,
        decreaseQuantity: decreaseQuantity,
        addCart: addCart,
        removeCart: removeCart,
        removeAllCart: removeAllCart,
        totalMoney: totalMoney,
        saveCart: saveCart,
        setPage: setPage,
        paypalClientIDx : paypalClientIDx
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