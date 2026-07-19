# Registro de Revisões

Este arquivo documenta as revisões humanas aplicadas aos prompts, às sugestões da IA e aos artefatos gerados, incluindo o resultado de cada revisão e sua respectiva justificativa técnica.

## Histórico

### Revisão da geração inicial dos artefatos SDD

- **ID:** Revisao-20260717-001
- **Data:** 2026-07-17
- **Revisor:** Guilherme Bezerra Antonio
- **Origem:** Artefatos iniciais gerados pelo GitHub Copilot Chat a partir dos prompts registrados em `ai/prompts.md`
- **Decisão:** Corrigida

#### Partes aceitas

- Criação da estrutura inicial de diretórios destinada aos artefatos do processo SDD;
- Criação dos arquivos:
  - `README.md`;
  - `openapi.yaml`;
  - `docs/decisoes.md`;
  - `ai/prompts.md`;
  - `ai/skills.md`;
  - `ai/revisoes.md`;
- Manutenção do projeto sem código-fonte durante a etapa inicial de especificação;
- Utilização do OpenAPI 3.0.3 como versão do contrato;
- Criação do `openapi.yaml` apenas como esqueleto inicial, sem endpoints implementados;
- Registro inicial das ferramentas e habilidades delegadas à IA;
- Separação entre documentação, decisões técnicas e registros relacionados ao uso de inteligência artificial.

#### Partes corrigidas

- A sequência do fluxo SDD apresentada no `README.md` foi ajustada para representar corretamente a condução adotada no projeto;
- A documentação foi alterada para deixar explícito que a especificação e as decisões técnicas devem preceder a implementação;
- O conteúdo de `ai/skills.md` foi revisado para representar apenas habilidades realmente delegadas à IA;
- A seção `servers` foi removida do `openapi.yaml`, pois nenhum ambiente de execução havia sido configurado;
- As referências a endpoints já gerados foram removidas, pois o contrato ainda era somente um esqueleto;
- O registro de autoria fictícia foi removido dos documentos;
- A documentação foi ajustada para diferenciar requisitos obrigatórios do desafio de decisões técnicas adotadas durante o projeto;
- Os prompts utilizados foram registrados em `ai/prompts.md` para preservar a rastreabilidade do processo.

#### Partes rejeitadas

- Suposições não solicitadas sobre utilização de PostgreSQL;
- Referências a ambientes de produção ainda inexistentes;
- Definições prematuras sobre deploy e infraestrutura;
- Menções à criação e execução de migrations antes da definição da estrutura da aplicação;
- Afirmações de que endpoints já haviam sido gerados;
- Registro de autor, equipe ou responsável fictício;
- Informações que não estavam presentes no prompt e ainda não haviam sido aprovadas como decisões do projeto.

#### Ação realizada

Os artefatos iniciais foram revisados manualmente e atualizados para remover pressupostos não sustentados pelo prompt.

A documentação foi mantida como base inicial do processo SDD, mas passou a representar somente o estado real do projeto naquele momento: estrutura documental criada, decisões iniciais registradas e contrato OpenAPI ainda sem endpoints.

#### Arquivos afetados

- `README.md`;
- `ai/prompts.md`;
- `ai/skills.md`;
- `ai/revisoes.md`;
- `docs/decisoes.md`;
- `openapi.yaml`.

#### Notas adicionais e justificativa técnica

As correções garantiram que os artefatos iniciais não declarassem decisões, infraestrutura ou funcionalidades que ainda não haviam sido definidas ou implementadas.

A remoção de referências a PostgreSQL, produção, deploy e migrations evitou que sugestões da IA fossem tratadas como requisitos reais do projeto.

A retirada da seção `servers` do OpenAPI também impediu a declaração prematura de uma URL de execução ainda não configurada.

Essa revisão preservou a separação entre:

- requisitos definidos no prompt;
- decisões técnicas aprovadas pelo responsável humano;
- sugestões produzidas pela IA;
- funcionalidades efetivamente implementadas.

---

### Revisão da primeira versão completa do contrato OpenAPI

