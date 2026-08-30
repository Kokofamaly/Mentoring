import React, { useState } from 'react';
import {UserContext, type User} from './UserContext'
import { useMutation } from '@tanstack/react-query';
import { data } from 'react-router-dom';

interface LoginProps {
  setUser: (user: User | null) => void;
}

export function Register({setUser} : LoginProps){
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");


    
    
    function handleRegister(e : React.SubmitEvent){
        e.preventDefault();
    }

    return(<>
        <form onSubmit={handleRegister}>
            <label>Name</label>
            <input type="text" placeholder="Enter your name" value={name} onChange={e => setName(e.target.value)}/>
            <label>Email</label>
            <input type="email" placeholder="Enter your email" value={email} onChange={e => setEmail(e.target.value)}/>
            <label>Password</label>
            <input type="password" placeholder="Enter your password" value={password} onChange={e => setPassword(e.target.value)}/>
            <button type="submit">Register</button>
        </form>
    </>)
}