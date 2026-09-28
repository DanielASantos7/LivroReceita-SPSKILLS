# LivroReceita - SPSKILLS

Projeto desenvolvido para a competição SPSKILLS na modalidade Desenvolvimento de Aplicativos de Software. A aplicação consiste em um sistema desktop para gerenciamento de receitas culinárias, construído sob restrições de tempo e requisitos operacionais.

## Condições da Prova

- Tempo máximo de execução: 2 horas e 30 minutos
- Escopo: Interface gráfica, autenticação, lógica de negócio e integração com banco de dados
- Modalidade: Desenvolvimento de Aplicativos de Software (Módulo: Aplicação Windowns Forms)

## Tecnologias Utilizadas

- C# / .NET Framework 4.8
- Windows Forms (WinForms)
- Entity Framework 6 (Database First)
- SQL Server

## Funcionalidades Implementadas

- Tela de introdução e validação de primeiro acesso do usuário
- Sistema de autenticação (Login)
- Mapeamento e persistência de dados via Entity Framework
- Interface gráfica baseada em requisitos de layout da prova

## Estrutura do Projeto

- LivroReceita.Data: Mapeamento de dados e contexto do Entity Framework
- LivroReceita.Views: Formulários da interface do usuário (Login, Introdução e navegação principal)
- App.config: Configurações da aplicação e string de conexão com o banco de dados

## Como Executar

1. Certifique-se de ter o SQL Server instalado e em execução na máquina local.
2. Certifique-se de que a base de dados `dbLivroReceita` está disponível na sua instância do SQL Server.
3. Abra a solução `LivroReceita.sln` no Visual Studio.
4. Caso a sua instância local do SQL Server utilize um nome diferente, ajuste o parâmetro `data source` dentro da tag `<connectionStrings>` no arquivo `App.config`.
5. Compile a solução em modo Debug ou Release (`Ctrl + Shift + B`).
6. Pressione `F5` para executar a aplicação a partir do Visual Studio.
7. As funcionalidades da aplicação, como utilizar a solução e simular possiveis cenários estão descritas no README dentro da pasta do projeto "LivroReceita".