import React, {useEffect, useRef, useState} from 'react';
import {Avatar, Button, Input, Popover, Typography} from "@material-tailwind/react";
import {BsChatDotsFill} from "react-icons/bs";
import {AiOutlineClose} from "react-icons/ai";
import {useForm} from "react-hook-form";
import {ChatValidate} from "../../libs/validates/FormValidate";
import {zodResolver} from "@hookform/resolvers/zod";
import {getLocalStorage, setLocalStorage} from "../../libs/Helpers/LocalStorageConfig";
import {useChat} from "../../context/ChatContext";

const ModalChat = () => {
    const [openModalChat, setOpenModalChat] = useState(false);
    const [getUserNameChatLocalStorage, setGetUserNameChatLocalStorage] = useState(() => {
        return getLocalStorage('username');
    })
    const {handleJoinChatRoom,connection,greeting,messages,handleSendMessage} = useChat();
    const {register,formState:{errors}, handleSubmit,reset,setError,} = useForm({
        defaultValues: {
            username: "",
        },
        resolver: zodResolver(ChatValidate),
    });
    const {register:registerChat,formState:{errors:errorsChat},handleSubmit:handleSubmitChat} = useForm({
        defaultValues:{
            chatting: ""
        }
    })
    useEffect(() => {
        setGetUserNameChatLocalStorage(getLocalStorage('username'));

    }, []);
    const handleEnterChat = async (data) => {
       const success = await handleJoinChatRoom({username:data.username,chatroom:"supporter"});
        if(!success) return;
        setLocalStorage('username',data.username, 5* 24 * 60);
        setGetUserNameChatLocalStorage(data.username);
    }
    const handleCloseModalChat  = (action) =>{
      switch (action) {
          case "open":
              setOpenModalChat(true);
              break;
          case "close":
              setOpenModalChat(false);
              break;
          default:
              break;
      }
    }
    const handleChat = async (data) => {
        await handleSendMessage(data.chatting);
    }
    const enterRoom = () => {
        return (
            <form className={'px-4'} onSubmit={handleSubmit(handleEnterChat)}>
                <div>
                    <Typography as={"label"} htmlFor={"username"} className={'text-lg font-medium font-sans tracking-wide text-emerald-400 mb-2'}>Username</Typography>
                    <Input type="text" isError={errors.username && true} {...register('username')} placeholder="Enter username" id={'username'} className="mb-2 w-full border px-4 py-1 rounded-md h-12 text-lg font-sans focus:outline-2 focus:border-blue-600 hover:border-emerald-500" />
                    {
                        errors.username?.message && (<Typography as={'span'} color={'error'}>
                            {errors.username?.message}
                        </Typography>)
                    }
                </div>
                <div className={'flex justify-end mt-4'}>
                    <Button isFullWidth={true} className={'font-bold text-lg bg-emerald-400 border-emerald-400 hover:bg-emerald-300 hover:border-emerald-300'}>
                        Start chatting
                    </Button>
                </div>
            </form>
        )
    }
    return (
        <Popover placement="top-end"
        open={openModalChat}
        handler={setOpenModalChat}
        >
        <Popover.Trigger as={Button} className={'rounded-full px-2'} onClick={() => handleCloseModalChat("open")} >
            <BsChatDotsFill className={'size-5'}/>
        </Popover.Trigger>
        <Popover.Content className={`max-w-[30rem] w-[30rem] ${connection ? 'h-[40rem]' : 'h-[15rem]' } flex flex-col p-0`} style={{boxShadow: `0 0 #0000, 0 0 #0000, 0 1px 2px 0 rgba(0, 0, 0, 0.05)`}}>
            <div className="flex flex-row bg-emerald-400 text-white rounded-t-lg w-full px-4 py-2 mb-2">
                <img src={"https://dub.sh/TdSBP0D"} className={"size-14 rounded-full"} alt="profile-picture"/>
                <div className={'flex flex-col justify-start align-middle ml-2'}>
                    <h1 className={'text-xl font-bold'}>FashionShop.com</h1>
                    <Typography as={"span"}>Chat with us</Typography>
                </div>
                <div className={'flex justify-end items-center w-full'}>
                    <Button className={"bg-emerald-400 outline-0 border-0 p-2 rounded-full hover:bg-gray-200 hover:text-red-500"} onClick={() => handleCloseModalChat("close")}>
                        <AiOutlineClose className={'size-5'}/>
                    </Button>
                </div>
            </div>

            {
              !connection || !getUserNameChatLocalStorage ?  enterRoom() : (
                      <div className="flex-1 px-4 gap-3 mb-4 mt-1 rounded overflow-y-auto text-gray-600 text-sm">
                          <div className={'grid grid-cols-12 gap-2'}>
                              <div className={'col-span-2'}>
                                  <img src={"https://dub.sh/TdSBP0D"} className={"size-12 rounded-full "} alt="test"/>
                              </div>
                              <div className={"col-span-10 bg-[#f0f2f5] rounded p-2 text-black text-sm font-sans"}>
                                  <p className="leading-relaxed">
                                      {greeting}
                                  </p>
                              </div>
                          </div>
                          {
                              messages.map((message,index) => (
                                  <div className={'grid grid-cols-12 gap-2'} key={index}>
                                      <div className={'col-span-2'}>
                                          <img src={"https://dub.sh/TdSBP0D"} className={"size-12 rounded-full "}
                                               alt="test"/>
                                      </div>
                                      <div
                                          className={"col-span-10 bg-[#f0f2f5] rounded p-2 text-black text-sm font-sans"}>
                                          <p className="leading-relaxed">
                                              {message.message}
                                          </p>
                                      </div>
                                  </div>
                              ))
                          }
                      </div>
              )
            }
            {
                connection && getUserNameChatLocalStorage && (
                    <div className="flex items-center pt-0 px-2 py-2">
                        <form className="flex items-center justify-center w-full space-x-2" onSubmit={handleSubmitChat(handleChat)}>
                            <input
                                {...registerChat("chatting")}
                                className="flex h-10 w-full rounded-md border border-[#e5e7eb] px-3 py-2 text-sm placeholder-[#6b7280] focus:outline-none focus:ring-2 focus:ring-[#9ca3af] disabled:cursor-not-allowed disabled:opacity-50 text-[#030712] focus-visible:ring-offset-2"
                                placeholder="Type your message"/>
                            <button
                                className="bg-emerald-400 inline-flex items-center justify-center rounded-md text-sm font-medium text-[#f9fafb] disabled:pointer-events-none disabled:opacity-50 hover:bg-emerald-300 font-semibold h-10 px-4 py-2">
                                Send
                            </button>
                        </form>
                    </div>
                )
            }

            <Popover.Arrow/>
        </Popover.Content>
        </Popover>
    )
};

export default ModalChat;