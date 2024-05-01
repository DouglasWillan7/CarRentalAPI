# README - API de Gerenciamento de Aluguel de Motos e Entregadores

Este é o repositório da API de gerenciamento de aluguel de motos e entregadores. A API foi desenvolvida utilizando .NET 8 e Docker Compose para facilitar a execução e implantação em diferentes ambientes.

## Funcionalidades Principais

- Cadastro de motos
- Consulta, filtragem e modificação de motos
- Remoção de motos
- Cadastro de entregadores
- Atualização de cadastro de entregadores para envio de foto de CNH
- Aluguel de motos por período

## Configuração e Execução

### Pré-requisitos

- Docker
- Docker Compose
- .NET 8 SDK

### Instruções

1. Clone este repositório em sua máquina local
2. Navegue até o diretório do projeto
3. Execute o Docker Compose para iniciar a aplicação
```
docker-compose up -d
```

4. Acesse a API através do endpoint especificado na saída do Docker Compose.
  ```
  http://localhost:5001/swagger/index.html
  ```
5. Utilize a collection para postman para facilitar a intereção com a api [Collection Postman](https://www.icloud.com/iclouddrive/018LR2-bbcbV1VlU_4m_P-U1Q#Rental_API.postman%5Fcollection)
6. Para acessar a base de dados utilize o SGBD da sua preferencia com o endereço `localhost:5432` (configurada no docker-compose.yml) com o usuario `postgres` e senha `postgres`
7. Para acessar o bucket de imagens acesse o endereço `localhost:9000` com o usuario `minio_user` e a senha `minio_password`

## Casos de Uso

Aqui estão os principais casos de uso da API:

### Admin

1. **Login:**
- Envia um requisição `POST` para rota `/api/v1/login`
- Usuário Admin: `username: admin - password: admin`

2. **Cadastrar uma nova moto:**
- Envie uma solicitação `POST` para `/api/v1/motorcycle` com os dados da moto no corpo da requisição.

2. **Consultar e filtrar motos:**
- Envie uma solicitação `GET` para `/api/v1/motorcycle/{{placa}}` para obter todas as motos.
- Utilize o parâmetro de consulta `placa` para filtrar por placa.

3. **Modificar a placa de uma moto:**
- Envie uma solicitação `PUT` para `/api/motos/{{placa}}` com os dados atualizados da moto.

4. **Remover uma moto:**
- Envie uma solicitação `DELETE` para `/api/v1/motorcycle/{{placa}}` para remover uma moto.

### Entregador

1. **Login:**
- Envia um requisição para rota `/api/v1/login`
- Usuário Deliveryman: `username: douglas - password: 123456`

1. **Cadastro na plataforma:**
- Envie uma solicitação POST para `/api/v1/deliveryman` com os dados do entregador no corpo da requisição.

2. **Atualizar cadastro com foto de CNH:**
- Envie uma solicitação PUT para `/api/v1/deliveryman/{{deliveryman_id}}/photo` com a foto da CNH anexada.

3. **Alugar uma moto:**
- Envie uma solicitação POST para `/api/v1/rental` com os detalhes do aluguel no corpo da requisição.

## Observações

- O serviço de mensageria para notificações está integrado na aplicação utilizando RabbitMq com MassTransient.
- O armazenamento da foto da CNH esta configurado utilizando MinIO.

Para mais detalhes sobre as rotas disponíveis e seus parâmetros, consulte a documentação da API.
