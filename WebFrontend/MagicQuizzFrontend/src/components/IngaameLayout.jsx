import { Outlet } from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import Navbar from "./Navbar";
import Footer from "./Footer";

/**
 * 
 * @returns Bejelentkezés utáni oldalak struktúrája
 */


function IngameLayout() {
    return (
        <div className="pagestandards">
            <div className="container">
                <Navbar />
                <main className="container">

                    
                    <Outlet />
                </main>             
            </div>
            <footer>
             <Footer />
            </footer>
            
        </div>

    );
}

export default IngameLayout;