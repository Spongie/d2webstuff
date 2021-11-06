import React, { useEffect } from "react";
import { useState } from "react";
import { Rune } from "../models/Rune";
import Httpcommon from "../util/httpcommon";
import { Runeword, ItemType, Modifier } from "../models/Runeword";
import { toast } from 'react-toastify';

const AdminComponent: React.FC = () => {
  const [runewordName, setRunewordName] = useState<string>('');
  const [runewordRunes, setRunewordRunes] = useState<string>('');
  const [requiredLevel, setRequiredLevel] = useState<number>(0);
  const [runewordModifiers, setRunewordModifiers] = useState<string>('');
  const [runewordItemTypes, setRunewordItemTypes] = useState<ItemType[]>(new Array<ItemType>());

  useEffect(() => {
    fetchItemTypes();
  }, []);

  const onRunewordSave = async () => {
    let value = 0;

    for (const itemType of runewordItemTypes.filter(x => x.selected)) {
      value = value | itemType.value;
    }

    let runeword: Runeword = {
      id: 0,
      requiredLevel: requiredLevel,
      name: runewordName,
      modifiers: runewordModifiers.split("\n").map(m => { return { text: m } as Modifier }),
      runes: runewordRunes.split(' ').map(x => { return { name: x } as Rune }),
      targetTypes: value
    };

    console.log(runeword);

    try {
      const response: Runeword = await Httpcommon.post('/Runewords', runeword);
      console.log(response);
      toast('Saved Successfully', { autoClose: 5000, pauseOnHover: true, hideProgressBar: false, type: toast.TYPE.SUCCESS });
    }
    catch (error) {
      console.error(error);
      toast('Error while saving', { autoClose: 5000, pauseOnHover: true, hideProgressBar: false, type: toast.TYPE.ERROR });
    }
  }

  const fetchItemTypes = async () => {
    const response = await Httpcommon.get<Array<ItemType>>('/ItemTypes');

    for (let itemType of response) {
      itemType.selected = false;
    }

    console.log(response);

    setRunewordItemTypes(response);
  }

  const onItemTypeCheckboxChanged = (e: React.SyntheticEvent<HTMLInputElement>) => {
    const changedIndex = parseInt((e.target as Element).id.replace('ItemTypeRadio_', ''));

    setRunewordItemTypes(runewordItemTypes.map((itemType, index) => {
      if (index === changedIndex) {
        itemType.selected = !itemType.selected;
      }

      return itemType;
    }));
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
              <button className="btn btn-primary" type="button" onClick={() => { onRunewordSave() }}>Save</button>
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
