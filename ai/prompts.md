
# Registro de Prompts

## PROMPT-001 — Estrutura documental inicial

- **Data:** 2026-07-17
- **Ferramenta:** GitHub Copilot Chat
- **Modelo:** GPT-5 mini
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
- **Modelo:** GPT-5 mini
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
- **Modelo:** GPT-5 mini
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
- **Modelo:** GPT-5 mini
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

O GitHub Copilot Chat criou a base de domínio e persistência do TaskFlow com:

- entidades `Project` e `TaskItem`;
- enums para status de projeto, status de tarefa e prioridade;
- `TaskFlowDbContext`;
- configurações do EF Core separadas por entidade;
- relacionamento de um projeto para muitas tarefas;
- campos normalizados e índices únicos;
- persistência com EF Core e SQLite;
- manifesto local com `dotnet-ef`;
- migration inicial.

A primeira saída foi preservada no commit `7bfd217`.

Durante a revisão humana, foram identificados e corrigidos os seguintes pontos:

- remoção do limite de 4.000 caracteres adicionado à descrição do projeto sem previsão no contrato;
- renomeação de `TaskStatus` para `TaskItemStatus`, evitando conflito com o tipo nativo do .NET;
- adequação dos enums às convenções de nomenclatura do C#;
- correção da nulabilidade da navegação obrigatória entre `TaskItem` e `Project`;
- aplicação das regras de persistência somente para entidades adicionadas ou modificadas;
- utilização de um único instante UTC por operação de persistência;
- remoção do fallback silencioso da connection string;
- remoção da duplicação da connection string no arquivo de desenvolvimento;
- remoção do arquivo SQL redundante;
- regeneração da migration `InitialCreate`.

A migration não foi aplicada ao banco e nenhum arquivo físico do SQLite foi criado.

Após as correções, a solution foi compilada com zero avisos e zero erros.

### Arquivos relacionados

- `src/TaskFlow.Api/Domain/`;
- `src/TaskFlow.Api/Data/`;
- `src/TaskFlow.Api/Migrations/`;
- `src/TaskFlow.Api/Program.cs`;
- `src/TaskFlow.Api/TaskFlow.Api.csproj`.

### Limitações

Este ciclo cria somente a estrutura do domínio e a persistência. Controllers, Services, DTOs, endpoints e regras de atualização de projetos serão implementados em um ciclo posterior.

---

## PROMPT-007 — Planejamento e implementação da API de projetos

- **Data:** 2026-07-18
- **Ferramenta:** GitHub Copilot Chat
- **Modelo:** GPT-5.6 Terra
- **Etapa:** Implementação — API de projetos
- **Objetivo:** Implementar os contratos HTTP, regras de negócio, Service, Controller e tratamento de erros dos endpoints de projetos.

### Prompt completo

