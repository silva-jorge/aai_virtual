# Tasks: AI Portfolio Manager

**Input**: Design documents from `/specs/001-ai-portfolio-manager/`
**Prerequisites**: plan.md ✅, spec.md ✅, research.md ✅, data-model.md ✅, contracts/api.yaml ✅, quickstart.md ✅

**Tests**: Conforme Constitution §II, testes são cidadãos de primeira classe. Cobertura mínima de 80% para lógica de negócio. Tarefas de teste estão incluídas nas fases de implementação (marcadas com sufixo .Tests).

**Organization**: Tasks são organizadas por user story para permitir implementação e testes independentes de cada história.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Pode rodar em paralelo (arquivos diferentes, sem dependências)
- **[Story]**: Qual user story esta tarefa pertence (ex: US1, US2, US3)
- Inclui caminhos exatos de arquivos nas descrições

## Path Conventions

- **Backend**: `backend/src/AAI.{Domain,Application,Infrastructure,WebAPI}/`
- **Frontend**: `frontend/src/`
- **Tests Backend**: `backend/tests/AAI.{Domain,Application,Infrastructure,WebAPI}.Tests/`
- **Tests Frontend**: `frontend/tests/`

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Inicialização do projeto e estrutura básica

- [x] T001 Create project structure per implementation plan (backend/, frontend/, specs/)
- [x] T002 Initialize .NET 8 solution with Clean Architecture projects per quickstart.md in backend/
- [x] T003 [P] Configure Directory.Build.props with .NET 8 settings in backend/Directory.Build.props
- [x] T004 [P] Initialize Vite + React + TypeScript project per quickstart.md in frontend/
- [x] T005 [P] Configure ESLint, Prettier for frontend in frontend/.eslintrc.cjs, frontend/.prettierrc
- [x] T006 [P] Configure .editorconfig for repository root in .editorconfig
- [x] T007 Install NuGet packages for all backend projects per quickstart.md
- [x] T008 Install npm packages for frontend per quickstart.md in frontend/package.json
- [x] T009 [P] Create appsettings.json and appsettings.Development.json in backend/src/AAI.WebAPI/
- [x] T010 [P] Create .env.development for frontend in frontend/.env.development
- [x] T011 [P] Create .gitignore for repository root

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Infraestrutura core que DEVE estar completa antes de QUALQUER user story

**⚠️ CRITICAL**: Nenhum trabalho de user story pode começar até esta fase estar completa

### Domain Layer (AAI.Domain)

- [x] T012 Create base Entity class in backend/src/AAI.Domain/Common/BaseEntity.cs
- [x] T013 [P] Create Money value object in backend/src/AAI.Domain/ValueObjects/Money.cs
- [x] T014 [P] Create Percentage value object in backend/src/AAI.Domain/ValueObjects/Percentage.cs
- [x] T015 [P] Create RiskProfile enum in backend/src/AAI.Domain/Enums/RiskProfile.cs
- [x] T016 [P] Create AssetClass enum in backend/src/AAI.Domain/Enums/AssetClass.cs
- [x] T017 [P] Create TransactionType enum in backend/src/AAI.Domain/Enums/TransactionType.cs
- [x] T018 [P] Create RecommendationActionType enum in backend/src/AAI.Domain/Enums/RecommendationActionType.cs
- [x] T019 [P] Create RecommendationStatus enum in backend/src/AAI.Domain/Enums/RecommendationStatus.cs
- [x] T020 [P] Create Priority enum in backend/src/AAI.Domain/Enums/Priority.cs
- [x] T021 [P] Create Sentiment enum in backend/src/AAI.Domain/Enums/Sentiment.cs
- [x] T022 [P] Create AlertType enum in backend/src/AAI.Domain/Enums/AlertType.cs
- [x] T023 [P] Create IUnitOfWork interface in backend/src/AAI.Domain/Interfaces/IUnitOfWork.cs
- [x] T024 [P] Create IRepository base interface in backend/src/AAI.Domain/Interfaces/IRepository.cs

### Application Layer (AAI.Application)

- [x] T025 Configure MediatR pipeline behaviors in backend/src/AAI.Application/Common/Behaviors/
- [x] T026 [P] Create ValidationBehavior in backend/src/AAI.Application/Common/Behaviors/ValidationBehavior.cs
- [x] T027 [P] Create LoggingBehavior in backend/src/AAI.Application/Common/Behaviors/LoggingBehavior.cs
- [x] T028 [P] Create CachingBehavior in backend/src/AAI.Application/Common/Behaviors/CachingBehavior.cs
- [x] T029 Create AutoMapper MappingProfile in backend/src/AAI.Application/Common/Mappings/MappingProfile.cs
- [x] T030 [P] Create IMarketDataService interface in backend/src/AAI.Application/Common/Interfaces/IMarketDataService.cs
- [x] T031 [P] Create INewsService interface in backend/src/AAI.Application/Common/Interfaces/INewsService.cs
- [x] T032 [P] Create IAIRecommendationService interface in backend/src/AAI.Application/Common/Interfaces/IAIRecommendationService.cs
- [x] T033 [P] Create INotificationService interface in backend/src/AAI.Application/Common/Interfaces/INotificationService.cs
- [x] T034 Create DependencyInjection.cs for Application layer in backend/src/AAI.Application/DependencyInjection.cs

### Infrastructure Layer (AAI.Infrastructure)

