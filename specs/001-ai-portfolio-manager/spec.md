# Feature Specification: AI Portfolio Manager

**Feature Branch**: `001-ai-portfolio-manager`  
**Created**: 2026-01-01  
**Status**: Draft  
**Input**: User description: "Build an AI-powered autonomous investment portfolio management application that intelligently rebalances portfolios based on asset performance. The system monitors both individual portfolio holdings and broader market movements to identify rebalancing opportunities..."

## Clarifications

### Session 2026-01-01
- Q: Qual é a plataforma alvo do aplicativo? → A: Web App (SPA) - Aplicação web responsiva acessível via navegador
- Q: Qual estratégia de LLM para processamento de IA? → A: API Cloud com dados completos - contexto incluindo dados do portfólio para análise mais precisa
- Q: Qual arquitetura de backend? → A: Backend .NET 8 Clean Architecture - Backend completo com C# .NET 8 Web API, SQLite local, seguindo Clean Architecture com CQRS
- Q: Qual stack tecnológico do frontend? → A: React + TypeScript - Ecossistema maduro para dashboards financeiros complexos
- Q: Qual fonte de dados de mercado? → A: APIs gratuitas/freemium (Yahoo Finance, Brapi, Alpha Vantage) com delay de 15-20min

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Visualização do Dashboard de Portfólio (Priority: P1)

Como investidor, quero visualizar um dashboard completo do meu portfólio de investimentos para entender minha alocação atual, performance geral e identificar rapidamente oportunidades ou problemas.

**Why this priority**: O dashboard é a porta de entrada do sistema e fornece a visão essencial que todo investidor precisa para tomar decisões. Sem ele, nenhuma outra funcionalidade faz sentido.

**Independent Test**: Pode ser testado com dados mockados de portfólio, exibindo composição, alocação percentual e performance vs benchmarks (Ibovespa, CDI, IPCA+).

**Acceptance Scenarios**:

1. **Given** um usuário autenticado com portfólio cadastrado, **When** acessa o dashboard, **Then** visualiza a composição do portfólio com alocação percentual por classe de ativo (ações, ETFs, FIIs, renda fixa, ativos internacionais)
2. **Given** um portfólio com histórico de transações, **When** visualiza o dashboard, **Then** vê a performance acumulada comparada aos benchmarks (Ibovespa, CDI, IPCA+)
3. **Given** ativos com valorização ou desvalorização significativa, **When** visualiza o dashboard, **Then** cards individuais de ativos destacam visualmente tendências de alta/baixa
4. **Given** um usuário com perfil de risco definido, **When** visualiza o dashboard, **Then** vê indicador de aderência da alocação atual ao perfil configurado

---

### User Story 2 - Configuração de Perfil de Risco e Metas (Priority: P1)

Como investidor, quero definir meu perfil de risco, objetivos de investimento e thresholds de rebalanceamento para que o sistema personalize recomendações de acordo com minhas preferências.

**Why this priority**: Fundamental para a personalização do sistema. As recomendações de IA dependem dessas configurações para serem relevantes.

**Independent Test**: Pode ser testado configurando diferentes perfis (conservador, moderado, agressivo) e verificando se os thresholds são salvos corretamente.

**Acceptance Scenarios**:

1. **Given** um novo usuário, **When** configura seu perfil, **Then** pode selecionar perfil de risco (conservador, moderado, agressivo, ou personalizado)
2. **Given** um usuário configurando metas, **When** define objetivos de investimento, **Then** pode especificar horizonte temporal, meta de rentabilidade e tolerância a volatilidade
3. **Given** um usuário configurando thresholds, **When** define limites de rebalanceamento, **Then** pode especificar desvio percentual máximo antes de sugerir rebalanceamento (ex: 5%, 10%, 15%)
4. **Given** configurações salvas, **When** retorna ao sistema, **Then** as preferências persistem entre sessões

---

### User Story 3 - Feed de Notícias com Análise por IA (Priority: P2)

Como investidor, quero acessar um feed de notícias financeiras com resumos gerados por IA para me manter informado sobre eventos que podem impactar meus investimentos sem precisar ler dezenas de artigos.

**Why this priority**: Informação é crucial para decisões, mas o valor principal está nas recomendações (P1). O feed complementa as recomendações fornecendo contexto.

**Independent Test**: Pode ser testado exibindo notícias agregadas de fontes configuradas com resumos sintéticos gerados.