```text
Implemente a API de projetos do TaskFlow com base nos artefatos já aprovados no repositório.

Antes de realizar qualquer alteração, leia integralmente:

- openapi.yaml
- docs/decisoes.md
- ai/skills.md
- src/TaskFlow.Api/Program.cs
- src/TaskFlow.Api/Data/TaskFlowDbContext.cs
- src/TaskFlow.Api/Data/Configurations/ProjectConfiguration.cs
- src/TaskFlow.Api/Data/Configurations/TaskItemConfiguration.cs
- src/TaskFlow.Api/Domain/Entities/Project.cs
- src/TaskFlow.Api/Domain/Entities/TaskItem.cs
- src/TaskFlow.Api/Domain/Enums/ProjectStatus.cs
- src/TaskFlow.Api/Domain/Enums/TaskItemStatus.cs
- src/TaskFlow.Api/Domain/Enums/TaskPriority.cs

Considere openapi.yaml e docs/decisoes.md como as únicas fontes normativas da implementação.

As listas presentes neste prompt servem apenas como checklist operacional e delimitador de escopo. Elas não alteram, complementam ou substituem os requisitos registrados nos artefatos.

Caso uma instrução deste prompt pareça divergir dos artefatos, interrompa a execução e apresente o conflito antes de modificar qualquer arquivo.

Não crie regras, limites, campos ou comportamentos que não estejam registrados nesses arquivos.

A execução deve ocorrer obrigatoriamente em duas fases.

FASE 1 — PLANEJAMENTO

Antes de modificar qualquer arquivo:

1. Apresente uma lista ordenada das tarefas necessárias.
2. Informe todos os arquivos e diretórios que pretende criar, alterar ou remover.
3. Informe todos os comandos que pretende executar.
4. Explique como serão implementados:
   - DTOs de criação, atualização e resposta;
   - diferenciação entre campo omitido e campo enviado como null no PATCH;
   - validação do corpo vazio no PATCH;
   - rejeição de propriedades não previstas nos DTOs;
   - serialização dos enums conforme o contrato;
   - ProjectService;
   - ProjectsController;
   - ValidationProblemDetails;
   - ProblemDetails;
   - códigos estáveis de erro;
   - tratamento global de exceções;
   - unicidade do nome do projeto;
   - regra de arquivamento;
   - filtro por status.
5. Informe como serão tratados conflitos de unicidade causados por concorrência no momento do SaveChanges.
6. Identifique ambiguidades, suposições ou decisões não registradas.
7. Não modifique nenhum arquivo nesta fase.
8. Aguarde minha confirmação explícita antes de iniciar a execução.

FASE 2 — EXECUÇÃO

Somente após minha confirmação explícita, implemente o plano aprovado.

ENDPOINTS

Implemente exclusivamente:

- POST /projetos
- GET /projetos
- GET /projetos/{id}
- PATCH /projetos/{id}

CONTRATOS HTTP

1. Criar DTOs separados para:
   - criação de projeto;
   - atualização parcial de projeto;
   - resposta de projeto.
2. Não utilizar as entidades do EF Core diretamente como contratos HTTP.
3. No POST, permitir somente os campos definidos no contrato de criação.
4. Rejeitar campos controlados pelo servidor, como id, status e createdAt.
5. No PATCH, permitir somente name, description e status.
6. Rejeitar id, createdAt e outras propriedades não previstas.
7. Rejeitar PATCH com corpo vazio.
8. Diferenciar:
   - campo description omitido: manter valor atual;
   - campo description enviado como null: remover a descrição.
9. O nome deve ser normalizado removendo espaços do início e do final antes da persistência.
10. O nome deve possuir pelo menos um caractere diferente de espaço.
11. Não adicionar limite para description caso ele não esteja definido no contrato.

SERIALIZAÇÃO

1. Serializar enums como strings no formato definido no contrato.
2. Utilizar snake_case em minúsculas quando necessário.
3. Garantir que ProjectStatus.Active seja exposto como active.
4. Garantir que ProjectStatus.Archived seja exposto como archived.
5. Rejeitar valores inválidos de enum com status 400.
6. Rejeitar propriedades JSON não mapeadas nos DTOs.

SERVICE

1. Criar IProjectService e ProjectService.
2. O ProjectService deve acessar diretamente TaskFlowDbContext.
3. Não criar repositório genérico.
4. Implementar:
   - criação;
   - listagem;
   - consulta por identificador;
   - atualização parcial.
5. A listagem deve aceitar filtro opcional por status.
6. Os filtros devem ser combináveis quando novos filtros forem adicionados futuramente, sem introduzir paginação nesta versão.
7. Não garantir ordenação na listagem.
8. Em consultas somente de leitura, utilizar AsNoTracking quando apropriado.
9. Propagar CancellationToken nas operações assíncronas.

REGRAS CRÍTICAS A VALIDAR

As regras abaixo são um checklist operacional extraído do openapi.yaml e do docs/decisoes.md. Esta seção não substitui esses artefatos.

Em caso de divergência, interrompa a execução e apresente o conflito. Não escolha uma das versões por conta própria.

1. Implementar exatamente as regras de criação, consulta e atualização de projetos definidas nos artefatos.
2. Garantir a unicidade global do nome do projeto com normalização de espaços e comparação sem diferenciação entre maiúsculas e minúsculas.
3. Retornar 409 com code project_name_conflict em conflitos de nome, inclusive quando detectados pelo índice único durante SaveChanges.
4. Retornar 404 com code project_not_found quando o projeto não existir.
5. Impedir o arquivamento quando houver tarefa InProgress, retornando 422 com code project_has_in_progress_tasks.
6. Permitir a reativação de Archived para Active.
7. Tratar a atualização para o mesmo status como idempotente.
8. Permitir alterações de nome e descrição em projetos arquivados.
9. Manter id, status inicial e createdAt sob controle do servidor.
10. Não implementar nenhuma regra de tarefa além da consulta necessária para validar o arquivamento.

ERROS

1. Utilizar ValidationProblemDetails para erros de entrada com status 400.
2. Incluir code validation_error nas respostas de validação.
3. Utilizar ProblemDetails para:
   - 404 project_not_found;
   - 409 project_name_conflict;
   - 422 project_has_in_progress_tasks;
   - erros inesperados.
4. Criar tratamento global de exceções utilizando os recursos nativos do ASP.NET Core e .NET 8.
5. Não retornar stack trace, nomes de classes internas ou informações sensíveis.
6. Preencher status, title, detail, instance e code conforme o contrato.
7. O campo code deve ser adicionado em ProblemDetails.Extensions.
8. Manter as respostas aderentes aos schemas e exemplos definidos no openapi.yaml.
9. Não utilizar exceções genéricas para representar regras de negócio conhecidas.

CONTROLLER

1. Criar ProjectsController baseado em ControllerBase.
2. Utilizar ApiController.
3. Usar a rota /projetos.
4. O Controller não deve acessar TaskFlowDbContext diretamente.
5. O Controller não deve conter regras de negócio.
6. POST /projetos deve retornar 201.
7. POST /projetos deve definir o header Location como /projetos/{id}.
8. GET /projetos deve retornar 200 e um array, inclusive quando estiver vazio.
9. GET /projetos/{id} deve retornar 200 quando encontrado.
10. PATCH /projetos/{id} deve retornar 200 com o projeto atualizado.

REGISTRO DE DEPENDÊNCIAS

1. Registrar IProjectService e ProjectService no container.
2. Registrar ProblemDetails.
3. Registrar o handler global de exceções.
4. Configurar a resposta automática de validação para incluir code validation_error.
5. Configurar o System.Text.Json para rejeitar propriedades não mapeadas.
6. Configurar a serialização de enums conforme o contrato.

ESTRUTURA 

src/TaskFlow.Api/
├── Controllers/
│   └── ProjectsController.cs
├── Contracts/
│   └── Projects/
│       ├── CreateProjectRequest.cs
│       ├── UpdateProjectRequest.cs
│       └── ProjectResponse.cs
├── Errors/
│   ├── Exceptions/
│   └── GlobalExceptionHandler.cs
├── Services/
│   └── Projects/
│       ├── IProjectService.cs
│       └── ProjectService.cs
└── Program.cs

A estrutura pode ser ajustada somente quando houver justificativa objetiva, sem criar projetos ou abstrações adicionais.

RESTRIÇÕES

1. Não implementar endpoints de tarefas.
2. Não implementar TasksController.
3. Não implementar TaskService.
4. Não adicionar xUnit, WebApplicationFactory ou projetos de testes.
5. Não aplicar migrations automaticamente.
6. Não executar database update.
7. Não criar nem versionar o arquivo físico do SQLite.
8. Não criar repositório genérico.
9. Não adicionar CQRS ou MediatR.
10. Não dividir a solution em múltiplos projetos.
11. Não alterar:
    - openapi.yaml;
    - docs/decisoes.md;
    - ai/prompts.md;
    - ai/revisoes.md;
    - ai/skills.md;
    - README.md.
12. Não criar commits.
13. Não executar git push.
14. Não alterar a migration atual, salvo se uma mudança de persistência realmente necessária for identificada e previamente apresentada.
15. Caso encontre uma ambiguidade, interrompa a execução e apresente a dúvida antes de modificar o repositório.
16. Não realizar alterações fora do escopo aprovado.

VALIDAÇÃO

Após a implementação:

1. Execute dotnet restore TaskFlow.sln.
2. Execute dotnet build TaskFlow.sln.
3. Não execute database update.
4. Apresente:
   - tarefas concluídas;
   - arquivos criados, alterados e removidos;
   - comandos executados;
   - validações implementadas;
   - regras de negócio implementadas;
   - códigos de erro implementados;
   - resultado do restore;
   - resultado do build;
   - diferenças entre o plano aprovado e a execução;
   - decisões ou suposições realizadas.
```

