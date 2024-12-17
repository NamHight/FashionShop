import React, {useEffect, useRef, useState} from 'react';
import {Outlet} from "react-router";
import Header from "../components/Header/Header";
import Footer from "../components/Footer/Footer";
import {Button} from "@material-tailwind/react";
import {BiSolidArrowToTop} from "react-icons/bi";
import { BsChatDotsFill } from "react-icons/bs";
const Layout = () => {
    const [isInVisible, setIsInVisible] = useState(false);
    const layoutRef = useRef(null);
    const handleScrollTop = () => {
        if (layoutRef.current) {
            layoutRef.current.scrollTo({top: 0, behavior: "smooth"});
        }

    }
    const handleScroll = () => {
        if (layoutRef.current) {
            const positionScroll = layoutRef.current.scrollTop;
            setIsInVisible(positionScroll > 500);
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
        <div ref={layoutRef} className={'h-screen w-screen max-w-full max-h-full overflow-auto'}>
            <div className={'sticky top-0 max-w-full w-screen bg-emerald-400 h-[5.5rem]'}>
                <Header/>
            </div>
            <div>
                <Outlet/>
            </div>
            <div className={'grid grid-rows-2 gap-1 justify-end mr-4 z-10'}>
                <Button type={"button"} className={'rounded-full px-2'}>
                    <BsChatDotsFill className={'size-5'}/>
                </Button>
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