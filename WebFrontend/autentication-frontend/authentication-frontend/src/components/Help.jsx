import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import "./help.css"

function Help() {
    return (
        <div className="card backgroundcolor">
            <div className="card-header titletext">
                Segítségek
            </div>
            <ul className="list-group list-group-flush">
                <li className="list-group-item helptext">Segítség 1</li>
                <li className="list-group-item helptext">Segítség 2</li>
                <li className="list-group-item helptext">Segítség 3</li>
            </ul>
        </div>);
}

export default Help;