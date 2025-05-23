import {BASE_URL} from './BASE_URL'

async function GetTasks(query) {
    const queryString = new URLSearchParams(query).toString();
    try {
        const res = await fetch(BASE_URL + "list?" + queryString, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        const data = await res.json();

        if (!res.ok) {
            console.log(data.description);
            return;
        }
        console.log(data);
        return data;
    } catch (error) {
        console.log(error);
    }
}

export default GetTasks;