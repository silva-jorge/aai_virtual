<!--
================================================================================
SYNC IMPACT REPORT
================================================================================
Version change: [NEW] → 1.0.0
Modified principles: N/A (initial creation)
Added sections:
  - I. Qualidade de Código
  - II. Padrões de Teste
  - III. Experiência do Usuário Consistente
  - IV. Requisitos de Performance
  - Métricas e Validação
  - Processo de Desenvolvimento
  - Governance
Removed sections: N/A
Templates requiring updates:
  ✅ .specify/templates/plan-template.md - Constitution Check section aligned
  ✅ .specify/templates/spec-template.md - Success Criteria aligned with performance metrics
  ✅ .specify/templates/tasks-template.md - Testing phases aligned
Follow-up TODOs: None
================================================================================
-->

# AAI Virtual Constitution

## Core Principles

### I. Qualidade de Código

Todo código produzido DEVE aderir a padrões rigorosos de qualidade que garantam manutenibilidade, legibilidade e robustez a longo prazo.

**Regras Não-Negociáveis:**

- **Clean Code**: Código DEVE ser autoexplicativo; nomes de variáveis, funções e classes DEVEM expressar claramente sua intenção
- **DRY (Don't Repeat Yourself)**: Duplicação de lógica é proibida; abstrações DEVEM ser criadas para código reutilizado mais de uma vez
- **SOLID Principles**: Classes e módulos DEVEM seguir os princípios SOLID, especialmente Single Responsibility e Dependency Inversion
- **Documentação**: Funções públicas e APIs DEVEM ter documentação clara; código complexo DEVE incluir comentários explicativos do "porquê"
- **Code Reviews**: Todo código DEVE passar por revisão antes de merge; revisões DEVEM verificar conformidade com esta constituição
- **Linting/Formatting**: Código DEVE passar em todas as verificações de linting configuradas; formatação DEVE ser automática e consistente

**Rationale**: Código de alta qualidade reduz débito técnico, facilita onboarding de novos desenvolvedores e diminui bugs em produção.

### II. Padrões de Teste

Testes são cidadãos de primeira classe no desenvolvimento. A abordagem Test-First é RECOMENDADA para features críticas e OBRIGATÓRIA para correções de bugs.

**Regras Não-Negociáveis:**

- **Cobertura Mínima**: Código novo DEVE manter cobertura de testes ≥ 80% para lógica de negócio
- **Pirâmide de Testes**: DEVE-SE seguir a pirâmide: muitos testes unitários, alguns de integração, poucos E2E
- **Testes Unitários**: DEVEM ser isolados, rápidos (<100ms cada) e determinísticos
- **Testes de Integração**: DEVEM validar contratos entre componentes e serviços externos
- **Testes E2E**: DEVEM cobrir jornadas críticas do usuário definidas nas user stories P1
- **Bug Fixes**: Toda correção de bug DEVE incluir um teste que reproduza o bug antes da correção (red-green-refactor)
- **CI/CD Gate**: Testes DEVEM passar antes de qualquer merge; builds quebrados DEVEM ser corrigidos imediatamente

**Rationale**: Testes robustos garantem regressões detectadas cedo, permitem refatoração segura e servem como documentação viva do comportamento esperado.

### III. Experiência do Usuário Consistente

A experiência do usuário DEVE ser consistente, intuitiva e acessível em todas as interfaces e pontos de contato do sistema.

**Regras Não-Negociáveis:**

- **Design System**: Componentes de UI DEVEM seguir o design system definido; novos componentes DEVEM ser adicionados ao sistema antes do uso
- **Consistência Visual**: Cores, tipografia, espaçamentos e iconografia DEVEM seguir tokens de design padronizados
- **Padrões de Interação**: Comportamentos de UI (loading states, error handling, confirmações) DEVEM ser padronizados em toda aplicação
- **Acessibilidade (a11y)**: Interfaces DEVEM seguir WCAG 2.1 nível AA no mínimo; componentes DEVEM ser navegáveis por teclado e compatíveis com screen readers
- **Responsividade**: UI DEVE funcionar corretamente em breakpoints mobile (320px), tablet (768px) e desktop (1024px+)
- **Feedback ao Usuário**: Toda ação DEVE ter feedback visual claro; estados de loading, sucesso e erro DEVEM ser explícitos
- **Internacionalização**: Textos DEVEM ser externalizados para suportar múltiplos idiomas quando aplicável

**Rationale**: Consistência na UX reduz curva de aprendizado, aumenta confiança do usuário e melhora métricas de satisfação e retenção.

### IV. Requisitos de Performance

Performance é uma feature, não um afterthought. Todo código DEVE ser desenvolvido com performance em mente desde o início.

**Regras Não-Negociáveis:**

- **Tempos de Resposta API**: Endpoints DEVEM responder em < 200ms (p95) para operações simples, < 1s (p95) para operações complexas
- **Time to Interactive (TTI)**: Páginas web DEVEM atingir TTI < 3s em conexão 3G
- **First Contentful Paint (FCP)**: FCP DEVE ser < 1.5s em condições normais de rede
- **Bundle Size**: Bundles JavaScript DEVEM ser otimizados; lazy loading DEVE ser usado para código não-crítico
- **Consultas de Banco**: Queries DEVEM ser otimizadas com índices apropriados; N+1 queries são PROIBIDAS
- **Caching**: Dados frequentemente acessados DEVEM ter estratégia de cache definida
- **Memory/CPU**: Operações DEVEM evitar memory leaks e uso excessivo de CPU; profiling DEVE ser feito para operações críticas
- **Performance Budget**: Degradações de performance > 10% em métricas críticas DEVEM bloquear deploy até resolução

**Rationale**: Performance impacta diretamente satisfação do usuário, SEO, conversão e custos de infraestrutura.

## Métricas e Validação

Esta seção define como os princípios são medidos e validados continuamente.

**Métricas de Qualidade de Código:**
- Complexidade ciclomática máxima por função: 10
- Profundidade máxima de aninhamento: 4 níveis
- Linhas máximas por arquivo: 400 (exceto gerados)
- Debt ratio (SonarQube ou similar): < 5%

**Métricas de Teste:**
- Cobertura de código: ≥ 80% (lógica de negócio)
- Cobertura de branches: ≥ 70%
- Testes flaky: 0 tolerância (devem ser corrigidos ou removidos)
- Tempo total de suite: < 10 minutos para CI

**Métricas de UX:**
- Lighthouse Score: ≥ 90 para Performance, Accessibility, Best Practices
- Core Web Vitals: LCP < 2.5s, FID < 100ms, CLS < 0.1
- Taxa de erro de UI: < 0.1% de sessões

**Métricas de Performance:**
- Uptime: ≥ 99.9%
- Error rate: < 0.1% de requests
- P95 latency: conforme definido por endpoint

## Processo de Desenvolvimento

O processo de desenvolvimento DEVE seguir práticas que garantam qualidade contínua.

**Workflow de Desenvolvimento:**
1. **Planejamento**: Features DEVEM ter especificação clara antes do desenvolvimento
2. **Branching**: Usar Git Flow ou trunk-based development conforme definido pela equipe
3. **Commits**: Mensagens DEVEM seguir Conventional Commits (feat:, fix:, docs:, etc.)
4. **Pull Requests**: PRs DEVEM ser pequenos (< 400 linhas de código novo), focados e revisáveis
5. **Code Review**: Mínimo de 1 aprovação antes de merge; reviewers DEVEM verificar conformidade com constituição
6. **CI/CD**: Pipeline DEVE executar linting, testes e build em todo PR
7. **Deploy**: Deploys DEVEM ser automatizados e reversíveis; feature flags para releases graduais quando aplicável

**Quality Gates:**
- Lint check: DEVE passar
- Testes: DEVEM passar
- Cobertura: NÃO PODE diminuir
- Build: DEVE completar sem warnings
- Security scan: DEVE passar (sem vulnerabilidades críticas/altas)

## Governance

Esta constituição é o documento autoritativo para padrões de desenvolvimento do projeto AAI Virtual. Em caso de conflito com outras documentações, esta constituição prevalece.

**Procedimento de Emenda:**
1. Propostas de mudança DEVEM ser documentadas com rationale
2. Mudanças DEVEM ser aprovadas por pelo menos 2 membros seniores da equipe
3. Emendas DEVEM incluir plano de migração se afetarem código existente
4. Versão DEVE ser incrementada seguindo semver (MAJOR.MINOR.PATCH)

**Política de Versionamento:**
- **MAJOR**: Mudanças incompatíveis com práticas anteriores ou remoção de princípios
- **MINOR**: Novos princípios ou expansão material de orientações existentes
- **PATCH**: Clarificações, correções de texto, refinamentos não-semânticos

**Expectativas de Compliance:**
- Todo PR/review DEVE verificar conformidade com esta constituição
- Violações DEVEM ser justificadas explicitamente no PR
- Exceções DEVEM ser documentadas no Complexity Tracking do plan.md
- Auditorias de compliance DEVEM ser realizadas trimestralmente

**Version**: 1.0.0 | **Ratified**: 2026-01-01 | **Last Amended**: 2026-01-01
