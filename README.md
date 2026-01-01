# AAI Virtual - AI Portfolio Manager

Sistema de gerenciamento de portfólio de investimentos com inteligência artificial.

## 🎯 Visão Geral

O AAI Virtual é uma aplicação web full-stack que utiliza inteligência artificial para analisar e recomendar rebalanceamentos de portfólio de investimentos. O sistema monitora ativos financeiros, agrega notícias e dados de mercado, e fornece recomendações personalizadas baseadas no perfil de risco do investidor.

## 🏗️ Arquitetura

- **Frontend**: React 18 + TypeScript + Vite
- **Backend**: C# .NET 9 Web API com Clean Architecture
- **Database**: SQLite com criptografia AES-256
- **IA**: OpenAI GPT-4 / Anthropic Claude para análises e recomendações

## 📋 Features Principais

### MVP (Priority 1)
- ✅ **Setup Completo**: Estrutura do projeto configurada
- 🔒 **Segurança (US8)**: Autenticação local com criptografia de dados
- ⚙️ **Perfil (US2)**: Configuração de perfil de risco e thresholds
- 📊 **Dashboard (US1)**: Visualização de portfólio com alocação e performance
- 🤖 **IA (US4)**: Recomendações inteligentes de rebalanceamento

### Features Adicionais (Priority 2)
- 📰 **Notícias (US3)**: Feed com resumos gerados por IA
- 🎲 **Simulação (US5)**: Cenários de rebalanceamento
- 🔔 **Alertas (US6)**: Notificações de eventos de mercado
- 📈 **Analytics (US7)**: Histórico e métricas de performance

## 🚀 Quick Start

Consulte o [QUICKSTART.md](./QUICKSTART.md) para instruções detalhadas de setup.

### Início Rápido

```bash
# Backend
cd backend
dotnet restore
dotnet run --project src/AAI.WebAPI

# Frontend (em outro terminal)
cd frontend
npm install
npm run dev
```

## 📁 Estrutura do Projeto

```
aai_virtual/
├── backend/              # .NET 9 Web API
│   ├── src/
│   │   ├── AAI.Domain/          # Entidades e regras de negócio
│   │   ├── AAI.Application/     # Casos de uso (CQRS)
│   │   ├── AAI.Infrastructure/  # Persistência e APIs externas
│   │   └── AAI.WebAPI/          # Controllers e configuração
│   └── tests/                   # Testes unitários e integração
│
├── frontend/             # React + TypeScript
│   ├── src/
│   │   ├── features/            # Módulos por domínio
│   │   ├── shared/              # Componentes reutilizáveis
│   │   └── services/            # Serviços de API
│   └── tests/                   # Testes frontend
│
└── specs/                # Documentação da especificação
    └── 001-ai-portfolio-manager/
        ├── spec.md              # Especificação completa
        ├── plan.md              # Plano de implementação
        ├── tasks.md             # Tarefas detalhadas
        ├── data-model.md        # Modelo de dados
        └── contracts/api.yaml   # Contratos OpenAPI
```

## 🛠️ Tecnologias

### Backend
- .NET 9.0
- Entity Framework Core 9.0 (SQLite)
- MediatR (CQRS)
- FluentValidation
- AutoMapper
- Serilog
- SignalR (real-time)

### Frontend
- React 18
- TypeScript 5
- Vite 5
- React Query (TanStack Query)
- React Router 6
- Recharts
- Axios
- CSS Modules

## 📚 Documentação

- [Especificação Completa](./specs/001-ai-portfolio-manager/spec.md)
- [Plano de Implementação](./specs/001-ai-portfolio-manager/plan.md)
- [Tarefas Detalhadas](./specs/001-ai-portfolio-manager/tasks.md)
- [Modelo de Dados](./specs/001-ai-portfolio-manager/data-model.md)
- [API Contracts](./specs/001-ai-portfolio-manager/contracts/api.yaml)
- [Quick Start](./QUICKSTART.md)

## 🧪 Testes

```bash
# Backend
cd backend
dotnet test

# Frontend
cd frontend
npm test
```

## 📈 Status do Projeto

- [x] Phase 1: Setup (Concluída)
- [ ] Phase 2: Foundational (Em Progresso)
- [ ] Phase 3: User Story 8 - Segurança
- [ ] Phase 4: User Story 2 - Perfil
- [ ] Phase 5: User Story 1 - Dashboard
- [ ] Phase 6: User Story 4 - Recomendações IA

## 🤝 Contribuindo

Este é um projeto em desenvolvimento ativo. Consulte as [tarefas pendentes](./specs/001-ai-portfolio-manager/tasks.md) para ver o que precisa ser implementado.

## 📄 Licença

[Adicionar licença apropriada]

## ✨ Constitution Check

Este projeto segue os princípios da Constitution:
- ✅ Clean Code (SOLID, DRY, documentação)
- ✅ Cobertura de testes ≥ 80%
- ✅ UX consistente (Design System, WCAG 2.1 AA)
- ✅ Performance (TTI < 3s, API < 200ms p95)
