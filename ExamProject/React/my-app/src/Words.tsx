import { useDeferredValue, useOptimistic, useState } from "react";


export function Words(){
    const [wordList, setWordList] = useState([]);
    const [optimisticWordList, setOptimisticWordList] = useOptimistic(wordList);
    const [searchWord, setSearchWord] = useState<string>();
    const [selectedWord, setSelectedWord] = useState(null);
    const filteredWordList = wordList.filter(w => w.word.toLowerCase().startsWith(searchWord.toLowerCase()));
    return(<div>
        <button>Add word</button>
        <input type="text" placeholder="search..." value={searchWord} onChange={e => {setSearchWord(e.target.value); }}/>
        <ul>
            {searchWord ? filteredWordList.map(w => <li>w.word</li>) : optimisticWordList.map(w => <li>w.word</li>)}
        </ul>

    </div>);
}