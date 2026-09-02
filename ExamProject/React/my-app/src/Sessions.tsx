import { useMutation, useQuery } from "@tanstack/react-query";
import { startTransition, useEffect, useOptimistic, useState, useTransition } from "react";
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
    const [isAdding, setIsAdding] = useState(false);
    const [newSession, setNewSession] = useState<Omit<Session, "id" | "createdAt">>({language: "", category: ""});
    const [isPending, startTransition] = useTransition();

    const getSessionsQuery = useQuery({
        queryKey: ['sessions'], 
        queryFn: async () => {
            const response = await apiFetch("/learningsession");
            const data = await response.json();
            if(!response.ok){
                throw new Error(data.message);
            }
            return data as Array<Session>;
        }
    });

    useEffect(() =>{
        if(getSessionsQuery.data){
            setSessionList(getSessionsQuery.data);
        }
    }, [getSessionsQuery.data]);

    
    const addSessionMutation = useMutation({
        mutationFn: addSession,
        onSuccess: (data) => setSessionList(prev => [...prev, data as Session]),
        onError: (error) => alert(error.message)
    });

    // const [isPending, error, data] = useQuery({
    //     queryKey: ['sessionList'],
    //     queryFn: () => fetch("/learningsession").then(res => res.json())
    // });
    async function addSession(newSession: Omit<Session, "id" | "createdAt">){
        const response = await apiFetch("/learningsession", {
            method: "POST",
            body: JSON.stringify(newSession)
        });

        const data = await response.json();

        if(!response.ok){
            throw new Error(data.message);
        }

        return data;
    }

    function handleAdd(newSession: Omit<Session, "id" | "createdAt">){
        startTransition(() => {
            setOptimisticSessionList(prev => [...prev, {...newSession, createdAt: new Date().toISOString(), id: crypto.randomUUID()}]);
            addSessionMutation.mutate(newSession);
        });
    }

    return (
        <section className="sessions area">
            <h2>Learning sessions:</h2>
            <button onClick={() => setIsAdding(true)}>Add</button>
            <hr />
            { isAdding 
            ? <form onSubmit={(e) => {
                e.preventDefault();
                handleAdd(newSession);
                setIsAdding(false);
            }}>

                <label>Category</label>
                <input type="text" value={newSession.category} onChange={(e) => setNewSession(s => ({...s, category: e.target.value}))} />

                <label>Language</label>
                <input type="text" value={newSession.language} onChange={(e) => setNewSession(s => ({...s, language: e.target.value}))} />

                <button type="submit" disabled={addSessionMutation.isPending}>Confirm</button>
                <button type="button" onClick={() => setIsAdding(false)}>Close</button>
                
            </form> 
            : <>
            <ul>{[...optimisticSessionList]
                .sort((a, b) => new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime())
                .map(s => 
                    <SessionCard session={s} setSessionList={setSessionList} setOptimisticSessionList={setOptimisticSessionList}/>
            )}</ul></>}
        </section>
    );
}

function SessionCard({ session, setSessionList, setOptimisticSessionList } : SessionCardProps){
    const sessionCreatedAt = new Date(session.createdAt);
    const [isPending, startTransition] = useTransition();

    const deleteSessionMutation = useMutation({
        mutationFn: deleteSession,
        onSuccess: (sessionId) => { setSessionList(prev => prev.filter(s => s.id !== sessionId)) },
        onError: error => alert(error.message)
    });

    async function deleteSession(sessionId: string){
            const response = await apiFetch(`/learningsession/${sessionId}`, {
                method: "DELETE"
            });
            

            if(!response.ok){
                const data = await response.json();
                throw new Error(data.message);
            }

            return sessionId;
    }
    function handleDelete(sessionId: string){
        startTransition(() => {
            setOptimisticSessionList(prev => prev.filter(s => s.id !== sessionId));
            deleteSessionMutation.mutate(sessionId);
        });

    }

    return(
        <li>
            <div className="session card">
                <span className="id">{session.id}</span>
                <span className="date">{sessionCreatedAt.getDate()}.{sessionCreatedAt.getMonth() + 1}.{sessionCreatedAt.getFullYear()}</span>
                {session.language && <span className="language">{session.language}</span>}
                {session.category && <span className="category">{session.category}</span>}
                <button onClick={() => handleDelete(session.id)} disabled={deleteSessionMutation.isPending}>Delete</button>
            </div>
        </li>
    );
}