# APSNET - API REST - Padrão de Início de Projeto

Bem-vindo ao APSNET, um modelo de projeto para APIs REST. Este template oferece uma abordagem estruturada para iniciar novos projetos de API com uma arquitetura consistente e funcionalidades comuns já implementadas. Abaixo está uma visão geral da estrutura do projeto e dos componentes principais.

Estrutura do Projeto
O projeto APSNET é dividido em vários componentes principais, cada um com um propósito distinto:

APSNET.API: O projeto principal da API que define os controladores e os endpoints.
APSNET.Aplicacao: Centraliza os métodos para consultas no banco de dados.
APSNET.Dominio: Contém os objetos de domínio utilizados em todo o projeto.
APSNET.Repositorio: Responsável pela conexão com o banco de dados e coleta de informações das tabelas.
APSNET.Tools: Centraliza ferramentas utilizadas em todos os projetos.
Detalhes dos Componentes

### APSNET.API
Este componente define a estrutura da API, incluindo os controladores que expõem os endpoints para as operações CRUD (Create, Read, Update, Delete). Utiliza o ASP.NET Core para a construção dos endpoints.

### APSNET.Aplicacao
Centraliza a lógica de negócios e métodos de consulta ao banco de dados. Este componente age como uma camada intermediária entre os controladores e o repositório, garantindo que a lógica de negócios seja separada da lógica de acesso a dados.

### APSNET.Dominio
Contém as classes de domínio que representam as entidades do negócio. Esses objetos são utilizados em todo o projeto, garantindo que todos os componentes trabalhem com uma representação consistente dos dados.

### APSNET.Repositorio
Responsável por gerenciar a conexão com o banco de dados e executar operações de leitura e escrita. Implementa padrões de repositório para facilitar a separação de preocupações e a manutenção do código. Neste componente, você também define onde a aplicação irá conectar no banco de dados:

this.utilizandoConexao = true;
this.DB = "api_teste";
this.Server = "localhost";
this.Username = "root";
this.Password = "caara511";


### APSNET.Tools
Centraliza utilitários e ferramentas que são usados em todo o projeto, como métodos de extensão, helpers, e outras funcionalidades auxiliares que podem ser reutilizadas em diferentes partes da aplicação.

### Como Usar
Para iniciar um novo projeto de API usando o template APSNET:

Clone o Repositório: Faça o clone deste repositório para sua máquina local.
Configure o Banco de Dados: Ajuste as configurações de conexão com o banco de dados no componente APSNET.Repositorio.
Defina seus Domínios: Adicione ou modifique as classes de domínio no componente APSNET.Dominio conforme necessário.
Implemente sua Lógica de Negócio: Adicione ou ajuste a lógica de negócios no componente APSNET.Aplicacao.
Configure os Endpoints: Defina ou ajuste os controladores e endpoints no componente APSNET.API.
Contribuições
Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou enviar pull requests com melhorias, correções de bugs ou novas funcionalidades.

### Licença
Este projeto é licenciado sob a MIT License. Consulte o arquivo LICENSE para obter mais informações.
