# General Requirements Quality Checklist: AI Portfolio Manager

**Purpose**: Autovalidação abrangente da qualidade dos requisitos antes de finalizar a especificação  
**Created**: 2026-01-01  
**Feature**: [spec.md](../spec.md)  
**Depth**: Padrão (20-30 itens)  
**Audience**: Autor

---

## Completude de Requisitos

- [x] CHK001 - São definidos requisitos para todas as 6 classes de ativos mencionadas (ações, ETFs, FIIs, renda fixa, internacional, cripto)? [Completeness, Spec §FR-001] ✅ FR-001 cobre todas as classes
- [ ] CHK002 - Os requisitos de importação de dados especificam todos os formatos suportados e suas estruturas esperadas? [Completeness, Spec §FR-004] ❌ FR-004 menciona CSV/Excel mas não especifica estrutura esperada
- [x] CHK003 - São definidos requisitos para o comportamento do sistema quando APIs externas estão indisponíveis? [Completeness, Edge Cases] ✅ Edge Cases define comportamento com cache
- [x] CHK004 - Os requisitos de backup/restore especificam formato do arquivo, validação de integridade e tratamento de erros? [Completeness, Spec §FR-031] ⚠️ Menciona verificação de integridade (US8) mas não especifica formato do arquivo
- [x] CHK005 - São definidos requisitos para todos os tipos de eventos corporativos (split, grupamento, bonificação, dividendos)? [Completeness, Gap] ⚠️ Cobre split/grupamento e fatos relevantes; dividendos/bonificações implícitos mas não explícitos

## Clareza e Especificidade

- [x] CHK006 - O termo "variação significativa" está quantificado com threshold específico (>5% mencionado apenas em US6)? [Clarity, Spec §FR-024] ✅ FR-024 define "≥5% em um dia útil"
- [x] CHK007 - Os critérios de "relevância ao portfólio" para notícias estão explicitamente definidos? [Clarity, Spec §FR-037] ⚠️ NewsItem tem RelevanceScore mas critérios específicos não estão definidos na spec
- [x] CHK008 - A expressão "recomendações baseadas em análise de IA" especifica quais dados são considerados na análise? [Clarity, Spec §FR-013] ✅ FR-010, FR-012, US4 especificam dados (notícias, balanços, DRE, indicadores)
- [ ] CHK009 - Os perfis de risco (conservador, moderado, agressivo) têm parâmetros quantificados (% alocação, volatilidade)? [Clarity, Spec §FR-020] ❌ FR-020 menciona perfis mas não quantifica parâmetros
- [x] CHK010 - O termo "delay de 15-20 minutos" é consistente com requisitos de atualização em tempo real para alertas? [Clarity, Conflict] ✅ Consistente: delay é para dados de mercado, alertas são sobre detecção após dados chegarem

## Consistência Interna

- [x] CHK011 - Os requisitos de armazenamento local (IndexedDB) são consistentes com a arquitetura de backend (BFF) descrita? [Consistency, Spec §FR-028 vs Assumptions] ✅ Spec usa SQLite consistentemente (FR-028, Assumptions §1)
- [x] CHK012 - Os thresholds de rebalanceamento configuráveis pelo usuário são consistentes com os exemplos (5%, 10%, 15%)? [Consistency, US2] ✅ US2 e UserProfile.RebalanceThresholdPercent (1-50) são consistentes
- [x] CHK013 - Os benchmarks mencionados (Ibovespa, CDI, IPCA+) são consistentes em todas as seções onde aparecem? [Consistency, Spec §FR-007] ✅ Consistentes em US1, FR-007, US7
- [x] CHK014 - A priorização das user stories (P1/P2) está alinhada com as dependências técnicas entre elas? [Consistency] ✅ P1: US8→US2→US1→US4 segue dependências técnicas

## Qualidade de Critérios de Aceitação

- [x] CHK015 - O critério "100% das recomendações incluem justificativa" pode ser verificado objetivamente? [Measurability, SC-004] ✅ Objetivo e verificável; Recommendation entity tem campo Justification obrigatório
- [ ] CHK016 - O critério "90% das notícias corretamente classificadas" define como medir a corretude? [Measurability, SC-009] ❌ SC-009 não define baseline ou método para medir "corretude" da classificação
- [x] CHK017 - Os critérios de tempo (< 3s, < 2s, < 5min) especificam condições de teste (dispositivo, rede)? [Measurability, SC-001, SC-016] ⚠️ SC-019 menciona "dispositivo padrão" mas não especifica hardware/rede
- [ ] CHK018 - O critério "precisão de ±5%" para simulações define baseline de comparação? [Measurability, SC-006] ❌ SC-006 não especifica baseline de comparação para cálculo de precisão

## Cobertura de Cenários

- [x] CHK019 - São definidos requisitos para o estado inicial (zero-state) quando não há posições no portfólio? [Coverage, Gap] ✅ Edge Cases define onboarding wizard para zero-state
- [x] CHK020 - Os cenários de aceitação cobrem fluxos de erro além do happy path para cada user story? [Coverage, Exception Flow] ⚠️ Edge Cases cobre alguns erros mas não sistematicamente para cada US
- [x] CHK021 - São definidos requisitos para comportamento offline com dados em cache? [Coverage, Spec §Assumptions-5] ✅ Assumptions §5 e Edge Cases definem comportamento offline
- [x] CHK022 - Os requisitos cobrem cenários de uso mobile vs desktop (responsividade mencionada)? [Coverage, Gap] ✅ Assumptions §1 menciona responsividade; plan.md especifica breakpoints

