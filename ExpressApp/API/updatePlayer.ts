import Player from "../model/playerModel"; // Modèle joueur
import PlayerData from "../types/playerType"; // Interface PlayerData

// Mise à jour du portefeuille d'un joueur
export async function updatePlayer(id:string,playerData: PlayerData) {
    try {
        const query = { name: playerData.name }; // Recherche par nom
        const update = { wallet: playerData.wallet }; // Mise à jour du portefeuille

        // Mise à jour du joueur
        const updatedPlayer = await Player.findOneAndUpdate(query, update, { new: true }).exec();

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
