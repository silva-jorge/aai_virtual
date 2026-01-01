# Data Model: AI Portfolio Manager

**Feature**: 001-ai-portfolio-manager  
**Date**: 2026-01-01  
**Status**: Completed

## Overview

Este documento define o modelo de dados para o AI Portfolio Manager. As entidades são derivadas dos requisitos funcionais da especificação e seguem os princípios de Domain-Driven Design.

---

## Entity Relationship Diagram

```
┌─────────────────┐       ┌─────────────────┐       ┌─────────────────┐
│   UserProfile   │       │    Portfolio    │       │      Asset      │
├─────────────────┤       ├─────────────────┤       ├─────────────────┤
│ Id              │───1:1─│ Id              │       │ Id              │
│ RiskProfile     │       │ Name            │       │ Ticker          │
│ InvestmentGoal  │       │ UserId          │       │ Name            │
│ VolatilityTol   │       │ CreatedAt       │       │ AssetClass      │
│ TimeHorizon     │       │ UpdatedAt       │       │ Exchange        │
│ RebalanceThresh │       └────────┬────────┘       │ CurrentPrice    │
│ TargetAlloc     │                │                │ LastPriceUpdate │
│ CreatedAt       │                │ 1:N            │ Currency        │
└─────────────────┘                │                └────────┬────────┘
                                   ▼                         │
                    ┌─────────────────────────┐              │
                    │       Position          │              │
                    ├─────────────────────────┤              │
                    │ Id                      │──────────────┘
                    │ PortfolioId (FK)        │         N:1
                    │ AssetId (FK)            │
                    │ Quantity                │
                    │ AverageCost             │
                    │ CurrentValue            │
                    │ AllocationPercentage    │
                    │ CreatedAt               │
                    │ UpdatedAt               │
                    └───────────┬─────────────┘
                                │
                                │ 1:N
                                ▼
                    ┌─────────────────────────┐
                    │      Transaction        │
                    ├─────────────────────────┤
                    │ Id                      │
                    │ PositionId (FK)         │
                    │ TransactionType         │
                    │ Quantity                │
                    │ UnitPrice               │
                    │ TotalValue              │
                    │ Fees                    │
                    │ TransactionDate         │
                    │ Notes                   │
                    │ CreatedAt               │
                    └─────────────────────────┘

┌─────────────────┐       ┌─────────────────┐       ┌─────────────────┐
│ Recommendation  │       │   MarketEvent   │       │    NewsItem     │
├─────────────────┤       ├─────────────────┤       ├─────────────────┤
│ Id              │       │ Id              │       │ Id              │
│ PortfolioId (FK)│       │ EventType       │       │ Title           │
│ AssetId (FK)    │       │ Title           │       │ Source          │
│ ActionType      │       │ Description     │       │ Url             │
│ SuggestedQty    │       │ PublishedAt     │       │ PublishedAt     │
│ CurrentPrice    │       │ AffectedAssets  │       │ Content         │
│ TargetPrice     │       │ ImpactAnalysis  │       │ AISummary       │
│ Justification   │       │ Severity        │       │ RelevanceScore  │
│ DataSources     │       │ IsProcessed     │       │ RelatedAssets   │
│ Priority        │       │ CreatedAt       │       │ IsRead          │
│ Status          │       └─────────────────┘       │ CreatedAt       │
│ ExpiresAt       │                                 └─────────────────┘
│ CreatedAt       │
│ ProcessedAt     │       ┌─────────────────┐       ┌─────────────────┐
└─────────────────┘       │    Benchmark    │       │   PriceHistory  │
                          ├─────────────────┤       ├─────────────────┤
                          │ Id              │       │ Id              │
                          │ Name            │       │ AssetId (FK)    │
                          │ Symbol          │       │ Date            │
                          │ Type            │       │ OpenPrice       │
                          │ Description     │       │ HighPrice       │
                          └────────┬────────┘       │ LowPrice        │
                                   │                │ ClosePrice      │
                                   │ 1:N            │ Volume          │
                                   ▼                │ AdjustedClose   │
                          ┌─────────────────┐       └─────────────────┘
                          │ BenchmarkValue  │
                          ├─────────────────┤
                          │ Id              │
                          │ BenchmarkId (FK)│
                          │ Date            │
                          │ Value           │
                          │ DailyReturn     │
                          └─────────────────┘
```

