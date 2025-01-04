import {useRef} from 'react';
import {Button, Checkbox, Dialog, IconButton, Spinner, Tabs, Typography} from "@material-tailwind/react";
import {RxAvatar} from "react-icons/rx";
import {IoClose, IoLogoGoogle} from "react-icons/io5";
import {Link, useNavigate} from "react-router";
import {login, signup} from "../../services/api/AuthServices";
import {useAuth} from "../../context/AuthContext";
import {TbArrowWaveLeftDown, TbArrowWaveRightDown} from "react-icons/tb";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {LoginValidate, RegisterValidate} from "../../libs/validates/FormValidate";
import TextFormField from "../Input/TextFormField";
import {MdOutlineDriveFileRenameOutline, MdOutlineEmail, MdOutlineLock, MdPhone} from "react-icons/md";
import {useMutation, useQueryClient} from "@tanstack/react-query"; // thêm

const ModalLoginRegister = () => {
    const queryClient = useQueryClient();
    const {setIsAuthenticated} = useAuth();
    const navigate = useNavigate();
    const dismissDialog = useRef(null);
    const mutationRegister = useMutation({
        mutationKey: ["signup"],
        mutationFn: async (data) => {
            console.log(data);
            return await signup(data);
        }, onSuccess: data => {
            console.log(data);
            if (data?.StatusCode === 409) {
                setRegisterError("email", {type: "custom", message: "Email already exists"});
            } else if (data?.status === 400) {
                setRegisterError("error", {type: "custom", message: "Something went wrong"});
            } else if (data?.status === 500) {
                setRegisterError("error", {type: "custom", message: "Something went wrong"});
            }else {
                handleClose();
                navigate("/verify-password");
                resetRegister();
            }
        },onError: error => {
            console.log(error)
            setRegisterError("error", {type: "custom", message: "Something went wrong"});
        },
    });
    const mutationLogin = useMutation({
        mutationKey: ["login"],
        mutationFn: async (data, remember) => {
            return await login(data, remember);
        },
        onSuccess: data => {
            if (data?.StatusCode === 404) {
                setLoginError("email", {type: "custom", message: "Email does not exist"});
            } else if (data?.StatusCode === 401) {
                setLoginError("email", {type: "custom", message: "Email or password is incorrect"});
            } else if (data?.StatusCode === 429) {
                setLoginError("email", {type: "custom", message: data?.Message});
            } else if(data.status === 500) {
                setLoginError("error", {type: "custom", message: "Something went wrong"});
            } else if(data.status === 400) {
                setLoginError("error", {type: "custom", message: "Something went wrong"});
            }else {
                localStorage.setItem("token", data?.token);
                setIsAuthenticated(true);
                handleClose();
                resetLogin();
            }
        },
        onError: error => {
            setLoginError("error", {type: "custom", message: "Something went wrong"});
        },
        onSettled: async () =>{
           await queryClient.invalidateQueries(['user']);
        }
    });
    const {
        register: registerRegister,
        setError: setRegisterError,
        handleSubmit: handleRegister,
        reset: resetRegister,
        formState: {errors: registerErrors}
    } = useForm({
        resolver: zodResolver(RegisterValidate),
        defaultValues: {
            email: "",
            password: "",
            confirmPassword: "",
            customerName: "",
            phone: "",
            gender: null,
        }
    });
    const {
        register: loginRegister,
        handleSubmit: handleLogin,
        setError: setLoginError,
        reset: resetLogin,
        formState: {errors: loginErrors}
    } = useForm({
        defaultValues: {
            email: "",
            password: "",
            remember: false
        },
        resolver: zodResolver(LoginValidate)
    });

    async function formActionLogin(data) {
        const parseData = await LoginValidate.safeParseAsync(data);
        if (parseData.error) {
            return;
        }
        await mutationLogin.mutateAsync(parseData.data, parseData.data?.remember); // void
    }
    async function formActionRegister(data) {
        const parseData = await RegisterValidate.safeParseAsync(data);
        if (parseData.error) {
            return;
        }
        await mutationRegister.mutateAsync(parseData.data);
    }

    const handleClose = () => {
        dismissDialog.current.click();
    }
    return (
        <Dialog size="sm">
            <Dialog.Trigger as={Button}
                            className={'text-white rounded-md px-1 py-2 w-20 border-none bg-emerald-400 hover:bg-emerald-400 hover:text-red-500'}>
                <RxAvatar className={'mr-1 size-6'}/>
                Sign in
            </Dialog.Trigger>
            <Dialog.Overlay>
                <Dialog.Content>
                    <Dialog.DismissTrigger
                        ref={dismissDialog}
                        as={IconButton}
                        id={'triggerClose'}
                        size="sm"
                        onClick={handleClose}
                        variant="ghost"
                        color="secondary"
                        className="absolute right-2 top-2 text-emerald-500 hover:text-red-500"
                        isCircular
                    >
                        <IoClose className="h-5 w-5"/>
                    </Dialog.DismissTrigger>
                    <Typography type="h5" className="flex justify-center items-center gap-2 mb-1 text-emerald-500 ">
                        <TbArrowWaveRightDown/> Fashion Shop <TbArrowWaveLeftDown/>
                    </Typography>
                    <div className={'flex justify-center items-center my-4'}>
                        <Button isFullWidth className={'gap-3 text-lg'}><IoLogoGoogle className={'text-xl'}/>Continue to
                            Google</Button>
                    </div>
                    <div className={'relative my-6'}>
                        <div className={'border-b-2 border-black'}></div>
                        <Typography
                            className={'absolute left-1/2 top-1/2 -translate-x-1/2 -translate-y-1/2 bg-white px-5'}>Or</Typography>
                    </div>
                    <Tabs defaultValue="login">
                        <Tabs.List className="w-full rounded-none border-b border-secondary-dark bg-transparent py-0">
                            <Tabs.Trigger className="w-full font-semibold text-emerald-400" value="login">
                                Login
                            </Tabs.Trigger>
                            <Tabs.Trigger className="w-full font-semibold text-emerald-400" value="register">
                                Register
                            </Tabs.Trigger>
                            <Tabs.TriggerIndicator
                                className="rounded-none border-b-2 border-primary bg-transparent shadow-none"/>
                        </Tabs.List>
                        <Tabs.Panel value="login">
                            <form onSubmit={handleLogin(formActionLogin)} className="mt-4">

                                <div className="mb-4 mt-2 space-y-1.5">
                                    <TextFormField icon={<MdOutlineEmail className={'w-full h-full'}/>} label={"Email"}
                                                   error={loginErrors.email?.message} {...loginRegister("email")}
                                                   placeholder={"user@gmail.com"}/>
                                </div>
                                <div className="mb-4 space-y-1.5">
                                    <TextFormField icon={<MdOutlineLock className={'h-full w-full'}/>} type={"password"}
                                                   label={"Password"}
                                                   error={loginErrors.password?.message} {...loginRegister("password")}
                                                   placeholder={"**********"}/>
                                </div>
                                {
                                    loginErrors.error?.message && (
                                        <Typography type={"small"} className={'text-red-500'}>{registerErrors.error?.message}</Typography>
                                    )
                                }
                                <div className="mb-4 flex justify-between items-center gap-2">
                                    <div className={'flex items-center gap-2'}>
                                        <Checkbox id="remember"
                                                  name={"remember"}
                                                  className="appearance-none text-white bg-emerald-500  checked:bg-emerald-500">
                                            <Checkbox.Indicator  {...loginRegister("remember")}/>
                                        </Checkbox>
                                        <Typography
                                            as="label"
                                            htmlFor="remember"
                                            className="text-foreground cursor-pointer"
                                        >
                                            Remember Me
                                        </Typography>
                                    </div>
                                    <div>
                                        <Link to={'/'} className={'text-emerald-400 hover:text-red-500 text-sm'}>Forgot
                                            password</Link>
                                    </div>
                                </div>

                                <Button isFullWidth disabled={mutationLogin.isPending && true} className={'bg-emerald-400 outline-none border-none text-lg font-bold tracking-wider'}>
                                    {
                                        mutationLogin.isPending ? <Spinner className={"mr-2"}/> : null
                                    }
                                    Sign In
                                </Button>
                            </form>
                        </Tabs.Panel>
                        <Tabs.Panel value="register">
                            <form onSubmit={handleRegister(formActionRegister)} className="mt-4">

                                <div className="mb-4 mt-2 space-y-1.5">
                                    <TextFormField {...registerRegister("email")} label={'Email'}
                                                   error={registerErrors.email?.message}
                                                   icon={<MdOutlineEmail className={'w-full h-full'}/>}
                                                   placeholder={"user@gmail.com"}/>
                                </div>
                                <div className="mb-4 space-y-1.5">
                                    <TextFormField {...registerRegister("password")} label={"Password"}
                                                   error={registerErrors.password?.message}
                                                   type={"password"}
                                                   icon={<MdOutlineLock className={'h-full w-full'}/>}
                                                   placeholder={"**********"}/>
                                </div>
                                <div className="mb-4 space-y-1.5">
                                    <TextFormField {...registerRegister("confirmPassword")} label={"Confirm Password"}
                                                   error={registerErrors.confirmPassword?.message}
                                                   type={"password"}
                                                   icon={<MdOutlineLock className={'h-full w-full'}/>}
                                                   placeholder={"**********"}/>
                                </div>
                                <div className="mb-4 space-y-1.5">
                                    <TextFormField {...registerRegister("customerName")} label={"Full Name"}
                                                   icon={<MdOutlineDriveFileRenameOutline className={'w-full h-full'}/>}
                                                   error={registerErrors.customerName?.message}
                                                   placeholder={"John Doe"}/>
                                </div>
                                <div className="mb-4 space-y-1.5">
                                    <TextFormField {...registerRegister("phone")} label={"Phone"}
                                                   icon={<MdPhone className={'w-full h-full'}/>}
                                                   error={registerErrors.phone?.message}
                                                   placeholder={"012345678"}/>
                                </div>
                                {
                                    registerErrors.error?.message && (
                                        <Typography type={"small"} className={'text-red-500'}>{registerErrors.error?.message}</Typography>
                                    )
                                }
                                <Button isFullWidth disabled={mutationRegister.isPending && true}
                                        className={'bg-emerald-400 outline-none border-none mt-5 text-lg font-bold tracking-wider'}>
                                    {
                                        mutationRegister.isPending ? <Spinner className={"mr-2"}/> : null
                                    }
                                    Sign Up
                                </Button>
                            </form>
                        </Tabs.Panel>
                    </Tabs>

                </Dialog.Content>
            </Dialog.Overlay>
        </Dialog>
    );
};

export default ModalLoginRegister;