# LivroReceita - SPSKILLS

Projeto desenvolvido para a competição SPSKILLS na modalidade **Desenvolvimento de Aplicativos de Software**. A aplicação consiste em um sistema desktop para gerenciamento de receitas culinárias, desenvolvido sob restrições de tempo e requisitos operacionais.

## Condições da Prova

- **Tempo máximo de execução:** 2 horas e 30 minutos
- **Escopo:** Interface gráfica, autenticação, lógica de negócio e integração com banco de dados
- **Modalidade:** Desenvolvimento de Aplicativos de Software
- **Módulo:** Aplicação Windows Forms

## Tecnologias Utilizadas

- C#
- .NET Framework 4.8
- Windows Forms (WinForms)
- Entity Framework 6 (Database First)
- SQL Server

## Funcionalidades Implementadas

- Tela de introdução e validação de primeiro acesso do usuário
- Sistema de autenticação
- Mapeamento e persistência de dados com Entity Framework
- Interface gráfica baseada nos requisitos de layout da prova

## Estrutura do Projeto

- **LivroReceita.Data:** Mapeamento de dados e contexto do Entity Framework
- **LivroReceita.Views:** Formulários da interface do usuário, incluindo Login, Introdução e navegação principal
- **App.config:** Configurações da aplicação e string de conexão com o banco de dados

## Como Executar

1. Certifique-se de que o SQL Server esteja instalado e em execução na máquina local.
2. Certifique-se de que a base de dados `dbLivroReceita` esteja disponível na instância do SQL Server.
3. Abra a solução `LivroReceita.sln` no Visual Studio.
4. Caso sua instância local do SQL Server utilize um nome diferente, ajuste o parâmetro `data source` dentro da tag `<connectionStrings>` no arquivo `App.config`.
5. Compile a solução em modo **Debug** ou **Release** utilizando `Ctrl + Shift + B`.
6. Pressione `F5` para executar a aplicação a partir do Visual Studio.
7. As instruções para utilização da solução e simulação de possíveis cenários estão descritas no README localizado dentro da pasta do projeto `LivroReceita`.