### Resultado obtido

### Arquivos relacionados

- `src/TaskFlow.Api/Controllers/`;
- `src/TaskFlow.Api/Contracts/Projects/`;
- `src/TaskFlow.Api/Services/Projects/`;
- `src/TaskFlow.Api/Errors/`;
- `src/TaskFlow.Api/Program.cs`.

### Limitações

Este ciclo implementa exclusivamente a API de projetos. A API de tarefas e os testes de contrato serão implementados em ciclos posteriores.

---

### PROMPT-008 — Implementação da API de tarefas

- **Data:** 2026-07-19
- **Ferramenta:** GitHub Copilot Chat
- **Modelo:** GPT-5 mini
- **Objetivo:** Implementar a API de tarefas com base nos artefatos SDD existentes.
- **Resultado esperado:** Implementação inicial da API de tarefas para posterior revisão humana.
- **Status:** Executado

#### Prompt utilizado

```text
Atue como um engenheiro de software responsável pela implementação da API de tarefas do projeto TaskFlow.

Antes de alterar qualquer arquivo, leia integralmente:

- openapi.yaml
- docs/decisoes.md
- ai/skills.md
- a implementação atual em src/TaskFlow.Api

Considere o openapi.yaml e o docs/decisoes.md como fontes de verdade. Não replique, simplifique ou altere as regras definidas nesses artefatos para adequá-las à implementação.

Antes de alterar qualquer arquivo, verifique quais operações do openapi.yaml já estão implementadas e quais ainda estão ausentes, e desenvolva as novas seguindo os padrões adotados nas que já foram aplicadas.

Considere como escopo deste ciclo somente os quatro endpoints de tarefas explicitamente indicados abaixo:

- POST /projetos/{id}/tarefas
- GET /projetos/{id}/tarefas
- PATCH /tarefas/{id}
- DELETE /tarefas/{id}

Identifique no openapi.yaml e no docs/decisoes.md todas as regras, contratos, respostas HTTP e códigos de erro aplicáveis a esses endpoints.

Não crie outros endpoints e não reimplemente os endpoints de projetos já existentes.

Na API de projetos, faça somente ajustes estritamente necessários para manter a regra de arquivamento relacionada às tarefas em andamento.

Utilize a implementação atual da API de projetos como referência para:

- organização dos contratos, Controllers e Services;
- validação das requisições;
- serialização JSON;
- tratamento com ProblemDetails;
- acesso ao TaskFlowDbContext;
- códigos estáveis de erro;
- tratamento de conflitos de concorrência;
- propagação de CancellationToken.

Mantenha a arquitetura atual. Não adicione CQRS, MediatR, AutoMapper, repositórios genéricos ou novas camadas.

Não implemente testes automatizados neste ciclo. Eles serão tratados na Etapa 3.

Não altere openapi.yaml ou docs/decisoes.md.

A entidade TaskItem, seus enums, sua configuração de persistência e sua migration já existem. Não crie uma nova migration, exceto se identificar uma divergência real entre o modelo atual e os artefatos. Nesse caso, não faça a alteração automaticamente: apenas informe a divergência ao final.

Antes de implementar, apresente de forma resumida:

1. os endpoints de tarefas identificados;
2. as principais regras de negócio identificadas nos artefatos;
3. os arquivos que pretende criar ou alterar;
4. eventuais ambiguidades ou divergências encontradas.

Depois dessa análise, implemente a API de tarefas seguindo os artefatos e o padrão existente no projeto.

Ao terminar:

1. execute dotnet restore TaskFlow.sln;
2. execute dotnet build TaskFlow.sln;
3. corrija somente erros de compilação causados pela implementação;
4. não crie ou execute testes automatizados;
5. não execute comandos Git;
6. apresente os arquivos criados e alterados;
7. informe as decisões técnicas tomadas;
8. informe riscos, dúvidas ou divergências encontradas;
9. informe explicitamente se nenhuma migration foi criada.
```

