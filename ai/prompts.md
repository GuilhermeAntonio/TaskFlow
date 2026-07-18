
# Registro de Prompts

## PROMPT-001 — Estrutura documental inicial

- **Data:** 2026-07-17
- **Ferramenta:** GitHub Copilot Chat
- **Objetivo:** Criar a estrutura documental inicial do projeto TaskFlow e documentar o processo de geração.

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

- **Data:** 2026-07-17
- **Ferramenta:** GitHub Copilot Chat
- **Objetivo:** Rever e corrigir os artefatos documentais iniciais do projeto TaskFlow.

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


## PROMPT-004 — Refinamento do contrato OpenAPI

- **Data:** 2026-07-17
- **Ferramenta:** GitHub Copilot Chat
- **Etapa:** Especificação
- **Objetivo:** Refinar o arquivo `openapi.yaml`, corrigindo schemas, exemplos de erros e descrições das operações sem alterar as decisões já aprovadas.

### Prompt completo

```text
Revisando o arquivo openapi.yaml, identifiquei que precisa ser feito alguns ajustes.

Nesse caso não é necessário criar um novo arquivo, apenas ajustar o que for solicitado. 

1. Melhorar o response de erro 400

O componente BadRequest atualmente possui somente um exemplo genérico.

Para o BadRequest mantenha o schema ValidationProblemDetails e adicione exemplos nomeados para casos:

- corpo vazio em uma requisição PATCH;
- propriedade não permitida pelo schema;
- envio de campo controlado pela aplicação, como id, createdAt, completedAt ou projectId;
- UUID inválido no parâmetro de rota;
- filtro inválido;
- valor inválido para um enum;
- nome ou título acima do limite máximo de caracteres;
- nome ou título contendo somente espaços.

Todos os exemplos devem utilizar:

- content type application/problem+json;
- status 400;
- code validation_error;
- campo errors com mensagens relacionadas ao erro demonstrado;
- valores concretos no campo instance, sem placeholders como {id}.

Não altere os responses 404, 409 e 422 que já foram separados por rota.

2. Adicionar descrições nos endpoints de criação

No POST /projetos, deixe explícito que:

- id é gerado automaticamente;
- createdAt é preenchido automaticamente em UTC;
- status é definido inicialmente como active;
- esses campos não podem ser enviados pelo cliente;
- o nome é normalizado removendo espaços no início e no final.

No POST /projetos/{id}/tarefas, deixe explícito que:

- id é gerado automaticamente;
- createdAt é preenchido automaticamente em UTC;
- status é definido inicialmente como pending;
- completedAt inicia como null;
- projectId é obtido pelo parâmetro da rota;
- esses campos não podem ser enviados pelo cliente;
- o título é normalizado removendo espaços no início e no final;
- não é permitido criar tarefas em um projeto arquivado.

3. Documentar os campos de data

Adicione descrições nos schemas de resposta informando que:

- ProjectResponse.createdAt é gerado pela aplicação em UTC;
- TaskResponse.createdAt é gerado pela aplicação em UTC;
- TaskResponse.completedAt é preenchido automaticamente em UTC quando a tarefa passa de in_progress para done;
- completedAt permanece null enquanto a tarefa não estiver concluída;
- esses campos são somente leitura.

4. Reforçar as restrições nos schemas de resposta

No ProjectResponse.name, adicione as restrições compatíveis com o request:

- minLength 1;
- maxLength 100;
- pattern para impedir strings formadas somente por espaços.

No TaskResponse.title, adicione:

- minLength 1;
- maxLength 200;
- pattern para impedir strings formadas somente por espaços.

Não remova os campos required já definidos nos schemas de resposta.

5. Restrições gerais

Não crie novos endpoints.

Não adicione autenticação, paginação, servidor, banco de dados, migrations, deploy ou tecnologias que não façam parte do contrato atual.

Não altere as regras de transição de status.

Não altere o comportamento idempotente dos PATCH.

Não altere os nomes dos códigos de erro existentes.

Não altere os UUIDs usados nos exemplos atuais.

Não modifique os arquivos ai/prompts.md, ai/revisoes.md ou docs/decisoes.md.

Não defina uma ordenação para as listagens, pois essa decisão ainda será analisada separadamente.

Não faça commit.

Antes de finalizar, confira:

- se todos os $ref apontam para componentes existentes;
- se nenhum instance possui {id};
- se não existem componentes sem uso;
- se a indentação do YAML permanece válida;
- se não foram introduzidos espaços no final das linhas.
```

