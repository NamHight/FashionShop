import React from 'react';
import {Typography} from "@material-tailwind/react";

const EmailConfirmError = () => {
    return (
        <div className={'flex justify-center items-center text-red-500 text-xl font-bold my-10'}>
            <Typography>Email verification failed. Please request a new verification link or contact support.</Typography>
        </div>
    );
};

export default EmailConfirmError;