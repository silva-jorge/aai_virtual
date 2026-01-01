# Research: AI Portfolio Manager

**Feature**: 001-ai-portfolio-manager  
**Date**: 2026-01-01  
**Status**: Completed

## Overview

Este documento consolida as decisões de pesquisa técnica para o AI Portfolio Manager. Cada decisão inclui rationale, alternativas consideradas e referências.

---

## 1. Arquitetura Backend

### Decision: Clean Architecture com CQRS

**Escolha**: C# .NET 8 com Clean Architecture (4 layers) + CQRS via MediatR

**Rationale**:
- Clean Architecture permite separação clara de responsabilidades e testabilidade
- CQRS é ideal para este domínio onde queries (leitura de portfólio, analytics) são muito mais frequentes que commands (transações)
- MediatR fornece pipeline behaviors para cross-cutting concerns (validation, logging, caching)
- .NET 8 oferece performance nativa e excelente suporte a background services

**Alternativas Consideradas**:

| Alternativa | Prós | Contras | Por que rejeitada |
|-------------|------|---------|-------------------|
| Node.js + Express | Ecossistema JS unificado, rápido desenvolvimento | Tipagem mais fraca, menos estrutura para projetos complexos | Domínio financeiro requer tipagem forte e validações rigorosas |
| Python + FastAPI | Bom para ML/AI, rápido desenvolvimento | Performance inferior, menos suporte a real-time nativo | .NET oferece SignalR nativo e melhor performance |
| .NET Minimal APIs | Menos boilerplate | Menos estrutura para projetos grandes | Clean Architecture fornece melhor organização para 38+ requisitos funcionais |

