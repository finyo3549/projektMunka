import { Link } from "react-router-dom";
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import './Navbar.css';
// import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";


function Navbar() {

    const apiUrl = "http://localhost:8000/api";
    const navigate = useNavigate();

    const logout = async () => {
        const token = localStorage.getItem("token");

        const url = apiUrl + "/logout";
        const response = await fetch(url, {
            method: "POST",
            headers: {
                Accept: "application/json",
                Authorization: "Bearer " + token,
            }
        })
        if (response.ok) {

            localStorage.removeItem("token");
            navigate("/login");

        } else {
            const data = await response.json();
            alert(data.message)
        }
    }

    return (

        <div className="container text-center navbarmargin" >
            <div className="row">
                <div className="col">
                    <button className="buttonstandards"><Link className="navbartext" to="/mainpage">Kezdőlap</Link></button>
                </div>
                <div className="col">
                    <button className="buttonstandards" ><Link className="navbartext"  to="/user">Profil</Link></button>
                </div>
                <div className="col">
                    <button className="buttonstandards" type="button" onClick={() => logout()}>Kijelentkezés</button>
                </div>
            </div>
        </div>
    );
}

export default Navbar;