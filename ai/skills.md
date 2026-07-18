# Registro de Habilidades Delegadas à IA

Este documento registra habilidades efetivamente delegadas à IA para apoiar o desenvolvimento SDD do TaskFlow.

## Critério de confiança

O nível de confiança representa a expectativa inicial sobre a confiabilidade da saída produzida pela IA antes da revisão humana. Ele não substitui validações automatizadas nem a aprovação do responsável técnico.

- **Alto:** Tarefa objetiva, de baixo risco e com resultado facilmente verificável.
- **Médio:** Tarefa que exige interpretação e pode produzir inconsistências, devendo passar por revisão humana estruturada.
- **Baixo:** Tarefa exploratória, ambígua ou de alto impacto técnico, cuja saída deve ser tratada apenas como sugestão inicial.

## Habilidade: Estruturação e refinamento de prompts

- **Ferramenta:** ChatGPT
- **Modelo:** GPT-5.6 Thinking
- **Descrição:** Apoiar a estruturação e o refinamento de prompts utilizados para orientar outras ferramentas de IA durante o desenvolvimento, buscando aumentar a clareza, a precisão e a aderência dos resultados gerados pelo GitHub Copilot Chat.
- **Escopo:** Organização do contexto, explicitação de restrições, definição de formato de saída, identificação de ambiguidades e melhoria da clareza dos prompts.
- **Entradas:** Rascunhos de prompts, requisitos, decisões técnicas aprovadas e objetivos definidos.
- **Saída:** Prompts refinados para execução no GitHub Copilot Chat.
- **Nível de confiança:** Médio
- **Responsável humano pela revisão:** Guilherme Bezerra Antonio
- **Restrições:** O ChatGPT atua como ferramenta de apoio e não realiza alterações diretamente no repositório. Os prompts refinados devem ser revisados e aprovados pelo responsável humano antes da execução.

## Habilidade: Estruturação documental inicial do fluxo SDD

- **Ferramenta:** GitHub Copilot Chat
- **Modelo:** GPT-5 mini
- **Descrição:** Produzir a estrutura documental inicial para conduzir o projeto por Specification-Driven Development (SDD), incluindo templates e artefatos de rastreabilidade.
- **Escopo:** Geração de artefatos documentais (sem código-fonte nem endpoints); não inclui decisões de aprovação final.
- **Entradas:** Contexto técnico (requisitos obrigatórios e restrições) e o prompt de orientação (restrições do enunciado).
- **Saídas:** Os seguintes documentos gerados nesta etapa:

  - `ai/prompts.md`
  - `ai/revisoes.md`
  - `ai/skills.md`
  - `docs/decisoes.md`
  - `openapi.yaml` (esqueleto OpenAPI 3.0.3)
  - `README.md`
- **Nível de confiança:** Médio
- **Responsável humano (revisão):** Guilherme Bezerra Antonio
- **Observações / Restrições:** A IA não possui autoridade para aprovar requisitos, decisões de projeto ou assumir responsabilidade por aprovações formais; todas as saídas devem ser validadas por revisores humanos.

**Importante:** Nenhum endpoint ou schema de domínio foi definido nesta etapa; o `openapi.yaml` contém apenas o esqueleto inicial solicitado.

## Habilidade: Modelagem inicial do contrato OpenAPI

- **Ferramenta:** GitHub Copilot Chat
- **Modelo:** GPT-5 mini
- **Descrição:** Gerar a primeira versão do contrato OpenAPI a partir dos requisitos funcionais, regras de negócio e decisões de design registradas.
- **Escopo:** Endpoints, parâmetros, schemas, requests, responses, erros e exemplos; sem geração de código de implementação.
- **Entradas:** Enunciado do desafio, decisões documentadas e prompt com restrições explícitas.
- **Saída:** Primeira versão completa do arquivo `openapi.yaml`.
- **Nível de confiança:** Médio
- **Responsável humano pela revisão:** Guilherme Bezerra Antonio
- **Restrições:** A versão gerada não é considerada aprovada antes da revisão humana de consistência e aderência ao enunciado.

## Habilidade: Refinamento de contrato OpenAPI existente

- **Ferramenta:** GitHub Copilot Chat
- **Modelo:** GPT-5 mini
- **Descrição:** Refinar uma versão existente do contrato OpenAPI a partir de inconsistências e melhorias identificadas durante a revisão humana.
- **Escopo:** Ajustes em schemas, descrições de operações, exemplos de erros, campos obrigatórios, referências reutilizáveis e restrições de validação; sem alteração das decisões técnicas aprovadas.
- **Entradas:** Versão inicial do `openapi.yaml`, decisões registradas, problemas identificados durante a revisão humana e o `PROMPT-004`.
- **Saída:** Versão refinada do arquivo `openapi.yaml`.
- **Nível de confiança:** Médio
- **Responsável humano pela revisão:** Guilherme Bezerra Antonio
- **Restrições:** O refinamento gerado pela IA não é considerado aprovado automaticamente. O resultado deve passar por validação sintática, análise de consistência e nova revisão humana antes de orientar a implementação.
