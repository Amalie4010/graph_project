import { useState } from "react";
import useHomePage from "../viewModels/useHomePage";

// assets
import minecraftLogo from "../assets/MincraftLogo.png";

// Style
import "./CSS/HomePage.css"

// Component
import SideNav from "../components/SideNav"

function HomePage() {
    const [serverAddress, setAddress] = useState("");
    const { create } = useHomePage();

    return (
        <>
            <div id="layout">
                <SideNav />
                <main>
                    <img src={minecraftLogo} id="minecraftLogo"/>
                    <p>Server status</p>
                    <input
                        type="text"
                        value={serverAddress}
                        onChange={(e) => setAddress(e.target.value)}
                    />
                    <button className="action" onClick={() => create(serverAddress)}>
                        Add server
                    </button>
                </main>
            </div>
        </>
    )
}

export default HomePage