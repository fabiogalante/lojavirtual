using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quiron.LojaVirtual.Dominio.Entidades;
using Quiron.LojaVirtual.Dominio.Repositorio;

namespace Quiron.LojaVirtual.Web.Areas.Administrativo.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private ProdutosRepositorio _repositorio;
        public ActionResult Index()
        {
            _repositorio = new ProdutosRepositorio();
            var produtos = _repositorio.Produtos;
            return View(produtos);
        }


        public ViewResult NovoProduto()
        {
            return View("Alterar", new Produto());
        }


        public ViewResult Alterar(int produtoId)
        {
            _repositorio = new ProdutosRepositorio();
            Produto produto = _repositorio.Produtos
            .FirstOrDefault(p => p.ProdutoId == produtoId);
            return View(produto);
        }


        //[HttpPost] 
        //public ActionResult Alterar(Produto produto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _repositorio = new ProdutosRepositorio();
        //        _repositorio.Salvar(produto);
        //        TempData["mensagem"] = string.Format("{0} foi salvo com sucesso!", produto.Nome);
        //        return RedirectToAction("Index");
        //    }

        //    //Erro
        //    return View(produto);
        //}



        //Com a imagem
        [HttpPost]
        public ActionResult Alterar(Produto produto, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    produto.ImagemMimeType = image.ContentType;
                    produto.Imagem = new byte[image.ContentLength];
                    image.InputStream.Read(produto.Imagem, 0, image.ContentLength);
                }


                _repositorio = new ProdutosRepositorio();
                _repositorio.Salvar(produto);
                TempData["mensagem"] = string.Format("{0} foi salvo com sucesso!", produto.Nome);
                return RedirectToAction("Index");
            }

            //Erro
            return View(produto);
        }


        //[HttpPost]
        //public ActionResult Excluir(int produtoId)
        //{
        //    _repositorio = new ProdutosRepositorio();
        //    Produto prod = _repositorio.Excluir(produtoId);
        //    if (prod != null)
        //    {
        //        TempData["mensagem"] = string.Format("{0} excluído com sucesso", prod.Nome);
        //    }
        //    return RedirectToAction("Index");
        //}


        [HttpPost]
        public JsonResult Excluir(int produtoId)
        {
            string mensagem = null;
            _repositorio = new ProdutosRepositorio();
            Produto prod = _repositorio.Excluir(produtoId);
            if (prod != null)
            {
                mensagem = string.Format("{0} excluído com sucesso", prod.Nome);
            }
            return Json(mensagem , JsonRequestBehavior.DenyGet);
        }

    }
}