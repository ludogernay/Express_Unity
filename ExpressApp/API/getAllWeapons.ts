import WeaponData from "../types/weaponType";
import Weapon from "../model/weaponModel";

export async function getAllWeapons(): Promise<WeaponData[]> {
    try {
        const weapons = await Weapon.find().exec();
        if (!weapons) {
            throw new Error("No weapons found");
        }else{
            console.log(weapons);
        }
        return weapons;
    } catch (e) {
        throw new Error(`Error getting weapons: ${e}`);

    }
}