---

## Entity Definitions

### UserProfile

Representa as configurações e preferências do usuário investidor.

| Field | Type | Nullable | Description | Validation Rules |
|-------|------|----------|-------------|------------------|
| Id | Guid | No | Identificador único | Primary Key |
| RiskProfile | enum | No | Perfil de risco | conservador, moderado, agressivo, personalizado |
| InvestmentGoal | string | Yes | Objetivo de investimento | Max 500 chars |
| VolatilityTolerance | decimal | No | Tolerância a volatilidade (%) | 0-100 |
| TimeHorizonMonths | int | No | Horizonte temporal em meses | 1-600 |
| RebalanceThresholdPercent | decimal | No | Threshold para sugerir rebalanceamento | 1-50 |
| TargetAllocationJson | string | No | Alocação-alvo por classe (JSON) | Valid JSON |
| PasswordHash | string | No | Hash da senha do usuário | Argon2id hash |
| PasswordSalt | string | No | Salt da senha | Base64 encoded |
| CreatedAt | DateTime | No | Data de criação | UTC |
| UpdatedAt | DateTime | No | Data de atualização | UTC |

**State Transitions**: N/A (configuration entity)

---

### Portfolio

Representa o portfólio de investimentos do usuário.

| Field | Type | Nullable | Description | Validation Rules |
|-------|------|----------|-------------|------------------|
| Id | Guid | No | Identificador único | Primary Key |
| UserId | Guid | No | Referência ao UserProfile | Foreign Key |
| Name | string | No | Nome do portfólio | 1-100 chars |
| Description | string | Yes | Descrição | Max 500 chars |
| Currency | string | No | Moeda base | ISO 4217 (BRL, USD) |
| CreatedAt | DateTime | No | Data de criação | UTC |
| UpdatedAt | DateTime | No | Data de atualização | UTC |

**Relationships**:
- 1:1 com UserProfile
- 1:N com Position
- 1:N com Recommendation

---

### Asset

Representa um ativo investível (ação, ETF, FII, etc).

| Field | Type | Nullable | Description | Validation Rules |
|-------|------|----------|-------------|------------------|
| Id | Guid | No | Identificador único | Primary Key |
| Ticker | string | No | Código do ativo | 1-20 chars, uppercase |
| Name | string | No | Nome do ativo | 1-200 chars |
| AssetClass | enum | No | Classe do ativo | acao, etf, fii, renda_fixa, internacional, cripto |
| Exchange | string | Yes | Bolsa de negociação | B3, NYSE, NASDAQ, etc |
| Sector | string | Yes | Setor econômico | Max 100 chars |
| Currency | string | No | Moeda de cotação | ISO 4217 |
| CurrentPrice | decimal | Yes | Preço atual | >= 0 |
| LastPriceUpdate | DateTime | Yes | Última atualização de preço | UTC |
| IsActive | bool | No | Se o ativo está ativo para trading | |
| IsManualEntry | bool | No | Se é entrada manual (sem cotação automática) | |
| CreatedAt | DateTime | No | Data de criação | UTC |
| UpdatedAt | DateTime | No | Data de atualização | UTC |

**Relationships**:
- 1:N com Position
- 1:N com PriceHistory
- N:M com NewsItem (via RelatedAssets)
- N:M com MarketEvent (via AffectedAssets)

---

### Position

Representa uma posição em um ativo específico dentro do portfólio.

| Field | Type | Nullable | Description | Validation Rules |
|-------|------|----------|-------------|------------------|
| Id | Guid | No | Identificador único | Primary Key |
| PortfolioId | Guid | No | Referência ao Portfolio | Foreign Key |
| AssetId | Guid | No | Referência ao Asset | Foreign Key |
| Quantity | decimal | No | Quantidade de unidades | > 0 |
| AverageCost | decimal | No | Custo médio de aquisição | >= 0 |
| TotalInvested | decimal | No | Valor total investido | >= 0 |
| CurrentValue | decimal | No | Valor atual (calculado) | >= 0 |
| AllocationPercent | decimal | No | % da alocação no portfólio | 0-100 |
| UnrealizedGainLoss | decimal | No | Ganho/perda não realizado | |
| UnrealizedGainLossPercent | decimal | No | % de ganho/perda | |
| CreatedAt | DateTime | No | Data de criação | UTC |
| UpdatedAt | DateTime | No | Data de atualização | UTC |

