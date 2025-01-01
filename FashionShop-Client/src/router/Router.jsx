import Home from "../pages/Home/Index";
import About from "../pages/About/Index";
import Cart from "../pages/Cart/Index";

export const Router = [
    {path: "/", name: "Home",element: <Home/>},
    {path: "/about", name: "About", element: <About/>},
    {path: "/cart", name: "Cart", element: <Cart/>}
];