- **ID:** Revisao-20260717-002
- **Data:** 2026-07-17
- **Revisor:** Guilherme Bezerra Antonio
- **Origem:** Primeira versão completa do `openapi.yaml`, gerada pelo GitHub Copilot Chat a partir do `PROMPT-003 — Geração da primeira versão completa do contrato OpenAPI`
- **Versão revisada:** Commit `577e69c`
- **Decisão:** Corrigida

#### Partes aceitas

- Utilização do OpenAPI 3.0.3;
- Inclusão dos endpoints obrigatórios definidos no enunciado;
- Separação dos schemas de criação, atualização e resposta;
- Utilização de `additionalProperties: false` nos schemas de requisição;
- Utilização de `minProperties: 1` nos schemas de atualização parcial;
- Definição dos enums de status de projeto, status de tarefa e prioridade;
- Criação de códigos estáveis para identificação dos erros;
- Cobertura dos status HTTP `200`, `201`, `204`, `400`, `404`, `409` e `422`;
- Utilização de `application/problem+json` nas respostas de erro;
- Separação entre `ProblemDetails` e `ValidationProblemDetails`.

#### Partes corrigidas

- Renomeação do componente `ProblemDetailsBase` para `ProblemDetails`, conforme definido durante a modelagem do contrato;
- Inclusão de todos os campos obrigatórios nos schemas de resposta, inclusive campos que podem possuir valor `null`;
- Inclusão de validações para impedir nomes e títulos formados apenas por espaços;
- Remoção do componente reutilizável `ListaVazia`, que não era utilizado por nenhuma operação;
- Inclusão de descrições para os endpoints `PATCH /projetos/{id}` e `PATCH /tarefas/{id}`;
- Ajuste da resposta `422` do endpoint `PATCH /tarefas/{id}` para representar transições de status inválidas e tentativas de alteração de tarefas concluídas;
- Substituição de identificadores genéricos por UUIDs concretos nos exemplos de resposta;
- Ajuste de componentes e exemplos para que representem corretamente a rota e o recurso associados ao erro.

#### Partes rejeitadas

- O componente `components/examples/ListaVazia` foi rejeitado porque não era referenciado por nenhuma operação e duplicava os exemplos de lista vazia já definidos diretamente nos endpoints.

#### Ação realizada

A primeira versão produzida pela IA foi preservada no commit `577e69c`, permitindo a comparação com as versões posteriores.

Após a revisão humana, os problemas encontrados foram utilizados como entrada para o `PROMPT-004 — Refinamento do contrato OpenAPI`. O resultado gerado pelo Copilot foi novamente analisado antes de sua aprovação.

#### Arquivos afetados

- `openapi.yaml`;
- `ai/prompts.md`;
- `ai/revisoes.md`.

#### Notas adicionais e justificativa técnica

A estrutura geral produzida pela IA foi aproveitada, pois estava alinhada aos endpoints e às principais regras definidas para a API.

Entretanto, a primeira versão ainda apresentava inconsistências nos schemas, exemplos de erro, validações de texto e componentes reutilizáveis. Por esse motivo, ela não foi considerada aprovada para orientar diretamente a implementação.

A preservação da versão original e o registro das correções demonstram a separação entre o conteúdo produzido pela IA e as decisões aprovadas durante a revisão humana.

---

### Revisão final do contrato OpenAPI refinado

- **ID:** Revisao-20260717-003
- **Data:** 2026-07-17
- **Revisor:** Guilherme Bezerra Antonio
- **Origem:** Versão refinada do `openapi.yaml`, produzida pelo GitHub Copilot Chat a partir do `PROMPT-004 — Refinamento do contrato OpenAPI`
- **Versão revisada:** Commit `ffbb304`
- **Decisão:** Corrigida

#### Partes aceitas

- Inclusão de descrições mais completas para as operações de criação de projetos e tarefas;
- Documentação de que os campos de data e hora utilizam UTC;
- Inclusão das restrições de tamanho, conteúdo e obrigatoriedade nos schemas de resposta;
- Ampliação dos exemplos de `400 Bad Request` para representar diferentes falhas de validação;
- Manutenção dos endpoints, códigos de erro, regras de negócio e schemas previamente aprovados;
- Utilização de UUIDs concretos nos campos `instance` dos exemplos;
- Ausência de referências `$ref` inválidas ou componentes reutilizáveis sem utilização;
- Preservação do contrato em OpenAPI 3.0.3.

