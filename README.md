# 🍔 Good Hamburger - Sistema de Pedidos

Este projeto é um sistema de gerenciamento de pedidos para a lanchonete **Good Hamburger**, desenvolvido como demonstração técnica utilizando **.NET 8**.

## 🚀 Tecnologias Utilizadas

- **Backend:** ASP.NET Core Web API
- **Frontend:** Blazor Web App (Interactive Server Mode)
- **Testes:** xUnit
- **Documentação:** Swagger/OpenAPI

## 📋 Regras de Negócio Implementadas

O sistema calcula descontos automaticamente com base na composição do pedido:
- **Combo Completo (Sanduíche + Batata + Refrigerante):** 20% de desconto.
- **Sanduíche + Refrigerante:** 15% de desconto.
- **Sanduíche + Batata:** 10% de desconto.
- **Validação:** Cada pedido permite apenas um item de cada categoria (Sanduíche, Acompanhamento, Bebida).

## 🛠️ Como Executar

### Pré-requisitos
- .NET 8 SDK instalado.

### 1. Executar a API
Navegue até a pasta da API e execute:
```bash
cd GoodHamburger.Api
dotnet run --urls "http://localhost:5000"
```
Acesse o Swagger em: `http://localhost:5000/swagger`

### 2. Executar o Frontend
Em outro terminal, navegue até a pasta do Frontend e execute:
```bash
cd GoodHamburger.Frontend
dotnet run --urls "http://localhost:5001"
```
Acesse o sistema em: `http://localhost:5001`

### 3. Executar os Testes
Para validar as regras de desconto e validações:
```bash
cd GoodHamburger.Tests
dotnet test
```

## 📂 Estrutura do Projeto

- `GoodHamburger.Api`: API REST com lógica de domínio e endpoints.
- `GoodHamburger.Frontend`: Interface Blazor para interação com o usuário.
- `GoodHamburger.Tests`: Testes unitários das regras de negócio.
