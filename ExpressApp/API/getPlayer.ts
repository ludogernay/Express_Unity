import {ObjectId} from "mongodb";
import PlayerData from "../types/playerType";
import Player from "../model/playerModel";

export async function getPlayer(id: ObjectId): Promise<PlayerData> {
    try {
        const player = await Player.findById(id).exec()
        if (!player) {
            throw new Error("Player not found");
        }
        console.log(player)
        return player;
    } catch (error) {
        console.error("Error fetching player:", error);
        throw error;
    }
}