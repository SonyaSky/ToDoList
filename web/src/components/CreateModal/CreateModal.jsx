import React, {useState} from "react";
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';

import './createModal.css';
import TextInput from "../Inputs/TextInput";
import Select from "../Inputs/Select";

const priorities = ["Low","Medium", "High", "Critical"]

function MyVerticallyCenteredModal(props) {
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
        <TextInput type="text" placeholder="Название" title="Название"/>
        <TextInput as="textarea" placeholder="Описание дела" title="Описание"/>
        <Row>
        <Col>
        <TextInput type="date"  title="Дедлайн"/>
        </Col>
        <Col>
          <Select title="Приоритет" options={priorities}/>
        </Col>
      </Row>
      <div className="d-flex justify-content-end">
        <button className='create-btn bp' onClick={props.onHide}>
            Создать
          </button>
      </div>
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