### Resultado produzido

Foi gerada uma segunda versão completa do `openapi.yaml`, contendo os endpoints, schemas, parâmetros, responses, exemplos e contratos de erro da API TaskFlow.

### Arquivos relacionados

- `openapi.yaml`.

### Limitações

A saída representa uma nova versão do contrato produzida pela IA a partir dos pontos identificados durante a revisão humana. O resultado ainda deve ser validado antes de ser considerado aprovado para orientar a implementação.

---

## PROMPT-005 — Planejamento e criação do bootstrap da solução .NET

- **Data:** 2026-07-18
- **Ferramenta:** GitHub Copilot Chat
- **Modelo:** GPT-5.6 Terra
- **Etapa:** Implementação — Bootstrap
- **Objetivo:** Planejar e criar somente a estrutura inicial da solução .NET 8 e do projeto ASP.NET Core Web API, sem implementar funcionalidades ou testes de contrato.

### Prompt completo

```text
A partir da especificação e das decisões já aprovadas no repositório, Implementação do TaskFlow

Antes de realizar qualquer alteração, leia os arquivos para formar o contexto:

- openapi.yaml
- docs/decisoes.md
- ai/skills.md
- README.md

Considere o arquivo openapi.yaml e o docs/decisoes.md como fontes de verdade para a implementação.

Nessa etapa deve criar somente o bootstrap da solução .NET. Não implemente entidades, persistência, DTOs, endpoints do domínio, validações, regras de negócio ou testes de contrato.

A execução deve ocorrer nessa ordem:

FASE 1 — PLANEJAMENTO

Antes de modificar qualquer arquivo:

1. Leia os artefatos indicados.
2. Apresente uma lista ordenada das tarefas necessárias para criar o bootstrap.
3. Informe os comandos que pretende executar.
4. Informe os arquivos e diretórios que pretende criar, alterar ou remover.
5. Identifique ambiguidades, suposições ou decisões que não estejam registradas.
6. Não modifique nenhum arquivo nesta fase.
7. Aguarde minha confirmação explícita antes de iniciar a execução.

FASE 2 — EXECUÇÃO

Somente após minha confirmação, execute as tarefas aprovadas na ordem apresentada.

Crie a seguinte estrutura:

TaskFlow.sln

src/
  TaskFlow.Api/
    TaskFlow.Api.csproj

Requisitos:

1. Utilizar .NET 8.
2. Criar o projeto TaskFlow.Api como ASP.NET Core Web API
3. Adicionar o projeto TaskFlow.Api à solution.
4. Remover arquivos, modelos e endpoints demonstrativos gerados automaticamente pelo template, como WeatherForecast.
5. Manter apenas a configuração mínima necessária para:
   - registrar Controllers;
   - mapear Controllers;
   - executar a aplicação;
   - compilar a solution.
6. Manter o Swagger/OpenAPI gerado pelo template somente se ele fizer parte da configuração padrão mínima da aplicação. Não alterar o contrato openapi.yaml já aprovado.
7. Não adicionar EF Core, SQLite, WebApplicationFactory, xUnit ou outras dependências nesta etapa.
8. Não criar o diretório tests nem qualquer projeto de testes.
9. Não criar arquitetura com múltiplos projetos, CQRS, MediatR, repositórios genéricos ou abstrações ainda não justificadas.
10. Não implementar Controllers do domínio, entidades, Services, UseCases, DTOs ou regras de negócio.
11. Não alterar:
    - openapi.yaml;
    - docs/decisoes.md;
    - ai/prompts.md;
    - ai/revisoes.md;
    - ai/skills.md;
    - README.md.
12. Não criar commits nem executar git push.
13. Caso encontre uma ambiguidade, não invente uma decisão técnica. Interrompa e apresente a dúvida antes de modificar o repositório.

Após criar a estrutura:

1. Execute dotnet restore.
2. Execute dotnet build.
3. Apresente:
   - as tarefas concluídas;
   - os arquivos criados, alterados e removidos;
   - os comandos executados;
   - o resultado do restore;
   - o resultado do build;
   - qualquer diferença entre o plano aprovado e a execução realizada;
   - qualquer decisão ou suposição feita durante a geração.
```

