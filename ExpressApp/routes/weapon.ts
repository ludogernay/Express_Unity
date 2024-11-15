import {Router} from "express";
import {Request, Response} from "express";
import {getAllWeapons} from "../API/getAllWeapons";
import {getWeapon} from "../API/getWeapon";
import {createWeapon} from "../API/createWeapon";
import {updateWeapon} from "../API/updateWeapon";
import {deleteWeapon} from "../API/deleteWeapon";
import {getPlayer} from "../API/getPlayer";
import {updatePlayer} from "../API/updatePlayer";
import mongoose from "mongoose";
import {getWeaponLinks} from "../API/getWeaponLinks";

const router = Router();

router.get('/api/weapons', async (req : Request, res : Response) => {
    try {
        const weapons = await getAllWeapons();
        res.status(200).json(weapons);
    } catch (error : any) {
        res.status(500).json({ message: error.message });
    }
});

router.get('/api/weapons/search', async (req : Request, res : Response) : Promise<any | Record<string, any>> => {
    try{
        const { category } = req.query;
        if (!category) {
            return res.status(400).json({ message: "Category is required" });
        }
        const weapons = await getAllWeapons();
        const filteredWeapons = weapons.filter((weapon: any) => weapon.category === category);

        if (filteredWeapons.length === 0) {
            return res.status(404).json({ message: "No weapons found for this category" });
        }

        res.status(200).json(filteredWeapons);
        

    } catch (error : any) {
        res.status(500).json({ message: error.message });
    }



});

router.get('/api/weapons/:id', async (req: Request, res: Response): Promise<any | Record<string, any>> => {
    try {
        const weaponId = new mongoose.Types.ObjectId(req.params.id);
        const weapon = await getWeapon(weaponId);

        if (!weapon) {
            return res.status(404).json({ message: "Weapon not found" });
        }

        // Récupère les liens vers l'arme précédente et suivante
        const links = await getWeaponLinks(weaponId);

        res.status(200).json({
            weapon,
            links: {
                previous: `/api/weapons/${links.previous}`,
                next: `/api/weapons/${links.next}`
            }
        });
    } catch (error: any) {
        res.status(500).json({ message: error.message });
    }
});

router.post('/api/weapons', async (req : Request, res : Response) => {
    try {
        const weapon = await createWeapon(req.body);
        res.status(204).json(weapon);
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
        res.status(204).json(weapon);
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

router.patch('/api/players/:id', async (req : Request, res : Response) : Promise<any | Record<string, any>> => {
    try {
        const { name, wallet } = req.body;
        if (!name && !wallet) {
            return res.status(400).json({ message: "At least one field must be filled" });
        }
        const player = await updatePlayer(req.params.id, req.body);
        if (!player) {
            return res.status(404).json({ message: "Player not found" });
        }
        res.status(201).json(player);
        return player;
    } catch (error : any) {
        res.status(500).json({ message: error.message });
    }
});

router.get('/api/players/:id', async (req: Request, res: Response): Promise<any | Record<string, any>> => {
    try {
        const playerId = new mongoose.Types.ObjectId(req.params.id);
        const player = await getPlayer(playerId);

        if (!player) {
            return res.status(404).json({ message: "Player not found" });
        }
        res.status(200).json({
            player
        });
    } catch (error: any) {
        res.status(500).json({ message: error.message });
    }
});

export default router;