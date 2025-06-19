# 🧠 TodoVerse

TodoVerse é um projeto de To-Do List multi-tecnologia, com múltiplas APIs em diferentes linguagens (C#, Node.js, Python, PHP, Java) e frontends em frameworks modernos (React, Vue, Angular).  

Todos os serviços utilizam um **banco de dados SQLite compartilhado**, gerenciado por container, com especificações unificadas via OpenAPI e coleção do Postman para testes.

---

## 📦 Estrutura do Projeto

```plaintext
todoverse/
├── backends/                  # APIs por linguagem
│   ├── dotnet/                # API em ASP.NET Core
│   ├── node/                  # API em Node.JS
├── fronts/                    # Frontends desacoplados
│   └── angular/
├── shared/
│   ├── databases/
│   │   ├── init.sql           # Script SQL de criação do banco
│   │   ├── Dockerfile         # Dockerfile do inicializador
│   │   └── todoverse.db       # Banco SQLite (criado por volume)
│   └── schemas/
│       ├── openapi.yaml       # Especificação da API (OpenAPI 3.0)
│       └── TodoVerse.postman_collection.json # Coleção de testes no Postman
├── .gitignore                 # Arquivos a serem ignorados pelo GIT
├── docker-compose.yml         # Orquestração dos serviços
└── README.md                  # Este arquivo
```

## 🚀 Como rodar o projeto com Docker
Todo o ecossistema do TodoVerse foi projetado para funcionar de forma orquestrada via Docker Compose.

Esse comando constrói e sobe todos os serviços necessários de forma automática:

```bash
docker compose up --build
```

### ✅ O que esse comando faz:
1 - Executa o script SQL init.sql para criar o banco SQLite (todoverse.db) com a tabela Todos, se ainda não existir.

2 - Sobe todos os demais serviços de API's e frontends

### 🛑 Para parar os serviços:
```bash
docker compose down
```

### 🔄 Para parar e remover tudo (inclusive volumes):
```bash
docker compose down -v
```

## 📂 Banco de dados
* Localizado em: shared/databases/todoverse.db

* Compartilhado por volume entre todas as APIs

* Criado automaticamente se não existir

* Script de estrutura: init.sql

## 🌐 APIs disponíveis
✅ ASP.NET Core (.NET 9 | EF Core) — pronto

✅ Node.js (Express | Prisma) — pronto

⏳ Python — em construção

⏳ PHP — em construção

⏳ Java — em construção

Todas as APIs usam o mesmo banco e seguem a especificação OpenAPI.

## 💡 Documentação da API
* 📄 [OpenAPI YAML](/shared/schema/openapi.yaml)

* 📬 [Postman Collection](/shared/schema/TodoVerse.postman_collection.json)

Use esses arquivos para testar ou importar em ferramentas de API (Swagger UI, Insomnia, Postman etc.).

## 🎨 Frontends
Todos os frontends são desacoplados e podem trocar dinamicamente qual API estão usando.

* ⏳ Angular — em construção

* ⏳ React — em construção

* ⏳ Vue — em construção