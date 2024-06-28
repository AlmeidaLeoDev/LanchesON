using LanchesMac.Context; // Importa o namespace que contém o contexto do banco de dados
using Microsoft.EntityFrameworkCore; // Importa o namespace para funcionalidades do Entity Framework Core

namespace LanchesMac.Models // Define um namespace para organizar as classes do projeto
{
    public class CarrinhoCompra // Define a classe CarrinhoCompra
    {
        private readonly AppDbContext _context; // Declara uma variável privada somente leitura para o contexto do banco de dados

        public CarrinhoCompra(AppDbContext context) // Construtor da classe que recebe o contexto do banco de dados como parâmetro
        {
            _context = context; // Inicializa o contexto com o valor passado
        }

        public string CarrinhoCompraId { get; set; } // Propriedade para armazenar o ID do carrinho de compras
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; } // Propriedade para armazenar os itens do carrinho

        /* Persistência da Sessão: O método utiliza a sessão do usuário para associar o carrinho de compras. Isso garante que cada usuário tenha
        um carrinho exclusivo que persiste enquanto a sessão está ativa. Dessa forma, o carrinho não é perdido se o usuário navega para outras
        páginas do site */
        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            // Define uma sessão
            // A sessão é usada para armazenar e recuperar o ID do carrinho associado ao usuário atual
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // Obtem um serviço do tipo do nosso contexto 
            // Permite que a instância de CarrinhoCompra interaja com o banco de dados para realizar operações como adicionar ou remover itens do carrinho
            var context = services.GetService<AppDbContext>();

            // Obtem ou gera o Id do carrinho
            // Assegura que cada usuário tem um ID de carrinho único, essencial para identificar o carrinho de cada usuário
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // Atribui o id do carrinho na Sessão
            // Mantém o ID do carrinho disponível para futuras requisições, permitindo persistência do carrinho enquanto a sessão estiver ativa
            // Se um novo ID foi gerado, ele precisa ser salvo na sessão. Se um ID existente foi recuperado, esta linha reforça seu armazenamento
            session.SetString("CarrinhoId", carrinhoId);

            // Retorna o carrinho com o contexto e o Id atribuido ou obtido
            /* Fornece uma instância de CarrinhoCompra pronta para uso, com o contexto e o ID configurados, permitindo que outras partes do código
            acessem e manipulem o carrinho de compras do usuário */
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        // Método para adicionar um lanche ao carrinho
        public void AdicionarAoCarrinho(Lanche lanche)
        {
            // Procura um item no carrinho que corresponda ao lanche e ao ID do carrinho
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                     s => s.Lanche.LancheId == lanche.LancheId &&
                     s.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null) // Se o item não existir no carrinho
            {
                // Cria um novo item do carrinho
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem); // Adiciona o item ao contexto
            }
            else
            {
                carrinhoCompraItem.Quantidade++; // Incrementa a quantidade do item existente
            }
            _context.SaveChanges(); // Salva as mudanças no contexto
        }

        public int RemoverDoCarrinho(Lanche lanche) // Método para remover um lanche do carrinho
        {
            // Procura um item no carrinho que corresponda ao lanche e ao ID do carrinho
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                   s => s.Lanche.LancheId == lanche.LancheId &&
                   s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null)
            {
                // Se houver mais de um item do mesmo lanche, reduz a quantidade
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    // Se houver apenas um item, remove-o completamente do carrinho
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            // Salva as alterações no contexto do banco de dados
            _context.SaveChanges();
            // Retorna a quantidade local atualizada (pode ser útil para notificações ou controle de interface)
            return quantidadeLocal;
        }

        /* Método para obter os itens do carrinho de compras associados a este CarrinhoCompraId,
        carregando-os do banco de dados se ainda não estiverem carregados. */
        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItems ??
                   (CarrinhoCompraItems =
                       _context.CarrinhoCompraItens
                           .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                           .Include(s => s.Lanche)
                           .ToList());
        }

        // Método para limpar todos os itens do carrinho de compras associados a este CarrinhoCompraId do banco de dados.
        public void LimparCarrinho()
        {
            // Obtém todos os itens do carrinho associados a este CarrinhoCompraId
            var carrinhoItens = _context.CarrinhoCompraItens
                                 .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);
            // Remove todos os itens do carrinho encontrados
            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            // Salva as alterações no banco de dados
            _context.SaveChanges();
        }

        // Método para obter o total da compra no carrinho de compras associado a este CarrinhoCompraId.
        public decimal GetCarrinhoCompraTotal()
        {
            // Calcula o total da compra somando os preços dos itens multiplicados pela quantidade
            var total = _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade)
                .Sum();
            return total;
        }
    }
}
