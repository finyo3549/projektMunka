import { useEffect, useState } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import Avatar from "../components/Avatar";
import { Link } from "react-router-dom";
import "./UserProfile.css";
import "../standards.css"
import axios from "axios";

function UserProfilePage() {
    const [user, setUser] = useState(null);
    const [usernameInput, setUsernameInput] = useState(""); // Felhasználónév input állapot
    const apiUrl = "http://localhost:8000/api";
    const [userScore, setUserScore] = useState(null);

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            loadUserData();
            console.log(token)
        } else {
            setUser(null);
        }
    }, []);

    useEffect(() => {
        if (user) {
            const userId = user.id;
            axios.get('http://localhost:8000/api/user-ranks')
                .then(response => {
                    const userData = response.data.find(user => user.user_id === userId);
                    console.log(userData);
                    if (userData) {
                        setUserScore(userData.score);
                    } else {
                        console.error('User not found with ID:', userId);
                    }
                })
                .catch(error => {
                    console.error('Error fetching user score:', error);
                });
        }
    }, [user]);

    const loadUserData = async () => {
        const token = localStorage.getItem("token");

        const url = apiUrl + "/user"
        const response = await fetch(url, {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer "+token,
            },
        });
        const data = await response.json();
        if (response.ok) {
            setUser(data);
        } else {
            //localStorage.removeItem("token");
        }
        
    }

    const handleUsernameChange = (event) => {
        setUsernameInput(event.target.value);
    };

    const updateUser = () => {
        const token = localStorage.getItem("token");
    
        axios.put(`http://localhost:8000/api/users/${user.id}`, {
            name: usernameInput
        }, {
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer "+token,
            },
        })
        .then(response => {
            console.log('User name updated successfully:', response.data);
            setUser(response.data);
            setUsernameInput("");
        })
        .catch(error => {
            console.error('Error updating user name:', error);
        });
    };
    

    return (
        <div className='container'>
            <div className="row">
                <div className="col">
                    <div style={{ width: "50%", marginLeft: "25%" }}>
                        <Avatar />
                    </div>
                    <h2 style={{ width: "50%", marginLeft: "25%" }} className="backgroundcolor ">
                        <img style={{ width: "27%" }} className="picture" src="../files/star.png" alt="Rank" />
                        Pontszám: <p className="middle">{userScore}</p>
                    </h2>
                </div>
                <div className="col ">
                    <form  style={{padding: "10%"}} className="backgroundcolor titletext">
                        <h2 className="titletext">Felhasználói adatok módosítása</h2>
                        <div className="mb-3 ">
                            <label htmlFor="username" className="form-label desctext">Felhasználónév</label>
                            <input type="text" className="form-control desctext" id="username" value={usernameInput} onChange={handleUsernameChange} aria-describedby="Name" />
                        </div>
                        {/*<div className="mb-3">
                            <label htmlFor="password" className="form-label desctext">Jelszó</label>
                            <input type="password" className="form-control" id="password" />
    </div>*/}
                        <button style={{marginTop: "10%"}} type="button" className="buttonstandards" onClick={updateUser}>Mentés</button>
                    </form>
                    <button style={{ marginTop: "20%", width: "50%", marginLeft: "25%" }} className="buttonstandards">
                        <Link className="titletext" to="/gameroom">Játék indítása</Link>
                    </button>
                </div>
            </div>
        </div>
    );
}

export default UserProfilePage;
