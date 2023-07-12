import React from 'react';
import { Route, Routes } from 'react-router-dom';
import { Container, Col, Row } from 'react-bootstrap';
import Sidebar from '../components/sidebar/sidebar';
import Navbar from '../components/navbar/navbar';
import Home from '../components/home/home';
import Login from './login'

export default function Dashboard(props) {
    return (
        <Container fluid style={{ padding: '4px', margin: '0px' }}>
            <Row style={{ height: '98vh' }}>
                <div className='sidebar-col' style={{ padding: '0px', width: '80px', margin: '0px', zIndex: '9999', position: 'fixed', height: '100vh' }}>
                    <Sidebar />
                </div>
                <Col>
                    <Navbar />
                </Col>
                <Col md={11} style={{ flexGrow: '1', marginTop: '50px', left: window.innerWidth > 976 ? '80px' : '0px', padding: '20px', alignItems: 'center', justifyContent: 'center' }}>
                    <Routes>
                        <Route exact path="/" element={<Home />} />
                        <Route exact path="/login" element={<Login />} />
                    </Routes>
                </Col>
            </Row>
        </Container>
    );
}
