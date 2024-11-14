import Weapon from "../model/weaponModel";
import WeaponData from "../types/weaponType";
import {ObjectId} from "mongodb";

export async function getWeapon(id: string): Promise<WeaponData> {
    try {
        const query = { _id: new ObjectId(id) };
        const weapon = await Weapon.findById(query).exec();
        if (!weapon) {
            throw new Error("Weapon not found");
        } else {
            console.log(weapon);
        }
        return weapon;
    } catch (error) {
        console.error("Error fetching weapon:", error);
        throw error;
    }
}