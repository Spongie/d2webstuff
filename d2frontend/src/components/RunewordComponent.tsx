import { useEffect, useState } from "react";
import { Runeword } from "../models/Runeword";
import { ItemTypeService } from "../services/ItemTypeService";

type RunewordComponentProps = {
    runeword: Runeword
}

const RunewordComponent: React.FC<RunewordComponentProps> = (props: RunewordComponentProps) => {
    const [runwordTypeNames, setRunewordTypeNames] = useState('');

    const getRunewordTypes = async (targetTypes: number) => {
        const typeNames = await ItemTypeService.getInstance().getItemTypesFromValue(targetTypes);
        setRunewordTypeNames(typeNames);
    }

    useEffect(() => {
        getRunewordTypes(props.runeword.targetTypes);
    }, [props.runeword.targetTypes]);

    const header = props.runeword.name + '  ( ' + props.runeword.runes.map(rune => rune.name).join(' + ') + ' )';

    return <div className="card text-center">
        <div className="card-header">{header}</div>
        <div className="card-header"><b>{runwordTypeNames}</b></div>
        <div className="card-body">
            {
                props.runeword.modifiers.map(modifier => {
                    return <p className="card-text" key={'modifier_' + modifier.id}>{modifier.text}</p>
                })
            }
        </div>
    </div>
}

export default RunewordComponent;