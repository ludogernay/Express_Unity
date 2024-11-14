import {ObjectId} from "mongodb";

export default interface PlayerData {
    name: string,
    wallet: ObjectId[]
}