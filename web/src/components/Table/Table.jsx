import React, {useState} from "react";

import './table.css';
import { useTasks } from "../../context/TasksContext";
import EditButton from "../TaskModal/EditButton";
import ViewLink from "../ViewModal/ViewModal";
import { formatDate } from "../../helpers/formatDate";

const Table = () => {
    const {tasks, CheckTask, DeleteTask} = useTasks();
    if (tasks.length != 0) {
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
                    {task.isChecked ? 
                    <img className="check" src="check.png" onClick={() => CheckTask(task.id)}/> : 
                    <div className="unchecked" onClick={() => CheckTask(task.id)}/>}
                    </div>
                    </td>
                    <td className="breaking-words">
                        <ViewLink taskId={task.id}/>
                    </td>
                    <td className={`${task.status} colorful`}>{task.status}</td>
                    <td className={`${task.priority} colorful`}>{task.priority}</td>
                    <td>{formatDate(task.deadline)}</td>
                    <td>
                        <div className="image-div">
                            <EditButton task={task} />
                            <img className="image" src="delete.png" onClick={() => DeleteTask(task.id)}></img>
                        </div>
                    </td>
                    </tr>
                ))}
            </tbody>
            </table>
        </div>
    );}
}

export default Table;