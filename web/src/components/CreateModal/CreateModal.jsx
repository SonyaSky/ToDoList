import React, {useState} from "react";
import Form from 'react-bootstrap/Form';
import Modal from 'react-bootstrap/Modal';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';

import './createModal.css';
import TextInput from "../Inputs/TextInput";
import Select from "../Inputs/Select";
import { useTasks } from "../../context/TasksContext";

const priorities = ["Low","Medium", "High", "Critical"]

function MyVerticallyCenteredModal(props) {
    const [task, setTask] = useState({
        name: "",
        description: "",
        deadline: "",
        priority: "Medium"
    });

    const {AddTask} = useTasks();

    const ChangeTask = (type) => (e) => {
        const value = e.target.value;
        setTask(prevTask => ({
            ...prevTask,
            [type]: value
        }));
    };

    const CreateTask = (e) => {
        e.preventDefault();
        console.log(task);
        AddTask(task);
        props.onHide();
    };

    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
            className="modal"
        >
            <Modal.Header closeButton className="modal-header">
                <Modal.Title className="title">
                    Новое дело
                </Modal.Title>
            </Modal.Header>
            <Modal.Body className="modal-body">
                <Form onSubmit={CreateTask}>
                    <TextInput
                        type="text"
                        placeholder="Название"
                        title="Название"
                        required
                        onChange={ChangeTask("name")}
                    />
                    <TextInput
                        as="textarea"
                        placeholder="Описание дела"
                        title="Описание"
                        onChange={ChangeTask("description")}
                    />
                    <Row>
                        <Col>
                            <TextInput
                                type="date"
                                title="Дедлайн"
                                onChange={ChangeTask("deadline")}
                            />
                        </Col>
                        <Col>
                            <Select
                                title="Приоритет"
                                options={priorities}
                                onChange={(selectedOption) => {
                                    setTask(prevTask => ({
                                        ...prevTask,
                                        priority: selectedOption.value
                                    }));
                                }}
                            />
                        </Col>
                    </Row>
                    <div className="d-flex justify-content-end">
                        <button className='create-btn bp' type="submit">
                            Создать
                        </button>
                    </div>
                </Form>
            </Modal.Body>
        </Modal>
    );
}

const CreateButton = () => {
    const [modalShow, setModalShow] = useState(false);
    return (
        <>
            <button className='create-btn' onClick={() => setModalShow(true)}>
                Добавить
            </button>

            <MyVerticallyCenteredModal
                show={modalShow}
                onHide={() => setModalShow(false)}
            />
        </>
    );
}

export default CreateButton;