- [x] T035 Create AAIDbContext with EF Core in backend/src/AAI.Infrastructure/Persistence/AAIDbContext.cs
- [x] T036 Create UnitOfWork implementation in backend/src/AAI.Infrastructure/Persistence/UnitOfWork.cs
- [x] T037 [P] Create InMemoryCacheService in backend/src/AAI.Infrastructure/Caching/InMemoryCacheService.cs
- [x] T038 Create DependencyInjection.cs for Infrastructure layer in backend/src/AAI.Infrastructure/DependencyInjection.cs

### WebAPI Layer (AAI.WebAPI)

- [x] T039 Configure Program.cs with DI, middleware, Serilog in backend/src/AAI.WebAPI/Program.cs
- [x] T040 [P] Create ExceptionHandlingMiddleware in backend/src/AAI.WebAPI/Middleware/ExceptionHandlingMiddleware.cs
- [x] T041 [P] Create RequestLoggingMiddleware in backend/src/AAI.WebAPI/Middleware/RequestLoggingMiddleware.cs
- [x] T042 [P] Create ValidationFilter in backend/src/AAI.WebAPI/Filters/ValidationFilter.cs
- [x] T043 Configure Swagger/OpenAPI in backend/src/AAI.WebAPI/Program.cs
- [x] T044 Configure CORS for frontend origin in backend/src/AAI.WebAPI/Program.cs

### Frontend Foundation

- [x] T045 Create App.tsx with providers setup in frontend/src/app/App.tsx
- [x] T046 [P] Create QueryProvider with React Query in frontend/src/app/providers/QueryProvider.tsx
- [x] T047 [P] Create NotificationProvider in frontend/src/app/providers/NotificationProvider.tsx
- [x] T048 Create routes.tsx with React Router in frontend/src/app/routes.tsx
- [x] T049 [P] Create apiClient with Axios in frontend/src/services/api/apiClient.ts
- [x] T050 [P] Create endpoints constants in frontend/src/services/api/endpoints.ts
- [x] T051 Create CSS design tokens in frontend/src/shared/styles/tokens.css
- [x] T052 [P] Create global styles in frontend/src/shared/styles/globals.css
- [x] T053 [P] Create CSS variables in frontend/src/shared/styles/variables.css
- [x] T054 [P] Create Button component in frontend/src/shared/components/ui/Button.tsx
- [x] T055 [P] Create Card component in frontend/src/shared/components/ui/Card.tsx
- [x] T056 [P] Create Modal component in frontend/src/shared/components/ui/Modal.tsx
- [x] T057 [P] Create Toast component in frontend/src/shared/components/ui/Toast.tsx
- [x] T058 [P] Create Skeleton component in frontend/src/shared/components/ui/Skeleton.tsx
- [x] T059 [P] Create Input component in frontend/src/shared/components/ui/Input.tsx
- [x] T060 [P] Create Select component in frontend/src/shared/components/ui/Select.tsx
- [x] T061 Create MainLayout component in frontend/src/shared/components/layout/MainLayout.tsx
- [x] T062 [P] Create Header component in frontend/src/shared/components/layout/Header.tsx
- [x] T063 [P] Create Sidebar component in frontend/src/shared/components/layout/Sidebar.tsx
- [x] T064 [P] Create PageContainer component in frontend/src/shared/components/layout/PageContainer.tsx
- [x] T065 Create formatters utilities in frontend/src/shared/utils/formatters.ts
- [x] T066 [P] Create validators utilities in frontend/src/shared/utils/validators.ts
- [x] T067 Create common types in frontend/src/shared/types/common.ts
- [x] T068 Create main.tsx entry point in frontend/src/main.tsx

**Checkpoint**: Foundation ready - implementação de user stories pode começar

---

## Phase 3: User Story 8 - Gerenciamento Seguro de Dados Locais (Priority: P1) 🎯 MVP

**Goal**: Dados financeiros sensíveis armazenados localmente com segurança, autenticação por senha/PIN

**Independent Test**: Verificar que dados são persistidos localmente com criptografia e não são transmitidos a servidores externos sem consentimento

### Domain

- [x] T069 [P] [US8] Create UserProfile entity in backend/src/AAI.Domain/Entities/UserProfile.cs
- [x] T070 [P] [US8] Create IUserProfileRepository interface in backend/src/AAI.Domain/Interfaces/IUserProfileRepository.cs

### Infrastructure - Persistence

- [x] T071 [US8] Create UserProfileConfiguration for EF Core in backend/src/AAI.Infrastructure/Persistence/Configurations/UserProfileConfiguration.cs
- [x] T072 [US8] Create UserProfileRepository in backend/src/AAI.Infrastructure/Persistence/Repositories/UserProfileRepository.cs
- [x] T073 [US8] Create initial migration in backend/src/AAI.Infrastructure/Persistence/Migrations/

### Application - Auth

- [x] T074 [US8] Create SetupPasswordCommand in backend/src/AAI.Application/Auth/Commands/SetupPassword/
- [x] T075 [US8] Create LoginCommand in backend/src/AAI.Application/Auth/Commands/Login/
- [x] T076 [US8] Create RefreshTokenCommand in backend/src/AAI.Application/Auth/Commands/RefreshToken/
- [x] T077 [US8] Create ChangePasswordCommand in backend/src/AAI.Application/Auth/Commands/ChangePassword/
- [x] T078 [P] [US8] Create AuthDTOs in backend/src/AAI.Application/Auth/DTOs/

### Infrastructure - Security

