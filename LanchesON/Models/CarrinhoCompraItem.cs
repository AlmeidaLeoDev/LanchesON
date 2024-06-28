using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    // Especifica que esta classe mapeia para a tabela "CarrinhoCompraItens" no banco de dados
    [Table("CarrinhoCompraItens")]
    public class CarrinhoCompraItem
    {
        // Primary Key
        public int CarrinhoCompraItemId { get; set; }
        // Foreign Key
        // Relacionamento Um-Para-Muito (Um Lanche / Muitos CarrinhoCompraItem)
        /* Por convencao, quando definimos a propriedade como um tipo que representa uma tabela já existente 
        (já existe uma entidade e tabela "Lanche" criada), entao será subentendido que queremos uma Foreign Key. Será criada na tabela CarrinhoCompraItens
        uma coluna lancheId, que representa o Id do lanche que foi incluido no carrinho, através desse Id podemos recuperar todas as informaçoes do lanche
        e assim nao é preciso armazenar novamente na tabela informaçoes do lanche ou produto que o usuário selecionou*/
        public Lanche Lanche { get; set; }
        // Quantidade de itens no carrinho
        public int Quantidade { get; set; }
        // Identificador único do carrinho de compras onde os ítens foram colocados
        // Estou usando strign pois o valor que vou atribuir a coluna no banco de dados vai um GUID (identificador global único)
        // A classe CarrinhoCompra ainda nao foi criada, mas já mapeei está propriedade 
        [StringLength(200)]
        public string CarrinhoCompraId { get; set; }
    }
}
