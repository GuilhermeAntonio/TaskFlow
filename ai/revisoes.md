# Registro de Revisões

Este arquivo documenta revisões humanas aplicadas aos prompts, sugestões da IA e artefatos gerados, incluindo o resultado da revisão e a justificativa técnica associada.

## Modelo de entrada de revisão

- **ID:** Revisao-YYYYMMDD-###
- **Data:** YYYY-MM-DD
- **Revisor:** Nome do revisor humano
- **Origem:** (ex.: prompt, ai/prompts.md, openapi.yaml)
- **Partes aceitas:** Lista das partes do artefato aceitas sem alteração
- **Partes corrigidas:** Lista das partes que foram alteradas e descrição da correção
- **Partes rejeitadas:** Lista das partes rejeitadas e motivo
- **Decisão:** Aceita | Corrigida | Rejeitada
- **Ação realizada:** Resumo das mudanças aplicadas
- **Arquivo(s) afetados:** Lista de arquivos atualizados
- **Notas adicionais / Justificativa técnica:** Explicação técnica da decisão tomada durante a revisão

## Histórico

- **Revisao-20260717-001** — 2026-07-17 — Revisor: Guilherme Bezerra Antonio — Origem: Revisão humana da geração inicial — Decisão: Corrigida
  - **Partes aceitas:** Estrutura de diretórios; ausência de implementação; versão OpenAPI 3.0.3 como esqueleto inicial.
  - **Partes corrigidas:** Sequência do fluxo SDD no `README.md`; registro das habilidades em `ai/skills.md`; remoção da seção `servers` em `openapi.yaml`.
  - **Partes rejeitadas:** Suposições não solicitadas sobre PostgreSQL, produção, deploy e migração; menção à geração de endpoints; registro de autor fictício.
  - **Ação realizada:** Atualização dos arquivos para alinhá-los ao enunciado e às decisões aprovadas.
  - **Arquivos afetados:** `README.md`, `ai/skills.md`, `ai/prompts.md`, `ai/revisoes.md`, `docs/decisoes.md` e `openapi.yaml`.
  - **Notas adicionais / Justificativa técnica:** As correções removem pressupostos não sustentados pelo enunciado e garantem que o contrato OpenAPI permaneça, nesta etapa, como um esqueleto sem declarar um ambiente de execução ainda não configurado. A remoção de exemplos fictícios de infraestrutura evita decisões prematuras e preserva a separação entre requisitos obrigatórios e escolhas técnicas realizadas no projeto.