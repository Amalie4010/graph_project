import { useEffect, useState } from "react";

// Models
import type { ServerData } from "../models/ServerData"
import type { Server } from "../models/Server"

function useServerPage(id: number) {
    const [data, setData] = useState<ServerData | null>(null);
    const [info, setInfo] = useState<Server | null>(null);
    
    async function fetchData(): Promise<void> {
        const res = await fetch(`http://localhost:5140/server/data/${id}`, {
            method: "GET"
        });
        console.log("Fetched:", res);
        
        setData(await res.json());
    }

    async function fetchInfo(): Promise<void> {
        const res = await fetch(`http://localhost:5140/server/${id}`, {
            method: "GET"
        });
        console.log("Fetched:", res);
        
        setInfo(await res.json());
    }

    async function remove(): Promise<void> {
        const res = await fetch(`http://localhost:5140/server/${id}`, {
            method: "DELETE"
        });
        console.log("Fetched:", res);
    }
    
    useEffect(() => {
        fetchData();
        fetchInfo();
    }, [id]);

    return { data, info, remove}
}

export default useServerPage;