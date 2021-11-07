export default class HttpCommon {
    static async get<T>(url: string): Promise<T> {
        const response = await fetch('/api' + url);
        const data = await response.json();

        return data;
    }

    static async post<T>(url: string, data: T) {
        const response = await fetch('/api' + url, {
            method: 'POST',
            headers: {
                'Accept': '*/*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        return await response.json();
    }
}