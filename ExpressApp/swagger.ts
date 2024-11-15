// swagger.js

const swaggerJSDoc = require('swagger-jsdoc');

const options = {
    definition: {
        openapi: '3.0.0',
        info: {
            title: 'PROJET API YNOV',
            version: '1.0.0',
            description: 'application mongodb express unity',
        },
        servers: [
            {
                url: 'http://localhost:3000',
            },
        ],
        components: {
            schemas: {
                Weapon: {
                    type: 'object',
                    required: ['name', 'category', 'price'],
                    properties: {
                        _id:
                            {
                                type: 'string',
                                description: 'Unique identifier for the weapon',
                                example: '5f8d0d55b54764421b7156c2',
                            },
                        name: {
                            type: 'string',
                            description: 'Name of the weapon',
                            example: 'Excalibur',
                        },
                        category: {
                            type: 'string',
                            description: 'Category of the weapon',
                            example: 'Sword',
                        },
                        price: {
                            type: 'number',
                            description: 'Price of the weapon',
                            example: 1000,
                        },
                    },
                },
            },
        },
    },
    apis: ['./routes/*.ts'], // Adjust the path if necessary
};

const swaggerSpec = swaggerJSDoc(options);

module.exports = swaggerSpec;