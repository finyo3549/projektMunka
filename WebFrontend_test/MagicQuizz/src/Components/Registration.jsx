import "./Content.css";
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle'
import { ErrorMessage } from "./ErrorMessage";
import "./Default.css"
import { useState } from "react";
import "./Registration.css";

function RegisterForm() {
    const apiurl = "http://localhost:8000/api"
    const nameRef = useRef(null);
    const passwordRef = useRef(null);

    const handleFormSumbit = event => {
        event.preventDefault()
        const newUser = {
            name: nameRef.current.value,
            password: passwordRef.current.value,

        };
        register(newUser)


    }

    const register = async userData => {
        const url = apiurl + "/register"
        const response = await fetch(url, {
            method: "POST",
            body: JSON.stringify(userData),
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        });

        const data = await response.json();
        console.log(data)
        if (response.ok) {

            alert("Sikeres regisztráció")
        } else {
            alert(data.message)
        }

    }

    return (
        <>
            <div className="text-center pagestandards ">
                <header className="row">
                    <div className="col-4"></div>
                    <div className="col-4 title"><h1>Regisztráció</h1></div>
                    <div className="col-4"></div>
                </header>
                <body className="row ">
                    <div className="col-4"></div>
                    <div className="col bluebackground nullmargin ">
                        <h2 className="bluebackground text">Felhasználónév:</h2>
                        <div className=""><input type="text" placeholder="Felhasználónév" value={username} onInput={event => { usernameInput(event.currentTarget.value) }} /></div>
                        <h2 className="bluebackground text">Jelszó:</h2>
                        <div className=""><input type="password" placeholder="Jelszó" value={password} onInput={event => { passwordInput(event.currentTarget.value) }} /></div>
                        <ErrorMessage messages={messages} />
                    </div>
                    <div className="col-4"></div>
                </body>
            </div>
        </>

    );
}

export default Registration;