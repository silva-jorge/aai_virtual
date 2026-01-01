# Implementation Plan: AI Portfolio Manager

**Branch**: `001-ai-portfolio-manager` | **Date**: 2026-01-01 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/001-ai-portfolio-manager/spec.md`

## Summary

Sistema de gerenciamento de portfólio de investimentos com inteligência artificial. Aplicação web full-stack com React + TypeScript no frontend e C# .NET 8 Web API no backend, seguindo Clean Architecture com CQRS. O sistema monitora portfólios de investimentos, agrega dados de mercado (cotações, notícias, balanços), e utiliza LLMs externos (OpenAI/Anthropic) para gerar recomendações de rebalanceamento personalizadas. Dados sensíveis são armazenados localmente em SQLite com criptografia.

## Technical Context

**Language/Version**: 
- Frontend: TypeScript 5.x + React 18.x
- Backend: C# 12 / .NET 8.0

**Primary Dependencies**: 
- Frontend: React Query, React Router, Recharts, CSS Modules
- Backend: MediatR (CQRS), Entity Framework Core 8, FluentValidation, Serilog, SignalR

**Storage**: SQLite (local) via Entity Framework Core

**Testing**: 
- Frontend: Vitest + React Testing Library
- Backend: xUnit + FluentAssertions + NSubstitute + TestContainers

**Target Platform**: Web (SPA) - Navegadores modernos (Chrome, Firefox, Safari, Edge)

**Project Type**: Web Application (Frontend SPA + Backend API)

**Performance Goals**: 
- API: < 200ms p95 para operações simples, < 1s p95 para agregações complexas
- Frontend: TTI < 3s, FCP < 1.5s, Lighthouse Score ≥ 90

**Constraints**: 
- Dados sensíveis armazenados localmente (sem cloud storage)
- Cotações com delay de 15-20 minutos (APIs gratuitas)
- Consentimento explícito para envio de dados a LLM

**Scale/Scope**: 
- Single-user application (portfólio pessoal)
- Suporte a 1000+ ativos B3
- Histórico de 10+ anos de transações

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### I. Qualidade de Código ✅

| Princípio | Implementação |
|-----------|---------------|
| Clean Code | Naming conventions em português para domínio, inglês para técnico. Interfaces e DTOs autoexplicativos |
| DRY | Shared library para modelos comuns. Component library no frontend |
| SOLID | Clean Architecture garante SRP. Dependency Injection nativo do .NET |
| Documentação | XML docs para APIs públicas. JSDoc para componentes React |
| Code Reviews | PR workflow configurado no repositório |
| Linting/Formatting | ESLint + Prettier (frontend), dotnet format + StyleCop (backend) |

### II. Padrões de Teste ✅

| Regra | Implementação |
|-------|---------------|
| Cobertura Mínima | ≥ 80% para Domain e Application layers |
| Pirâmide de Testes | Unit (Domain/Application) > Integration (Infrastructure) > E2E (Playwright) |
| Testes Unitários | xUnit + NSubstitute para backend, Vitest para frontend |
| Testes de Integração | TestContainers para SQLite, MSW para mocking de APIs |
| Testes E2E | Playwright para jornadas P1 (dashboard, recomendações) |
| CI/CD Gate | GitHub Actions com gates de lint, test, coverage |

### III. Experiência do Usuário Consistente ✅

| Regra | Implementação |
|-------|---------------|
| Design System | Componentes base criados antes das features. Tokens de design padronizados |
| Consistência Visual | CSS Custom Properties para cores/tipografia/spacing |
| Padrões de Interação | Loading skeletons, toast notifications, modal confirmations padronizados |
| Acessibilidade | WCAG 2.1 AA. Semantic HTML. ARIA labels. Navegação por teclado |
| Responsividade | Mobile-first. Breakpoints: 320px, 768px, 1024px |
| Feedback ao Usuário | React Query states (loading/error/success) expostos consistentemente |
| Internacionalização | pt-BR como idioma base. Estrutura preparada para i18n futuro |

### IV. Requisitos de Performance ✅

| Regra | Implementação |
|-------|---------------|
| Tempos de Resposta API | < 200ms p95 (CRUD), < 1s p95 (agregações com cache miss) |
| TTI / FCP | Code splitting por rota. Lazy loading de módulos não-críticos |
| Bundle Size | Tree shaking. Dynamic imports para Recharts |
| Consultas de Banco | EF Core com eager loading explícito. Índices em foreign keys |
| Caching | In-memory cache para cotações (TTL 15min). EF Core query caching |
| Memory/CPU | Background services com throttling. Batch processing para updates |
| Performance Budget | Lighthouse CI integrado. Alertas para degradações > 10% |

## Project Structure

### Documentation (this feature)

```text
specs/001-ai-portfolio-manager/
├── plan.md              # Este arquivo
├── research.md          # Phase 0 output
├── data-model.md        # Phase 1 output
├── quickstart.md        # Phase 1 output
├── contracts/           # Phase 1 output (OpenAPI specs)
│   └── api.yaml
├── checklists/
│   └── requirements.md  # Checklist de requisitos
└── tasks.md             # Phase 2 output (/speckit.tasks)
```

### Source Code (repository root)

```text
# Backend (.NET 8 Clean Architecture)
backend/
├── src/
│   ├── AAI.Domain/                    # Entities, Value Objects, Domain Events, Interfaces
│   │   ├── Entities/
│   │   │   ├── Portfolio.cs
│   │   │   ├── Position.cs
│   │   │   ├── Asset.cs
│   │   │   ├── Transaction.cs
│   │   │   ├── UserProfile.cs
│   │   │   ├── Recommendation.cs
│   │   │   ├── MarketEvent.cs
│   │   │   └── NewsItem.cs
│   │   ├── ValueObjects/
│   │   │   ├── Money.cs
│   │   │   ├── Percentage.cs
│   │   │   └── AssetClass.cs
│   │   ├── Enums/
│   │   │   ├── RiskProfile.cs
│   │   │   ├── TransactionType.cs
│   │   │   └── Priority.cs
│   │   ├── Events/
│   │   │   └── DomainEvents.cs
│   │   └── Interfaces/
│   │       ├── IPortfolioRepository.cs
│   │       ├── IAssetRepository.cs
│   │       └── IUnitOfWork.cs
│   │
│   ├── AAI.Application/               # Use Cases, DTOs, CQRS Handlers, Validators
│   │   ├── Common/
│   │   │   ├── Behaviors/
│   │   │   │   ├── ValidationBehavior.cs
│   │   │   │   ├── LoggingBehavior.cs
│   │   │   │   └── CachingBehavior.cs
│   │   │   ├── Interfaces/
│   │   │   │   ├── IMarketDataService.cs
│   │   │   │   ├── INewsService.cs
│   │   │   │   ├── IAIRecommendationService.cs
│   │   │   │   └── INotificationService.cs
│   │   │   └── Mappings/
│   │   │       └── MappingProfile.cs
│   │   ├── Portfolio/
│   │   │   ├── Commands/
│   │   │   │   ├── CreatePosition/
│   │   │   │   ├── UpdatePosition/
│   │   │   │   ├── DeletePosition/
│   │   │   │   └── ImportTransactions/
│   │   │   ├── Queries/
│   │   │   │   ├── GetPortfolioSummary/
│   │   │   │   ├── GetAllocationBreakdown/
│   │   │   │   ├── GetPerformanceMetrics/
│   │   │   │   └── GetTransactionHistory/
│   │   │   └── DTOs/
│   │   ├── Rebalancing/
│   │   │   ├── Commands/
│   │   │   │   ├── ApplyRecommendation/
│   │   │   │   └── RejectRecommendation/
│   │   │   ├── Queries/
│   │   │   │   ├── GetRecommendations/
│   │   │   │   └── SimulateRebalancing/
│   │   │   └── DTOs/
│   │   ├── News/
│   │   │   ├── Queries/
│   │   │   │   ├── GetNewsFeed/
│   │   │   │   └── GetNewsForAsset/
│   │   │   └── DTOs/
│   │   ├── UserProfile/
│   │   │   ├── Commands/
│   │   │   │   ├── UpdateRiskProfile/
│   │   │   │   └── UpdateThresholds/
│   │   │   ├── Queries/
│   │   │   │   └── GetUserProfile/
│   │   │   └── DTOs/
│   │   └── Analytics/
│   │       ├── Queries/
│   │       │   ├── GetHistoricalPerformance/
│   │       │   └── GetRiskMetrics/
│   │       └── DTOs/
│   │
│   ├── AAI.Infrastructure/            # EF Core, External APIs, SQLite, Caching
│   │   ├── Persistence/
│   │   │   ├── AAIDbContext.cs
│   │   │   ├── Configurations/
│   │   │   │   ├── PortfolioConfiguration.cs
│   │   │   │   ├── PositionConfiguration.cs
│   │   │   │   └── ...
│   │   │   ├── Repositories/
│   │   │   │   ├── PortfolioRepository.cs
│   │   │   │   ├── AssetRepository.cs
│   │   │   │   └── ...
│   │   │   ├── Migrations/
│   │   │   └── UnitOfWork.cs
│   │   ├── ExternalServices/
│   │   │   ├── MarketData/
│   │   │   │   ├── BrapiClient.cs
│   │   │   │   ├── YahooFinanceClient.cs
│   │   │   │   └── MarketDataService.cs
│   │   │   ├── News/
│   │   │   │   ├── NewsScraperService.cs
│   │   │   │   └── NewsAggregatorService.cs
│   │   │   └── AI/
│   │   │       ├── OpenAIClient.cs
│   │   │       ├── AnthropicClient.cs
│   │   │       └── AIRecommendationService.cs
│   │   ├── Caching/
│   │   │   └── InMemoryCacheService.cs
│   │   ├── BackgroundServices/
│   │   │   ├── MarketDataUpdateService.cs
│   │   │   ├── NewsAggregationService.cs
│   │   │   └── AlertMonitoringService.cs
│   │   └── DependencyInjection.cs
│   │
│   └── AAI.WebAPI/                    # Controllers, SignalR Hubs, Middleware
│       ├── Controllers/
│       │   ├── PortfolioController.cs
│       │   ├── AssetsController.cs
│       │   ├── RebalancingController.cs
│       │   ├── NewsController.cs
│       │   ├── ProfileController.cs
│       │   ├── AnalyticsController.cs
│       │   └── AuthController.cs
│       ├── Hubs/
│       │   └── NotificationHub.cs
│       ├── Middleware/
│       │   ├── ExceptionHandlingMiddleware.cs
│       │   └── RequestLoggingMiddleware.cs
│       ├── Filters/
│       │   └── ValidationFilter.cs
│       ├── Program.cs
│       └── appsettings.json
│
├── tests/
│   ├── AAI.Domain.Tests/
│   ├── AAI.Application.Tests/
│   ├── AAI.Infrastructure.Tests/
│   └── AAI.WebAPI.Tests/
│
├── AAI.sln
└── Directory.Build.props

