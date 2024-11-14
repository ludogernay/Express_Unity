import Weapon from "../model/weaponModel";
import WeaponData from "../types/weaponType";

export async function createWeapon(weapon: WeaponData): Promise<WeaponData> {
    try {
        const newWeapon = new Weapon(weapon);
        const savedWeapon = await newWeapon.save();
        console.log("Weapon saved successfully:", savedWeapon);
        return weapon;
    } catch (e) {
        throw new Error(`Error creating weapon: ${e}`);
    }
}