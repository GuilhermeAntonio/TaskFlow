
# Registro de Prompts

## PROMPT-001 — Estrutura documental inicial

**Prompt:**


```text
Atuando como um engenheiro de software .NET responsável por preparar a estrutura inicial de um projeto conduzido por Specification-Driven Development (SDD), para um projeto chamado TaskFlow, uma API REST para gerenciamento de projetos e tarefas. Siga as seguintes
orientações:

Requisitos técnicos já definidos:

- .NET 8;
- ASP.NET Core Web API baseada em Controllers;
- Entity Framework Core;
- SQLite;
- respostas de erro no formato ProblemDetails;
- testes de integração com xUnit e WebApplicationFactory;
- contrato da API definido em OpenAPI 3.0 antes da implementação.

Essa é a etapa inicial no projeto no qual estamos preparando as especificações

Não crie:

- solution ou projetos .NET;
- Controllers;
- Services;
- entidades;
- DTOs;
- DbContext;
- migrations;
- testes;
- qualquer código de implementação.

Crie somente a seguinte estrutura documental que dará a estrutura base para o projeto com SDD:

TaskFlow/
├── ai/
│   ├── prompts.md
│   ├── revisoes.md
│   └── skills.md
├── docs/
│   └── decisoes.md
├── openapi.yaml
└── README.md

Regras para os arquivos:

1. O README.md deve apresentar o projeto, explicar o fluxo SDD do projeto e informar a fase atual de estruturação do projeto.
2. O openapi.yaml deve conter somente a estrutura inicial válida em OpenAPI 3.0.3.
3. O docs/decisoes.md deve possuir uma estrutura para registrar decisões, justificativas, alternativas consideradas e status.
4. O ai/skills.md deve possuir uma estrutura para registrar habilidades ou áreas de conhecimento delegadas à IA.
5. O ai/prompts.md deve registrar este prompt, sua finalidade e o resultado produzido.
6. O ai/revisoes.md deve possuir uma estrutura para registrar sugestões aceitas, corrigidas ou rejeitadas após revisão humana.
7. Utilize Markdown em português.
8. Não altere o arquivo .gitignore.

Antes de criar os arquivos, apresente resumidamente quais arquivos serão criados e qual será a responsabilidade de cada um. Depois, realize as alterações no workspace.


```


**Finalidade:** Registrar o escopo e as restrições para a geração dos artefatos iniciais de SDD.

**Resultado produzido (etapa inicial):** Artefatos documentais conforme lista de arquivos afetados.

**Arquivos afetados:** `ai/prompts.md`, `ai/revisoes.md`, `ai/skills.md`, `docs/decisoes.md`, `openapi.yaml`, `README.md`.

**Limitações:** A geração foi restrita aos artefatos documentais iniciais e não incluiu código de implementação, endpoints ou schemas de domínio. O `openapi.yaml` foi produzido somente como um esqueleto inicial.

## PROMPT-002 — Revisão crítica da estrutura documental

**Prompt:**

```text
Revise criticamente os artefatos documentais que você acabou de gerar para o projeto TaskFlow.

Não crie novos arquivos e não produza nenhum código de implementação. Altere somente:

- README.md;
- openapi.yaml;
- docs/decisoes.md;
- ai/skills.md;
- ai/prompts.md;
- ai/revisoes.md.

Aplique as seguintes correções:

1. No README.md, represente o fluxo SDD nesta ordem:
   - especificar o contrato e os comportamentos;
   - validar criticamente a especificação;
   - implementar guiado pela especificação;
   - provar a aderência por meio de testes automatizados.

2. Remova a afirmação de que os testes serão escritos depois da implementação, mas falharão inicialmente para guiá-la. Não introduza TDD como decisão do projeto, pois isso não foi definido.

3. No openapi.yaml, remova a seção `servers`, porque nenhuma porta de execução foi definida. Mantenha somente um template mínimo e válido em OpenAPI 3.0.3, sem endpoints ou schemas.

4. Em docs/decisoes.md:
   - altere o título para “Decisões de Design e Arquitetura”;
   - mantenha um template para decisões;
   - remova o exemplo fictício sobre PostgreSQL, produção, deploy e migração;
   - não invente autores, ambientes ou alternativas ainda não analisadas;
   - utilize os status: Proposta, Aceita, Rejeitada e Substituída.

5. Em ai/skills.md:
   - identifique a ferramenta como GitHub Copilot Chat;
   - registre como habilidade a estruturação documental inicial do fluxo SDD;
   - informe como entradas o contexto técnico e as restrições do prompt;
   - informe como saídas os seis documentos gerados;
   - defina Guilherme Bezerra Antonio como responsável pela revisão humana;
   - deixe claro que a IA não possui autoridade para aprovar requisitos ou decisões;
   - remova a afirmação de que foram gerados endpoints, pois nenhum endpoint foi e deve ser criado nessa etapa.

6. Em ai/prompts.md:
   - mantenha o prompt utilizado;
   - caso o texto registrado não seja integral, use o título “Resumo do prompt”, e não “Prompt original”;
   - informe finalidade, resultado, arquivos afetados e limitações da geração;
   - adicione este prompt de revisão como um novo registro.

7. Em ai/revisoes.md:
   - remova a revisão fictícia com “Revisor: exemplo”;
   - registre a revisão humana real desta primeira geração;
   - identifique como partes aceitas: estrutura de diretórios, ausência de implementação e versão OpenAPI 3.0.3;
   - identifique como partes corrigidas: sequência do fluxo SDD no README, registro real das habilidades delegadas e remoção do servidor ainda não configurado;
   - identifique como partes rejeitadas: suposições sobre PostgreSQL, produção, deploy, migração, equipe fictícia e geração de endpoints que não ocorreu;
   - registre Guilherme Bezerra Antonio como revisor;
   - explique a justificativa técnica de cada decisão baseada no fato de que foram usados exemplos e predeterminações que não foram solicitadas.

8. Diferencie claramente requisitos obrigatórios de decisões adotadas no projeto.

9. Preserve todos os arquivos em UTF-8 e em português.

10. Não altere o arquivo .gitignore.

Antes de aplicar as alterações, apresente um resumo do que será modificado. Após aplicar, mostre os arquivos alterados.

```

**Finalidade da revisão:** Garantir que os documentos reflitam apenas as decisões e requisitos explicitamente solicitados, corrigir exemplos inventados e melhorar rastreabilidade.

**Resultado produzido pela revisão:** Atualizações em `README.md`, `openapi.yaml`, `docs/decisoes.md`, `ai/skills.md`, `ai/revisoes.md` e `ai/prompts.md` para atender às instruções de revisão.

**Arquivos afetados pela revisão:** `README.md`, `openapi.yaml`, `docs/decisoes.md`, `ai/skills.md`, `ai/revisoes.md`, `ai/prompts.md`.

**Limitações da revisão:** Alterações restritas à documentação; não houve geração de código, endpoints ou schemas de domínio.

**Registro de revisão do prompt:**

- **ID:** Revisao-Prompt-20260717-001
- **Data:** 2026-07-17
- **Revisor:** Guilherme Bezerra Antonio
- **Origem:** Solicitação de revisão humana
- **Resumo da ação:** Ajustes aplicados conforme as instruções de revisão (sequência SDD, remoção de servers do OpenAPI, template de decisões, registro de habilidades e padronização do nome do revisor).