- [x] T079 [US8] Implement JWT token generation service in backend/src/AAI.Infrastructure/Security/JwtTokenService.cs
- [x] T080 [US8] Implement password hashing with Argon2id in backend/src/AAI.Infrastructure/Security/PasswordHasher.cs

### WebAPI - Auth

- [x] T081 [US8] Create AuthController with login, refresh, setup endpoints in backend/src/AAI.WebAPI/Controllers/AuthController.cs
- [x] T082 [US8] Configure JWT authentication in Program.cs in backend/src/AAI.WebAPI/Program.cs

### Application - Profile

- [ ] T083 [US8] Create ExportDataQuery in backend/src/AAI.Application/UserProfile/Queries/ExportData/
- [ ] T084 [US8] Create ImportDataCommand in backend/src/AAI.Application/UserProfile/Commands/ImportData/
- [ ] T085 [US8] Create DeleteAllDataCommand in backend/src/AAI.Application/UserProfile/Commands/DeleteAllData/

### WebAPI - Profile (Security)

- [ ] T086 [US8] Create ProfileController export/import/delete endpoints in backend/src/AAI.WebAPI/Controllers/ProfileController.cs

### Frontend - Auth

- [x] T087 [US8] Create AuthProvider context in frontend/src/app/providers/AuthProvider.tsx
- [x] T088 [US8] Create useAuth hook in frontend/src/features/auth/hooks/useAuth.ts
- [x] T089 [US8] Create authApi service in frontend/src/features/auth/api/authApi.ts
- [x] T090 [US8] Create LoginForm component in frontend/src/features/auth/components/LoginForm.tsx
- [x] T091 [US8] Create PinSetup component in frontend/src/features/auth/components/PinSetup.tsx
- [x] T092 [US8] Create encryption utility for local storage in frontend/src/shared/utils/encryption.ts
- [x] T093 [US8] Create localStorageService with encryption in frontend/src/services/storage/localStorageService.ts

**Checkpoint**: User Story 8 completa - autenticação e segurança de dados funcionais

---

## Phase 4: User Story 2 - Configuração de Perfil de Risco e Metas (Priority: P1)

**Goal**: Usuário pode definir perfil de risco, objetivos de investimento e thresholds de rebalanceamento

**Independent Test**: Configurar diferentes perfis (conservador, moderado, agressivo) e verificar se thresholds são salvos corretamente

### Application - UserProfile

- [x] T094 [US2] Create GetUserProfileQuery in backend/src/AAI.Application/UserProfile/Queries/GetUserProfile/
- [x] T095 [US2] Create UpdateRiskProfileCommand in backend/src/AAI.Application/UserProfile/Commands/UpdateRiskProfile/
- [x] T096 [US2] Create UpdateThresholdsCommand in backend/src/AAI.Application/UserProfile/Commands/UpdateThresholds/
- [x] T097 [P] [US2] Create UserProfileDTOs in backend/src/AAI.Application/UserProfile/DTOs/

### WebAPI - Profile

- [x] T098 [US2] Add GET /profile and PUT /profile endpoints to ProfileController in backend/src/AAI.WebAPI/Controllers/ProfileController.cs

### Frontend - Profile

- [x] T099 [US2] Create profile types in frontend/src/features/profile/types/profile.ts
- [x] T100 [US2] Create profileApi service in frontend/src/features/profile/api/profileApi.ts
- [x] T101 [US2] Create useProfile hook in frontend/src/features/profile/hooks/useProfile.ts
- [x] T102 [US2] Create ProfileSettings component in frontend/src/features/profile/components/ProfileSettings.tsx
- [x] T103 [P] [US2] Create RiskProfileSelector component in frontend/src/features/profile/components/RiskProfileSelector.tsx
- [x] T104 [P] [US2] Create ThresholdConfig component in frontend/src/features/profile/components/ThresholdConfig.tsx

**Checkpoint**: User Story 2 completa - configuração de perfil funcional

---

## Phase 5: User Story 1 - Visualização do Dashboard de Portfólio (Priority: P1) 🎯 MVP

**Goal**: Dashboard completo do portfólio com alocação atual, performance geral e identificação de oportunidades/problemas

**Independent Test**: Exibir dados mockados de portfólio com composição, alocação percentual e performance vs benchmarks

### Domain - Portfolio

- [x] T105 [P] [US1] Create Portfolio entity in backend/src/AAI.Domain/Entities/Portfolio.cs
- [x] T106 [P] [US1] Create Position entity in backend/src/AAI.Domain/Entities/Position.cs
- [x] T107 [P] [US1] Create Asset entity in backend/src/AAI.Domain/Entities/Asset.cs
- [x] T108 [P] [US1] Create Transaction entity in backend/src/AAI.Domain/Entities/Transaction.cs
- [x] T109 [P] [US1] Create Benchmark entity in backend/src/AAI.Domain/Entities/Benchmark.cs
- [x] T110 [P] [US1] Create BenchmarkValue entity in backend/src/AAI.Domain/Entities/BenchmarkValue.cs
- [x] T111 [P] [US1] Create PriceHistory entity in backend/src/AAI.Domain/Entities/PriceHistory.cs
- [x] T112 [P] [US1] Create IPortfolioRepository interface in backend/src/AAI.Domain/Interfaces/IPortfolioRepository.cs
- [x] T113 [P] [US1] Create IAssetRepository interface in backend/src/AAI.Domain/Interfaces/IAssetRepository.cs
- [x] T114 [P] [US1] Create IPositionRepository interface in backend/src/AAI.Domain/Interfaces/IPositionRepository.cs