**Relationships**:
- N:1 com Portfolio
- N:1 com Asset
- 1:N com Transaction

**Computed Fields**:
- CurrentValue = Quantity * Asset.CurrentPrice
- UnrealizedGainLoss = CurrentValue - TotalInvested
- UnrealizedGainLossPercent = (UnrealizedGainLoss / TotalInvested) * 100

---

### Transaction

Registro de operação de compra ou venda.

| Field | Type | Nullable | Description | Validation Rules |
|-------|------|----------|-------------|------------------|
| Id | Guid | No | Identificador único | Primary Key |
| PositionId | Guid | No | Referência à Position | Foreign Key |
| TransactionType | enum | No | Tipo de transação | compra, venda, dividendo, jscp, bonificacao, grupamento, desdobramento |
| Quantity | decimal | No | Quantidade transacionada | > 0 |
| UnitPrice | decimal | No | Preço unitário | >= 0 |
| TotalValue | decimal | No | Valor total da transação | >= 0 |
| Fees | decimal | No | Taxas e custos | >= 0 |
| TransactionDate | DateTime | No | Data da transação | UTC, <= today |
| Broker | string | Yes | Corretora | Max 100 chars |
| Notes | string | Yes | Observações | Max 500 chars |
| ImportSource | string | Yes | Fonte de importação | manual, csv, excel |
| CreatedAt | DateTime | No | Data de criação | UTC |

**Business Rules**:
- Ao adicionar compra: recalcula AverageCost da Position
- Ao adicionar venda: valida que Quantity <= Position.Quantity
- Eventos corporativos (grupamento, desdobramento) ajustam histórico

---

### Recommendation

Sugestão de ação gerada pela IA.

| Field | Type | Nullable | Description | Validation Rules |
|-------|------|----------|-------------|------------------|
| Id | Guid | No | Identificador único | Primary Key |
| PortfolioId | Guid | No | Referência ao Portfolio | Foreign Key |
| AssetId | Guid | No | Referência ao Asset | Foreign Key |
| ActionType | enum | No | Tipo de ação sugerida | comprar, vender, manter, aumentar, reduzir |
| SuggestedQuantity | decimal | Yes | Quantidade sugerida | > 0 |
| SuggestedValue | decimal | Yes | Valor sugerido | > 0 |
| CurrentPrice | decimal | No | Preço no momento da sugestão | > 0 |
| TargetPrice | decimal | Yes | Preço-alvo estimado | > 0 |
| Justification | string | No | Justificativa detalhada | Max 2000 chars |
| DataSourcesJson | string | No | Fontes de dados usadas (JSON) | Valid JSON |
| Priority | enum | No | Prioridade da sugestão | alta, media, baixa |
| Status | enum | No | Status da recomendação | pendente, aceita, rejeitada, expirada |
| ConfidenceScore | decimal | Yes | Score de confiança da IA | 0-100 |
| ExpiresAt | DateTime | No | Data de expiração | UTC, > CreatedAt |
| CreatedAt | DateTime | No | Data de criação | UTC |
| ProcessedAt | DateTime | Yes | Data de processamento | UTC |
| ProcessedNotes | string | Yes | Notas do usuário ao processar | Max 500 chars |

**State Transitions**:
```
pendente → aceita (user accepts)
pendente → rejeitada (user rejects)
pendente → expirada (ExpiresAt reached)
```

---

### MarketEvent

Evento de mercado relevante (fato relevante, balanço, indicador).

| Field | Type | Nullable | Description | Validation Rules |
|-------|------|----------|-------------|------------------|
| Id | Guid | No | Identificador único | Primary Key |
| EventType | enum | No | Tipo de evento | fato_relevante, balanco, dre, indicador_macro, split, grupamento |
| Title | string | No | Título do evento | 1-200 chars |
| Description | string | No | Descrição completa | Max 5000 chars |
| SourceUrl | string | Yes | URL da fonte | Valid URL |
| PublishedAt | DateTime | No | Data de publicação | UTC |
| AffectedAssetsJson | string | Yes | Ativos afetados (JSON array) | Valid JSON |
| ImpactAnalysis | string | Yes | Análise de impacto gerada por IA | Max 2000 chars |
| Severity | enum | No | Severidade | alta, media, baixa |
| IsProcessed | bool | No | Se já foi processado pela IA | |
| IsAlertSent | bool | No | Se alerta foi enviado | |
| CreatedAt | DateTime | No | Data de criação | UTC |

