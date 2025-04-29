import React, {useState, useEffect} from "react";
import Form from 'react-bootstrap/Form';
import Modal from 'react-bootstrap/Modal';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';

import './createModal.css';
import TextInput from "../Inputs/TextInput";
import Select from "../Inputs/Select";
import { useTasks } from "../../context/TasksContext";
import { formatDateForInput } from "../../helpers/formatDateForInput";

const priorities = ["", "Low","Medium", "High", "Critical"]
const emptyTask = {
    name: "",
    description: "",
    deadline: "",
    priority: null
}

function TaskModal({ show, onHide, baseTask, title, buttonName, buttonFunction }) {
    const { EditTask, AddTask } = useTasks();
    const [task, setTask] = useState(emptyTask);
    const [error, setError] = useState("");

    useEffect(() => {
        if (baseTask) {
            setTask(baseTask); 
        }
    }, [baseTask]);

    const ChangeTask = (type) => (e) => {
        const value = e.target.value;
        setTask(prevTask => ({
            ...prevTask,
            [type]: value
        }));

        if (type === "name" && value.length >= 4) {
            setError("");
        }
    };

    const EditTaskFunc = (e) => {
        e.preventDefault();
        if (task.name.length < 4) {
            setError("Название должно содержать не менее 4 символов.");
            return; 
        }
        console.log(task);
        EditTask(task, task.id);
        onHide();
    };

    const AddTaskFunc = (e) => {
        e.preventDefault();
        console.log(task);
        if (task.name.length < 4) {
            setError("Название должно содержать не менее 4 символов.");
            return;
        }
        AddTask(task);
        setTask(emptyTask);
        onHide();
    };

    return (
        <Modal
            show={show}
            onHide={onHide}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
            className="modal"
        >
            <Modal.Header closeButton className="modal-header">
                <Modal.Title className="title">
                    {title}
                </Modal.Title>
            </Modal.Header>
            <Modal.Body className="modal-body">
                <Form onSubmit={(buttonFunction === "edit" ? EditTaskFunc : AddTaskFunc)}>
                    <TextInput
                        type="text"
                        placeholder="Название"
                        title="Название"
                        value={task.name}
                        required
                        onChange={ChangeTask("name")}
                    />
                    {error && <div className="text-danger">{error}</div>}
                    <TextInput
                        as="textarea"
                        placeholder="Описание дела"
                        title="Описание"
                        value={task.description}
                        onChange={ChangeTask("description")}
                    />
                    <Row>
                        <Col>
                            <TextInput
                                type="date"
                                title="Дедлайн"
                                value={task.deadline ? formatDateForInput(task.deadline) : ''}
                                onChange={ChangeTask("deadline")}
                            />
                        </Col>
                        <Col>
                            <Select
                                title="Приоритет"
                                options={priorities}
                                value={task.priority}
                                onChange={(selectedOption) => {
                                    setTask(prevTask => ({
                                        ...prevTask,
                                        priority: selectedOption.value === "" ? null : selectedOption.value
                                    }));
                                }}
                            />
                        </Col>
                    </Row>
                    <div className="d-flex justify-content-end">
                        <button className='create-btn bp' type="submit">
                            {buttonName}
                        </button>
                    </div>
                </Form>
            </Modal.Body>
        </Modal>
    );
}



export default TaskModal;