### Infrastructure - Persistence

- [x] T115 [US1] Create PortfolioConfiguration in backend/src/AAI.Infrastructure/Persistence/Configurations/PortfolioConfiguration.cs
- [x] T116 [P] [US1] Create PositionConfiguration in backend/src/AAI.Infrastructure/Persistence/Configurations/PositionConfiguration.cs
- [x] T117 [P] [US1] Create AssetConfiguration in backend/src/AAI.Infrastructure/Persistence/Configurations/AssetConfiguration.cs
- [x] T118 [P] [US1] Create TransactionConfiguration in backend/src/AAI.Infrastructure/Persistence/Configurations/TransactionConfiguration.cs
- [x] T119 [P] [US1] Create BenchmarkConfiguration in backend/src/AAI.Infrastructure/Persistence/Configurations/BenchmarkConfiguration.cs
- [x] T120 [P] [US1] Create PriceHistoryConfiguration in backend/src/AAI.Infrastructure/Persistence/Configurations/PriceHistoryConfiguration.cs
- [x] T121 [US1] Create PortfolioRepository in backend/src/AAI.Infrastructure/Persistence/Repositories/PortfolioRepository.cs
- [x] T122 [P] [US1] Create AssetRepository in backend/src/AAI.Infrastructure/Persistence/Repositories/AssetRepository.cs
- [x] T123 [P] [US1] Create PositionRepository in backend/src/AAI.Infrastructure/Persistence/Repositories/PositionRepository.cs
- [x] T124 [US1] Add Portfolio migration in backend/src/AAI.Infrastructure/Persistence/Migrations/

### Application - Portfolio Queries

- [x] T125 [US1] Create GetPortfolioSummaryQuery in backend/src/AAI.Application/Portfolio/Queries/GetPortfolioSummary/
- [x] T126 [US1] Create GetAllocationBreakdownQuery in backend/src/AAI.Application/Portfolio/Queries/GetAllocationBreakdown/
- [x] T127 [US1] Create GetPerformanceMetricsQuery in backend/src/AAI.Application/Portfolio/Queries/GetPerformanceMetrics/
- [x] T128 [P] [US1] Create PortfolioDTOs in backend/src/AAI.Application/Portfolio/DTOs/

### Application - Position Commands

- [ ] T129 [US1] Create CreatePositionCommand in backend/src/AAI.Application/Portfolio/Commands/CreatePosition/
- [ ] T130 [US1] Create UpdatePositionCommand in backend/src/AAI.Application/Portfolio/Commands/UpdatePosition/
- [ ] T131 [US1] Create DeletePositionCommand in backend/src/AAI.Application/Portfolio/Commands/DeletePosition/
- [ ] T132 [US1] Create ImportTransactionsCommand in backend/src/AAI.Application/Portfolio/Commands/ImportTransactions/

### WebAPI - Portfolio

- [x] T133 [US1] Create PortfolioController in backend/src/AAI.WebAPI/Controllers/PortfolioController.cs
- [ ] T134 [US1] Create PositionsController in backend/src/AAI.WebAPI/Controllers/PositionsController.cs
- [ ] T135 [US1] Create AssetsController in backend/src/AAI.WebAPI/Controllers/AssetsController.cs
- [ ] T136 [US1] Create TransactionsController in backend/src/AAI.WebAPI/Controllers/TransactionsController.cs

### Infrastructure - Market Data

- [ ] T137 [US1] Create BrapiClient for B3 market data in backend/src/AAI.Infrastructure/ExternalServices/MarketData/BrapiClient.cs
- [ ] T138 [US1] Create YahooFinanceClient as fallback in backend/src/AAI.Infrastructure/ExternalServices/MarketData/YahooFinanceClient.cs
- [ ] T139 [US1] Implement MarketDataService with fallback logic in backend/src/AAI.Infrastructure/ExternalServices/MarketData/MarketDataService.cs
- [ ] T140 [US1] Create MarketDataUpdateService background job in backend/src/AAI.Infrastructure/BackgroundServices/MarketDataUpdateService.cs

### Frontend - Portfolio

- [x] T141 [US1] Create portfolio types in frontend/src/features/portfolio/types/portfolio.ts
- [x] T142 [US1] Create portfolioApi service in frontend/src/features/portfolio/api/portfolioApi.ts
- [x] T143 [US1] Create usePortfolio hook in frontend/src/features/portfolio/hooks/usePortfolio.ts
- [x] T144 [US1] Create usePositions hook in frontend/src/features/portfolio/hooks/usePositions.ts
- [x] T145 [US1] Create PortfolioDashboard component in frontend/src/features/portfolio/components/PortfolioDashboard.tsx
- [x] T146 [P] [US1] Create AllocationChart component with Recharts in frontend/src/features/portfolio/components/AllocationChart.tsx
- [x] T147 [P] [US1] Create PositionCard component in frontend/src/features/portfolio/components/PositionCard.tsx
- [x] T148 [P] [US1] Create PositionList component in frontend/src/features/portfolio/components/PositionList.tsx
- [x] T149 [P] [US1] Create PieChart wrapper component in frontend/src/shared/components/charts/PieChart.tsx
- [x] T150 [P] [US1] Create LineChart wrapper component in frontend/src/shared/components/charts/LineChart.tsx
- [x] T151 [P] [US1] Create BarChart wrapper component in frontend/src/shared/components/charts/BarChart.tsx

