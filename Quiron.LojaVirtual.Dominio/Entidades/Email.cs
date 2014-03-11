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

        private EmailConfiguraçoes emailConfiguracoes;

        public EmailPedido(EmailConfiguraçoes emailConfiguracoes)
        {
            this.emailConfiguracoes = emailConfiguracoes;
        }

        public void ProcessarPedido(Carrinho carrinho, Pedido pedido)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailConfiguracoes.UsarSsl;
                smtpClient.Host = emailConfiguracoes.ServidorSmtp;
                smtpClient.Port = emailConfiguracoes.ServidorPorta;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailConfiguracoes.Usuario,emailConfiguracoes.ServidorSmtp);

                if (emailConfiguracoes.EscreverArquivo)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailConfiguracoes.PastaArquivo;
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

                MailMessage mailMessage = new MailMessage(emailConfiguracoes.De,emailConfiguracoes.Para, "Novo pedido!", body.ToString());
                
                if (emailConfiguracoes.EscreverArquivo)
                {
                    mailMessage.BodyEncoding = Encoding.GetEncoding("ISO-8859-1"); 
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}