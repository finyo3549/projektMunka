import { useEffect } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import { Link } from 'react-router-dom';

/**
 * A fogadó oldal a weblap megnyitása esetén
 * @returns kezdőlap komponens
 */

function HomePage() {

    useEffect(() => {
        
    }, []);
    return ( 
        <div className="text-center">

        <h1>MAGIC QUIZZ</h1>
                <img src={"../files/clock.png"} alt="Clock" />

                <div className="row">
                    <div className="col-sm-2"></div>
                    <div className="col-sm-3">
                    <Link to="/registration">
                        <button className="buttonstandards" href= "" >Regisztráció</button>
                    </Link>
                        </div>
                    <div className="col-sm-2"></div>
                    <div className="col-sm-3 ">
                    <Link to="/Login">
                        <button className="buttonstandards">Belépés</button>
                    </Link>
                        </div>
                    <div className="col-sm-2"></div>
                </div>
        </div>
     );
}

export default HomePage;