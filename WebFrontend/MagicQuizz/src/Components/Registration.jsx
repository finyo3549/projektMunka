import "./Content.css";
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle'
import { ErrorMessage } from "./ErrorMessage";
import "./Default.css"
import { useState } from "react";
import "./Registration.css";

function Registration() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const messages = [
    ];

    if (username.length < 1) {
        messages.push("Név megadása kötelező")
    }
    if (password.length < 8) {
        messages.push("A jelszó legalább 8 karakterből kell, hogy álljon")
    }

    const usernameInput = (value) => {
        setUsername(value)
    }
    const passwordInput = (value) => {
        setPassword(value)
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