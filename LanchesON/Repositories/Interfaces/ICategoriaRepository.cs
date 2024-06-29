using LanchesON.Models;

namespace LanchesON.Repositories.Interfaces
{
    // Definição de uma interface chamada ICategoriaRepository
    public interface ICategoriaRepository
    {
        // Propriedade que retorna todas as categorias em uma coleção de IEnumerable<Categoria>
        // IEnumerable é uma interface que permite a iteração sobre uma coleção de objetos do tipo Categoria
        IEnumerable<Categoria> Categorias { get; }
    }
}
