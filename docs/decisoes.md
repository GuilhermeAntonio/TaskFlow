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

### DEC-20260717-004 — Reativação de projetos arquivados

- **Data:** 2026-07-17
- **Status:** Aceita
- **Decisão:** Permitir a alteração do status de um projeto de `archived` para `active`.
- **Justificativa:** O enunciado restringe o arquivamento de projetos com tarefas em andamento, mas não define que o arquivamento seja irreversível. A reativação foi permitida para eliminar essa ambiguidade antes da implementação.
- **Alternativas consideradas:** Impedir a reativação de projetos arquivados.
- **Impacto:** Projetos reativados voltarão a aceitar a criação de novas tarefas.

### DEC-20260717-005 — Validação explícita das requisições

- **Data:** 2026-07-17
- **Status:** Aceita
- **Decisão:** Requisições PATCH sem campos atualizáveis e requisições que incluam campos controlados exclusivamente pela aplicação serão rejeitadas com `400 Bad Request`.
- **Justificativa:** Evitar operações sem efeito e impedir que o consumidor interprete campos não permitidos como aceitos pela API.
- **Alternativas consideradas:** Aceitar PATCH vazio como operação sem alteração ou ignorar silenciosamente os campos não permitidos.
- **Impacto:** Esses comportamentos deverão ser documentados no contrato OpenAPI e cobertos pelos testes de contrato.

### DEC-20260717-006 — Identificador estável nos erros

- **Data:** 2026-07-17
- **Status:** Aceita
- **Decisão:** Adicionar a extensão `code` aos retornos `ProblemDetails` e `ValidationProblemDetails`.
- **Justificativa:** Tornar os erros da API adequados para consumidores e testes automatizados, sem depender do texto completo das mensagens.
- **Alternativas consideradas:** Utilizar somente os campos nativos de `ProblemDetails`.
- **Impacto:** Os códigos deverão ser documentados no OpenAPI e mantidos consistentes nos testes e na implementação.

#### Códigos de erro definidos

- `validation_error`;
- `project_not_found`;
- `task_not_found`;
- `project_has_in_progress_tasks`;
- `archived_project_does_not_accept_tasks`;
- `invalid_task_status_transition`;
- `task_cannot_be_deleted`;
- `completed_task_cannot_be_modified`;
- `project_name_conflict`;
- `task_title_conflict`.

### DEC-20260717-007 — Paginação fora da primeira versão

- **Data:** 2026-07-17
- **Status:** Aceita
- **Decisão:** Não incluir paginação na primeira versão do contrato da API.
- **Justificativa:** A primeira versão priorizará os endpoints obrigatórios, as regras de negócio e os testes de aderência.
- **Alternativas consideradas:** Definir desde o início parâmetros e envelopes de paginação.
- **Impacto:** As listagens retornarão arrays completos. Caso a paginação seja adicionada posteriormente, o contrato OpenAPI deverá ser atualizado e validado antes da implementação.

### DEC-20260717-008 — Imutabilidade de tarefas concluídas

- **Data:** 2026-07-17
- **Status:** Aceita
- **Decisão:** Uma tarefa com status `done` não poderá ter título, descrição, prioridade ou status alterados. O reenvio isolado de `status: done` será aceito como operação idempotente, preservando o valor original de `completedAt`.
- **Justificativa:** A tarefa concluída representa um trabalho encerrado. Torná-la imutável evita alterações posteriores que modifiquem o registro da conclusão.
- **Alternativas consideradas:** Permitir a edição de campos não relacionados ao status após a conclusão.
- **Impacto:** Tentativas de modificar uma tarefa concluída retornarão `422 Unprocessable Entity` com o código `completed_task_cannot_be_modified`.

### DEC-20260717-009 — Unicidade de projetos e tarefas

- **Data:** 2026-07-17
- **Status:** Aceita
- **Decisão:** Os nomes dos projetos serão únicos globalmente. Os títulos das tarefas serão únicos dentro do respectivo projeto. A comparação ignorará diferenças entre letras maiúsculas e minúsculas e desconsiderará espaços no início e no final.
- **Justificativa:** A unicidade reduz ambiguidades na identificação dos recursos e evita registros equivalentes gerados apenas por diferenças de formatação.
- **Alternativas consideradas:** Permitir nomes de projetos e títulos de tarefas duplicados.
- **Impacto:** Conflitos durante criação ou atualização retornarão `409 Conflict` com `ProblemDetails`. Serão utilizados os códigos `project_name_conflict` e `task_title_conflict`.
