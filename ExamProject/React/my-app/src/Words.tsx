import { useDeferredValue, useOptimistic, useState } from "react";
import "./Words.css";

interface Word{
    id: string,
    word: string,
    translation: string,
    language: string,
    category?: string,
    usageExample?: string
}


export function Words(){
    const [wordList, setWordList] = useState<Array<Word>>([]);
    const [optimisticWordList, setOptimisticWordList] = useOptimistic(wordList);
    const [searchWord, setSearchWord] = useState<string>("");
    const [selectedWordId, setSelectedWordId] = useState<string | null>(null);
    const [mode, setMode] = useState<"editing" | "adding" | null>(null);
    const [editedWord, setEditedWord] = useState<Word | null>(null);

    const selectedWord = optimisticWordList.find(w => w.id === selectedWordId);
    const filteredWordList: Array<Word> = wordList.filter(w => w.word.toLowerCase().startsWith(searchWord.toLowerCase()));
    
    function handleEdit(wordId: string, updatedWord: Word){
        setMode(null);
        setEditedWord(null);
    }
    function handleDelete(wordId: string){

    }


    return(
        <div className="words area">
            { mode === "adding" 
            ? <AddForm setMode={setMode}/> 
            :
            <>
                <button onClick={() => setMode("adding")}>Add word</button>
                <input type="text" placeholder="search..." value={searchWord} onChange={e => setSearchWord(e.target.value)}/>
                <ul>
                    {searchWord 
                    ? filteredWordList.map(w => <li onClick={() => setSelectedWordId(w.id)}>{w.word}</li>) 
                    : optimisticWordList.map(w => selectedWord === w 
                        ? mode === "editing" ? <li>
                                <input type="text" value={editedWord!.word} onChange={(e) => setEditedWord({...editedWord!, word: e.target.value})}/>
                                <input type="text" value={editedWord!.translation} onChange={(e) => setEditedWord({...editedWord!, translation: e.target.value})}/>
                                <input type="text" value={editedWord!.language} onChange={(e) => setEditedWord({...editedWord!, language: e.target.value})}/>
                                <input type="text" value={editedWord!.category} onChange={(e) => setEditedWord({...editedWord!, category: e.target.value})}/>
                                <input type="text" value={editedWord!.usageExample} onChange={(e) => setEditedWord({...editedWord!, usageExample: e.target.value})}/>
                                <button onClick={() => handleEdit(selectedWordId!, editedWord!)}>Confirm</button>
                                <button onClick={() => setMode(null)}>Close</button>
                            </li>
                            : <li className="wordcard selected" onClick={() => setSelectedWordId(null)}>
                                <span>{w.word}</span>
                                <span>{w.translation}</span>
                                <span>{w.language}</span>
                                {w.category && <span>{w.category}</span>}
                                {w.usageExample && <span>{w.usageExample}</span>}
                                <button onClick={() => {
                                    setMode("editing");
                                    setEditedWord(selectedWord);
                                    }}>Edit</button>
                                <button onClick={() => handleDelete(w.id)}>Delete</button>
                            </li> 
                        : <li className="wordcard" onClick={() => setSelectedWordId(w.id)}>{w.word}</li>)}
                </ul>
            </>}

        </div>);
}

function AddForm({ setMode } : {setMode: (mode: "editing" | "adding" | null) => void}){
    const [addedWord, setAddedWord] = useState<Omit<Word, "id">>({word:"", translation:"", language:"", category:"", usageExample:""});
    
    function handleAdd(newWord: Omit<Word, "id">){
        setMode(null);
    }

    return (<form onSubmit={() => handleAdd(addedWord)}>

                <label>Word</label>
                <input type="text" value={addedWord.word} onChange={e => setAddedWord({...addedWord, word: e.target.value})} required/>

                <label>Translation</label>
                <input type="text" value={addedWord.translation} onChange={e => setAddedWord({...addedWord, translation: e.target.value})} required/>

                <label>Language</label>
                <input type="text" value={addedWord.language} onChange={e => setAddedWord({...addedWord, language: e.target.value})} required/>

                <label>Category</label>
                <input type="text" value={addedWord.category} onChange={e => setAddedWord({...addedWord, category: e.target.value})} />

                <label>Usage example</label>
                <input type="text" value={addedWord.usageExample} onChange={e => setAddedWord({...addedWord, usageExample: e.target.value})} />

                <button type="submit">Confirm</button>
                <button onClick={() => setMode(null)}>Close</button>
            </form> );
}