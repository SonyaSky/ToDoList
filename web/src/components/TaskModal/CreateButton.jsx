import React, {useState} from "react";

import './createModal.css';
import TaskModal from "./TaskModal";

const CreateButton = () => {
    const [modalShow, setModalShow] = useState(false);
    return (
        <>
            <button className='create-btn' onClick={() => setModalShow(true)}>
                Добавить
            </button>

            <TaskModal
                show={modalShow}
                onHide={() => setModalShow(false)}
                title="Новое дело"
                buttonName="Создать"
                buttonFunction="add"
            />
        </>
    );
}

export default CreateButton;