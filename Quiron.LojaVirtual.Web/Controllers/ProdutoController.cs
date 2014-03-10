using System.Web.Mvc;
using Quiron.LojaVirtual.Dominio.Repositorio;

namespace Quiron.LojaVirtual.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private ProdutosRepositorio _repositorio;
      

        public ActionResult ListaProdutos()
        {
            _repositorio = new ProdutosRepositorio();
            return View(_repositorio.Produtos);
        }
	}
}