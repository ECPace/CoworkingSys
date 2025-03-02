<h1> Sistema de Reservas para Coworking </h1>

Este é um sistema de gerenciamento de reservas para espaços de coworking, desenvolvido em ASP.NET Core. Ele permite cadastrar e gerenciar clientes, funcionários, salas e reservas, calculando automaticamente o valor da reserva com base na duração e no preço da sala. O sistema inclui uma funcionalidade de mapa para selecionar a localização das salas e faz uso de relacionamentos entre modelos para uma estrutura robusta de dados.

<h2>Funcionalidades:</h2>

Cadastro de Clientes, Salas e Funcionários. <br>
Criação de Reservas com cálculo automático do valor total. <br>
Visualização da localização da Sala em um mapa interativo. <br>
Relacionamentos entre entidades, como Cliente, Sala e Reserva. <br>
Persistência de dados em banco relacional e interface intuitiva. 

<h2>Tecnologias Utilizadas:</h2>

ASP.NET Core <br>
Entity Framework Core <br>
Leaflet para API de Mapa <br>
SQL Server para armazenamento de dados

<h2>Requisitos:</h2>

.NET SDK 6.0 ou superior <br>
SQL Server <br>
Node.js e npm (opcional, para ferramentas frontend)

<h2>Instruções de Instalação:</h2>

<h3>Clonar o Repositório</h3>
Para começar, clone o repositório em sua máquina local: <br>
git clone https://github.com/ECPace/CoworkingSys.git  <br>
<h3>Configurar o Banco de Dados</h3>
Crie o banco de dados utilizando o seguinte código no Console de Gerenciador de Pacotes:  <br>
update-database  <br>
Execute o projeto com a opção "Iniciar sem depurar" do Visual Studio.

<h2>Estrutura do Projeto:</h2>

Controllers/ - Contém os controladores para gerenciar rotas e lógica de aplicação. <br>
Models/ - Define as classes e modelos de dados. <br>
Views/ - Contém as views para interação com o usuário. <br>
wwwroot/ - Recursos estáticos, como arquivos de CSS, JS, e mapas.
