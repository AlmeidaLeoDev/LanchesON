﻿namespace LanchesMac.Models
{
    public class Lanche
    {
        public int LancheId { get; set; }
        public string Nome { get; set; }
        public string DescricaoCurta { get; set; }
        public string DescricaoDetalhada { get; set; }
        public decimal Preco { get; set; }
        public string ImagemUrl { get; set; }
        public string ImagemThumbnailUrl { get; set; }
        public bool IsLanchePreferido { get; set; }
        public bool EmEstoque { get; set; }

        // Chave estrangeira
        public int CategoriaId { get; set; }
        // Propriedade de navegação
        public virtual Categoria Categoria { get; set; }
    }
}