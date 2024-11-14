import Weapon from "../model/weaponModel";
import WeaponData from "../types/weaponType";
import {ObjectId} from "mongodb";

export async function getWeapon(id: ObjectId): Promise<WeaponData> {
    try {
        const weapon = await Weapon.findById(id).exec();
        if (!weapon) {
            throw new Error("Weapon not found");
        }
        return weapon;
    } catch (error) {
        console.error("Error fetching weapon:", error);
        throw error;
    }
}