Segue um README atualizado para o contexto da sua API atual:

✅ .NET
✅ Dapper
✅ PostgreSQL
✅ Sem JWT
✅ CORS liberado
✅ Arquitetura Repository + Service
✅ Regra de negócio para ativar produtor
✅ API externa simulada (situação financeira)

Você pode colar direto no GitHub.

🚀 Como rodar a API (.NET + Dapper + PostgreSQL)
📌 Requisitos

Antes de começar, instale:

🟦 .NET SDK (recomendado: .NET 8)

🐘 PostgreSQL instalado e em execução

🧰 DBeaver / PgAdmin (opcional, mas recomendado)

🛠️ Configuração do Banco de Dados

Crie um banco no PostgreSQL:

CREATE DATABASE producerdb;

📍 Configure a ConnectionString

No arquivo:

appsettings.json


Exemplo:

{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=producerdb;Username=postgres;Password=SUA_SENHA"
  }
}


⚠️ Nunca envie senhas reais para o GitHub.
Use variáveis de ambiente em produção.

🗂️ Criar as Tabelas

⚠️ Este projeto usa Dapper, então não possui migrations.

Execute o script abaixo no PostgreSQL:

✅ Tabela Producer
CREATE TABLE producer (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    document VARCHAR(20) NOT NULL UNIQUE,
    status VARCHAR(20) NOT NULL,
    date_registration TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

✅ Tabela Territory
CREATE TABLE territory (
    id SERIAL PRIMARY KEY,
    producer_id INT NOT NULL,
    name VARCHAR(255) NOT NULL,
    area NUMERIC(10,2) NOT NULL,
    type_territory VARCHAR(20) NOT NULL,

    CONSTRAINT fk_producer
        FOREIGN KEY (producer_id)
        REFERENCES producer(id)
        ON DELETE CASCADE
);

▶️ Compilar e Rodar a API

Dentro da pasta do projeto:

dotnet restore
dotnet build
dotnet run


Você verá algo como:

Now listening on: https://localhost:7181

🔎 Testar a API

Abra o Swagger:

https://localhost:7181/swagger

📚 Principais Endpoints
✅ Producers
Método	Endpoint
GET	/Producer
GET	/Producer/{id}
POST	/Producer
PUT	/Producer/{id}
DELETE	/Producer/{id}
✅ Territories
Método	Endpoint
GET	/Territory
GET	/Territory/{id}
POST	/Territory
PUT	/Territory/{id}
DELETE	/Territory/{id}
✅ Situação Financeira (API Externa Simulada)

Consulta o score financeiro do produtor.

GET /Financial/situation/{document}


Exemplo:

/Financial/situation/12345678900


Retorno:

{
  "document": "12345678900",
  "score": 720,
  "situation": "Aprovado",
  "creditLimit": 5000
}

⭐ Regra de Negócio

Um produtor só pode estar ATIVO se:

✅ possuir pelo menos 1 território cadastrado
✅ tiver situação financeira Regular ou Aprovada

Essa regra está centralizada na camada de Service, garantindo:

melhor organização

código testável

separação de responsabilidades

arquitetura profissional

🧠 Arquitetura do Projeto

O projeto segue o padrão:

Controllers → Services → Repositories → Database

✔️ Controllers

Responsáveis pelos endpoints HTTP.

✔️ Services

Onde ficam as regras de negócio.

✔️ Repositories

Acesso ao banco via Dapper.

🌐 CORS

A API está configurada para aceitar requisições de qualquer origem (ideal para desenvolvimento).

⚠️ Em produção, restrinja os domínios permitidos.