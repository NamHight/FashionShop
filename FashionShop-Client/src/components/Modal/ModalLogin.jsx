import React from 'react';
import {Dialog, IconButton, Input, Typography,Checkbox,Button} from "@material-tailwind/react";
import {RxAvatar} from "react-icons/rx";
import { IoClose } from "react-icons/io5";


const ModalLogin = () => {
    return (
        <Dialog size="sm">
            <Dialog.Trigger as={Button} className={'text-white rounded-md px-1 py-2 w-20 border-none bg-emerald-400 hover:bg-emerald-400 hover:text-red-500'}>
                <RxAvatar className={'mr-1 size-6'}/>
                Sign in
            </Dialog.Trigger>
            <Dialog.Overlay>
                <Dialog.Content>
                    <Dialog.DismissTrigger
                        as={IconButton}
                        size="sm"
                        variant="ghost"
                        color="secondary"
                        className="absolute right-2 top-2"
                        isCircular
                    >
                        <IoClose className="h-5 w-5" />
                    </Dialog.DismissTrigger>
                    <Typography type="h6" className="mb-1">
                        Sign In
                    </Typography>
                    <Typography className="text-foreground">
                        Enter your email and password to Sign In.
                    </Typography>
                    <form action="#" className="mt-6">
                        <div className="mb-4 mt-2 space-y-1.5">
                            <Typography
                                as="label"
                                htmlFor="email"
                                type="small"
                                color="default"
                                className="font-semibold"
                            >
                                Email
                            </Typography>
                            <Input
                                id="email"
                                type="email"
                                placeholder="someone@example.com"
                            />
                        </div>
                        <div className="mb-4 space-y-1.5">
                            <Typography
                                as="label"
                                htmlFor="password"
                                type="small"
                                color="default"
                                className="font-semibold"
                            >
                                Password
                            </Typography>
                            <Input id="password" type="password" placeholder="************" />
                        </div>
                        <div className="mb-4 flex items-center gap-2">
                            <Checkbox id="checkbox">
                                <Checkbox.Indicator />
                            </Checkbox>
                            <Typography
                                as="label"
                                htmlFor="checkbox"
                                className="text-foreground"
                            >
                                Remember Me
                            </Typography>
                        </div>
                        <Button isFullWidth>Sign In</Button>
                    </form>
                    <Typography
                        type="small"
                        className="mb-2 mt-3 flex items-center justify-center gap-1 text-foreground"
                    >
                        Don't have an account?
                        <Typography
                            type="small"
                            color="primary"
                            as="a"
                            href="#"
                            className="font-semibold"
                        >
                            Sign up
                        </Typography>
                    </Typography>
                </Dialog.Content>
            </Dialog.Overlay>
        </Dialog>
    );
};

export default ModalLogin;