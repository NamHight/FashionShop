import {replace, Route, Routes, useLocation, useNavigate} from "react-router";
import Layout from "./pages/Layout";
import {Router} from "./router/Router";
import {useEffect} from "react";
import {useAuth} from "./context/AuthContext";
import VerifyPassword from "./pages/VerifyPassword";

const TitleUpdater = () => {
    const location = useLocation();

    useEffect(() => {
        const titles = {
            '/': 'Fashion - Home',
            '/about': 'Fashion - About',
            '/blog': 'Fashion - Blog',
            '/verify-password': 'Fashion - Verify Password',
        };
        document.title = titles[location.pathname] || 'Fashion';
    }, [location]);
    return null;
};
const AuthRoute = ({children}) =>{
    const {user} = useAuth();
    const navigate = useNavigate();
    useEffect(() => {
        if(!user){
            navigate('/',{replace: true});
        }
    }, [user,navigate]);
    return children;
}
function App() {
  return (
      <>
          <TitleUpdater/>
          <Routes >
              <Route element={<Layout />}>
                  {Router.map((route) => {
                      return (
                          <Route key={route.name} path={route.path} element={route.element} />
                      );
                  })}
              </Route>
          </Routes>
      </>

  );
}

export default App;
