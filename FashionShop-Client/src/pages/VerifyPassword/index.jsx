import React from 'react';
import {Typography} from "@material-tailwind/react";

const VerifyPassword = () => {
    return (
        <div className={'flex justify-center items-center text-emerald-400 text-xl font-bold my-10'}>
            <Typography>Please verify your email to complete the registration process.</Typography>
        </div>
    );
};

export default VerifyPassword;