**Checkpoint**: User Story 1 completa - dashboard de portfólio funcional com alocação e performance

---

## Phase 6: User Story 4 - Recomendações de Rebalanceamento com IA (Priority: P1) 🎯 MVP

**Goal**: Recomendações inteligentes de rebalanceamento baseadas em análise de IA

**Independent Test**: Portfólio com desvios de alocação gera sugestões de compra/venda com justificativas

### Domain - Recommendation

- [x] T152 [P] [US4] Create Recommendation entity in backend/src/AAI.Domain/Entities/Recommendation.cs
- [x] T153 [P] [US4] Create IRecommendationRepository interface in backend/src/AAI.Domain/Interfaces/IRecommendationRepository.cs

### Infrastructure - Persistence

- [x] T154 [US4] Create RecommendationConfiguration in backend/src/AAI.Infrastructure/Persistence/Configurations/RecommendationConfiguration.cs
- [x] T155 [US4] Create RecommendationRepository in backend/src/AAI.Infrastructure/Persistence/Repositories/RecommendationRepository.cs

### Infrastructure - AI

- [ ] T156 [US4] Create OpenAIClient in backend/src/AAI.Infrastructure/ExternalServices/AI/OpenAIClient.cs
- [ ] T157 [US4] Create AnthropicClient in backend/src/AAI.Infrastructure/ExternalServices/AI/AnthropicClient.cs
- [ ] T158 [US4] Implement AIRecommendationService in backend/src/AAI.Infrastructure/ExternalServices/AI/AIRecommendationService.cs

### Application - Rebalancing

- [x] T159 [US4] Create GetRecommendationsQuery in backend/src/AAI.Application/Rebalancing/Queries/GetRecommendations/
- [x] T160 [US4] Create RequestRecommendationsCommand in backend/src/AAI.Application/Rebalancing/Commands/RequestRecommendations/
- [ ] T161 [US4] Create ApplyRecommendationCommand in backend/src/AAI.Application/Rebalancing/Commands/ApplyRecommendation/
- [ ] T162 [US4] Create RejectRecommendationCommand in backend/src/AAI.Application/Rebalancing/Commands/RejectRecommendation/
- [x] T163 [P] [US4] Create RebalancingDTOs in backend/src/AAI.Application/Rebalancing/DTOs/

### WebAPI - Rebalancing

- [x] T164 [US4] Create RebalancingController in backend/src/AAI.WebAPI/Controllers/RebalancingController.cs

### Frontend - Rebalancing

- [x] T165 [US4] Create rebalancing types in frontend/src/features/rebalancing/types/rebalancing.ts
- [x] T166 [US4] Create rebalancingApi service in frontend/src/features/rebalancing/api/rebalancingApi.ts
- [x] T167 [US4] Create useRecommendations hook in frontend/src/features/rebalancing/hooks/useRecommendations.ts
- [ ] T168 [US4] Create RecommendationPanel component in frontend/src/features/rebalancing/components/RecommendationPanel.tsx
- [ ] T169 [P] [US4] Create RecommendationCard component in frontend/src/features/rebalancing/components/RecommendationCard.tsx
- [ ] T170 [US4] Create ConsentModal for AI data sharing in frontend/src/features/rebalancing/components/ConsentModal.tsx

**Checkpoint**: User Story 4 completa - recomendações de IA funcionais com consentimento

---

## Phase 7: User Story 3 - Feed de Notícias com Análise por IA (Priority: P2)

**Goal**: Feed de notícias financeiras com resumos gerados por IA

**Independent Test**: Exibir notícias agregadas de fontes configuradas com resumos sintéticos gerados

### Domain - News

- [x] T171 [P] [US3] Create NewsItem entity in backend/src/AAI.Domain/Entities/NewsItem.cs
- [x] T172 [P] [US3] Create MarketEvent entity in backend/src/AAI.Domain/Entities/MarketEvent.cs
- [ ] T173 [P] [US3] Create INewsItemRepository interface in backend/src/AAI.Domain/Interfaces/INewsItemRepository.cs

### Infrastructure - Persistence

- [x] T174 [US3] Create NewsItemConfiguration in backend/src/AAI.Infrastructure/Persistence/Configurations/NewsItemConfiguration.cs
- [x] T175 [US3] Create MarketEventConfiguration in backend/src/AAI.Infrastructure/Persistence/Configurations/MarketEventConfiguration.cs
- [ ] T176 [US3] Create NewsItemRepository in backend/src/AAI.Infrastructure/Persistence/Repositories/NewsItemRepository.cs

### Infrastructure - News

- [ ] T177 [US3] Create NewsScraperService in backend/src/AAI.Infrastructure/ExternalServices/News/NewsScraperService.cs
- [ ] T178 [US3] Create NewsAggregatorService in backend/src/AAI.Infrastructure/ExternalServices/News/NewsAggregatorService.cs
- [ ] T179 [US3] Create NewsAggregationService background job in backend/src/AAI.Infrastructure/BackgroundServices/NewsAggregationService.cs

### Application - News

- [ ] T180 [US3] Create GetNewsFeedQuery in backend/src/AAI.Application/News/Queries/GetNewsFeed/
- [ ] T181 [US3] Create GetNewsForAssetQuery in backend/src/AAI.Application/News/Queries/GetNewsForAsset/
- [ ] T182 [US3] Create MarkNewsAsReadCommand in backend/src/AAI.Application/News/Commands/MarkNewsAsRead/
- [ ] T183 [P] [US3] Create NewsDTOs in backend/src/AAI.Application/News/DTOs/

