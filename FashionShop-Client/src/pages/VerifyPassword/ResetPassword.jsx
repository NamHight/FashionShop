import React, {useState} from 'react';
import {Button, Input, Spinner, Typography} from "@material-tailwind/react";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {ResetPasswordValidate} from "../../libs/validates/FormValidate";
import {useNavigate, useSearchParams} from "react-router";
import {useMutation} from "@tanstack/react-query";
import {resetPassword} from "../../services/api/AuthServices";
import {toast} from "react-toastify";


const ResetPassword = () => {
    const [isResetSuccess, setIsResetSuccess] = useState(false);
    const mutationResetPassword = useMutation({
        mutationKey: ['resetPassword'],
        mutationFn: async (data) => await resetPassword(data),
        onSuccess: async (data) => {
            if(data?.status === 404){
                setError("errorEmail", {type: "custom", message: data.data?.Message});
            }else if(data?.status === 400){
                setError("errorEmail", {type: "custom", message: "Something went wrong"});
            }else if(data?.status === 500){
                setError("errorEmail", {type: "custom", message: "Something went wrong"});
            }else{
                setIsResetSuccess(true);
                toast.success("Password reset successfully");
            }
        }
    });
    const [params]= useSearchParams();
    const {register,setError,formState:{errors},handleSubmit,reset} = useForm({
        defaultValues:{
            password:"",
            confirmPassword:""
        },
        resolver: zodResolver(ResetPasswordValidate)
    })
    const handleResetPassword =async  ({password}) =>{
        let token = params.get("token");
        const data = {
            token:token,
            password:password
        }
        await mutationResetPassword.mutateAsync(data);
    }
    if(isResetSuccess){
        return (
            <div className={' bg-white p-5 shadow rounded shadow-slate-500 max-w-xl w-full font-sans'}>
                <Typography as={"h1"} className={"text-3xl font-bold"}>Reset Password</Typography>
                <div className={'mt-5 '}>
                    <div className={'flex justify-center items-center bg-green-500 text-white w-full rounded py-2 mb-2'}>
                        <Typography className={'font-semibold'}>Password reset successfully</Typography>
                    </div>
                </div>
            </div>
        )
    }
    return (
        <div className={' bg-white p-5 shadow rounded shadow-slate-500 max-w-xl w-full font-sans'}>
            <Typography as={"h1"} className={"text-3xl font-bold"}>Reset Password</Typography>
            <div className={'mt-5 '}>
                {
                    mutationResetPassword.isError &&
                    <div className={'flex justify-center items-center bg-red-500 text-white w-full rounded py-2 mb-2'}>
                        <Typography className={'font-semibold'}>{mutationResetPassword.error.message}</Typography>
                    </div>
                }
                {
                    errors.errorEmail?.message &&
                    <div className={'flex justify-center items-center bg-red-500 text-white w-full rounded py-2 mb-2'}>
                        <Typography className={'font-semibold'}>{errors.errorEmail?.message}</Typography>
                    </div>
                }
                <form onSubmit={handleSubmit(handleResetPassword)} className={'gap-2 flex flex-col'}>
                    <div>
                        <label htmlFor={"password"} className={'text-lg font-semibold'}>
                        New Password
                        </label>
                        <Input readOnly={mutationResetPassword.isPending && true} disabled={mutationResetPassword.isPending && true} isError={errors.password?.message && true} {...register("password")} id={"password"} type={"password"} className={`w-full mt-2 ${mutationResetPassword.isPending && "bg-slate-400 cursor-not-allowed"}`}/>
                        {
                            errors.password?.message && <Typography className={'text-red-500'}>{errors.password.message}</Typography>
                        }
                    </div>
                    <div>
                        <label htmlFor={"confirmPassword"} className={'text-lg font-semibold'}>
                            Confirm Password
                        </label>
                        <Input readOnly={mutationResetPassword.isPending && true} disabled={mutationResetPassword.isPending && true} isError={errors.confirmPassword?.message && true} {...register("confirmPassword")} id={"confirmPassword"} type={"password"} className={`w-full mt-2 ${mutationResetPassword.isPending && "bg-slate-400 cursor-not-allowed"} `}/>
                        {
                            errors.confirmPassword?.message && <Typography className={'text-red-500'}>{errors.confirmPassword?.message}</Typography>
                        }
                    </div>
                    <div>
                        <Button type={"submit"} className={`w-full mt-5 ${mutationResetPassword.isPending && "bg-slate-400 cursor-not-allowed"} `} disabled={mutationResetPassword.isPending && true}>
                            {
                                mutationResetPassword.isPending && <Spinner className={'size-10 mr-3'}/>
                            }
                            Reset
                        </Button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default ResetPassword;