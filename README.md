# Sistema de Cadastro de Clientes e Endereços

Este projeto é um **Sistema para Cadastro de Clientes e Endereços**, desenvolvido utilizando **.NET 8**, seguindo os princípios de **Domain-Driven Design (DDD)** e **Arquitetura Limpa**. O sistema foi containerizado utilizando **Docker**, e o banco de dados utilizado é o **MSSQL Server**. Além disso, foram implementados **testes de unidade** para garantir a qualidade do código e das funcionalidades.

## Tecnologias Utilizadas

- **.NET 8**
- **Domain-Driven Design (DDD)**
- **Arquitetura Limpa**
- **MSSQL Server**
- **Docker**
- **Testes de Unidade (xUnit)**

## Estrutura do Projeto

A aplicação segue a **Arquitetura Limpa**, separando claramente as responsabilidades em camadas, permitindo maior flexibilidade e facilidade de manutenção. A estrutura básica do projeto é a seguinte:

- **Domain**: Contém as entidades e objetos de valor, que representam o núcleo do domínio da aplicação.
- **Application**: Contém os serviços de aplicação, interfaces e casos de uso.
- **Infrastructure**: Implementa a persistência de dados (com repositórios para o MSSQL Server), integração com serviços externos, etc.
- **Presentation**: Contém as APIs ou interfaces de usuário.
- **Tests**: Contém os testes de unidade para a camada de domínio.
 
### Como rodar o projeto com Docker

1. Certifique-se de ter o **Docker** instalado.
2. Rode o seguinte comando para construir e iniciar os contêineres:

   ```bash
   docker-compose up --build
3. A aplicação estará disponível em `http://localhost:5000/swagger/index.html`.

### Testes de Unidade
Foram implementados testes de unidade para garantir a qualidade do código e validar o comportamento das entidades. 
O framework utilizado para os testes é o xUnit. Os testes cobrem:

Validação de regras de negócio.
Verificação de integridade dos Value Objects (ex.: Address).
Executando os testes
Para executar os testes de unidade, utilize o comando:

```bash 
  dotnet test