### WebAPI - News

- [ ] T184 [US3] Create NewsController in backend/src/AAI.WebAPI/Controllers/NewsController.cs

### Frontend - News

- [ ] T185 [US3] Create news types in frontend/src/features/news/types/news.ts
- [ ] T186 [US3] Create newsApi service in frontend/src/features/news/api/newsApi.ts
- [ ] T187 [US3] Create useNews hook in frontend/src/features/news/hooks/useNews.ts
- [ ] T188 [US3] Create NewsFeed component in frontend/src/features/news/components/NewsFeed.tsx
- [ ] T189 [P] [US3] Create NewsCard component in frontend/src/features/news/components/NewsCard.tsx
- [ ] T190 [P] [US3] Create NewsFilters component in frontend/src/features/news/components/NewsFilters.tsx

**Checkpoint**: User Story 3 completa - feed de notícias funcional com resumos IA

---

## Phase 8: User Story 5 - Simulação de Cenários de Rebalanceamento (Priority: P2)

**Goal**: Simular cenários de rebalanceamento antes de executar operações

**Independent Test**: Selecionar uma recomendação e visualizar projeção antes/depois

### Application - Simulation

- [ ] T191 [US5] Create SimulateRebalancingQuery in backend/src/AAI.Application/Rebalancing/Queries/SimulateRebalancing/
- [ ] T192 [P] [US5] Create SimulationDTOs in backend/src/AAI.Application/Rebalancing/DTOs/SimulationDTOs.cs

### WebAPI - Simulation

- [ ] T193 [US5] Add POST /rebalancing/simulate endpoint to RebalancingController in backend/src/AAI.WebAPI/Controllers/RebalancingController.cs

### Frontend - Simulation

- [ ] T194 [US5] Create useSimulation hook in frontend/src/features/rebalancing/hooks/useSimulation.ts
- [ ] T195 [US5] Create SimulationView component in frontend/src/features/rebalancing/components/SimulationView.tsx
- [ ] T196 [US5] Create RebalancingWizard component in frontend/src/features/rebalancing/components/RebalancingWizard.tsx

**Checkpoint**: User Story 5 completa - simulação de rebalanceamento funcional

---

## Phase 9: User Story 6 - Alertas de Eventos de Mercado (Priority: P2)

**Goal**: Alertas sobre eventos significativos de mercado que afetam ativos do portfólio

**Independent Test**: Configurar alertas e verificar notificações quando condições são atendidas

### Domain - Alerts

- [x] T197 [P] [US6] Create Alert entity in backend/src/AAI.Domain/Entities/Alert.cs
- [x] T198 [P] [US6] Create AlertHistory entity in backend/src/AAI.Domain/Entities/AlertHistory.cs
- [ ] T199 [P] [US6] Create IAlertRepository interface in backend/src/AAI.Domain/Interfaces/IAlertRepository.cs

### Infrastructure - Persistence

- [x] T200 [US6] Create AlertConfiguration in backend/src/AAI.Infrastructure/Persistence/Configurations/AlertConfiguration.cs
- [x] T201 [US6] Create AlertHistoryConfiguration in backend/src/AAI.Infrastructure/Persistence/Configurations/AlertHistoryConfiguration.cs
- [ ] T202 [US6] Create AlertRepository in backend/src/AAI.Infrastructure/Persistence/Repositories/AlertRepository.cs

### Infrastructure - Alerts

- [ ] T203 [US6] Create AlertMonitoringService background job in backend/src/AAI.Infrastructure/BackgroundServices/AlertMonitoringService.cs
- [ ] T203.Tests [P] [US6] Create unit tests for price variation detection logic (≥5% threshold) in backend/tests/AAI.Infrastructure.Tests/BackgroundServices/AlertMonitoringServiceTests.cs
- [ ] T204 [US6] Create NotificationHub for SignalR in backend/src/AAI.WebAPI/Hubs/NotificationHub.cs
- [ ] T205 [US6] Implement NotificationService with SignalR in backend/src/AAI.Infrastructure/Notifications/NotificationService.cs

### Application - Alerts

- [ ] T206 [US6] Create GetAlertsQuery in backend/src/AAI.Application/Alerts/Queries/GetAlerts/
- [ ] T207 [US6] Create GetAlertHistoryQuery in backend/src/AAI.Application/Alerts/Queries/GetAlertHistory/
- [ ] T208 [US6] Create CreateAlertCommand in backend/src/AAI.Application/Alerts/Commands/CreateAlert/
- [ ] T209 [US6] Create DeleteAlertCommand in backend/src/AAI.Application/Alerts/Commands/DeleteAlert/
- [ ] T210 [P] [US6] Create AlertDTOs in backend/src/AAI.Application/Alerts/DTOs/

### WebAPI - Alerts

- [ ] T211 [US6] Create AlertsController in backend/src/AAI.WebAPI/Controllers/AlertsController.cs

### Frontend - Alerts & SignalR

- [ ] T212 [US6] Create notificationClient with SignalR in frontend/src/services/signalr/notificationClient.ts
- [ ] T213 [US6] Create useNotification hook in frontend/src/shared/hooks/useNotification.ts
- [ ] T214 [US6] Create AlertSettings component in frontend/src/features/alerts/components/AlertSettings.tsx
- [ ] T215 [P] [US6] Create AlertHistoryList component in frontend/src/features/alerts/components/AlertHistoryList.tsx

