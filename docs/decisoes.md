# Decisões de Design e Arquitetura

Este documento registra as decisões de design e arquitetura, suas justificativas, alternativas consideradas e impactos no projeto.

## Template de decisão

- **ID da decisão:** DEC-YYYYMMDD-001
- **Título:** Resumo curto da decisão
- **Data:** YYYY-MM-DD
- **Status:** Proposta | Aceita | Rejeitada | Substituída
- **Decisão:** Descrição clara da decisão adotada
- **Justificativa:** Por que essa decisão foi tomada (fundamentação técnica)
- **Alternativas consideradas:** Lista das alternativas e motivos de não-adoção (se aplicável)
- **Impacto:** Impacto técnico e de processo
- **Referências:** Links para discussões, PRs, issues

## Observação sobre requisitos vs decisões

Os requisitos obrigatórios são derivados do enunciado do desafio e refletidos nos diferentes artefatos do projeto. O contrato OpenAPI (`openapi.yaml`) formaliza especificamente a interface HTTP, incluindo endpoints, parâmetros, requests, responses e erros. Este documento registra as escolhas técnicas e arquiteturais adotadas no projeto.

## Decisões registradas

### DEC-20260717-001 — Condução manual do fluxo SDD

- **Data:** 2026-07-17
- **Status:** Aceita
- **Decisão:** Não utilizar frameworks de SDD, conduzindo explicitamente o processo por meio dos artefatos versionados no repositório.
- **Justificativa:** Tornar visíveis a evolução da especificação, as decisões técnicas, os prompts utilizados e as revisões humanas realizadas durante o projeto.
- **Alternativas consideradas:** Utilização do SpecKit.
- **Impacto:** Os artefatos de especificação e rastreabilidade deverão ser mantidos manualmente durante o desenvolvimento.

### DEC-20260717-002 — Utilização do .NET 8

- **Data:** 2026-07-17
- **Status:** Aceita
- **Decisão:** Utilizar o .NET 8 como versão alvo da aplicação.
- **Justificativa:** A versão atende ao requisito mínimo do desafio, possui suporte de longo prazo e está disponível no ambiente de desenvolvimento.
- **Alternativas consideradas:** Versões superiores do .NET.
- **Impacto:** A solução e seus projetos deverão utilizar `net8.0` como target framework.

### DEC-20260717-003 — Persistência com SQLite

- **Data:** 2026-07-17
- **Status:** Aceita
- **Decisão:** Utilizar Entity Framework Core com SQLite como persistência principal da aplicação.
- **Justificativa:** SQLite permite uma persistência relacional real, execução local simples e não exige infraestrutura externa.
- **Alternativas consideradas:** Banco in-memory, também permitido pelo desafio.
- **Impacto:** Os testes deverão garantir isolamento dos dados e controle do estado do banco entre as execuções.