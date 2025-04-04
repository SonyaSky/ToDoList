import React, {useState} from "react";
import Modal from 'react-bootstrap/Modal';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';

import './viewModal.css';


function ViewModal({ show, onHide, task}) {

    return (
        <Modal
            show={show}
            onHide={onHide}
            size="xl"
            aria-labelledby="contained-modal-title-vcenter"
            centered
            className="custom-modal"
        >
            <Modal.Header closeButton className="modal-header">
            <div className="task-name">
                {task.name}
            </div>
            </Modal.Header>
            <Modal.Body className="modal-body">
                <div className="task-desc col-12">
                    {task.description}
                </div>
                <Row className="task-info gy-1">
                    <Col xs={6} xl={3} className="type">
                        Статус:
                    </Col>
                    <Col xs={6} xl={3} className="value d-flex align-items-end">
                        {task.status}
                    </Col>
                    <Col xs={8} xl={4}  className="type">
                        Дата создания:
                    </Col>
                    <Col xs={4} xl={2} className="value d-flex align-items-end">
                        {task.deadline} 
                        {/* нужно потом нормальные поля вставить */}
                    </Col>
                    <Col xs={6} xl={3} className="type ">
                        Приоритет:
                    </Col>
                    <Col xs={6} xl={3} className="value d-flex align-items-end">
                        {task.priority}
                    </Col>
                    <Col xs={8} xl={4} className="type">
                        Дата редактирования:
                    </Col>
                    <Col xs={4} xl={2} className="value d-flex align-items-end">
                        {task.deadline} 
                        {/* нужно потом нормальные поля вставить */}
                    </Col>
                </Row>
                {task.deadline && ( 
                <Row className="deadline">
                    <Col xs={6} xl={3} className="type">
                        Дедлайн:
                    </Col>
                    <Col xs={6} xl={3} className="value d-flex align-items-end">
                        {task.deadline}
                    </Col>
                </Row>
            )}
            </Modal.Body>
        </Modal>
    );
}

const ViewLink = ({task}) => {
    const [modalShow, setModalShow] = useState(false);
    return (
        <>
          <a className='name-link' onClick={() => setModalShow(true)}>
            {task.name}
          </a>
    
          <ViewModal
            show={modalShow}
            onHide={() => setModalShow(false)}
            task={task}
          />
        </>
      );
}

export default ViewLink;