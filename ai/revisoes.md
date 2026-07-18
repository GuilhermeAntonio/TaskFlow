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