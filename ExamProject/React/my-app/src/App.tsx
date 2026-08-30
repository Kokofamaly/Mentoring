import { useState, useContext } from 'react'
import { BrowserRouter, Routes, Route, createContext } from "react-router-dom";
import { UserContext, type User} from './UserContext';
import { Header } from './Header';
import './App.css'

function App() {
  const [user, setUser] = useState<User | null>(null);

  return (
    <UserContext value={user}>
      <BrowserRouter>
        <Routes>
          <Route element={<Header />}>
            
          </Route>
        </Routes>
      </BrowserRouter>
    </UserContext>
  )
}

export default App
