import './App.css';
import Dashboard from './layout/dashboard'
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.min.js';
import 'semantic-ui-css/semantic.min.css';

import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Lobby from './components/Lobbyy';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { useState } from 'react';
import Chat from './components/Chat';


function App() {
	// return (
	// 	<div className="App">
	// 		<div className='container-fluid' style={{ background: '#f2ebfb' }}>
	// 			<Dashboard />
	// 		</div>
	// 		<ToastContainer theme='dark' />
	// 	</div>
	// );
	const [connection, setConnection] = useState();
	const [messages, setMessages] = useState([]);
	const [users,setUsers] = useState([]);
	
	const joinRoom = async (user, room) => {
		try {
			const connection = new HubConnectionBuilder()
				.withUrl("https://localhost:7246/chat")
				.configureLogging(LogLevel.Information)
				.build();

			connection.on("UsersInRoom", (users) => {
				setUsers(users)
			})

			connection.on("ReceiveMessage", (user, message) => {
				setMessages(messages => [...messages, { user, message }]);
			});

			connection.onclose(e => {
				setConnection();
				setMessages([]);
				setUsers([]);
			});

			await connection.start();
			await connection.invoke("joinRoom", { user, room });
			setConnection(connection)
		}
		catch (e) {
			console.log(e);
		}
	}

	const closeConnection = async () => {
		try {
			await connection.stop();
		} catch (error) {
			console.log(error);
		}
	}

	const sendMessage = async (message) => {
		try {
			await connection.invoke("SendMessage", message)
		} catch (error) {
			console.log(error);
		}
	}

	return (
		<div className='app'>
			<h2>MyChat</h2>
			<hr className='line' />
			{!connection ?
				<Lobby joinRoom={joinRoom} />
				: <Chat messages={messages} sendMessage={sendMessage} closeConnection={closeConnection} users={users} />
			}
		</div>
	)
}

export default App;
