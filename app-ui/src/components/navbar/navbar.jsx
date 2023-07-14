import React from 'react'
import "./navbar.css"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faArrowRightFromBracket, faBars, faClose, faGear, faHouse, faInbox } from '@fortawesome/free-solid-svg-icons'
import { useNavigate } from 'react-router-dom'

export default function Navbar() {

    const navigate = useNavigate();

    const handleRoute = (path) => {
        navigate(path);
        if(path === '/Logout'){
            localStorage.removeItem("token");
        }
    }

    return (
        <div className='nav'>
            <button className="btn hamburger" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasTop" aria-controls="offcanvasTop">
                <FontAwesomeIcon icon={faBars} style={{ color: "#fff", fontSize: 36 }} />
            </button>

            <div className="offcanvas offcanvas-top" tabIndex="-1" id="offcanvasTop" aria-labelledby="offcanvasTopLabel">
                <div className="offcanvas-body">
                    <FontAwesomeIcon onClick={() => handleRoute("/")} className="icon" icon={faHouse} role='button' style={{ color: "#fff", fontSize: 36 }} />
                    <FontAwesomeIcon onClick={() => handleRoute("/Message")} className="icon" icon={faInbox} role='button' style={{ color: "#fff", fontSize: 36 }} />
                    <FontAwesomeIcon onClick={() => handleRoute("/Settings")} className="icon" icon={faGear} role='button' style={{ color: "#fff", fontSize: 36 }} />
                    <FontAwesomeIcon onClick={() => handleRoute("/Logout")} className="icon" icon={faArrowRightFromBracket} role='button' style={{ color: "#fff", fontSize: 36 }} />
                </div>
            </div>
        </div>
    )
}