#### Resultado obtido

O agente implementou a estrutura inicial da API de tarefas, criando:

- contratos de criação, atualização e resposta;
- `TasksController`;
- interface `ITaskService`;
- implementação `TaskService`;
- registro do serviço no `Program.cs`.

A geração foi restaurada e compilada com sucesso:

```text
dotnet restore TaskFlow.sln
dotnet build TaskFlow.sln
```

Resultado:

```text
0 Aviso(s)
0 Erro(s)
```

Não foram criadas migrations ou estruturas de testes automatizados.

#### Interação complementar

Durante a execução, o agente solicitou confirmação sobre a regra de exclusão de tarefas.

**Pergunta do agente:**

```text
Solicito confirmação rápida sobre o DELETE: qual a regra exata que gera task_cannot_be_deleted?

Opção A: impedir exclusão se status == in_progress, mas permitir pending e done.
Opção B: impedir exclusão se status == done.
Opção C: impedir exclusão se status == in_progress ou done.
Opção D: permitir exclusão sempre.
```

**Resposta fornecida:**

```text
Opção C.

Somente tarefas com status pending podem ser excluídas.

Essa regra já está definida nos artefatos do projeto. Prossiga com a implementação mantendo openapi.yaml e docs/decisoes.md como fontes de verdade.
```

