# TaskFlow

TaskFlow é uma API REST para gerenciamento de projetos e tarefas, desenvolvida em .NET 8 seguindo uma abordagem de Specification-Driven Development (SDD).

O contrato OpenAPI, as decisões técnicas, os prompts utilizados e as revisões humanas das saídas produzidas por inteligência artificial são mantidos como artefatos versionados no repositório.

## Tecnologias

- .NET 8
- ASP.NET Core Web API baseada em Controllers
- Entity Framework Core 8
- SQLite
- ProblemDetails
- OpenAPI 3.0.3
- Swagger
- xUnit
- WebApplicationFactory
- Microsoft.OpenApi
- NJsonSchema

## Abordagem SDD

O contrato da API é definido no arquivo `openapi.yaml`, utilizado como fonte de verdade para a implementação e para os testes automatizados.

Fluxo adotado:

1. levantamento e análise dos requisitos;
2. definição das decisões técnicas;
3. modelagem do contrato OpenAPI;
4. revisão humana do contrato;
5. implementação orientada pela especificação;
6. revisão das saídas produzidas por IA;
7. validação da aderência por meio de testes automatizados.

O fluxo foi conduzido diretamente por meio dos artefatos versionados no repositório, sem utilização de um framework específico de SDD.

## Decisões adotadas

- utilização do .NET 8 como versão alvo;
- persistência principal com SQLite;
- utilização de UUIDs, representados por `Guid` no código C#;
- uso de migrations do Entity Framework Core;
- serialização de enums em `snake_case`;
- rejeição de propriedades JSON não mapeadas;
- utilização de `application/problem+json` nas respostas de erro;
- condução explícita do fluxo SDD por meio de artefatos versionados;
- utilização do `openapi.yaml` versionado como única fonte de verdade do contrato e como documento apresentado pelo Swagger UI;
- adoção de uma arquitetura em camadas simples e proporcional ao escopo da aplicação;
- geração, persistência e retorno dos campos de data e horário em UTC, deixando a conversão para o fuso local sob responsabilidade do consumidor.

As decisões completas estão registradas em `docs/decisoes.md`.

## Pré-requisitos

### .NET 8

Para executar, compilar e testar o projeto, é necessário possuir o SDK do .NET 8 instalado.

Verifique as versões instaladas:

```powershell
dotnet --list-sdks
```

Caso o SDK do .NET 8 não esteja instalado, no Windows ele pode ser instalado pelo WinGet:

```powershell
winget install --id Microsoft.DotNet.SDK.8 -e
```

Após a instalação, abra um novo terminal e verifique novamente:

```powershell
dotnet --list-sdks
```

A lista deve conter pelo menos uma versão `8.0.x`.

### Entity Framework Core CLI

Para executar as migrations, verifique se a ferramenta `dotnet-ef` está disponível:

```powershell
dotnet ef --version
```

Caso o comando não seja reconhecido, instale a versão compatível com o .NET 8:

```powershell
dotnet tool install --global dotnet-ef --version "8.*"
```

Após a instalação, abra um novo terminal e confirme:

```powershell
dotnet ef --version
```

## Clonando o repositório

```powershell
git clone https://github.com/GuilhermeAntonio/TaskFlow.git
cd TaskFlow
```

## Restaurando as dependências

```powershell
dotnet restore TaskFlow.sln
```

## Configuração do banco de dados

Para criar ou atualizar o banco de dados:

```powershell
dotnet ef database update --project src/TaskFlow.Api --startup-project src/TaskFlow.Api
```

