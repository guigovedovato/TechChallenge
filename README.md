# FIAP Cloud Games

A FIAP Cloud Games (FCG) é uma plataforma de venda de jogos digitais e gestão de servidores para partidas online.

## Projeto

API REST em .NET8 para gerenciar usuários e seus jogos adquiridos.

* Para executar o programa com sucesso, é necessário:
  * Por favor, veja [Requisitos](#requisitos) para os requisitos deste projeto;
  * Baixe e configure a aplicação na seção [Iniciando](#iniciando);
  * Como executar a aplicação na seção [Execução](#execução);
  * Veja a API na seção [API](#api);

## Requisitos

* Baixe e instale o [.NET8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Iniciando

* Clone o projeto

``` dotnet
git clone https://github.com/guigovedovato/fiap.git
```

* Navegue até a pasta do projeto

``` dotnet
cd TechChallenge
```

* Builde o projeto

``` dotnet
dotnet build
```

* Configure o Banco de Dados
  * Altere a configuração da _connection string_ com as informações do teu banco de dados
  * Corra as **Migrations**, na folder de Infrastructure para a criação do banco de dados:

``` dotnet
Update-Database -Project FCG.Infrastructure -s FCG.API
```

## Execução

* Inicie a API

``` dotnet
dotnet run
```

## API

* Abra o **navegador** e acesse <http://localhost:5267/swagger/index.html> ou [clique aqui](http://localhost:5267/swagger/index.html)
