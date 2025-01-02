import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import {BrowserRouter} from "react-router";
import {AnimatePresence} from 'motion/react';
import {AuthProvider} from "./context/AuthContext";
import {QueryClient, QueryClientProvider} from "@tanstack/react-query";
import { CartContextProvider } from './context/CartContext';    
const queryClient = new QueryClient()
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
      <QueryClientProvider client={queryClient}>
          <AuthProvider>
            <CartContextProvider>
              <BrowserRouter>
                  <AnimatePresence>
                      <App />
                  </AnimatePresence>
              </BrowserRouter>
            </CartContextProvider>
          </AuthProvider>
      </QueryClientProvider>
  </React.StrictMode>

);

