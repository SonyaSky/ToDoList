import React, {useState} from "react";
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';
import Form from 'react-bootstrap/Form';

import './filters.css';
import TextInput from "../Inputs/TextInput";
import Select from "../Inputs/Select";
import { useTasks } from "../../context/TasksContext";

const statuses = ["All", "Active","Completed", "Overdue", "Late"]
const sorting = ["По дате создания (сначала новые)", 
    "По дате создания (сначала старые)",
    "По срочности (сначала срочные)",
"По срочности (сначала не срочные)"
]


const Filters = () => {
    const {FilterTasks} = useTasks();
    const [filters, setFilters] = useState({
        name: "",
        status: statuses[0],
        sorting: sorting[0]
    });
    

    const ChangeFilters = (type) => (e) => {
        const value = e.target.value;
        setFilters(prevTask => ({
            ...prevTask,
            [type]: value
        }));
    };

    const SendFilters = (e) => {
        e.preventDefault();
        const status = statuses.findIndex(status => status === filters.status);
        const query = {
            name: filters.name,
            status: status == 0 ? "" : status - 1,
            sorting: sorting.findIndex(sort => sort === filters.sorting)
        }
        FilterTasks(query);
        console.log(filters);
    }

    return (
        <div className="filters">
            <div className="filters-name">
                Фильтры и сортировки
            </div>
            <Form onSubmit={SendFilters}>
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
                    <button className="create-btn bp" type="submit">
                        Поиск
                    </button>
                </Col>
            </Row>
            </Form>
        </div>
    )
}

export default Filters;