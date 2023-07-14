import React, { useState } from 'react';
import "./password-recovery.css";
import GenericApiService from '../../services/GenericApiService';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faArrowLeft, faKey, faUser } from '@fortawesome/free-solid-svg-icons'
import emailjs from 'emailjs-com';
import { useNavigate } from 'react-router-dom';
import GenericApiService from '../../services/GenericApiService';

export default function PasswordRecovery() {
    const generateCode = () => {
        const codeLength = 6;
        let code = '';
        const characters = '0123456789';
        let randomIndex = 1;
        for (let i = 0; i < codeLength; i++) {
            randomIndex = Math.floor(Math.random() * characters.length);
            code += characters[randomIndex];
        }
        setCode(code);
    };

    const navigate = useNavigate()

    const [code, setCode] = useState("");
    const [userName, setUserName] = useState("");
    const [userCode, setUserCode] = useState("");
    const [infoText, setInfoText] = useState("");
    const [visible, setVisible] = useState(true);
    const [visibleNewPass, setVisibleNewPass] = useState(false);
    const [newPass, setNewPass] = useState("");
    const [renewPass, resetNewPass] = useState("");

    const genericApiService = new GenericApiService();

    const handleSubmit = (e) => {
        e.preventDefault();
        setVisible(false);
        setInfoText("Doğrulama kodu mail adresinize gönderildi.");
        sendEmail()
    };

    const handleConfirm = async (e) => {
        e.preventDefault();
        if (userCode === code) {
            setInfoText("");
            setVisibleNewPass(true)
        } else {
            setInfoText("Doğrulama kodu hatalı.");
        }
    };

    const handleSave = async (e) => {
        e.preventDefault();
        if(newPass.length > 0 && newPass === renewPass){
            const data = {
                email:userName,
                newPass:newPass
            }

            apiService = new GenericApiService();

            apiService.post("Auth/PasswordChange");

            navigate("/login")
        }else{
            setInfoText("Paralolar eşleşmedi.");
        }
    };

    const sendEmail = () => {
        // const templateParams = {
        //     from_name: 'Vesperia Soft',
        //     to_name: userName,
        //     message: code 
        // };

        // emailjs.send('service_wnqct8m', 'template_mcr4ceg', templateParams, 'MZNtWfNh7kiP78S03')
        //     .then((response) => {
        //         console.log('E-posta gönderildi!', response.status, response.text);
        //     })
        //     .catch((error) => {
        //         console.error('E-posta gönderme hatası:', error);
        //     });
        console.log("giden kod: " + code);
    };

    return (
        <div className="wrapper">
            <div className="logo">
                <img src="https://media.licdn.com/dms/image/D4D0BAQFZEEJVPC_GRg/company-logo_200_200/0/1687459723526?e=1697068800&v=beta&t=r0ZQTcvX6v71TsZfmGLANO3tKzjV5VAq5odGN-ljC7Q" alt="" />
            </div>
            <div className="text-center mt-4 name">
                Vesperia Soft
            </div>
            <form className="p-3 mt-3" onSubmit={handleSubmit} style={visible && !visibleNewPass ? { display: 'block' } : { display: 'none' }}>
                <div className="form-field d-flex align-items-center">
                    <FontAwesomeIcon style={{ fontSize: 24 }} icon={faUser} />
                    <input type="text" name="userName" id="userName" placeholder="Kullanıcı Adı" value={userName} onChange={(e) => setUserName(e.target.value)} />
                </div>
                <button onClick={generateCode} className="btn btn-custom mt-3" type='submit'>Kod Gönder</button>
            </form>
            <form className="p-3 mt-3" onSubmit={handleConfirm} style={!visible && !visibleNewPass ? { display: 'block' } : { display: 'none' }}>
                <div className="form-field d-flex align-items-center">
                    <FontAwesomeIcon style={{ fontSize: 24 }} icon={faKey} />
                    <input type="number" name="userCode" id="userCode" placeholder="Doğrulama Kodu" value={userCode} onChange={(e) => setUserCode(e.target.value)} />
                </div>
                <div className='d-flex justify-content-center align-items-center'>
                    <FontAwesomeIcon style={{ fontSize: 24, marginRight: 20 }} icon={faArrowLeft} role='button' onClick={() => setVisible(false) && setVisibleNewPass(false)} />
                    <button style={{ width: '100%' }} className="btn btn-custom mt-3" type='submit'>Doğrula</button>
                </div>
            </form>

            <form className="p-3 mt-3" onSubmit={handleSave} style={visibleNewPass ? { display: 'block' } : { display: 'none' }}>
                <div className="form-field d-flex align-items-center">
                    <FontAwesomeIcon style={{ fontSize: 24 }} icon={faKey} />
                    <input type="password" name="newPass" id="newPass" placeholder="Yeni Şifre" value={newPass} onChange={(e) => setNewPass(e.target.value)} />
                </div>
                <div className="form-field d-flex align-items-center">
                    <FontAwesomeIcon style={{ fontSize: 24 }} icon={faKey} />
                    <input type="password" name="newPass2" id="newPass2" placeholder="Yeni Şifre Tekrarı" value={renewPass} onChange={(e) => resetNewPass(e.target.value)} />
                </div>
                <button style={{ width: '100%' }} className="btn btn-custom mt-3" type='submit'>Onayla</button>
            </form>

            <div className="text-center" style={{ marginBottom: '120px', marginTop: '15px' }}>
                <a style={{ fontSize: '24px' }} href="/login">Giriş Yap</a>
            </div>
            <span>{infoText}</span>
        </div>
    )
}