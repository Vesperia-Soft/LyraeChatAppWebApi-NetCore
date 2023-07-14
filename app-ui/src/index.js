// index.js
import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import "./index.css"
import reportWebVitals from './reportWebVitals';
import { BrowserRouter } from 'react-router-dom';
import 'font-awesome/less/font-awesome.less'

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
	<BrowserRouter>
		<App />
	</BrowserRouter>
);

reportWebVitals();