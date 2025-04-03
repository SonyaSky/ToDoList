import React from "react";
import Form from 'react-bootstrap/Form';

import './textInput.css';

const TextInput = ({type, placeholder, title, as, required, onChange}) => {
    return (
          <Form.Group className="mb-3 group" >
            <Form.Label className="label">{title}</Form.Label>
            <Form.Control onChange={onChange} required={required} className="text-input" {...(as == null ? { type: type } : { as: as })}  placeholder={placeholder} />
          </Form.Group>
      );
}

export default TextInput;