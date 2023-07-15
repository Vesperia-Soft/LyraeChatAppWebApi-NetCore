import React, { useState } from "react";
import { Row, Col, Container } from "react-bootstrap";
import "./message.css"

function Message() {

    const [selectedUser, setSelectedUser] = useState({})


    const handleClickUser = (user) => {
        setSelectedUser(user)
    }

    return (
        <Container fluid>
            <Row>
                <Col sm={3} className="d-flex flex-column justify-content-around align-items-center " style={{backgroundColor:'#2D4356',height:'calc(100vh - 50px)',maxHeight:'calc(100vh - 50px)', overflowY:'auto'}}>
                    <div role="button" className="card w-100" onClick={() => handleClickUser({name:"Batuhan Topcu"})}>
                        <div className="card-body">
                            Batuhan Topcu <img alt="icon" className="avatar" src="https://static.vecteezy.com/system/resources/previews/009/734/564/original/default-avatar-profile-icon-of-social-media-user-vector.jpg"></img>
                        </div>
                    </div>
                    <div role="button" className="card w-100" onClick={() => handleClickUser({name:"Abdullah Balıkçı"})}>
                        <div className="card-body">
                            Abdullah Balıkçı <img alt="icon" className="avatar" src="https://static.vecteezy.com/system/resources/previews/009/734/564/original/default-avatar-profile-icon-of-social-media-user-vector.jpg"></img>
                        </div>
                    </div>
                    <div role="button" className="card w-100" onClick={() => handleClickUser({name:"Hadise"})}>
                        <div className="card-body">
                            Hadise <img alt="icon" className="avatar" src="https://static.vecteezy.com/system/resources/previews/009/734/564/original/default-avatar-profile-icon-of-social-media-user-vector.jpg"></img>
                        </div>
                    </div>
                    <div role="button" className="card w-100" onClick={() => handleClickUser({name:"Murat Boz"})}>
                        <div className="card-body">
                            Murat Boz <img alt="icon" className="avatar" src="https://static.vecteezy.com/system/resources/previews/009/734/564/original/default-avatar-profile-icon-of-social-media-user-vector.jpg"></img>
                        </div>
                    </div>
                    <div role="button" className="card w-100" onClick={() => handleClickUser({name:"Sagopa Kajmer"})}>
                        <div className="card-body">
                            Sagopa Kajmer <img alt="icon" className="avatar" src="https://static.vecteezy.com/system/resources/previews/009/734/564/original/default-avatar-profile-icon-of-social-media-user-vector.jpg"></img>
                        </div>
                    </div>
                    <div role="button" className="card w-100" onClick={() => handleClickUser({name:"Abuzer Kömürcü"})}>
                        <div className="card-body">
                            Abuzer Kömürcü <img alt="icon" className="avatar" src="https://static.vecteezy.com/system/resources/previews/009/734/564/original/default-avatar-profile-icon-of-social-media-user-vector.jpg"></img>
                        </div>
                    </div>
                    <div role="button" className="card w-100" onClick={() => handleClickUser({name:"Erdal Kömürcü"})}>
                        <div className="card-body">
                        Erdal Kömürcü <img alt="icon" className="avatar" src="https://static.vecteezy.com/system/resources/previews/009/734/564/original/default-avatar-profile-icon-of-social-media-user-vector.jpg"></img>
                        </div>
                    </div>
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