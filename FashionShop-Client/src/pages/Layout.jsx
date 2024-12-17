import React, {useState} from 'react';
import {Outlet} from "react-router";
import Header from "../components/Header/Header";
import Footer from "../components/Footer/Footer";
import {Helmet} from "react-helmet-async";


const Layout = () => {

    return (
        <div className={'h-screen w-screen max-w-full max-h-full overflow-auto'}>
            <div className={'sticky top-0 max-w-full w-screen bg-emerald-400 h-[5.5rem]'}>
                <Header/>
            </div>
            <div>
                <Outlet/>
            </div>
            <Footer/>
        </div>
    );
};

export default Layout;