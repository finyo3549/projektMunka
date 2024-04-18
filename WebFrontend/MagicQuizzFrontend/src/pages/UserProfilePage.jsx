// import PropTypes from "prop-types"
import { useEffect, useState } from "react";
// import { useNavigate } from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import Avatar from "../components/Avatar";
import { Link } from "react-router-dom";
import "./UserProfile.css";
import "../standards.css"


function UserProfilePage() {
    const [user, setUser] = useState(null);
    const apiUrl = "http://localhost:8000/api";
    //  const navigate = useNavigate();

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

        const url = apiUrl + "/users"
        const response = await fetch(url, {

            method: "GET",
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer " + token
            }
        });
        const data = await response.json();
        if (response.ok) {
            setUser(data);
        } else {
            localStorage.removeItem("token");
        }
    }


    return (
        <div className='container'>
            <div className="row">
                <div className="col">
                    <div style={{ width: "50%", marginLeft: "25%" }}>
                        <Avatar />
                    </div>
                    <h2 style={{ width: "50%", marginLeft: "25%" }} className="backgroundcolor "><img style={{ width: "27%" }} className="picture" src="../files/star.png" alt="Rank" />Helyezés: 25</h2>

                </div>
                <div className="col ">
                    <form  style={{padding: "10%"}}className="backgroundcolor titletext">
                        <div className="mb-3 ">
                            <label htmlFor="username" className="form-label">Felhasználónév</label>
                            <input type="name" className="form-control" id="username" aria-describedby="Name" />
                        </div>
                        <div className="mb-3">
                            <label htmlFor="password" className="form-label">Jelszó</label>
                            <input type="password" className="form-control" id="password" />
                        </div>
                        <button style={{marginTop: "10%"}} type="submit" className="buttonstandards">Mentés</button>
                    </form>
                    <button style={{ marginTop: "20%", width: "50%", marginLeft: "25%" }} className="buttonstandards"><Link className="titletext" to="/gameroom">Játék indítása</Link></button>
                </div>
            </div>
        </div>)


}

/* Bejelentkezés

        :
        (
            <div>
                <h2>A varázslatos kérdések töltődnek</h2>
            </div>
        );

 */

export default UserProfilePage;