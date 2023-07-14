import React, { useEffect,useState } from 'react';
import { Route, Routes, useNavigate } from 'react-router-dom';
import { Container, Col, Row } from 'react-bootstrap';
import Navbar from '../components/navbar/navbar';
import Login from '../components/login/login';
import PasswordRecovery from '../components/password-recovery/password-recovery';
import Message from '../components/message/message';

export default function Dashboard() {
    const navigate = useNavigate();
    const [flag, setFlag] = useState(true)

    useEffect(() => {
        const token = localStorage.getItem('token');
        const path = window.location.pathname;
        const allowedPaths = ['/login', '/register', '/password-recovery'];
        token ? setFlag(true) : setFlag(false)
        if (!token && !allowedPaths.includes(path)) {
            navigate('/login');
        }else if(allowedPaths.includes(path)){
            setFlag(false)
        }
    }, [navigate]);

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
                        <Route exact path="/" element={<Message />} />
                        <Route exact path="/login" element={<Login />} />
                        {/* <Route exact path="/register" element={<Register />} /> */}
                        <Route exact path="/password-recovery" element={<PasswordRecovery />} />
                    </Routes>
                </Col>
            </Row>
        </Container>
    );
}