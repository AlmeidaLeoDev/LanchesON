using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

// Declara um namespace chamado LanchesMac.Context, que agrupa logicamente as classes relacionadas ao contexto do banco de dados
namespace LanchesMac.Context
{
    // Define a classe AppDbContext, que herda de DbContext
    // Esta classe representa a sessão com o banco de dados e é usada para configurar e acessar dados
    public class AppDbContext : DbContext
    {
        /* DbContextOptions<TContext>:
           - Classe que contém configurações do banco de dados (string de conexão, provedores, etc.) 
           - DbContextOptions<AppDbContext> significa que essas configurações são específicas para o contexto AppDbContext.
         */

        /* Construtor:
           -- Parâmetro: DbContextOptions<AppDbContext> options
                - O parâmetro options recebe uma instância de DbContextOptions<AppDbContext> contendo as configurações para o contexto.
           -- Chamada ao Construtor da Classe Base: : base(options)
                - base(options) chama o construtor da classe base (DbContext) e passa as configurações (options) para ele.
                - Isso garante que o DbContext seja configurado corretamente com as opções fornecidas.
         */

        /* Quando você herda de uma classe (neste caso, DbContext), a classe base (DbContext) pode precisar de algumas configurações
         específicas para funcionar corretamente. Passar o parâmetro options para : base(options) garante que a classe base receba
         essas configurações e seja inicializada corretamente. */
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            /* O corpo do construtor está vazio, pois toda a configuração necessária 
               já foi feita pela classe base com o 'options' passado 
            */
        }
        // Define uma propriedade DbSet para a entidade Categoria
        // Um DbSet representa uma coleção de todas as entidades no contexto, ou que podem ser consultadas do banco de dados, de um determinado tipo
        public DbSet<Categoria> Categorias { get; set; }
        // Define uma propriedade DbSet para a entidade Lanche
        public DbSet<Lanche> Lanches { get; set; }
        // Define uma propriedade DbSet para a entidade CarrinhoCompraItens
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

    }
}