**References**:
- [Clean Architecture - Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern - Microsoft](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [MediatR Documentation](https://github.com/jbogard/MediatR)

---

## 2. Frontend Architecture

### Decision: React com Feature-based Organization

**Escolha**: React 18 + TypeScript + React Query + React Router

**Rationale**:
- React Query gerencia server state com caching inteligente, ideal para dados de mercado que atualizam periodicamente
- Hooks e Context API são suficientes para estado local, evitando overhead de Redux
- Feature-based organization agrupa código relacionado, facilitando navegação e manutenção
- TypeScript garante type safety end-to-end com DTOs do backend

**Alternativas Consideradas**:

| Alternativa | Prós | Contras | Por que rejeitada |
|-------------|------|---------|-------------------|
| Next.js | SSR, file-based routing | Overhead para SPA local, complexidade de deploy | Aplicação é single-user local, SSR não agrega valor |
| Vue.js | Curva de aprendizado menor | Ecossistema menor para dashboards financeiros | React tem mais bibliotecas para charts e tabelas financeiras |
| Redux Toolkit | Estado global robusto | Boilerplate, overhead para aplicação single-user | React Query + Context cobre os casos de uso com menos código |

**Key Libraries**:
- **React Query**: Server state, caching, background refetching
- **React Router**: Client-side routing com lazy loading
- **Recharts**: Gráficos leves baseados em SVG
- **CSS Modules**: Scoped styling sem runtime overhead

---

## 3. Database Strategy

### Decision: SQLite com Entity Framework Core

**Escolha**: SQLite como banco local + EF Core 8 como ORM

**Rationale**:
- SQLite é self-contained, zero-config, ideal para aplicação single-user local
- EF Core oferece migrations, strong typing, e LINQ para queries complexas
- Suporte nativo a criptografia via SQLite extensions (SQLCipher) para dados sensíveis
- Performance adequada para volume esperado (10k+ transações, 1k+ ativos)

**Alternativas Consideradas**:

| Alternativa | Prós | Contras | Por que rejeitada |
|-------------|------|---------|-------------------|
| PostgreSQL | Features avançadas, melhor para concorrência | Requer instalação separada, overhead para single-user | SQLite é suficiente e mais simples |
| LiteDB | NoSQL .NET nativo | Menos maduro, queries menos expressivas | EF Core com SQLite é mais estabelecido |
| IndexedDB (browser) | Zero backend para storage | Limitações de query, dados presos no browser | Backend permite backup centralizado e processamento pesado |

**Data Encryption Strategy**:
- Dados sensíveis (valores de posições, transações) criptografados em repouso
- Chave derivada de senha do usuário via PBKDF2
- SQLite com extensão de criptografia AES-256

---

## 4. Market Data Integration

### Decision: Multi-source with Fallback

**Escolha**: Brapi (primário) + Yahoo Finance (fallback) + Alpha Vantage (supplemental)

**Rationale**:
- Brapi é especializada em B3, oferece API gratuita com dados adequados
- Yahoo Finance como fallback global e para ativos internacionais
- Alpha Vantage para dados fundamentalistas complementares
- Delay de 15-20 minutos é aceitável para estratégia de long-term investing

**API Integration Pattern**:
```
MarketDataService
├── IBrapiClient (primary - B3 assets)
├── IYahooFinanceClient (fallback - all assets)
└── IAlphaVantageClient (fundamentals)
```

**Caching Strategy**:
- Cotações: In-memory cache com TTL de 15 minutos
- Dados fundamentalistas: Cache em SQLite com TTL de 24 horas
- Fallback automático se API primária falhar

**Rate Limiting Considerations**:
- Brapi: ~5 req/min no plano gratuito → Batch requests, cache agressivo
- Yahoo Finance: Limites variáveis → Implementar retry with exponential backoff
- Alpha Vantage: 5 calls/min, 500/day → Usar apenas para dados que Brapi não fornece

---

## 5. AI/LLM Integration

### Decision: External LLM APIs with Consent Flow

**Escolha**: OpenAI GPT-4 (primary) + Anthropic Claude (alternative)

**Rationale**:
- APIs cloud são mais práticas que modelos locais para capacidade de análise necessária
- GPT-4 tem forte capacidade de análise de texto financeiro
- Anthropic Claude como alternativa para failover ou preferência do usuário
- Consentimento explícito garante compliance com requisitos de privacidade

**Integration Flow**:
```
1. User solicita recomendação
2. Sistema exibe dados que serão enviados
3. User confirma consentimento
4. Backend envia contexto (posições, notícias, perfil) para LLM
5. Resposta estruturada é parseada e exibida
```

**Prompt Engineering Strategy**:
- System prompt com persona de analista financeiro
- Context window otimizado: resumo de posições + notícias relevantes
- Output structure definido via JSON schema
- Temperature baixa (0.3) para respostas mais determinísticas

**Cost Optimization**:
- Cache de análises por 24 horas para mesmos inputs
- Batch de notícias para análise única
- Resumo progressivo para caber em context window

---

## 6. Real-time Notifications

### Decision: SignalR for Push Notifications

**Escolha**: SignalR Hub para notificações em tempo real

**Rationale**:
- SignalR é nativo do .NET, integração seamless
- Suporta WebSocket com fallback para long polling
- Ideal para alertas de mercado que requerem entrega imediata
- Permite broadcasting para múltiplas abas/sessões do mesmo usuário

**Notification Types**:
| Tipo | Trigger | Urgência |
|------|---------|----------|
| Price Alert | Variação > threshold configurado | Alta |
| Market Event | Fato relevante publicado | Alta |
| Rebalancing Opportunity | Alocação desviou > threshold | Média |
| News Update | Nova notícia relevante | Baixa |
| Data Sync | Cotações atualizadas | Baixa |

**Frontend Integration**:
- React context para connection state
- Toast notifications para alertas
- Badge counters para notificações não lidas

---

## 6.1. News Sources Configuration

### Decision: Multi-source News Aggregation

**Escolha**: Fontes públicas brasileiras com fallback internacional

**Fontes Primárias (Brasil)**:

| Fonte | Tipo | Rate Limit | Formato |
|-------|------|------------|---------|
| InfoMoney RSS | RSS Feed | Sem limite | XML/RSS 2.0 |
| Valor Econômico RSS | RSS Feed | Sem limite | XML/RSS 2.0 |
| Investing.com BR | RSS Feed | Sem limite | XML/RSS 2.0 |
| B3 Fatos Relevantes | Web Scraping | ~1 req/min | HTML parsing |
| CVM Documentos | API REST | ~10 req/min | JSON |

**Fontes Secundárias (Internacional)**:

| Fonte | Tipo | Rate Limit | Formato |
|-------|------|------------|---------|
| Yahoo Finance News | API | 100 req/day | JSON |
| Alpha Vantage News | API | 5 req/min | JSON |
| Google News RSS | RSS Feed | Sem limite | XML/RSS 2.0 |

**Estratégia de Agregação**:
- Polling a cada 30 minutos para RSS feeds
- Fatos relevantes B3: polling a cada 15 minutos durante pregão
- Deduplicação por URL e título similaridade (>90%)
- Priorização por relevância ao portfólio do usuário

**Rate Limiting Safeguards**:
- Queue com exponential backoff para APIs com limite
- Cache de 1 hora para notícias já processadas
- Circuit breaker para fontes com falhas recorrentes

---

## 7. Background Services

### Decision: IHostedService for Periodic Tasks

**Escolha**: .NET IHostedService / BackgroundService para tasks periódicas

**Rationale**:
- Integração nativa com .NET DI container
- Lifecycle gerenciado pelo host (graceful shutdown)
- Simples de testar e debugar
- Não requer infraestrutura externa (Redis, message queues)

**Services Planned**:

| Service | Frequency | Purpose |
|---------|-----------|---------|
| MarketDataUpdateService | 15 min | Atualiza cotações de ativos do portfólio |
| NewsAggregationService | 30 min | Coleta e processa notícias de fontes configuradas |
| AlertMonitoringService | 5 min | Verifica condições de alerta (variação, threshold) |
| FinancialStatementsService | 24 h | Atualiza dados de balanços e DREs |

**Error Handling**:
- Retry com exponential backoff para falhas de API
- Circuit breaker para APIs com falhas recorrentes
- Logging estruturado via Serilog para debugging

---

## 8. Authentication & Security

### Decision: JWT Local Authentication

**Escolha**: JWT para autenticação entre frontend e backend + PIN/senha local

**Rationale**:
- Aplicação é local/single-user, não precisa de IdP externo
- JWT permite stateless auth entre SPA e API
- PIN/senha protege acesso aos dados sensíveis
- Refresh tokens com rotação para segurança

**Security Measures**:
- Senha hashada com Argon2id
- JWT com short expiry (15 min) + refresh token (7 dias)
- HTTPS obrigatório (mesmo localhost)
- Rate limiting em endpoints de auth
- Dados sensíveis criptografados em repouso (AES-256)

**Key Derivation**:
```
User Password → PBKDF2 (100k iterations) → Master Key
Master Key → Database encryption key
Master Key → JWT signing key
```

---

## 9. Styling Strategy

### Decision: CSS Modules with Design Tokens

**Escolha**: CSS Modules + CSS Custom Properties (tokens)

**Rationale**:
- Zero runtime overhead vs CSS-in-JS
- Scoped by default, evita conflitos
- Custom Properties para theming e consistência
- Suporte nativo de browsers, sem build complexity

**Token System**:
```css
:root {
  /* Colors */
  --color-primary: #2563eb;
  --color-success: #16a34a;
  --color-danger: #dc2626;
  --color-warning: #d97706;
  
  /* Spacing */
  --space-xs: 0.25rem;
  --space-sm: 0.5rem;
  --space-md: 1rem;
  --space-lg: 1.5rem;
  --space-xl: 2rem;
  
  /* Typography */
  --font-sans: 'Inter', system-ui, sans-serif;
  --font-mono: 'JetBrains Mono', monospace;
  
  /* Shadows */
  --shadow-sm: 0 1px 2px rgba(0,0,0,0.05);
  --shadow-md: 0 4px 6px rgba(0,0,0,0.1);
}
```

**Component Patterns**:
- Base components em `shared/components/ui/`
- Feature-specific components colocated com features
- Composition over inheritance

---

## 10. Testing Strategy

### Decision: Comprehensive Testing Pyramid

**Escolha**: xUnit + Vitest + Playwright

**Rationale**:
- xUnit é padrão .NET, integração excelente com tooling
- Vitest é rápido e compatível com Vite
- Playwright para E2E cross-browser
- Cobertura mínima de 80% para lógica de negócio

**Test Distribution**:
```
E2E (Playwright)     ~10%  - Jornadas P1 críticas
Integration          ~20%  - APIs, DB, external services
Unit                 ~70%  - Domain logic, handlers, components
```

**Backend Test Libraries**:
- xUnit: Test framework
- FluentAssertions: Readable assertions
- NSubstitute: Mocking
- TestContainers: Integration tests com SQLite

**Frontend Test Libraries**:
- Vitest: Unit tests
- React Testing Library: Component tests
- MSW: API mocking
- Playwright: E2E tests

---

## Summary of Key Decisions

| Area | Decision | Confidence |
|------|----------|------------|
| Backend Framework | .NET 8 + Clean Architecture | High |
| CQRS Implementation | MediatR | High |
| Frontend Framework | React 18 + TypeScript | High |
| Server State | React Query | High |
| Database | SQLite + EF Core | High |
| Market Data | Brapi + Yahoo Finance | Medium (API stability) |
| AI Integration | OpenAI GPT-4 | Medium (cost/availability) |
| Real-time | SignalR | High |
| Background Jobs | IHostedService | High |
| Auth | JWT local | High |
| Styling | CSS Modules | High |
| Testing | xUnit + Vitest + Playwright | High |

---

## Open Questions Resolved

1. ✅ **Como lidar com rate limits de APIs de mercado?** → Caching agressivo + batch requests + fallback
2. ✅ **Qual estratégia de criptografia para dados locais?** → SQLite com AES-256, chave derivada de senha
3. ✅ **Como estruturar prompts para LLM?** → System prompt + JSON schema output + temperature 0.3
4. ✅ **Qual granularidade de background services?** → Separados por domínio (market data, news, alerts)
5. ✅ **Como garantir consistência de UI?** → Design tokens + component library em `shared/`