**Acceptance Scenarios**:

1. **Given** um usuário no dashboard, **When** acessa o feed de notícias, **Then** visualiza notícias financeiras relevantes ordenadas por recência
2. **Given** uma notícia no feed, **When** visualiza, **Then** cada notícia exibe título, fonte, data, e resumo gerado por IA
3. **Given** ativos no portfólio do usuário, **When** notícias relacionadas chegam, **Then** são destacadas com indicador de relevância ao portfólio
4. **Given** eventos de mercado significativos (fatos relevantes, balanços), **When** publicados, **Then** aparecem no feed com análise de potencial impacto

---

### User Story 4 - Recomendações de Rebalanceamento com IA (Priority: P1)

Como investidor, quero receber recomendações inteligentes de rebalanceamento baseadas em análise de IA para otimizar meu portfólio de acordo com meu perfil e condições de mercado.

**Why this priority**: Esta é a proposta central de valor do sistema - usar IA para automatizar e otimizar decisões de investimento.

**Independent Test**: Pode ser testado com portfólio com desvios de alocação, verificando se sugestões de compra/venda são geradas com justificativas.

**Acceptance Scenarios**:

1. **Given** um portfólio com alocação desviada dos thresholds definidos, **When** o sistema analisa, **Then** gera recomendações de rebalanceamento com ações específicas (comprar X, vender Y)
2. **Given** uma recomendação de rebalanceamento, **When** visualizada pelo usuário, **Then** inclui justificativa baseada em dados analisados (notícias, balanços, indicadores)
3. **Given** dados de mercado (DRE, balanços, indicadores macroeconômicos), **When** processados pela IA, **Then** são considerados na geração de recomendações
4. **Given** múltiplas oportunidades identificadas, **When** exibidas no painel de recomendações, **Then** são priorizadas por potencial de retorno ajustado ao risco
5. **Given** uma recomendação, **When** usuário analisa, **Then** pode ver detalhes sobre os dados que fundamentaram a sugestão

---

### User Story 5 - Simulação de Cenários de Rebalanceamento (Priority: P2)

Como investidor, quero simular cenários de rebalanceamento antes de executar operações para entender o impacto potencial na composição e performance do portfólio.

**Why this priority**: Importante para tomada de decisão informada, mas secundária à funcionalidade principal de recomendações.

**Independent Test**: Pode ser testado selecionando uma recomendação e visualizando projeção antes/depois.

**Acceptance Scenarios**:

1. **Given** uma recomendação de rebalanceamento, **When** usuário seleciona simular, **Then** visualiza comparação lado-a-lado da alocação atual vs projetada
2. **Given** uma simulação em andamento, **When** ajusta valores de compra/venda, **Then** a projeção atualiza em tempo real
3. **Given** simulação completa, **When** usuário revisa, **Then** vê estimativa de custos (taxas, impostos) e impacto na alocação por classe de ativo
4. **Given** simulação satisfatória, **When** usuário confirma, **Then** pode salvar cenário para referência futura ou marcar para execução

---

### User Story 6 - Alertas de Eventos de Mercado (Priority: P2)

Como investidor, quero receber alertas sobre eventos significativos de mercado que afetam meus ativos para reagir rapidamente a oportunidades ou riscos.

**Why this priority**: Alertas proativos aumentam engajamento e permitem ações tempestivas, complementando o monitoramento passivo.

**Independent Test**: Pode ser testado configurando alertas e verificando notificações quando condições são atendidas.

**Acceptance Scenarios**:

1. **Given** um ativo no portfólio com variação significativa (>5% em um dia), **When** detectado, **Then** usuário recebe alerta com detalhes do movimento
2. **Given** publicação de fato relevante de empresa no portfólio, **When** detectado, **Then** usuário recebe alerta com resumo do evento
3. **Given** divulgação de balanço/DRE de empresa no portfólio, **When** publicado, **Then** usuário recebe alerta com análise preliminar
4. **Given** indicadores macroeconômicos relevantes (Selic, IPCA), **When** divulgados, **Then** usuário recebe alerta com potencial impacto no portfólio

---

### User Story 7 - Histórico e Analytics de Performance (Priority: P2)

Como investidor, quero visualizar o histórico detalhado de performance do meu portfólio com analytics avançados para entender a evolução dos meus investimentos ao longo do tempo.

**Why this priority**: Valioso para análise retrospectiva e aprendizado, mas não essencial para operação diária.

