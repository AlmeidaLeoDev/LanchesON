using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LanchesON.TagHelpers 
{
    public class EmailTagHelper : TagHelper
    {
        // Propriedade pública que armazena o endereço de email
        public string Endereco { get; set; }
        // Propriedade pública que armazena o conteúdo a ser exibido dentro da tag <a>
        public string Conteudo { get; set; }

        // Sobrescreve o método Process da classe base TagHelper
        // Este método é chamado para processar a tag e modificar sua saída
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Define o nome da tag HTML que será gerada. Neste caso, substitui a tag original por <a>
            output.TagName = "a";
            // Adiciona ou define o atributo href da tag <a> com um link de email formatado usando o valor da propriedade Endereco
            output.Attributes.SetAttribute("href", "mailto:" + Endereco);
            // Define o conteúdo interno da tag <a> usando o valor da propriedade Conteudo
            output.Content.SetContent(Conteudo);
        }
    }
}
