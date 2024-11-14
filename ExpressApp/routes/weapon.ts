import {Router} from "express";
import {Request, Response} from "express";
import {getAllWeapons} from "../API/getAllWeapons";
import {getWeapon} from "../API/getWeapon";
import {createWeapon} from "../API/createWeapon";
import {updateWeapon} from "../API/updateWeapon";
import {deleteWeapon} from "../API/deleteWeapon";
import mongoose from "mongoose";

const router = Router();

router.get('/api/weapons', async (req : Request, res : Response) => {
    try {
        const weapons = await getAllWeapons();
        res.status(200).json(weapons);
    } catch (error : any) {
        res.status(500).json({ message: error.message });
    }
});

router.get('/api/weapons/:id', async (req : Request, res : Response) : Promise<any | Record<string, any>> => {
    try {
        const weapon = await getWeapon(req.params.id);
        if (!weapon) {
            return res.status(404).json({ message: "Weapon not found" });
        }
        res.status(200).json(weapon);
    } catch (error : any) {
        res.status(500).json({ message: error.message });
    }
});

router.post('/api/weapons', async (req : Request, res : Response) => {
    try {
        const weapon = await createWeapon(req.body);
        res.status(201).json(weapon);
    } catch (error : any) {
        res.status(500).json({ message: error.message });
    }
});

router.patch('/api/weapons/:id', async (req : Request, res : Response) : Promise<any | Record<string, any>> => {
    try {
        const { name, category, price } = req.body;
        if (!name && !category && !price) {
            return res.status(400).json({ message: "At least one field must be filled" });
        }
        const weapon = await updateWeapon(req.params.id, req.body);
        if (!weapon) {
            return res.status(404).json({ message: "Weapon not found" });
        }
        res.status(200).json(weapon);
        return weapon;
    } catch (error : any) {
        res.status(500).json({ message: error.message });
    }
});

router.delete('/api/weapons/:id', async (req : Request, res : Response) : Promise<any | Record<string, any>> => {
    try {
        const weaponId : string = req.params.id;
        if (!mongoose.Types.ObjectId.isValid(weaponId)) {
            return res.status(400).json({ message: "Invalid weapon ID" });
        }
        const weapon = await deleteWeapon(req.params.id);
        if (!weapon) {
            return res.status(404).json({ message: "Weapon not found" });
        }
        res.status(204).json(weapon);
    } catch (error : any) {
        res.status(500).json({ message: error.message });
    }
});


export default router;