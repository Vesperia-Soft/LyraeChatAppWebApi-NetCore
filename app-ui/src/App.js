import './App.css';
import Dashboard from './layout/dashboard'
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.min.js';
import 'semantic-ui-css/semantic.min.css';

import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function App() {
	return (
		<div className="App">
			<div className='container-fluid' style={{ background: '#f2ebfb' }}>
				<Dashboard />
			</div>
			<ToastContainer theme='dark' />
		</div>
	);
}

export default App;
