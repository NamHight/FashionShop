import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import {BrowserRouter} from "react-router";
import {AnimatePresence} from 'motion/react';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
      <BrowserRouter>
          <AnimatePresence>
                  <App />
          </AnimatePresence>
      </BrowserRouter>
  </React.StrictMode>
);

