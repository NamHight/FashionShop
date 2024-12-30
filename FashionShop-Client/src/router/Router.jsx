import Home from "../pages/Home/Index";
import About from "../pages/About/Index";
import VerifyPassword from "../pages/VerifyPassword";
import EmailConfirm from "../pages/VerifyPassword/EmailConfirm";
import EmailConfirmError from "../pages/VerifyPassword/EmailConfirmError";

export const Router = [
    {path: "/", name: "Home",element: <Home/>},
    {path: "/about", name: "About", element: <About/>},
    {path: "/verify-password", name: "VerifyPassword", element: <VerifyPassword/>},
    {path: "email-confirmation", name: "EmailConfirm", element: <EmailConfirm/>},
    {path: "email-confirmation-error", name: "EmailConfirmError", element: <EmailConfirmError/>},
];