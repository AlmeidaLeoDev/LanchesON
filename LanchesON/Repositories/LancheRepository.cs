using LanchesON.Context;
using LanchesON.Models;
using LanchesON.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesON.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        // Campo somente leitura e privado para que não possa ser modificado fora desta classe
        private readonly AppDbContext _context;

        // Construtor da classe LancheRepository que recebe uma instância de AppDbContext como parâmetro
        public LancheRepository(AppDbContext context)
        {
            _context = context; // Atribui o contexto ao campo privado _context
        }

        // Implementação da propriedade Lanches definida na interface ILancheRepository
        // Retorna todos os lanches da base de dados, incluindo as informações de suas categorias
        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

        // Implementação da propriedade LanchesPreferidos definida na interface ILancheRepository
        // Retorna todos os lanches preferidos da base de dados, ou seja, aqueles que têm IsLanchePreferido como verdadeiro
        // Inclui também as informações de suas categorias
        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches
                                                                .Where(l => l.IsLanchePreferido)
                                                                .Include(c => c.Categoria);

        // Implementação do método GetLancheById definido na interface ILancheRepository
        // Retorna um lanche específico baseado no ID fornecido, ou null se não encontrar
        public Lanche GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(i => i.LancheId == lancheId);
        }
    }
}
