import "./Content.css";
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle'
import { ErrorMessage } from "./ErrorMessage";
import "./Default.css"

function Registration() {

    const messages = [
        "Név megadása kötelező",
        "A jelszó legalább 8 karakterből kell,"
    ]

    return (
        <>
            <div className="text-center pagestandards">
                <header className="row">
                    <div className="col-4"></div>
                    <div className="col-4 title"><h1>Regisztráció</h1></div>
                    <div className="col-4"></div>
                </header>
                <body>
                    <ErrorMessage messages={messages}/>
                </body>
            </div>
        </>

    );
}

export default Registration;