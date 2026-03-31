using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A_Doninha_Encantada_Classes
{
    // --- CLASSE PAI ---
    public abstract class ProdutoRPG       
    {
        public string Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }

        public int Quantidade { get; private set; }

        public ProdutoRPG(string id, string nome, decimal preco, int quantidade)
        {
            // Validações para garantir que os dados do produto sejam válidos
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome do produto não pode ser vazio.", nameof(nome));   
            }

            if(preco < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(preco), "O preço do produto não pode ser negativo.");
            }
            
            if(string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "O ID do produto não pode ser vazio.");
            }

            Id = id;
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }

        // Método para atualizar o preço de um produto específico, garantindo que o novo preço seja válido
        public void AtualizarPreco(decimal novoPreco)
        {
            if (novoPreco < 0)
             throw new ArgumentOutOfRangeException(nameof(novoPreco), "O preço do produto não pode ser negativo.");
            
            Preco = novoPreco;
            Console.WriteLine($"O preço do produto {Nome} foi atualizado para {Preco:C}.");
        }

        // Método para repor o estoque de um produto, garantindo que a quantidade adicionada seja positiva
        public void ReporEstoque(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade a ser reabastecida deve ser maior que zero.");
            }

            Quantidade += quantidade;
            Console.WriteLine($"O estoque do produto {Nome} foi reposto. Quantidade atual: {Quantidade}.");
        }


        // Método para calcular o valor total de uma venda
        public virtual decimal CalcularVenda(int quantidadeVendida)
        {
             decimal totalVenda = quantidadeVendida * Preco;
             return totalVenda;
        }

        // Método para registrar uma venda, verificando se há estoque suficiente antes de processar a venda e atualizar o estoque
        // Retorna true se a venda foi registrada com sucesso, ou false se não há estoque suficiente
        public virtual bool RegistrarVenda(int quantidadeVendida)
        {
     
            if (quantidadeVendida <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantidadeVendida), "A quantidade vendida deve ser maior que zero.");
            }

            if (quantidadeVendida <= Quantidade)
            {
                Quantidade -= quantidadeVendida;
                decimal totalVenda = CalcularVenda(quantidadeVendida);
                Console.WriteLine($"Venda registrada: {quantidadeVendida} unidades de {Nome} vendidas por R${totalVenda}. Estoque restante: {Quantidade}.");
                return true;
            }
            else
            {
                Console.WriteLine($"Não há estoque suficiente para vender {quantidadeVendida} unidades de {Nome}. Estoque atual: {Quantidade}.");
                return false;
            }
            
        }

        // Método para exibir os detalhes de um produto
        public virtual string ExibirDetalhes()
        {
            return $"[{Id}] {Nome} - Preço: {Preco:C} - Estoque: {Quantidade}";
        }
    }

    // --- CLASSE DE VENDAS ---
    public class Venda
    {
        public int Id { get; private set; }
        public ProdutoRPG Produto { get; private set; }
        public int QuantidadeVendida { get; private set; }
        public decimal TotalVenda { get; private set; }
        public Venda(int id, ProdutoRPG produto, int quantidadeVendida, decimal totalVenda)
        {
            Id = id;
            Produto = produto;
            QuantidadeVendida = quantidadeVendida;
            TotalVenda = totalVenda;
        }

        // Método para exibir os detalhes de uma venda
        public string ExibirDetalhes()
        {
            return $"Venda {Id} Item: {Produto.Nome} - Quantidade: {QuantidadeVendida} - Total: {TotalVenda:C}";
        }
    }

    // --- CLASSES FILHAS ---

    public class Equipamento : ProdutoRPG
    {
        public string TipoEquipamento { get; private set; }
        public Equipamento(string id, string nome, decimal preco, int quantidade, string tipoEquipamento)
            : base(id, nome, preco, quantidade)
        {
            TipoEquipamento = tipoEquipamento;
        }

        // Sobrescreve o método ExibirDetalhes para incluir informações específicas do equipamento
        public override string ExibirDetalhes()
        {
            return base.ExibirDetalhes() + $" - Tipo: {TipoEquipamento} (Equipamento)";
        }

        // Sobrescreve o método CalcularVenda para aplicar uma taxa
        public override decimal CalcularVenda(int quantidadeVendida)
        {

            decimal totalVenda = base.CalcularVenda(quantidadeVendida);

            totalVenda *= 1.15m; // Aumenta o preço em 15% para equipamentos
            return totalVenda;
        }

        // Sobrescreve o método RegistrarVenda para informar que uma taxa foi aplicada para a compra de equipamentos
        public override bool RegistrarVenda(int quantidadeVendida)
        {
            bool sucesso = base.RegistrarVenda(quantidadeVendida);
            if (sucesso)
            {
                Console.WriteLine($"Frete de 15% aplicado na compra de equipamentos");
            }
            return sucesso;

        }
    }

    public class Consumivel : ProdutoRPG
    {
        public string Efeito { get; private set; }
        public Consumivel(string id, string nome, decimal preco, int quantidade, string efeito)
            : base(id, nome, preco, quantidade)
        {
            Efeito = efeito;
        }

        // Sobrescreve o método ExibirDetalhes para incluir informações específicas do consumível
        public override string ExibirDetalhes()
        {
            return base.ExibirDetalhes() + $" - Efeito: {Efeito} (Consumível)";
        }

        // Sobrescreve o método RegistrarVenda informando os Descontos aplicados
        public override bool RegistrarVenda(int quantidadeVendida)
        {
            bool sucesso = base.RegistrarVenda(quantidadeVendida);
            if (sucesso)
            {
                if (quantidadeVendida >= 3 && quantidadeVendida < 20)
                {
                    Console.WriteLine($"Desconto aplicado: 20% em compras acima de 3 unidades.");
                }
                if (quantidadeVendida >= 20)
                {
                    Console.WriteLine($"Desconto aplicado: 35% em compras acima de 20 unidades.");
                }
            }
            return sucesso;

        }

        // Sobrescreve o método CalcularVenda para aplicar um descontos
        public override decimal CalcularVenda(int quantidadeVendida)
        {
            decimal totalVenda = base.CalcularVenda(quantidadeVendida);
            if (quantidadeVendida >= 3 && quantidadeVendida < 20)
            {
                totalVenda *= 0.8m; // Reduz o preço em 20% para consumíveis
            }
            if (quantidadeVendida >= 20)
            {
                totalVenda *= 0.65m; // Reduz o preço em 35% para compras acima de 20 unidades de consumíveis
            }

            return totalVenda;
        }
    }
}