# DEVLOG — Imagina Só ERP

## Sprint 0 — Fundação do Projeto
**Status:** Em andamento

### Objetivo
Preparar o ambiente de desenvolvimento e a estrutura base da Clean Architecture.

### Decisões técnicas
- Migração de .NET 9 → .NET 10 LTS (ver ADR 0001).
- PostgreSQL rodando via Docker Compose (sem instalação nativa).
- Visual Studio 2026 para o backend .NET; VS Code para Python e React.

### O que aprendi
- Diferença entre versões STS e LTS do .NET.
- Estrutura de camadas da Clean Architecture e a regra de dependência
  (Domain não depende de nada; Application só depende de Domain).

### Próximos passos
- Configurar docker-compose.yml com PostgreSQL 17.
- Configurar Entity Framework Core 10 + connection string.
- Subir o primeiro endpoint /health.