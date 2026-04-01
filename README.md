# 🧙‍♂️ A Doninha Encantada RPG - Sistema de Gerenciamento

Bem-vindo ao repositório do sistema **A Doninha Encantada**, uma aplicação de console desenvolvida em C# para gerenciar o estoque, as vendas e o caixa de uma loja de itens mágicos e equipamentos de RPG.

## 📋 Sobre o Projeto
Este sistema foi projetado com foco em boas práticas de Engenharia de Software, aplicando rigorosamente os pilares da **Programação Orientada a Objetos (POO)** e os princípios de **Arquitetura Limpa (Clean Architecture)**. A interface de usuário (Console) foi separada da regra de negócios (Services), garantindo um código altamente coeso, de fácil manutenção e escalável.

## ✨ Funcionalidades
* **Gestão de Estoque:** Cadastro de novos Equipamentos e Consumíveis, reposição de estoque e atualização de preços.
* **Controle de Vendas:** Validação inteligente de estoque e aplicação automática de regras de negócio (taxas de ferreiro para equipamentos e descontos por volume para consumíveis).
* **Relatórios Dinâmicos:** Geração de relatórios de estoque e histórico de vendas utilizando consultas avançadas com LINQ.
* **Fechamento de Caixa:** Cálculo automatizado do faturamento total da loja.

## 🚀 Tecnologias e Conceitos Aplicados
* **Linguagem:** C# (Console Application)
* **Programação Orientada a Objetos (POO):**
  * **Herança & Polimorfismo:** Classe base abstrata (`ProdutoRPG`) com comportamentos específicos sobrescritos (`override`) nas classes filhas (`Equipamento` e `Consumivel`).
  * **Encapsulamento Forte:** Utilização de `private set` nas propriedades de domínio, garantindo que o estado dos objetos só possa ser alterado através de métodos controlados.
* **Arquitetura Limpa:** Separação clara de responsabilidades entre a camada de apresentação (`Program.cs`) e a camada de serviço (`Service.cs`).
* **Manipulação de Dados (LINQ):** Uso fluente de expressões LINQ (`Where`, `Any`, `Sum`, `OrderByDescending`, `Max`) para filtragem e cálculos em listas genéricas (`List<T>`).
* **Tratamento de Exceções (Fail-Fast):** Validações robustas de entrada nos construtores e métodos usando `throw new ArgumentException`, `ArgumentOutOfRangeException` e blocos `try/catch`.

## ⚙️ Como Executar
1. Clone este repositório ou faça o download dos arquivos `.cs`.
2. Abra o projeto no Visual Studio ou VS Code.
3. Execute o projeto no Visual Studio.

## 👨‍💻 Desenvolvedor
* **Rodrigo Vieira Santos Raw**
