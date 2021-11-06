import { ItemType } from "../models/Runeword";
import HttpCommon from "../util/httpcommon";

export class ItemTypeService {
    private static instance: ItemTypeService;

    private constructor(private itemTypes: Array<ItemType> | undefined) {

    }

    public static getInstance(): ItemTypeService {
        if (!this.instance) {
            this.instance = new ItemTypeService(undefined);
        }

        return this.instance;
    }

    async getItemTypes(): Promise<Array<ItemType>> {
        if (this.itemTypes !== undefined) {
            return this.itemTypes;
        }

        const response = await HttpCommon.get<Array<ItemType>>('/ItemTypes');
        this.itemTypes = response;

        return this.itemTypes;
    }

    async getItemTypesFromValue(value: number): Promise<string> {
        const itemTypes = await this.getItemTypes();

        return itemTypes.filter(itemType => (itemType.value & value) > 0).map(itemType => itemType.name).join(' & ');
    }
}