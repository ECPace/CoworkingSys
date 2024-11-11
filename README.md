Sistema de Reservas para Coworking

Este é um sistema de gerenciamento de reservas para espaços de coworking, desenvolvido em ASP.NET Core. Ele permite cadastrar e gerenciar clientes, funcionários, salas e reservas, calculando automaticamente o valor da reserva com base na duração e no preço da sala. O sistema inclui uma funcionalidade de mapa para selecionar a localização das salas e faz uso de relacionamentos entre modelos para uma estrutura robusta de dados.

Funcionalidades:

Cadastro de Clientes, Salas e Funcionários. |
Criação de Reservas com cálculo automático do valor total. |
Visualização da localização da Sala em um mapa interativo. |
Relacionamentos entre entidades, como Cliente, Sala e Reserva. |
Persistência de dados em banco relacional e interface intuitiva. |

Tecnologias Utilizadas:

ASP.NET Core |
Entity Framework Core |
Leaflet para API de Mapa |
SQL Server para armazenamento de dados

Requisitos:

.NET SDK 6.0 ou superior |
SQL Server |
Node.js e npm (opcional, para ferramentas frontend)

Instruções de Instalação:

Clonar o Repositório
Para começar, clone o repositório em sua máquina local: |
git clone https://github.com/Pardalzovisk/CoworkingSys.git  |
cd projeto-reserva-coworking  |
Configurar o Banco de Dados  ||
Crie o banco de dados e aplique as migrações:  |
update-database  |
Execute o projeto com a opção "Iniciar sem depurar" do Visual Studio.  |

Estrutura do Projeto:

Controllers/ - Contém os controladores para gerenciar rotas e lógica de aplicação. |
Models/ - Define as classes e modelos de dados. |
Views/ - Contém as views para interação com o usuário. |
wwwroot/ - Recursos estáticos, como arquivos de CSS, JS, e mapas.