**Independent Test**: Pode ser testado com dados históricos, exibindo gráficos de evolução patrimonial e comparativos.

**Acceptance Scenarios**:

1. **Given** um portfólio com histórico de transações, **When** acessa analytics, **Then** visualiza gráfico de evolução patrimonial ao longo do tempo
2. **Given** período selecionado (1M, 6M, 1A, YTD, All), **When** filtrado, **Then** métricas e gráficos refletem o período escolhido
3. **Given** benchmarks configurados, **When** visualiza performance, **Then** vê comparativo detalhado com Ibovespa, CDI, IPCA+ no mesmo período
4. **Given** múltiplas classes de ativos, **When** analisa contribuição, **Then** vê breakdown de retorno por classe de ativo
5. **Given** histórico de rebalanceamentos, **When** revisa, **Then** pode ver impacto de cada decisão de rebalanceamento na performance

---

### User Story 8 - Gerenciamento Seguro de Dados Locais (Priority: P1)

Como investidor, quero que meus dados financeiros sensíveis sejam armazenados localmente com segurança para manter privacidade e controle sobre minhas informações.

**Why this priority**: Segurança e privacidade são requisitos fundamentais para qualquer aplicação financeira.

**Independent Test**: Pode ser testado verificando que dados são persistidos localmente com criptografia e não são transmitidos a servidores externos sem consentimento.

**Acceptance Scenarios**:

1. **Given** dados de portfólio inseridos, **When** salvos, **Then** são armazenados localmente no dispositivo do usuário
2. **Given** dados sensíveis (posições, valores), **When** persistidos, **Then** são criptografados em repouso
3. **Given** primeiro acesso ao sistema, **When** inicia, **Then** usuário pode configurar senha/PIN para proteção adicional
4. **Given** exportação de dados, **When** solicitada, **Then** usuário pode exportar backup criptografado
5. **Given** importação de dados, **When** executada, **Then** pode restaurar backup com verificação de integridade

---

### Edge Cases

- O que acontece quando a API da B3 ou fontes de dados estão indisponíveis?
  - Sistema exibe dados em cache com indicador de última atualização e aviso de indisponibilidade
- Como o sistema lida com ativos sem cotação disponível (ações OTC, ativos privados)?
  - Permite entrada manual de valor com flag de "valor informado manualmente"
- O que acontece quando recomendações conflitam com o perfil de risco do usuário?
  - Sistema apresenta aviso destacado explicando o conflito e requer confirmação explícita
- Como o sistema trata split/grupamento de ações?
  - Detecta eventos corporativos e ajusta automaticamente posições históricas
- O que acontece se o usuário não define um perfil de risco?
  - Sistema assume perfil moderado como padrão, com sugestão persistente para configurar
- O que acontece quando um ativo tem cotação zero ou está suspenso?
  - Sistema exibe último preço conhecido com flag "Cotação Suspensa" e data da última atualização válida
- O que acontece em caso de timeout ou falha da API de LLM?
  - Sistema exibe mensagem de erro amigável, permite retry manual, e sugere tentar novamente mais tarde
- O que acontece quando o portfólio está vazio (zero-state)?
  - Dashboard exibe onboarding wizard guiando o usuário para cadastrar sua primeira posição

## Requirements *(mandatory)*

### Functional Requirements

**Gestão de Portfólio**
- **FR-001**: Sistema DEVE permitir cadastro manual de posições em múltiplas classes de ativos (ações, ETFs, FIIs, renda fixa, ativos internacionais, criptomoedas)
- **FR-002**: Sistema DEVE calcular e exibir alocação percentual atual por classe de ativo e por ativo individual
- **FR-003**: Sistema DEVE atualizar cotações automaticamente de ativos listados na B3 (delay de 15-20 minutos via APIs gratuitas)
- **FR-004**: Sistema DEVE suportar importação de extrato de posições via arquivo (CSV, Excel)
- **FR-005**: Sistema DEVE calcular custo médio de aquisição para cada posição

**Análise e Comparativos**
- **FR-006**: Sistema DEVE calcular e exibir performance do portfólio em múltiplos períodos (1M, 3M, 6M, YTD, 1A, All)
- **FR-007**: Sistema DEVE comparar performance do portfólio com benchmarks (Ibovespa, CDI, IPCA+)
- **FR-008**: Sistema DEVE exibir gráficos de evolução patrimonial e alocação ao longo do tempo
- **FR-009**: Sistema DEVE calcular métricas de risco (volatilidade, Sharpe ratio, drawdown máximo)

