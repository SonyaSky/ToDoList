import {BASE_URL} from './BASE_URL'

async function ToggleTask(id) {
    try {
        const res = await fetch(BASE_URL + id + "/toggle", {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        const data = await res.json();

        if (!res.ok) {
            console.log(data.description);
            return;
        }
        return data;
    } catch (error) {
        console.log(error);
    }
}

export default ToggleTask;