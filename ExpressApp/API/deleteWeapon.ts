import Weapon from "../model/weaponModel";
import WeaponData from "../types/weaponType";
import {ObjectId} from "mongodb";

export async function deleteWeapon(id: string): Promise<WeaponData> {
    try {
        const query = { _id: new ObjectId(id) };
        const deletedWeapon = await Weapon.findByIdAndDelete(query).exec();
        if (!deletedWeapon) {
            throw new Error("Weapon not found");
        }
        console.log("Weapon deleted successfully", deletedWeapon);
        return deletedWeapon;
    } catch(error) {
        console.error("Error deleting weapon:", error);
        throw error;
    }
}