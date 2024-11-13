import express, { Request, Response } from 'express';

const app = express();
const port = 3000;

app.get('/weapons', (req: Request, res: Response) => {
    res.send('Hello, TypeScript avec Express!');
});

app.listen(port, () => {
    console.log(`Serveur démarré sur http://localhost:${port}`);
});
