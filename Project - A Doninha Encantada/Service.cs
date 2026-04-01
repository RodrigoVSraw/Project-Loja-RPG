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

            if (preco < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(preco), "O preço do produto não pode ser negativo.");
            }

            if (string.IsNullOrWhiteSpace(id))
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

    public class GerenciaLists
    {
        private List<ProdutoRPG> produtoEstoque;
        private List<Venda> vendasRealizadas;

        public GerenciaLists()
        {
            produtoEstoque = new List<ProdutoRPG>();
            vendasRealizadas = new List<Venda>();
            CarregarProdutosIniciais();
        }

        public void CarregarProdutosIniciais()
        {
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
        }

        public void CadastrarEquipamento(string id, string nome, decimal preco, int quantidade, string tipoEquipamento)
        {
            produtoEstoque.Add(new Equipamento(id, nome, preco, quantidade, tipoEquipamento));
        }

        public void CadastrarConsumivel(string id, string nome, decimal preco, int quantidade, string efeito)
        {
            produtoEstoque.Add(new Consumivel(id, nome, preco, quantidade, efeito));
        }

        public ProdutoRPG BuscarProdutoPorId(string id)
        {
            return produtoEstoque.Find(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public void EfetuarVenda(ProdutoRPG produto, int quantidadeVendida)
        {
            if (produto.RegistrarVenda(quantidadeVendida))// Verifica se a venda é valida (quantidade disponível no estoque) e registra a venda
            {
                decimal totalVenda = produto.CalcularVenda(quantidadeVendida);
                int idVendaRegistro = vendasRealizadas.Any() ? vendasRealizadas.Max(v => v.Id) + 1 : 1; // Gerar um ID sequencial para a venda
                vendasRealizadas.Add(new Venda(idVendaRegistro, produto, quantidadeVendida, totalVenda));
            }
        }

        public IEnumerable<ProdutoRPG> ObterEstoque()
        {
            return produtoEstoque.Where(pe => pe.Quantidade > 0)
             .OrderByDescending(pe => pe.Preco); 
        }

        public IEnumerable<Venda> ObterVendas()
        {
            return vendasRealizadas;
        }

        public decimal CalcularTotalCaixa()
        {
            return vendasRealizadas.Sum(v => v.TotalVenda);
        }
    }
}