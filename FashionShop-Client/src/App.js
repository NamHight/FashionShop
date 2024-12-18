import {Route, Routes, useLocation} from "react-router";
import Layout from "./pages/Layout";
import {Router} from "./router/Router";
import {useEffect} from "react";

const TitleUpdater = () => {
    const location = useLocation();

    useEffect(() => {
        const titles = {
            '/': 'Fashion - Home',
            '/about': 'Fashion - About',
            '/blog': 'Fashion - Blog',
        };
        document.title = titles[location.pathname] || 'Fashion';
    }, [location]);
    return null;
};

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
