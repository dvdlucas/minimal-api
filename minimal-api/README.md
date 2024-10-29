passo a passo para a a criação de uma minimmal api em c#
instalar os framework se necessário > .NET Core, SQL

comando dotnet new web -o Nome
criar models > classe de repository, camada de acesso ao banco > service camada de regras de negócios > controller camada de lidar com requisições e respostas

criar meu banco context que é a minha conexão com o banco

comandos para gerar migrations

dotnet-ef migrations add MinhaMigration
dotnet-ef update database
