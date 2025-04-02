import React, {useState} from "react";

import './table.css';

const data = [
    {
        id: '2',
        name: "To do smth",
        description: "To do something",
        status: "Active",
        priority: "Medium",
        deadline: "03.05.2025",
        checked: true
    },
    {
        id: '3',
        name: "Todosmthsdkhfsjkdvskjdvksjvssdfsdfsdfsdfsdfsdhfcbjhbsdhfgsjcbsnbdsdhcksjbcnxzbcmksdhksdchksjcbksjbcbsdchs",
        description: "To do something",
        status: "Completed",
        priority: "Low",
        deadline: "03.05.2025",
        checked: true
    },
    {
        id: '4',
        name: "To do smth",
        description: "To do something",
        status: "Overdue",
        priority: "High",
        deadline: "03.05.2025",
        checked: false
    },
    {
        id: '5',
        name: "To do smth",
        description: "To do something",
        status: "Late",
        priority: "Critical",
        deadline: "03.05.2025",
        checked: false
    },
]


const Table = () => {
    const [tasks, setTasks] = useState(data);
    return (
        <div className="table-div">
            <table>
            <thead>
            <tr>
            <th scope="col"/>
            <th scope="col">Название</th>
            <th className="fixed" scope="col">Статус</th>
            <th className="fixed" scope="col">Приоритет</th>
            <th className="fixed" scope="col">Дедлайн</th>
            <th scope="col"/>
            </tr>
            </thead>
            <tbody>
                {tasks.map((task) => (
                    <tr key={task.id}>
                    <td scope="row"><div className="image-div">
                    {task.checked ? <img className="check" src="check.png"/> : <div className="unchecked"/>}
                        </div>
                    </td>
                    <td className="breaking-words">{task.name}</td>
                    <td>{task.status}</td>
                    <td>{task.priority}</td>
                    <td>{task.deadline}</td>
                    <td>
                        <div className="image-div">
                            <img className="image" src="edit.png"></img>
                            <img className="image" src="delete.png"></img>

                        </div>
                    </td>
                    </tr>
                ))}
            </tbody>
            </table>
        </div>
    );
}

export default Table;