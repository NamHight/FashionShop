import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import {BrowserRouter} from "react-router";
import {AnimatePresence} from 'motion/react';
import {AuthProvider} from "./context/AuthContext";
import {QueryClient, QueryClientProvider} from "@tanstack/react-query";
import { CartContextProvider } from './context/CartContext';
import {GoogleOAuthProvider} from "@react-oauth/google";
import {ChatProvider} from "./context/ChatContext";
import {HelmetProvider} from "react-helmet-async";
const queryClient = new QueryClient()
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
      <QueryClientProvider client={queryClient}>
          <GoogleOAuthProvider clientId={process.env.REACT_APP_GOOGLE_CLIENT_ID}>
              <HelmetProvider>

                  <AuthProvider>
                      <ChatProvider>
                          <CartContextProvider>
                              <BrowserRouter>
                                  <AnimatePresence>
                                      <App />
                                  </AnimatePresence>
                              </BrowserRouter>
                          </CartContextProvider>
                      </ChatProvider>
                  </AuthProvider>
              </HelmetProvider>
          </GoogleOAuthProvider>
      </QueryClientProvider>
  </React.StrictMode>

);

