import React, { useEffect } from "react";
import { useState } from "react";
import { Rune } from "../models/rune";
import httpcommon from "../util/httpcommon";

interface RunelistProps {
    chunksize: number
}

const RunelistComponent: React.FC<RunelistProps> = (props: RunelistProps) => {
    const [runes, setRunes] = useState<Array<Rune>>([]);

    useEffect(() => {
        fetchRunes();
    }, []);

    const fetchRunes = async () => {
        const response = await httpcommon.get<Array<Rune>>('http://localhost:5000/Runes');

        setRunes(response.data);
    }

    let chunks: Array<Array<Rune>> = [];

    for (let i = 0; i < runes.length; i += props.chunksize) {
        const chunk = runes.slice(i, i + props.chunksize);
        chunks.push(chunk);
    }

    return (
        <div>
            {
                chunks.map((_, rowIndex) => {

                    return <div className="row" key={'runeRow_' + rowIndex}>
                        {
                            chunks[rowIndex].map((rune, runeIndex) => {
                                const imageSource = 'http://localhost:5000/' + rune.imagePath;
                                return <div className="col" key={runeIndex}>
                                    <img src={imageSource} width="64" height="64" alt={rune.name} />
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