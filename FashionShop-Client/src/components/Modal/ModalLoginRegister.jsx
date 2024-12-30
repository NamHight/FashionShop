import {useState} from 'react';
import {Button, Checkbox, Dialog, IconButton, Input, Tabs, Typography} from "@material-tailwind/react";
import {RxAvatar} from "react-icons/rx";
import {IoClose, IoLogoGoogle} from "react-icons/io5";
import {Link, useNavigate} from "react-router";
import {login, register} from "../../services/api/AuthServices";
import {useAuth} from "../../context/AuthContext";
import {TbArrowWaveLeftDown, TbArrowWaveRightDown} from "react-icons/tb";

const ModalLoginRegister = () => {
    const [handleErrorLogin, setHandleErrorLogin] = useState({Email: [], Password: []});
    const [handleErrorRegister, setHandleErrorRegister] = useState({Email: [], Password: [], RePassword: [], Fullname: [], Phone: []});
    const [isOpen, setIsOpen] = useState(null);
    const {setIsAuthenticated} = useAuth();
    const navigate = useNavigate();
    async function formActionLogin(e) {
        e.preventDefault();
        const formValues = Object.fromEntries(new FormData(e.target));
        if (!formValues.email || !formValues.password) {
            setHandleErrorLogin({
                Email: !formValues.email ? ["Email is required"] : [],
                Password: !formValues.password ? ["Password is required"] : [],
            });
            return;
        }
        const data = {
            email: formValues.email,
            password: formValues.password
        }
        const result = await login(data, formValues.remember === "on");
        console.log(result);
        if (result?.StatusCode === 404) {
            setHandleErrorLogin(pre => ({...pre, Email: ["Email does not exist"], Password: []}));
        } else if (result?.StatusCode === 401) {
            setHandleErrorLogin(pre => ({...pre, Email: ["Email or password is incorrect"], Password: []}));
        } else if (result?.StatusCode === 429) {
            setHandleErrorLogin(pre => ({...pre, Email: [result?.Message], Password: []}));
        } else {
            localStorage.setItem("token", result?.token);
            setIsAuthenticated(true);
            handleClose();
            setHandleErrorLogin({Email: [], Password: []});
        }
    }

    async function formActionRegister(e) {
        e.preventDefault();
        const formValues = Object.fromEntries(new FormData(e.target));
        if (!formValues.emailRegister || !formValues.passwordRegister || !formValues.repassword || !formValues.fullname || !formValues.phone ) {
            setHandleErrorRegister({
                Email: !formValues.emailRegister ? ["Email is required"] : [],
                Password: !formValues.passwordRegister ? ["Password is required"] : [],
                RePassword: !formValues.repassword ? ["RePassword is required"] : [],
                Fullname: !formValues.fullname ? ["Fullname is required"] : [],
                Phone: !formValues.phone ? ["Phone is required"] : [],
            });
            return;
        }
        const passwordRegex = /^(?=.*[a-zA-Z])(?=.*\d)/;
        if (!passwordRegex.test(formValues.passwordRegister)) {
            setHandleErrorRegister({ Password: ["Password must contain at least one letter and one number"] });
            return;
        }
        if (formValues.passwordRegister !== formValues.repassword) {
            setHandleErrorRegister({ Password: [], RePassword: ["Password and RePassword do not match"]});
            return;
        }
        if(formValues.fullname.length <5 || formValues.fullname.length > 250){
            setHandleErrorRegister({Fullname: ["Fullname must be between 5 and 250"]});
        }
        if(formValues.phone.length < 10 || formValues.phone.length > 15){
            setHandleErrorRegister({Phone: ["Phone number must be between 10 and 15"]});
            return;
        }
        const phoneRegex = /^09\d{8,13}$/;
        if (!phoneRegex.test(formValues.phone)) {
            setHandleErrorRegister({ Phone: ["Phone number must start with '09' and contain only digits"] });
            return;
        }
        const data = {
            email: formValues.emailRegister,
            password: formValues.passwordRegister,
            confirmPassword: formValues.repassword,
            customerName: formValues.fullname,
            phone: formValues.phone
        }
        const result = await register(data);
        console.log(result);
        if(result?.StatusCode === 409){
            setHandleErrorRegister({ Email: ["Email already exists"]});
        }else{
            navigate("/verify-password");
        }
    }
    const handleClose = () => {
        setIsOpen(false);
    }
    return (
        <Dialog size="sm" open={isOpen}>
            <Dialog.Trigger as={Button}
                            className={'text-white rounded-md px-1 py-2 w-20 border-none bg-emerald-400 hover:bg-emerald-400 hover:text-red-500'}>
                <RxAvatar className={'mr-1 size-6'}/>
                Sign in
            </Dialog.Trigger>
            <Dialog.Overlay>
                <Dialog.Content>
                    <Dialog.DismissTrigger
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
                            <form onSubmit={formActionLogin} className="mt-4">
                                <div className="mb-4 mt-2 space-y-1.5">
                                    <Typography
                                        as="label"
                                        htmlFor="email"
                                        type="small"
                                        color="default"
                                        className="font-semibold tracking-wider text-emerald-400"
                                        tabIndex={0}
                                    >
                                        Email
                                    </Typography>
                                    <Input
                                        id="email"
                                        name={'email'}
                                        type="email"
                                        placeholder="someone@example.com"
                                    />
                                    {
                                        handleErrorLogin.Email?.length > 0 && handleErrorLogin.Email.map((item, index) => (
                                            <Typography key={index} className={'text-red-500 text-sm'}>{item}</Typography>
                                        ))
                                    }
                                </div>
                                <div className="mb-4 space-y-1.5">
                                    <Typography
                                        as="label"
                                        htmlFor="password"
                                        type="small"
                                        color="default"
                                        className="font-semibold tracking-wider text-emerald-400"
                                        tabIndex={1}
                                    >
                                        Password
                                    </Typography>
                                    <Input id="password" name={"password"} type="password" placeholder="************"/>
                                    {
                                        handleErrorLogin.Password?.length > 0 && handleErrorLogin.Password.map((item, index) => (
                                            <Typography key={index} className={'text-red-500 text-sm'}>{item}</Typography>
                                        ))
                                    }
                                </div>
                                <div className="mb-4 flex justify-between items-center gap-2">
                                    <div className={'flex items-center gap-2'}>
                                        <Checkbox id="remember"
                                                  name={"remember"}
                                                  className="appearance-none text-white bg-emerald-500  checked:bg-emerald-500">
                                            <Checkbox.Indicator/>
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
                                <Button isFullWidth
                                        className={'bg-emerald-400 outline-none border-none text-lg font-bold tracking-wider'}>Sign
                                    In</Button>
                            </form>
                        </Tabs.Panel>
                        <Tabs.Panel value="register">
                            <form onSubmit={formActionRegister} className="mt-4">
                                <div className="mb-4 mt-2 space-y-1.5">
                                    <Typography
                                        as="label"
                                        htmlFor="emailRegister"
                                        type="small"
                                        color="default"
                                        className="font-semibold tracking-wider text-emerald-400"
                                        tabIndex={0}
                                    >
                                        Email
                                    </Typography>
                                    <Input
                                        id="emailRegister"
                                        name={'emailRegister'}
                                        type="email"
                                        placeholder="someone@example.com"
                                    />
                                    {
                                        handleErrorRegister.Email?.length > 0 && handleErrorRegister.Email.map((item, index) => (
                                            <Typography key={index} className={'text-red-500 text-sm'}>{item}</Typography>
                                        ))
                                    }
                                </div>
                                <div className="mb-4 space-y-1.5">
                                    <Typography
                                        as="label"
                                        htmlFor="passwordRegister"
                                        type="small"
                                        color="default"
                                        className="font-semibold tracking-wider text-emerald-400"
                                        tabIndex={1}
                                    >
                                        Password
                                    </Typography>
                                    <Input id="passwordRegister" name={"passwordRegister"} type="password" placeholder="************"/>
                                    {
                                        handleErrorRegister.Password?.length > 0 && handleErrorRegister.Password?.map((item, index) => (
                                            <Typography key={index} className={'text-red-500 text-sm'}>{item}</Typography>
                                        ))
                                    }
                                </div>
                                <div className="mb-4 space-y-1.5">
                                    <Typography
                                        as="label"
                                        htmlFor="repassword"
                                        type="small"
                                        color="default"
                                        className="font-semibold tracking-wider text-emerald-400"
                                        tabIndex={1}
                                    >
                                        Re-Password
                                    </Typography>
                                    <Input id="repassword" name={"repassword"} type="password" placeholder="************"/>
                                    {
                                        handleErrorRegister.RePassword?.length > 0 && handleErrorRegister.RePassword?.map((item, index) => (
                                            <Typography key={index} className={'text-red-500 text-sm'}>{item}</Typography>
                                        ))
                                    }
                                </div>
                                <div className="mb-4 space-y-1.5">
                                    <Typography
                                        as="label"
                                        htmlFor="fullname"
                                        type="small"
                                        color="default"
                                        className="font-semibold tracking-wider text-emerald-400"
                                        tabIndex={1}
                                    >
                                        Full Name
                                    </Typography>
                                    <Input id="fullname" name={"fullname"} type="text" placeholder="full name"/>
                                    {
                                        handleErrorRegister.Fullname?.length > 0 && handleErrorRegister.Fullname?.map((item, index) => (
                                            <Typography key={index} className={'text-red-500 text-sm'}>{item}</Typography>
                                        ))
                                    }
                                </div>
                                <div className="mb-4 space-y-1.5">
                                    <Typography
                                        as="label"
                                        htmlFor="phone"
                                        type="small"
                                        color="default"
                                        className="font-semibold tracking-wider text-emerald-400"
                                        tabIndex={1}
                                    >
                                        Phone
                                    </Typography>
                                    <Input id="phone" name={"phone"} type="number" placeholder="098*******"/>
                                    {
                                        handleErrorRegister.Phone?.length > 0 && handleErrorRegister.Phone?.map((item, index) => (
                                            <Typography key={index} className={'text-red-500 text-sm'}>{item}</Typography>
                                        ))
                                    }
                                </div>
                                <div className="mb-4 flex justify-between items-center gap-2">
                                    <div className={'flex items-center gap-2'}>
                                        <Checkbox id="remember"
                                                  name={"remember"}
                                                  className="appearance-none text-white bg-emerald-500  checked:bg-emerald-500">
                                            <Checkbox.Indicator/>
                                        </Checkbox>
                                        <Typography
                                            as="label"
                                            htmlFor="remember"
                                            className="text-foreground cursor-pointer"
                                        >
                                            Remember Me
                                        </Typography>
                                    </div>
                                </div>
                                <Button isFullWidth
                                        className={'bg-emerald-400 outline-none border-none text-lg font-bold tracking-wider'}>Sign Up</Button>
                            </form>
                        </Tabs.Panel>
                    </Tabs>

                </Dialog.Content>
            </Dialog.Overlay>
        </Dialog>
    );
};

export default ModalLoginRegister;