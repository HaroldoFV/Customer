# Sistema de Cadastro de Clientes e Endereços

Este projeto é um **Sistema para Cadastro de Clientes e Endereços**, desenvolvido utilizando **.NET 8**, seguindo os princípios de **Domain-Driven Design (DDD)** e **Arquitetura Limpa**. O sistema foi containerizado utilizando **Docker**, e o banco de dados utilizado é o **MSSQL Server**. Além disso, foram implementados **testes de unidade** para garantir a qualidade do código e das funcionalidades.

## Tecnologias Utilizadas

- **.NET 8**
- **Domain-Driven Design (DDD)**
- **Arquitetura Limpa**
- **MSSQL Server**
- **Docker**
- **Testes de Unidade (xUnit)**
- **Angular 11**: Framework de desenvolvimento para a criação de aplicações web frontend.

## Estrutura do Projeto(backend)

A aplicação segue a **Arquitetura Limpa**, separando claramente as responsabilidades em camadas, permitindo maior flexibilidade e facilidade de manutenção. A estrutura básica do projeto é a seguinte:

- **Domain**: Contém as entidades e objetos de valor, que representam o núcleo do domínio da aplicação.
- **Application**: Contém os serviços de aplicação, interfaces e casos de uso.
- **Infrastructure**: Implementa a persistência de dados (com repositórios para o MSSQL Server), integração com serviços externos, etc.
- **Presentation**: Contém as APIs ou interfaces de usuário.
- **Tests**: Contém os testes de unidade para a camada de domínio.

### Como rodar o projeto com Docker

1. Certifique-se de ter o **Docker** instalado.
2. Clone o repositório do projeto para sua máquina local.
3. Navegue até a pasta raiz do projeto, onde está localizado o arquivo `docker-compose.yml`.
4. Execute o seguinte comando para construir e iniciar os contêineres do serviço, do banco de dados e do frontend:

    ```bash
    docker-compose up --build
    ```

4. Após a inicialização dos containers, a aplicação(frontend, backend) estará acessível através do navegador 
ou cliente de API no seguinte endereços respectivamente:

    ```
    http://localhost:4200
    http://localhost:5000/swagger/index.html
    ```
 
### Testes de Unidade
Foram implementados testes de unidade para garantir a qualidade do código e validar o comportamento das entidades. 
O framework utilizado para os testes é o xUnit. Os testes cobrem:

Validação de regras de negócio.
Verificação de integridade dos Value Objects (ex.: Address).
Executando os testes
Para executar os testes de unidade, utilize o comando:

```bash 
  dotnet test
```

# Alternativa em caso de problemas ao rodar o SQL Server no Docker

Caso não seja possível rodar o banco de dados **SQL Server** no Docker, é possível seguir os passos abaixo para rodar o banco de forma local:

1. **Comentar o container do SQL Server no `docker-compose.yml`**:
   
   Comente ou remova a seção `customer.db` no arquivo `docker-compose.yml` para evitar que o Docker tente criar o container do SQL Server.

   Exemplo:
   ```yaml
   # customer.db:
   #   image: mcr.microsoft.com/mssql/server:2019-latest
   #   container_name: customer_db
   #   ports:
   #     - "1433:1433"
   #   environment:
   #     - ACCEPT_EULA=Y
   #     - SA_PASSWORD=ComplexPass123!
   #     - MSSQL_PID=Express
   #   volumes:
   #     - ./src/sql:/docker-entrypoint-initdb.d
   #     - sqlserver_data:/var/opt/mssql
   #   networks:
   #     - customer_network
   #   restart: on-failure
   #   command: >
   #     /bin/bash -c "
   #     /opt/mssql/bin/sqlservr --accept-eula &
   #     pid=$!
   #     sleep 30s && 
   #     /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P 'ComplexPass123!' -i /docker-entrypoint-initdb.d/init-db.sql -C;
   #     wait $pid   
   #     "
   ```
   
2 .Atualizar o arquivo appsettings.Development.json: No projeto da API, altere o valor da chave ConnectionStrings:DefaultConnection para apontar para o SQL Server local (localhost). Exemplo de alteração no arquivo appsettings.Development.json:
    ```json
        {
        "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=CustomerDb;User Id=sa;Password=ComplexPass123!"
        }
        }
    ```
    
3. Executar o script init-db.sql manualmente: O arquivo de inicialização do banco de dados (init-db.sql) pode ser encontrado no diretório src/sql. Você deve executá-lo manualmente no seu SQL Server local para criar o banco e suas tabelas.