### Resultado obtido

O GitHub Copilot Chat criou a solution `TaskFlow.sln`, o projeto `TaskFlow.Api` em .NET 8 e adicionou o projeto à solution.

A primeira saída gerada compilou com sucesso, mas não atendeu completamente às restrições do prompt. O template padrão foi mantido como Minimal API, incluindo o endpoint `/weatherforecast`, o modelo `WeatherForecast` e o arquivo demonstrativo `TaskFlow.Api.http`.

A versão inicial produzida pela IA foi preservada no commit `aff22a4` antes das correções humanas.

Durante a revisão, foram realizadas manualmente as seguintes correções:

- substituição da configuração de Minimal API pelo registro de Controllers com `AddControllers()`;
- inclusão do mapeamento de Controllers com `MapControllers()`;
- remoção do endpoint e do modelo `WeatherForecast`;
- remoção do arquivo demonstrativo `TaskFlow.Api.http`;
- Remoção do pacote `Microsoft.AspNetCore.OpenApi`, que deixou de ser utilizado após a retirada de `WithOpenApi()`.

Após as correções, foram executados `dotnet restore TaskFlow.sln` e `dotnet build TaskFlow.sln`. A compilação foi concluída com zero avisos e zero erros.

### Arquivos relacionados

- `TaskFlow.sln`;
- `src/TaskFlow.Api/`.

### Limitações

Esta etapa deve criar somente uma base executável e compilável para a API. Nenhuma funcionalidade do domínio ou infraestrutura de testes deve ser implementada antes da revisão humana do bootstrap.

---

## PROMPT-006 — Planejamento e criação da base de domínio e persistência

- **Data:** 2026-07-18
- **Ferramenta:** GitHub Copilot Chat
- **Modelo:** GPT-5.6 Terra
- **Etapa:** Implementação — Domínio e persistência
- **Objetivo:** Criar a base do domínio e configurar a persistência com EF Core e SQLite, sem implementar endpoints.

### Prompt completo

