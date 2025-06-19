const express = require('express');
const cors = require('cors');
const fs = require('fs');
const path = require('path');
const YAML = require('yamljs');
const swaggerUi = require('swagger-ui-express');
const { execSync } = require('child_process');

const app = express();
const port = process.env.PORT || 4000;
const envPath = path.resolve(__dirname, '.env');

const isDocker = () => process.env.DOCKER === 'true';

function writeEnvFile() {
  const dbPath = isDocker()
    ? process.env.DATABASE_PATH
    : 'databases/todoverse.db';

  const content = `DATABASE_URL="file:${dbPath}"\n`;

  fs.writeFileSync(envPath, content);
  console.log(`✔ .env sobrescrito com DATABASE_URL para ${dbPath}`);
}

function setupPrisma() {
  console.log('⚙️ Gerando Prisma Client...');
  execSync('npx prisma generate', { stdio: 'inherit' });

  if (!isDocker()) {
    try {
      console.log('📦 Sincronizando estrutura do banco com prisma db push...');
      execSync('npx prisma db push', { stdio: 'inherit' });
    } catch (err) {
      console.error('❌ Erro ao rodar prisma db push:', err);
    }
  }
}

const swaggerDocument = YAML.load(
  isDocker() ? process.env.OPENAPI_PATH : path.join(__dirname, '../../shared/schema/openapi.yaml')
);

writeEnvFile();
setupPrisma();

app.use(cors());
app.use(express.json());
app.use('/swagger', swaggerUi.serve, swaggerUi.setup(swaggerDocument));

const todosController = require('./controllers/todos.controller');
app.use('/todos', todosController);

app.listen(port, () => {
  console.log(`✔ API rodando na porta ${port}`);
  console.log(`✔ Documentação disponível em http://localhost:${port}/swagger`);
});
