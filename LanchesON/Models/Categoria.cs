namespace LanchesMac.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string CategoriaNome { get; set; }
        public string Descricao { get; set; }

        // Uma categoria possui muitos lanches
        // Propriedade de navegação
        public List<Lanche> Lanches { get; set; }
    }
}
