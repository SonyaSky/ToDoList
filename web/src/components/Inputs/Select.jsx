import React from "react";
import Form from 'react-bootstrap/Form';

import './textInput.css';

const Select = ({options, title, onChange}) => {
    return (

        <Form.Group className="mb-3 group" >
        <Form.Label className="label">{title}</Form.Label>
        <Form.Select className="text-input" defaultValue={options[1]} onChange={(e) => onChange({ value: e.target.value })}> 
                {options.map((option, index) => (
                    <option key={index} value={option}>{option}</option> 
                ))}
            </Form.Select>
      </Form.Group>
      );
}

export default Select;