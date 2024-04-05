import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import "./Avatar.css";

function Avatar() {
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