# API de Agendamento de Serviços

## ✅ Pré-requisitos
Para executar este projeto, você precisa ter instalado apenas:
- Docker Desktop (que já inclui o Docker Compose)
- Git

## 🚀 Como Executar

1. Clone o Repositório
Abra seu terminal e clone o projeto do GitHub:
```Bash
git clone https://github.com/GuuilhermeRS/AgendamentoServicos.git
```

2. Navegue até a Pasta do Projeto
```Bash
cd AgendamentoServicos
```

3. Inicie a Aplicação com Docker Compose

Execute o seguinte comando na raiz do projeto. Ele irá construir a imagem da API e iniciar os containers da API e do banco de dados em segundo plano.
```Bash
docker-compose up --build -d
```

A primeira execução pode demorar alguns minutos, pois o Docker precisará baixar as imagens base do .NET e do MySQL. As execuções seguintes serão muito mais rápidas.

4. Verifique se Tudo Está Funcionando

5. Após alguns segundos, execute o comando abaixo para ver o status dos containers:
```Bash
docker-compose ps
```

Você deverá ver os dois serviços, api_agendamento e mysql_agendamento, com o status Up ou Running.

Pronto! A API está no ar.

## 📖 Endpoints da API (Swagger)

Com a aplicação em execução, a documentação interativa da API (Swagger UI) está disponível no seu navegador. Através dela, você pode ver todos os endpoints, seus parâmetros, e até mesmo testá-los diretamente.

Acesse a documentação em: http://localhost:8080/swagger

🛑 Como Parar a Aplicação

Para parar todos os containers relacionados ao projeto, execute o seguinte comando na raiz do projeto:
```Bash
docker-compose down
```