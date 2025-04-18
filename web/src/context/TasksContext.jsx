import { createContext, useState, useContext, React } from 'react';

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
    const [tasks, setTasks] = useState(data);

    const AddTask = (task) => {
        task = {
            ...task,
            status: "Active",
            id: tasks.length + 1
        }
        //here will be sending task to api and receiving normal task
        setTasks((prevTasks) => [task, ...prevTasks]);
    }

    const CheckTask = (taskId) => {
        setTasks(prevTasks =>
            prevTasks.map(task =>
                task.id === taskId ? { ...task, checked: !task.checked } : task
            )
        );
    }

    const DeleteTask = (taskId) => {
        setTasks(prevTasks => prevTasks.filter(task => task.id !== taskId));
    }

    const EditTask = (newTask) => {
        //also sending a request and getting new task
        setTasks((tasks) => tasks.map((task) => {
            if (task.id === newTask.id) {
                task = newTask;
                return newTask
            }
            return task;
        }));
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