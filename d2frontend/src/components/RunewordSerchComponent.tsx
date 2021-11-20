import { useState } from "react";
import { Rune } from "../models/Rune";
import { Runeword } from "../models/Runeword";
import HttpCommon from "../util/Httpcommon";
import RunelistComponent from "./RunelistComponent";
import RunewordComponent from "./RunewordComponent";

function RunewordSerchComponent() {
  const [runewords, setRunewords] = useState(new Array<Runeword>());

  const onRunesChanged = async (selectedRunes: Array<Rune>) => {
    const parameters = selectedRunes.map(rune => rune.number).join(',');
    const response = await HttpCommon.get<Array<Runeword>>('/Runewords/search?runeNumbers=' + parameters);

    setRunewords(response);
  }

  return (
    <div className="container-fluid">
      <div className="row">
        <div className="row">
          <div className="col-2 border-end border-2">
            <RunelistComponent chunksize={3} onSelectedRuneChanged={onRunesChanged} />
          </div>

          <div className="col-10">
            <div className="row">
              {
                runewords.map((runeword) => {
                  return <div className="col-3" key={'runeword_' + runeword.id}>
                    <RunewordComponent runeword={runeword} />
                  </div>
                })
              }
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default RunewordSerchComponent;