**Checkpoint**: User Story 6 completa - alertas e notificações em tempo real funcionais

---

## Phase 10: User Story 7 - Histórico e Analytics de Performance (Priority: P2)

**Goal**: Histórico detalhado de performance do portfólio com analytics avançados

**Independent Test**: Exibir dados históricos com gráficos de evolução patrimonial e comparativos

### Application - Analytics

- [ ] T216 [US7] Create GetHistoricalPerformanceQuery in backend/src/AAI.Application/Analytics/Queries/GetHistoricalPerformance/
- [ ] T217 [US7] Create GetRiskMetricsQuery in backend/src/AAI.Application/Analytics/Queries/GetRiskMetrics/
- [ ] T218 [US7] Create GetBenchmarkComparisonQuery in backend/src/AAI.Application/Analytics/Queries/GetBenchmarkComparison/
- [ ] T219 [P] [US7] Create AnalyticsDTOs in backend/src/AAI.Application/Analytics/DTOs/

### Application - Transaction History

- [ ] T220 [US7] Create GetTransactionHistoryQuery in backend/src/AAI.Application/Portfolio/Queries/GetTransactionHistory/

### WebAPI - Analytics

- [ ] T221 [US7] Create AnalyticsController in backend/src/AAI.WebAPI/Controllers/AnalyticsController.cs

### Frontend - Analytics

- [ ] T222 [US7] Create analytics types in frontend/src/features/analytics/types/analytics.ts
- [ ] T223 [US7] Create analyticsApi service in frontend/src/features/analytics/api/analyticsApi.ts
- [ ] T224 [US7] Create useAnalytics hook in frontend/src/features/analytics/hooks/useAnalytics.ts
- [ ] T225 [US7] Create PerformanceChart component in frontend/src/features/analytics/components/PerformanceChart.tsx
- [ ] T226 [P] [US7] Create BenchmarkComparison component in frontend/src/features/analytics/components/BenchmarkComparison.tsx
- [ ] T227 [P] [US7] Create RiskMetrics component in frontend/src/features/analytics/components/RiskMetrics.tsx
- [ ] T228 [P] [US7] Create HistoricalView component in frontend/src/features/analytics/components/HistoricalView.tsx

**Checkpoint**: User Story 7 completa - analytics de performance funcionais

---

## Phase 11: Polish & Cross-Cutting Concerns

**Purpose**: Melhorias que afetam múltiplas user stories

### Seed Data & Validation

- [ ] T229 [P] Add seed data for benchmarks (IBOV, CDI, IPCA+) in backend/src/AAI.Infrastructure/Persistence/Seeds/
- [ ] T230 [P] Add seed data for common B3 assets in backend/src/AAI.Infrastructure/Persistence/Seeds/
- [ ] T231 [P] Configure OpenAPI validation with Spectral/Prism in .github/workflows/api-validation.yml
- [ ] T231.1 [P] Create integration tests validating API responses match contracts/api.yaml schemas in backend/tests/AAI.WebAPI.Tests/ContractValidationTests.cs
- [ ] T232 [P] Ensure all frontend components follow design token system

### Security & Performance (Constitution Compliance)

- [ ] T233 Implement SQLite encryption at rest with AES-256 in backend/src/AAI.Infrastructure/Persistence/EncryptedDbContext.cs
- [ ] T234 Security hardening - review all sensitive data handling, validate encryption implementation
- [ ] T235 Performance optimization - add indexes per data-model.md
- [ ] T236 Implement tax impact calculation for rebalancing simulation in backend/src/AAI.Application/Rebalancing/Services/TaxCalculationService.cs

### Accessibility (Constitution §III - WCAG 2.1 AA)

- [ ] T237 Configure axe-core accessibility testing in frontend/tests/a11y/
- [ ] T238 [P] Add keyboard navigation tests for all interactive components
- [ ] T239 [P] Add ARIA labels and roles to all UI components
- [ ] T240 [P] Implement skip-to-content and focus management
- [ ] T241 Run accessibility audit and fix violations (target: 0 critical/serious)

### Performance Monitoring (Constitution §IV)

- [ ] T242 Configure Lighthouse CI in GitHub Actions workflow in .github/workflows/lighthouse.yml
- [ ] T243 Set performance budgets: TTI < 3s, FCP < 1.5s, Lighthouse Score ≥ 90

### Final Validation

- [ ] T244 Run quickstart.md validation to ensure project runs correctly
- [ ] T245 Run full test suite and verify coverage ≥ 80% for Domain/Application layers

### E2E Tests (Constitution §II - P1 Journeys)

- [ ] T246 [P] [E2E] Configure Playwright for E2E testing in frontend/tests/e2e/playwright.config.ts
- [ ] T247 [E2E] Create E2E test: Login → Dashboard load → Portfolio visualization in frontend/tests/e2e/dashboard.spec.ts
- [ ] T248 [E2E] Create E2E test: Profile configuration → Risk profile save → Threshold configuration in frontend/tests/e2e/profile.spec.ts
- [ ] T249 [E2E] Create E2E test: Request AI recommendation → Consent modal → View recommendation details in frontend/tests/e2e/recommendations.spec.ts
- [ ] T250 [E2E] Create E2E test: Add position → View in dashboard → Edit position in frontend/tests/e2e/positions.spec.ts

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - pode começar imediatamente
- **Foundational (Phase 2)**: Depende de Setup - BLOQUEIA todas user stories
- **User Stories (Phase 3+)**: Todas dependem de Foundational estar completa
  - User stories P1 devem ser completadas primeiro (ordem: US8 → US2 → US1 → US4)
  - User stories P2 podem ser paralelizadas após P1 (US3, US5, US6, US7)
