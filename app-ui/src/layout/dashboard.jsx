import React from 'react';
import { Route, Routes } from 'react-router-dom';
import { Container, Col, Row } from 'react-bootstrap';
import Navbar from '../components/navbar/navbar';
import Home from '../components/home/home';
import Login from '../components/login/login';

export default function Dashboard() {
    return (
        <Container fluid className='m-0 p-0' style={{height: '100vh'}}>
            <Row>
                <div className='m-0 p-0'>  
                    <Navbar />
                </div>
                <Col md={12} className='m-0 p-0'>
                    <Routes>
                        <Route exact path="/" element={<Home />} />
                        <Route exact path="/login" element={<Login />} />
                    </Routes>
                </Col>
            </Row>
        </Container>
    );
}