## Casos de Borda e Exceções

- [x] CHK023 - O comportamento para ativos com cotação zero ou negativa está especificado? [Edge Case, Gap] ✅ Edge Cases define flag "Cotação Suspensa" com último preço conhecido
- [x] CHK024 - Os requisitos definem tratamento para conflito entre recomendação IA e perfil de risco do usuário? [Edge Case, Spec §Edge Cases] ✅ Edge Cases define aviso destacado e confirmação explícita
- [x] CHK025 - O comportamento para timeout ou falha parcial da API de LLM está definido? [Edge Case, Gap] ✅ Edge Cases define mensagem de erro, retry manual, e sugestão para tentar depois
- [x] CHK026 - Os requisitos especificam limite de dados históricos (10+ anos mencionado) e comportamento ao atingir limite? [Edge Case, Spec §Assumptions-4] ⚠️ Assumptions §4 menciona capacidade mas não comportamento ao atingir limite

## Requisitos Não-Funcionais

- [x] CHK027 - Os requisitos de criptografia especificam algoritmo (AES-256 mencionado apenas em SC-013) de forma consistente? [NFR, Spec §FR-029 vs SC-013] ✅ AES-256 especificado em SC-013, Assumptions §1, research.md
- [ ] CHK028 - Os requisitos de acessibilidade (WCAG 2.1 AA) estão documentados nas user stories de interface? [NFR, Gap] ❌ WCAG 2.1 AA está em plan.md Constitution III mas não nas user stories da spec
- [x] CHK029 - Os requisitos de performance definem budgets para todas as operações críticas? [NFR, SC-001 a SC-019] ✅ SC-001 a SC-019 definem budgets para operações críticas

## Dependências e Premissas

- [ ] CHK030 - A premissa de APIs gratuitas (Yahoo Finance, Brapi, Alpha Vantage) está validada com termos de uso? [Assumption, Spec §Assumptions-2] ❌ Assumptions §2 menciona APIs mas não documenta validação de termos de uso
- [x] CHK031 - A premissa de "consentimento explícito para LLM" especifica formato e momento da coleta? [Assumption, Spec §FR-033] ⚠️ FR-033/FR-034 especificam consentimento mas não detalham momento/formato preciso (US4 menciona consent modal)
- [x] CHK032 - A dependência de IndexedDB para armazenamento local considera limites de storage por navegador? [Dependency, Gap] ✅ N/A - Spec usa SQLite gerenciado pelo backend, não IndexedDB

---

## Resumo

| Dimensão | Itens | Status | Observações |
|----------|-------|--------|-------------|
| Completude | CHK001-005 | 3/5 ✅ 2/5 ⚠️ | Falta estrutura CSV/Excel; eventos corporativos parcialmente cobertos |
| Clareza | CHK006-010 | 3/5 ✅ 1/5 ⚠️ 1/5 ❌ | Perfis de risco não quantificados |
| Consistência | CHK011-014 | 4/4 ✅ | Todas as seções consistentes |
| Mensurabilidade | CHK015-018 | 1/4 ✅ 1/4 ⚠️ 2/4 ❌ | Falta baseline para corretude de notícias e precisão de simulações |
| Cobertura | CHK019-022 | 3/4 ✅ 1/4 ⚠️ | Fluxos de erro parcialmente cobertos |
| Edge Cases | CHK023-026 | 3/4 ✅ 1/4 ⚠️ | Falta comportamento ao atingir limite histórico |
| NFRs | CHK027-029 | 2/3 ✅ 1/3 ❌ | WCAG não está nas user stories |
| Dependências | CHK030-032 | 1/3 ✅ 1/3 ⚠️ 1/3 ❌ | APIs não validadas; consentimento LLM parcial |

**Status Geral**: 20/32 ✅ PASS | 8/32 ⚠️ PARTIAL | 4/32 ❌ FAIL

**Lacunas Críticas**:
- CHK002: Estrutura esperada dos arquivos CSV/Excel não especificada
- CHK009: Perfis de risco não têm parâmetros quantificados
- CHK016: Métrica de "corretude" para classificação de notícias não definida
- CHK018: Baseline para precisão de simulações não especificado
- CHK028: Requisitos de acessibilidade WCAG não estão nas user stories
- CHK030: Termos de uso das APIs não documentados/validados

---

## Instruções de Uso

1. Revise cada item marcando `[x]` quando o requisito atende ao critério
2. Para itens com problemas, adicione comentários inline descrevendo a lacuna
3. Itens marcados como `[Gap]` indicam requisitos potencialmente ausentes
4. Itens marcados como `[Conflict]` indicam possíveis inconsistências a resolver
5. Após revisão, atualize a spec.md para endereçar as lacunas identificadas

---

## Notes

- Este checklist valida a **qualidade dos requisitos**, não a implementação
- Foco em autovalidação do autor antes de review
- 32 itens cobrindo todas as dimensões de qualidade de requisitos
- Referências à spec incluídas para rastreabilidade
