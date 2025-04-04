import React, {useState} from "react";
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';

import './filters.css';
import TextInput from "../Inputs/TextInput";
import Select from "../Inputs/Select";

const statuses = ["All", "Active","Completed", "Overdue", "Late"]
const sorting = ["По дате создания (сначала новые)", 
    "По дате создания (сначала старые)",
    "По срочности (сначала срочные)",
"По срочности (сначала не срочные)"
]

const Filters = () => {
    const [filters, setFilters] = useState({
        name: "",
        status: "All",
        sorting: statuses[0]
    });
    const ChangeFilters = (type) => (e) => {
        const value = e.target.value;
        setFilters(prevTask => ({
            ...prevTask,
            [type]: value
        }));
    };
    return (
        <div className="filters">
            <div className="filters-name">
                Фильтры и сортировки
            </div>
            <Row className="filters-row">
                <Col className="col-5">
                    <TextInput 
                    type="text" 
                    placeholder="Название" 
                    title="Название"
                    onChange={ChangeFilters("name")}/>
                </Col>
                <Col className="col-3">
                    <Select 
                    options={statuses} 
                    value={filters.status} 
                    title="Статус"
                    onChange={(selectedOption) => {
                        setFilters(prevTask => ({
                            ...prevTask,
                            status: selectedOption.value
                        }));
                    }}/>
                </Col>
                <Col className="col-4">
                    <Select 
                    options={sorting} 
                    value={filters.sorting} 
                    title="Сортировка"
                    onChange={(selectedOption) => {
                        setFilters(prevTask => ({
                            ...prevTask,
                            sorting: selectedOption.value
                        }));
                    }}/>
                </Col>
                <Col className="col-12 d-flex justify-content-end">
                    <button className="create-btn bp">
                        Поиск
                    </button>
                </Col>
            </Row>
        </div>
    )
}

export default Filters;