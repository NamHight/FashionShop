import Home from "../pages/Home/Index";
import VerifyPassword from "../pages/VerifyPassword";
import EmailConfirm from "../pages/VerifyPassword/EmailConfirm";
import EmailConfirmError from "../pages/VerifyPassword/EmailConfirmError";
import Contact from "../pages/Contact/index";
import Orders from "../pages/Account/Orders";
import Profile from "../pages/Account/Profile";
import ListFavorites from "./../pages/Account/ListFavorites/index";
import Cart from "../pages/Cart/Inndex";
import Payment from "./../pages/Payment/Index";
import About from "./../pages/About/Index";
import Blog from "../pages/Blog/index";
import BlogArticle from "../pages/Blog/Article/index";
import DetailProduct from "../pages/Product/detailProduct";

export const Router = [
  { path: "/", name: "Home", element: <Home /> },
  { path: "/about", name: "About", element: <About /> },
  { path: "/contact", name: "Contact", element: <Contact /> },
  { path: "/blog", name: "Blog", element: <Blog /> },
  { path: "/blog/article", name: "BlogArticle", element: <BlogArticle /> },
  {
    path: "/verify-password",
    name: "VerifyPassword",
    element: <VerifyPassword />,
  },
  {
    path: "email-confirmation",
    name: "EmailConfirm",
    element: <EmailConfirm />,
  },
  {
    path: "email-confirmation-error",
    name: "EmailConfirmError",
    element: <EmailConfirmError />,
  },
  { path: "/cart", name: "Cart", element: <Cart /> },
  { path: "/payment", name: "Payment", element: <Payment /> },
  { path: "/detailProduct", name: "detailProduct", element: <DetailProduct /> },
];
export const routerAccount = [
  { path: "", name: "Profile", element: <Profile /> },
  { path: "orders", name: "Orders", element: <Orders /> },
  { path: "listfavorite", name: "List Favorite", element: <ListFavorites /> },
];
