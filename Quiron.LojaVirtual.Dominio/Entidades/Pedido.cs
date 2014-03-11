using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiron.LojaVirtual.Dominio.Entidades
{
    public class Pedido
    {
        [Required(ErrorMessage = "Informe seu nome")]
        public string NomeCliente { get; set; }

        public string Cep { get; set; }

        [Required(ErrorMessage = "Informe seu endereço")]
        public string Endereco { get; set; }
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Informe sua cidade")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Informe seu estado")]
        public string Estado { get; set; }
        public bool EmbrulharPresente { get; set; }
    }
}
