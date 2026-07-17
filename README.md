# TaskFlow

TaskFlow é uma API REST para gerenciamento de projetos e tarefas, desenvolvida seguindo Specification-Driven Development (SDD).

**Tecnologias:** .NET 8, ASP.NET Core Web API baseada em Controllers, Entity Framework Core, SQLite, ProblemDetails, xUnit e WebApplicationFactory.

## Abordagem SDD

O contrato da API é definido e validado no arquivo `openapi.yaml` antes do início da implementação. As decisões técnicas e as revisões das sugestões produzidas por IA são versionadas junto ao código.

**Fluxo SDD resumido:**

1. Especificar o contrato e os comportamentos.
2. Validar criticamente a especificação.
3. Implementar guiado pela especificação.
4. Provar a aderência por meio de testes automatizados.

## Requisitos obrigatórios e decisões adotadas

**Requisitos obrigatórios:**

- .NET 8 ou superior
- ASP.NET Core Web API (Controllers)
- Entity Framework Core
- Persistência com SQLite ou in-memory
- Respostas de erro no formato `ProblemDetails`
- xUnit e `WebApplicationFactory` para testes de integração

**Decisões adotadas:**

- Uso específico do .NET 8 como versão alvo;
- Persistência principal com SQLite, em vez de banco in-memory;
- Não utilizar frameworks de SDD, conduzindo explicitamente o fluxo de Specification-Driven Development por meio dos artefatos versionados no repositório.

---

As decisões técnicas estão registradas em `docs/decisoes.md`, e as revisões humanas das sugestões da IA estão documentadas em `ai/revisoes.md`.