#### Partes corrigidas

- A mensagem do exemplo de PATCH vazio foi ajustada para representar o envio de um objeto sem propriedades, alinhando-se à restrição `minProperties: 1`;
- O exemplo genérico de campos controlados pela aplicação foi separado entre criação de projeto e criação de tarefa;
- Os campos `projectId` e `completedAt` foram removidos do exemplo de criação de projeto, pois pertencem ao recurso tarefa;
- O campo `status` foi incluído entre os campos controlados pela aplicação durante a criação dos recursos;
- O exemplo de enum inválido passou a utilizar a rota `PATCH /projetos/{id}`, na qual o campo `status` pode ser enviado pelo cliente;
- As descrições de `GET /projetos` e `GET /projetos/{id}/tarefas` foram atualizadas para informar que as listagens são retornadas sem ordenação definida.

#### Partes rejeitadas

- O exemplo único que misturava campos controlados de projetos e tarefas foi rejeitado por não representar corretamente nenhum dos dois recursos;
- O uso de `/projetos` no exemplo de status inválido foi rejeitado porque o endpoint de criação não permite o envio desse campo;
- A expressão “corpo vazio” foi rejeitada no exemplo associado a `minProperties: 1`, pois o caso representado é um objeto vazio, e não necessariamente a ausência completa do corpo da requisição.

#### Validações realizadas

Foi executada a validação formal automatizada do contrato OpenAPI por meio do Redocly CLI:

```text
npx @redocly/cli@latest lint openapi.yaml --extends=spec
```

A validação foi realizada com o Redocly CLI 2.39.0 e retornou:

```text
openapi.yaml: validated
Woohoo! Your API description is valid.
```

#### Ação realizada

A versão refinada produzida com auxílio da IA foi preservada no commit `ffbb304`.

Após nova revisão humana, os exemplos de validação e as descrições das listagens foram corrigidos manualmente. Em seguida, o arquivo foi validado quanto à formatação e à conformidade estrutural com a especificação OpenAPI.

Com essas correções e validações, o contrato foi considerado aprovado para orientar a implementação da API.

#### Arquivos afetados

- `openapi.yaml`;
- `ai/prompts.md`;
- `ai/revisoes.md`;
- `ai/skills.md`.

#### Notas adicionais e justificativa técnica

A aprovação do contrato não garante, por si só, que a implementação futura responderá exatamente conforme os schemas documentados.

Durante a implementação, serão criados testes de contrato com xUnit e `WebApplicationFactory` para comparar as respostas reais da API com o comportamento definido no `openapi.yaml`.

A revisão final preserva a separação entre a geração assistida por IA, as correções humanas e a validação automatizada do artefato.

---

### Revisão da estrutura inicial da solução .NET

- **ID:** Revisao-20260718-004
- **Data:** 2026-07-18
- **Revisor:** Guilherme Bezerra Antonio
- **Origem:** Estrutura inicial gerada pelo GitHub Copilot Chat a partir do `PROMPT-005 — Planejamento e criação do bootstrap da solução .NET`
- **Versão revisada:** Commit `aff22a4`
- **Decisão:** Corrigida

#### Partes aceitas

- Criação da solution `TaskFlow.sln`;
- Criação do projeto `src/TaskFlow.Api/TaskFlow.Api.csproj`;
- Utilização do .NET 8;
- Adição do projeto `TaskFlow.Api` à solution;
- Manutenção das configurações padrão de ambiente e execução;
- Inclusão do Swagger para utilização em ambiente de desenvolvimento;
- Ausência de dependências de persistência, testes ou arquiteturas não solicitadas;
- Compilação bem-sucedida da estrutura inicialmente gerada.

#### Partes corrigidas

- Substituição da configuração baseada em Minimal API pelo registro de Controllers com `AddControllers()`;
- Inclusão de `MapControllers()` no pipeline da aplicação;
- Remoção do endpoint demonstrativo `/weatherforecast`;
- Remoção do modelo demonstrativo `WeatherForecast`;
- Remoção do arquivo `TaskFlow.Api.http`, que ainda referenciava o endpoint demonstrativo;
- Remoção do pacote `Microsoft.AspNetCore.OpenApi`, que deixou de possuir utilização após a retirada de `WithOpenApi()`.