A aplicação utiliza SQLite por meio da seguinte string de conexão:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=taskflow.db"
  }
}
```

As migrations estão localizadas em:

```text
src/TaskFlow.Api/Data/Migrations
```

Após a execução das migrations, o arquivo `taskflow.db` será criado para utilização pela aplicação.

Os testes automatizados não utilizam esse banco. Cada teste cria um banco SQLite temporário e isolado.

## Executando a API

Para iniciar a aplicação:

```powershell
dotnet run --project src/TaskFlow.Api
```

No perfil de desenvolvimento, a API pode ser acessada em:

```text
http://localhost:5034
https://localhost:7148
```

O Swagger estará disponível em:

```text
http://localhost:5034/swagger
https://localhost:7148/swagger
```

O Swagger UI exibe diretamente o contrato definido no arquivo `openapi.yaml` versionado na raiz do repositório. Os testes de contrato também carregam esse mesmo arquivo para validar as respostas da API, evitando a manutenção de especificações paralelas.

O Swagger UI é habilitado somente quando a aplicação é executada no ambiente `Development`.

## Principais endpoints

### Projetos

```text
POST   /projetos
GET    /projetos
GET    /projetos/{id}
PATCH  /projetos/{id}
```

### Tarefas

```text
POST    /projetos/{id}/tarefas
GET     /projetos/{id}/tarefas
PATCH   /tarefas/{id}
DELETE  /tarefas/{id}
```

O contrato completo, incluindo requests, responses, schemas, filtros e exemplos, está disponível em `openapi.yaml`.

## Principais regras de negócio

### Projetos

- o nome do projeto deve ser único;
- a comparação de nomes desconsidera diferenças entre letras maiúsculas e minúsculas;
- espaços no início e no final são removidos;
- um projeto não pode ser arquivado enquanto possuir tarefas com status `in_progress`;
- projetos arquivados podem ser reativados;
- projetos arquivados não aceitam a criação de novas tarefas.

### Tarefas

- o título da tarefa deve ser único dentro do projeto;
- a prioridade é obrigatória na criação;
- as transições de status permitidas são:

```text
pending → in_progress → done
```

- não é permitido avançar diretamente de `pending` para `done`;
- não é permitido retroceder de `in_progress` para `pending`;
- o campo `completedAt` é preenchido automaticamente quando a tarefa é concluída;
- tarefas concluídas não podem ser modificadas;
- uma tarefa concluída aceita apenas o envio isolado e idempotente de `{ "status": "done" }`;
- somente tarefas com status `pending` podem ser excluídas.

## Respostas de erro

As respostas de erro utilizam o formato ProblemDetails e o tipo de conteúdo:

```text
application/problem+json
```

Os principais status utilizados são:

- `400 Bad Request`: dados de entrada inválidos;
- `404 Not Found`: projeto ou tarefa inexistente;
- `409 Conflict`: conflito de unicidade;
- `422 Unprocessable Entity`: violação de regra de negócio.

Além dos campos padrão de ProblemDetails, as respostas incluem o campo `code`, utilizado para identificar o erro de forma estável.

Exemplo:

```json
{
  "type": "about:blank",
  "title": "Projeto não encontrado",
  "status": 404,
  "detail": "O projeto informado não existe.",
  "instance": "/projetos/d290f1ee-6c54-4b01-90e6-d701748f0851",
  "code": "project_not_found"
}
```

## Executando os testes

Primeiro compile a solução:

```powershell
dotnet build TaskFlow.sln
```

Depois execute a suíte de testes:

```powershell
dotnet test tests/TaskFlow.ContractTests --no-build
```

Resultado validado durante o desenvolvimento:

```text
Com falha: 0
Aprovado: 29
Ignorado: 0
Total: 29
```

## Estratégia de testes

A suíte utiliza:

- xUnit;
- WebApplicationFactory;
- requisições HTTP reais contra a aplicação hospedada no ambiente de testes;
- SQLite temporário e isolado para cada teste;
- carregamento dinâmico do arquivo `openapi.yaml`;
- validação de status HTTP;
- validação de tipo de conteúdo;
- validação dos schemas das respostas com NJsonSchema.

Os dados utilizados nos cenários são definidos nos próprios testes.

A aplicação testada não é substituída por mocks: controllers, services, Entity Framework Core e SQLite são executados durante os testes.

A cobertura inclui:

- criação válida de projetos e tarefas;
- recursos inexistentes com retorno `404`;
- regras de negócio com retorno `422`;
- conflitos de unicidade com retorno `409`;
- validações de entrada com retorno `400`;
- operações válidas com retornos `200`, `201` e `204`;
- aderência das respostas ao contrato OpenAPI.

## Uso de inteligência artificial

As ferramentas de IA foram utilizadas como apoio ao processo de especificação, implementação e testes.

Os principais usos incluíram:

- estruturação e refinamento de prompts;
- geração da estrutura documental inicial;
- modelagem e refinamento do contrato OpenAPI;
- geração inicial de código;
- geração inicial da suíte de testes;
- sugestões inline para complementação de código e testes.

As saídas produzidas por IA não foram consideradas automaticamente aprovadas.

O fluxo adotado foi:

```text
geração pela IA
→ preservação da saída original
→ revisão humana
→ correção ou rejeição
→ compilação
→ execução dos testes
→ aprovação humana
```

A rastreabilidade está registrada nos seguintes arquivos:

- `ai/prompts.md`: prompts utilizados;
- `ai/revisoes.md`: análises, correções e rejeições humanas;
- `ai/skills.md`: habilidades delegadas às ferramentas de IA;
- `docs/decisoes.md`: decisões técnicas aprovadas.

## Contrato OpenAPI

O arquivo `openapi.yaml` é a fonte de verdade do contrato da API e define:

- endpoints;
- parâmetros;
- corpos das requisições;
- schemas das respostas;
- códigos HTTP;
- formatos de erro;
- regras de validação representáveis no contrato.

Os testes de contrato carregam esse arquivo diretamente do repositório e validam as respostas produzidas pela aplicação.
