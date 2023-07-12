import './App.css';
import Dashboard from './layout/dashboard'
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.min.js';
import 'semantic-ui-css/semantic.min.css';

function App() {
	return (
		<div className="App">
			<div className='container-fluid' style={{ background: '#f2ebfb' }}>
				<Dashboard />
			</div>
		</div>
	);
}

export default App;