---

### PROMPT-009 — Testes de integração e aderência ao contrato

- **Data:** 2026-07-19
- **Ferramenta:** GitHub Copilot Chat
- **Modelo:** GPT-5 mini
- **Objetivo:** Implementar os testes automatizados e comprovar a aderência da API ao contrato OpenAPI.
- **Resultado esperado:** Projeto de testes de integração e contrato para posterior revisão humana.
- **Status:** Executado com falha de compilação

#### Prompt utilizado

```text
Atue como um engenheiro de software responsável pela validação da aderência do projeto TaskFlow.

Antes de alterar qualquer arquivo, leia integralmente:

- openapi.yaml
- docs/decisoes.md
- ai/skills.md
- a implementação existente em src/TaskFlow.Api
- a solução TaskFlow.sln

Considere openapi.yaml e docs/decisoes.md como fontes de verdade. Os testes devem comprovar que a implementação segue esses artefatos, e não adaptar os artefatos ao comportamento atual do código.

Crie o projeto:

tests/TaskFlow.ContractTests

Adicione-o à solução TaskFlow.sln.

Utilize:

- .NET 8;
- xUnit;
- WebApplicationFactory;
- System.Net.Http.Json;
- Microsoft.OpenApi;
- NJsonSchema.

Implemente testes de integração executando a aplicação em memória por meio de WebApplicationFactory.

Derive do openapi.yaml e do docs/decisoes.md uma matriz de cenários para todos os endpoints atualmente especificados e implementados.

Os testes devem cobrir, no mínimo:

- criação válida de recursos;
- recursos inexistentes com retorno 404;
- regras de negócio com retorno 422;
- validações de entrada com retorno 400;
- conflitos de unicidade com retorno 409;
- exclusões válidas com retorno 204;
- filtros e atualizações parciais;
- transições e comportamentos idempotentes definidos nos artefatos;
- respostas ProblemDetails e ValidationProblemDetails;
- códigos estáveis presentes na extensão code.

Além das asserções funcionais, valide os corpos JSON das respostas contra o schema correspondente à operação e ao status HTTP definidos no openapi.yaml.

Não duplique manualmente os schemas OpenAPI dentro dos testes. Leia e resolva o documento versionado no repositório utilizando Microsoft.OpenApi e NJsonSchema.

A validação contratual deve considerar:

- operação e path executados;
- status HTTP retornado;
- Content-Type;
- schema da resposta correspondente;
- propriedades obrigatórias;
- tipos e formatos;
- additionalProperties;
- enums;
- campos ProblemDetails e o código estável de erro.

Utilize persistência SQLite isolada para os testes.

Os testes não podem utilizar, modificar ou depender do arquivo local taskflow.db. Cada execução deve começar em estado conhecido e não depender da ordem dos testes.

Mantenha os testes determinísticos, independentes e legíveis. Organize-os de forma simples, separando:

- fixture da aplicação;
- infraestrutura de validação do OpenAPI;
- testes de projetos;
- testes de tarefas;
- helpers estritamente necessários.

Não adicione mocks das regras centrais, repositórios, Docker, serviços externos, GitHub Actions ou novas camadas.

Não altere openapi.yaml ou docs/decisoes.md.

Não altere o comportamento da API apenas para fazer os testes passarem. Mudanças no código de produção são permitidas somente quando estritamente necessárias para viabilizar WebApplicationFactory ou o isolamento da persistência, como a exposição do tipo Program. Toda alteração desse tipo deve ser informada ao final.

Adicione comentários apenas quando forem necessários para explicar uma decisão não óbvia, uma limitação técnica, a estratégia de isolamento do banco, a resolução do schema OpenAPI ou um comportamento relevante do teste.

Não adicione comentários que apenas repitam o que o código já expressa claramente.

Preserve comentários úteis gerados na implementação inicial para que possam ser avaliados durante a revisão humana.

Não crie endpoints adicionais.

Antes de implementar, apresente resumidamente:

1. a matriz de operações e categorias de teste identificadas;
2. os arquivos que pretende criar ou alterar;
3. a estratégia de isolamento do SQLite;
4. a estratégia para localizar e validar o openapi.yaml;
5. ambiguidades ou divergências encontradas.

Depois dessa análise, prossiga com a implementação sem aguardar nova confirmação, exceto caso exista uma ambiguidade real que impeça uma decisão aderente aos artefatos.

Ao concluir:

1. execute dotnet restore TaskFlow.sln;
2. execute dotnet build TaskFlow.sln;
3. execute dotnet test tests/TaskFlow.ContractTests;
4. corrija apenas erros de compilação ou infraestrutura produzidos pela geração;
5. não altere regras de negócio para forçar testes a passar;
6. não execute comandos Git;
7. não altere arquivos da pasta ai;
8. informe todos os arquivos criados e alterados;
9. informe a quantidade de testes executados, aprovados e reprovados;
10. informe riscos, limitações e possíveis divergências encontradas.
```

