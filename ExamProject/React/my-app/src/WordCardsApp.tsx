import { useQuery } from "@tanstack/react-query";
import { Sessions } from "./Sessions";
import { Words } from "./Words";


export function WordCardsApp(){
    return(<>
            <Sessions />
            <Words />
        </>
    );
}