```text
Implemente a base de domínio e persistência do TaskFlow a partir dos artefatos já aprovados no repositório.

Antes de realizar qualquer alteração, leia integralmente:

- openapi.yaml
- docs/decisoes.md
- ai/skills.md
- src/TaskFlow.Api/Program.cs
- src/TaskFlow.Api/TaskFlow.Api.csproj
- src/TaskFlow.Api/appsettings.json
- src/TaskFlow.Api/appsettings.Development.json

Considere openapi.yaml e docs/decisoes.md como fontes de verdade. Não crie requisitos ou regras que não estejam registrados nesses arquivos.

A execução deve ocorrer obrigatoriamente em duas fases.

FASE 1 — PLANEJAMENTO

Antes de modificar qualquer arquivo:

1. Apresente uma lista ordenada das tarefas necessárias.
2. Informe todos os comandos que pretende executar.
3. Informe todos os arquivos e diretórios que pretende criar, alterar ou remover.
4. Explique como serão representados:
   - Project;
   - TaskItem;
   - status de projeto;
   - status de tarefa;
   - prioridade da tarefa;
   - relacionamento entre projeto e tarefas;
   - unicidade de nomes e títulos sem diferenciação entre maiúsculas e minúsculas.
5. Informe como a migration inicial será criada.
6. Identifique ambiguidades, suposições ou decisões que não estejam registradas.
7. Não modifique nenhum arquivo nesta fase.
8. Aguarde minha confirmação explícita antes de iniciar a execução.

FASE 2 — EXECUÇÃO

Somente após minha confirmação explícita, implemente o plano aprovado.

ESCOPO

1. Adicionar EF Core com SQLite ao projeto TaskFlow.Api.
2. Manter todos os pacotes do EF Core na mesma versão compatível com .NET 8.
3. Criar um manifesto local de ferramentas .NET, caso ele ainda não exista.
4. Registrar dotnet-ef como ferramenta local, utilizando a mesma versão dos pacotes do EF Core.
5. Criar a entidade Project com os campos definidos no contrato.
6. Criar a entidade TaskItem com todos os campos definidos no contrato.
7. Utilizar o nome TaskItem para evitar conflito com System.Threading.Tasks.Task.
8. Criar os enums necessários para:
   - status de projeto;
   - status de tarefa;
   - prioridade de tarefa.
9. Criar TaskFlowDbContext.
10. Criar configurações de persistência separadas para Project e TaskItem utilizando IEntityTypeConfiguration.
11. Configurar o relacionamento de um projeto para muitas tarefas.
12. Configurar os tamanhos máximos, obrigatoriedade e demais restrições exatamente como definidos no contrato.
13. Preparar a persistência para garantir:
   - nome de projeto único globalmente;
   - comparação de nome sem diferenciação entre maiúsculas e minúsculas;
   - título de tarefa único dentro do mesmo projeto;
   - comparação de título sem diferenciação entre maiúsculas e minúsculas.
14. Para suportar a unicidade, podem ser utilizados campos internos normalizados, desde que eles não façam parte do contrato público da API.
15. Registrar TaskFlowDbContext no container de dependências.
16. Configurar a connection string do SQLite nos arquivos de configuração apropriados.
17. Adicionar ao .gitignore os arquivos locais do SQLite, caso ainda não estejam ignorados.
18. Criar a migration inicial com o nome InitialCreate.
19. Executar restore e build ao final.

RESTRIÇÕES

1. Não implementar ProjectsController ou TasksController.
2. Não implementar Services ou UseCases.
3. Não implementar DTOs ou contratos HTTP.
4. Não implementar endpoints.
5. Não implementar tratamento global de erros nesta etapa.
6. Não adicionar xUnit, WebApplicationFactory ou projetos de testes.
7. Não criar repositório genérico.
8. Não adicionar CQRS, MediatR ou novas camadas de projeto.
9. Não utilizar EnsureCreated.
10. Não executar Database.Migrate ou MigrateAsync no Program.cs.
11. Não aplicar migrations automaticamente durante a inicialização da API.
12. Não executar database update nesta etapa.
13. Não criar nem versionar o arquivo físico do banco SQLite.
14. Não alterar:
    - openapi.yaml;
    - docs/decisoes.md;
    - ai/prompts.md;
    - ai/revisoes.md;
    - ai/skills.md;
    - README.md.
15. Não criar commits nem executar git push.
16. Caso encontre uma ambiguidade não resolvida pelos artefatos, interrompa a execução e apresente a dúvida antes de modificar o repositório.
17. Não realize alterações fora do escopo aprovado.

ESTRUTURA ESPERADA

A estrutura pode seguir esta organização, sem criar projetos adicionais:

src/TaskFlow.Api/
├── Data/
│   ├── Configurations/
│   │   ├── ProjectConfiguration.cs
│   │   └── TaskItemConfiguration.cs
│   └── TaskFlowDbContext.cs
├── Domain/
│   ├── Entities/
│   │   ├── Project.cs
│   │   └── TaskItem.cs
│   └── Enums/
├── Migrations/
└── Program.cs

VALIDAÇÃO

Após a execução:

1. Execute dotnet restore TaskFlow.sln.
2. Execute dotnet build TaskFlow.sln.
3. Apresente:
   - tarefas concluídas;
   - arquivos criados, alterados e removidos;
   - comandos executados;
   - pacotes e versões adicionados;
   - conteúdo da migration criada;
   - resultado do restore;
   - resultado do build;
   - diferenças entre o plano aprovado e a execução;
   - decisões ou suposições realizadas.
```

### Resultado obtido

A preencher após o planejamento, a execução e a revisão humana.

### Arquivos relacionados

- `src/TaskFlow.Api/Domain/`;
- `src/TaskFlow.Api/Data/`;
- `src/TaskFlow.Api/Migrations/`;
- `src/TaskFlow.Api/Program.cs`;
- `src/TaskFlow.Api/TaskFlow.Api.csproj`.

### Limitações

Este ciclo cria somente a estrutura do domínio e a persistência. Controllers, Services, DTOs, endpoints e regras de atualização de projetos serão implementados em um ciclo posterior.