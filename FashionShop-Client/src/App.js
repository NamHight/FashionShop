import { Route, Routes, useLocation, useNavigate } from "react-router";
import Layout from "./pages/Layout";
import { Router, routerAccount } from "./router/Router";
import {useEffect, useRef, useState} from "react";
import { useAuth } from "./context/AuthContext";
import VerifyPassword from "./pages/VerifyPassword";
import Account from "./pages/Account";
import Loading from './components/Loading';
import ProductListByCategory from "./components/List/ProductListByCategory";

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
      "/account/listfavorite": "Fashion - List Favorite",
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
    } else {
      setisLoading(false);
    }
  }, [user, navigate]);
  if (isLoading) {
    return <Loading />;
  }
  return children;
};
function App() {
  const [isInVisible, setIsInVisible] = useState(false);
  const layoutRef = useRef(null);
  const handleScrollTop = () => {
    if (layoutRef.current) {
      layoutRef.current.scrollTo({top: 0, behavior: "smooth"});
    }

  }
  const handleScroll = () => {
    if (layoutRef) {
      const positionScroll = layoutRef.current.scrollTop;
      const scrollHeight = layoutRef.current.scrollHeight - layoutRef.current.clientHeight;
      const scrollMath=Math.round((positionScroll * 100) / scrollHeight);
      setIsInVisible(scrollMath > 60 );
    }
  }
  useEffect(() => {
    const layoutCurrent = layoutRef.current;
    if (layoutCurrent) {
      layoutCurrent.addEventListener("scroll", handleScroll);
    }
    return () => {
      if (layoutCurrent) {
        layoutCurrent.removeEventListener("scroll", handleScroll);
      }
    };
  }, []);
  return (
    <div ref={layoutRef} className={'h-screen w-full max-h-screen max-w-full overflow-y-auto'}>
      <TitleUpdater />
      <Routes>
        <Route element={<Layout isInVisible={isInVisible} handleScrollTop={() => handleScrollTop()} />}>
          {Router.map((route) => {
            return (
              <Route
                key={route.name}
                path={route.path}
                element={route.element}
              />
            );
          })}
          <Route
            path="account"
            element={
              <AuthRoute>
                {" "}
                <Account />
              </AuthRoute>
            }
          >
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
          <Route path="/categories/:categorySlug" element={<ProductListByCategory />} />
        </Route>
      </Routes>
    </div>
  );
}

export default App;