**Inteligência Artificial e Recomendações**
- **FR-010**: Sistema DEVE agregar dados de múltiplas fontes (notícias, fatos relevantes, balanços, indicadores)
- **FR-011**: Sistema DEVE processar notícias financeiras e gerar resumos com IA
- **FR-012**: Sistema DEVE analisar demonstrativos financeiros (DRE, balanços) das empresas no portfólio
- **FR-013**: Sistema DEVE gerar recomendações de rebalanceamento baseadas em análise de IA
- **FR-014**: Sistema DEVE fornecer justificativa detalhada para cada recomendação
- **FR-015**: Sistema DEVE identificar oportunidades de compra/venda em todas as classes de ativos

**Rebalanceamento**
- **FR-016**: Sistema DEVE detectar quando alocação atual desvia dos thresholds configurados
- **FR-017**: Sistema DEVE permitir simulação de cenários de rebalanceamento antes de execução
- **FR-018**: Sistema DEVE calcular custos estimados de transação e impacto tributário de rebalanceamento
- **FR-019**: Sistema DEVE permitir que usuário aceite, rejeite ou modifique recomendações

**Perfil e Configurações**
- **FR-020**: Sistema DEVE permitir configuração de perfil de risco (conservador, moderado, agressivo, personalizado)
- **FR-021**: Sistema DEVE permitir definição de metas de investimento (horizonte, rentabilidade alvo)
- **FR-022**: Sistema DEVE permitir configuração de thresholds de rebalanceamento personalizados
- **FR-023**: Sistema DEVE permitir configuração de alocação-alvo por classe de ativo

**Alertas e Notificações**
- **FR-024**: Sistema DEVE monitorar e alertar sobre variações significativas (≥5% em um dia útil) em ativos do portfólio
- **FR-025**: Sistema DEVE alertar sobre publicação de fatos relevantes de empresas do portfólio
- **FR-026**: Sistema DEVE alertar sobre divulgação de balanços/DRE de empresas do portfólio
- **FR-027**: Sistema DEVE alertar sobre indicadores macroeconômicos relevantes

**Segurança e Dados**
- **FR-028**: Sistema DEVE armazenar dados de portfólio localmente em SQLite como fonte primária, gerenciado pelo backend local
- **FR-029**: Sistema DEVE criptografar dados sensíveis em repouso no armazenamento local
- **FR-030**: Sistema DEVE permitir configuração de senha/PIN para acesso
- **FR-031**: Sistema DEVE permitir exportação e importação de backup criptografado
- **FR-032**: Sistema DEVE permitir exclusão completa de dados sob demanda
- **FR-033**: Sistema DEVE obter consentimento explícito do usuário antes de transmitir dados do portfólio para API de LLM
- **FR-034**: Sistema DEVE exibir claramente quais dados serão enviados para processamento externo

**Interface e Experiência**
- **FR-035**: Sistema DEVE exibir dashboard consolidado com visão geral do portfólio
- **FR-036**: Sistema DEVE exibir cards individuais por ativo com métricas-chave
- **FR-037**: Sistema DEVE exibir feed de notícias com filtros por relevância ao portfólio
- **FR-038**: Sistema DEVE exibir painel de recomendações com priorização

### Key Entities

- **Portfolio**: Representa o conjunto completo de investimentos do usuário; contém múltiplas posições, configurações de perfil e histórico de transações
- **Position**: Uma posição individual em um ativo específico; inclui quantidade, custo médio, data de aquisição e classe de ativo
- **Asset**: Representa um ativo investível (ação, ETF, FII, título de renda fixa, ativo internacional); contém código, nome, tipo, cotação atual e histórico
- **Transaction**: Registro de operação de compra ou venda; inclui ativo, quantidade, preço, data, custos e tipo (compra/venda)
- **UserProfile**: Configurações pessoais do investidor; inclui perfil de risco, metas, thresholds e preferências de alerta
- **Recommendation**: Sugestão de ação gerada pela IA; inclui ativo, tipo de ação (comprar/vender), quantidade sugerida, justificativa e dados de suporte
- **MarketEvent**: Evento de mercado relevante (fato relevante, balanço, indicador); inclui tipo, data, conteúdo, ativos relacionados e análise de impacto
- **NewsItem**: Notícia agregada de fonte externa; inclui título, fonte, data, conteúdo, resumo IA e relevância ao portfólio
- **Benchmark**: Índice de referência para comparação (Ibovespa, CDI, IPCA+); contém valores históricos para cálculo de performance comparativa

