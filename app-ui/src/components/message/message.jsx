import React, { useEffect, useState } from "react";
import { Row, Col, Container } from "react-bootstrap";
import "./message.css";
import GenericApiService from "../../services/GenericApiService";
import { useRef } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPaperPlane, faPlane } from "@fortawesome/free-solid-svg-icons";

function Message({ joinRoom, sendMessage, messages, closeConnection }) {
	const [selectedUser, setSelectedUser] = useState({});
	const [oldMessages, setOldMessages] = useState([]);
	const [userName, setUserName] = useState();
	const [userId, setUserId] = useState();
	const [users, setUsers] = useState([]);
	const [message, setMessage] = useState('');
	const [localStoreId, setLocalStoreId] = useState(0);
	const [isHovered, setIsHovered] = useState(false);
	const [activeRoomId, setActiveRoomId] = useState(0);


	const apiService = new GenericApiService();

	useEffect(() => {
		const token = localStorage.getItem("token");
		if (token) {
			const decodedToken = atob(token?.split(".")[1]);
			const parsedToken = JSON.parse(decodedToken);
			const userId = parsedToken['Id'];
			setLocalStoreId(userId);
			setUserName(
				parsedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
			);
			setUserId(parsedToken["Id"])
		}
	}, []);

	useEffect(() => {
		const getUsers = async () => {
			const response = await apiService.get(`/Room/GetAllUserRoom?userId=${localStoreId}`);
			setUsers(response.data ? response.data.data : []);
		};
		getUsers();
	}, [localStoreId]);

	// useEffect(() => {
	// 	const createRoom = async () => {
	// 		if (room !== undefined) {
	// 			const response = await apiService.post("/Room/Create", {
	// 				name: room,
	// 				isActive: true,
	// 			});
	// 			if (response.data > 0) {
	// 				joinRoom(userName, `${response.data}`);
	// 			}
	// 		}
	// 	};
	// 	createRoom();
	// }, [room]);

	const getOldUser = async (roomId) => {
		const response = await apiService.get(`/Message/GetAll?PageNumber=1&PageSize=10&roomId=${roomId}`);
		setOldMessages(response.data.items)
	}

	const handleClickUser = ({ user }) => {
		selectedUser && closeConnection()
		setSelectedUser(user);
		joinRoom(userName, `${user.roomId}`);
		setActiveRoomId(user.roomId)
		getOldUser(user.roomId);
	};

	const handleSendMessage = async (e) => {
		e.preventDefault();
		sendMessage(message);
		const response = await apiService.post(`/Message/Create`, { text: message, userId: userId, roomId: activeRoomId, creatorName: userName });
		// console.log(response);
		setMessage('');
	}

	const messageRef = useRef();

	useEffect(() => {
		if (messageRef && messageRef.current) {
			const { scrollHeight, clientHeight } = messageRef.current;
			messageRef.current.scrollTo({ left: 0, top: scrollHeight - clientHeight, behavior: 'smooth' });
		}
	}, [messages])

	return (
		<Container fluid>
			<Row>
				<Col
					md={3}
					className="d-flex flex-column justify-content-center align-items-center left-side"
					style={{
						backgroundColor: "#2D4356",
						height: "calc(100vh - 114px)",
						maxHeight: "calc(100vh - 50px)",
						overflowY: "auto",
					}}
				>
					{users.length > 0
						? users.map((user, index) => {
							if (user.userName === localStorage.getItem('loginUserName')) {
								return null;
							}
							return (
								<div
									role="button"
									className="card w-100"
									key={index}
									onClick={() => handleClickUser({ user: user })}
									style={{ maxHeight: '175px' }}
								>
									<div className="card-body d-flex justify-content-between align-items-center">
										<img
											src={`https://localhost:7246/Image/GetImage/${user.photo}`}
											alt={user.userName}
											className="m-0 avatar"
										/>
										<span className="username">{user.name + ' ' + user.surName}{" "}</span>
									</div>
								</div>
							);
						})
						: null}
				</Col>
				<Col md={9} className="message-area p-0">
					{selectedUser.name?.length > 0 ? (
						<>
							<div className="read-message h-100">
								<div ref={messageRef} className="message-container h-100" style={{ maxHeight: 'calc(100vh - 135px)' }}>
									{
										oldMessages.length > 0 && oldMessages.map((m, index) => (
											<div key={index} style={m.userId != userId ? { float: 'left' } : { float: 'right' }} className={"user-message"}>
												<div style={m.userId != userId ? { float: 'left' } : { float: 'right' }} className="message">{m.text}</div>
												{/* <div className="from-user">{m.user}</div> */}
											</div>
										))
									}{

										messages.map((m, index) => (
											<div key={index} style={m.user !== userName ? { float: 'left' } : { float: 'right' }} className={"user-message"}>
												<div style={m.user !== userName ? { float: 'left' } : { float: 'right' }} className="message">{m.message}</div>
												{/* <div className="from-user">{m.user}</div> */}
											</div>
										))
									}
								</div>
							</div>
							<div className="send-message">
								<form className="d-flex p-4 formArea" onSubmit={handleSendMessage}>
									<input type="text" className="form-control m-0" placeholder="message..." onChange={e => setMessage(e.target.value)} value={message} />
									<button onMouseEnter={() => setIsHovered(true)} onMouseLeave={() => setIsHovered(false)} role="button" type="submit" disabled={!message} className="btn btn-custom w-25 mx-3 sendButton" >
										<FontAwesomeIcon className="planeIcon" icon={faPaperPlane} />
									</button>
								</form>
							</div>
						</>
					) :
						<div className="noneSelectedUserArea">
							Konuşmak için Birini Seçiniz...
						</div>
					}
				</Col>
			</Row>
		</Container>
	);
}

export default Message;
