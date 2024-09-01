# Desafio Técnico - Benner

Apresentação em vídeo: https://youtu.be/1pkTg2ndZ1c

# Objetivos do projeto:
- Desenvolver um aplicativo simples para controle de estacionamento onde o usuário poderá registrar a entrada e saída dos veículos. 
- Os valores praticados pelo estacionamento devem ser parametrizados em uma tabela de preços com controle vigência. Exemplo: Valores válidos para o período de 01/01/2024 até 31/12/2024.
- Utilizar a data de entrada do veículo como referência para buscar a tabela de preços.
- A tabela de preço deve contemplar o valor da hora inicial e valor para as horas adicionais.
- Será cobrado metade do valor da hora inicial quando o tempo total de permanência no estacionamento for igual ou inferior a 30 minutos.
- O valor da hora adicional possui uma tolerância de 10 minutos para cada 1 hora. Exemplo: 30 minutos valor R$ 1,00 | 1 hora valor R$ 2,00 | 1 hora 10 minutos valor R$ 2,00 | 1 hora e 15 minutos valor R$ 3,00 | 2 horas e 5 minutos valor R$ 3,00 | 2 horas e 15 minutos valor R$ 4,00.
- Utilizar a placa do veículo como chave de busca. 
- O sistema deve possuir uma interface desktop ou web para registrar as entradas, saídas e parametrizações.
- Utilizar uma estrutura de armazenamento local como Arquivo, SQLite, Access, MySql, DB4o, etc.
- O sistema deve ser implementado em C#. 
- A interface pode ser Desktop ou Web.
- Se possível utilizar conceitos de mercado como Entity framework, LINQ, MVC, design patterns, TDD.

# Arquitetura do sistema:
![Arquitetura do sistema](https://res.cloudinary.com/dtvypcack/image/upload/v1725220273/Arquitetura_dbolfh.png)

Tecnologias utilizadas:
- Building Blocks:
	- FluentValidation Versão 11.9.2
	- FluentValidation.AspNetCore Versão 11.3.0
	- FluentValidation.DependencyInjectionExtensions Versão 11.9.2
	- Mapster Versão 7.4.0
	- MediatR Versão 12.4.0
- API:
	- Carter Versão 8.0.0
	- Grpc.AspNetCore Versão 2.57.0
	- Microsoft.EntityFrameworkCore.Sqlite Versão 8.0.1
	- Microsoft.EntityFrameworkCore.Tools Versão 8.0.1
- gRPC:
	- Grpc.AspNetCore Versão 2.57.0
	- Microsoft.EntityFrameworkCore.Sqlite Versão 8.0.1
	- Microsoft.EntityFrameworkCore.Tools Versão 8.0.1
- UI:
	- MudBlazor Versão 7.6.0
	- Refit.HttpClientFactory Versão 7.1.2

Bancos de dados:
- API:
	- SQLite: Data Source = parkingdb
- gRPC:
	- SQLite: Data Source = precosbasedb

# Detalhes técnicos

***Documentação da API:*** https://documenter.getpostman.com/view/30577432/2sAXjM3rBz

***Configuração do ambiente:*** Para configurar o ambiente em sua máquina local, você deve seguir os passos listados abaixo:

1. Instalar o Docker: caso você ainda não possua o Docker instalado em sua máquina, você pode instalá-lo através do link.
	1. Windows: https://docs.docker.com/desktop/install/windows-install/
	2. Linux: https://docs.docker.com/desktop/install/linux-install/
2. Após isto, clone o projeto através do github utilizando este comando git:
	1. `git clone https://github.com/brenovandall/benner.git`
3. Com o projeto clonado, navegue até o diretório `/src` do projeto:
	1. Geralmente: `cd ~/BennerSolution/src`
4. Com o Docker iniciado, basta executar este comando dentro do diretório /src para inicializar todos os containeres:
	1. `docker compose up`
5. Após todos os containeres iniciares, a aplicação pode ser acessada em: https://localhost:6062

# Sumário:
Neste documento, você pode entender sobre:
1. [Objetivos do projeto](#objetivos-do-projeto)
2. [Arquitetura do sistema e tecnologias utilizadas](#arquitetura-do-sistema)
3. [Documentação da API e configuração do ambiente](#detalhes-técnicos)
