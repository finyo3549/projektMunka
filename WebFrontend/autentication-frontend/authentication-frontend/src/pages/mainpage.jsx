import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import Avatar from '../components/Avatar'
import Help from "../components/Help"
import Scoretable from '../components/Scoretable';
import "../components/Avatar.css";

import { Link } from "react-router-dom";

function Mainpage() {
    return (
        <div className='container'>
            <div className="row">
                <div className="col">
                    <div style={{width: "50%", marginLeft: "25%"}}>
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