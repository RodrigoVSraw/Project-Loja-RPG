using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using System.Threading.Tasks;
using Project_A_Doninha_Encantada_Classes;

namespace A_Doninha_Encantada_Painel
{ 
    internal class Program
    {

        static void Main(string[] args)
        {
            // Instância da classe "ProdutoRPG" para armazenar os produtos do estoque
            List<ProdutoRPG> produtoEstoque = new List<ProdutoRPG>(); 
            List<Venda> vendasRealizadas = new List<Venda>();


            // --- Itens para testar na loja, se quiser usar, selecionar todos com o mouse e apertar (Ctrl + ;) ---

            // --- EQUIPAMENTOS ---
            produtoEstoque.Add(new Equipamento("EQ01", "Espada Longa de Ferro", 150.00m, 75, "Arma Corpo-a-Corpo"));
            produtoEstoque.Add(new Equipamento("EQ02", "Escudo de Carvalho Anfíbio", 45.00m, 112, "Defesa Mão Secundária"));
            produtoEstoque.Add(new Equipamento("EQ03", "Armadura Escamas de Dragão", 1250.00m, 92, "Armadura Pesada"));
            produtoEstoque.Add(new Equipamento("EQ04", "Cajado da Estrela Cadente", 320.50m, 81, "Arma Mágica"));
            produtoEstoque.Add(new Equipamento("EQ05", "Botas Aladas de Hermes", 150.00m, 0, "Acessório Mágico"));

            // --- CONSUMÍVEIS ---
            produtoEstoque.Add(new Consumivel("CS01", "Poção de Cura Menor", 25.50m, 30, "Cura 50 HP"));
            produtoEstoque.Add(new Consumivel("CS02", "Elixir da Invisibilidade", 200.00m, 30, "Invisibilidade por 3 turnos"));
            produtoEstoque.Add(new Consumivel("CS03", "Pergaminho: Bola de Fogo", 120.00m, 50, "Dano em Área (Fogo)"));
            produtoEstoque.Add(new Consumivel("CS04", "Ração de Viagem Elfica", 5.50m, 50, "Cura 10 HP e remove Fome"));
            produtoEstoque.Add(new Consumivel("CS05", "Lágrima de Fênix", 5000.00m, 10, "Ressurreição Completa"));

            int opcoes;
            do
            {
                // Painel de opções para o usuário escolher entre as funcionalidades do sistema
                Console.Clear();
                Console.WriteLine("====================================");  
                Console.WriteLine("       A Doninha Encantada RPG        ");
                Console.WriteLine("====================================");
                Console.WriteLine("Bem-vindo à Doninha Encantada!");
                Console.WriteLine("1. Gestão de Estoque");
                Console.WriteLine("2. Controle de Vendas");
                Console.WriteLine("3. Relatórios");
                Console.WriteLine("4. Sair");
                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcoes))
                {
                    opcoes = 0; // Força cair no default do switch para opções inválidas
                }