- **Polish (Phase 11)**: Depende de todas user stories desejadas estarem completas

### User Story Dependencies

- **US8 (Segurança)**: Primeira a implementar - outras stories dependem de auth
- **US2 (Perfil)**: Depende de US8 (auth) - configurações de usuário
- **US1 (Dashboard)**: Depende de US8 e US2 - core da aplicação
- **US4 (Recomendações IA)**: Depende de US1 (precisa de dados de portfólio)
- **US3 (Notícias)**: Pode começar após Foundational - parcialmente independente
- **US5 (Simulação)**: Depende de US4 (usa recomendações)
- **US6 (Alertas)**: Depende de US1 (monitora posições)
- **US7 (Analytics)**: Depende de US1 (usa dados históricos de portfólio)

### Within Each User Story

- Domain entities antes de repositories
- Repositories antes de services
- Services antes de controllers
- Backend antes de frontend (para cada funcionalidade)

### Parallel Opportunities

- Todas as tasks Setup marcadas [P] podem rodar em paralelo
- Todas as tasks Foundational marcadas [P] podem rodar em paralelo (dentro da Phase 2)
- Após Foundational, tasks de diferentes user stories marcadas [P] podem rodar em paralelo
- Entities do Domain marcadas [P] podem rodar em paralelo
- Configurations do EF Core marcadas [P] podem rodar em paralelo
- Componentes frontend marcados [P] podem rodar em paralelo

---

## Parallel Example: User Story 1

```bash
# Launch all Domain entities together:
Task: "Create Portfolio entity in backend/src/AAI.Domain/Entities/Portfolio.cs"
Task: "Create Position entity in backend/src/AAI.Domain/Entities/Position.cs"
Task: "Create Asset entity in backend/src/AAI.Domain/Entities/Asset.cs"
Task: "Create Transaction entity in backend/src/AAI.Domain/Entities/Transaction.cs"

# Launch all EF Configurations together:
Task: "Create PortfolioConfiguration in backend/src/AAI.Infrastructure/..."
Task: "Create PositionConfiguration in backend/src/AAI.Infrastructure/..."
Task: "Create AssetConfiguration in backend/src/AAI.Infrastructure/..."

# Launch all frontend charts together:
Task: "Create PieChart wrapper in frontend/src/shared/components/charts/PieChart.tsx"
Task: "Create LineChart wrapper in frontend/src/shared/components/charts/LineChart.tsx"
Task: "Create BarChart wrapper in frontend/src/shared/components/charts/BarChart.tsx"
```

---

## Implementation Strategy

### MVP First (P1 User Stories Only)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational (CRITICAL - blocks all stories)
3. Complete Phase 3: User Story 8 (Security/Auth)
4. Complete Phase 4: User Story 2 (Profile)
5. Complete Phase 5: User Story 1 (Dashboard)
6. Complete Phase 6: User Story 4 (AI Recommendations)
7. **STOP and VALIDATE**: Test all P1 stories independently
8. Deploy/demo if ready - MVP completo!

### Incremental Delivery

1. Complete Setup + Foundational → Foundation ready
2. Add US8 (Auth) → Test → Secure base
3. Add US2 (Profile) → Test → User configured
4. Add US1 (Dashboard) → Test → Core MVP visible
5. Add US4 (Recommendations) → Test → AI value delivered (MVP COMPLETE!)
6. Add US3, US5, US6, US7 → Full feature set

### Parallel Team Strategy

Com múltiplos desenvolvedores após Foundational:
- **Dev A (Backend Focus)**: Domain + Infrastructure
- **Dev B (Frontend Focus)**: Components + Hooks
- **Dev C (Integration Focus)**: APIs + Services

---

## Summary

| Phase | Stories | Task Count | Priority |
|-------|---------|------------|----------|
| Phase 1 | Setup | 11 | - |
| Phase 2 | Foundational | 57 | - |
| Phase 3 | US8 (Security) | 25 | P1 |
| Phase 4 | US2 (Profile) | 10 | P1 |
| Phase 5 | US1 (Dashboard) | 47 | P1 |
| Phase 6 | US4 (AI Recs) | 19 | P1 |
| Phase 7 | US3 (News) | 20 | P2 |
| Phase 8 | US5 (Simulation) | 6 | P2 |
| Phase 9 | US6 (Alerts) | 20 | P2 |
| Phase 10 | US7 (Analytics) | 13 | P2 |
| Phase 11 | Polish & Compliance | 24 | - |
| **Total** | | **252** | |

### MVP Scope (P1 Only)

- Phases 1-6: 169 tasks
- Delivers: Auth, Profile, Dashboard, AI Recommendations

### Full Feature Set

- All phases: 252 tasks
- Adds: News, Simulation, Alerts, Analytics, Polish, Accessibility, Performance Monitoring, E2E Tests

---

## Notes

- [P] tasks = arquivos diferentes, sem dependências
- [Story] label mapeia task à user story específica para rastreabilidade
- Cada user story deve ser independentemente completável e testável
- Commit após cada task ou grupo lógico
- Pare em qualquer checkpoint para validar story independentemente
- Evitar: tasks vagas, conflitos de mesmo arquivo, dependências cross-story que quebram independência
