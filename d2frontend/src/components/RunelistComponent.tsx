import React, { useEffect } from "react";
import { useState } from "react";
import { Rune } from "../models/Rune";
import HttpCommon from "../util/Httpcommon";

interface RunelistProps {
    chunksize: number,
    onSelectedRuneChanged: (selectedRunes: Array<Rune>) => void
}

const RunelistComponent: React.FC<RunelistProps> = (props: RunelistProps) => {
    const [runes, setRunes] = useState<Array<Rune>>([]);

    useEffect(() => {
        fetchRunes();
    }, []);

    const fetchRunes = async () => {
        const response = await HttpCommon.get<Array<Rune>>('/Runes');

        for (let rune of response) {
            rune.selected = false;
        }

        setRunes(response);
    }

    let chunks: Array<Array<Rune>> = [];

    for (let i = 0; i < runes.length; i += props.chunksize) {
        const chunk = runes.slice(i, i + props.chunksize);
        chunks.push(chunk);
    }

    const onRuneClick = (runeId: number) => {
        const runeIndex = runes.findIndex((rune) => rune.id === runeId);

        if (runeIndex === -1) {
            return;
        }

        setRunes(runes.map((rune, index) => {
            if (index === runeIndex) {
                rune.selected = !rune.selected;
            }
            return rune;
        }));

        if (props.onSelectedRuneChanged !== undefined) {
            props.onSelectedRuneChanged(runes.filter((rune) => rune.selected));
        }

    }

    return (
        <div>
            {
                chunks.map((_, rowIndex) => {

                    return <div className="row" key={'runeRow_' + rowIndex}>
                        {
                            chunks[rowIndex].map((rune, runeIndex) => {
                                const color = rune.selected ? 'bg-success' : '';

                                return <div className="col text-center" key={runeIndex} onClick={() => { onRuneClick(rune.id) }}>
                                    <img src={rune.imagePath} className={color} width="64" height="64" alt={rune.name} />
                                    <h2>{rune.name}</h2>
                                </div>
                            })
                        }
                    </div>
                })
            }
        </div>
    );
}

export default RunelistComponent;