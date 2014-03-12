using System.Collections.Generic;
using System.Web.Mvc;
using Quiron.LojaVirtual.Dominio.Entidades;





namespace Quiron.LojaVirtual.Dominio.Repositorio
{
    public class ProdutosRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();

        public IEnumerable<Produto> Produtos
        {
            get { return _context.Produtos; }
        }

        public void Salvar(Produto produto)
        {
            if (produto.ProdutoId == 0)
            {
                _context.Produtos.Add(produto);
            }
            else
            {
                Produto prod = _context.Produtos.Find(produto.ProdutoId);
                if (prod != null)
                {
                    prod.Nome = produto.Nome;
                    prod.Descricao = produto.Descricao;
                    prod.Preco = produto.Preco;
                    prod.Categoria = produto.Categoria;
                    prod.Imagem = produto.Imagem;
                    prod.ImagemMimeType = produto.ImagemMimeType;
                }
            }
            _context.SaveChanges();
        }


        public Produto Excluir(int produtoId)
        {
            Produto prod = _context.Produtos.Find(produtoId);
            if (prod != null)
            {
                _context.Produtos.Remove(prod);
                _context.SaveChanges();
            }
            return prod;
        }

      
    }

}

