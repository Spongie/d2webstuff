import { Runeword } from "../models/Runeword";

type RunewordComponentProps = {
    runeword: Runeword
}

const RunewordComponent: React.FC<RunewordComponentProps> = (props: RunewordComponentProps) => {

    return <div>
        <h1>{props.runeword.name}</h1>
    </div>
}

export default RunewordComponent;