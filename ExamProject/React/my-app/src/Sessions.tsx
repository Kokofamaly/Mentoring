import { useQuery } from "@tanstack/react-query";
import { useOptimistic, useState } from "react";
import "./Sessions.css";

interface Session{
    id: string,
    createdAt: string,
    language?: string,
    category?: string

}

interface SessionCardProps{
    session: Session
}

export function Sessions(){
    const [sessionList, setSessionList] = useState<Array<Session>>([]);
    const [optimisticSessionList, setOptimisticSessionList] = useOptimistic(sessionList);

    // const [isPending, error, data] = useQuery({
    //     queryKey: ['sessionList'],
    //     queryFn: () => fetch("/learningsession").then(res => res.json())
    // });

    function handleAdd(newSession: Session){}

    return (
        <div className="sessions area">
            <ul>{[...optimisticSessionList]
                .sort((a, b) => new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime())
                .map(s => 
                    <SessionCard session={s}/>
            )}</ul>
        </div>
    );
}

function SessionCard({ session } : SessionCardProps){
    const sessionCreatedAt = new Date(session.createdAt);

    function handleDelete(sessionId: string){}

    return(
        <li className="session card">
            <span className="id">{session.id}</span>
            <span className="date">{sessionCreatedAt.getDate()}.{sessionCreatedAt.getMonth() + 1}.{sessionCreatedAt.getFullYear()}</span>
            {session.language && <span className="language">{session.language}</span>}
            {session.category && <span className="category">{session.category}</span>}
        </li>
    );
}