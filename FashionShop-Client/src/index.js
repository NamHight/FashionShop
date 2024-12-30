import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import {BrowserRouter} from "react-router";
import {AnimatePresence} from 'motion/react';
import {AuthProvider} from "./context/AuthContext";
import {QueryClient, QueryClientProvider} from "@tanstack/react-query";
const queryClient = new QueryClient()
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
      <QueryClientProvider client={queryClient}>
          <AuthProvider>
              <BrowserRouter>
                  <AnimatePresence>
                      <App />
                  </AnimatePresence>
              </BrowserRouter>
          </AuthProvider>
      </QueryClientProvider>
  </React.StrictMode>
);

