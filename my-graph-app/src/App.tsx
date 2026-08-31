import { BrowserRouter, Routes, Route } from 'react-router-dom';

import HomePage from "./veiws/HomePage"
import ServerPage from "./veiws/ServerPage"

import './App.css'

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes >
          <Route path="/" element={<HomePage />} />
          <Route path="/:id" element={<ServerPage />} />
        </Routes>
      </BrowserRouter>
    </>
  )
}

export default App
