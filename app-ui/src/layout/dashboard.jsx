import React from 'react';
import { Route, Routes } from 'react-router-dom';
import { Container, Col, Row } from 'react-bootstrap';
import Sidebar from '../components/sidebar/sidebar';
import Navbar from '../components/navbar/navbar';
import Home from '../components/home/home';
import Login from './login'
import Message from '../pages/message';

export default function Dashboard(props) {
    return (
        <Container fluid className='m-0 p-0'>
            <Row>
                <div className='m-0 p-0'>  
                    <Navbar />
                </div>
                <Col md={1} className='m-0 p-0'>
                    <Sidebar />
                </Col>
                <Col md={11} className='m-0 p-0'>
                    <Routes>
                        <Route exact path="/" element={<Home />} />
                        <Route exact path="/login" element={<Login />} />
                        <Route exact path="/message" element={<Message />} />
                    </Routes>
                </Col>
            </Row>
        </Container>
    );
}
