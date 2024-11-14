import mongoose from "mongoose";

const weaponSchema = new mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    category: {
        type: String,
        required: true
    },
    price: {
        type: Number,
        required: true
    }
} , { timestamps: true });

export default weaponSchema;