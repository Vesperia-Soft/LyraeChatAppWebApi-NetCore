import React from 'react';
import "./login.css";
import GenericApiService from '../../services/GenericApiService';
import { useNavigate } from 'react-router-dom';

export default function Navbar() {
    const [userName, setUserName] = React.useState("");
    const [password, setPassword] = React.useState("");

    const genericApiService = new GenericApiService();

    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        const data = {
            userName: userName,
            password: password
        }
        await genericApiService.post("/Auth/Login", data);

        if(localStorage.getItem("token").length > 0){
            navigate('/');
        }
    };

    return (
        <div className="wrapper">
            <div className="logo">
                <img src="https://media.licdn.com/dms/image/D4D0BAQFZEEJVPC_GRg/company-logo_200_200/0/1687459723526?e=1697068800&v=beta&t=r0ZQTcvX6v71TsZfmGLANO3tKzjV5VAq5odGN-ljC7Q" alt="" />
            </div>
            <div className="text-center mt-4 name">
                Vesperia Soft
            </div>
            <form className="p-3 mt-3" onSubmit={handleSubmit}>
                <div className="form-field d-flex align-items-center">
                    <span className="far fa-user"></span>
                    <input type="text" name="userName" id="userName" placeholder="Kullanıcı Adı" value={userName} onChange={(e) => setUserName(e.target.value)} />
                </div>
                <div className="form-field d-flex align-items-center">
                    <span className="fas fa-key"></span>
                    <input type="password" name="password" id="pwd" placeholder="Şifre" value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
                <button className="btn btn-custom mt-3" type='submit'>Giriş Yap</button>
            </form>
            <div className="text-center">
                <a style={{ fontSize: '18px' }} href="#">Şifreni mi unuttun?</a>
            </div>
        </div>
    )
}