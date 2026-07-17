# Registro de Habilidades Delegadas à IA

**Ferramenta identificada:** GitHub Copilot Chat

Este documento registra habilidades efetivamente delegadas à IA para apoiar o desenvolvimento SDD do TaskFlow.

## Habilidade: Estruturação documental inicial do fluxo SDD

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
