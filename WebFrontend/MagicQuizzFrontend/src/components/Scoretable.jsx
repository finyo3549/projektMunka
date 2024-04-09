import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import "./Scoretable.css"

function Scoretable() {
  return (
    <ul className="list-group hoverbackground">
      <div className="card-header titletext">
        Ranglista
      </div>
      <li className="list-group-item d-flex justify-content-between align-items-center">
        Alma János
        <span className="badge text-bg-primary rounded-pill">8000</span>
      </li>
      <li className="list-group-item d-flex justify-content-between align-items-center">
        Kiss Emese
        <span className="badge text-bg-primary rounded-pill">6500</span>
      </li>
      <li className="list-group-item d-flex justify-content-between align-items-center">
        Hurka Gyuri
        <span className="badge text-bg-primary rounded-pill">4000</span>
      </li>
      <li className="list-group-item d-flex justify-content-between align-items-center">
        Hurka Gyuri
        <span className="badge text-bg-primary rounded-pill">3800</span>
      </li>
      <li className="list-group-item d-flex justify-content-between align-items-center">
        Hurka Gyuri
        <span className="badge text-bg-primary rounded-pill">3700</span>
      </li>
      <li className="list-group-item d-flex justify-content-between align-items-center">
        Hurka Gyuri
        <span className="badge text-bg-primary rounded-pill">2500</span>
      </li>
      <li className="list-group-item d-flex justify-content-between align-items-center">
        Hurka Gyuri
        <span className="badge text-bg-primary rounded-pill">2000</span>
      </li>
      <li className="list-group-item d-flex justify-content-between align-items-center">
        Hurka Gyuri
        <span className="badge text-bg-primary rounded-pill">1500</span>
      </li>
      <li className="list-group-item d-flex justify-content-between align-items-center">
        Hurka Gyuri
        <span className="badge text-bg-primary rounded-pill">1300</span>
      </li>
      <li className="list-group-item d-flex justify-content-between align-items-center">
        Hurka Gyuri
        <span className="badge text-bg-primary rounded-pill">1000</span>
      </li>
    </ul>);
}

export default Scoretable;