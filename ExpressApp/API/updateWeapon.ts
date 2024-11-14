import Weapon from "../model/weaponModel";
import WeaponData from "../types/weaponType";
import {ObjectId} from "mongodb";

export async function updateWeapon(id: string, weapon: WeaponData): Promise<WeaponData> {
    try {
        const query = { _id: new ObjectId(id) };
        const updatedWeapon = await Weapon.findByIdAndUpdate(query, weapon, { new: true }).exec();
        if (!updatedWeapon) {
            throw new Error("Weapon not found");
        }
        console.log("Weapon updated successfully", updatedWeapon);
        return updatedWeapon;
    } catch(error) {
        console.error("Error updating weapon:", error);
        throw error;
    }
}