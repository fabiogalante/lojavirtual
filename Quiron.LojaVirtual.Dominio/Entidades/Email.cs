using System.Net;
using System.Net.Mail;
using System.Text;

namespace Quiron.LojaVirtual.Dominio.Entidades
{
    public class EmailConfiguraçoes
    {

        public string Para = "pedido@quiron.com.br";
        public string De = "quiron@quiron.com.br";
        public bool UsarSsl = true;
        public string Usuario = "quiron";
        public string Senha = "senhaquiron";
        public string ServidorSmtp = "smtp.quiron.com.br";
        public int ServidorPorta = 587;
        public bool EscreverArquivo = false;
        public string PastaArquivo = @"c:\envioemail";
    }

    public class EmailPedido
    {

        private readonly EmailConfiguraçoes _emailConfiguracoes;

        public EmailPedido(EmailConfiguraçoes emailConfiguracoes)
        {
            this._emailConfiguracoes = emailConfiguracoes;
        }

        public void ProcessarPedido(Carrinho carrinho, Pedido pedido)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = _emailConfiguracoes.UsarSsl;
                smtpClient.Host = _emailConfiguracoes.ServidorSmtp;
                smtpClient.Port = _emailConfiguracoes.ServidorPorta;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_emailConfiguracoes.Usuario,_emailConfiguracoes.ServidorSmtp);

                if (_emailConfiguracoes.EscreverArquivo)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = _emailConfiguracoes.PastaArquivo;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder()
                .AppendLine("Novo pedido:")
                .AppendLine("---")
                .AppendLine("Itens:");
                foreach (var item in carrinho.ItensCarrinho)
                {
                    var subtotal = item.Produto.Preco * item.Quantidade;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c}",
                    item.Quantidade,
                    item.Produto.Nome,
                    subtotal);
                }
                body.AppendFormat("Valor total do pedido: {0:c}",carrinho.ObterValorTotal())
                .AppendLine("-----------------")
                .AppendLine("Enviar para:")
                .AppendLine(pedido.NomeCliente)
                .AppendLine(pedido.Email)
                .AppendLine(pedido.Endereco ?? "")
                .AppendLine(pedido.Cidade ?? "")
                .AppendLine(pedido.Estado)
                .AppendLine(pedido.Cep ?? "")
                .AppendLine(pedido.Complemento)
                .AppendLine("-----------------")
                .AppendFormat("Para presente?: {0}", pedido.EmbrulharPresente ? "Sim" : "Não");

                MailMessage mailMessage = new MailMessage(_emailConfiguracoes.De,_emailConfiguracoes.Para, "Novo pedido!", body.ToString());
                
                if (_emailConfiguracoes.EscreverArquivo)
                {
                    mailMessage.BodyEncoding = Encoding.GetEncoding("ISO-8859-1"); 
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}