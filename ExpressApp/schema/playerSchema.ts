import mongoose from "mongoose";

const playerSchema = new mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    wallet: {
        type: Number
    }
}, { timestamps: true });

export default playerSchema;