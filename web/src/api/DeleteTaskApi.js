import {BASE_URL} from './BASE_URL'

async function DeleteTaskApi(id) {
    try {
        const res = await fetch(BASE_URL + id, {
            method: 'DELETE',
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

export default DeleteTaskApi;