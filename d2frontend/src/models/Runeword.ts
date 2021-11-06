import { Rune } from "./Rune";

export type Modifier = {
    id: number,
    text: string,
    runewordId: number
}

export type Runeword = {
    id: number,
    name: string,
    requiredLevel: number,
    modifiers: Array<Modifier>,
    targetTypes: number,
    runes: Array<Rune>
}

export type ItemType = {
    name: string,
    value: number,
    selected: boolean
}