                switch (opcoes)
                {
                    case 1:   // Gestão de Estoque (cadastro de itens, atualização de preços, e reposição de estoque)  
                        Console.Clear();
                        Console.WriteLine("--- Gestão de Estoque ---");
                        Console.WriteLine("1. Cadastrar Novo Equipamento");
                        Console.WriteLine("2. Cadastrar Novo Consumível");
                        Console.WriteLine("3. Repor Estoque de item existente");
                        Console.WriteLine("4. Atualizar preço de item existente");
                        Console.Write("\nEscolha: ");

                        string escolha = Console.ReadLine();

                        // Lógica para cadastro de novos itens ou reposição de estoque
                        try
                        {
                            if (escolha == "1" || escolha == "2") // Cadastro de novos itens (equipamentos ou consumíveis)
                            {
                                Console.Clear();
                                Console.Write("Digite um Id para o item: "); string id = Console.ReadLine();
                                Console.Write("Digite um nome para o item: "); string nome = Console.ReadLine();
                                Console.Write("Defina o preço do item: "); decimal preco = decimal.Parse(Console.ReadLine());
                                Console.Write("Defina uma quantidade inicial para o item: "); int quantidade = int.Parse(Console.ReadLine());

                                if (escolha == "1") // Cadastro de Equipamento  
                                {
                                    Console.Write("Escolha qual será o tipo de equipamento: "); string tipoEquipamento = Console.ReadLine();
                                    produtoEstoque.Add(new Equipamento(id, nome, preco, quantidade, tipoEquipamento));
                                    Console.WriteLine("\nEquipamento cadastrado com sucesso!");
                                }
                                else // Cadastro de Consumível
                                {
                                    Console.Write("Escolha qual será o efeito do consumível: "); string efeito = Console.ReadLine();
                                    produtoEstoque.Add(new Consumivel(id, nome, preco, quantidade, efeito));
                                    Console.WriteLine("\nConsumível cadastrado com sucesso!");
                                }
                            }
                            else if (escolha == "3") // Reposição de estoque para um item existente
                            {
                                Console.Clear();
                                Console.Write("Digite o ID do item que deseja repor: ");
                                string idRepor = Console.ReadLine();

                                ProdutoRPG produtoRepor = produtoEstoque.Find(p => p.Id == idRepor);

                                if (produtoRepor != null)
                                {
                                    Console.Write("Digite a quantidade a ser reposta: "); int quantidadeRepor = int.Parse(Console.ReadLine());
                                    produtoRepor.ReporEstoque(quantidadeRepor);
                                    Console.WriteLine("\nEstoque atualizado com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("Produto não encontrado no estoque...");
                                }
                            }
                            else if (escolha == "4") // Atualização de preço para um item existente
                            {
                                Console.Clear();
                                Console.Write("Digite o ID do item que deseja atualizar o preço: ");
                                string idAtualizar = Console.ReadLine();
                                ProdutoRPG produtoAtualizar = produtoEstoque.Find(p => p.Id == idAtualizar);

                                if (produtoAtualizar != null)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Preço atual do item {produtoAtualizar.Nome}: {produtoAtualizar.Preco:C}");
                                    Console.Write("Digite o novo preço do item: "); decimal novoPreco = decimal.Parse(Console.ReadLine());
                                    produtoAtualizar.AtualizarPreco(novoPreco);
                                    Console.WriteLine("\nPreço atualizado com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("Produto não encontrado no estoque...");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Opção inválida. Retornando ao menu principal...");
                            }

                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine($"Ocorreu um erro: {ex.Message}. Operação cancelada.");
                        }
                        Console.WriteLine("\n------------------------------------");
                        Console.WriteLine("Pressione qualquer tecla outra tecla para voltar...");
                        Console.ReadKey(); 
                        break;

                    case 2:  // Controle de Vendas (venda de itens, cálculo de totais, e registro de vendas)
                        Console.Clear();
                        Console.WriteLine("--- Controle de Vendas ---");
                        Console.WriteLine("Tipo de itens para venda:");
                        Console.WriteLine("-Equipamentos ID(EQ00)-");
                        Console.WriteLine("-Consumíveis ID(CS00)-");
                        Console.Write("\nDigite o ID do item que deseja vender: ");

                        // Lógica para buscar o produto no estoque usando "LINQ"
                        string idVenda = Console.ReadLine();
                        ProdutoRPG produtoVenda = produtoEstoque.Find(p => p.Id == idVenda);

                        
                        if(produtoVenda != null) // Produto encontrado no estoque, prosseguir com a venda
                        {
                            Console.Clear();
                            Console.WriteLine($"Produto encontrado: {produtoVenda.Nome} | Estoque Atual: {produtoVenda.Quantidade} | Preço: {produtoVenda.Preco:C}");

                            if (produtoVenda.Quantidade <= 0) // Verifica se o produto está esgotado
                            {
                                Console.WriteLine("Produto esgotado. Não é possível realizar a venda.");
                                Console.WriteLine("\n------------------------------------");
                                Console.WriteLine("Pressione qualquer tecla para continuar...");
                                Console.ReadKey();
                                break; // Sai do case para retornar ao menu principal
                            }
                            Console.Write("Quantidade a vender: ");

                            if (int.TryParse(Console.ReadLine(), out int quantidadeVenda)) // Verifica se a quantidade digitada é um número válido
                            {
                                if (produtoVenda.RegistrarVenda(quantidadeVenda))// Verifica se a venda é valida (quantidade disponível no estoque) e registra a venda
                                {
                                    decimal totalVenda = produtoVenda.CalcularVenda(quantidadeVenda); 
                                    int idVendaRegistro = vendasRealizadas.Any() ? vendasRealizadas.Max(v => v.Id) + 1 : 1; // Gerar um ID sequencial para a venda
                                    vendasRealizadas.Add(new Venda(idVendaRegistro, produtoVenda, quantidadeVenda, totalVenda));
                                }
                                
                            }
                            else // Quantidade com números inválidos, cancela a operação
                            {
                                Console.WriteLine("Quantidade inválida. Operação cancelada.");
                            }

                        }
                        else // Produto não encontrado no estoque, exibe mensagem de erro
                        {
                            Console.WriteLine("Produto não encontrado no estoque...");
                        }
                        Console.WriteLine("\n------------------------------------");
                        Console.WriteLine("Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case 3:  // Relatórios (relatório de vendas, relatório de estoque, e fechamento de caixa)
                        Console.Clear();
                        Console.WriteLine("--- Relatórios ---");
                        Console.WriteLine("1. Relatório de Estoque");
                        Console.WriteLine("2. Relatório de Vendas");
                        Console.WriteLine("3. Fechamento de Caixa");
                        Console.Write("\nEscolha: ");

                        if (!int.TryParse(Console.ReadLine(), out int escolhaRelatorio))
                        {
                            Console.WriteLine("Opção inválida. Retornando ao menu principal...");
                            escolhaRelatorio = default; // Sai do switch para retornar ao menu principal
                        }
                        else
                        {
                            switch (escolhaRelatorio)
                            {
                                case 1:
                                    Console.Clear();
                                    Console.WriteLine("--- Relatório de Estoque ---\n");
                                    // Lógica para gerar o relatório de estoque usando "LINQ"
                                    if (!produtoEstoque.Any())
                                    {
                                        Console.WriteLine("O estoque está vazio...");
                                    }
                                    else
                                    {
                                        produtoEstoque.Where(pe => pe.Quantidade > 0)
                                        .OrderByDescending(pe => pe.Preco)
                                        .ToList()
                                        .ForEach(pe => Console.WriteLine(pe.ExibirDetalhes()));
                                    }
                                    break;

                                case 2:
                                    Console.Clear();
                                    Console.WriteLine("--- Relatório de Vendas ---\n");
                                    // Lógica para gerar o relatório de vendas usando "LINQ"
                                    if (!vendasRealizadas.Any())
                                    {
                                        Console.WriteLine("Nenhuma venda realizada ainda...");
                                    }
                                    else
                                    {
                                        vendasRealizadas.ForEach(vr => Console.WriteLine(vr.ExibirDetalhes()));
                                    }
                                    break;

                                case 3:
                                    Console.Clear();
                                    Console.WriteLine("--- Fechamento de Caixa ---\n");
                                    // Lógica para calcular o total do caixa usando "LINQ"
                                    decimal totalCaixa = vendasRealizadas.Sum(v => v.TotalVenda);
                                    Console.WriteLine($"Total do Caixa: {totalCaixa:C}");
                                    break;

                                default:
                                    Console.WriteLine("Opção inválida. Retornando ao menu principal...");
                                    break;
                            }
                        }
                        Console.WriteLine("\n------------------------------------");
                        Console.WriteLine("Pressione qualquer tecla para voltar...");
                        Console.ReadKey();
                        break;
                    case 4: // Sair do sistema
                        Console.WriteLine("\nSaindo do sistema. Obrigado por usar a Doninha Encantada!");
                        break;  

                    default: // Opção inválida
                        Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                        break;
                }


            }while (opcoes != 4);

        }
    }
}   