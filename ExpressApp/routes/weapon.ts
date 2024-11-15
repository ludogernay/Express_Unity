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
/**
 * @swagger
 * tags:
 *   name: Weapons
 *   description: API endpoints for managing weapons
 */

/**
 * @swagger
 * components:
 *   schemas:
 *     Weapon:
 *       type: object
 *       required:
 *         - name
 *         - category
 *         - price
 *       properties:
 *         _id:
 *           type: string
 *           description: Unique identifier for the weapon
 *           example: '5f8d0d55b54764421b7156c2'
 *         name:
 *           type: string
 *           description: Name of the weapon
 *           example: 'Excalibur'
 *         category:
 *           type: string
 *           description: Category of the weapon
 *           example: 'Sword'
 *         price:
 *           type: number
 *           description: Price of the weapon
 *           example: 1000
 */

/**
 * @swagger
 * /api/weapons:
 *   get:
 *     summary: Retrieve all weapons
 *     description: Retrieve a list of all weapons.
 *     tags: [Weapons]
 *     responses:
 *       200:
 *         description: A list of weapons.
 *         content:
 *           application/json:
 *             schema:
 *               type: array
 *               items:
 *                 $ref: '#/components/schemas/Weapon'
 *       500:
 *         description: Internal server error
 */


router.get('/api/weapons', async (req : Request, res : Response) => {
    try {
        const weapons = await getAllWeapons();
        res.status(200).json(weapons);
    } catch (error : any) {
        res.status(500).json({ message: error.message });
    }
});

/**
 * @swagger
 * /api/weapons/search:
 *   get:
 *     summary: Search weapons by category
 *     description: Retrieve a list of weapons filtered by category.
 *     tags: [Weapons]
 *     parameters:
 *       - in: query
 *         name: category
 *         schema:
 *           type: string
 *         required: true
 *         description: Category to filter weapons by
 *     responses:
 *       200:
 *         description: A list of weapons matching the category.
 *         content:
 *           application/json:
 *             schema:
 *               type: array
 *               items:
 *                 $ref: '#/components/schemas/Weapon'
 *       400:
 *         description: Category is required
 *       404:
 *         description: No weapons found for this category
 *       500:
 *         description: Internal server error
 */

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
/**
 * @swagger
 * /api/weapons/{id}:
 *   get:
 *     summary: Get a weapon by ID
 *     description: Retrieve a single weapon by its ID.
 *     tags: [Weapons]
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The weapon ID
 *     responses:
 *       200:
 *         description: A weapon object along with navigation links.
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 weapon:
 *                   $ref: '#/components/schemas/Weapon'
 *                 links:
 *                   type: object
 *                   properties:
 *                     previous:
 *                       type: string
 *                       description: Link to the previous weapon
 *                       example: '/api/weapons/5f8d0d55b54764421b7156c1'
 *                     next:
 *                       type: string
 *                       description: Link to the next weapon
 *                       example: '/api/weapons/5f8d0d55b54764421b7156c3'
 *       404:
 *         description: Weapon not found
 *       500:
 *         description: Internal server error
 */

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

/**
 * @swagger
 * /api/weapons:
 *   post:
 *     summary: Create a new weapon
 *     description: Create a new weapon with the provided data.
 *     tags: [Weapons]
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/Weapon'
 *     responses:
 *       201:
 *         description: Weapon created successfully
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/Weapon'
 *       500:
 *         description: Internal server error
 */

router.post('/api/weapons', async (req : Request, res : Response) => {
    try {
        const weapon = await createWeapon(req.body);
        res.status(201).json(weapon);
    } catch (error : any) {
        res.status(500).json({ message: error.message });
    }
});

/**
 * @swagger
 * /api/weapons/{id}:
 *   patch:
 *     summary: Update a weapon
 *     description: Update a weapon's information.
 *     tags: [Weapons]
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The weapon ID
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             description: Fields to update
 *             properties:
 *               name:
 *                 type: string
 *               category:
 *                 type: string
 *               price:
 *                 type: number
 *     responses:
 *       200:
 *         description: Weapon updated successfully
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/Weapon'
 *       400:
 *         description: At least one field must be filled
 *       404:
 *         description: Weapon not found
 *       500:
 *         description: Internal server error
 */
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
        res.status(201).json(weapon);
        return weapon;
    } catch (error : any) {
        res.status(500).json({ message: error.message });
    }
});
/**
 * @swagger
 * /api/weapons/{id}:
 *   delete:
 *     summary: Delete a weapon
 *     description: Delete a weapon by its ID.
 *     tags: [Weapons]
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The weapon ID
 *     responses:
 *       204:
 *         description: Weapon deleted successfully
 *       400:
 *         description: Invalid weapon ID
 *       404:
 *         description: Weapon not found
 *       500:
 *         description: Internal server error
 */
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

/**
 * @swagger
 * tags:
 *   name: Players
 *   description: API endpoints for managing players
 */

/**
 * @swagger
 * components:
 *   schemas:
 *     Player:
 *       type: object
 *       required:
 *         - name
 *         - wallet
 *       properties:
 *         _id:
 *           type: string
 *           description: Unique identifier for the player
 *           example: '5f8d0d55b54764421b7156c2'
 *         name:
 *           type: string
 *           description: Name of the player
 *           example: 'John Doe'
 *         wallet:
 *           type: number
 *           description: Player's wallet balance
 *           example: 1500
 *     ErrorResponse:
 *       type: object
 *       properties:
 *         message:
 *           type: string
 *           description: Error message
 *           example: 'Player not found'
 */

/**
 * @swagger
 * /api/players/{id}:
 *   patch:
 *     summary: Partially update a player
 *     description: Update one or more fields of a player.
 *     tags: [Players]
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The player ID
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             description: Fields to update
 *             properties:
 *               name:
 *                 type: string
 *                 description: Name of the player
 *                 example: 'Jane Smith'
 *               wallet:
 *                 type: number
 *                 description: Player's wallet balance
 *                 example: 2000
 *     responses:
 *       201:
 *         description: Player updated successfully
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/Player'
 *       400:
 *         description: At least one field must be filled
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/ErrorResponse'
 *       404:
 *         description: Player not found
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/ErrorResponse'
 *       500:
 *         description: Internal server error
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/ErrorResponse'
 */
router.patch('/api/players/:id', async (req: Request, res: Response): Promise<any | Record<string, any>> => {
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
    } catch (error: any) {
        res.status(500).json({ message: error.message });
    }
});

/**
 * @swagger
 * /api/players/{id}:
 *   get:
 *     summary: Retrieve a player by ID
 *     description: Retrieve a player's details by their ID.
 *     tags: [Players]
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         schema:
 *           type: string
 *         description: The player ID
 *     responses:
 *       200:
 *         description: A player object
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 player:
 *                   $ref: '#/components/schemas/Player'
 *       404:
 *         description: Player not found
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/ErrorResponse'
 *       500:
 *         description: Internal server error
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/ErrorResponse'
 */
router.get('/api/players/:id', async (req: Request, res: Response): Promise<any | Record<string, any>> => {
    try {
        const playerId = new mongoose.Types.ObjectId(req.params.id);
        const player = await getPlayer(playerId);

        if (!player) {
            return res.status(404).json({ message: "Player not found" });
        }
        res.status(200).json({
            player,
        });
    } catch (error: any) {
        res.status(500).json({ message: error.message });
    }
});

export default router;