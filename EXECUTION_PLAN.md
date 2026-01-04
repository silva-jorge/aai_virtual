# Plano de Execução Otimizado - AI Portfolio Manager

**Data**: 2026-01-02  
**Objetivo**: Implementar todas as 125 tarefas pendentes com otimização de paralelização  
**Estratégia**: Modelos menores para tarefas simples, modelos poderosos para complexas  

## Status Geral

- **Total de Tarefas**: 252
- **Completadas**: 127 ✅
- **Pendentes**: 125 ⏳
- **Conclusão Atual**: 50.4%

## Distribuição por Fase

| Fase | Descrição | Total | Pendentes | Status |
|------|-----------|-------|-----------|--------|
| 1 | Setup | 11 | 0 | ✅ COMPLETA |
| 2 | Foundation | 57 | 7 | 🔄 QUASE PRONTA |
| 3 | US8 (Auth) | 25 | 10 | 🔄 EM PROGRESSO |
| 4 | US2 (Profile) | 10 | 3 | 🔄 EM PROGRESSO |
| 5 | US1 (Dashboard) | 47 | 33 | ⏳ PENDENTE |
| 6 | US4 (AI Recs) | 19 | 8 | ⏳ PENDENTE |
| 7+ | P2 Stories | 83 | 64 | ⏳ BLOQUEADA |

## Próximos Passos - Ordem de Execução

### 1️⃣ FASE 2 (Foundation) - 7 tarefas bloqueadoras
**Modelo**: Claude 4 (tarefas técnicas)
**Tempo Estimado**: 1-2 horas
- T029: AutoMapper MappingProfile (simples)
- T061-064: Layout components (4 tarefas paralelas, simples)
- T065, T067: Formatters & types (2 tarefas paralelas, simples)

### 2️⃣ PHASE 3 (User Story 8 - Auth/Security) - 10 tarefas
**Modelo**: Claude 4.5 (segurança é crítica)
**Tempo Estimado**: 3-4 horas
- Backend: Commands (SetupPassword, RefreshToken, ChangePassword)
- Frontend: LoginForm, PinSetup, encryption, storage service

### 3️⃣ PHASE 4 (User Story 2 - Profile) - 3 tarefas
**Modelo**: Claude 3.5 Sonnet (padrão frontend)
**Tempo Estimado**: 1-2 horas
- Frontend: ProfileSettings, RiskProfileSelector, ThresholdConfig

### 4️⃣ PHASE 5 (User Story 1 - Dashboard MVP) - 33 tarefas MAIORES
**Modelo**: Mix estratégico
**Tempo Estimado**: 6-8 horas
- Domain & Infrastructure (simples): Claude 3.5 Sonnet
- Application Commands/Queries (complexo): Claude 4.5
- Frontend Components (padrão): Claude 3.5 Sonnet
- Market Data Services (integração): Claude 4.5

### 5️⃣ PHASE 6 (User Story 4 - AI Recommendations) - 8 tarefas
**Modelo**: Claude 4.5 (complexidade de IA)
**Tempo Estimado**: 2-3 horas

## Estratégia de Paralelização

### Batch 1 (Foundation Finalization) - IMEDIATO
```
[PARALELO] T061-064 (Layout components)
[PARALELO] T065, T067 (Utilities)
[SEQUENCIAL] T029 (AutoMapper - depende de outros)
```

### Batch 2 (Auth Implementation) - APÓS Batch 1
```
[PARALELO] T078 (AuthDTOs - depende de T029)
[PARALELO] T074, T076, T077 (Commands)
[PARALELO] T090, T091 (Components)
[PARALELO] T092, T093 (Encryption & Storage)
```

### Batch 3 (Profile) - APÓS Batch 2 completo
```
[PARALELO] T103, T104 (RiskProfileSelector, ThresholdConfig)
[SEQUENCIAL] T102 (ProfileSettings - use T103/T104)
```

### Batch 4 (Dashboard) - MAIOR complexidade
**Fase 4a: Domain & Repos (simples)**
```
[PARALELO] T114 (IPositionRepository interface)
[PARALELO] T123 (PositionRepository)
```

**Fase 4b: Application Layer (complexo)**
```
[PARALELO] T129, T130, T131, T132 (Position Commands)
```

