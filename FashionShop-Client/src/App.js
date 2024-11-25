import {Route, Routes} from "react-router";
import Layout from "./pages/Layout";
import Home from "./pages/Home/Home";


function App() {
  return (
    <Routes >
        <Route element={<Layout />}>
            <Route index element={<Home/>} />
        </Route>
    </Routes>
  );
}

export default App;
