import { useState } from 'react'

import './App.css'
import Table from './components/Table/Table'
import CreateButton from './components/CreateModal/CreateModal'

function App() {
  const [count, setCount] = useState(0)

  return (
    <div className="app-div">
      <div className="row">
        <div className='task-list col'>
          Список дел
        </div>
        <div className="col d-flex align-self-center justify-content-end">
        <CreateButton/>
        </div>
      </div>
      <Table/>
    </div>
  )
}

export default App
