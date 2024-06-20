using LanchesMac.Context;
using LanchesMac.Models;
using LanchesON.Repositories.Interfaces;

namespace LanchesON.Repositories
{
    // Implementação da interface ICategoriaRepository através da classe CategoriaRepository
    public class CategoriaRepository : ICategoriaRepository
    {
        // Campo somente leitura e privado para que não possa ser modificado fora desta classe
        private readonly AppDbContext _context;

        // Construtor da classe CategoriaRepository que recebe uma instância de AppDbContext como parâmetro
        public CategoriaRepository(AppDbContext context)
        {
            _context = context; // Atribui o contexto ao campo privado _context
        }

        // Implementação da propriedade Categorias definida na interface ICategoriaRepository
        // Retorna todas as categorias da base de dados
        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
