import express, { Request, Response } from 'express';
import dotenv from 'dotenv';
import { connectDB } from './connect_db';
import router from './routes/weapon';


connectDB(process.env.MONGODB_URL!).then();
const app = express();
const port = 3000;
app.use(express.json());
app.use(router);
app.use((err: any, req: Request, res: Response) => {
    console.error(err.stack);
    res.status(500).send({ message: 'Something went wrong!' });
});


app.listen(port, () => {
    console.log(`Serveur démarré sur http://localhost:${port}`);
});

