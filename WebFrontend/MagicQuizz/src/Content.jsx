import "./Content.css";
import clock from './clock.jpg';
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle'

function Content() {
    return ( 
    <main className="content text-center">

        <div >
            <h1 className = "title">MAGIC QUIZZ</h1>
        </div>
        <img src={clock} alt="Clock" />
        
        <div className="row">
                <div className="col-sm-2"></div>
                <div className="col-sm-3"><button className="button">Regisztráció</button></div>
                <div className="col-sm-2"></div>
                <div className="col-sm-3 "><button className="button">Belépés</button></div>
                <div className="col-sm-2"></div>
        </div>
        
        
    </main> 
    );
}

export default Content;