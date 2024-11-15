import mongoose from "mongoose";

const playerSchema = new mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    wallet: {
        type: Number,
        required : true
    }
}, { timestamps: true });

export default playerSchema;