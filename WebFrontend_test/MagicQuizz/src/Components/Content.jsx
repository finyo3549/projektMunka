import "./Content.css";
import "./Default.css"
import clock from './clock.jpg';
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle'

function Content() {
    return (
        <div className="pagestandards text-center">
            <main >

                <div >
                    <h1 className="title">MAGIC QUIZZ</h1>
                </div>
                <img src={clock} alt="Clock" />

                <div className="row">
                    <div className="col-sm-2"></div>
                    <div className="col-sm-3"><button className="buttonstandards">Regisztráció</button></div>
                    <div className="col-sm-2"></div>
                    <div className="col-sm-3 "><button className="buttonstandards">Belépés</button></div>
                    <div className="col-sm-2"></div>
                </div>


            </main>

        </div>

    );
}

export default Content;