**Fase 4c: WebAPI Controllers (padrão)**
```
[PARALELO] T134, T135, T136 (Controllers)
```

**Fase 4d: Market Data Services (integração)**
```
[PARALELO] T137, T138 (BrapiClient, YahooFinanceClient)
[SEQUENCIAL] T139 (MarketDataService - usa acima)
[SEQUENCIAL] T140 (Background Job)
```

**Fase 4e: Frontend Components (padrão)**
```
[PARALELO] T146-151 (Chart components + PositionCard/List)
[SEQUENCIAL] T144 (usePositions hook)
[SEQUENCIAL] T145 (PortfolioDashboard - compõe acima)
```

### Batch 5 (AI Recommendations)
```
[PARALELO] T156, T157 (OpenAI, Anthropic clients)
[SEQUENCIAL] T158 (AIRecommendationService)
[PARALELO] T161, T162 (Commands)
[PARALELO] T169 (RecommendationCard)
[SEQUENCIAL] T168 (RecommendationPanel)
```

## Alocação Inteligente de Modelos

### Claude 4.5 (Tarefas Complexas/Críticas)
- Segurança: T074, T076, T077, T092, T093 (Auth)
- Integração: T139, T140, T158 (Market Data, AI)
- CQRS Complexo: T129-T132 (Position Commands)
- Controladores: T134-T136 (WebAPI)

### Claude 3.5 Sonnet (Tarefas Padrão)
- Layout & Componentes: T061-064, T090, T091, T103, T104, T145
- Utilities: T065, T067, T078 (AuthDTOs)
- Repositories: T114, T123
- Chart Wrappers: T146-T151
- Card Components: T169

### Claude Haiku/Sonnet (Tarefas Triviais)
- AutoMapper: T029
- Clients simples: T156, T157

## Checkpoints de Validação

1. ✅ **Após Phase 2**: Compilação backend + builds frontend
2. 🔐 **Após Phase 3 (Auth)**: Login funcional, passwords hasheados
3. 👤 **Após Phase 4 (Profile)**: Perfil de risco configurável
4. 📊 **Após Phase 5 (Dashboard)**: Dashboard carrega dados mockados
5. 🤖 **Após Phase 6 (AI)**: Recomendações geradas com consentimento
6. ✨ **Final**: MVP completo com E2E tests P1

## Estimativa de Tempo

| Batch | Tarefas | Tempo | Modelo |
|-------|---------|-------|--------|
| 1 | 7 | 1h | Claude 3.5/4 |
| 2 | 10 | 2h | Claude 4.5 |
| 3 | 3 | 1h | Claude 3.5 |
| 4a | 2 | 30m | Claude 3.5 |
| 4b | 4 | 2h | Claude 4.5 |
| 4c | 3 | 1h | Claude 3.5 |
| 4d | 4 | 2h | Claude 4.5 |
| 4e | 6 | 2h | Claude 3.5 |
| 5 | 8 | 2h | Claude 4.5 |
| **TOTAL** | **47** | **13.5h** | **Mix** |

## Comandos de Execução Recomendados

```bash
# Batch 1 - Foundation
# Paralelos: T061-064 (4 componentes layout)
# Paralelos: T065, T067 (formatters, types)
# Sequencial: T029 (AutoMapper depois de tudo)

# Batch 2 - Auth
# Paralelos: T074, T076, T077 (Commands)
# Paralelos: T090, T091, T092, T093 (Frontend)
# Paralelos: T078 (DTOs)

# Batch 3 - Profile
# Paralelos: T103, T104 (Components)
# Sequencial: T102 (ProfileSettings)

# Batch 4 - Dashboard
# ... (maior volume, requer mais cuidado)

# Batch 5 - AI
# ... (integração complexa)
```

## Notas Importantes

1. **Dependências Respeitadas**: Phase 2 → Phase 3+ (não pode iniciar US stories sem Foundation)
2. **Compilação**: Verificar após cada batch para pegar erros cedo
3. **Testes**: Incluídos nas tarefas [Tests] onde aplicável
4. **Commits**: Um commit por task ou por batch lógico
5. **Paralelização**: Máximo 4-6 tarefas simultâneas com IA para manter qualidade

