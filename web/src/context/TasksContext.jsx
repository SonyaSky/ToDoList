import { createContext, useState, useContext, React, useEffect } from 'react';

import GetTasks from '../api/getTasks';
import CreateTask from '../api/createTask';
import ToggleTask from '../api/toggleTask';
import DeleteTaskApi from '../api/DeleteTaskApi';
import EditTaskApi from '../api/editTaskApi';

const data = [
    {
        id: '1',
        name: "To do smth",
        description: "To do something",
        status: "Active",
        priority: "Medium",
        deadline: "03.05.2025",
        checked: true
    },
    {
        id: '2',
        name: "Todosmthsdkhfsjkdvskjdvksjvssdfsdfsdfsdfsdfsdhfcbjhbsdhfgsjcbsnbdsdhcksjbcnxzbcmksdhksdchksjcbksjbcbsdchs",
        description: "To do something",
        status: "Completed",
        priority: "Low",
        deadline: "03.05.2025",
        checked: true
    },
    {
        id: '3',
        name: "To do smth",
        description: "To do something",
        status: "Overdue",
        priority: "High",
        deadline: "03.05.2025",
        checked: false
    },
    {
        id: '4',
        name: "To do smth",
        description: "To do something",
        status: "Late",
        priority: "Critical",
        deadline: "03.05.2025",
        checked: false
    },
]
const sorting = [
    {
        value: "DateDesc",
        name: "По дате создания (сначала новые)"
    },
    {
        value: "DateAsc",
        name: "По дате создания (сначала старые)"
    },
    {
        value: "PriorityDesc",
        name: "По срочности (сначала срочные)"
    },
    {
        value: "PriorityAsc",
        name: "По срочности (сначала не срочные)"
    }
]

export const TasksContext = createContext();

export const TasksProvider = ({children}) => {
    const [tasks, setTasks] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const data = await GetTasks();
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

    const FilterTasks = (filters) => {
        //sending filters to api
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