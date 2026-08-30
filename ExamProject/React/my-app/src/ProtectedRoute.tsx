import { useContext } from "react";
import { UserContext } from "./UserContext";
import { Navigate, Outlet } from "react-router-dom";


export function ProtectedRoute(){
    const user = useContext(UserContext);

    if(!user){
        return <Navigate to="login" replace/>;
    }

    return <Outlet />;
}