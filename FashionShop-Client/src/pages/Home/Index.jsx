import React from 'react';
import {motion } from "motion/react";
import {Button} from "@material-tailwind/react";
import {Helmet} from "react-helmet-async";
const routeVariants = {
    initial: { opacity: 0, y: 50 },
    animate: { opacity: 1, y: 0 },
    exit: { opacity: 0, y: -50 },
};
const Home = () => {
    return (
        <motion.div variants={routeVariants} className={'flex justify-center items-center text-red-500 h-[1000px]'}>
            Hello Home
        </motion.div>
    );
};

export default Home;