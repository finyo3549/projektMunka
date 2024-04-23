import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import "./help.css"

/**
 * 
 * @returns Segítségek komponens létrehozása
 */

function Help() {
    return (
        <div className="card backgroundcolor">
            <div className="card-header titletext">
                Segítségek
            </div>
            <ul className="list-group list-group-flush">
                <li className="list-group-item helptext">Telefon</li>
                <li className="list-group-item helptext">Felezés</li>
            </ul>
        </div>);
}

export default Help;