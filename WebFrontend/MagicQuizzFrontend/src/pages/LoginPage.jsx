import { useRef } from "react";
import { useNavigate } from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import { Link } from 'react-router-dom';

/**
 * 
 * @returns Belépési oldal
 */

function LoginPage() {

    // const nameRef = useRef(null); //Felhasználóneves bejelentkezés hozzáadása
    const passwordRef = useRef(null);
    const emailRef = useRef(null);
    const apiUrl = "http://localhost:8000/api";
    const navigate = useNavigate();


    const handleFormSubmit = event => {
        event.preventDefault()
        const user = {
            password: passwordRef.current.value,
            // name: nameRef.current.value, //Felhasználóneves bejelentkezés hozzáadása
            email: emailRef.current.value
        };
        login(user)

    }

    const login = async (formData) => {
        const url = apiUrl + "/login";

        const response = await fetch(url, {
            method: "POST",
            body: JSON.stringify(formData),
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json",
            },

        });
        const data = await response.json();
        console.log(data);
        if (response.ok) {
            localStorage.setItem("token", data.token)
            navigate("/mainpage");
            alert("Sikeres belépés")
        } else {
            alert(data.message);
        }
    };


    return (
        <form onSubmit={handleFormSubmit} className="middle text container middle">
            <h1 style={{marginBottom: "15%"}} >Bejelentkezés</h1>
            {/*<div>
                <label className="bluebackground" htmlFor="loginName">Felhasználónév</label>
                <br></br>
                <input style={{ width: '100%' }} type="text" id="loginName" ref={nameRef} />
            </div>                          //Felhasználóneves bejelentkezés hozzáadása
    */}
            <div>
                <label className="bluebackground" htmlFor="email">Email</label>
                <br></br>
                <input style={{ width: '100%' }} type="email" id="email" ref={emailRef} />
            </div>
            <div>
                <label className="bluebackground" htmlFor="loginPassword">Jelszó</label>
                <br></br>
                <input style={{ width: '100%' , marginBottom: "15%"}} type="password" id="loginPassword" ref={passwordRef} />
            </div>

            <button type="submit" className="buttonstandards">Belépés</button>
            <Link to="/">
                <button className="buttonstandards">Vissza a főoldalra</button>
            </Link>

        </form>);
}


export default LoginPage;