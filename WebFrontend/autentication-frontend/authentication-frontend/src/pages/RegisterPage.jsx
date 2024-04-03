import { useRef } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import { useNavigate } from "react-router-dom";
import { Link } from 'react-router-dom';



function RegisterPage() {
    const apiUrl = "http://localhost:8000/api"
    const nameRef = useRef(null);
    const passwordRef = useRef(null);
    const navigate = useNavigate();


    const handleFormSubmit = event =>{
        
        event.preventDefault()
        const newUser = {
            password: passwordRef.current.value,
            name: nameRef.current.value,
        };
        register(newUser)

    }

    const register = async userData => {
        const url = apiUrl + "/registration";
        
        const response = await fetch(url, {
             method: "POST",
             body: JSON.stringify(userData),
             headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
             },

        });
        const data = response.json();
        console.log(data);
        if (response.ok){
            alert("Sikeres regisztráció");
            navigate("/Login");
        } else {
            alert(data.message);
        }
    }

    return (  
        <form onSubmit={handleFormSubmit}  className="middle text container middle">
            <h1>Regisztráció</h1>
            <div>
                <label className="bluebackground" htmlFor="name">Felhasználónév</label>
                <br></br>
                <input style={{ width: '100%' }} type="text" id="name" ref={nameRef}/>
            </div>
            <div>
                <label className="bluebackground" htmlFor="password">Jelszó</label>
                <br></br>
                <input style={{ width: '100%' }} type="password" id="password" ref={passwordRef}/>
            </div>
            <button type="submit" className="buttonstandards">Regisztráció</button>
            <Link to="/">
                        <button className="buttonstandards">Vissza a főoldalra</button>
            </Link>
        </form>
    );
}

export default RegisterPage;