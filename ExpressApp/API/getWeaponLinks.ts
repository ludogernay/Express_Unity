import mongoose, { ObjectId } from 'mongoose';
import Weapon from '../model/weaponModel'; // Modèle de votre arme

export async function getWeaponLinks(currentWeaponId: mongoose.Types.ObjectId) {
    const weapons = await Weapon.find().sort({ _id: 1 }).exec(); // Tri par ordre croissant de `_id`
    const currentIndex = weapons.findIndex(weapon => weapon._id.equals(currentWeaponId));

    if (currentIndex === -1) {
        throw new Error("Weapon not found");
    }

    const previousIndex = (currentIndex - 1 + weapons.length) % weapons.length;
    const nextIndex = (currentIndex + 1) % weapons.length;

    return {
        previous: weapons[previousIndex]?._id.toString(), // Convertit en chaîne
        next: weapons[nextIndex]?._id.toString()
    };
}
