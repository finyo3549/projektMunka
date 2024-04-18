import { Outlet } from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import '../standards.css';
import Footer from "./Footer";
import { Link } from "react-router-dom";

function Layout() {
    return ( 
        <div className="pagestandards">
        <main>
            <Outlet />
            
        </main>
             <Footer />
        </div>

     );
}

export default Layout;