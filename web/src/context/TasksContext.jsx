import { createContext, useState, useContext, React, useEffect } from 'react';

import GetTasks from '../api/getTasks';
import CreateTask from '../api/createTask';
import ToggleTask from '../api/toggleTask';
import DeleteTaskApi from '../api/DeleteTaskApi';
import EditTaskApi from '../api/editTaskApi';

const baseQuery = {
    name: "",
    status: "",
    sorting: 0
}

export const TasksContext = createContext();

export const TasksProvider = ({children}) => {
    const [tasks, setTasks] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const data = await GetTasks(baseQuery);
            if (data) {
                setTasks(data);
                console.log(data);
            }
        };
        fetchData();
    }, []);

    const AddTask = async (task) => {
        task.deadline = task.deadline === '' ? null : task.deadline;
        const newTask = await CreateTask(task);
        setTasks((prevTasks) => [newTask, ...prevTasks]);
    }

    const CheckTask = async (taskId) => {
        const updatedTask = await ToggleTask(taskId);

        setTasks(prevTasks =>
            prevTasks.map(task =>
                task.id === taskId ? updatedTask : task
            )
        );
    }

    const DeleteTask = async (taskId) => {
        var id = await DeleteTaskApi(taskId);
        setTasks(prevTasks => prevTasks.filter(task => task.id !== id));
    }

    const EditTask = async (newTask, id) => {
        newTask.deadline = newTask.deadline === '' ? null : newTask.deadline;
        const apiTask = await EditTaskApi(newTask, id);
        setTasks(prevTasks =>
            prevTasks.map(task =>
                task.id === id ? apiTask : task
            )
        );
    }

    const FilterTasks = async (filters) => {
        const tasks = await GetTasks(filters);
        setTasks(tasks);
    }

    return (
        <TasksContext.Provider value={{tasks, AddTask, DeleteTask, EditTask, setTasks, CheckTask, FilterTasks}}>
            {children}
        </TasksContext.Provider>
    )
}

export const useTasks = () => {
    return useContext(TasksContext);
}