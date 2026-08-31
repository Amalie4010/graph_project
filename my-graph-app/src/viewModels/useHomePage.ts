function useHomePage() {
    async function create(address: string): Promise<void> {
        const res = await fetch("http://localhost:5140/server", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ address })
        });
        console.log("Fetched:", res);
    }

    return { create };
}

export default useHomePage;