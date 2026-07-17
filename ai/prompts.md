
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

## PROMPT-003 — Geração da primeira versão completa do contrato OpenAPI

- **Data:** 2026-07-17
- **Ferramenta:** GitHub Copilot Chat
- **Etapa:** Especificação
- **Objetivo:** Gerar a primeira versão completa do contrato da API com base no enunciado e nas decisões registradas.

### Prompt completo

```text
Atue com um especialista de design de APIs e OpenAPI 3.0., usando o como contento as documentações do projeto atual, e com isso atualize o arquivo openapi.yaml de acordo com as descrições abaixo.


Pontos de atenção

Altere somente o arquivo openapi.yaml
Não gere nenhum código ou teste nessa etapa do processo


Considere as entidades abaixo:

Projeto

 id: UUID gerado pela aplicação;
 name: string obrigatória, não pode ser vazia ou conter somente espaços, máximo de 100 caracteres;
 description: string opcional e nullable;
 status: enum `active` ou `archived`, sendo `active` o valor inicial e não podendo ter nenhum valor diferente desses;
 createdAt: date-time UTC gerado pela aplicação.


Tarefa

 id: UUID gerado pela aplicação;
 title: string obrigatória, não pode ser vazia ou conter somente espaços, máximo de 200 caracteres;
 description: string opcional e nullable, sem limite adicional;
 status: enum `pending`, `in_progress` ou `done`, sendo `pending` o valor inicial, e não podendo ter nenhum valor diferente desses;
 priority: enum obrigatório `low`, `medium` ou `high`, não permitindo nenhum valor diferente desses;
 createdAt: date-time UTC gerado pela aplicação, não permitindo que o usuário cadastre manualmente;
 completedAt: date-time UTC nullable, preenchido automaticamente quando a tarefa passa de `in_progress` para `done`;


Considerando essas entidades, represente todos os endpoints abaixo

POST /projetos
GET /projetos
GET /projetos/{id}
PATCH /projetos/{id}
POST /projetos/{id}/tarefas
GET /projetos/{id}/tarefas
PATCH /tarefas/{id}
DELETE /tarefas/{id}



Sobre cada endpoint, considere esses comportamento:

POST

POST /projetos:

	Só aceita no body, name e description, o usuário não pode enviar id, status, ou createdAt, pois, esses dados são inseridos pelo próprio sistema.

	O projeto obrigatoriamente deve iniciar com o status "active

	Em caso se sucesso, ele deve trazer na response os dados do projeto criado e header Location apontando para /projetos/{id}

POST /projetos/{id}/tarefas

	O request aceita somente: title, description e priority.
	O usuário não pode enviar id, status, createdAt, completedAt ou projectId.
	A tarefa deve nascer com status `pending`.
	Em caso se sucesso, ele deve trazer na response a tarefa criada.

GET

GET /projetos

	Possui o filtro opcional de status
	Em caso se sucesso, retorna uma  array de projetos.
	Quando não tiver resultados, retornar 200 OK com [].
	Não incluir regras de paginação

GET /projeto

	Em caso se sucesso, retorna os dados do projeto.
	Quando não tiver resultados, retornar 200 OK com [].

GET /projetos/{id}/tarefas

	Possui o filtro opcional de status e prioridade
	Em caso se sucesso, retorna uma  array de projetos.
	Quando não tiver resultados, retornar 200 OK com [].
	Caso não encontre o id projeto retornar 404
	Não incluir regras de paginação

PATCH

Campos omitidos devem permanecer inalterados.
não permitir campos controlados exclusivamente pela aplicação
rejeitar requests vazios como {}
possuir minProperties: 1
possuir additionalProperties: false

PATCH /projetos/{id}

Permite alterar somente name, description e status.
Só pode permitir que um projeto no status active mude para archived, quando esse projeto não tiver nenhuma tarefa vinculada ao id dele com o status in_progress.
É permitido alterar archived para activeactive
reenviar o mesmo status é aceito como operação idempotente
Em caso se sucesso, projeto atualizado com `200 OK`


PATCH /tarefas/{id}

Permite alterar somente  title e description, status, priority.
o fluxo obrigatório de status é pending → in_progress → done
não é permitido pular etapas dos status
não é permitido retroceder status
reenviar o mesmo status para uma tarefa pending ou in_progress é idempotente
o passar de `in_progress` para `done`, completedAt é preenchido automaticamente
completedAt não pode ser informado ou alterado pelo usuário
uma tarefa com o status "done" não pode ser alterada
qualquer outra tentativa de modificar uma tarefa done retorna "422"
o reenvio isolado de "status": "done" para tarefa já concluída é aceito como idempotente e deve manter o completedAt
retornar a tarefa atualizada com 200 OK


DELETE

DELETE /tarefas/{id}

Uma tarefa só pode se excluída se estiver no status pending, qualquer outro status ela não pode ser excluída
tarefa inexistente: retorn 404 Not Found
tarefa in_progress ou done ao tentar serem excluídas retorna 422 Unprocessable Entity
Em caso se sucesso, deve retornar 204, sem retornar body.


Unicidade

nomes de projetos são únicos globalmente;
títulos de tarefas são únicos dentro do mesmo projeto;
a comparação ignora maiúsculas, minúsculas e espaços no início e no final;
conflitos em criação ou atualização retornam `409 Conflict`.

Normalização

name e title devem ser documentados como valores normalizados sem espaços no início e no final;
name e title vazios ou formados somente por espaços retornam `400`;
description pode ser omitida ou enviada como null;
em PATCH, description igual a null remove a descrição atual;
omitir description mantém o valor atual.


Status HTTP

O conjunto do contrato deve cobrir:

- 200 OK
- 201 Created
- 204 No Content
- 400 Bad Request
- 404 Not Found
- 409 Conflict
- 422 Unprocessable Entity

Inclua em cada endpoint somente os status semanticamente aplicáveis.


ERROS

Utilize:

ValidationProblemDetails para 400
ProblemDetails para 404, 409 e 422
media type application/problem+json

Todos os erros devem conter:

- type;
- title;
- status;
- detail;
- instance;
- code.

Os erros `400` também devem conter:

errors: objeto cujas propriedades possuem arrays de mensagens.

Defina schemas reutilizáveis em components/schemas.

Permita extensões adicionais nos schemas de erro para não impedir propriedades de diagnóstico, como traceId.

Utilize exatamente estes códigos:

- validation_error
- project_not_found
- task_not_found
- project_has_in_progress_tasks
- archived_project_does_not_accept_tasks
- invalid_task_status_transition
- task_cannot_be_deleted
- completed_task_cannot_be_modified
- project_name_conflict
- task_title_conflict


ESPECIFICAÇÃO DE ERROS

Adicione 400 ValidationProblemDetails nos casos:


- JSON inválido;
- UUID inválido;
- campo obrigatório ausente;
- enum inválido;
- name ou title vazio;
- limite de caracteres excedido;
- filtro inválido;
- PATCH sem campos;
- propriedade não permitida ou campo somente de leitura enviado.

E para esses casos defina o code como "validation_error"


Adicione 404 — ProblemDetails nos casos:

- projeto inexistente: "project_not_found";
- tarefa inexistente: "task_not_found".

Adicione 409 — ProblemDetails, para o casos:

- projeto com nome duplicado: code "project_name_conflict";
- tarefa com título duplicado dentro do projeto: code "task_title_conflict".


Adicione  422 — ProblemDetails

- arquivar projeto com tarefa in_progress: "project_has_in_progress_tasks"
- criar tarefa em projeto archived: "archived_project_does_not_accept_tasks"
- transição inválida de status: "invalid_task_status_transition"
- excluir tarefa que não esteja pending: "task_cannot_be_deleted"
- modificar tarefa done: "completed_task_cannot_be_modified"


Schemas

Crie schemas separados para:

- CreateProjectRequest
- UpdateProjectRequest
- ProjectResponse
- CreateTaskRequest
- UpdateTaskRequest
- TaskResponse
- ProjectStatus
- TaskStatus
- TaskPriority
- ProblemDetails
- ValidationProblemDetails
- ErrorCode

Nos schemas de request:

- utilize additionalProperties: false
- não inclua campos somente de leitura
- use required somente para campos obrigatórios
- use nullable: true para description
- use minLength e maxLength quando aplicável
- use minProperties: 1 nos PATCH.

Nos schemas de response:

- marque campos gerados pela aplicação como `readOnly: true`;
- inclua description e completedAt como propriedades nullable;
- considere description e completedAt presentes no response, mesmo quando null.

Exemplos

Inclua exemplos para os casos:

- criação de projeto;
- criação de tarefa;
- consulta de projeto;
- consulta de tarefa;
- lista vazia;
- erro de validação;
- projeto não encontrado;
- tarefa não encontrada;
- conflito de nome de projeto;
- conflito de título de tarefa;
- cada regra de negócio que retorna 422.

Utilize UUIDs válidos e timestamps UTC no formato ISO 8601.

Organização

- use as tags Projetos e Tarefas
- defina operationId em todos os endpoints
- reutilize schemas, parâmetros e responses em components
- não adicione autenticação
- não adicione servidores
- não adicione endpoints não solicitados
- não adicione paginação
- não crie novas regras de negocio que não foram passadas
- preserve o arquivo em UTF-8

Antes de alterar o arquivo, apresente um resumo do que foi aplicado, e após a aplicação apresente o que foi adicionado.
```

### Resultado produzido

Foi gerada a primeira versão completa do `openapi.yaml`, contendo os endpoints, schemas, parâmetros, responses, exemplos e contratos de erro da API TaskFlow.

### Arquivos relacionados

- `openapi.yaml`;
- `docs/decisoes.md`.

### Limitações

A saída representa uma primeira versão produzida pela IA e deverá passar por revisão humana antes de ser considerada aprovada para orientar a implementação.
