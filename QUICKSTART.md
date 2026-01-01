# Quickstart Guide: AI Portfolio Manager

## Pré-requisitos

### Backend
- .NET 9.0 SDK ou superior
- SQLite (incluído com o Entity Framework Core)

### Frontend
- Node.js 18.x ou superior
- npm ou yarn

## Configuração Inicial

### 1. Backend Setup

```bash
# Navegue para o diretório do backend
cd backend

# Restaure as dependências
dotnet restore

# Compile a solução
dotnet build

# Execute as migrações do banco de dados
cd src/AAI.WebAPI
dotnet ef database update

# Execute o backend
dotnet run
```

O backend estará disponível em `http://localhost:5000`

### 2. Frontend Setup

```bash
# Navegue para o diretório do frontend
cd frontend

# Instale as dependências
npm install

# Execute o servidor de desenvolvimento
npm run dev
```

O frontend estará disponível em `http://localhost:3000`

## Estrutura do Projeto

```
aai_virtual/
├── backend/
│   ├── src/
│   │   ├── AAI.Domain/          # Entidades e lógica de negócio
│   │   ├── AAI.Application/     # Casos de uso e CQRS
│   │   ├── AAI.Infrastructure/  # EF Core, APIs externas
│   │   └── AAI.WebAPI/          # Controllers e configuração
│   ├── tests/
│   │   ├── AAI.Domain.Tests/
│   │   ├── AAI.Application.Tests/
│   │   ├── AAI.Infrastructure.Tests/
│   │   └── AAI.WebAPI.Tests/
│   └── AAI.sln
│
├── frontend/
│   ├── src/
│   │   ├── app/                # Configuração da aplicação
│   │   ├── features/           # Features organizadas por domínio
│   │   ├── shared/             # Componentes reutilizáveis
│   │   └── services/           # Serviços de API
│   ├── tests/
│   └── package.json
│
└── specs/
    └── 001-ai-portfolio-manager/  # Documentação da feature
```

## Comandos Úteis

### Backend

```bash
# Executar testes
dotnet test

# Criar nova migration
dotnet ef migrations add MigrationName -p src/AAI.Infrastructure -s src/AAI.WebAPI

# Atualizar banco de dados
dotnet ef database update -p src/AAI.Infrastructure -s src/AAI.WebAPI

# Build de produção
dotnet build -c Release
```

### Frontend

```bash
# Executar testes
npm test

# Build de produção
npm run build

# Lint e formatação
npm run lint
npm run format

# Preview da build de produção
npm run preview
```

## Configuração de Ambiente

### Backend (appsettings.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=aai_portfolio.db"
  },
  "JWT": {
    "Secret": "your-secret-key-min-32-chars-change-in-production",
    "ExpirationInMinutes": 60
  },
  "ExternalServices": {
    "OpenAI": {
      "ApiKey": "YOUR_OPENAI_API_KEY"
    }
  }
}
```

### Frontend (.env.development)

Crie um arquivo `.env.development` no diretório frontend com:

```
VITE_API_URL=http://localhost:5000/api
VITE_SIGNALR_URL=http://localhost:5000/hubs
```

## Próximos Passos

1. **Phase 2: Foundational** - Implementar camadas base (Domain, Application, Infrastructure)
2. **Phase 3: User Story 8** - Segurança e Autenticação
3. **Phase 4: User Story 2** - Configuração de Perfil
4. **Phase 5: User Story 1** - Dashboard de Portfólio
5. **Phase 6: User Story 4** - Recomendações de IA

## Troubleshooting

### Backend não inicia
- Verifique se o .NET 9.0 SDK está instalado: `dotnet --version`
- Verifique se todas as dependências foram restauradas: `dotnet restore`

### Frontend não compila
- Limpe node_modules e reinstale: `rm -rf node_modules && npm install`
- Verifique a versão do Node.js: `node --version`

### Erro de conexão com banco de dados
- Certifique-se de que as migrações foram aplicadas
- Verifique a string de conexão no appsettings.json
