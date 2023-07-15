import React, { useEffect, useState } from "react";
import { Row, Col, Container } from "react-bootstrap";
import "./message.css"
import GenericApiService from "../../services/GenericApiService";
import { async } from "q";

function Message({ joinRoom }) {

    const [selectedUser, setSelectedUser] = useState({})
    const [room, setRoom] = useState();
    const [userName, setUserName] = useState();
    const [users, setUsers] = useState([]);

    const apiService = new GenericApiService();

    useEffect(() => {
        const token = localStorage.getItem('token');

        const decodedToken = atob(token.split('.')[1]);
        const parsedToken = JSON.parse(decodedToken);
        setUserName(parsedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"])

        const getUsers = async () => {
            const response = await apiService.get("/User/GetAll")
            setUsers(response.data.items)
        }

        getUsers()

    }, [])

    useEffect(() => {
        const createRoom = async () => {
            if (room !== undefined) {
                const response = await apiService.post("/Room/Create", { name: room, isActive: true });
                if (response.data > 0) {
                    joinRoom(userName, `${response.data}`);
                }
            }
        };
        createRoom();
    }, [room]);

    const handleClickUser = (user) => {
        setSelectedUser(user)
        const newRoom = userName + user.name;
        setRoom(newRoom);
    }
    return (
        <Container fluid>
            <Row>
                <Col sm={3} className="d-flex flex-column justify-content-around align-items-center " style={{ backgroundColor: '#2D4356', height: 'calc(100vh - 50px)', maxHeight: 'calc(100vh - 50px)', overflowY: 'auto' }}>

                    {users.length > 0 ? users.map((user, index) => (
                        <div role="button" className="card w-100" key={index} onClick={() => handleClickUser({ name: user.name })}>
                            <div className="card-body">
                                {user.name} <img alt="icon" className="avatar" src={user.avatar}></img>
                            </div>
                        </div>
                    )): null}

                </Col>
                <Col sm={9} className="message-area">
                    <h1>
                        {selectedUser.name}
                    </h1>
                </Col>
            </Row>
        </Container>
    );
}

export default Message;