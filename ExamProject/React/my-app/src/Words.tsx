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

    const selectedWord = optimisticWordList.find(w => w.id === selectedWordId);
    const filteredWordList: Array<Word> = wordList.filter(w => w.word.toLowerCase().startsWith(searchWord.toLowerCase()));
    
    function handleEdit(wordId: string, updatedWord: Word){}
    function handleDelete(wordId: string){}
    function handleAdd(newWord: Word){}

    return(
        <div className="words area">
            <button>Add word</button>
            <input type="text" placeholder="search..." value={searchWord} onChange={e => setSearchWord(e.target.value)}/>
            <ul>
                {searchWord 
                ? filteredWordList.map(w => <li onClick={() => setSelectedWordId(w.id)}>{w.word}</li>) 
                : optimisticWordList.map(w => selectedWord === w 
                    ? <li className="wordcard selected" onClick={() => setSelectedWordId(null)}>
                            <span>{w.word}</span>
                            <span>{w.translation}</span>
                            <button>Edit</button>
                            <button>Delete</button>
                        </li>
                    : <li className="wordcard" onClick={() => setSelectedWordId(w.id)}>{w.word}</li>)}
            </ul>

        </div>);
}