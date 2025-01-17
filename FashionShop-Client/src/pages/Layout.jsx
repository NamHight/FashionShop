import React from 'react';
import {Outlet} from "react-router";
import Header from "../components/Header/Header";
import Footer from "../components/Footer/Footer";
import {Button} from "@material-tailwind/react";
import {BiSolidArrowToTop} from "react-icons/bi";
import ModalChat from "../components/Modal/ModalChat";
import {ToastContainer, Bounce} from 'react-toastify';
const Layout = ({isInVisible,handleScrollTop}) => {
    return (
        <div className={'flex flex-col min-h-screen relative'}>
            <ToastContainer position="top-right"
                            autoClose={3000}
                            hideProgressBar={false}
                            newestOnTop={false}
                            closeOnClick={false}
                            rtl={false}
                            pauseOnFocusLoss
                            draggable
                            pauseOnHover
                            theme="light"
                            transition={Bounce}/>
            <div className={'sticky top-0 max-w-full w-screen bg-emerald-400 z-[999]'}>
                <Header/>
            </div>
            <div className={'flex-1 container flex justify-center items-center mx-auto'}>
                <Outlet/>
            </div>
            <div className={'fixed bottom-0 right-1 grid grid-rows-2 gap-1 justify-end mr-5'}>
                <ModalChat/>
                {
                    isInVisible && (
                        <Button type={"button"} className={`rounded-full px-2  ${isInVisible ? 'opacity-100 transition-all ease-in duration-300' : 'opacity-0'}`} onClick={handleScrollTop}>
                            <BiSolidArrowToTop className={'size-5'}/>
                        </Button>
                    )
                }
            </div>
            <Footer/>

        </div>
    );
};

export default Layout;