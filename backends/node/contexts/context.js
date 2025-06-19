const { PrismaClient } = require('@prisma/client');
const context = new PrismaClient();
module.exports = context;