---

### NewsItem

Notícia agregada de fonte externa.

| Field | Type | Nullable | Description | Validation Rules |
|-------|------|----------|-------------|------------------|
| Id | Guid | No | Identificador único | Primary Key |
| Title | string | No | Título da notícia | 1-300 chars |
| Source | string | No | Fonte da notícia | Max 100 chars |
| SourceUrl | string | No | URL original | Valid URL |
| PublishedAt | DateTime | No | Data de publicação | UTC |
| Content | string | Yes | Conteúdo completo | Max 10000 chars |
| AISummary | string | Yes | Resumo gerado por IA | Max 500 chars |
| Sentiment | enum | Yes | Sentimento detectado | positivo, neutro, negativo |
| RelevanceScore | decimal | Yes | Score de relevância ao portfólio | 0-100 |
| RelatedAssetsJson | string | Yes | Ativos relacionados (JSON) | Valid JSON |
| IsRead | bool | No | Se foi lida pelo usuário | |
| CreatedAt | DateTime | No | Data de criação | UTC |

---

### Benchmark

Índice de referência para comparação.

| Field | Type | Nullable | Description | Validation Rules |
|-------|------|----------|-------------|------------------|
| Id | Guid | No | Identificador único | Primary Key |
| Name | string | No | Nome do benchmark | 1-100 chars |
| Symbol | string | No | Símbolo (IBOV, CDI, IPCA) | 1-20 chars |
| Type | enum | No | Tipo | indice_acoes, renda_fixa, inflacao |
| Description | string | Yes | Descrição | Max 500 chars |
| IsActive | bool | No | Se está ativo | |
| CreatedAt | DateTime | No | Data de criação | UTC |

**Relationships**:
- 1:N com BenchmarkValue

---

### BenchmarkValue

Valores históricos de benchmarks.

| Field | Type | Nullable | Description | Validation Rules |
|-------|------|----------|-------------|------------------|
| Id | Guid | No | Identificador único | Primary Key |
| BenchmarkId | Guid | No | Referência ao Benchmark | Foreign Key |
| Date | DateOnly | No | Data do valor | |
| Value | decimal | No | Valor/pontos no dia | |
| DailyReturn | decimal | Yes | Retorno diário (%) | |
| AccumulatedReturn | decimal | Yes | Retorno acumulado no ano (%) | |
| CreatedAt | DateTime | No | Data de criação | UTC |

---

### PriceHistory

Histórico de preços de ativos.

| Field | Type | Nullable | Description | Validation Rules |
|-------|------|----------|-------------|------------------|
| Id | Guid | No | Identificador único | Primary Key |
| AssetId | Guid | No | Referência ao Asset | Foreign Key |
| Date | DateOnly | No | Data | |
| OpenPrice | decimal | No | Preço de abertura | >= 0 |
| HighPrice | decimal | No | Preço máximo | >= 0 |
| LowPrice | decimal | No | Preço mínimo | >= 0 |
| ClosePrice | decimal | No | Preço de fechamento | >= 0 |
| AdjustedClose | decimal | No | Fechamento ajustado | >= 0 |
| Volume | long | No | Volume negociado | >= 0 |
| CreatedAt | DateTime | No | Data de criação | UTC |

---

### Alert

Configuração de alertas do usuário.

| Field | Type | Nullable | Description | Validation Rules |
|-------|------|----------|-------------|------------------|
| Id | Guid | No | Identificador único | Primary Key |
| UserId | Guid | No | Referência ao UserProfile | Foreign Key |
| AssetId | Guid | Yes | Ativo específico (null = todos) | Foreign Key |
| AlertType | enum | No | Tipo de alerta | price_variation, threshold_breach, market_event, news |
| Condition | string | No | Condição (JSON) | Valid JSON |
| IsActive | bool | No | Se está ativo | |
| LastTriggered | DateTime | Yes | Último disparo | UTC |
| CreatedAt | DateTime | No | Data de criação | UTC |

---

### AlertHistory

Histórico de alertas disparados.

