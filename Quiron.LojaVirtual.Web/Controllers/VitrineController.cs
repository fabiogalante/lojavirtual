﻿using System.Linq;
using System.Web.Mvc;
using Quiron.LojaVirtual.Dominio.Entidades;
using Quiron.LojaVirtual.Dominio.Repositorio;
using Quiron.LojaVirtual.Web.Models;

namespace Quiron.LojaVirtual.Web.Controllers
{
    public class VitrineController : Controller
    {
        private ProdutosRepositorio _repositorio;
        public int ProdutosPorPagina = 3;


        public ViewResult ListaProdutos(string categoria, int pagina = 1)
        {
            _repositorio = new ProdutosRepositorio();

            ProdutosViewModel model = new ProdutosViewModel
            {
                Produtos = _repositorio.Produtos
                .Where(p => categoria == null || p.Categoria == categoria)
                .OrderBy(p => p.ProdutoId)
                .Skip((pagina - 1) * ProdutosPorPagina)
                .Take(ProdutosPorPagina),

                Paginacao = new Paginacao
                {
                    PaginaAtual = pagina,
                    ItensPorPagina = ProdutosPorPagina,
                    //ItensTotal = _repositorio.Produtos.Count()
                    ItensTotal = categoria == null ? _repositorio.Produtos.Count() : _repositorio.Produtos.Count(e => e.Categoria == categoria)
                },

                CategoriaAtual = categoria
           
            };
            return View(model);
        }



        public FileContentResult ObterImagem(int produtoId)
        {
            _repositorio = new ProdutosRepositorio();
            Produto prod = _repositorio.Produtos
            .FirstOrDefault(p => p.ProdutoId == produtoId);
            if (prod != null)
            {
                return File(prod.Imagem, prod.ImagemMimeType);
            }

            return null;
        }
	}
}