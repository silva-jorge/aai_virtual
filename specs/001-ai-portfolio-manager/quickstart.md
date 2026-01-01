# Quickstart: AI Portfolio Manager

**Feature**: 001-ai-portfolio-manager  
**Date**: 2026-01-01  
**Status**: Ready for Development

## Pré-requisitos

### Backend (.NET 8)

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (8.0.x)
- IDE: Visual Studio 2022 17.8+, Rider 2024.1+, ou VS Code com C# Dev Kit

```bash
# Verificar instalação
dotnet --version  # Deve mostrar 8.0.x
```

### Frontend (React + TypeScript)

- [Node.js](https://nodejs.org/) 20.x LTS
- [pnpm](https://pnpm.io/) 8.x (recomendado) ou npm 10.x

```bash
# Verificar instalação
node --version  # Deve mostrar v20.x
pnpm --version  # Deve mostrar 8.x
```

### Ferramentas Opcionais

- [Docker](https://www.docker.com/) - Para containerização (opcional)
- [Bruno](https://www.usebruno.com/) ou Postman - Para testar APIs
- [SQLite Browser](https://sqlitebrowser.org/) - Para inspecionar banco de dados

---

## Configuração do Projeto

### 1. Clonar e Preparar Estrutura

```bash
# Na raiz do repositório
cd aai_virtual

# Criar estrutura de diretórios
mkdir -p backend/src frontend
```

### 2. Configurar Backend (.NET 8)

```bash
cd backend

# Criar solution
dotnet new sln -n AAI

# Criar projetos
dotnet new classlib -n AAI.Domain -o src/AAI.Domain
dotnet new classlib -n AAI.Application -o src/AAI.Application
dotnet new classlib -n AAI.Infrastructure -o src/AAI.Infrastructure
dotnet new webapi -n AAI.WebAPI -o src/AAI.WebAPI

# Criar projetos de teste
dotnet new xunit -n AAI.Domain.Tests -o tests/AAI.Domain.Tests
dotnet new xunit -n AAI.Application.Tests -o tests/AAI.Application.Tests
dotnet new xunit -n AAI.Infrastructure.Tests -o tests/AAI.Infrastructure.Tests
dotnet new xunit -n AAI.WebAPI.Tests -o tests/AAI.WebAPI.Tests

# Adicionar projetos à solution
dotnet sln add src/AAI.Domain/AAI.Domain.csproj
dotnet sln add src/AAI.Application/AAI.Application.csproj
dotnet sln add src/AAI.Infrastructure/AAI.Infrastructure.csproj
dotnet sln add src/AAI.WebAPI/AAI.WebAPI.csproj
dotnet sln add tests/AAI.Domain.Tests/AAI.Domain.Tests.csproj
dotnet sln add tests/AAI.Application.Tests/AAI.Application.Tests.csproj
dotnet sln add tests/AAI.Infrastructure.Tests/AAI.Infrastructure.Tests.csproj
dotnet sln add tests/AAI.WebAPI.Tests/AAI.WebAPI.Tests.csproj

# Configurar referências entre projetos
dotnet add src/AAI.Application/AAI.Application.csproj reference src/AAI.Domain/AAI.Domain.csproj
dotnet add src/AAI.Infrastructure/AAI.Infrastructure.csproj reference src/AAI.Application/AAI.Application.csproj
dotnet add src/AAI.WebAPI/AAI.WebAPI.csproj reference src/AAI.Infrastructure/AAI.Infrastructure.csproj
```

### 3. Instalar Pacotes NuGet (Backend)

```bash
cd backend

# AAI.Domain - sem dependências externas (pure domain)

# AAI.Application
cd src/AAI.Application
dotnet add package MediatR --version 12.2.0
dotnet add package FluentValidation --version 11.9.0
dotnet add package FluentValidation.DependencyInjectionExtensions --version 11.9.0
dotnet add package AutoMapper --version 13.0.1
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1

# AAI.Infrastructure
cd ../AAI.Infrastructure
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.1
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.1
dotnet add package Microsoft.Extensions.Caching.Memory --version 8.0.0
dotnet add package Polly --version 8.2.1
dotnet add package Polly.Extensions.Http --version 3.0.0

# AAI.WebAPI
cd ../AAI.WebAPI
dotnet add package Serilog.AspNetCore --version 8.0.0
dotnet add package Serilog.Sinks.Console --version 5.0.1
dotnet add package Serilog.Sinks.File --version 5.0.0
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.1
dotnet add package Swashbuckle.AspNetCore --version 6.5.0
dotnet add package Microsoft.AspNetCore.SignalR --version 1.1.0

# Test projects
cd ../../tests/AAI.Domain.Tests
dotnet add package FluentAssertions --version 6.12.0
dotnet add package NSubstitute --version 5.1.0

cd ../AAI.Application.Tests
dotnet add package FluentAssertions --version 6.12.0
dotnet add package NSubstitute --version 5.1.0

cd ../AAI.Infrastructure.Tests
dotnet add package FluentAssertions --version 6.12.0
dotnet add package NSubstitute --version 5.1.0
dotnet add package Testcontainers --version 3.7.0

cd ../AAI.WebAPI.Tests
dotnet add package FluentAssertions --version 6.12.0
dotnet add package Microsoft.AspNetCore.Mvc.Testing --version 8.0.1
```

### 4. Configurar Frontend (React)

```bash
cd frontend

# Criar projeto Vite + React + TypeScript
pnpm create vite@latest . --template react-ts

# Instalar dependências
pnpm install

# Dependências principais
pnpm add @tanstack/react-query @tanstack/react-query-devtools
pnpm add react-router-dom
pnpm add recharts
pnpm add @microsoft/signalr
pnpm add axios
pnpm add date-fns
pnpm add clsx

# Dependências de desenvolvimento
pnpm add -D vitest @testing-library/react @testing-library/jest-dom jsdom
pnpm add -D msw
pnpm add -D @playwright/test
pnpm add -D eslint-plugin-react-hooks eslint-plugin-react-refresh
pnpm add -D @types/node
```

### 5. Criar Arquivos de Configuração

#### Directory.Build.props (Backend)

```bash
cat > backend/Directory.Build.props << 'EOF'
<Project>
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <AnalysisLevel>latest</AnalysisLevel>
  </PropertyGroup>
</Project>
EOF
```

#### .editorconfig (Raiz)

```bash
cat > .editorconfig << 'EOF'
root = true

[*]
charset = utf-8
end_of_line = lf
insert_final_newline = true
trim_trailing_whitespace = true
indent_style = space

[*.{cs,csx}]
indent_size = 4

[*.{ts,tsx,js,jsx,json,css}]
indent_size = 2

[*.md]
trim_trailing_whitespace = false
EOF
```

#### appsettings.Development.json (Backend)

```bash
cat > backend/src/AAI.WebAPI/appsettings.Development.json << 'EOF'
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=aai_portfolio.db"
  },
  "Jwt": {
    "Secret": "development-secret-key-minimum-32-characters-long",
    "Issuer": "AAI-Virtual",
    "Audience": "AAI-Virtual-Client",
    "AccessTokenExpirationMinutes": 15,
    "RefreshTokenExpirationDays": 7
  },
  "ExternalApis": {
    "Brapi": {
      "BaseUrl": "https://brapi.dev/api",
      "ApiKey": ""
    },
    "OpenAI": {
      "BaseUrl": "https://api.openai.com/v1",
      "ApiKey": "",
      "Model": "gpt-4-turbo-preview"
    }
  },
  "Cors": {
    "AllowedOrigins": ["http://localhost:5173"]
  }
}
EOF
```

#### .env.development (Frontend)

```bash
cat > frontend/.env.development << 'EOF'
VITE_API_BASE_URL=http://localhost:5000/api/v1
VITE_SIGNALR_HUB_URL=http://localhost:5000/hubs/notifications
EOF
```

---

## Executar o Projeto

### Backend

```bash
cd backend/src/AAI.WebAPI

# Restaurar pacotes
dotnet restore

# Executar migrations (após criar DbContext)
dotnet ef database update

# Executar em modo desenvolvimento
dotnet run

# Ou com hot reload
dotnet watch run
```

O backend estará disponível em:
- API: http://localhost:5000
- Swagger: http://localhost:5000/swagger

### Frontend

```bash
cd frontend

# Instalar dependências (se ainda não instalou)
pnpm install

# Executar em modo desenvolvimento
pnpm dev
```

O frontend estará disponível em:
- http://localhost:5173

### Executar Testes

```bash
# Backend - todos os testes
cd backend
dotnet test

# Backend - com coverage
dotnet test --collect:"XPlat Code Coverage"

# Frontend - unit tests
cd frontend
pnpm test

# Frontend - E2E tests
pnpm exec playwright test
```

---

## Estrutura de Pastas Final

```
aai_virtual/
├── backend/
│   ├── src/
│   │   ├── AAI.Domain/
│   │   ├── AAI.Application/
│   │   ├── AAI.Infrastructure/
│   │   └── AAI.WebAPI/
│   ├── tests/
│   │   ├── AAI.Domain.Tests/
│   │   ├── AAI.Application.Tests/
│   │   ├── AAI.Infrastructure.Tests/
│   │   └── AAI.WebAPI.Tests/
│   ├── AAI.sln
│   └── Directory.Build.props
├── frontend/
│   ├── src/
│   │   ├── app/
│   │   ├── features/
│   │   ├── shared/
│   │   └── services/
│   ├── tests/
│   ├── package.json
│   └── vite.config.ts
├── specs/
│   └── 001-ai-portfolio-manager/
│       ├── spec.md
│       ├── plan.md
│       ├── research.md
│       ├── data-model.md
│       ├── quickstart.md
│       └── contracts/
│           └── api.yaml
├── .editorconfig
├── .gitignore
└── README.md
```

---

## Configuração de IDE

### VS Code

Extensões recomendadas:

```json
// .vscode/extensions.json
{
  "recommendations": [
    "ms-dotnettools.csdevkit",
    "ms-dotnettools.csharp",
    "dbaeumer.vscode-eslint",
    "esbenp.prettier-vscode",
    "bradlc.vscode-tailwindcss",
    "ms-azuretools.vscode-docker"
  ]
}
```

Settings do workspace:

```json
// .vscode/settings.json
{
  "editor.formatOnSave": true,
  "editor.defaultFormatter": "esbenp.prettier-vscode",
  "[csharp]": {
    "editor.defaultFormatter": "ms-dotnettools.csharp"
  },
  "typescript.preferences.importModuleSpecifier": "relative",
  "eslint.workingDirectories": ["frontend"]
}
```

### Visual Studio / Rider

- Habilitar "Format on Save"
- Configurar StyleCop Analyzers
- Configurar code cleanup on save

---

## Próximos Passos

Após configurar o ambiente:

1. **Execute `/speckit.tasks`** para gerar as tarefas de implementação
2. Implemente as entidades do Domain (ver `data-model.md`)
3. Configure o DbContext e migrations
4. Implemente os primeiros endpoints (Portfolio, Positions)
5. Configure o frontend com React Query
6. Integre SignalR para notificações

---

## Troubleshooting

### Erro: "SDK not found"

```bash
# Verificar SDKs instalados
dotnet --list-sdks

# Se necessário, instalar .NET 8
# macOS: brew install dotnet@8
# Windows: winget install Microsoft.DotNet.SDK.8
```

### Erro: "Port already in use"

```bash
# Alterar porta do backend
# Em launchSettings.json, modificar applicationUrl

# Alterar porta do frontend
# pnpm dev -- --port 3000
```

### Erro: "CORS blocked"

Verificar se `Cors.AllowedOrigins` em `appsettings.Development.json` inclui a URL do frontend.

### Erro: "Database locked"

```bash
# Parar o processo que está usando o SQLite
# Ou deletar o arquivo .db e recriar
rm backend/src/AAI.WebAPI/aai_portfolio.db
dotnet ef database update
```

---

## Links Úteis

- [.NET 8 Documentation](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)
- [Clean Architecture Template](https://github.com/jasontaylordev/CleanArchitecture)
- [MediatR Documentation](https://github.com/jbogard/MediatR/wiki)
- [React Query Documentation](https://tanstack.com/query/latest)
- [Recharts Examples](https://recharts.org/en-US/examples)
- [SignalR Documentation](https://learn.microsoft.com/en-us/aspnet/core/signalr)