#### Partes rejeitadas

- O endpoint `/weatherforecast` e o modelo `WeatherForecast` foram rejeitados porque eram exemplos do template e não pertenciam ao domínio do TaskFlow;
- A configuração por Minimal API foi rejeitada porque o prompt determinava o uso de ASP.NET Core Web API com Controllers;
- O arquivo `TaskFlow.Api.http` foi rejeitado porque continha somente uma requisição ao endpoint demonstrativo removido.

#### Validações realizadas

Foram executados os seguintes comandos após as correções:

```text
dotnet restore TaskFlow.sln
dotnet build TaskFlow.sln
```

O build foi concluído com:
```text
0 Aviso(s)
0 Erro(s)
```
#### Ação realizada

saída da IA → preservação → revisão humana → correção → validação

#### Arquivos afetados

- `src/TaskFlow.Api/Program.cs`;
- `src/TaskFlow.Api/TaskFlow.Api.csproj`;
- `src/TaskFlow.Api/TaskFlow.Api.http`;
- `ai/prompts.md`;
- `ai/revisoes.md`.

---

### Revisão da base de domínio e persistência

- **ID:** Revisao-20260718-005
- **Data:** 2026-07-18
- **Revisor:** Guilherme Bezerra Antonio
- **Origem:** Base gerada pelo GitHub Copilot Chat a partir do `PROMPT-006`
- **Versão revisada:** Commit `7bfd217`
- **Decisão:** Corrigida

#### Partes aceitas

- Estrutura das entidades `Project` e `TaskItem`;
- configuração do EF Core com SQLite;
- criação do `TaskFlowDbContext`;
- configurações separadas por entidade;
- relacionamento de um projeto para muitas tarefas;
- campos internos normalizados;
- índice único global para nomes de projeto;
- índice único de títulos de tarefa dentro de cada projeto;
- ferramenta local `dotnet-ef`;
- estratégia de migrations;
- ausência de Controllers, Services e endpoints fora do escopo.

#### Partes corrigidas

- Remoção do limite de 4.000 caracteres da descrição do projeto;
- renomeação de `TaskStatus` para `TaskItemStatus`;
- adequação dos enums às convenções do C#;
- correção da nulabilidade da navegação `TaskItem.Project`;
- restrição das regras do `SaveChanges` aos estados `Added` e `Modified`;
- utilização de um único valor UTC em cada operação;
- remoção do fallback silencioso da connection string;
- limpeza do `appsettings.Development.json`;
- regeneração da migration inicial.

#### Partes rejeitadas

- O limite de 4.000 caracteres foi rejeitado porque não estava definido nos artefatos do projeto;
- o arquivo `InitialCreate.sql` foi rejeitado por duplicar a migration mantida pelo EF Core;
- o nome `TaskStatus` foi rejeitado por conflitar com um tipo existente no .NET.

#### Validações realizadas

Foram executados:

```text
dotnet restore TaskFlow.sln
dotnet build TaskFlow.sln
```

Resultado:

```text
0 Aviso(s)
0 Erro(s)
```

A migration regenerada foi revisada para confirmar:

- campos obrigatórios e opcionais;
- tamanhos máximos;
- chave estrangeira;
- exclusão com `Restrict`;
- índices únicos;
- ausência do limite não especificado de 4.000 caracteres.

Também foi confirmado que a migration ainda não foi aplicada e nenhum banco SQLite foi criado.

#### Ação realizada

A saída inicial da IA foi preservada no commit `7bfd217`. As correções foram realizadas manualmente e a migration foi regenerada após a estabilização do modelo.

---

### Revisão da API de projetos

- **ID:** Revisao-20260718-006
- **Data:** 2026-07-18
- **Revisor:** Guilherme Bezerra Antonio
- **Origem:** API de projetos gerada pelo GitHub Copilot Chat a partir do `PROMPT-007`
- **Versão revisada:** Commit `26d8616`
- **Decisão:** Corrigida

#### Partes aceitas

