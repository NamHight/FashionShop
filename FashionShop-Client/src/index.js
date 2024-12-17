import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import {BrowserRouter} from "react-router";
import {AnimatePresence} from 'motion/react';
import {HelmetProvider} from "react-helmet-async";

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
      <BrowserRouter>
          <AnimatePresence>
              <HelmetProvider>
                  <App />
              </HelmetProvider>
          </AnimatePresence>
      </BrowserRouter>
  </React.StrictMode>
);

