import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import "./Avatar.css";
import { useEffect, useState } from 'react';
import axios from 'axios';

function Avatar() {
    const [users, setUsers] = useState([]);
    const [user, setUser] = useState(null);
    const apiUrl = "http://localhost:8000/api";

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            loadUserData();
        } else {
            setUser(null);
        }
    }, []);

    const loadUserData = async () => {
        const token = localStorage.getItem("token");
    
        if (!token) {
            console.error("Token hiányzik.");
            return;
        }
    
        const url = `${apiUrl}/user`;
        try {
            const response = await fetch(url, {
                method: "GET",
                headers: {
                    "Accept": "application/json",
                    "Authorization": `Bearer ${token}`,
                },
            });
            const data = await response.json();
            
            if (response.ok) {
                setUser(data);
            } else {
                console.error("A kérés sikertelen volt, állapotkód:", response.status);
                console.error("Hiba adatok:", data);
            }
        } catch (error) {
            console.error("Hiba történt az adatok lekérése közben:", error);
        }
    };
    
    
    useEffect(() => {
        axios.get('http://localhost:8000/api/user-ranks')
            .then(response => {
                const Users = response.data
                setUsers(Users);
                console.log(users)
              })
            .catch(error => {
                console.error('Felhasználók lekérése sikertelen:', error);
            });
    }, []);

    return (
        <div className="card cardmargin backgroundcolor">
            <div className="card-body">
                <h5 className="card-title titletext">Avatár</h5>
                {user && <p className="card-text desctext">Felhasználó: {user.name}</p>}
            </div>
            <img className="picture" src="../files/avatar.png" alt="Avatar" />
        </div>
    );
}

export default Avatar;
