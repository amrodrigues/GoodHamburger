# 🍔 Good Hamburger - Sistema de Pedidos

Este projeto é uma plataforma de gerenciamento de pedidos para a lanchonete **Good Hamburger**, desenvolvida para demonstrar arquitetura moderna com .NET 10 (compatível com .NET 8) e Blazor Interativo.


<img width="1737" height="1021" alt="Fronthamb" src="https://github.com/user-attachments/assets/cb9fcbfd-489a-4f44-acd4-c57f70a082bc" />


## 🚀 Tecnologias Utilizadas

- **Backend:** ASP.NET Core Web API
- **Frontend:** Blazor Web App (Interactive Server Mode)
- **ORM:** Entity Framework Core com SQLite/SQL Server
- **Testes:** xUnit
- **Documentação:** Swagger/OpenAPI


## 🛠️ Como Executar

### Pré-requisitos
- .NET 8 SDK instalado.

### 1. Executar a API
Navegue até a pasta da API e execute:
```bash
cd GoodHamburger.Api
dotnet run --urls "https://localhost:7286"
```
Acesse o Swagger em: `https://localhost:7286/swagger`

<img width="1911" height="1021" alt="APIhamb" src="https://github.com/user-attachments/assets/22eba44e-56f2-4ef0-a92c-d0a76b561856" />


### 2. Executar o Frontend
Em outro terminal, navegue até a pasta do Frontend e execute:
```bash
cd GoodHamburger.Frontend
dotnet run --urls "https://localhost:7275"
```
Acesse o sistema em: `https://localhost:7275`

<img width="1669" height="941" alt="Pedidos" src="https://github.com/user-attachments/assets/4e68f448-4e28-4dec-b8dd-5fa5e8b06846" />

### 3. Executar os Testes
Para validar as regras de desconto e validações:
```bash
cd GoodHamburger.Tests
dotnet test
```
<img width="1351" height="703" alt="testes" src="https://github.com/user-attachments/assets/73f58aa2-b6b1-4f9e-9e7c-d8251fca4cb3" />


## 📋 Regras de Negócio Implementadas

O sistema calcula descontos automaticamente com base na composição do pedido:
- **Combo Completo (Sanduíche + Batata + Refrigerante):** 20% de desconto.
- **Sanduíche + Refrigerante:** 15% de desconto.
- **Sanduíche + Batata:** 10% de desconto.
- **Validação:** Cada pedido permite apenas um item de cada categoria (Sanduíche, Acompanhamento, Bebida).


<img width="1917" height="919" alt="Só escolher um Hamb" src="https://github.com/user-attachments/assets/8d8e0767-d5db-4f14-9cfc-44d9aab3ecf5" />


## 📂 Estrutura do Projeto

- `GoodHamburger.Api`: API REST com lógica de domínio e endpoints.
- `GoodHamburger.Frontend`: Interface Blazor para interação com o usuário.
- `GoodHamburger.Tests`: Testes unitários das regras de negócio.

 ### 💡 Notas de Implementação
* **Interatividade:** O frontend utiliza `@rendermode InteractiveServer` globalmente no App.razor para permitir manipulação dinâmica de eventos (DOM).

 * **CORS:** A API possui políticas de Cross-Origin habilitadas para permitir a comunicação segura com o cliente Blazor.

 





