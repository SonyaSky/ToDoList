import {BASE_URL} from './BASE_URL'

async function EditTaskApi(newTask, id) {
    try {
        const res = await fetch(BASE_URL + id, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                name: newTask.name,
                description: newTask.description,
                priority: newTask.priority,
                deadline: newTask.deadline 
            })
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

export default EditTaskApi;