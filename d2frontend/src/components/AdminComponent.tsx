import React, { useEffect } from "react";
import { useState } from "react";
import { Rune } from "../models/Rune";
import Httpcommon from "../util/httpcommon";
import { Runeword, ItemType, Modifier } from "../models/Runeword";

const onRunewordSave = async (name: string, runes: string, modifiers: string, requiredLevel: number, itemTypes: Array<ItemType>) => {
  let value = 0;

  for (const itemType of itemTypes.filter(x => x.selected)) {
    value = value | itemType.value;
  }

  let runeword: Runeword = {
    id: 0,
    requiredLevel: requiredLevel,
    name: name,
    modifiers: modifiers.split("/\r?\n/").map(m => { return { text: m } as Modifier }),
    runes: runes.split(' ').map(x => { return { name: x } as Rune }),
    targetTypes: value
  };

  const response = await Httpcommon.post('/Runewords', runeword);

  if (response.status !== 200) {
    console.error(response.statusText);
  }
}

const AdminComponent: React.FC = () => {
  const [runewordName, setRunewordName] = useState<string>('');
  const [runewordRunes, setRunewordRunes] = useState<string>('');
  const [requiredLevel, setRequiredLevel] = useState<number>(0);
  const [runewordModifiers, setRunewordModifiers] = useState<string>('');
  const [runewordItemTypes, setRunewordItemTypes] = useState<ItemType[]>([]);

  useEffect(() => {
    fetchItemTypes();
  }, []);

  const fetchItemTypes = async () => {
    const response = await Httpcommon.get<Array<ItemType>>('/ItemTypes');

    console.log(response);

    setRunewordItemTypes(response);
  }

  const onItemTypeCheckboxChanged = (e: React.SyntheticEvent<HTMLInputElement>) => {
    const index = parseInt((e.target as Element).id.replace('ItemTypeRadio_', ''));

    runewordItemTypes[index].selected = !runewordItemTypes[index].selected;
    setRunewordItemTypes(runewordItemTypes);
  }

  return (
    <div className="container">
      <div className="row">
        <div className="col-4">

        </div>
        <div className="col-4">
          <div className="form-group">
            <div className="mb-3">
              <label htmlFor="runeword_name" className="form-label ">Name</label>
              <input type="text" id="runeword_name" className="form-control" value={runewordName} onChange={e => { setRunewordName(e.target.value) }} />
            </div>
            <div className="mb-3">
              <label htmlFor="runeword_runes" className="form-label">Runes</label>
              <input type="text" id="runeword_runes" className="form-control" value={runewordRunes} onChange={e => { setRunewordRunes(e.target.value) }} />
            </div>
            <div className="mb-3">
              <label htmlFor="runeword_level" className="form-label">Required Level</label>
              <input type="number" id="runeword_level" className="form-control" value={requiredLevel} onChange={e => { setRequiredLevel(parseInt(e.target.value)) }} />
            </div>
            <div className="mb-3">
              {
                runewordItemTypes.map((itemType, index) => {
                  return <div className="form-check form-check-inline" key={'ItemTypeRadioKey_' + itemType.value}>
                    <input className="form-check-input" type="checkbox" value="" id={'ItemTypeRadio_' + index} checked={itemType.selected} onChange={onItemTypeCheckboxChanged} />
                    <label className="form-check-label" htmlFor={'ItemTypeRadio_' + index}>{itemType.name}</label>
                  </div>
                })
              }
            </div>
            <div className="mb-3">
              <label htmlFor="runeword_modifiers" className="form-label">Modifiers</label>
              <textarea id="runeword_modifiers" className="form-control" rows={12} value={runewordModifiers} onChange={e => { setRunewordModifiers(e.target.value) }} />
            </div>
            <div className="col-12">
              <button className="btn btn-primary" onClick={e => { onRunewordSave(runewordName, runewordRunes, runewordModifiers, requiredLevel, runewordItemTypes) }}>Save</button>
            </div>
          </div>
        </div>
        <div className="col-4">

        </div>
      </div>
    </div>
  );
}

export default AdminComponent;
