import React from 'react';
import "./login.css";
import GenericApiService from '../../services/GenericApiService';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faKey, faUser } from '@fortawesome/free-solid-svg-icons'
import { toast } from 'react-toastify';

export default function Login() {
    const [userName, setUserName] = React.useState("");
    const [password, setPassword] = React.useState("");
    const genericApiService = new GenericApiService();
    const navigate = useNavigate();

    const notify = (type, message) => {
        switch (type) {
            case 'success':
                toast.success(message, { position: 'bottom-right' })
                break;
            case 'error':
                toast.error(message, { position: 'bottom-right' })
                break;
            default:
                break;
        }
    }
    const handleSubmit = async (e) => {
        e.preventDefault();
        const data = {
            userName: userName,
            password: password
        }
        const response = await genericApiService.post("/Auth/Login", data);
        if (response.status === 200) {
            navigate('/');
            notify('success', "Giriş Başarılı")
        }else{
            notify('error', "Kullanıcı adı veya parola hatalı")
        }
    };
    return (
        <div className="wrapper">
            <div className="logo">
                <img src="https://media.licdn.com/dms/image/D4D0BAQFZEEJVPC_GRg/company-logo_200_200/0/1687459723526?e=1697068800&v=beta&t=r0ZQTcvX6v71TsZfmGLANO3tKzjV5VAq5odGN-ljC7Q" alt="" />
            </div>
            <div className="text-center mt-4  name">
                Vesperia 
            </div>
            <div className='text-center fs-2  blockquote-footer m-3 '>
            Open Source
            </div>
            <form className="p-3 mt-3" onSubmit={handleSubmit}>
                <div className="form-field d-flex align-items-center">
                    <FontAwesomeIcon style={{ fontSize: 24 }} icon={faUser} />
                    <input type="text" name="userName" id="userName" placeholder="Kullanıcı Adı" value={userName} onChange={(e) => setUserName(e.target.value)} />
                </div>
                <div className="form-field d-flex align-items-center">
                    <FontAwesomeIcon style={{ fontSize: 24 }} icon={faKey} />
                    <input type="password" name="password" id="pwd" placeholder="Şifre" value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
                <button className="btn btn-custom mt-3" type='submit'>Giriş Yap</button>
            </form>
            <div className="text-center" style={{ marginBottom: '120px', marginTop: '15px' }}>
                <a style={{ fontSize: '24px' }} href="/password-recovery">Şifreni mi unuttun?</a>
            </div>
        </div>
    )
}