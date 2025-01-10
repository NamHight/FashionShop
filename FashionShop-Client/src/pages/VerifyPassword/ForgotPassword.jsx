import React from 'react';
import {Input, Spinner, Typography} from "@material-tailwind/react";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {ForgotPasswordValidate} from "../../libs/validates/FormValidate";
import {useMutation} from "@tanstack/react-query";
import {forgotPassword} from "../../services/api/AuthServices";

const ForgotPassword = () => {
    const mutationPassword = useMutation({
       mutationKey: ["forgotPassword"],
       mutationFn: async (data) => {
           return await forgotPassword(data);
       },onSuccess: (data) => {
           if(data?.status === 404){
               setError("errorEmail", {type: "custom", message: data.data?.Message});
           }else if(data?.status === 400){
               setError("errorEmail", {type: "custom", message: "Something went wrong"});
           }else if(data?.status === 500){
               setError("errorEmail", {type: "custom", message: "Something went wrong"});
           }
       }
    });
    const {register:registerEmail,setError,formState:{errors}, reset, handleSubmit} = useForm({
        defaultValues:{
            email: ""
        },
        resolver: zodResolver(ForgotPasswordValidate)
    });
    const handleEmail =async ({email}) =>{
        await mutationPassword.mutateAsync(email);
        reset();
    }
    return (
        <div className="max-w-xl w-full bg-white p-8 rounded-xl shadow shadow-slate-300">
            <Typography as={"h1"} className="text-4xl font-medium">Forgot password</Typography>
            <p className="text-slate-500">Fill up the form to reset the password</p>
            <form onSubmit={handleSubmit(handleEmail)} className="my-8">
                {
                    mutationPassword.isSuccess && <div className={'bg-emerald-400 w-full justify-center items-center flex mb-3 rounded-lg'}>
                        <Typography type={"small"} className="text-white  w-full py-2 text-center">Email sent successfully, please check your email !!!</Typography>
                    </div>
                }
                {
                    mutationPassword.isError && <div className={'bg-red-400 w-full justify-center items-center flex mb-3 rounded-lg'}>
                        <Typography type={"small"} className="text-white  w-full py-2 text-center">Email sent failed</Typography>
                    </div>
                }
                {
                    errors.errorEmail?.message && <div className={'bg-red-400 w-full justify-center items-center flex mb-3 rounded-lg'}>
                        <Typography type={"small"} className="text-white w-full py-2 text-center">errors.errorEmail?.message</Typography>
                    </div>
                }
                <div className="flex flex-col space-y-5">
                    <label htmlFor="email">
                        <Typography as={"p"} className="font-bold text-slate-700 pb-2">Email address</Typography>
                        <Input id="email" {...registerEmail("email")} type="email"
                               className={`${mutationPassword.isPending && 'cursor-not-allowed'} w-full py-3 border border-slate-200 rounded-lg px-3 focus:outline-none mb-1 focus:border-slate-500 hover:shadow`}
                               placeholder="Enter email address" isError={errors.email && true}
                                readOnly={mutationPassword.isPending && true}
                        />
                        {
                            errors.email && <Typography type={"small"} className="text-red-600">{errors.email?.message}</Typography>
                        }
                    </label>
                    <button
                        disabled={mutationPassword.isPending && true}
                        type={"submit"}
                        className={`${mutationPassword.isPending && 'cursor-not-allowed'} w-full py-3 font-medium text-white bg-indigo-600 hover:bg-indigo-500 rounded-lg border-indigo-500 hover:shadow inline-flex space-x-2 items-center justify-center`}>
                        {
                            mutationPassword.isPending ? <Spinner className={'size-4'}/> :
                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
                                     strokeWidth="1.5"
                                     stroke="currentColor" className="w-6 h-6">
                                    <path strokeLinecap="round" strokeLinejoin="round"
                                          d="M15.75 5.25a3 3 0 013 3m3 0a6 6 0 01-7.029 5.912c-.563-.097-1.159.026-1.563.43L10.5 17.25H8.25v2.25H6v2.25H2.25v-2.818c0-.597.237-1.17.659-1.591l6.499-6.499c.404-.404.527-1 .43-1.563A6 6 0 1121.75 8.25z"/>
                                </svg>
                        }
                        <Typography as={"span"}>Reset password</Typography>
                    </button>
                </div>
            </form>
        </div>
    );
};

export default ForgotPassword;