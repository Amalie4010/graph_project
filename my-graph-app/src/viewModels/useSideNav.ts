import { useEffect, useState } from "react";

// model
import type { Server } from "../models/Server"

function useSideNav() {
    const [addresses, setAddresses] = useState<Server[]>([]);

    async function fetchAddresses(): Promise<void> {
        const res = await fetch(`http://localhost:5140/server`, {
            method: "GET" // fetch is automaticly GET, i have written for readebility
        });
        console.log("Fetched:", res);
    
        setAddresses(await res.json());
    }

    useEffect(() => {
        fetchAddresses();
    }, []);

    return { addresses, fetchAddresses }
}

export default useSideNav;