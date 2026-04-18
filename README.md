# 🍔 Good Hamburger - Sistema de Pedidos

Este projeto é uma plataforma de gerenciamento de pedidos para a lanchonete **Good Hamburger**, desenvolvida para demonstrar arquitetura moderna com .NET 10 (compatível com .NET 8) e Blazor Interativo.


<img width="1737" height="1021" alt="Fronthamb" src="https://github.com/user-attachments/assets/cb9fcbfd-489a-4f44-acd4-c57f70a082bc" />


## 🚀 Tecnologias Utilizadas

- **Backend:** ASP.NET Core Web API
- **Frontend:** Blazor Web App (Interactive Server Mode)
- **ORM:** Entity Framework Core com SQLite/SQL Server
- **Testes:** xUnit
- **Documentação:** Swagger/OpenAPI


## 🛠️ Instruções de Execução

### Pré-requisitos
- .NET 8 SDK instalado.
- Visual Studio 2022 ou VS Code.

### Passo a passo:

Clone o repositório:
```bash
git clone https://github.com/seu-usuario/good-hamburger.git
```
Restaure e compile a solução:
```bash
dotnet restore
dotnet build
```
🛠️ Como configurar o Banco
Para rodar o projeto com SQL Server, siga estes passos:

Connection String: No arquivo appsettings.json da GoodHamburger.Api, ajuste a string de conexão para apontar para sua instância local:
```bash
JSON
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GoodHamburger;Trusted_Connection=True;"
}
```

Atualizar o Banco: Execute o comando abaixo no terminal (dentro da pasta da API) para criar as tabelas:

```bash
dotnet ef database update
```


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

## 🏗️ Decisões de Arquitetura

- **Blazor Interactive Server Mode:** Escolhido para garantir uma comunicação robusta em tempo real via SignalR. Isso permite que a lógica de UI permaneça próxima ao servidor, facilitando o gerenciamento de estado e segurança (Antiforgery) sem a complexidade inicial de um Client-Side puro.

- **Separação de Responsabilidades (SoC):** A lógica de cálculo de descontos foi isolada em uma camada de serviço (OrderService) no Backend. Isso garante que as regras de negócio sejam centralizadas, facilitando a manutenção e permitindo que sejam testadas de forma isolada (Testes Unitários).

- **Injeção de Dependência:** Utilizada para desacoplar o HttpClient e os serviços, facilitando a substituição por implementações de mock no futuro, se necessário.

- **Padrão AAA nos Testes:** Adotado para garantir que a suite de testes seja legível e siga o padrão de mercado (Arrange, Act, Assert).
  
-  **Persistência Real em Banco de Dados:** Dados gravado no Banco de Dados(SqlServer).

## 🗄️ Persistência de Dados

- **SQL Server:** Utilizado como banco de dados relacional para garantir a consistência e integridade dos pedidos e itens do cardápio.

- **Entity Framework Core:** Implementado como ORM para mapeamento objeto-relacional, utilizando a abordagem Code First.

- **Migrations:** O versionamento do banco de dados é gerido via EF Migrations, permitindo a evolução do esquema de dados de forma controlada e auditável.

- **Data Seeding:** O cardápio inicial (Sanduíches, Acompanhamentos e Bebidas) é populado automaticamente durante a inicialização/migração, garantindo que o sistema esteja pronto para uso imediato após o deploy.


<img width="1349" height="707" alt="Banco de dados" src="https://github.com/user-attachments/assets/d6c56a5e-915a-4e9b-a4be-543fcfa68544" />


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

## 🚧 O que ficou de fora (Próximos Passos)

- **Autenticação e Autorização:** O sistema não possui controle de acesso. O próximo passo seria implementar ASP.NET Core Identity ou integração com Auth0/Azure AD.

- **Dockerização:** A criação de um Dockerfile e docker-compose.yml para facilitar o deploy e a orquestração dos serviços de API e Frontend.

- **Logging Estruturado:** Implementação de Serilog para rastreio de erros e monitoramento de performance em produção.

- **Arredondamento Monetário:** Atualmente o sistema trabalha com precisão decimal total. Uma melhoria seria implementar uma política global de arredondamento para duas casas decimais seguindo normas contábeis.

 





