import React, {useState} from "react";

import TaskModal from "./TaskModal";
import './createModal.css';


const EditButton = ({task}) => {
    const [modalShow, setModalShow] = useState(false);
    return (
        <>
            <img className="image" src="edit.png" onClick={() => setModalShow(true)}></img>

            <TaskModal
                show={modalShow}
                onHide={() => setModalShow(false)}
                baseTask={task}
                title="Редактировать"
                buttonName="Сохранить"
                buttonFunction="edit"
            />
        </>
    );
}

export default EditButton;