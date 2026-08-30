import { useContext } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import { UserContext } from "./UserContext";


export function Header(){

    const user = useContext(UserContext);
    const navigate = useNavigate();
    return (<>
        <header>
            <div>
                {user ? (`Добро пожаловать, ${user.name}`) 
                : (<>
                    <button onClick={() => navigate("/register")}>Register</button>
                    <button onClick={() => navigate("/login")}>Login</button>
                </>)}
            </div>
        </header>
        <main>
            <Outlet />
        </main>
    </>
    )
}