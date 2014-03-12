namespace Quiron.LojaVirtual.Dominio.Entidades
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class Produto
    {
        [HiddenInput(DisplayValue = false)]
        public int ProdutoId { get; set; }


        [Required(ErrorMessage = "Digite o nome do projeto")]
        public string Nome { get; set; }

        [DataType(DataType.MultilineText)]


        [Required(ErrorMessage = "Digite a descrição")]
        public string Descricao { get; set; }


        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor inválido")]
        public decimal Preco { get; set; }


        [Required(ErrorMessage = "Selecione uma categoria")]
        public string Categoria { get; set; }


        public byte[] Imagem { get; set; }
        public string ImagemMimeType { get; set; }
    }
}
