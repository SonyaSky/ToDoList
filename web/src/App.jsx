import React from 'react'

import './App.css'
import Table from './components/Table/Table'
import CreateButton from './components/TaskModal/CreateButton'
import { TasksProvider } from './context/TasksContext'

function App() {
  return (
    <TasksProvider>
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
    </TasksProvider>
  )
}

export default App
