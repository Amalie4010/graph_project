import { Link } from "react-router-dom";
import useSideNav from "../viewModels/useSideNav";

// Style
import "./CSS/Sidenav.css"


function SideNav() {
    const { addresses } = useSideNav();

    return (
        <div id="sideNav">
            <Link to="/"> 
                <button>
                    Add server
                </button>
            </Link>
            {addresses.map( server => 
                <Link to={`/${server.id}`} key={server.id}> 
                    <button>
                        {server.address}
                    </button>
                </Link>
            )}            
        </div>
    )
}

export default SideNav