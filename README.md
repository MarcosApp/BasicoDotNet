# 🚀 Sistema de Aviso – Clean Architecture + CQRS + MediatR
[![Build](https://github.com/MarcosApp/BasicoDotNet/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/MarcosApp/BasicoDotNet/actions/workflows/dotnet-ci.yml)
![C#](https://img.shields.io/badge/.NET-8.0-blue)
![Clean Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-green)
![CQRS](https://img.shields.io/badge/Pattern-CQRS-orange)
![MediatR](https://img.shields.io/badge/Mediator-MediatR-purple)
![Validation](https://img.shields.io/badge/Validation-FluentValidation-yellow)
![Tests](https://img.shields.io/badge/Tests-Integration%20Tests-brightgreen)

---

## 📋 Descrição

O **Sistema de Aviso** é uma implementação completa do fluxo de Avisos dentro de uma API .NET seguindo os princípios de **Clean Architecture**, **CQRS**, **MediatR**, **Validação na borda**, **Soft Delete** e **Auditoria**.

Desenvolvido para demonstrar clareza, boas práticas, visão arquitetural e capacidade analítica — exatamente como solicitado no desafio técnico.

---

## 🎯 Objetivos Atendidos

- ✔ Criar, atualizar, consultar e remover avisos  
- ✔ Implementação de Soft Delete  
- ✔ Campos de auditoria (`DataCriacao`, `DataAtualizacao`)  
- ✔ Validação com FluentValidation  
- ✔ Arquitetura Clean + CQRS preservada  
- ✔ Testes de integração cobrindo fluxo real  
- ✔ Código limpo, organizado e extensível  

---

## 🧠 Arquitetura Utilizada

O projeto segue:

- Clean Architecture  
- Commands e Queries separados (CQRS)  
- MediatR como mediador central  
- FluentValidation para validações  
- Repository Pattern  
- Domain com invariantes  
- InMemory para testes  
- Handlers com responsabilidade única  
- Extension Methods para mapeamento  

---

## 🧩 Funcionalidades do Módulo

### ➕ Criar Aviso  
`POST /api/v1/avisos`  
Valida título e mensagem. Cria aviso ativo com DataCriacao.

### 🔍 Obter Aviso por ID  
`GET /api/v1/avisos/{id}`  
Retorna somente avisos ativos. Rejeita ID inválido.

### ✏ Atualizar Somente a Mensagem  
`PUT /api/v1/avisos/{id}`  
Regra: somente a *mensagem* pode ser alterada.

### ❌ Remover (Soft Delete)  
`DELETE /api/v1/avisos/{id}`  
Somente marca o aviso como inativo.  
Após isso, `GET` retorna 404.

---

## 🛠️ Tecnologias Utilizadas

- .NET 8  
- ASP.NET Core  
- Entity Framework Core (InMemory)  
- MediatR  
- FluentValidation  
- Clean Architecture  
- xUnit  
- FluentAssertions  
- WebApplicationFactory  

---

## 🧪 Testes de Integração

Os testes cobrem o fluxo real da API usando:

- WebApplicationFactory  
- InMemory Database  
- xUnit  
- FluentAssertions  

### Testes Implementados:
- GET → 200  
- GET (not found) → 404  
- GET (bad request ID inválido) → 400  
- POST → 201  
- POST inválido → 400  
- PUT → 200  
- DELETE (soft delete) → 204  
- DELETE + GET → 404  


---
**Marcos Gotado**

- GitHub: https://github.com/MarcosApp  
- Email: marcosdossantos43@hotmail.com  

---
