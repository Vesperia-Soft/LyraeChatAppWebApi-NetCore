import React, { useState, useEffect } from "react";
import "./navbar.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faArrowRightFromBracket,
  faBars,
  faGear,
  faHouse,
} from "@fortawesome/free-solid-svg-icons";
import { useNavigate } from "react-router-dom";

export default function Navbar() {
  const [user, setUser] = useState({});
  const [userPhoto, setUserPhoto] = useState({});
  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      const decodedToken = atob(token?.split(".")[1]);
      const parsedToken = JSON.parse(decodedToken);
      const userName =
        parsedToken[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        ];
      localStorage.setItem("loginUserName", userName);
      const userPhoto = parsedToken["Photo"];
      setUserPhoto(userPhoto);
      setUser({
        userName: userName,
      });
    }
  }, []);

  const navigate = useNavigate();

  const handleRoute = (path) => {
    navigate(path);
    if (path === "/Logout") {
      localStorage.removeItem("token");
    }
  };
  return (
    <div className="nav d-flex justify-content-between align-items-center">
      <button
        className="btn hamburger"
        type="button"
        data-bs-toggle="offcanvas"
        data-bs-target="#offcanvasTop"
        aria-controls="offcanvasTop"
      >
        <FontAwesomeIcon
          icon={faBars}
          style={{ color: "#fff", fontSize: 36 }}
        />
      </button>
      <h1 className="title-head">Vesperia</h1>
      <div>
        <img
          src={`https://localhost:7246/Image/GetImage/${userPhoto}`}
          alt={user.userName}
          className="m-0"
          style={{width:'75px', height:'75px'}}
        />
        <span className="fs-2 text-white mx-4">{user.userName}</span>
      </div>
      <div
        className="offcanvas offcanvas-top"
        tabIndex="-1"
        id="offcanvasTop"
        aria-labelledby="offcanvasTopLabel"
      >
        <div className="offcanvas-body">
          <FontAwesomeIcon
            onClick={() => handleRoute("/")}
            className="icon"
            icon={faHouse}
            role="button"
            style={{ color: "#fff", fontSize: 36 }}
          />
          <FontAwesomeIcon
            onClick={() => handleRoute("/settings")}
            className="icon"
            icon={faGear}
            role="button"
            style={{ color: "#fff", fontSize: 36 }}
          />
          <FontAwesomeIcon
            onClick={() => handleRoute("/Logout")}
            className="icon"
            icon={faArrowRightFromBracket}
            role="button"
            style={{ color: "#fff", fontSize: 36 }}
          />
        </div>
      </div>
    </div>
  );
}
