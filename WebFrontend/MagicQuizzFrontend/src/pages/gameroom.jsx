import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import './gameroom.css';
import { Link } from 'react-router-dom';

function Gameroom() {
    return (
        <div className="container text-center">
            <div className="row">
                <div className="col"><button className="doorbutton"><Link className="navbartext" to="/game">Minden témakör</Link></button></div>
                <div className="col"><button disabled className="doorbutton"><Link className="navbartext" to="/game">Matematika (Hamarosan)</Link></button></div>
                <div className="col"><button disabled className="doorbutton"><Link className="navbartext" to="/game">Tudományok (Hamarosan)</Link></button></div>
                <div className="col"><button disabled className="doorbutton"><Link className="navbartext" to="/game">Idegennyelv (Hamarosan)</Link></button></div>
            </div>
            <div className="row">
                <div className="col"><button disabled className="doorbutton"><Link className="navbartext" to="/game">Földrajz (Hamarosan)</Link></button></div>
                <div className="col"><button disabled className="doorbutton"><Link className="navbartext" to="/game">Történelem (Hamarosan)</Link></button></div>
                <div className="col"><button disabled className="doorbutton"><Link className="navbartext" to="/game">Irodalom (Hamarosan)</Link></button></div>
                <div className="col"><button disabled className="doorbutton"><Link className="navbartext" to="/game">Rangsorolt (Hamarosan)</Link></button></div>
            </div>
            
            
        </div>
    );
}

export default Gameroom;