#### Resultado obtido

O GitHub Copilot criou o projeto `tests/TaskFlow.ContractTests`, adicionou-o à solução e gerou:

- `TaskFlowApiFactory.cs`;
- `OpenApiDocumentFixture.cs`;
- `OpenApiResponseValidator.cs`;
- `ProjectTests.cs`;
- `TaskTests.cs`;
- `TaskFlow.ContractTests.csproj`.

Também adicionou `public partial class Program` ao projeto da API para permitir a inicialização por meio de `WebApplicationFactory`.

A geração inicial conseguiu restaurar os pacotes, mas não compilou.

Resultado:

```text
2 Aviso(s)
2 Erro(s)
```

Erros identificados:

```text
CS1503: não é possível converter de object para string
CS0200: JsonSchema.ActualSchema é somente leitura
```

Os erros foram produzidos pela implementação inicial da validação OpenAPI com NJsonSchema. Como a compilação falhou, nenhum teste automatizado foi executado.

A execução do agente foi interrompida antes da criação de um projeto temporário de inspeção. Nenhuma correção manual foi realizada antes da preservação desta saída.

## PROMPT-010 — Ampliação direcionada da cobertura de testes

- **Data:** 2026-07-20
- **Ferramenta:** GitHub Copilot Chat
- **Modelo:** GPT-5 mini
- **Etapa:** Testes e validação contratual
- **Objetivo:** Ampliar a cobertura da suíte existente com cenários relevantes identificados durante a revisão, sem alterar as regras de negócio ou o contrato da API.

