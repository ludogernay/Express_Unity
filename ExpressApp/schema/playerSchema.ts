import mongoose from "mongoose";
import {ObjectId} from "mongodb";

const playerSchema = new mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    wallet: {
        type: [mongoose.Schema.Types.ObjectId]
    }
}, { timestamps: true });

export default mongoose.model("Player", playerSchema);