using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quiron.LojaVirtual.Dominio.Repositorio;

namespace Quiron.LojaVirtual.Web.Controllers
{
    public class CategoriaController : Controller
    {

        private ProdutosRepositorio _repositorio;

        //public string Menu()
        //{
        //    return "Sou o menu de categorias";
        //}

        public PartialViewResult Menu(string categoria = null)
        {

            ViewBag.CategoriaSelecionada = categoria;

            _repositorio = new ProdutosRepositorio();

            IEnumerable<string> categorias = _repositorio.Produtos
                .Select(c => c.Categoria)
                .Distinct()
                .OrderBy(c => c);

            return PartialView(categorias);
        }
	}
}