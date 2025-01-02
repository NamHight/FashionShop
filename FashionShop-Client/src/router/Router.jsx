import Home from "../pages/Home/Index";
import About from "../pages/About/Index";
import VerifyPassword from "../pages/VerifyPassword";
import EmailConfirm from "../pages/VerifyPassword/EmailConfirm";
import EmailConfirmError from "../pages/VerifyPassword/EmailConfirmError";
import Cart from "../pages/Cart/Inndex";


export const Router = [
    {path: "/", name: "Home",element: <Home/>},
    {path: "/about", name: "About", element: <About/>},
    {path: "/cart", name: "Cart", element: <Cart/>}
];