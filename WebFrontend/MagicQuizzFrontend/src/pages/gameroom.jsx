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
                <div className="col"><button disabled className="doorbutton">Matematika (Hamarosan)</button></div>
                <div className="col"><button disabled className="doorbutton">Tudományok (Hamarosan)</button></div>
                <div className="col"><button disabled className="doorbutton">Idegennyelv (Hamarosan)</button></div>
            </div>
            <div className="row">
                <div className="col"><button disabled className="doorbutton">Földrajz (Hamarosan)</button></div>
                <div className="col"><button disabled className="doorbutton">Történelem (Hamarosan)</button></div>
                <div className="col"><button disabled className="doorbutton">Irodalom (Hamarosan)</button></div>
                <div className="col"><button disabled className="doorbutton">Rangsorolt (Hamarosan)</button></div>
            </div>
            
            
        </div>
    );
}

export default Gameroom;