import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import "./Avatar.css";
import { useEffect, useState } from 'react';
import axios from 'axios';

function Avatar() {
    const [users, setUsers] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:8000/api/user-ranks')
            .then(response => {
                const Users = response.data
                setUsers(Users);
              })
            .catch(error => {
                console.error('Error fetching users:', error);
            });
    }, []);

    return (
        <div className="card cardmargin backgroundcolor">
            <div className="card-body">
                <h5 className="card-title titletext">Avatár</h5>
                <p className="card-text desctext">Felhasználó: Alma János</p>
            </div>
            <img className="picture" src="../files/avatar.png" alt="Avatar" />
        </div>);
}

export default Avatar;