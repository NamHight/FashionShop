import {createContext, useContext, useState} from "react";
import {HubConnectionBuilder,HubConnectionState, LogLevel} from "@microsoft/signalr";

export const ChatContext = createContext(null);

export const ChatProvider = ({children}) => {
    const [connection, setConnection] = useState(null);
    const [greeting, setGreeting] = useState("");
    const [messages, setMessages] = useState([]);
    const handleJoinChatRoom = async ({username,chatroom}) =>{
        try {
            const conn = new HubConnectionBuilder()
                .withUrl("http://localhost:7068/chat")
                .configureLogging(LogLevel.Information)
                .build();
            conn.on("JoinSpecificChatRoomSupporter", (message, username) => {
                setGreeting(message);
            });
            conn.on("ReceiveSpecificSupporter", (username, message) => {
                setMessages(messages => [...messages, {username, message}]);
            })
            await conn.start();
            console.log("Joining chat room with username:", username, "and chatroom:", chatroom);
            if (conn.state === HubConnectionState.Disconnected) {
                await conn.start();
            }
            await conn.invoke("JoinSpecificChatRoomSupporter",{username,chatroom}).catch((err) => {
                console.log(err);
                return false;
            });
            setConnection(conn);
            return true;
        } catch (e) {
            console.log(e);
            return false;
        }  
    }
    const handleSendMessage = async (message) => {
        try {
            await connection.invoke("SendMessage", message).catch((err) => {
                console.log(err);
            });
        } catch (e) {
            console.log(e);
        }
    }
    const value ={
        handleJoinChatRoom,
        connection,
        messages,
        greeting,
        handleSendMessage
    };
    return <ChatContext.Provider value={value}>{children}</ChatContext.Provider>
}


export const useChat = () => {
    const context = useContext(ChatContext);
    if (!context) {
        throw new Error("useChat must be used within a ChatProvider");
    }
    return context;
}