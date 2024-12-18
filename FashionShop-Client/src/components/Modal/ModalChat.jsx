import React from 'react';
import {Button, Popover} from "@material-tailwind/react";
import {BsChatDotsFill} from "react-icons/bs";

const ModalChat = () => {
    return (<Popover placement="top-end">
        <Popover.Trigger as={Button} className={'rounded-full px-2'}>
            <BsChatDotsFill className={'size-5'}/>
        </Popover.Trigger>
        <Popover.Content className="max-w-[30rem] w-[30rem] h-[40rem] p-3 flex flex-col" style={{ boxShadow: "0 0 #0000, 0 0 #0000, 0 1px 2px 0 rgba(0, 0, 0, 0.05)" }}>
                    <div className="flex flex-col space-y-1.5 pb-6">
                        <h2 className="font-semibold text-lg tracking-tight">Chat</h2>
                        <p className="text-sm text-[#6b7280] leading-3">Powered by Mendable and Vercel</p>
                    </div>
                    <div className="flex-1 border-2 gap-3 my-4 overflow-y-auto text-gray-600 text-sm">
                        <div className={'grid-rows-1'}>
<span
    className="relative flex shrink-0 overflow-hidden rounded-full w-8 h-8">
                          <div className="rounded-full bg-gray-100 border p-1"><svg stroke="none" fill="black"
                                                                                    stroke-width="0"
                                                                                    viewBox="0 0 16 16" height="20"
                                                                                    width="20"
                                                                                    xmlns="http://www.w3.org/2000/svg">
                              <path
                                  d="M8 8a3 3 0 1 0 0-6 3 3 0 0 0 0 6Zm2-3a2 2 0 1 1-4 0 2 2 0 0 1 4 0Zm4 8c0 1-1 1-1 1H3s-1 0-1-1 1-4 6-4 6 3 6 4Zm-1-.004c-.001-.246-.154-.986-.832-1.664C11.516 10.68 10.289 10 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664h10Z">
                              </path>
                            </svg>
                          </div>
                        </span>
                            <p className="leading-relaxed"><span className="block font-bold text-gray-700">You </span>fewafef
                            </p>
                        </div>
                    </div>

            <div className="flex items-center pt-0">
                <form className="flex items-center justify-center w-full space-x-2">
                    <input
                        className="flex h-10 w-full rounded-md border border-[#e5e7eb] px-3 py-2 text-sm placeholder-[#6b7280] focus:outline-none focus:ring-2 focus:ring-[#9ca3af] disabled:cursor-not-allowed disabled:opacity-50 text-[#030712] focus-visible:ring-offset-2"
                        placeholder="Type your message" value=""/>
                    <button
                        className="inline-flex items-center justify-center rounded-md text-sm font-medium text-[#f9fafb] disabled:pointer-events-none disabled:opacity-50 bg-black hover:bg-[#111827E6] h-10 px-4 py-2">
                        Send
                    </button>
                </form>
            </div>
            <Popover.Arrow/>
        </Popover.Content>
    </Popover>);
};

export default ModalChat;