import Home from "../pages/Home/Index";
import VerifyPassword from "../pages/VerifyPassword";
import EmailConfirm from "../pages/VerifyPassword/EmailConfirm";
import EmailConfirmError from "../pages/VerifyPassword/EmailConfirmError";
import Contact from "../pages/Contact/index";
import Orders from "../pages/Account/Orders";
import Profile from "../pages/Account/Profile";
import ListFavorites from './../pages/Account/ListFavorites/index';
import Cart from "../pages/Cart/Inndex";
import Payment from "./../pages/Payment/Index";
import About from './../pages/About/Index';
import Blog from "../pages/Blog/Promotions/index";
import BlogArticle from "../pages/Blog/Article/index";
import DetailProduct from "../pages/Product/detailProduct";
import ForgotPassword from "../pages/VerifyPassword/ForgotPassword";
import ResetPassword from "../pages/VerifyPassword/ResetPassword";
import Page404 from "../pages/Page404/Index";
import Test from "../pages/Test/Index";


export const Router = [
    {path: "/", name: "Home",element: <Home/>},
    {path: "/about", name: "About", element: <About/>},
    {path: "/contact", name: "Contact", element: <Contact />},
    {path: "/blog", name: "Blog", element : <Blog />},
    {path: "/blog/article", name: "BlogArticle",element: <BlogArticle />},
    {path: "/verify-password", name: "VerifyPassword", element: <VerifyPassword/>},
    {path: "/email-confirmation", name: "EmailConfirm", element: <EmailConfirm/>},
    {path: "/forgot-password", name: "ForgotPassword", element: <ForgotPassword/>},
    {path: "/email-confirmation-error", name: "EmailConfirmError", element: <EmailConfirmError/>},
    {path: "/reset-password",name: "ResetPassword", element: <ResetPassword/>},
    {path: "/cart", name: "Cart", element: <Cart/>},
    {path: "/payment", name: "Payment", element: <Payment/>},
    {path: "/:categorySlug/:productSlug", name: "detailProduct", element: <DetailProduct/>},
    {path: "/page404/:param1", name:"Page404", element: <Page404/>},
    {path: "/test", name:"Test", element: <Test/>}
];
export const routerAccount = [
    {path: "", name: "Profile",element: <Profile/>},
    {path: "orders", name: "Orders",element: <Orders/>},
    {path: "listfavorite", name: "List Favorite",element: <ListFavorites/>},
];