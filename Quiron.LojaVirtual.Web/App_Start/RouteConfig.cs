using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Quiron.LojaVirtual.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //routes.MapRoute(
            //    name: null,
            //    url: "Pagina{pagina}",
            //    defaults: new { Controller = "Produto", action = "ListaProdutos" }
            //    );

          

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Produto", action = "ListaProdutos", id = UrlParameter.Optional }
            //);



            // 1 - Home

            routes.MapRoute(null, "",
                new {controller = "Produto", action = "ListaProdutos", categoria = (string) null, pagina = 1});


            // 2 - 
            routes.MapRoute(null,
                "Pagina{pagina}",
                new {controller = "Produto", action = "ListaProdutos", categoria = (string) null}, new {pagina = @"\d+"}
                );


            routes.MapRoute(null,
                "{categoria}", new {controller = "Produto", action = "ListaProdutos", pagina = 1}
                );



            routes.MapRoute(null,
                "{categoria}/Pagina{pagina}",new {controller = "Produto", action = "ListaProdutos"},new {pagina = @"\d+"}
                );


            routes.MapRoute(null, "{controller}/{action}");







        }
    }
}
