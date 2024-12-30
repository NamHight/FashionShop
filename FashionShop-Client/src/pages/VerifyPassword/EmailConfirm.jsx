import React from 'react';
import {Typography} from "@material-tailwind/react";

const EmailConfirm = () => {
    return (
        <div className={'flex justify-center items-center text-emerald-400 text-xl font-bold my-10'}>
            <Typography>Your email has been successfully verified. You can now log in to your account.</Typography>
        </div>
    );
};

export default EmailConfirm;