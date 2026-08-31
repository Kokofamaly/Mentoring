const API_URL = "http://localhost:5071";

export async function apiFetch(
    endpoint: string,
    options: RequestInit = {}
) {
    let accessToken = localStorage.getItem("accessToken");

    let response = await fetch(`${API_URL}${endpoint}`, {
        ...options,
        headers: {
            ...options.headers,
            Authorization: `Bearer ${accessToken}`,
            "Content-Type": "application/json"
        },
        credentials: "include"
    });

    if (response.status === 401) {
        const refreshResponse = await fetch(`${API_URL}/auth/refresh`, {
            method: "POST",
            credentials: "include"
        });

        if (!refreshResponse.ok) {
            throw new Error("Session expired");
        }

        const data = await refreshResponse.json();

        accessToken = data.accessToken;
        localStorage.setItem("accessToken", accessToken!);

        response = await fetch(`${API_URL}${endpoint}`, {
            ...options,
            headers: {
                ...options.headers,
                Authorization: `Bearer ${accessToken}`,
                "Content-Type": "application/json"
            },
            credentials: "include"
        });
    }

    return response;
}