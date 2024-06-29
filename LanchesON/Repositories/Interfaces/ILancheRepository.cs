using LanchesON.Models;

namespace LanchesON.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        // Propriedade que retorna todos os lanches em uma coleção de IEnumerable<Lanche>
        // IEnumerable é uma interface que permite a iteração sobre uma coleção de objetos do tipo Lanche
        IEnumerable<Lanche> Lanches { get; }
        // Propriedade que retorna todos os lanches preferidos em uma coleção de IEnumerable<Lanche>
        // Similar à propriedade anterior, mas focada em lanches preferidos
        IEnumerable<Lanche> LanchesPreferidos { get; }
        // Método que retorna um lanche específico baseado no ID do lanche
        // Recebe um parâmetro do tipo int que representa o ID do lanche e retorna um objeto do tipo Lanche
        Lanche GetLancheById(int lancheId); 
    }
}
