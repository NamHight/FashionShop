import { Route, Routes, useLocation, useNavigate} from "react-router";
import Layout from "./pages/Layout";
import { Router, routerAccount } from "./router/Router";
import { useEffect, useState } from "react";
import { useAuth } from "./context/AuthContext";
import VerifyPassword from "./pages/VerifyPassword";
import Account from "./pages/Account";
import Loading from './components/Loading';

const TitleUpdater = () => {
  const location = useLocation();

  useEffect(() => {
    const titles = {
      "/": "Fashion - Home",
      "/about": "Fashion - About",
      "/blog": "Fashion - Blog",
      "/blog/article": "Fashion - BlogArticle",
      "/verify-password": "Fashion - Verify Password",
      "/account": "Fashion - My Profile",
      "/account/orders": "Fashion - Orders",
      "/account/listfavorite": "Fashion - List Favorite"
    };
    document.title = titles[location.pathname] || "Fashion";
  }, [location]);
  return null;
};
const AuthRoute = ({ children }) => {
  const { user } = useAuth();
  const [isLoading, setisLoading] = useState(true);
  const navigate = useNavigate();
  useEffect(() => {
    console.log(user);
    if (!user) {
      navigate("/", { replace: true });
    }else{
      setisLoading(false)
    }
  }, [user, navigate]);
  if(isLoading){
    return  <Loading/>
  }
  return children;
};
function App() {
  return (
    <>
      <TitleUpdater />
      <Routes>
        <Route element={<Layout />}>
          {Router.map((route) => {
            return (
              <Route
                key={route.name}
                path={route.path}
                element={route.element}
              />
            );
          })}
          <Route path="account" element={ <AuthRoute> <Account /></AuthRoute>}>
            {routerAccount.map((route) => {
              return (
                <Route
                  key={route.name}
                  path={route.path}
                  element={route.element}
                />
              );
            })}
          </Route>
        </Route>
      </Routes>
    </>
  );
}

export default App;
