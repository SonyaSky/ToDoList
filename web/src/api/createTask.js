import {BASE_URL} from './BASE_URL'

async function CreateTask(task) {
    try {
        const res = await fetch(BASE_URL , {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(task)
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

export default CreateTask;