import mongoose from 'mongoose';
import weaponSchema from "../schema/weaponSchema";

// Create the User model based on the schema
const weaponModel = mongoose.model('Weapon', weaponSchema,'Weapons');

export default weaponModel;