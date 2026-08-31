import { useMutation, useQuery } from "@tanstack/react-query";
import { useOptimistic, useState, useTransition } from "react";
import "./Sessions.css";
import { apiFetch } from "./api/apiFetch";

interface Session{
    id: string,
    createdAt: string,
    language?: string,
    category?: string

}

interface SessionCardProps{
    session: Session,
    setSessionList: React.Dispatch<React.SetStateAction<Session[]>>,
    setOptimisticSessionList: (action: Session[] | ((pendingState: Session[]) => Session[])) => void
}

export function Sessions(){
    const [sessionList, setSessionList] = useState<Array<Session>>([]);
    const [optimisticSessionList, setOptimisticSessionList] = useOptimistic(sessionList);

    const getSessionsQuery = useQuery({queryKey: ['sessions'], queryFn:});
    
    const addSessionMutation = useMutation({
        mutationFn:,
        onSuccess: (data) => setSessionList(prev => [...prev, data as Session]),
        onError: (error) => alert(error.message)
    });

    // const [isPending, error, data] = useQuery({
    //     queryKey: ['sessionList'],
    //     queryFn: () => fetch("/learningsession").then(res => res.json())
    // });

    function handleAdd(newSession: Session){}

    return (
        <div className="sessions area">
            <button>Add</button>
            <ul>{[...optimisticSessionList]
                .sort((a, b) => new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime())
                .map(s => 
                    <SessionCard session={s} setSessionList={setSessionList} setOptimisticSessionList={setOptimisticSessionList}/>
            )}</ul>
        </div>
    );
}

function SessionCard({ session, setSessionList, setOptimisticSessionList } : SessionCardProps){
    const sessionCreatedAt = new Date(session.createdAt);
    const [isPending, startTransition] = useTransition();

    const deleteSessionMutation = useMutation({
        mutationFn: deleteSession,
        onSuccess: (_, sessionId) => { setSessionList(prev => prev.filter(s => s.id !== sessionId)) },
        onError: error => alert(error.message)
    });

    async function deleteSession(sessionId: string){
            const response = await apiFetch(`/learningsession/${sessionId}`, {
                method: "DELETE"
            });
            const data = await response.json();

            if(!response.ok){
                throw new Error(data.message);
            }

            return data;
    }
    function handleDelete(sessionId: string){
        startTransition(() => {
            setOptimisticSessionList(prev => prev.filter(s => s.id !== sessionId));
            deleteSessionMutation.mutate(sessionId);
        });

    }

    return(
        <li className="session card">
            <span className="id">{session.id}</span>
            <span className="date">{sessionCreatedAt.getDate()}.{sessionCreatedAt.getMonth() + 1}.{sessionCreatedAt.getFullYear()}</span>
            {session.language && <span className="language">{session.language}</span>}
            {session.category && <span className="category">{session.category}</span>}
            <button onClick={() => handleDelete(session.id)} disabled={deleteSessionMutation.isPending}>Delete</button>
        </li>
    );
}