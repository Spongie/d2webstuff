import React, { useEffect } from "react";
import { useState } from "react";
import { Rune } from "../models/Rune";
import HttpCommon from "../util/HttpCommon";

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

        const selectedRunesJson = localStorage.getItem('selected-runes');
        let preSelected = false;

        if (selectedRunesJson != null) {
            const selectedRunes = JSON.parse(selectedRunesJson) as Array<number>;

            for (const runeNumber of selectedRunes) {
                for (let rune of response) {
                    if (rune.number === runeNumber) {
                        rune.selected = true;
                        preSelected = true;
                    }
                }
            }
        }

        setRunes(response);


        if (preSelected && props.onSelectedRuneChanged !== undefined) {
            props.onSelectedRuneChanged(response.filter((rune) => rune.selected));
        }
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

        const selectedRunesList: Array<number> = runes.filter((rune) => rune.selected).map((rune) => rune.number);
        localStorage.setItem('selected-runes', JSON.stringify(selectedRunesList));

        if (props.onSelectedRuneChanged !== undefined) {
            props.onSelectedRuneChanged(runes.filter((rune) => rune.selected));
        }

    }

    return (
        <div className="row">
            {
                runes.map((rune, index) => {
                    const color = rune.selected ? 'rune-image bg-success' : 'rune-image';

                    return <div className="col text-center" key={index} onClick={() => { onRuneClick(rune.id) }}>
                        <img src={rune.imagePath} className={color} alt={rune.name} />
                        <p className="rune-text">{rune.name}</p>
                    </div>
                })
            }
        </div>
    );
}

export default RunelistComponent;