| Field | Type | Nullable | Description | Validation Rules |
|-------|------|----------|-------------|------------------|
| Id | Guid | No | Identificador único | Primary Key |
| AlertId | Guid | No | Referência ao Alert | Foreign Key |
| Title | string | No | Título do alerta | 1-200 chars |
| Message | string | No | Mensagem detalhada | Max 1000 chars |
| TriggeredAt | DateTime | No | Data de disparo | UTC |
| IsRead | bool | No | Se foi lido | |
| RelatedEntityType | string | Yes | Tipo de entidade relacionada | |
| RelatedEntityId | Guid | Yes | ID da entidade relacionada | |

---

## Enumerations

### RiskProfile
```csharp
public enum RiskProfile
{
    Conservador = 1,
    Moderado = 2,
    Agressivo = 3,
    Personalizado = 4
}
```

### AssetClass
```csharp
public enum AssetClass
{
    Acao = 1,
    ETF = 2,
    FII = 3,
    RendaFixa = 4,
    Internacional = 5,
    Cripto = 6
}
```

### TransactionType
```csharp
public enum TransactionType
{
    Compra = 1,
    Venda = 2,
    Dividendo = 3,
    JSCP = 4,
    Bonificacao = 5,
    Grupamento = 6,
    Desdobramento = 7
}
```

### RecommendationActionType
```csharp
public enum RecommendationActionType
{
    Comprar = 1,
    Vender = 2,
    Manter = 3,
    Aumentar = 4,
    Reduzir = 5
}
```

### RecommendationStatus
```csharp
public enum RecommendationStatus
{
    Pendente = 1,
    Aceita = 2,
    Rejeitada = 3,
    Expirada = 4
}
```

### Priority
```csharp
public enum Priority
{
    Alta = 1,
    Media = 2,
    Baixa = 3
}
```

### Sentiment
```csharp
public enum Sentiment
{
    Positivo = 1,
    Neutro = 2,
    Negativo = 3
}
```

---

## Indexes

### Performance-Critical Indexes

```sql
-- Portfolio queries
CREATE INDEX IX_Position_PortfolioId ON Position(PortfolioId);
CREATE INDEX IX_Transaction_PositionId ON Transaction(PositionId);
CREATE INDEX IX_Transaction_TransactionDate ON Transaction(TransactionDate DESC);

-- Asset lookups
CREATE UNIQUE INDEX IX_Asset_Ticker ON Asset(Ticker);
CREATE INDEX IX_Asset_AssetClass ON Asset(AssetClass);

-- Price history queries
CREATE INDEX IX_PriceHistory_AssetId_Date ON PriceHistory(AssetId, Date DESC);

-- Recommendations
CREATE INDEX IX_Recommendation_PortfolioId_Status ON Recommendation(PortfolioId, Status);
CREATE INDEX IX_Recommendation_ExpiresAt ON Recommendation(ExpiresAt) WHERE Status = 1; -- Pendente

-- News and Events
CREATE INDEX IX_NewsItem_PublishedAt ON NewsItem(PublishedAt DESC);
CREATE INDEX IX_MarketEvent_PublishedAt ON MarketEvent(PublishedAt DESC);
CREATE INDEX IX_MarketEvent_IsProcessed ON MarketEvent(IsProcessed) WHERE IsProcessed = 0;

-- Benchmarks
CREATE INDEX IX_BenchmarkValue_BenchmarkId_Date ON BenchmarkValue(BenchmarkId, Date DESC);

-- Alerts
CREATE INDEX IX_Alert_UserId_IsActive ON Alert(UserId, IsActive);
CREATE INDEX IX_AlertHistory_AlertId_TriggeredAt ON AlertHistory(AlertId, TriggeredAt DESC);
```

---

## Data Retention

| Entity | Retention Policy |
|--------|------------------|
| Transaction | Indefinido (dados fiscais) |
| PriceHistory | 10 anos |
| NewsItem | 1 ano (AISummary preservado) |
| MarketEvent | 5 anos |
| Recommendation | 2 anos |
| AlertHistory | 1 ano |
| BenchmarkValue | 10 anos |

---

## Migration Strategy

1. **Initial Migration**: Criar todas as tabelas com constraints básicas
2. **Seed Data**: Inserir benchmarks (IBOV, CDI, IPCA+) e ativos B3 principais
3. **Index Migration**: Criar índices de performance após dados iniciais
4. **Encryption Migration**: Migrar dados existentes para formato criptografado (se aplicável)
