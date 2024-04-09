import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import './gameroom.css';
import { Link } from 'react-router-dom';

function Gameroom() {
    return (
        <div className="container text-center">
            <div className="row">
                <div className="col"><button className="doorbutton"><Link className="navbartext" to="/game">Témakör 1</Link></button></div>
                <div className="col"><button className="doorbutton"><Link className="navbartext" to="/game">Témakör 2</Link></button></div>
                <div className="col"><button className="doorbutton"><Link className="navbartext" to="/game">Témakör 3</Link></button></div>
                <div className="col"><button className="doorbutton"><Link className="navbartext" to="/game">Témakör 4</Link></button></div>
            </div>
            <div className="row">
                <div className="col"><button className="doorbutton"><Link className="navbartext" to="/game">Témakör 5</Link></button></div>
                <div className="col"><button className="doorbutton"><Link className="navbartext" to="/game">Témakör 6</Link></button></div>
                <div className="col"><button className="doorbutton"><Link className="navbartext" to="/game">Témakör 7</Link></button></div>
                <div className="col"><button className="doorbutton"><Link className="navbartext" to="/game">Témakör 8</Link></button></div>
            </div>
            
            
        </div>
    );
}

export default Gameroom;