## Success Criteria *(mandatory)*

### Measurable Outcomes

**Eficiência do Investidor**
- **SC-001**: Usuários podem visualizar composição completa do portfólio em menos de 3 segundos após abrir o dashboard
- **SC-002**: Usuários conseguem identificar desvios de alocação em relação ao perfil configurado em menos de 10 segundos
- **SC-003**: Tempo médio para analisar uma recomendação de rebalanceamento é inferior a 2 minutos

**Qualidade das Recomendações**
- **SC-004**: 100% das recomendações incluem justificativa baseada em dados analisados
- **SC-005**: Recomendações são atualizadas em até 24 horas após publicação de eventos relevantes (balanços, fatos relevantes)
- **SC-006**: Simulações de rebalanceamento refletem custos e impacto tributário com precisão de ±5%

**Informação e Contexto**
- **SC-007**: Feed de notícias é atualizado em até 15 minutos após publicação em fontes primárias
- **SC-008**: Resumos gerados por IA capturam os pontos principais da notícia em até 3 sentenças
- **SC-009**: 90% das notícias são corretamente classificadas quanto à relevância para o portfólio do usuário

**Cobertura de Ativos**
- **SC-010**: Sistema suporta 100% dos ativos listados na B3 (ações, BDRs, ETFs, FIIs)
- **SC-011**: Sistema permite cadastro manual de ativos não listados (renda fixa, ativos internacionais)

**Segurança e Privacidade**
- **SC-012**: Dados de portfólio são armazenados localmente como fonte primária; transmissão para LLM requer consentimento explícito
- **SC-013**: Dados sensíveis são criptografados em repouso utilizando AES-256 ou equivalente
- **SC-014**: Usuário pode exportar backup completo em menos de 30 segundos
- **SC-015**: 100% das transmissões de dados para APIs externas são realizadas via HTTPS/TLS

**Alertas e Monitoramento**
- **SC-016**: Alertas de variação significativa são enviados em até 5 minutos após detecção do evento
- **SC-017**: 95% dos fatos relevantes publicados são capturados e alertados no mesmo dia útil

**Experiência do Usuário**
- **SC-018**: Usuários completam configuração inicial de perfil em menos de 5 minutos
- **SC-019**: Dashboard carrega completamente em menos de 2 segundos em dispositivo padrão
- **SC-020**: Usuários encontram informações desejadas em no máximo 3 cliques/toques a partir do dashboard

## Assumptions

1. **Plataforma e Stack**: Aplicação Web SPA construída com React + TypeScript, acessível via navegador moderno. Backend com C# .NET 8 Web API seguindo Clean Architecture. Armazenamento local via SQLite com criptografia (AES-256) gerenciado pelo backend. O backend atua como BFF para proxy de APIs externas e proteção de chaves de API.

2. **Fontes de Dados de Mercado**: APIs gratuitas/freemium (Yahoo Finance, Brapi, Alpha Vantage) para cotações com delay de 15-20 minutos. Suficiente para análise de portfólio de longo prazo; não adequado para day trading. Fontes de notícias e dados fundamentalistas via APIs públicas ou scraping com fallback.

3. **Modelo de IA**: Será utilizada API de LLM cloud (ex: OpenAI, Anthropic) com envio de contexto completo incluindo dados do portfólio para análises mais precisas. O usuário deve consentir explicitamente com o envio de dados para processamento externo.

4. **Armazenamento Local**: O aplicativo terá capacidade de armazenar localmente volumes de dados compatíveis com histórico de 10+ anos de transações e cotações.

5. **Conectividade**: Para atualização de cotações e notícias, conectividade com internet é necessária, mas o sistema funciona offline com dados em cache.

6. **Escopo de Recomendações**: As recomendações de IA são sugestões informativas, não constituindo recomendação de investimento formal nos termos da CVM.

7. **Classes de Ativos Suportadas**: Inicialmente focado em ativos brasileiros (B3) com suporte básico para ativos internacionais via cadastro manual.

8. **Autenticação**: Autenticação local (senha/PIN) é suficiente para primeira versão, sem necessidade de autenticação em servidor.
