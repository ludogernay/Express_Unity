import mongoose from 'mongoose';
import playerSchema from "../schema/playerSchema";

const playerModel = mongoose.model('Player', playerSchema, 'Players');

export default playerModel;
