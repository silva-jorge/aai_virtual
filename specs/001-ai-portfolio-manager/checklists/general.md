# General Requirements Quality Checklist: AI Portfolio Manager

**Purpose**: Autovalidação abrangente da qualidade dos requisitos antes de finalizar a especificação  
**Created**: 2026-01-01  
**Feature**: [spec.md](../spec.md)  
**Depth**: Padrão (20-30 itens)  
**Audience**: Autor

---

## Completude de Requisitos

- [ ] CHK001 - São definidos requisitos para todas as 6 classes de ativos mencionadas (ações, ETFs, FIIs, renda fixa, internacional, cripto)? [Completeness, Spec §FR-001]
- [ ] CHK002 - Os requisitos de importação de dados especificam todos os formatos suportados e suas estruturas esperadas? [Completeness, Spec §FR-004]
- [ ] CHK003 - São definidos requisitos para o comportamento do sistema quando APIs externas estão indisponíveis? [Completeness, Edge Cases]
- [ ] CHK004 - Os requisitos de backup/restore especificam formato do arquivo, validação de integridade e tratamento de erros? [Completeness, Spec §FR-031]
- [ ] CHK005 - São definidos requisitos para todos os tipos de eventos corporativos (split, grupamento, bonificação, dividendos)? [Completeness, Gap]

## Clareza e Especificidade

- [ ] CHK006 - O termo "variação significativa" está quantificado com threshold específico (>5% mencionado apenas em US6)? [Clarity, Spec §FR-024]
- [ ] CHK007 - Os critérios de "relevância ao portfólio" para notícias estão explicitamente definidos? [Clarity, Spec §FR-037]
- [ ] CHK008 - A expressão "recomendações baseadas em análise de IA" especifica quais dados são considerados na análise? [Clarity, Spec §FR-013]
- [ ] CHK009 - Os perfis de risco (conservador, moderado, agressivo) têm parâmetros quantificados (% alocação, volatilidade)? [Clarity, Spec §FR-020]
- [ ] CHK010 - O termo "delay de 15-20 minutos" é consistente com requisitos de atualização em tempo real para alertas? [Clarity, Conflict]

## Consistência Interna

- [ ] CHK011 - Os requisitos de armazenamento local (IndexedDB) são consistentes com a arquitetura de backend (BFF) descrita? [Consistency, Spec §FR-028 vs Assumptions]
- [ ] CHK012 - Os thresholds de rebalanceamento configuráveis pelo usuário são consistentes com os exemplos (5%, 10%, 15%)? [Consistency, US2]
- [ ] CHK013 - Os benchmarks mencionados (Ibovespa, CDI, IPCA+) são consistentes em todas as seções onde aparecem? [Consistency, Spec §FR-007]
- [ ] CHK014 - A priorização das user stories (P1/P2) está alinhada com as dependências técnicas entre elas? [Consistency]

## Qualidade de Critérios de Aceitação

- [ ] CHK015 - O critério "100% das recomendações incluem justificativa" pode ser verificado objetivamente? [Measurability, SC-004]
- [ ] CHK016 - O critério "90% das notícias corretamente classificadas" define como medir a corretude? [Measurability, SC-009]
- [ ] CHK017 - Os critérios de tempo (< 3s, < 2s, < 5min) especificam condições de teste (dispositivo, rede)? [Measurability, SC-001, SC-016]
- [ ] CHK018 - O critério "precisão de ±5%" para simulações define baseline de comparação? [Measurability, SC-006]

## Cobertura de Cenários

- [ ] CHK019 - São definidos requisitos para o estado inicial (zero-state) quando não há posições no portfólio? [Coverage, Gap]
- [ ] CHK020 - Os cenários de aceitação cobrem fluxos de erro além do happy path para cada user story? [Coverage, Exception Flow]
- [ ] CHK021 - São definidos requisitos para comportamento offline com dados em cache? [Coverage, Spec §Assumptions-5]
- [ ] CHK022 - Os requisitos cobrem cenários de uso mobile vs desktop (responsividade mencionada)? [Coverage, Gap]

## Casos de Borda e Exceções

- [ ] CHK023 - O comportamento para ativos com cotação zero ou negativa está especificado? [Edge Case, Gap]
- [ ] CHK024 - Os requisitos definem tratamento para conflito entre recomendação IA e perfil de risco do usuário? [Edge Case, Spec §Edge Cases]
- [ ] CHK025 - O comportamento para timeout ou falha parcial da API de LLM está definido? [Edge Case, Gap]
- [ ] CHK026 - Os requisitos especificam limite de dados históricos (10+ anos mencionado) e comportamento ao atingir limite? [Edge Case, Spec §Assumptions-4]

## Requisitos Não-Funcionais

- [ ] CHK027 - Os requisitos de criptografia especificam algoritmo (AES-256 mencionado apenas em SC-013) de forma consistente? [NFR, Spec §FR-029 vs SC-013]
- [ ] CHK028 - Os requisitos de acessibilidade (WCAG 2.1 AA) estão documentados nas user stories de interface? [NFR, Gap]
- [ ] CHK029 - Os requisitos de performance definem budgets para todas as operações críticas? [NFR, SC-001 a SC-019]

## Dependências e Premissas

- [ ] CHK030 - A premissa de APIs gratuitas (Yahoo Finance, Brapi, Alpha Vantage) está validada com termos de uso? [Assumption, Spec §Assumptions-2]
- [ ] CHK031 - A premissa de "consentimento explícito para LLM" especifica formato e momento da coleta? [Assumption, Spec §FR-033]
- [ ] CHK032 - A dependência de IndexedDB para armazenamento local considera limites de storage por navegador? [Dependency, Gap]

---

## Resumo

| Dimensão | Itens | Foco |
|----------|-------|------|
| Completude | CHK001-005 | Requisitos presentes para todos os cenários |
| Clareza | CHK006-010 | Termos específicos e quantificados |
| Consistência | CHK011-014 | Alinhamento entre seções |
| Mensurabilidade | CHK015-018 | Critérios objetivamente verificáveis |
| Cobertura | CHK019-022 | Todos os fluxos endereçados |
| Edge Cases | CHK023-026 | Comportamentos de exceção definidos |
| NFRs | CHK027-029 | Performance, segurança, acessibilidade |
| Dependências | CHK030-032 | Premissas validadas e documentadas |

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
