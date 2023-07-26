import React, { useEffect, useState } from 'react';
import { Route, Routes, useNavigate } from 'react-router-dom';
import { Container, Col, Row } from 'react-bootstrap';
import Navbar from '../components/navbar/navbar';
import Login from '../components/login/login';
import PasswordRecovery from '../components/password-recovery/password-recovery';
import Message from '../components/message/message';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';


export default function Dashboard() {
    const navigate = useNavigate();
    const [flag, setFlag] = useState(true)
    const [connection, setConnection] = useState();
    const [messages, setMessages] = useState([]);

    useEffect(() => {
        const token = localStorage.getItem('token');
        const path = window.location.pathname;
        const allowedPaths = ['/login', '/register', '/password-recovery'];
        token ? setFlag(true) : setFlag(false)
        if (!token && !allowedPaths.includes(path)) {
            navigate('/login');
        } else if (allowedPaths.includes(path)) {
            setFlag(false)
        }
    }, [navigate]);

    const joinRoom = async (user, room) => {
        try {
            const connection = new HubConnectionBuilder()
                .withUrl("https://localhost:7246/chat")
                .configureLogging(LogLevel.Information)
                .build();

            connection.on("ReceiveMessage", (user, message) => {
                setMessages(messages => [...messages, { user, message }]);
            });

            connection.onclose(e => {
                setConnection();
                setMessages([]);
            });

            await connection.start();
            await connection.invoke("joinRoom", { user, room });
            setConnection(connection)
        }
        catch (e) {
            // console.log(e);
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
        <Container fluid className='m-0 p-0' style={{ height: '100vh' }}>
            <Row>
                <div className='m-0 p-0'>
                    {
                        flag ?
                            <Navbar />
                            : null
                    }
                </div>
                <Col md={12} className='m-0 p-0 w-100'>
                    <Routes>
                        <Route exact path="/" element={<Message closeConnection={closeConnection} joinRoom={joinRoom} sendMessage={sendMessage} messages={messages}/>} />
                        <Route exact path="/login" element={<Login />} />
                        <Route exact path="/password-recovery" element={<PasswordRecovery />} />
                    </Routes>
                </Col>
            </Row>
        </Container>
    );
}