# Frontend (React + TypeScript)
frontend/
├── src/
│   ├── app/
│   │   ├── App.tsx
│   │   ├── routes.tsx
│   │   └── providers/
│   │       ├── QueryProvider.tsx
│   │       ├── AuthProvider.tsx
│   │       └── NotificationProvider.tsx
│   │
│   ├── features/
│   │   ├── portfolio/
│   │   │   ├── components/
│   │   │   │   ├── PortfolioDashboard.tsx
│   │   │   │   ├── AllocationChart.tsx
│   │   │   │   ├── PositionCard.tsx
│   │   │   │   └── PositionList.tsx
│   │   │   ├── hooks/
│   │   │   │   ├── usePortfolio.ts
│   │   │   │   └── usePositions.ts
│   │   │   ├── api/
│   │   │   │   └── portfolioApi.ts
│   │   │   └── types/
│   │   │       └── portfolio.ts
│   │   │
│   │   ├── rebalancing/
│   │   │   ├── components/
│   │   │   │   ├── RecommendationPanel.tsx
│   │   │   │   ├── RecommendationCard.tsx
│   │   │   │   ├── SimulationView.tsx
│   │   │   │   └── RebalancingWizard.tsx
│   │   │   ├── hooks/
│   │   │   │   ├── useRecommendations.ts
│   │   │   │   └── useSimulation.ts
│   │   │   └── api/
│   │   │       └── rebalancingApi.ts
│   │   │
│   │   ├── news/
│   │   │   ├── components/
│   │   │   │   ├── NewsFeed.tsx
│   │   │   │   ├── NewsCard.tsx
│   │   │   │   └── NewsFilters.tsx
│   │   │   ├── hooks/
│   │   │   │   └── useNews.ts
│   │   │   └── api/
│   │   │       └── newsApi.ts
│   │   │
│   │   ├── analytics/
│   │   │   ├── components/
│   │   │   │   ├── PerformanceChart.tsx
│   │   │   │   ├── BenchmarkComparison.tsx
│   │   │   │   ├── RiskMetrics.tsx
│   │   │   │   └── HistoricalView.tsx
│   │   │   ├── hooks/
│   │   │   │   └── useAnalytics.ts
│   │   │   └── api/
│   │   │       └── analyticsApi.ts
│   │   │
│   │   ├── profile/
│   │   │   ├── components/
│   │   │   │   ├── ProfileSettings.tsx
│   │   │   │   ├── RiskProfileSelector.tsx
│   │   │   │   └── ThresholdConfig.tsx
│   │   │   ├── hooks/
│   │   │   │   └── useProfile.ts
│   │   │   └── api/
│   │   │       └── profileApi.ts
│   │   │
│   │   └── auth/
│   │       ├── components/
│   │       │   ├── LoginForm.tsx
│   │       │   └── PinSetup.tsx
│   │       ├── hooks/
│   │       │   └── useAuth.ts
│   │       └── api/
│   │           └── authApi.ts
│   │
│   ├── shared/
│   │   ├── components/
│   │   │   ├── ui/
│   │   │   │   ├── Button.tsx
│   │   │   │   ├── Card.tsx
│   │   │   │   ├── Modal.tsx
│   │   │   │   ├── Toast.tsx
│   │   │   │   ├── Skeleton.tsx
│   │   │   │   ├── Input.tsx
│   │   │   │   └── Select.tsx
│   │   │   ├── layout/
│   │   │   │   ├── Header.tsx
│   │   │   │   ├── Sidebar.tsx
│   │   │   │   ├── MainLayout.tsx
│   │   │   │   └── PageContainer.tsx
│   │   │   └── charts/
│   │   │       ├── PieChart.tsx
│   │   │       ├── LineChart.tsx
│   │   │       └── BarChart.tsx
│   │   ├── hooks/
│   │   │   ├── useLocalStorage.ts
│   │   │   ├── useDebounce.ts
│   │   │   └── useNotification.ts
│   │   ├── utils/
│   │   │   ├── formatters.ts
│   │   │   ├── validators.ts
│   │   │   └── encryption.ts
│   │   ├── types/
│   │   │   └── common.ts
│   │   └── styles/
│   │       ├── variables.css
│   │       ├── globals.css
│   │       └── tokens.css
│   │
│   ├── services/
│   │   ├── api/
│   │   │   ├── apiClient.ts
│   │   │   └── endpoints.ts
│   │   ├── signalr/
│   │   │   └── notificationClient.ts
│   │   └── storage/
│   │       └── localStorageService.ts
│   │
│   └── main.tsx
│
├── tests/
│   ├── unit/
│   ├── integration/
│   └── e2e/
│
├── public/
├── index.html
├── vite.config.ts
├── tsconfig.json
├── package.json
└── .eslintrc.cjs
```

**Structure Decision**: Web Application com Clean Architecture no backend e Feature-based organization no frontend. O backend segue a estrutura de 4 projetos do Clean Architecture (Domain, Application, Infrastructure, WebAPI) conforme especificado. O frontend é organizado por features (portfolio, rebalancing, news, analytics, profile) com componentes compartilhados em `shared/`.

## Complexity Tracking

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| 4 projetos backend (vs 3) | Clean Architecture requer separação clara Domain/Application/Infrastructure/Presentation | Combinar layers violaria SRP e dificultaria testabilidade |
| CQRS com MediatR | Separação clara de comandos (write) e queries (read) para operações de portfólio com diferentes padrões de acesso | Simple CRUD controllers misturariam responsabilidades e dificultariam caching de queries |
| SignalR para real-time | Alertas de mercado requerem notificação push imediata | Polling seria ineficiente e atrasaria alertas críticos |
| IHostedService para background tasks | Coleta periódica de dados de mercado e processamento de notícias | Cron jobs externos adicionariam complexidade de infraestrutura |