- Separação entre contratos HTTP, Controller, Service, domínio e persistência;
- criação de DTOs específicos para criação, atualização e resposta de projetos;
- utilização de `OptionalField<T>` para diferenciar propriedades omitidas de propriedades enviadas explicitamente no PATCH;
- diferenciação entre `description` omitida e `description` enviada como `null`;
- validação de PATCH sem campos para atualização;
- implementação dos endpoints de criação, listagem, consulta por identificador e atualização de projetos;
- retorno de `201 Created` com header `Location` por meio de rota nomeada e `CreatedAtRoute`;
- utilização de `AsNoTracking` nas consultas somente de leitura;
- propagação de `CancellationToken`;
- normalização do nome do projeto;
- verificação prévia de unicidade;
- utilização do índice único do banco como segunda barreira contra conflitos de concorrência;
- suporte ao filtro opcional por status;
- permissão de reativação de projetos arquivados;
- comportamento idempotente ao atualizar um projeto para o status atual;
- bloqueio do arquivamento quando existem tarefas em andamento;
- ausência de endpoints de tarefas e testes fora do escopo deste ciclo.

#### Partes corrigidas

- Inclusão da validação de tamanho máximo de 100 caracteres para o nome do projeto no POST e no PATCH;
- substituição do formatter JSON personalizado pelo suporte nativo do .NET 8 para rejeição de propriedades desconhecidas;
- remoção da interface `IJsonExtensionDataContainer`, que não possui utilização;
- remoção da política personalizada de snake case em favor de `JsonNamingPolicy.SnakeCaseLower`;
- remoção da configuração duplicada do converter de `OptionalField<T>`;
- centralização das configurações de serialização JSON;
- adoção de `AddProblemDetails`, `AddExceptionHandler` e `IExceptionHandler`;
- correção do preenchimento de `type`, `detail` e `instance` nas respostas `ProblemDetails`;
- associação dos códigos estáveis de erro às próprias exceções, evitando que exceções genéricas sejam vinculadas permanentemente a erros de projeto;
- remoção do carregamento antecipado de todas as tarefas durante qualquer atualização de projeto;
- utilização de `AnyAsync` somente quando houver uma tentativa real de arquivamento;
- correção da atualização do nome quando houver alteração apenas de maiúsculas e minúsculas;
- revisão da aderência dos títulos, detalhes e códigos de erro ao `openapi.yaml`.
- correção da perda do `DateTimeKind.Utc` após a leitura das datas no SQLite;
- criação de conversores de valor para preservar a interpretação UTC de `CreatedAt` e `CompletedAt`;
- rejeição de valores numéricos para enums serializados como string;
- validação explícita do filtro de status da listagem;
- supressão do erro implícito e redundante de parâmetro obrigatório em falhas de desserialização;
- rejeição de propriedades não previstas nos contratos de criação e atualização;
- identificação do conflito de unicidade durante o `SaveChanges` pelo código estendido `2067` do SQLite e pela entidade `Project`;
- substituição de `CreatedAtAction` por uma rota nomeada com `CreatedAtRoute`, evitando falha na geração do header `Location` após o `SaveChanges`;

#### Partes rejeitadas

- O formatter `RejectUnknownJsonPropertiesInputFormatter` foi rejeitado por duplicar um recurso nativo do .NET 8 e adicionar leitura manual do corpo, reflexão e desserialização adicional;
- a interface `IJsonExtensionDataContainer` foi rejeitada por não possuir utilização na implementação;
- a classe `SnakeCaseNamingPolicy` foi rejeitada por duplicar `JsonNamingPolicy.SnakeCaseLower`;
- a configuração aninhada de `UseExceptionHandler` foi rejeitada por tornar o tratamento global de erros desnecessariamente complexo;
- o carregamento de toda a coleção de tarefas em qualquer PATCH de projeto foi rejeitado por realizar uma consulta desnecessária quando somente nome ou descrição são atualizados.

#### Validações iniciais realizadas

Foram executados:

```text
git diff --check
dotnet restore TaskFlow.sln
dotnet build TaskFlow.sln
```

Resultado inicial da geração:

```text
0 Aviso(s)
0 Erro(s)
```

A compilação bem-sucedida confirmou a validade sintática da implementação, mas não foi considerada suficiente para comprovar a aderência ao contrato.

#### Ação realizada

A saída inicial da IA foi preservada no commit `26d8616`. As correções identificadas nesta revisão serão realizadas manualmente e validadas antes da conclusão deste registro.