### Resumo fiel do prompt utilizado

```text
Revise a suíte de testes de integração e contrato existente no projeto TaskFlow e adicione somente os testes necessários para cobrir os cenários ainda não representados.

Utilize como fonte de verdade:

- openapi.yaml;
- docs/decisoes.md;
- implementação atual da API;
- estrutura existente do projeto tests/TaskFlow.ContractTests.

Mantenha o padrão atual dos testes:

- xUnit;
- WebApplicationFactory;
- SQLite temporário e isolado;
- validação das respostas contra o openapi.yaml;
- organização em Arrange, Act e Assert;
- comentários XML nos métodos de teste.

Adicione os seguintes cenários:

1. GET /projetos/{id} com UUID malformado deve retornar 400 Bad Request;
2. PATCH /tarefas/{id} com UUID malformado deve retornar 400 Bad Request;
3. criação de tarefa sem priority deve retornar 400 Bad Request;
4. criação de tarefa com priority inválida deve retornar 400 Bad Request;
5. PATCH contendo somente priority deve retornar 200 OK, alterar a prioridade e preservar os demais campos;
6. envio manual de completedAt no PATCH deve retornar 400 Bad Request;
7. reenvio isolado de status done para uma tarefa concluída deve retornar 200 OK e preservar exatamente o completedAt original;
8. tentativa de alterar uma tarefa concluída de done para in_progress deve retornar 422 Unprocessable Entity.

Para respostas de erro, valide:

- status HTTP;
- Content-Type;
- aderência ao schema OpenAPI;
- código estável presente na extensão code;
- existência dos erros de validação aplicáveis.

Não remova testes existentes.

Não converta os testes existentes para Theory.

Não altere regras de negócio, Controllers, Services, entidades, persistência, openapi.yaml, docs/decisoes.md ou arquivos da pasta ai.

Não crie novos endpoints.

Não execute comandos Git.

Ao concluir:

1. execute dotnet build TaskFlow.sln;
2. execute dotnet test tests/TaskFlow.ContractTests --no-build;
3. informe os arquivos alterados;
4. informe a quantidade de testes aprovados e reprovados;
5. descreva qualquer divergência encontrada.
```

### Resultado inicial da geração

O GitHub Copilot adicionou oito cenários à suíte existente:

- um teste em `ProjectTests.cs`;
- sete testes em `TaskTests.cs`.

Arquivos efetivamente alterados:

- `tests/TaskFlow.ContractTests/ProjectTests.cs`;
- `tests/TaskFlow.ContractTests/TaskTests.cs`.

A compilação foi concluída com sucesso:

```text
0 Aviso(s)
0 Erro(s)
```

A primeira execução da suíte apresentou:

```text
Com falha: 4
Aprovado: 25
Ignorado: 0
Total: 29
```

As falhas estavam relacionadas às expectativas geradas nos testes:

- busca das chaves de validação utilizando exatamente `priority`;
- busca da chave de validação utilizando exatamente `completedAt`;
- expectativa do código `invalid_task_status_transition` para a tentativa de alterar uma tarefa concluída de `done` para `in_progress`.

A saída gerada foi submetida à revisão humana antes de ser considerada aprovada.
