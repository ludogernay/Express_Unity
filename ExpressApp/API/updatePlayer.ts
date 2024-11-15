import Player from "../model/playerModel"; // Modèle joueur
import PlayerData from "../types/playerType";
import {ObjectId} from "mongodb"; // Interface PlayerData

// Mise à jour du portefeuille d'un joueur
export async function updatePlayer(id:string,player: PlayerData) {
    try {
        const query = { _id: new ObjectId(id) }; // Recherche par nom
        // Mise à jour du joueur
        const updatedPlayer = await Player.findOneAndUpdate(query,player, { new: true }).exec();

        if (!updatedPlayer) {
            throw new Error("Player not found");
        }

        console.log("Wallet updated successfully", updatedPlayer);
        return updatedPlayer;
    } catch (error) {
        console.error("Error updating wallet:", error);
        throw error;
    }
}
