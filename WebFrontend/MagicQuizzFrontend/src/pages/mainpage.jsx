import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import Avatar from '../components/Avatar'
import Help from "../components/Help"
import Scoretable from '../components/Scoretable';
import "../components/Avatar.css";

import { Link } from "react-router-dom";
import { useEffect, useState } from 'react';

function Mainpage() {


    const [user, setUser] = useState(null);
    const apiUrl = "http://localhost:8000/api";
    //  const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            loadUserData();
            console.log(token)
        } else {
            setUser(null);
        }
    }, []);

    const loadUserData = async () => {
        const token = localStorage.getItem("token");
    
        if (!token) {
            console.error("Token is missing.");
            return;
        }
    
        const url = `${apiUrl}/users`;
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
                console.error("Request failed with status:", response.status);
                console.error("Error data:", data);
            }
        } catch (error) {
            console.error("An error occurred while fetching data:", error);
        }
    };
    



    return (
        <div className='container'>
            <div className="row">
                <div className="col">
                    <div style={{width: "50%", marginLeft: "25%"}}>
                    {/*<p>Hello {user.name}</p>*/}
                    <Avatar />
                    </div>
                    
                    <Help />
                </div>
                <div className="col">
                    <Scoretable />
                    <button style={{ marginTop: "20%", width: "50%", marginLeft: "25%" }} className="buttonstandards"><Link className="titletext" to="/gameroom">Játék indítása</Link></button>
                </div>
            </div>
        </div>);

}

export default Mainpage;