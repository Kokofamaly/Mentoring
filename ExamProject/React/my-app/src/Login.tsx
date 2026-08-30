import React, { useState } from 'react';

function Login(){
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    
    function handleLogin(e : React.SubmitEvent){
        e.preventDefault();
        
    }

    return(<>
        <form onSubmit={handleLogin}>
            <label>Email</label>
            <input type="email" placeholder="Enter your email" value={email} onChange={e => setEmail(e.target.value)}/>
            <label>Password</label>
            <input type="password" placeholder="Enter your password" value={password} onChange={e => setPassword(e.target.value)}/>
            <button type="submit">Login</button>
        </form>
    </>)
}